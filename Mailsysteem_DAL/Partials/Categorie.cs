using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL
{
    public partial class Categorie
    {
        public override bool Equals(object obj)
        {
            return obj is Categorie categorie &&
                   id == categorie.id &&
                   naam == categorie.naam;
        }

        public override int GetHashCode()
        {
            int hashCode = 83964746;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(naam);
            return hashCode;
        }

        public override string ToString()
        {
            return naam;
        }


    }
}
