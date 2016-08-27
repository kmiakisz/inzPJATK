using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    public class StatisticsController
    {
        public static Statistic AvgImagesInSurveys()
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            Statistic s = new Statistic();
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("OverallStatistics", Sqlcon)) //statystyki ogólne
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter outPutParameter = new SqlParameter();

                    outPutParameter.ParameterName = "@avgImgInSurv";
                    outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
                    outPutParameter.Direction = System.Data.ParameterDirection.Output;
                    s.avgImgInSurv = Convert.ToInt32(cmd.Parameters.Add(outPutParameter));

                    //cmd.Parameters.Add("@avgImgInSurv", SqlDbType.Int);
                    //cmd.Parameters["@avgImgInSurv"].Direction = ParameterDirection.Output;
                    //s.avgImgInSurv = Convert.ToInt32(cmd.Parameters["@avgImgInSurv"].Value);
                    outPutParameter.ParameterName = "@avgVoteNum";
                    outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
                    outPutParameter.Direction = System.Data.ParameterDirection.Output;
                    s.avgVoteNum = Convert.ToInt32(cmd.Parameters.Add(outPutParameter));

                    //cmd.Parameters.Add("@avgVoteNum", SqlDbType.Int);
                    //cmd.Parameters["@avgVoteNum"].Direction = ParameterDirection.Output;
                    //s.avgVoteNum = Convert.ToInt32(cmd.Parameters["@avgVoteNum"].Value);

                    cmd.Parameters.Add("@voteNum", SqlDbType.Int);
                    cmd.Parameters["@voteNum"].Direction = ParameterDirection.Output;
                    s.voteNum = Convert.ToInt32(cmd.Parameters["@voteNum"].Value);

                    cmd.Parameters.Add("@numOfCreatedSurv", SqlDbType.Int);
                    cmd.Parameters["@numOfCreatedSurv"].Direction = ParameterDirection.Output;
                    s.numOfCreatedSurv = Convert.ToInt32(cmd.Parameters["@numOfCreatedSurv"].Value);

                    cmd.Parameters.Add("@numOfVisitors", SqlDbType.Int);
                    cmd.Parameters["@numOfVisitors"].Direction = ParameterDirection.Output;
                    s.numOfVisitors = Convert.ToInt32(cmd.Parameters["@numOfVisitors"].Value);

                    cmd.Parameters.Add("@numOfEmails", SqlDbType.Int);
                    cmd.Parameters["@numOfEmails"].Direction = ParameterDirection.Output;
                    s.numOfEmails = Convert.ToInt32(cmd.Parameters["@numOfEmails"].Value);
                    try
                    {
                        Sqlcon.Open();
                        cmd.ExecuteNonQuery();

                    }catch(Exception e)
                    {
                        throw new Exception("Błąd podczas aktualizacji statystyk!");
                    }
                    finally
                    {
                        Sqlcon.Close();
                    }
                }
            }
            return s;
        }
    }
}