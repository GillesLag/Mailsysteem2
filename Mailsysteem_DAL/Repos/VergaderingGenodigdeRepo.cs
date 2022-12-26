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
    public class VergaderingGenodigdeRepo : BaseRepository, IVergaderingGenodigdeRepo
    {
        public int GetGebruikerId(string emailGebruiker)
        {
            string sql = $"SELECT id FROM Mailsysteem.Gebruiker WHERE email = \'{emailGebruiker}\'";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                try
                {
                    return db.QuerySingle<int>(sql);
                }
                catch (Exception)
                {

                    return -1;
                }
            }
        }

        public void InsertVergaderingGenodigde(List<string> emails)
        {
            string sql = $@"INSERT INTO Mailsysteem.VergaderingGenodigde(vergaderingId, gebruikerId)
                            VALUES((SELECT TOP 1 id FROM Mailsysteem.Vergadering ORDER BY id DESC), @gebruikerId)";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                foreach (string email in emails)
                {
                    var parameters = new
                    {
                        @gebruikerId = GetGebruikerId(email)
                    };

                    db.Execute(sql, parameters);
                }
            }
        }
    }
}
