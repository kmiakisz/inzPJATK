using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using inzPJATKSNM.PrivilegeModels;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using inzPJATKSNM.AuthModels;

namespace inzPJATKSNM.Controllers
{
    public class UserController
    {
        public static UserPrivilege InsertPrivilege(int userId, int privilegeId)
        {
            UserPrivilege up = new UserPrivilege();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            string insertCmd = "INSERT INTO USER_PRIVILEDGES (ID_USER,ID_PRIVILEDGE) VALUES (@ID_USER,@ID_PRIV);";
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(insertCmd, Sqlcon);
                command.Parameters.Add("@ID_USER", SqlDbType.Int);
                command.Parameters["@ID_USER"].Value = userId;
                command.Parameters.Add("@ID_PRIV", SqlDbType.Int);
                command.Parameters["@ID_PRIV"].Value = privilegeId;
                try
                {
                    Sqlcon.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    inzPJATKSNM.Controllers.ErrorLogController.logToDb("InsertPrivilege", e.Message);
                    throw new Exception("Błąd podczas dodawaniu uprawnienia.");
                }
                finally
                {
                    Sqlcon.Close();
                }
            }
            return up;
        }
        public static void DeletePriv(int id_user_priviledges)
        {
            UserPrivilege up = new UserPrivilege();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            string deleteCommand = "DELETE UP FROM USER_PRIVILEDGES UP JOIN SNM_USER U ON U.ID_USER = UP.ID_USER JOIN SNM_PRIVILEDGES SP ON SP.ID_PRIVILEDGE = UP.ID_PRIVILEDGE WHERE UP.ID_USER_PRIVILEDGES = @ID;";
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    SqlCommand command = new SqlCommand(deleteCommand, Sqlcon);
                    SqlParameter param = new SqlParameter("@ID", id_user_priviledges);
                    command.Parameters.Add(param);
                    Sqlcon.Open();
                    command.ExecuteNonQuery();
                    Sqlcon.Close();
                }
            }
            catch(Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("DeletePriv", e.Message);
                throw new Exception(e.Message);
            }
        }
        public static User GetUser(int id)
        {
            User user = new User();
            string query = "SELECT U.EMAIL_LOGIN, U.NAME, U.SURNAME, R.ROLE_NAME FROM SNM_USER U JOIN SNM_ROLE R ON R.ID_ROLE = U.ID_ROLE WHERE U.ID_USER = " + id + ";";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(query, Sqlcon);
                    Sqlcon.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.login = reader.GetString(0);
                        user.imie = reader.GetString(1);
                        user.nazwisko = reader.GetString(2);
                        user.nazwaRoli = reader.GetString(3);
                    }
                    Sqlcon.Close();
                }
            }
            catch(Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("GetUser", e.Message);
                throw new Exception(e.Message);
            }

            return user;
        }
        public static List<UserPrivilege> GetUserPrivilegePerId(int userId)
        {
            List<UserPrivilege> listUserPrivilege = new List<UserPrivilege>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            string query = "SELECT UP.ID_USER_PRIVILEDGES, U.ID_USER, U.NAME, U.SURNAME, SP.ID_PRIVILEDGE, SP.PRIVILEDGE_NAME FROM SNM_USER U JOIN USER_PRIVILEDGES UP ON U.ID_USER = UP.ID_USER JOIN SNM_PRIVILEDGES SP ON SP.ID_PRIVILEDGE = UP.ID_PRIVILEDGE WHERE U.ID_USER =" + userId;
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserPrivilege up = new UserPrivilege();
                        up.id_user_priviledges = reader.GetInt32(0);
                        up.usedId = reader.GetInt32(1);
                        up.userName = reader.GetString(2);
                        up.userSurname = reader.GetString(3);
                        up.privilegeId = reader.GetInt32(4);
                        up.privilegeName = reader.GetString(5);
                        listUserPrivilege.Add(up);
                    }
                    con.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("GetUserPrivilegePerId", e.Message);
                throw new Exception(e.Message);
            }

            return listUserPrivilege;
        }
        public static List<Int32> GetUserPrivilegeListIdPerUserId(int userId)
        {
            List<Int32> listUserPrivilegeIds = new List<Int32>();
            int privilegeId;
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            string query = "SELECT SP.ID_PRIVILEDGE FROM SNM_USER U JOIN USER_PRIVILEDGES UP ON U.ID_USER = UP.ID_USER JOIN SNM_PRIVILEDGES SP ON SP.ID_PRIVILEDGE = UP.ID_PRIVILEDGE WHERE U.ID_USER=" + userId;
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //UserPrivilege up = new UserPrivilege();
                        privilegeId = reader.GetInt32(0);
                        listUserPrivilegeIds.Add(privilegeId);
                    }
                    con.Close();
                }
            }
            catch(Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("GetUserPrivilegeListIdPerUserId", e.Message);
                throw new Exception(e.Message);
            }

            return listUserPrivilegeIds;
        }
        public static List<Uprawnienia> GetAvailablePrivById(int userId)
        {
            List<Uprawnienia> listAvailablePriv = new List<Uprawnienia>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            string query = "SELECT id_priviledge, priviledge_name FROM snm_priviledges WHERE id_priviledge NOT IN(SELECT id_priviledge FROM user_priviledges WHERE id_user =" + userId + ");";
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Uprawnienia u = new Uprawnienia();
                            u.uprawnienieId = reader.GetInt32(0);
                            u.nazwa = reader.GetString(1);
                            listAvailablePriv.Add(u);
                        }
                        con.Close();
                    }
                }
            }
            catch(Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("GetAvailablePrivById", e.Message);
                throw new Exception(e.Message);
            }

            return listAvailablePriv;
        }
        public static List<Int32> GetUserIdList()
        {
            List<Int32> listUsersIds = new List<Int32>();
            int userId;
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            string query = "SELECT ID_USER FROM SNM_USER;";
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userId = reader.GetInt32(0);
                        listUsersIds.Add(userId);
                    }
                    con.Close();
                }
            }
            catch(Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("GetUserIdList", e.Message);
                throw new Exception(e.Message);
            }
            return listUsersIds;
        }
        public static List<String> getMailList()
        {
            List<String> listaMaili = new List<String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            string query = "select email_login from snm_user;";
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while(reader.Read())
                        {
                            listaMaili.Add(reader.GetString(0));
                        }
                        con.Close();
                    }
                }
            }
            catch(Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getUser", e.Message);
                throw new Exception(e.Message);
            }
            return listaMaili;
        }
    }
}