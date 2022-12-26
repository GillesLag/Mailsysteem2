using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Mailsysteem_DAL
{
    public class VergaderingRepo : BaseRepository, IVergaderingRepo
    {
        public bool InsertVergadering(Vergadering vergadering)
        {
            int affectedRows;
            string sql = $@"INSERT INTO Mailsysteem.Vergadering (onderwerp, organisatorId, datum, plaats, beginTijd, eindTijd)
                            VALUES(@onderwerp, @organisatorId, @datum, @plaats, @beginTijd, @eindTijd)";

            var parameters = new
            {
                @onderwerp = vergadering.onderwerp,
                @organisatorId = vergadering.organisatorId,
                @datum = vergadering.datum,
                @plaats = vergadering.plaats,
                @beginTijd = vergadering.beginTijd,
                @eindTijd = vergadering.eindTijd
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                affectedRows = db.Execute(sql, parameters);
            }

            if (affectedRows == 0)
                return false;

            return true;
        }

        public List<Vergadering> OphalenVergaderingen(int gebruikerId)
        {
            string sql = $@"SELECT v.*, vg.*, g.*
                    FROM Mailsysteem.Vergadering AS v INNER JOIN Mailsysteem.VergaderingGenodigde AS vg ON v.id = vg.vergaderingId
	                INNER JOIN Mailsysteem.Gebruiker AS g ON vg.gebruikerId = g.id
                    WHERE vg.vergaderingId IN(
							SELECT v.id
							FROM Mailsysteem.Vergadering AS v INNER JOIN Mailsysteem.VergaderingGenodigde AS vg ON v.id = vg.vergaderingId
							WHERE vg.gebruikerId = {gebruikerId})";

            Dictionary<int, Vergadering> opslagBoek = new Dictionary<int, Vergadering>();

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Vergadering, VergaderingGenodigde, Gebruiker, Vergadering>(sql, (v, vg, g) =>
                {
                    Vergadering vergadering;

                    if (!opslagBoek.TryGetValue(v.id, out vergadering))
                    {
                        vergadering = v;
                        if (vergadering.Gebruiker == null)
                            vergadering.Gebruiker = g;

                        if (vergadering.VergaderingGenodigde == null)
                            vergadering.VergaderingGenodigde = new List<VergaderingGenodigde>();

                        opslagBoek.Add(vergadering.id, vergadering);
                    }

                    if (vg != null)
                    {
                        vg.Vergadering = vergadering;
                        vg.Gebruiker = g;
                        vergadering.VergaderingGenodigde.Add(vg);
                    }

                    return vergadering;

                }, splitOn: "id").Distinct().ToList();
            }
        }
    }
}
