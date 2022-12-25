using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL
{
    public class DatabaseConnectie : Interfaces.IDatabaseConnectie
    {
        public IDbConnection Connectie { get; set; }

        public DatabaseConnectie()
        {
            Connecteren();
        }

        public void Connecteren()
        {
            try
            {
                Connectie = new SqlConnection(ConfigurationManager.ConnectionStrings["MailsysteemEntities"].ConnectionString);
            }
            catch (Exception ex)
            {
                //throw new Exception("Er is een probleem met de SQL-connectie.");
                throw new Exception(ex.ToString());
            }
        }
        public void Open()
        {
            if (Connectie == null) Connecteren();

            Connectie.Open();
        }

        public void Close()
        {
            if (Connectie != null)
                Connectie.Close();
        }
    }
}
