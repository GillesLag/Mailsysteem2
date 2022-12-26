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
    public class GebruikerRepo : BaseRepository, IGebruikerRepo
    {
        public List<Gebruiker> OphalenGebruikers()
        {
            string sql = "SELECT * FROM Mailsysteem.Gebruiker";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Gebruiker>(sql).ToList();
            }
        }
    }
}
