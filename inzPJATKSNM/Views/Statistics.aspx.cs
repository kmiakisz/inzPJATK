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
        protected void Page_Load(object sender, EventArgs e)
        {
            FillStatistics();
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
            }
            catch (Exception e)
            {
                throw new Exception("Błąd podczas aktualizacji statystyk!");
            }
        }
    }
}