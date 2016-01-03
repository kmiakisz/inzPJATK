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
        public static void saveGlosujacy(String email,int idNar, int idWiek, int idPlec,int idAnkiety)
        {
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
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

            }
        }
        public static int saveCheckIP(String IP)
        {
            int status = 0;// 0 ok -1 blokuj
            
            return status;
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
    }
}