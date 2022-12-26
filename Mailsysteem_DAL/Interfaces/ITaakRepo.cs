using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailsysteem_DAL
{
    public interface ITaakRepo
    {
        List<Taak> OphalenTaken(int gebruikerId);
        bool InsertTaak(Taak taak);
        bool UpdateTaak(Taak taak);
        bool DeleteTaak(Taak taak);
    }
}
