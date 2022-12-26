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
    public class BerichtOntvangerRepo : BaseRepository, IBerichtOntvangerRepo
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

        public void InsertBerichtOntvangers(string strOntvangers, string strCc)
        {
            List<string> ontvangers = strOntvangers.Trim().Split(';').Where(x => !(string.IsNullOrWhiteSpace(x))).Select(x => x.Trim()).ToList();
            List<string> ccOntvangers = strCc.Trim().Split(';').Where(x => !(string.IsNullOrWhiteSpace(x))).Select(x => x.Trim()).ToList();
            string sql = $"INSERT INTO Mailsysteem.berichtOntvanger (berichtId, gebruikerId, isCC)" +
                            $" VALUES((SELECT TOP 1 id FROM Mailsysteem.Bericht ORDER BY id DESC), (@gebruikerId), @isCC);";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                foreach (string ontvanger in ontvangers)
                {
                    var parameters = new
                    {
                        @gebruikerId = GetGebruikerId(ontvanger),
                        @isCC = 0
                    };

                    db.Execute(sql, parameters);
                }

                foreach (string cc in ccOntvangers)
                {
                    var parameters = new
                    {
                        @gebruikerId = GetGebruikerId(cc),
                        @isCC = 1
                    };

                    db.Execute(sql, parameters);
                }
            }
        }
    }
}
