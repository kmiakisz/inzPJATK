using inzPJATKSNM.AuthModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace inzPJATKSNM.Controllers
{

    public class AuthenticationController
    {
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        public static String passGen()
        {
            String password = "";
            password = System.Web.Security.Membership.GeneratePassword(8, 3);
            return password;
        }
        public static void saveUser(User user)
        {
            user.haslo = passGen();
            try
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "INSERT_USER";

                        cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar);
                        cmd.Parameters["@LOGIN"].Value = user.login;

                        cmd.Parameters.Add("@PWD", SqlDbType.VarChar);
                        cmd.Parameters["@PWD"].Value = encryptPass(user.haslo);

                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar);
                        cmd.Parameters["@NAME"].Value = user.imie;

                        cmd.Parameters.Add("@SURNAME", SqlDbType.VarChar);
                        cmd.Parameters["@SURNAME"].Value = user.nazwisko;

                        cmd.Parameters.Add("@ID_ROLE", SqlDbType.Int);
                        cmd.Parameters["@ID_ROLE"].Value = user.rola.roleId;

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Dodawanie użytkowmnika się nie powiodło!");
            }
            sendRegisterEmail(user);
        }
        public static void sendRegisterEmail(User user)
        {
            String subject = "Witamy w platformie ankiet wydziału Sztuki Nowych Mediów PJATK!";
            String body = "Witamy " + user.imie + "na platformie ankiet, \n Właśnie Twoje konto zostało utworzone w systemie! \n Od dzisiaj możesz logować się do systemu. \n Twój login: " + user.login + " \n Twoje hasło: " + user.haslo + "\nPo zalogowaniu się do aplikacji prosimy o zmianę hasła w zakładce zmień dane. \n Pozdrawiamy, \n zespół systemu ankiet SNM";
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ankietySNM@gmail.com");
                message.To.Add(new MailAddress(user.login));
                message.Subject = subject;
                message.Body = body;

                SmtpClient client = new SmtpClient();
                client.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception("Błąd podczas wysyłania maila rejestracyjnego");
            }
        }
        public static String encryptPass(String pass)
        {

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(pass);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);

        }
        public static void resetPassToken(User user)
        {
            String token = generateToken();
            try
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "UPDATE_TOKEN";

                        cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar);
                        cmd.Parameters["@LOGIN"].Value = user.login;

                        cmd.Parameters.Add("@TOKEN", SqlDbType.VarChar);
                        cmd.Parameters["@TOKEN"].Value = token;

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Reset hasła się nie powiódł");
            }
            sendResetPassTokenEmail(user, token);
        }
        public static void sendResetPassTokenEmail(User user, String token)
        {
            String subject = "Reset hasła w systemie ankiet SNM";
            String body = "Otrzymaliśmy prośbę o reset hasła w systemie ankiet SNM.\nJeśli to nie Ty " + user.imie + " ją wysłałeś prosimy o natychmiastowe skontakotowanie się z Administratorem lub o wiadomość na adres ankietysnm@gmail.com \n Twój token to: " + token + "\nProsimy o wpisanie go w odpowiednie pole w celu zresetowania hasła. \nPozdrawiamy, zespół systemu ankiet SNM";
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ankietySNM@gmail.com");
                message.To.Add(new MailAddress(user.login));
                message.Subject = subject;
                message.Body = body;

                SmtpClient client = new SmtpClient();
                client.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception("Błąd podczas wysyłania maila resetującego hasło");
            }
        }
        public static void sendNewPwd(User user, String pwd)
        {
            String subject = "Zmiana hasła w systemie ankiet SNM";
            String body = "Użytkowniku :" + user.imie + " " + user.nazwisko + "! \nOdnotowaliśmy zmianę hasła w systemie ankiet SNM. Twoje nowe hasło to : " + pwd + ". \nJeśli to nie Ty dokonałeś zmiany hasła, prosimy o natyczmiastowy kontakt z Administratorem lub o wiadomość na adres ankietysnm@gmail.com.\n\nPozdrawiamy,\n->Zespół systemu ankiet SNM.";
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ankietySNM@gmail.com");
                message.To.Add(new MailAddress(user.login));
                message.Subject = subject;
                message.Body = body;

                SmtpClient client = new SmtpClient();
                client.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception("Błąd podczas wysyłania maila z nowym hasłem");
            }

        }
        public static void resetPass(User user, String token)
        {
            if (token.Equals(user.token))
            {
                user.haslo = passGen();
                try
                {
                    String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                    using (SqlConnection Sqlcon = new SqlConnection(connStr))
                    {

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            Sqlcon.Open();
                            cmd.Connection = Sqlcon;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RESET_PASS";

                            cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar);
                            cmd.Parameters["@LOGIN"].Value = user.login;

                            cmd.Parameters.Add("@PWD", SqlDbType.VarChar);
                            cmd.Parameters["@PWD"].Value = encryptPass(user.haslo);

                            cmd.ExecuteNonQuery();
                            Sqlcon.Close();

                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Reset hasła się nie powiódł");
                }
                sendResetPassEmail(user);
            }
            else
            {
                throw new Exception("Nieprawidłowa token! Hasło nie zostało zmienione");
            }

        }
        public static void sendResetPassEmail(User user)
        {
            String subject = "Reset hasła w systemie ankiet SNM";
            String body = "Otrzymaliśmy prośbę o reset hasła w systemie ankiet SNM.\n Jeśli to nie Ty " + user.imie + " ją wysłałeś prosimy o natychmiastowe skontakotowanie się z Administratorem lub o wiadomość na adres ankietysnm@gmail.com \n Twoje nowe hasło to: " + user.haslo + "\nPozdrawiamy, zespół systemu ankiet SNM";
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ankietySNM@gmail.com");
                message.To.Add(new MailAddress(user.login));
                message.Subject = subject;
                message.Body = body;

                SmtpClient client = new SmtpClient();
                client.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception("Błąd podczas wysyłania maila resetującego hasło");
            }
        }
        public static User getUser(String login)
        {
            User user = new User();
            Rola role = new Rola();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
              try
              {
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("GET_USER", Sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter outPutParameter = new SqlParameter();

                    cmd.Parameters.Add("@ID_USER", SqlDbType.Int);
                    cmd.Parameters["@ID_USER"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar);
                    cmd.Parameters["@LOGIN"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@LOGIN"].Size = 250;
                    cmd.Parameters.Add("@PWD", SqlDbType.VarChar);
                    cmd.Parameters["@PWD"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@PWD"].Size = 250;
                    cmd.Parameters.Add("@NAME", SqlDbType.VarChar);
                    cmd.Parameters["@NAME"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@NAME"].Size = 250;
                    cmd.Parameters.Add("@SURNAME", SqlDbType.VarChar);
                    cmd.Parameters["@SURNAME"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@SURNAME"].Size = 250;
                    cmd.Parameters.Add("@TOKEN", SqlDbType.VarChar);
                    cmd.Parameters["@TOKEN"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@TOKEN"].Size = 250;
                    cmd.Parameters.Add("@ID_ROLE", SqlDbType.Int);
                    cmd.Parameters["@ID_ROLE"].Direction = ParameterDirection.Output;
                    // try
                    // {
                    Sqlcon.Open();
                    cmd.Parameters.Add("@LOGIN2", SqlDbType.VarChar);
                    cmd.Parameters["@LOGIN2"].Value = login;
                    cmd.Parameters["@LOGIN2"].Size = 100;

                    cmd.ExecuteNonQuery();
                    user.userId = Convert.ToInt32(cmd.Parameters["@ID_USER"].Value);
                    user.login = cmd.Parameters["@LOGIN"].Value.ToString();
                    user.haslo = cmd.Parameters["@PWD"].Value.ToString();
                    user.imie = cmd.Parameters["@NAME"].Value.ToString();
                    user.nazwisko = cmd.Parameters["@SURNAME"].Value.ToString();
                    user.token = cmd.Parameters["@TOKEN"].Value.ToString();
                    role.roleId = Convert.ToInt32(cmd.Parameters["@ID_ROLE"].Value);
                    user.rola = role;
                    user.rola.roleId = role.roleId;

                }
                Sqlcon.Close();
            }
              }
               catch (Exception e)
             {
               throw new Exception("Autor o podanym loginie nie istnieje w systemie!");
            }
            return user;
        }
        public static void deleteUser(User user, User administrator)
        {
            if (administrator.rola.roleId == 1)
            {
                try
                {
                    String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                    using (SqlConnection Sqlcon = new SqlConnection(connStr))
                    {

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            Sqlcon.Open();
                            cmd.Connection = Sqlcon;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "DELETE_USER";

                            cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar);
                            cmd.Parameters["@LOGIN"].Value = user.login;

                            cmd.ExecuteNonQuery();
                            Sqlcon.Close();

                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Usunięcie użytkowmnika się nie powiodło!");
                }
                sendDeleteUserEmail(user, administrator);
            }
        }
        public static void sendDeleteUserEmail(User user, User administrator)
        {
            String subject = "Usunięcie użytkownika " + user.login;
            String body = "Użytkownik " + user.login + " został usunięty. Od tego momentu nie może się logować do systemu.\n Pozdrawiamy, \n zespół systemu ankiet SNM";
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ankietySNM@gmail.com");
                message.To.Add(new MailAddress(user.login, administrator.login));
                message.Subject = subject;
                message.Body = body;

                SmtpClient client = new SmtpClient();
                client.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception("Błąd podczas wysyłania maila o usunięciu użytkownika hasło");
            }
        }
        public static void editUser(User user)
        {
            try
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "UPDATE_USER";

                        cmd.Parameters.Add("@PWD", SqlDbType.VarChar);
                        cmd.Parameters["@PWD"].Value = encryptPass(user.haslo);

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Zmiana danych użytkownika się nie powiodła!");
            }
        }
        public static void changePassword(User user,String pwd)
        {
            try
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "UPDATE_USER";

                        cmd.Parameters.Add("@ID", SqlDbType.VarChar);
                        cmd.Parameters["@ID"].Value = user.userId;

                        cmd.Parameters.Add("@PWD", SqlDbType.VarChar);
                        cmd.Parameters["@PWD"].Value = encryptPass(pwd);

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static string generateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
        public static Boolean checkUser(User user, String haslo)
        {
            Boolean isValid = false;
            String encryptedPass = getUser(user.login).haslo;
            String passPassed = encryptPass(haslo);
            if (encryptedPass.Equals(passPassed))
            {
                return true;
            }

            return isValid;

        }
        public static void changePriviledges(User user)
        {
            try
            {
                deletePrivliges(user);
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                foreach (Uprawnienia uprawnienie in user.uprawnieniaUsera)
                {
                    using (SqlConnection Sqlcon = new SqlConnection(connStr))
                    {

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            Sqlcon.Open();
                            cmd.Connection = Sqlcon;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "CHANGE_PRIVILEDGE";

                            cmd.Parameters.Add("@ID_USER", SqlDbType.Int);
                            cmd.Parameters["@ID_USER"].Value = user.userId;

                            cmd.Parameters.Add("@ID_PRIV", SqlDbType.Int);
                            cmd.Parameters["@ID_PRIV"].Value = uprawnienie.uprawnienieId;

                            cmd.ExecuteNonQuery();
                            Sqlcon.Close();

                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("Zmiana uprawnień się nie powiodła!");
            }
        }
        public static void changeRole(User user)
        {
            try
            {
                deletePrivliges(user);
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;

                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "CHANGE_ROLE";

                        cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar);
                        cmd.Parameters["@LOGIN"].Value = user.login;

                        cmd.Parameters.Add("@ID_ROLE", SqlDbType.Int);
                        cmd.Parameters["@ID_ROLE"].Value = user.rola.roleId;

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();

                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("Zmiana uprawnień się nie powiodła!");
            }
        }
        public static String getLogin(string s)
        {
            int idx = s.IndexOf(' ') + 1;
            int len = s.Length;
            string subs = s.Substring(idx, len - idx);

            return subs; 
        }
        public static void deletePrivliges(User user)
        {
            try
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                foreach (Uprawnienia uprawnienie in user.uprawnieniaUsera)
                {
                    using (SqlConnection Sqlcon = new SqlConnection(connStr))
                    {

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            Sqlcon.Open();
                            cmd.Connection = Sqlcon;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "DELETE_PRIVILEDGE";

                            cmd.Parameters.Add("@ID_USER", SqlDbType.Int);
                            cmd.Parameters["@ID_USER"].Value = user.userId;

                            cmd.ExecuteNonQuery();
                            Sqlcon.Close();

                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("Zmiana uprawnień się nie powiodła!");
            }
        }
        public static void insertTokenToUser(User user, String token)
        {
            try
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "INSERT_TOKEN_USER";

                        cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
                        cmd.Parameters["@USER_ID"].Value = user.userId;
                        cmd.Parameters.Add("@TOKEN", SqlDbType.VarChar);
                        cmd.Parameters["@TOKEN"].Value = generateToken();

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();
                    }
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void checkToken(User user)
        {
            try
            {
                String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Sqlcon.Open();
                        cmd.Connection = Sqlcon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "CHECK_TOKEN";

                        cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
                        cmd.Parameters["@USER_ID"].Value = user.userId;

                        cmd.ExecuteNonQuery();
                        Sqlcon.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }



}