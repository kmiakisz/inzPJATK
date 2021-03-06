﻿using inzPJATKSNM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class SurveyController
    {
        public Glosujący glosujacy = new Glosujący();
        public static List<Dzieło> getDziela(int idAnkieta)
        {
            List<Dzieło> dziela = new List<Dzieło>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Id_dzieło,URL,Id_Tech,Id_Kat,Id_autora from Dzieło inner join skład on Id_dzieło = Id_zdjecia where id_ankiety like @ID;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int);
                        command.Parameters["@ID"].Value = idAnkieta;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dzieło dzielo = new Dzieło();
                                dzielo.Id_dzieło = reader.GetInt32(0);
                                dzielo.URL = reader.GetString(1);
                                dzielo.Id_Tech = reader.GetInt32(2);
                                dzielo.Id_Kat = reader.GetInt32(3);
                                dzielo.Id_Autora = reader.GetInt32(4);
                                dziela.Add(dzielo);
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getDziela", e.Message);
                throw new Exception("Błąd podczas pobierania dzieł!");
            }

            return dziela;
        }
        public static void updateMailSend(int surveyId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "isMailSendUpdate";

                        cmd.Parameters.Add("@surveyId", SqlDbType.Int);
                        cmd.Parameters["@surveyId"].Value = surveyId;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    inzPJATKSNM.Controllers.ErrorLogController.logToDb("updateMailSend", e.Message);
                    throw new Exception(e.Message);
                }
                finally
                {
                    Sqlcon.Close();
                }

            }
        }
        public static String getSurveyName(int idSurvey)
        {
            String nazwa = "";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select nazwa from Ankieta where Id_ankiety = @id;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = idSurvey;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nazwa = reader.GetString(0);
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getSurveyName", e.Message);
                throw new Exception("Błąd podczas pobierania nazwy ankiety");
            }

            return nazwa;
        }
        public static String getTechnika(int idTech)
        {
            String technika = "";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Technika from technika where Id_Tech = @id;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = idTech;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                technika = reader.GetString(0);
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getTechnika", e.Message);
                throw new Exception("Błąd poczas pobierania techniki");
            }

            return technika;
        }
        public static String getKategoria(int idKategoria)
        {
            String kategoria = "";
            try
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Kategoria from Kategoria where Id_Kat = @id;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = idKategoria;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                kategoria = reader.GetString(0);
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getKategoria", e.Message);
                throw new Exception("Błąd podczas pobierania kategorii dzieła");
            }

            return kategoria;
        }
        public static String getAutor(int idAutor)
        {
            String autor = "";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Nazwisko from Autor where Id_Autora = @id;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = idAutor;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                autor = reader.GetString(0);
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getAutor", e.Message);
                throw new Exception("Błąd podczas pobierania autora");
            }

            return autor;
        }
        public static int saveGlosujacy(String email, int idNar, int idWiek, int idPlec, int idAnkiety)
        {
            int idOsoba = 0;
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("glosowanie", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@idnarod", idNar);
                        cmd.Parameters.AddWithValue("@idwiek", idWiek);
                        cmd.Parameters.AddWithValue("@idplec", idPlec);
                        cmd.Parameters.AddWithValue("@idAnkiety", idAnkiety);
                        cmd.Parameters.AddWithValue("@tmpOsobaId", "@tmpOsobaId");
                        con.Open();
                        cmd.ExecuteNonQuery();
                        idOsoba = int.Parse((string)cmd.Parameters["@idOsoba"].Value);
                    }

                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("saveGlosujacy", e.Message);
                throw new Exception("Błąd podczas zapisu głosującego");
            }

            return idOsoba;
        }

        public static int saveGlosujacy(int idNar, int idWiek, int idPlec, int idAnkiety)
        {
            int idOsoba = 0;
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "insert_glosujacy";

                        cmd.Parameters.Add("@idNar", SqlDbType.Int);
                        cmd.Parameters["@idNar"].Value = idNar;

                        cmd.Parameters.Add("@idWiek", SqlDbType.Int);
                        cmd.Parameters["@idWiek"].Value = idWiek;

                        cmd.Parameters.Add("@idPlec", SqlDbType.Int);
                        cmd.Parameters["@idPlec"].Value = idPlec;

                        cmd.Parameters.Add("@idAnkiety", SqlDbType.Int);
                        cmd.Parameters["@idAnkiety"].Value = idAnkiety;

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();

                    }
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("saveGlosujacy", e.Message);
                throw new Exception("Błąd podczas zapisu głosującego");
            }

            return getLastGlosujacy();
        }
        public static int getLastGlosujacy()
        {
            int id = 0;
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select max(Id_Osoba) from Glosujący ";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                id = reader.GetInt32(0);


                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getLastGlosujacy", e.Message);
                throw new Exception("Błąd podczas zapisu głosującego");
            }


            return id;
        }

        public static List<String> getBlockedIPs(int surId)
        {
            List<String> blokowaneIP = new List<String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {

                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select IPAdress from Ip_table inner join BlokowaneAdresy on Ip_table.Id_ip = BlokowaneAdresy.Id_ip where Id_Ankiety = @id;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = surId;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                blokowaneIP.Add(reader.GetString(0));
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getBlockedIPs", e.Message);
                throw new Exception("Błąd podczas pobierania listy zablokowanych adresów IP");
            }

            return blokowaneIP;
        }

        public static void saveVotes(int idDzielo, float ocena, int idOsoba, int idAnkiety)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("ankieta_glosowanie", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ocena", ocena);
                        cmd.Parameters.AddWithValue("@idosoba", idOsoba);
                        cmd.Parameters.AddWithValue("@idAnkiety", idAnkiety);
                        cmd.Parameters.AddWithValue("@idzdjecia", idDzielo);
                        con.Open();
                        cmd.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("saveVotes", e.Message);
                throw new Exception("Błąd podczas zapisywania wyników głosowania");
            }

        }
        public static void saveAll(Dictionary<int, float> ocenyDziel, int idAnkiety, String email, int idNar, int idWiek, int idPlec)
        {
            int idOsoba = saveGlosujacy(idNar, idWiek, idPlec, idAnkiety);

            foreach (KeyValuePair<int, float> mapa in ocenyDziel)
            {
                try
                {
                    saveVotes(mapa.Key, mapa.Value, idOsoba, idAnkiety);
                }
                catch (Exception u)
                {
                    inzPJATKSNM.Controllers.ErrorLogController.logToDb("saveAll", u.Message);
                    throw new Exception(u.Message);
                }

                // do something with entry.Value or entry.Key
            }

        }
        public static void saveIPAddress(String ip, int ankietaId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "insert_ip";

                        cmd.Parameters.Add("@ipAdress", SqlDbType.VarChar);
                        cmd.Parameters["@ipAdress"].Value = ip;

                        cmd.Parameters.Add("@ankietaId", SqlDbType.Int);
                        cmd.Parameters["@ankietaId"].Value = ankietaId;

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();

                    }
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("saveIPAddress", e.Message);
                throw new Exception("Błąd podczas zapisywania adresu IP");
            }

        }
        public static string getSurveyType(int id)
        {
            string type = "";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Typ from Ankieta where Id_Ankiety = @id;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = id;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                type = reader.GetString(0);
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getSurveyType", e.Message);
                throw new Exception("Błąd podczas pobierania typu ankiety");
            }

            return type;
        }
        public static Dictionary<string, string> getSurveyTokens(int ankietaId)
        {
            Dictionary<string, string> tokens = new Dictionary<string, string>();

            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Token,isUsed from Tokens t inner join AnkietaTokens at on t.ID = at.Token_id where Ankieta_id = @id;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = ankietaId;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tokens.Add(reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getSurveyTokens", e.Message);
                throw new Exception("Błąd podczas pobierania tokenów ankiety");
            }

            return tokens;
        }
        public static Boolean checkToken(string token, int ankietaId)
        {
            Boolean isValid = false;
            try
            {
                if (getSurveyTokens(ankietaId).ContainsKey(token) && getSurveyTokens(ankietaId)[token].Equals("f"))
                {
                    isValid = true;
                }
            }
            catch (Exception u)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("checkToken", u.Message);
                throw new Exception(u.Message);
            }


            return isValid;
        }
        public static void insertToken(string token, int ankietaId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "insert_token";

                        cmd.Parameters.Add("@token", SqlDbType.VarChar);
                        cmd.Parameters["@token"].Value = token;

                        cmd.Parameters.Add("@ankietaId", SqlDbType.Int);
                        cmd.Parameters["@ankietaId"].Value = ankietaId;

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();

                    }
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("insertToken", e.Message);
                throw new Exception("Błąd podczas zapisu ankiety");
            }

        }
        public static void changeTokenState(string token, int ankietaId)
        {

            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "change_token_state";

                        cmd.Parameters.Add("@token", SqlDbType.VarChar);
                        cmd.Parameters["@token"].Value = token;

                        cmd.Parameters.Add("@ankietaId", SqlDbType.Int);
                        cmd.Parameters["@ankietaId"].Value = ankietaId;

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();

                    }
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("changeTokenState", e.Message);
                throw new Exception("Błąd podczas zapisu wyników głosowania");
            }

        }

        public static Dictionary<int, String> getNazwy()
        {
            Dictionary<int, String> nazwy = new Dictionary<int, string>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Id_ankiety,nazwa from ankieta where Typ = 'PUBLIC' and Active =1;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nazwy.Add(reader.GetInt32(0), reader.GetString(1));
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getNazwy", e.Message);
                throw new Exception("Błąd podczas pobierania ankiety");
            }

            return nazwy;
        }
        public static Dictionary<int, String> getOpis()
        {
            Dictionary<int, String> opisy = new Dictionary<int, string>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select Id_ankiety,Opis_ankiety from ankieta where Typ = 'PUBLIC' and Active =1;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                opisy.Add(reader.GetInt32(0), reader.GetString(1));
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getOpis", e.Message);
                throw new Exception("Błąd podczas pobierania ankiety");
            }
            return opisy;
        }
        public static Dictionary<int, String> getFirstURL()
        {
            Dictionary<int, String> urle = new Dictionary<int, string>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select ankieta.id_ankiety, dzielo.url from ankieta outer apply ( select top(1) url from dzieło d join skład s on s.id_zdjecia = d.id_dzieło where s.id_ankiety = ankieta.id_ankiety) as dzielo where ankieta.Typ = 'PUBLIC' and ankieta.Active = 1;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                urle.Add(reader.GetInt32(0), reader.GetString(1));
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getFirstURL", e.Message);
                throw new Exception("Błąd podczas pobierania ankiety");
            }
            return urle;
        }
        public static void saveSubscriptionEmail(String email, int survey_id)
        {
            if (!email.Equals(""))
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                try
                {
                    using (SqlConnection Sqlcon = new SqlConnection(connStr))
                    {

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            Sqlcon.Open();
                            cmd.Connection = Sqlcon;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Subscription_Update";

                            cmd.Parameters.Add("@email", SqlDbType.VarChar);
                            cmd.Parameters["@email"].Value = email;
                            cmd.Parameters["@email"].Size = 250;


                            cmd.Parameters.Add("@ankietaId", SqlDbType.Int);
                            cmd.Parameters["@ankietaId"].Value = survey_id;

                            cmd.ExecuteNonQuery();
                            Sqlcon.Close();

                        }
                    }
                }
                catch (Exception e)
                {
                    inzPJATKSNM.Controllers.ErrorLogController.logToDb("saveSubscriptionEmail", e.Message);
                    throw new Exception("Błąd podczas zapisu adresu email");
                }

            }
        }
        public static Boolean CheckIpAddress(String addressToCheck, List<String> adressCollection)
        {
            bool isContains = false;
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select IPAdress from ip_table;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                adressCollection.Add(reader.GetString(0));
                            }
                        }
                    }
                    Sqlcon.Close();

                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("CheckIpAddress", e.Message);
                throw new Exception(e.Message);
            }
            if (adressCollection.Contains(addressToCheck))
            {
                isContains = true;
            }
            else
            {
                isContains = false;
            }


            return isContains;
        }
        public static Glosujący getInfoByIp(String adress)
        {
            Glosujący g = new Glosujący();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "select g.id_narod, g.id_wiek, g.id_plec from glosujący g join bierze_udzial bu on g.id_osoba = bu.id_osoba join blokowaneAdresy ba on ba.id_ankiety = bu.id_ankiety join ip_table ip on ip.id_ip = ba.id_ip where ipadress=@ADDR;";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        command.Parameters.Add("@ADDR", SqlDbType.VarChar);
                        command.Parameters["@ADDR"].Value = adress;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                g.Id_Narod = reader.GetInt32(0);
                                g.Id_Wiek = reader.GetInt32(1);
                                g.Id_Plec = reader.GetInt32(2);
                            }

                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getInfoByIp", e.Message);
                throw new Exception(e.Message);
            }
            return g;
        }
    }
}