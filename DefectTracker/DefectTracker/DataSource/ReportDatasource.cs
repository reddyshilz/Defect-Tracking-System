using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DefectTracker.Models;

namespace DefectTracker.DataSource
{
    public static class ReportDatasource
    {
        //define connection string
        private static string SqlConString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
        /// <summary>
        /// This method get the defect list based on filter search criteria -Prabasini & Shilpa
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="typeId"></param>
        /// <param name="priorityId"></param>
        /// <param name="statusId"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public static IEnumerable<Defect> GetDefectListBasedOnFilter(int projectId,int typeId,int priorityId,int statusId, string createdBy)
        {
            SqlConnection conn = new SqlConnection(SqlConString);
            List<Defect> defectlist = new List<Defect>();
            try
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("sp_GetDefectsBasedOnFilter", conn);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object                
                cmd.Parameters.AddWithValue("@Project", projectId);
                cmd.Parameters.AddWithValue("@DefectType", typeId);
                cmd.Parameters.AddWithValue("@DefectPriority", priorityId);
                cmd.Parameters.AddWithValue("@DefectStatus", statusId);
                cmd.Parameters.AddWithValue("@DefectCreatedBy", createdBy);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();//define reader object  and populate data in it
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Defect defect = new Defect();
                        defect.Id = int.Parse(reader["Id_Defect"].ToString());
                        defect.ProjectName = reader["ProjectName"].ToString();
                        defect.Title = reader["Txt_Title"].ToString();
                        defect.DefectType = reader["Txt_DefType"].ToString();
                        defect.Status = reader["Txt_Status"].ToString();
                        defect.CreatedBy = reader["Txt_CreatedBy"].ToString();
                        defect.AssignedTo = reader["Txt_AssignTo"].ToString();
                        defect.FixedBy = reader["Txt_FixedBy"].ToString();
                        defect.ClosedBy = reader["Txt_ClosedBy"].ToString();
                        defect.Priority = reader["Txt_Priority"].ToString();
                        if (reader["Dt_ClosedBy"] != System.DBNull.Value)
                            defect.DateClosed = Convert.ToDateTime(reader["Dt_ClosedBy"]);
                        defectlist.Add(defect);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return defectlist;
        }
    }

}