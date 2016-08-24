using inzPJATKSNM.Models;
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
            return dziela;
        }
        public static String getSurveyName(int idSurvey)
        {
            String nazwa = "";


            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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
            return nazwa;
        }
        public static String getTechnika(int idTech)
        {
            String technika = "";


            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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
            return technika;
        }
        public static String getKategoria(int idKategoria)
        {
            String kategoria = "";


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
            return kategoria;
        }
        public static String getAutor(int idAutor)
        {
            String autor = "";


            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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
            return autor;
        }
        public static int saveGlosujacy(String email,int idNar, int idWiek, int idPlec,int idAnkiety)
        {
            int idOsoba = 0;
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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
            return idOsoba;
        }

        public static int saveGlosujacy(int idNar, int idWiek, int idPlec, int idAnkiety)
        {
            int idOsoba = 0;
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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
            return getLastGlosujacy();
        }
        public static int getLastGlosujacy()
        {
            int id = 0;
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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
            return id;
        }

        public static List<String> getBlockedIPs(int surId)
        {
            List<String> blokowaneIP = new List<String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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
            return blokowaneIP;
        }

        public static void saveVotes(int idDzielo, int ocena, int idOsoba, int idAnkiety)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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
        public static void saveAll(Dictionary<int,int> ocenyDziel,int idAnkiety,String email,int idNar, int idWiek, int idPlec)
        {
            int idOsoba = saveGlosujacy(idNar,idWiek,idPlec,idAnkiety);

            foreach (KeyValuePair<int, int> mapa in ocenyDziel)
            {
                saveVotes(mapa.Key, mapa.Value, idOsoba, idAnkiety);
                // do something with entry.Value or entry.Key
            }

        }
        public static void saveIPAddress(String ip,int ankietaId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    Sqlcon.Open();
                    cmd.Connection = Sqlcon;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert_ip";

                    cmd.Parameters.Add("@@ipAdress", SqlDbType.VarChar);
                    cmd.Parameters["@@ipAdress"].Value = ip;

                    cmd.Parameters.Add("@ankietaId", SqlDbType.Int);
                    cmd.Parameters["@ankietaId"].Value = ankietaId;

                    cmd.ExecuteNonQuery();
                    Sqlcon.Close();

                }
            }
        }
        public static string getSurveyType(int id)
        {
            string type ="";
  
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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
            return type;
        }
        public static Dictionary<string,string> getSurveyTokens(int ankietaId)
        {
            Dictionary<string,string> tokens = new Dictionary<string,string>();

            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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

                            tokens.Add(reader.GetString(0),reader.GetString(1));


                        }
                    }
                }
                Sqlcon.Close();
            }
            return tokens;
        }
        public static Boolean checkToken(string token,int ankietaId)
        {
            Boolean isValid = false;
                if(getSurveyTokens(ankietaId).ContainsKey(token) && getSurveyTokens(ankietaId)[token].Equals("f")){
                    isValid = true;
                }
            
            return isValid;
        }
        public static void insertToken(string token, int ankietaId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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
        public static void changeTokenState(string token, int ankietaId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
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
    }
}