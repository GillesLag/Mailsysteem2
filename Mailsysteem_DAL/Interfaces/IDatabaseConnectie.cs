using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL.Interfaces
{
    public interface IDatabaseConnectie
    {
        void Connecteren();
        IDbConnection Connectie { get; set; }
        void Open();
        void Close();
    }
}
