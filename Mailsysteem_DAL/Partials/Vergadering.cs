using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL
{
    public partial class Vergadering : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(datum) && datum == null)
                    return "Datum moet ingevuld zijn!";

                if (columnName == nameof(beginTijd) && beginTijd == null)
                    return "Begintijd moet ingevuld zijn!";

                if (columnName == nameof(eindTijd) && eindTijd == null)
                    return "Eindtijd moet ingevuld zijn!";

                if (columnName == nameof(eindTijd) && eindTijd != null && beginTijd != null)
                {
                    if (TimeSpan.Compare(eindTijd, beginTijd) <= 0)
                        return "Eindtijd kan niet voor begintijd liggen!";
                }

                if (columnName == nameof(plaats) && string.IsNullOrWhiteSpace(plaats))
                    return "Locatie moet ingevuld zijn";

                return "";
            }
        }
        public string KorteDatum 
        { 
            get
            {
                return datum.ToShortDateString();
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Vergadering vergadering &&
                   id == vergadering.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}
