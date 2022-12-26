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
    public class CategorieRepo : BaseRepository, ICategorieRepo
    {
        public List<Categorie> OphalenCategorieën()
        {
            string sql = "SELECT * FROM Mailsysteem.Categorie";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Categorie>(sql).Distinct().ToList();
            }
        }
    }
}
