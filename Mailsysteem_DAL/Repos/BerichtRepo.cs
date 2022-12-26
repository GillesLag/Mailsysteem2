using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Mailsysteem_DAL
{
    public class BerichtRepo : BaseRepository, IBerichtRepo
    {
        public bool InsertBericht(Bericht bericht)
        {
            int affectedRows;
            string sql = @"INSERT INTO Mailsysteem.Bericht (onderwerp, datumVerstuurd, berichtTekst, bijlage, verzenderId, isVerwijderd)
                            VALUES (@onderwerp, @datumVerstuurd, @berichtTekst, @bijlage, @verzenderId, 0);";

            var parameters = new
            {
                @onderwerp = bericht.onderwerp,
                @datumVerstuurd = bericht.datumVerstuurd,
                @berichtTekst = bericht.berichtTekst,
                @bijlage = bericht.bijlage,
                @verzenderId = bericht.verzenderId,
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                affectedRows = db.Execute(sql, parameters);
            }


            if (affectedRows == 0)
                return false;

            return true;
        }

        public List<Bericht> OphalenBerichten(int gebruikerId)
        {
            string sql = $@"SELECT b.*, g.*, bo.*, og.*
                                    FROM Mailsysteem.Bericht AS b INNER JOIN Mailsysteem.Gebruiker AS g ON b.verzenderId = g.id
                                    INNER JOIN Mailsysteem.BerichtOntvanger AS bo ON bo.berichtId = b.id
                                    INNER JOIN Mailsysteem.Gebruiker AS og ON bo.gebruikerId = og.id
                                    WHERE b.verzenderId = {gebruikerId} OR b.id IN (
										SELECT berichtId
										FROM Mailsysteem.BerichtOntvanger
										WHERE gebruikerId = {gebruikerId}
									)";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dictionary<int, Bericht> opslagBoek = new Dictionary<int, Bericht>();

                return db.Query<Bericht, Gebruiker, BerichtOntvanger, Gebruiker, Bericht>(sql, (b, g, bo, og) =>
                {
                    Bericht bericht;
                    if (!opslagBoek.TryGetValue(b.id, out bericht))
                    {
                        bericht = b;
                        if (bericht.Gebruiker == null)
                            bericht.Gebruiker = g;

                        if (bericht.BerichtOntvanger == null)
                            bericht.BerichtOntvanger = new List<BerichtOntvanger>();

                        opslagBoek.Add(bericht.id, bericht);
                    }

                    bo.Gebruiker = og;
                    bericht.BerichtOntvanger.Add(bo);

                    return bericht;

                }, splitOn: "id").Distinct().ToList();
            }
        }

        public bool UpdateBericht(Bericht bericht)
        {
            int affectedRows;
            string sql = $@"UPDATE Mailsysteem.Bericht
                            SET isVerwijderd = @isVerwijderd
                            WHERE id = @id";

            var parameters = new
            {
                @isVerwijderd = bericht.isVerwijderd,
                @id = bericht.id
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                affectedRows = db.Execute(sql, parameters);
            }

            if (affectedRows == 0)
                return false;

            return true;
        }
    }
}
