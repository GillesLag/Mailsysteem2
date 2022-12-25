using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL
{
    public partial class Taak : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(naam) && string.IsNullOrWhiteSpace(naam))
                    return "Titel moet ingevuld zijn!";

                if (columnName == nameof(eindDatum) && eindDatum == null)
                    return "Einddatum moet ingevuld zijn!";

                if (columnName == nameof(herinneringDatum) && herinneringDatum != null)
                {
                    if (DateTime.Compare(eindDatum, herinneringDatum.Value) <= 0)
                        return "EindDatum kan niet voor herinneringsdatum liggen!";
                }

                return "";
            }
        }

        public string TaakTekst 
        {
            get
            {
                if (extraInfo == null)
                    return "";

                if (extraInfo.Length > 20)
                    return extraInfo.Substring(0, 20);

                return extraInfo;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Taak taak &&
                   id == taak.id &&
                   gebruikerId == taak.gebruikerId;
        }

        public override int GetHashCode()
        {
            int hashCode = 1421852382;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + gebruikerId.GetHashCode();
            return hashCode;
        }
    }
}
