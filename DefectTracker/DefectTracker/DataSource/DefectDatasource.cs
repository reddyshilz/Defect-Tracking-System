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
    /// <summary>
    /// Hold methods to add/update/select user details to/from db- Prabasini & Rekha
    /// </summary>
    public static class DefectDatasource
    {
        //define connection string
        private static string SqlConString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
        /// <summary>
        /// Get status data to populate status dropdown
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get priority data to populate priority dropdown
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get data from db to polate stageoforigin dropdown
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get all defect type to populate defect type dropdown
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Method to add defect in the database
        /// </summary>
        /// <param name="defect"></param>
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
        /// <summary>
        /// Method to update defect in the database
        /// </summary>
        /// <param name="defect"></param>
        public static void UpdateDefect(Defect defect)
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
                cmd.Parameters.AddWithValue("@FixedBy", defect.FixedBy);
                if (defect.DateClosed == DateTime.MinValue)                    
                     cmd.Parameters.AddWithValue("@ClosedDate", null);
                else
                    cmd.Parameters.AddWithValue("@ClosedDate", defect.DateClosed.ToString());

                cmd.Parameters.AddWithValue("@ClosedBy", defect.ClosedBy);
                cmd.Parameters.AddWithValue("@StatementType", "Update");

                //Open the connection and execute the query
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// get all defect list details from database
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleOfLogin"></param>
        /// <returns></returns>
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
                        defect.Id = int.Parse(reader["Id_Defect"].ToString());
                        defect.ProjectName  = reader["ProjectName"].ToString();
                        defect.Title = reader["Txt_Title"].ToString();
                        defect.DefectType = reader["Txt_DefType"].ToString();
                        defect.Status = reader["Txt_Status"].ToString();
                        defect.CreatedBy = reader["Txt_CreatedBy"].ToString();
                        defect.AssignedTo = reader["Txt_AssignTo"].ToString();
                        defect.FixedBy = reader["Txt_FixedBy"].ToString();
                        defect.ClosedBy = reader["Txt_ClosedBy"].ToString();
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
        /// <summary>
        /// Get user list to assign defect for a particular project
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<User> GetAssignUserList(int projectId)
        {
            SqlConnection con = new SqlConnection(SqlConString);
            List<User> usrList = new List<User>();
            try
            {
                string strquery = "Select usr.id, usr.UserId from DTSUserProject up inner join DTSUserProfile usr on up.UserID =usr.Id "+
                    "where projectId= "+ projectId;
                SqlCommand cmd = new SqlCommand(strquery, con); 
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var usr = new User();
                        usr.Id = int.Parse(reader["id"].ToString());
                        usr.UserId = reader["UserId"].ToString();

                        usrList.Add(usr);
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
            return usrList;
        }
        /// <summary>
        /// Get user list user under the manager
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<User> GetAssignUserList(string projOwner)
        {
            SqlConnection con = new SqlConnection(SqlConString);
            List<User> usrList = new List<User>();
            try
            {
                string strquery = " Select distinct (usr.UserId) from DTSUserProject up inner join DTSUserProfile usr on up.UserID =usr.Id "+
                    " where up.ProjectMgrId='" + projOwner +"'";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var usr = new User();
                       // usr.Id = int.Parse(reader["id"].ToString());
                        usr.UserId = reader["UserId"].ToString();

                        usrList.Add(usr);
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
            return usrList;
        }
        /// <summary>
        /// Get the defect details of a particular defectid
        /// </summary>
        /// <param name="defectId"></param>
        /// <returns></returns>
        public static Defect GetDefectById(int defectId)
        {
            SqlConnection con = new SqlConnection(SqlConString);
            Defect defect = new Defect();
            try
            {               
                SqlCommand cmd = new SqlCommand("sp_GetDefectDetails", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //Add the input parameters to the command object           
                cmd.Parameters.AddWithValue("@DefectID", defectId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        defect.Id = int.Parse(reader["Id_Defect"].ToString());
                        defect.ProjectId = int.Parse(reader["Id_Project"].ToString());

                        defect.ProjectName = reader["ProjectName"].ToString();                      
                        defect.StageOfOrigin = reader["Id_StageOfOrigin"].ToString();
                        defect.Priority = reader["Id_Priority"].ToString();
                        defect.DefectType = reader["Id_DefType"].ToString();
                        defect.Status = reader["Id_Status"].ToString();
                        defect.Title = reader["Txt_Title"].ToString();                        
                        defect.HowFixed = reader["Txt_HowFixed"].ToString();
                        defect.Description = reader["Txt_Description"].ToString();
                        defect.CreatedBy = reader["Txt_CreatedBy"].ToString();
                        defect.AssignedTo = reader["Txt_AssignTo"].ToString();

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
            return defect;
        }
        /// <summary>
        /// this method delete the defect from UI not from the DB
        /// </summary>
        /// <param name="defectId"></param>
        public static void DeleteDefect(int defectId)
        {
            SqlConnection con = new SqlConnection(SqlConString);

            try
            {               
                string query1 = "Update DTSDefects Set IsActive = 0 where Id_Defect = "+ defectId;
                SqlCommand cmd = new SqlCommand(query1, con);
                con.Open();
               cmd.ExecuteNonQuery();
                              
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null) con.Close();
            }
        }
    }
}