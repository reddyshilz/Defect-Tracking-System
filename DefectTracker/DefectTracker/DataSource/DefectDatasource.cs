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
    public static class DefectDatasource
    {
        private static string SqlConString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

        public static IEnumerable<Status> GetStatus()
        {
            SqlConnection con = new SqlConnection(SqlConString);
            List<Status> statusList = new List<Status>();
            try
            {
                string strquery = "Select * from DTSStatus where IsActive = 1";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var status = new Status();
                        status.Id = int.Parse(reader[0].ToString());
                        status.Description = reader[1].ToString();
                        status.IsActive = Convert.ToBoolean(reader[2].ToString());
                        statusList.Add(status);
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
                if (con != null) con.Close();
            }
            return statusList;
        }
        public static IEnumerable<Priority> GetPriority()
        {
            SqlConnection con = new SqlConnection(SqlConString);
            List<Priority> priorityList = new List<Priority>();
            try
            {
                string strquery = "Select * from DTSPriority where IsActive = 1";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var priority = new Priority();
                        priority.Id = int.Parse(reader[0].ToString());
                        priority.Description = reader[1].ToString();
                        priority.IsActive = Convert.ToBoolean(reader[2].ToString());
                        priorityList.Add(priority);
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
                if (con != null) con.Close();
            }
            return priorityList;
        }
        public static IEnumerable<StageOfOrigin> GetStageOfOrigin()
        {
            SqlConnection con = new SqlConnection(SqlConString);
            List<StageOfOrigin> originList = new List<StageOfOrigin>();
            try
            {
                string strquery = "Select * from DTSStageOfOrigin where IsActive = 1";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var origin = new StageOfOrigin();
                        origin.Id = int.Parse(reader[0].ToString());
                        origin.Description = reader[1].ToString();
                        origin.IsActive = Convert.ToBoolean(reader[2].ToString());
                        originList.Add(origin);
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
                if (con != null) con.Close();
            }
            return originList;
        }
        public static IEnumerable<DefectType> GetDefectType()
        {
            SqlConnection con = new SqlConnection(SqlConString);
            List<DefectType> defectTypes = new List<DefectType>();
            try
            {
                string strquery = "Select * from DTSDefType where IsActive = 1";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var defType = new DefectType();
                        defType.Id = int.Parse(reader[0].ToString());
                        defType.Description = reader[1].ToString();
                        defType.IsActive = Convert.ToBoolean(reader[2].ToString());
                        defectTypes.Add(defType);
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
                if (con != null) con.Close();
            }
            return defectTypes;
        }

        public static void AddDefect(Defect defect)
        {

            SqlConnection conn = new SqlConnection(SqlConString);
            try
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("sp_InsertUpdateSelectDefect", conn);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object     
                cmd.Parameters.AddWithValue("@Defect_Id", defect.Id);
                cmd.Parameters.AddWithValue("@Proj_Id", defect.ProjectId);
                cmd.Parameters.AddWithValue("@Title", defect.Title);
                cmd.Parameters.AddWithValue("@DefectType_Id", defect.DefectType);
                cmd.Parameters.AddWithValue("@StageOfOrigin_Id", defect.StageOfOrigin);
                cmd.Parameters.AddWithValue("@Priority_Id", defect.Priority);
                cmd.Parameters.AddWithValue("@Status_Id", defect.Status);
                cmd.Parameters.AddWithValue("@AssignTo", defect.AssignedTo);
                cmd.Parameters.AddWithValue("@Description", defect.Description);
                cmd.Parameters.AddWithValue("@HowFixed", defect.HowFixed);
                cmd.Parameters.AddWithValue("@CreatedBy", defect.CreatedBy);
                cmd.Parameters.AddWithValue("@StatementType", "Insert");

                //Open the connection and execute the query
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        public static IEnumerable<Defect> GetAllDefectList(string userID, string roleOfLogin)
        {
            SqlConnection conn = new SqlConnection(SqlConString);
            List<Defect> defectlist = new List<Defect>();
            try
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("sp_GetDefectsList", conn);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object           
                cmd.Parameters.AddWithValue("@LoginID", userID);
                cmd.Parameters.AddWithValue("@Role", roleOfLogin);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Defect defect = new Defect();
                        defect.ProjectName  = reader["ProjectName"].ToString();
                        defect.Title = reader["Txt_Title"].ToString();
                        defect.DefectType = reader["Txt_DefType"].ToString();
                        defect.Status = reader["Txt_Status"].ToString();
                        defect.CreatedBy = reader["Txt_CreatedBy"].ToString();
                        defect.AssignedTo = reader["Txt_AssignTo"].ToString();

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

        public static void DeleteDefect(int defectId)
        {

        }
    }
}