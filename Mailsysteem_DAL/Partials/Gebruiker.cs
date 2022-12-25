using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL
{
    public partial class Gebruiker
    {
        public string VolledigeNaam
        {
            get { return voornaam + " " + naam; }
        }
    }
}
