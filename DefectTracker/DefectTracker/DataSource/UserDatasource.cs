using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DefectTracker.Models;
using DefectTracker.DataSource;

namespace DefectTracker.DataSource
{
    /// <summary>
    /// Hold methods to add/update/select user details to/from db- Prabasini
    /// </summary>
    public static class UserDatasource
    {
        private static string SqlConString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

        static UserDatasource()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Validate user exist in the database or not
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool ValidateUser(string userID, string pwd)
        {
            SqlConnection con = new SqlConnection(SqlConString);

            try
            {
                bool verify = true;
                string query1 = "Select count(*) from DTSUserProfile where UserId='" + userID + "' and Password='" + pwd + "' ";
                SqlCommand cmd = new SqlCommand(query1, con);
                con.Open();
                verify = Convert.ToBoolean(cmd.ExecuteScalar());
                con.Close();

                return verify;
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
        /// Return role for a specific user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string UserRole(string userID)
        {
            SqlConnection con = new SqlConnection(SqlConString);
            try
            {
                string query = "Select b.Txt_Role from DTSUserProfile a inner join DTSRole b on a.Id_Role=b.Id_Role where a.UserId ='" + userID + "' and b.IsActive=1";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                string role = cmd.ExecuteScalar().ToString();

                return role;
            }
            finally { if (con != null) con.Close(); }
        }
        /// <summary>
        /// Update password to database -Shilpa
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="userID"></param>
        /// <param name="fromPage"></param>
        public static void UpdatePwd(string pwd, string userID, string fromPage)
        {
             SqlConnection con = new SqlConnection(SqlConString);
            try
            {
                string query = "Update DTSUserProfile set password= '" +pwd +"'  where UserId ='" + userID + "' ";
                if (fromPage =="FP")
                    query = "Update DTSUserProfile set password= '" + pwd + "'  where Email ='" + userID + "' ";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();               
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if (con != null)
                    con.Close();
            }
        }
        /// <summary>
        /// Get all user details from database
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<User> GetAllUser()
        {

            SqlConnection con = new SqlConnection(SqlConString);
            List<User> lst = new List<User>();
            try
            {
                string strquery = "Select u.Id, u.UserId,u.FirstName,u.LastName,u.Email, u.Id_Role,r.Txt_Role from DTSUserProfile u inner join DTSRole r " +
                                    " on u.Id_Role = r.Id_Role   where r.IsActive =1 and u.Id_Role <> 1 ";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        User usr = new User();
                        usr.Id = int.Parse(reader[0].ToString());
                        usr.UserId = reader[1].ToString();
                        usr.FirstName = reader[2].ToString();
                        usr.LastName = reader[3].ToString();
                        usr.EmailId = reader[4].ToString();
                        usr.RoleId = int.Parse(reader[5].ToString());
                        usr.Role = reader[6].ToString();
                        lst.Add(usr);
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
        /// get a specific user details with the login id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static User GetUserById(string userId)
        {
            SqlConnection con = new SqlConnection(SqlConString);           
            User  usr = new User();
            try
            {
                string strquery = "Select u.Id,u.UserId,u.FirstName,u.LastName,u.Email, u.Id_Role,r.Txt_Role, u.password from DTSUserProfile u inner join DTSRole r " +
                                    " on u.Id_Role = r.Id_Role   where r.IsActive =1 and u.UserId = '" + userId + "'";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        usr.Id = int.Parse(reader[0].ToString());
                        usr.UserId = reader[1].ToString();
                        usr.FirstName = reader[2].ToString();
                        usr.LastName = reader[3].ToString();
                        usr.EmailId = reader[4].ToString();
                        usr.RoleId = int.Parse(reader[5].ToString());
                        usr.Role = reader[6].ToString();    
                        usr.Password = reader[7].ToString();   
                       
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
            return usr;
        }
        /// <summary>
        /// Get user by dbid
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static User GetUserById(int Id)
        {
            SqlConnection con = new SqlConnection(SqlConString);
            User usr = new User();
            try
            {
                string strquery = "Select u.Id,u.UserId,u.FirstName,u.LastName,u.Email, u.Id_Role,r.Txt_Role, u.password from DTSUserProfile u inner join DTSRole r " +
                                    " on u.Id_Role = r.Id_Role   where r.IsActive =1 and u.Id = " + Id ;
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        usr.Id = int.Parse(reader[0].ToString());
                        usr.UserId = reader[1].ToString();
                        usr.FirstName = reader[2].ToString();
                        usr.LastName = reader[3].ToString();
                        usr.EmailId = reader[4].ToString();
                        usr.RoleId = int.Parse(reader[5].ToString());
                        usr.Role = reader[6].ToString();
                        usr.Password = reader[7].ToString();

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
            return usr;
        }
        /// <summary>
        /// Check email exist in db
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public static bool IsEmailExist(string emailId)
        {
            SqlConnection con = new SqlConnection(SqlConString);
            try
            {
                string query = "Select a.UserId from DTSUserProfile a where a.Email ='" + emailId + "'";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                 object result = cmd.ExecuteScalar();

                if (result == null)
                    return false;
                else
                    return true;
            }
            catch(Exception ex)
            { 
                throw ex; 
            }
            finally { if (con != null) con.Close(); }

        }
        /// <summary>
        /// Generate a random password and return
        /// </summary>
        /// <returns></returns>
        public static string PasswordGenerator()
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[7];
            Random rd = new Random();

            for (int i = 0; i < 7; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
        /// <summary>
        /// call Add and edit method
        /// </summary>
        /// <param name="user"></param>
        public static void AddUser(User user)
        {
            if (user.Id > 0)            
                EditUserDetails(user);            
            else           
                AddNewUser(user);            
        }
        /// <summary>
        /// Add new user to database and send mail to user with loginId and password
        /// </summary>
        /// <param name="user"></param>
        private static void AddNewUser(User user)
        {
            SqlConnection conn = new SqlConnection(SqlConString);

            SqlTransaction transaction;
            conn.Open();
            transaction = conn.BeginTransaction();
            try
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("sp_addeditdeleteUser", conn, transaction);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object     
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
                cmd.Parameters.AddWithValue("@RoleID", user.RoleId);            
                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                           
                cmd.ExecuteNonQuery();
                //If new user added to system, then send mail to User
                string msgBody = "Your manager has given access to use Defect Tracking application. Please use the  given login Id and password \n"+
                    "to log into system. \n Application url: "+ ConfigurationManager.AppSettings["webSiteURL"] + " LoginID: "+user.UserId + " password: "+ user.Password;

                Email.SendMail(user.EmailId, msgBody);
                
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// edit user details in database
        /// </summary>
        /// <param name="user"></param>
        private static void EditUserDetails(User user)
        {
            SqlConnection conn = new SqlConnection(SqlConString);
            try
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("sp_addeditdeleteUser", conn);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object   
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                // cmd.Parameters.AddWithValue("@Password", "TestDTS");
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
                cmd.Parameters.AddWithValue("@RoleID", user.RoleId);
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
            {
                conn.Close();
            }
        }
        /// <summary>
        /// delete user from system
        /// </summary>
        /// <param name="userId"></param>
        public static void DeleteUser(string userId)
        {
            SqlConnection conn = new SqlConnection(SqlConString);
            try
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("sp_addeditdeleteUser", conn);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object   
                cmd.Parameters.AddWithValue("@Id", userId);
                cmd.Parameters.AddWithValue("@UserId", userId);
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
        /// <summary>
        /// Allocate or deaalocate project to user
        /// </summary>
        /// <param name="assignments"></param>
        public static void AddRemoveProject(IEnumerable<ProjectAssignment> assignments)
        {
            var conn = new SqlConnection(SqlConString);
            //var transaction = conn.BeginTransaction();
            var sqlCommand = new SqlCommand { Connection = conn };
            try
            {
                var projectAssignments = assignments as ProjectAssignment[] ?? assignments.ToArray();
                string loginID = HttpContext.Current.User.Identity.Name;
                conn.Open();

                var strquery = "Delete from dbo.DTSUserProject where ProjectMgrId= '" + loginID + "'";
                   
                sqlCommand.CommandText = strquery;                   
                sqlCommand.ExecuteNonQuery();
                
               
                sqlCommand.CommandText = "SP_AddRemoveProject";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (var assginment in projectAssignments)
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@UserId", assginment.UserId);
                    sqlCommand.Parameters.AddWithValue("@ProjectId", assginment.ProjectId);
                    sqlCommand.Parameters.AddWithValue("@ProjectMgrId", loginID);
                    sqlCommand.ExecuteNonQuery();
                }
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                //transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// Get the allocated project for specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IEnumerable<ProjectAssignment> GetAssignedProject(string userId)
        {
            var conn = new SqlConnection(SqlConString);
            var assignments = new List<ProjectAssignment>();
            try
            {
                var sqlQuery = string.Format("select UserId,ProjectId from DTSUserProject where UserId={0} and ProjectMgrId='"+HttpContext.Current.User.Identity.Name +"'", userId);
                var sqlCommand = new SqlCommand { CommandText = sqlQuery, Connection = conn };
                conn.Open();
                var sqlReader = sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    var ap = new ProjectAssignment
                    {
                        UserId = int.Parse(sqlReader[0].ToString()),
                        ProjectId = int.Parse(sqlReader[1].ToString())
                    };
                    assignments.Add(ap);
                }

            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            return assignments;
        }
        /// <summary>
        /// get roles to populate role dropdown
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Role> GetRoles()
        {
            SqlConnection con = new SqlConnection(SqlConString);
            List<Role> roles = new List<Role>();
            try
            {
                string strquery = "Select * from DTSRole";
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var role = new Role();
                        role.RoleId = int.Parse(reader[0].ToString());
                        role.Description = reader[1].ToString();
                        role.IsActive = Convert.ToBoolean(reader[2].ToString());
                        roles.Add(role);
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
            return roles;
        }
        /// <summary>
        /// get allocated project list for a specific user
        /// </summary>
        /// <param name="dbUserId"></param>
        /// <returns></returns>
        public static IEnumerable<Project> GetUserProjects(int dbUserId)
        {
            SqlConnection con = new SqlConnection(SqlConString);
            List<Project> projects = new List<Project>();
            try
            {
                string strquery = "select p.Id_Project,p.ProjectName from DTSUserProject up inner join DTSProject p on up.ProjectId = p.Id_Project"+
                                    "where up.UserId =" + dbUserId;
                SqlCommand cmd = new SqlCommand(strquery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var project = new Project();
                        project.Id = int.Parse(reader["Id_Project"].ToString());
                        project.Name = reader["ProjectName"].ToString();

                        projects.Add(project);
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
            return projects;
        }
        
    }
}