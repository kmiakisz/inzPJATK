using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using inzPJATKSNM.Models;
using inzPJATKSNM.Controllers;
using inzPJATKSNM.PrivilegeModels;

namespace inzPJATKSNM.Views
{
    public partial class SendMailForm : System.Web.UI.Page
    {
        public int id;
        int loggedId;
        public List<String> lstitems;
        public List<String> lstitems2;
        public List<String> lstitems3;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();

            if (Session["token"] != null)
            {
                string username = inzPJATKSNM.Controllers.AuthenticationController.getLogin((string)HttpContext.Current.Session["token"]);
                inzPJATKSNM.AuthModels.User user = inzPJATKSNM.Controllers.AuthenticationController.getUser(username);
                loggedId = user.userId;
                List<Int32> list = UserController.GetUserPrivilegeListIdPerUserId(loggedId);
                if (!list.Contains(8))
                {
                    Response.Redirect("AdministratorPanel.aspx");
                }
                if (Request.QueryString["Id"] != null)
                {
                    id = int.Parse(Request.QueryString["Id"]);
                    if (!IsPostBack)
                    {
                        body.Value += " Ankieta pod adresem: http://localhost:11222/Views/StartPage.aspx?Id=" + id;
                    }
                    if (inzPJATKSNM.Controllers.SurveyController.getSurveyType(id).Equals("PUBLIC"))
                    {
                        CustomMail.Visible = false;
                        CustomLbl.Visible = false;
                        CustomMailTxt.Visible = false;
                    }
                }
                else
                {
                    //modal o pustym id
                }
            }
            else
            {
                Response.Redirect("LogInView.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        public List<String> getMail()
        {
            List<String> userMailList = new List<string>();
            try
            {
                userMailList = inzPJATKSNM.Controllers.MailController.getMailList();
            }
            catch (Exception ex)
            {
                Response.Redirect("/Views/ShowSurveys.aspx?err=" + ex.Message);
            }

            return userMailList;

        }
        protected void Accept_Click(object sender, EventArgs e)
        {
            int il = 0;
            try
            {
                if (inzPJATKSNM.Controllers.SurveyController.getSurveyType(id).Equals("PUBLIC"))
                {
                    il = inzPJATKSNM.Controllers.MailController.sendPublicMail(subject.Value, body.Value, getMail());
                }
                else
                {
                    il = inzPJATKSNM.Controllers.MailController.sendPrivateMail(subject.Value, body.Value, getMail(), id);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("/Views/ShowSurveys.aspx?err=" + ex.Message);
            }


            Response.Redirect("/Views/ShowSurveys.aspx?val=" + il);
        }

        protected void AllMailListLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstitems = new List<String>();
            for (int i = 0; i < AllMailListLst.Items.Count; i++)
            {
                if (AllMailListLst.Items[i].Selected)
                    lstitems.Add(AllMailListLst.Items[i].Value);
            }
        }

        protected void AllMailListLst_DataBound(object sender, EventArgs e)
        {

        }

        protected void SendMailButton_Click(object sender, EventArgs e)
        {
            int il = 0;
            if (CustomMail.Checked)
            {
                try
                {
                    if (inzPJATKSNM.Controllers.SurveyController.getSurveyType(id).Equals("PUBLIC"))
                    {
                        il = inzPJATKSNM.Controllers.MailController.sendPublicMail(subject.Value, body.Value, lstitems3);
                    }
                    else
                    {
                        il = inzPJATKSNM.Controllers.MailController.sendPrivateMail(subject.Value, body.Value, lstitems3, id);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Views/ShowSurveys.aspx?err=" + ex.Message);
                }
            }
            if (SubsMailList.Checked)
            {
                try
                {
                    if (inzPJATKSNM.Controllers.SurveyController.getSurveyType(id).Equals("PUBLIC"))
                    {
                        il = inzPJATKSNM.Controllers.MailController.sendPublicMail(subject.Value, body.Value, lstitems2);
                    }
                    else
                    {
                        il = inzPJATKSNM.Controllers.MailController.sendPrivateMail(subject.Value, body.Value, lstitems2, id);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Views/ShowSurveys.aspx?err=" + ex.Message);
                }
            }
            if (AllMailList.Checked)
            {
                try
                {
                    if (inzPJATKSNM.Controllers.SurveyController.getSurveyType(id).Equals("PUBLIC"))
                    {
                        il = inzPJATKSNM.Controllers.MailController.sendPublicMail(subject.Value, body.Value, lstitems);
                    }
                    else
                    {
                        il = inzPJATKSNM.Controllers.MailController.sendPrivateMail(subject.Value, body.Value, lstitems, id);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Views/ShowSurveys.aspx?err=" + ex.Message);
                }
            }
            if (!CustomMail.Checked && !SubsMailList.Checked && !AllMailList.Checked)
            {
                //blad ze nie zaznaczono checkboxa
            }
            Response.Redirect("/Views/ShowSurveys.aspx?val=" + il);
        }

        protected void SubsMailListLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstitems2 = new List<String>();
            for (int i = 0; i < SubsMailListLst.Items.Count; i++)
            {
                if (SubsMailListLst.Items[i].Selected)
                    lstitems2.Add(SubsMailListLst.Items[i].Value);
            }

        }

        protected void AllMailList_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void CustomMailTxt_TextChanged(object sender, EventArgs e)
        {
            lstitems3 = new List<String>();
            string str = CustomMailTxt.Text;
            String[] splitted = str.Split(';');

            foreach (String s in splitted)
            {
                lstitems3.Add(s);
            }
        }
        protected override void InitializeCulture()
        {
            HttpCookie cookie = Request.Cookies["CultureInfo"];

            if (cookie != null && cookie.Value != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value); ;
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pl-PL");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            }

            base.InitializeCulture();
        } 
    }
}