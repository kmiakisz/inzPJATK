using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class NewSurvey : System.Web.UI.Page
    {
        private List<String> photoFromDB;
        private static List<String> photoToSurvey;
        protected void Page_Load(object sender, EventArgs e)
        {
            loadPhotosFromDB();
        }
        public void loadPhotosFromDB()
        {

            photoFromDB = inzPJATKSNM.Controllers.NewSurveyController.getPhotoList();
        }
        public static void addToPhotoToSurvey(string url)
        {
            photoToSurvey.Add(url);
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {

        }

    }
}