using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL
{
    public interface IBerichtRepo
    {
        List<Bericht> OphalenBerichten(int gebruikerId);
        bool InsertBericht(Bericht bericht);
        bool UpdateBericht(Bericht bericht);
    }
}
