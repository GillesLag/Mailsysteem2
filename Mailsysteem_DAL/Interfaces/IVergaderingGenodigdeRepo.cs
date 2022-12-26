using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL
{
    public interface IVergaderingGenodigdeRepo
    {
        void InsertVergaderingGenodigde(List<string> emails);
        int GetGebruikerId(string emailGebruiker);
    }
}
