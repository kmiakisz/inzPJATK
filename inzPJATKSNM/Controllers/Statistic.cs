using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class Statistic
    {
        //OverallStatistics
        public int avgImgInSurv { get; set; }
        public int avgVoteNum { get; set; }
        public int voteNum { get; set; }
        public int numOfCreatedSurv { get; set; }
        public int numOfVisitors { get; set; }
        public int numOfEmails { get; set; }
        //StatisticsPerSurvey
        public int NumOfVotersOnSurvey { get; set; }
        public int NumOfVisitors { get; set; }
        public int NumOfSubs { get; set; }
        public string ImgMaxVoteNumName { get; set; }
        public string ImgMinVoteNumName { get; set; }
        //draw chart
        public int mark { get; set; }
        public int photoId { get; set; }
        public String photoName { get; set; }
        public String ImgMaxVoteNumUrl { get; set; }
        public String ImgMinVoteNumUrl { get; set; }
    }
}