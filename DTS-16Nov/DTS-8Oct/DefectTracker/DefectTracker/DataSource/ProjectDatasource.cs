using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefectTracker.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace DefectTracker.DataSource
{
    public static class ProjectDatasource
    {
        private static string SqlConString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

        //private static readonly Dictionary<int, Project> ProjectData = new Dictionary<int, Project>();
        //private static int _idCounter = 1;
        static ProjectDatasource()
        {
            //if (ProjectData.Count == 0)
            //{
            //    for (int i = 0; i < 15; i++)
            //    {
            //        Project project = new Project()
            //        {
            //            Id = _idCounter++,
            //            Name = "Project - " + i.ToString(),
            //            Description = "Project Description - " + i.ToString(),
            //            StartDate = DateTime.Now,
            //            EndDate = DateTime.Now.AddDays(60),
            //            IsActive = true
            //        };
            //        ProjectData.Add(project.Id, project);
            //    }
            //}
        }

        //public static IEnumerable<Project> GetAllProjects()
        //{
        //    return ProjectData.Values.ToList();
           
        //}
        public static IEnumerable<Project> GetAll()
        {
            
            SqlConnection con = new SqlConnection(SqlConString);
            List<Project> lst = new List<Project>();
            try
            {              
                string strquery = "Select * from DTSProject";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Project p = new Project();
                        p.Id = int.Parse(reader[0].ToString());
                        p.Name = reader[1].ToString();
                        p.StartDate =Convert.ToDateTime(reader[2].ToString());
                        p.EndDate = Convert.ToDateTime(reader[3].ToString());
                        p.Description = reader[4].ToString();
                        p.IsActive = Convert.ToBoolean(reader[5].ToString());
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

        //public static Project GetProject(int id)
        //{
        //    Project project = null;
        //    if (ProjectData.ContainsKey(id))
        //    {
        //        project = ProjectData[id];
        //    }
        //    return project;
        //}
        public static Project Get(int id)
        {
            SqlConnection con = new SqlConnection(SqlConString);
           // List<Project> lst = new List<Project>();
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
                        p.Id = int.Parse(reader[0].ToString());
                        p.Name = reader[1].ToString();
                        p.StartDate = Convert.ToDateTime(reader[2].ToString());
                        p.EndDate = Convert.ToDateTime(reader[3].ToString());
                        p.Description = reader[4].ToString();
                        p.IsActive = Convert.ToBoolean(reader[5].ToString());
                        //lst.Add(p);
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
        public static IEnumerable<Project> GetProjectsForAssignment()
        {
            SqlConnection con = new SqlConnection(SqlConString);
            List<Project> lst = new List<Project>();
            try
            {
                string strquery = "Select * from DTSProject";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Project p = new Project();
                        p.Id = int.Parse(reader[0].ToString());
                        p.Name = reader[1].ToString();
                        //p.StartDate = Convert.ToDateTime(reader[2].ToString());
                        //p.EndDate = Convert.ToDateTime(reader[3].ToString());
                        p.Description = reader[4].ToString();
                        //p.IsActive = Convert.ToBoolean(reader[5].ToString());
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
        public static void Add(Project project)
        {         

            if(project.Id> 0 )
            {
                        //Edit
                EditProject(project);

            }
            else
            {
                 //add
                AddProject(project);
            }

            //if (ProjectData.ContainsKey(project.Id))
            //{
            //    //edit
            //    ProjectData[project.Id] = project;
            //}
            //else
            //{
            //    //add
            //    project.Id = _idCounter++;
            //    ProjectData.Add(project.Id, project);
            //}
        }
        private static void AddProject(Project project)
        {
            SqlConnection conn = new SqlConnection(SqlConString);
                     
           //Create the SqlCommand object
            SqlCommand cmd = new SqlCommand("sp_addeditdeleteProject", conn);
            //Specify that the SqlCommand is a stored procedure
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Add the input parameters to the command object           
            cmd.Parameters.AddWithValue("@Proj_Id", project.Id);
            cmd.Parameters.AddWithValue("@ProjName",project.Name);
            cmd.Parameters.AddWithValue("@ProjDesc", project.Description);
            cmd.Parameters.AddWithValue("@IsActive", project.IsActive);
            cmd.Parameters.AddWithValue("@ModifyDate", project.EndDate);
            cmd.Parameters.AddWithValue("@StatementType", "Insert");
            //cmd.Parameters.Add(new SqlParameter("Id", project.Id));

            //Open the connection and execute the query
            conn.Open();
            cmd.ExecuteNonQuery();

        }
        private static void EditProject(Project project)
        {
            SqlConnection conn = new SqlConnection(SqlConString);
                      
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
            //cmd.Parameters.Add(new SqlParameter("Id", project.Id));

            //Open the connection and execute the query
            conn.Open();
            cmd.ExecuteNonQuery();

        }
        public static void Delete(int id)
        {
            SqlConnection conn = new SqlConnection(SqlConString);

            //Create the SqlCommand object
            SqlCommand cmd = new SqlCommand("sp_addeditdeleteProject", conn);
            //Specify that the SqlCommand is a stored procedure
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Add the input parameters to the command object           
            cmd.Parameters.AddWithValue("@Proj_Id", id);
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            //cmd.Parameters.Add(new SqlParameter("Id", project.Id));

            //Open the connection and execute the query
            conn.Open();
            cmd.ExecuteNonQuery();

        }


        //public static void DeleteProject(int id)
        //{
        //    if (ProjectData.ContainsKey(id))
        //    {
        //        ProjectData.Remove(id);
        //    }
        //}

    }
}