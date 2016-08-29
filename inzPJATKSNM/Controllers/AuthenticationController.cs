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
        public static void saveUser(User user){
            user.haslo = passGen();
             try{
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
         public static String encryptPass(String pass){
       
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
                 sendResetPassTokenEmail(user,token);
         }
         public static void sendResetPassTokenEmail(User user,String token)
         {
             String subject = "Reset hasła w systemie ankiet SNM";
             String body = "Otrzymaliśmy prośbę o reset hasła w systemie ankiet SNM.\n Jeśli to nie Ty " + user.imie + " ją wysłałeś prosimy o natychmiastowe skontakotowanie się z Administratorem lub o wiadomość na adres ankietysnm@gmail.com \n Twój token to: " + token + "\nProsimy o wpisanie go w odpowiednie pole w celu zresetowania hasła. \nPozdrawiamy, zespół systemu ankiet SNM";
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
        public static void resetPass(User user,String token){
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
            String body = "Otrzymaliśmy prośbę o reset hasła w systemie ankiet SNM.\n Jeśli to nie Ty "+ user.imie +" ją wysłałeś prosimy o natychmiastowe skontakotowanie się z Administratorem lub o wiadomość na adres ankietysnm@gmail.com \n Twoje nowe hasło to: "+user.haslo+"\nPozdrawiamy, zespół systemu ankiet SNM";
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
                        cmd.CommandText = "GET_USER";
                       
                            cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar);
                            cmd.Parameters["@LOGIN"].Value = user.login;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    user.userId = reader.GetInt32(0);
                                    user.login = reader.GetString(1);
                                    user.haslo = reader.GetString(2);
                                    user.imie = reader.GetString(3);
                                    user.nazwisko = reader.GetString(4);
                                    user.token = reader.GetString(5);
                                    user.rola.roleId = reader.GetInt32(6);
                                }
                            }
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
            if(administrator.rola.roleId==1){
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
                sendDeleteUserEmail(user,administrator);
            }
        }
        public static void sendDeleteUserEmail(User user,User administrator)
        {
            String subject = "Usunięcie użytkownika "+user.login;
            String body = "Użytkownik "+ user.login + " został usunięty. Od tego momentu nie może się logować do systemu.\n Pozdrawiamy, \n zespół systemu ankiet SNM";
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ankietySNM@gmail.com");
                message.To.Add(new MailAddress(user.login,administrator.login));
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
        public static string generateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
        public static Boolean checkUser(User user,String haslo)
        {
            Boolean isValid = false;
            if(encryptPass(getUser(user.login).haslo).Equals(encryptPass(haslo)){
                return true;
            }
            return isValid;

        }
    }
       

    
}