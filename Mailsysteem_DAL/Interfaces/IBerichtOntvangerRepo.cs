using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL
{
    public interface IBerichtOntvangerRepo
    {
        void InsertBerichtOntvangers(string strOntvangers, string strCc);
        int GetGebruikerId(string emailGebruiker);
    }
}
