using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class MailController
    {
        static bool isSend = false;
        public static List<String> getMailList()
        {
            List<String> mailList = new List<String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(connStr))
                {
                    Sqlcon.Open();
                    string query = "SELECT Email FROM Glosujący where email is not null";
                    using (SqlCommand command = new SqlCommand(query, Sqlcon))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mailList.Add(reader.GetString(0));
                            }
                        }
                    }
                    Sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("getMailList", e.Message);
                throw new Exception("Błąd podczas pobierania maili");
            }

            return mailList;
        }
        public static int sendPublicMail(String subject, String body, List<String> listaMaili)
        {
            int liczbaWyslanych = 0;
            try
            {
                MailMessage message = new MailMessage();
                //string userToken = "test";
                message.From = new MailAddress("ankietySNM@gmail.com");
                foreach (String mail in listaMaili)
                {
                    liczbaWyslanych++;
                    message.To.Add(new MailAddress(mail));
                }
                message.Subject = subject;
                message.Body = body;

                SmtpClient client = new SmtpClient();
                client.Send(message);
                //client.SendAsync(message,userToken);
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("sendPublicMail", e.Message);
                throw new Exception("Błąd podczas wysyłania maili do publicznej ankiety");
            }

            return liczbaWyslanych;
        }
        public static int sendPrivateMail(String subject, String body, List<String> listaMaili, int ankietaId)
        {
            int liczbaWyslanych = 0;
            //string userToken = "test"; 
            foreach (String mail in listaMaili)
            {
                liczbaWyslanych++;
                sendMessage(subject, body, mail, ankietaId);
            }
            //client.SendAsync(message,userToken);
            return liczbaWyslanych;
        }
        public static void sendMessage(String subject, String body, String emailAddress, int ankietaId)
        {
            String token = generateToken();
            try
            {

                body += "&Token=" + token;
                MailMessage message = new MailMessage("ankietySNM@gmail.com", emailAddress, subject, body);
                SmtpClient client = new SmtpClient();
                client.Send(message);
            }
            catch (Exception e)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("sendMessage", e.Message);
                throw new Exception("Błąd podczas wysyłania wiadomości - prywatna ankieta");
            }
            try
            {
                inzPJATKSNM.Controllers.SurveyController.insertToken(token, ankietaId);
            }
            catch (Exception u)
            {
                inzPJATKSNM.Controllers.ErrorLogController.logToDb("insertToken- mail cotroller", u.Message);
                throw new Exception(u.Message);
            }
        }
        public static string generateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

    }
}