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
    public class CategoryController
    {
        public static List<String> getCategory()
        {
            List<String> listCategory = new List<String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "Select Kategoria from Kategoria";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                             while (dr.Read())
                             {
                                 listCategory.Add(dr.GetString(0));
                             }
                        }
                    }
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCategory;
        }
        public static void insertCategory(string name)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = "INSERT_CATEGORY";

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