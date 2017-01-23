using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class CommonController
    {
        public static string GetVisitorIPAddress(bool GetLan = false)
        {
            string visitorIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (visitorIPAddress.Contains(":"))
            {
               int addrLen = visitorIPAddress.Length;
               int idxOf = visitorIPAddress.IndexOf(":");
               visitorIPAddress = visitorIPAddress.Substring(0, idxOf);
            }
            if (String.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
            {
                GetLan = true;
                visitorIPAddress = string.Empty;
            }

            if (GetLan && string.IsNullOrEmpty(visitorIPAddress))
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = Dns.GetHostName();
                //Get Ip Host Entry
                IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                    if (visitorIPAddress.Contains(":"))
                    {
                        int addrLen = visitorIPAddress.Length;
                        int idxOf = visitorIPAddress.IndexOf(":");
                        visitorIPAddress = visitorIPAddress.Substring(0, idxOf);
                    }
                }
                catch
                {
                    try
                    {
                        visitorIPAddress = arrIpAddress[0].ToString();
                        if(visitorIPAddress.Contains(":"))
                        {
                            int addrLen = visitorIPAddress.Length;
                            int idxOf = visitorIPAddress.IndexOf(":");
                            visitorIPAddress = visitorIPAddress.Substring(0, idxOf);
                        }
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = Dns.GetHostAddresses(stringHostName);
                            visitorIPAddress = arrIpAddress[0].ToString();
                            if (visitorIPAddress.Contains(":"))
                            {
                                int addrLen = visitorIPAddress.Length;
                                int idxOf = visitorIPAddress.IndexOf(":");
                                visitorIPAddress = visitorIPAddress.Substring(0, idxOf);
                            }
                        }
                        catch
                        {
                            visitorIPAddress = "127.0.0.1";
                        }
                    }
                }

            }


            return visitorIPAddress;
        }  
         /*public static Boolean checkSemicolon(String chuj){
             char[] dupa = chuj.ToCharArray();
             foreach(Char c in dupa){
                 if(c.Equals(':'))
                     return true;
             }
             return false;
        }*/
    }
   
  
}