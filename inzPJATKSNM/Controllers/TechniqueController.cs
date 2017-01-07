using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using inzPJATKSNM.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace inzPJATKSNM.Controllers
{
    public class TechniqueController
    {
       /* public static List<String> getTechnika()
        {
            List<String> listTechnic = new List<String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "select Technika from Technika";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader dr = new SqlDataReader())
                        {
                            while (dr.Read())
                            {
                                listTechnic.Add(dr.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listTechnic;
        }*/
        public static void insertTechnic(string name) 
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.CommandText = "INSERT_TECHNIQUE";

                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar);
                        cmd.Parameters["@NAME"].Value = name.ToString();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Podany element istnieje już bazie danych!");
            }
        }
    }
}