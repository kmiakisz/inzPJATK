using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class ErrorLogController
    {
        public static void logToDb(String view_name,String err_log_body, Int32 user_id)
        {
            try
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try 
                        {
                            Sqlcon.Open();
                            cmd.Connection = Sqlcon;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "LOG_TO_DB";

                            cmd.Parameters.Add("@view_name", SqlDbType.VarChar);
                            cmd.Parameters["@view_name"].Value = view_name;

                            cmd.Parameters.Add("@err_body", SqlDbType.VarChar);
                            cmd.Parameters["@err_body"].Value = err_log_body;

                            cmd.Parameters.Add("@user_id", SqlDbType.Int);
                            cmd.Parameters["@user_id"].Value = user_id;

                            cmd.ExecuteNonQuery(); 
                        }
                        catch(SqlException sqlE)
                        {
                            throw new Exception(sqlE.Message);
                        }
                        finally
                        {
                            Sqlcon.Close();
                        }                  
                    }
                }

            }catch(Exception e)
            {
                throw new Exception("Błąd podczas zapisu logu do bazy! Skontantuj się z administratorem!");
            }
        }
    }
}