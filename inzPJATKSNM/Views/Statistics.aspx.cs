using inzPJATKSNM.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class Statistics : System.Web.UI.Page
    {
        public static Statistic s;
        public static int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(Request.QueryString["Id"] != null)
            {
                id =  Int32.Parse(Request.QueryString["Id"]);
                int par = Convert.ToInt32(Request.QueryString["Id"]);
                FillStatisticsPerSurvey(par);
            }
            else
            {
                FillStatistics();
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
        public static inzPJATKSNM.Controllers.Statistic FillChart()
        {
            s = inzPJATKSNM.Controllers.StatisticsController.DrawChart(id);
            return s;           
        }
      

        protected void StatButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaticticsPerSurveys.aspx");
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaticticsPerSurveys.aspx");
        }
    }
}