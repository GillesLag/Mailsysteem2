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
    public class TaakRepo : BaseRepository, ITaakRepo
    {
        public bool DeleteTaak(Taak taak)
        {
            int affectedRows;
            string sql = @"DELETE FROM Mailsysteem.Taak WHERE id = @id";

            var parameters = new
            {
                @id = taak.id
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                affectedRows = db.Execute(sql, parameters);
            }

            if (affectedRows == 0)
                return false;

            return true;
        }

        public bool InsertTaak(Taak taak)
        {
            int affectedRows;
            string sql = $@"INSERT INTO Mailsysteem.Taak (naam, isVoltooid, herinneringDatum, eindDatum, gebruikerId, extraInfo)
                            VALUES(@naam, @isVoltooid, @herinneringDatum, @eindDatum, @gebruikerId, @extraInfo)";

            var parameters = new
            {
                @naam = taak.naam,
                @isVoltooid = taak.isVoltooid,
                @herinneringDatum = taak.herinneringDatum,
                @eindDatum = taak.eindDatum,
                @gebruikerId = taak.gebruikerId,
                @extraInfo = taak.extraInfo
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                affectedRows = db.Execute(sql, parameters);
            }

            if (affectedRows == 0)
                return false;

            return true;
        }

        public List<Taak> OphalenTaken(int gebruikerId)
        {
            string sql = $@"SELECT t.*, tc.*, c.*
                FROM Mailsysteem.Taak AS t FULL OUTER JOIN Mailsysteem.TaakCategorie AS tc ON t.id = tc.taakId
                FULL OUTER JOIN Mailsysteem.Categorie AS c ON c.id = tc.categorieId
                WHERE t.gebruikerId = {gebruikerId}";

            Dictionary<int, Taak> opslagBoek = new Dictionary<int, Taak>();

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Taak, TaakCategorie, Categorie, Taak>(sql, (t, tc, c) =>
                {
                    Taak taak;

                    if (!opslagBoek.TryGetValue(t.id, out taak))
                    {
                        taak = t;
                        if (taak.TaakCategorie == null)
                            taak.TaakCategorie = new List<TaakCategorie>();

                        opslagBoek.Add(t.id, taak);
                    }


                    if (tc != null)
                    {
                        tc.Categorie = c;
                        tc.Taak = taak;
                        taak.TaakCategorie.Add(tc);
                    }

                    return taak;

                }, splitOn: "id").Distinct().ToList();
            }
        }

        public bool UpdateTaak(Taak taak)
        {
            string sql;
            int affectedRows;

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (taak.herinneringDatum == null)
                {
                    sql = @"UPDATE Mailsysteem.Taak SET naam = @naam,
                            isVoltooid = @isVoltooid,
                            eindDatum = @eindDatum,
                            gebruikerId = @gebruikerId,
                            extraInfo = @extraInfo
                        WHERE id = @id";

                    var parameters = new
                    {
                        @naam = taak.naam,
                        @isVoltooid = taak.isVoltooid,
                        @eindDatum = taak.eindDatum,
                        @gebruikerId = taak.gebruikerId,
                        @extraInfo = taak.extraInfo,
                        @id = taak.id
                    };

                    affectedRows = db.Execute(sql, parameters);
                }

                else
                {
                    sql = @"UPDATE Mailsysteem.Taak SET naam = @naam,
                            isVoltooid = @isVoltooid,
                            herinneringDatum = @herinneringDatum,
                            eindDatum = @eindDatum,
                            gebruikerId = @gebruikerId,
                            extraInfo = @extraInfo
                        WHERE id = @id";

                    var parameters = new
                    {
                        @naam = taak.naam,
                        @isVoltooid = taak.isVoltooid,
                        @herinneringDatum = taak.herinneringDatum,
                        @eindDatum = taak.eindDatum,
                        @gebruikerId = taak.gebruikerId,
                        @extraInfo = taak.extraInfo,
                        @id = taak.id
                    };

                    affectedRows = db.Execute(sql, parameters);
                }
            }

            if (affectedRows == 1)
                return true;

            return false;
        }
    }
}
