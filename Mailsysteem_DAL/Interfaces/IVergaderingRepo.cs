using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL
{
    public interface IVergaderingRepo
    {
        List<Vergadering> OphalenVergaderingen(int gebruikerId);
        bool InsertVergadering(Vergadering vergadering);
    }
}
