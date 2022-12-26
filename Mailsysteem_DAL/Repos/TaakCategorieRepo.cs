using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Mailsysteem_DAL
{
    // Voor één of ander reden moet dit een partial class zijn. Anders gaat Visual Studio een error weergeven.
    public partial class TaakCategorieRepo : BaseRepository, ITaakCategorieRepo
    {
        public int GetCategorieId(string categorieNaam)
        {
            string sql = $"SELECT id FROM Mailsysteem.Categorie WHERE naam = \'{categorieNaam}\'";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.QuerySingle<int>(sql);
            }
        }

        public void InsertTaakCategorie(Taak taak, string categorieën)
        {
            List<string> cats = categorieën.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            string sql = $@"INSERT INTO Mailsysteem.TaakCategorie (taakId, categorieId)
                            VALUES((SELECT TOP 1 id FROM Mailsysteem.Taak ORDER BY id DESC), @categorieId)";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                foreach (string cat in cats)
                {
                    var parameters = new
                    {
                        @categorieId = GetCategorieId(cat)
                    };

                    db.Execute(sql, parameters);
                }
            }
        }
    }
}
