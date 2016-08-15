using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class MailController
    {
        static bool isSend = false;
        public static  List<String> getMailList()
        {
            List<String> mailList = new List<String>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                Sqlcon.Open();
                string query = "SELECT Email FROM Glosujący";
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
            return mailList;
        }

        public static int sendMail(String subject,String body, List<String> listaMaili)
        {
            int listaWyslanych = 0;
            
            MailMessage message = new MailMessage();
            //string userToken = "test";
            message.From = new MailAddress("ankietySNM@gmail.com");
            foreach(String mail in listaMaili)
            {
                listaWyslanych++;
                message.To.Add(new MailAddress(mail));
            }
            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new SmtpClient();
            client.Send(message);
            //client.SendAsync(message,userToken);
            return listaWyslanych;
            
        }
        
    }
}