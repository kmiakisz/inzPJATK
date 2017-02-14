using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class AutorController
    {
        public static void addNewAuthor(string imie , string nazwisko , int id_narodowosc , int id_plec , int id_epoka)
        {
            try{
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "insert_autor";

                        cmd.Parameters.Add("@imie", SqlDbType.VarChar);
                        cmd.Parameters["@imie"].Value = imie;

                        cmd.Parameters.Add("@nazwisko", SqlDbType.VarChar);
                        cmd.Parameters["@nazwisko"].Value = nazwisko;

                        cmd.Parameters.Add("@narodowosc", SqlDbType.Int);
                        cmd.Parameters["@narodowosc"].Value = id_narodowosc;

                        cmd.Parameters.Add("@plec", SqlDbType.Int);
                        cmd.Parameters["@plec"].Value = id_plec;

                        cmd.Parameters.Add("@epoka", SqlDbType.Int);
                        cmd.Parameters["@epoka"].Value = id_epoka;

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();

                    }
                
            }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("addNewAuthor", e.Message);
                throw new Exception("Dodawanie autora się nie powiodło!");
            }
         
        }
    }
}