using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Mailsysteem_DAL
{
    public static class DatabaseOperations
    {
        private static DatabaseConnectie _db;

        private static void Start()
        {
            _db = new DatabaseConnectie();
            _db.Open();
        }

        public static List<Gebruiker> OphalenGebruikers()
        {
            Start();
            List<Gebruiker> result = _db.Connectie.Query<Gebruiker>("SELECT * FROM Mailsysteem.Gebruiker").ToList();
            _db.Close();

            return result;
        }

        public static List<Bericht> OphalenBerichten(int gebruikerId)
        {
            Start();
            Dictionary<int, Bericht> opslagBoek = new Dictionary<int, Bericht>();

            List<Bericht> result = _db.Connectie.Query<Bericht, Gebruiker, BerichtOntvanger, Gebruiker, Bericht>($@"SELECT b.*, g.*, bo.*, og.*
                                    FROM Mailsysteem.Bericht AS b INNER JOIN Mailsysteem.Gebruiker AS g ON b.verzenderId = g.id
                                    INNER JOIN Mailsysteem.BerichtOntvanger AS bo ON bo.berichtId = b.id
                                    INNER JOIN Mailsysteem.Gebruiker AS og ON bo.gebruikerId = og.id
                                    WHERE b.verzenderId = {gebruikerId} OR b.id IN (
										SELECT berichtId
										FROM Mailsysteem.BerichtOntvanger
										WHERE gebruikerId = {gebruikerId}
									)", (b, g, bo, og) =>
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

            _db.Close();

            return result;
        }

        /*public static string OphalenVerzender(int verzenderId)
        {
            Start();
            string result = _db.Connectie.QueryFirst<Gebruiker>("SELECT * FROM Mailsysteem.Gebruiker" +
                $" WHERE id = {verzenderId}").email;

            _db.Close();

            return result;
        }*/


        public static List<Taak> OphalenTaken(int gebruikerId)
        {
            Start();
            Dictionary<int, Taak> opslagBoek = new Dictionary<int, Taak>();

            List<Taak> result = _db.Connectie.Query<Taak, TaakCategorie, Categorie, Taak>($@"SELECT t.*, tc.*, c.*
                FROM Mailsysteem.Taak AS t FULL OUTER JOIN Mailsysteem.TaakCategorie AS tc ON t.id = tc.taakId
                FULL OUTER JOIN Mailsysteem.Categorie AS c ON c.id = tc.categorieId
                WHERE t.gebruikerId = {gebruikerId}", (t, tc, c) =>
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

            _db.Close();

            return result;
        }

        public static List<Categorie> OphalenCategorieën()
        {
            Start();

            List<Categorie> result = _db.Connectie.Query<Categorie>("SELECT * FROM Mailsysteem.Categorie").Distinct().ToList();

            _db.Close();

            return result;
        }

        public static List<Vergadering> OphalenVergaderingen(int gebruikerId)
        {
            Start();

            Dictionary<int, Vergadering> opslagBoek = new Dictionary<int, Vergadering>();
            List<Vergadering> result = _db.Connectie.Query<Vergadering, VergaderingGenodigde, Gebruiker, Vergadering>($@"SELECT v.*, vg.*, g.*
                    FROM Mailsysteem.Vergadering AS v INNER JOIN Mailsysteem.VergaderingGenodigde AS vg ON v.id = vg.vergaderingId
	                INNER JOIN Mailsysteem.Gebruiker AS g ON vg.gebruikerId = g.id
                    WHERE vg.vergaderingId IN(
							SELECT v.id
							FROM Mailsysteem.Vergadering AS v INNER JOIN Mailsysteem.VergaderingGenodigde AS vg ON v.id = vg.vergaderingId
							WHERE vg.gebruikerId = {gebruikerId})", (v, vg, g) =>
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

            _db.Close();

            return result;
        }

        public static bool InsertBericht(Bericht bericht)
        {
            string sql = @"INSERT INTO Mailsysteem.Bericht (onderwerp, datumVerstuurd, berichtTekst, bijlage, verzenderId)
                            VALUES (@onderwerp, @datumVerstuurd, @berichtTekst, @bijlage, @verzenderId);";

            var parameters = new
            {
                @onderwerp = bericht.onderwerp,
                @datumVerstuurd = bericht.datumVerstuurd,
                @berichtTekst = bericht.berichtTekst,
                @bijlage = bericht.bijlage,
                @verzenderId = bericht.verzenderId,
            };

            Start();
            
            int rowsAffected = _db.Connectie.Execute(sql, parameters);

            _db.Close();

            if (rowsAffected == 0)
                return false;

            return true;
        }

        public static void InsertBerichtOntvangers(string strOntvangers, string strCc)
        {
            List<string> ontvangers = strOntvangers.Trim().Split(';').Where(x => !(string.IsNullOrWhiteSpace(x))).Select(x => x.Trim()).ToList();
            List<string> ccOntvangers = strCc.Trim().Split(';').Where(x => !(string.IsNullOrWhiteSpace(x))).Select(x => x.Trim()).ToList();
            string sql = $"INSERT INTO Mailsysteem.berichtOntvanger (berichtId, gebruikerId, isCC)" +
                            $" VALUES((SELECT TOP 1 id FROM Mailsysteem.Bericht ORDER BY id DESC), (@gebruikerId), @isCC);";
            Start();

            foreach (string ontvanger in ontvangers)
            {
                var parameters = new
                {
                    @gebruikerId = GetGebruikerId(ontvanger),
                    @isCC = 0
                };

                _db.Connectie.Execute(sql, parameters);
            }

            foreach (string cc in ccOntvangers)
            {
                var parameters = new
                {
                    @gebruikerId = GetGebruikerId(cc),
                    @isCC = 1
                };

                _db.Connectie.Execute(sql, parameters);
            }

            _db.Close();
        }

        public static bool InsertTaak(Taak taak)
        {
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

            Start();

            int affectedRows = _db.Connectie.Execute(sql, parameters);

            if (affectedRows == 0)
                return false;

            return true;
        }

        public static void InsertTaakCategorie(Taak taak, string categorieën)
        {
            List<string> cats = categorieën.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            string sql = $@"INSERT INTO Mailsysteem.TaakCategorie (taakId, categorieId)
                            VALUES((SELECT TOP 1 id FROM Mailsysteem.Taak ORDER BY id DESC), @categorieId)";

            Start();

            foreach (string cat in cats)
            {
                var parameters = new
                {
                    @categorieId = GetCategorieId(cat)
                };

                _db.Connectie.Execute(sql, parameters);
            }

            _db.Close();
        }

        public static bool InsertVergadering(Vergadering vergadering)
        {
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

            Start();

            int affectedRows = _db.Connectie.Execute(sql, parameters);

            _db.Close();

            if (affectedRows == 0)
                return false;

            return true;
        }

        public static void InsertVergaderingGenodigde(List<string> emails)
        {
            string sql = $@"INSERT INTO Mailsysteem.VergaderingGenodigde(vergaderingId, gebruikerId)
                            VALUES((SELECT TOP 1 id FROM Mailsysteem.Vergadering ORDER BY id DESC), @gebruikerId)";

            Start();
            foreach (string email in emails)
            {
                var parameters = new
                {
                    @gebruikerId = GetGebruikerId(email)
                };

               int test = _db.Connectie.Execute(sql, parameters);
            }

            _db.Close();
        }

        public static bool UpdateTaak(Taak taak)
        {
            string sql;
            int affectedRows;

            Start();

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

                affectedRows = _db.Connectie.Execute(sql, parameters);
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

                affectedRows = _db.Connectie.Execute(sql, parameters);
            }

            _db.Close();

            if (affectedRows == 1)
                return true;

            return false;
        }

        //TODO isVerwijderd column invoegen bij bericht en berichtontvanger in database
        //public static bool UpdateBericht(Bericht bericht)

        public static bool DeleteTaak(Taak taak)
        {
            string sql = @"DELETE FROM Mailsysteem.Taak WHERE id = @id";

            var parameters = new
            {
                @id = taak.id
            };

            Start();

            int affectedRows = _db.Connectie.Execute(sql, parameters);

            _db.Close();

            if (affectedRows == 0)
                return false;

            return true;
        }

        public static bool DeleteMail(Bericht bericht)
        {
            string sql = $@"DELETE FROM Mailsysteem.Bericht WHERE id = @id";

            var parameters = new
            {
                @id = bericht.id
            };

            Start();

            int affectedRows = _db.Connectie.Execute(sql, parameters);

            _db.Close();

            if (affectedRows == 0)
                return false;

            return true;
        }


        public static int GetGebruikerId(string emailGebruiker)
        {
            Start();
            int result;

            try
            {
                result = _db.Connectie.QuerySingle<int>($"SELECT id FROM Mailsysteem.Gebruiker WHERE email = \'{emailGebruiker}\'");
            }
            catch (Exception)
            {

                return -1;
            }

            _db.Close();

            return result;
        }

        public static int GetCategorieId(string categorieNaam)
        {
            int result;
            Start();

            result = _db.Connectie.QuerySingle<int>($"SELECT id FROM Mailsysteem.Categorie WHERE naam = \'{categorieNaam}\'");

            _db.Close();

            return result;

        }
    }
}
