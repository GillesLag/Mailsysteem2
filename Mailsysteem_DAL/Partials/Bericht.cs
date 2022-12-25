using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL
{
    public partial class Bericht : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(verzenderId) && verzenderId == null)
                    return "VerzenderId moet ingevuld zijn!";

                if (columnName == nameof(datumVerstuurd) && datumVerstuurd == null)
                    return "DatumVerstuurd moet ingevuld zijn!";

                return "";
            }
        }

        public string mailItemTekst 
        {
            get
            {
                if (berichtTekst == null)
                    return "";

                if (berichtTekst.Length > 20)
                    return berichtTekst.Trim().Substring(0, 20);

                return berichtTekst;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Bericht bericht &&
                   id == bericht.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}
