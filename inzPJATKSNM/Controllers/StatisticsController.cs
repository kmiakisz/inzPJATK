﻿using System;
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

                    cmd.Parameters.Add("@avgImgInSurv", SqlDbType.Int);
                    cmd.Parameters["@avgImgInSurv"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@avgVoteNum", SqlDbType.Int);
                    cmd.Parameters["@avgVoteNum"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@voteNum", SqlDbType.Int);
                    cmd.Parameters["@voteNum"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@numOfCreatedSurv", SqlDbType.Int);
                    cmd.Parameters["@numOfCreatedSurv"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@numOfVisitors", SqlDbType.Int);
                    cmd.Parameters["@numOfVisitors"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@numOfEmails", SqlDbType.Int);
                    cmd.Parameters["@numOfEmails"].Direction = ParameterDirection.Output;
                    try
                    {
                        Sqlcon.Open();
                        cmd.ExecuteNonQuery();
                        s.avgImgInSurv = Convert.ToInt32(cmd.Parameters["@avgImgInSurv"].Value);
                        s.avgVoteNum = Convert.ToInt32(cmd.Parameters["@avgVoteNum"].Value);
                        s.voteNum = Convert.ToInt32(cmd.Parameters["@voteNum"].Value);
                        s.numOfCreatedSurv = Convert.ToInt32(cmd.Parameters["@numOfCreatedSurv"].Value);
                        s.numOfVisitors = Convert.ToInt32(cmd.Parameters["@numOfVisitors"].Value);
                        s.numOfEmails = Convert.ToInt32(cmd.Parameters["@numOfEmails"].Value);
                    }
                    catch (Exception e)
                    {
                        inzPJATKSNM.Controllers.ErrorLogController.logToDb("AvgImagesInSurveys", e.Message);
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

        public static Statistic StatisticPerSurvey(int surveyId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            Statistic s = new Statistic();
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("StatisticsPerSurvey", Sqlcon)) //statystyki oper ankieta
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter outPutParameter = new SqlParameter();

                        cmd.Parameters.Add("@NumOfVotersOnSurvey", SqlDbType.Int);
                        cmd.Parameters["@NumOfVotersOnSurvey"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@NumOfVisitors", SqlDbType.Int);
                        cmd.Parameters["@NumOfVisitors"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@NumOfSubs", SqlDbType.Int);
                        cmd.Parameters["@NumOfSubs"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@ImgMaxVoteNumName", SqlDbType.VarChar);
                        cmd.Parameters["@ImgMaxVoteNumName"].Direction = ParameterDirection.Output;
                        cmd.Parameters["@ImgMaxVoteNumName"].Size = 250;

                        cmd.Parameters.Add("@ImgMinVoteNumName", SqlDbType.VarChar);
                        cmd.Parameters["@ImgMinVoteNumName"].Direction = ParameterDirection.Output;
                        cmd.Parameters["@ImgMinVoteNumName"].Size = 250;

                        cmd.Parameters.Add("@ImgMaxVoteNumUrl", SqlDbType.VarChar);
                        cmd.Parameters["@ImgMaxVoteNumUrl"].Direction = ParameterDirection.Output;
                        cmd.Parameters["@ImgMaxVoteNumUrl"].Size = 250;

                        cmd.Parameters.Add("@ImgMinVoteNumUrl", SqlDbType.VarChar);
                        cmd.Parameters["@ImgMinVoteNumUrl"].Direction = ParameterDirection.Output;
                        cmd.Parameters["@ImgMinVoteNumUrl"].Size = 250;

                        Sqlcon.Open();
                        cmd.Parameters.Add("@SurveyId", SqlDbType.Int);
                        cmd.Parameters["@SurveyId"].Value = surveyId;
                        cmd.ExecuteNonQuery();
                        s.NumOfVotersOnSurvey = Convert.ToInt32(cmd.Parameters["@NumOfVotersOnSurvey"].Value);
                        s.NumOfVisitors = Convert.ToInt32(cmd.Parameters["@NumOfVisitors"].Value);
                        s.NumOfSubs = Convert.ToInt32(cmd.Parameters["@NumOfSubs"].Value);
                        s.ImgMaxVoteNumName = Convert.ToString(cmd.Parameters["@ImgMaxVoteNumName"].Value);
                        s.ImgMinVoteNumName = Convert.ToString(cmd.Parameters["@ImgMinVoteNumName"].Value);
                        s.ImgMaxVoteNumUrl = Convert.ToString(cmd.Parameters["@ImgMaxVoteNumUrl"].Value);
                        s.ImgMinVoteNumUrl = Convert.ToString(cmd.Parameters["@ImgMinVoteNumUrl"].Value);

                    }
                }
                catch (Exception e)
                {
                    inzPJATKSNM.Controllers.ErrorLogController.logToDb("StatisticPerSurvey", e.Message);
                    throw new Exception(e.Message);
                }
                finally
                {
                    Sqlcon.Close();
                }
            }
            return s;
        }
        public static List<Statistic> DrawChart(int surveyId)
        {
            List<Statistic> statList = new List<Statistic>();
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            string query = "select avg(ocena),id_zdjecia,tytuł from Ocena inner join Dzieło on id_zdjecia = id_dzieło where id_ankiety = " + surveyId + " group by id_zdjecia,tytuł;";
            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(query, Sqlcon);
                Sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Statistic s = new Statistic();
                        s.mark = Convert.ToInt32(reader[0]);
                        s.photoId = Convert.ToInt32(reader[1]);
                        s.photoName = reader[2].ToString();
                        statList.Add(s);
                    }
                }
                catch (Exception e)
                {
                    inzPJATKSNM.Controllers.ErrorLogController.logToDb("DrawChart", e.Message);
                    throw new Exception(e.Message);
                }
                finally
                {
                    Sqlcon.Close();
                }

                Sqlcon.Close();
            }
            return statList;
        }

        public static String GetSurveyName(int surveyId)
        {
            String surveyName = "";
            String connStr = ConfigurationManager.ConnectionStrings["inzSNMConnectionString"].ConnectionString;
            string query = "SELECT NAZWA FROM ANKIETA WHERE ID_ANKIETY = " + surveyId;

            using (SqlConnection Sqlcon = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, Sqlcon);
                    Sqlcon.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        surveyName = reader[0].ToString();
                    }
                }
                catch (Exception e)
                {
                    inzPJATKSNM.Controllers.ErrorLogController.logToDb("GetSurveyName", e.Message);
                    throw new Exception(e.Message);
                }
                finally
                {
                    Sqlcon.Close();
                }
            }
            return surveyName;
        }
    }
}