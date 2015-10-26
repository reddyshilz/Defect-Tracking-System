using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefectTracker.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace DefectTracker.DataSource
{
    /// <summary>
    /// Hold methods to add/ updtate /select project details to db-Prabasini
    /// </summary>
    public static class ProjectDatasource
    {
        private static string SqlConString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
        static ProjectDatasource()
        {           
        }    
        /// <summary>
        /// Get all projects for the given project ownerID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IEnumerable<Project> GetAll( string userId)
        {
            
            SqlConnection con = new SqlConnection(SqlConString);
            List<Project> lst = new List<Project>();
            try
            {
                string strquery = "Select * from DTSProject where IsActive=1 and Project_Owner = '" + userId + "'";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Project p = new Project();
                        p.Id = int.Parse(reader["Id_Project"].ToString());
                        p.Name = reader["ProjectName"].ToString();
                        p.StartDate =Convert.ToDateTime(reader["Dt_Created"].ToString());
                        p.EndDate = Convert.ToDateTime(reader["Dt_Modify"].ToString());
                        p.Description = reader["Project_Desc"].ToString();
                        p.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                        lst.Add(p);
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
            return lst;
        }
        /// <summary>
        /// Get the details for a specfic projectID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Project Get(int id)
        {
            SqlConnection con = new SqlConnection(SqlConString);           
            Project p = new Project();
            try
            {
                string strquery = "Select * from DTSProject where Id_Project="+ id;
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {                        
                        p.Id = int.Parse(reader["Id_Project"].ToString());
                        p.Name = reader["ProjectName"].ToString();
                        p.StartDate = Convert.ToDateTime(reader["Dt_Created"].ToString());
                        p.EndDate = Convert.ToDateTime(reader["Dt_Modify"].ToString());
                        p.Description = reader["Project_Desc"].ToString();
                        p.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());                       
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
            return p;
        }
        /// <summary>
        /// Check the project already exist in database
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public static bool ProjectExist(string projectName)
        {
            SqlConnection con = new SqlConnection(SqlConString);

            try
            {
                bool isExist = true;
                string query1 = "Select count(*) from DTSProject where ProjectName='" + projectName + "' ";
                SqlCommand cmd = new SqlCommand(query1, con);
                con.Open();
                isExist = Convert.ToBoolean(cmd.ExecuteScalar());
                con.Close();

                return isExist;
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
        /// <summary>
        /// Get the project list under a specific manager to assign to user
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public static IEnumerable<Project> GetProjectsForAssignment(string loginId)
        {
            SqlConnection con = new SqlConnection(SqlConString);
            List<Project> lst = new List<Project>();
            try
            {
                string strquery = "Select * from DTSProject where Project_Owner= '" + loginId +"'";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Project p = new Project();
                        p.Id = int.Parse(reader["Id_Project"].ToString());
                        p.Name = reader["ProjectName"].ToString();                       
                        p.Description = reader["Project_Desc"].ToString();
                        lst.Add(p);
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
            return lst;
        }
        /// <summary>
        /// Method to call add or edit project based on new or existing project
        /// </summary>
        /// <param name="project"></param>
        public static void Add(Project project)
        {
            if(project.Id> 0 )
            {              
                EditProject(project);
            }
            else
            {                
                AddProject(project);
            }        
        }
        /// <summary>
        /// Method to add new project to system
        /// </summary>
        /// <param name="project"></param>
        private static void AddProject(Project project)
        {
            SqlConnection conn = new SqlConnection(SqlConString);
            try
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("sp_addeditdeleteProject", conn);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object           
                cmd.Parameters.AddWithValue("@Proj_Id", project.Id);
                cmd.Parameters.AddWithValue("@ProjName", project.Name);
                cmd.Parameters.AddWithValue("@ProjDesc", project.Description);
                cmd.Parameters.AddWithValue("@IsActive", project.IsActive);
                cmd.Parameters.AddWithValue("@ModifyDate", project.EndDate);
                cmd.Parameters.AddWithValue("@ProjectOwner", project.Owner);
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
        /// update project details for a specific project
        /// </summary>
        /// <param name="project"></param>
        private static void EditProject(Project project)
        {
            SqlConnection conn = new SqlConnection(SqlConString);

            try
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("sp_addeditdeleteProject", conn);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object           
                cmd.Parameters.AddWithValue("@Proj_Id", project.Id);
                cmd.Parameters.AddWithValue("@ProjName", project.Name);
                cmd.Parameters.AddWithValue("@ProjDesc", project.Description);
                cmd.Parameters.AddWithValue("@IsActive", project.IsActive);
                cmd.Parameters.AddWithValue("@ModifyDate", project.EndDate);
                cmd.Parameters.AddWithValue("@StatementType", "Update");

                //Open the connection and execute the query
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            { conn.Close();  }

        }
        /// <summary>
        /// Delete a specific project
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            SqlConnection conn = new SqlConnection(SqlConString);
            try
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("sp_addeditdeleteProject", conn);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object           
                cmd.Parameters.AddWithValue("@Proj_Id", id);
                cmd.Parameters.AddWithValue("@StatementType", "Delete");

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
    }
}