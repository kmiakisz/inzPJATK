using inzPJATKSNM.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using inzPJATKSNM.Models;
using inzPJATKSNM.Controllers;
using inzPJATKSNM.PrivilegeModels;

namespace inzPJATKSNM.Views
{
    public partial class Statistics : System.Web.UI.Page
    {
        public static Statistic s;
        public static List<Statistic> listaS;
        public static int id;
        int loggedId;
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
                if (!list.Contains(10))
                {
                    Response.Redirect("AdministratorPanel.aspx");
                }
                if (Request.QueryString["Id"] != null)
                {
                    id = Int32.Parse(Request.QueryString["Id"]);
                    int par = Convert.ToInt32(Request.QueryString["Id"]);
                    string surveyName = inzPJATKSNM.Controllers.StatisticsController.GetSurveyName(id);
                    HeaderLabel.Text = "Statystyki dla ankiety : " + surveyName;
                    FillStatisticsPerSurvey(par);
                }
                else
                {
                    HeaderLabel.Text = "Statystyki Ogólne.";
                    FillStatistics();
                }
            }
            else
            {
                Response.Redirect("LogInView.aspx");
            }
        }

        public void FillStatistics()
        {
            try
            {
                inzPJATKSNM.Controllers.Statistic s = new Controllers.Statistic();
                s = inzPJATKSNM.Controllers.StatisticsController.AvgImagesInSurveys();

                Lbl1.Text = "Średnia ilość dzieł w ankietach : " + Convert.ToString(s.avgImgInSurv);
                Lbl2.Text = "Średnia ilość głosów : " + Convert.ToString(s.avgVoteNum);
                Lbl3.Text = "Ogólna liczba głosów : " + Convert.ToString(s.voteNum);
                Lbl4.Text = "Ilość stworzonych ankiet : " + Convert.ToString(s.numOfCreatedSurv);
                Lbl5.Text = "Ilość odwiedzin : " + Convert.ToString(s.numOfVisitors);
                Lbl6.Text = "Ilość subskrybentów : " + Convert.ToString(s.numOfEmails);

                BackButton.Visible = false;
                Chart1.Visible = false;

            }
            catch (Exception e)
            {
                throw new Exception("Błąd podczas aktualizacji statystyk!");
            }
        }
        public static Statistic FillThumbnails()
        {
            inzPJATKSNM.Controllers.Statistic s = new Controllers.Statistic();
            return inzPJATKSNM.Controllers.StatisticsController.StatisticPerSurvey(id);
        }
        public void FillStatisticsPerSurvey(int surverId)
        {
            //try
            //{
            inzPJATKSNM.Controllers.Statistic s = new Controllers.Statistic();
            s = inzPJATKSNM.Controllers.StatisticsController.StatisticPerSurvey(surverId);

            Lbl1.Text = "Liczba głosujących : " + Convert.ToString(s.NumOfVotersOnSurvey);
            Lbl2.Text = "Liczba odwiedzających : " + Convert.ToString(s.NumOfVisitors);
            Lbl3.Text = "Liczba subskrybentów po ankiecie : " + Convert.ToString(s.NumOfSubs);
            Lbl4.Text = "Nazwa zdjęcia z największą sumą głosów : " + Convert.ToString(s.ImgMaxVoteNumName);
            Lbl5.Text = "Nazwa zdjęcia z najmniejszą sumą głosów : " + Convert.ToString(s.ImgMinVoteNumName);

            Lbl6.Visible = false;
            StatButton.Visible = false;

            //}
            //catch (Exception e)
            //{
            //  throw new Exception(e.Message);
            //}
        }
        public static List<inzPJATKSNM.Controllers.Statistic> FillChart()
        {
            listaS = inzPJATKSNM.Controllers.StatisticsController.DrawChart(id);
            return listaS;
        }


        protected void StatButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaticticsPerSurveys.aspx");
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaticticsPerSurveys.aspx");
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