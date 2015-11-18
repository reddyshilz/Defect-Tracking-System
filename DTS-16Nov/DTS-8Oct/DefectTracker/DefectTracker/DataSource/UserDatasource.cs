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
    public static class UserDatasource
    {
        private static string SqlConString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

        static UserDatasource()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
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

        public static IEnumerable<User> GetAllUser()
        {

            SqlConnection con = new SqlConnection(SqlConString);
            List<User> lst = new List<User>();
            try
            {
                string strquery = "Select u.Id, u.UserId,u.FirstName,u.LastName,u.Email, u.Id_Role,r.Txt_Role from DTSUserProfile u inner join DTSRole r " +
                                    " on u.Id_Role = r.Id_Role   where r.IsActive =1";
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
        public static void AddUser(User user)
        {
            if (user.Id > 0)
            {		            
                //Edit
                EditUserDetails(user);

            }
            else
            {
                //add
                AddNewUser(user);
            }
            
        }
        private static void AddNewUser(User user)
        {
            SqlConnection conn = new SqlConnection(SqlConString);

            //Create the SqlCommand object
            SqlCommand cmd = new SqlCommand("sp_addeditdeleteUser", conn);
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
            
            //Open the connection and execute the query
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        private static void EditUserDetails(User user)
        {
            SqlConnection conn = new SqlConnection(SqlConString);

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
            //cmd.Parameters.Add(new SqlParameter("Id", project.Id));

            //Open the connection and execute the query
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        public static void DeleteUser(string userId)
        {
            SqlConnection conn = new SqlConnection(SqlConString);

            //Create the SqlCommand object
            SqlCommand cmd = new SqlCommand("sp_addeditdeleteUser", conn);
            //Specify that the SqlCommand is a stored procedure
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Add the input parameters to the command object   
            cmd.Parameters.AddWithValue("@Id", userId);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            //cmd.Parameters.Add(new SqlParameter("Id", project.Id));

            //Open the connection and execute the query
            conn.Open();
            cmd.ExecuteNonQuery();

        }
        public static void AddRemoveProject(IEnumerable<ProjectAssignment> assignments)
        {
            var conn = new SqlConnection(SqlConString);
            //var transaction = conn.BeginTransaction();
            var sqlCommand = new SqlCommand { Connection = conn };
            try
            {
                var projectAssignments = assignments as ProjectAssignment[] ?? assignments.ToArray();
                var firstOrDefault = projectAssignments.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    var strquery = string.Format("Delete from dbo.DTSUserProject where UserId ={0}",
                        firstOrDefault.UserId);
                    sqlCommand.CommandText = strquery;
                }
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "SP_AddRemoveProject";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (var assginment in projectAssignments)
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@UserId", assginment.UserId);
                    sqlCommand.Parameters.AddWithValue("@projectId", assginment.ProjectId);
                    sqlCommand.ExecuteNonQuery();
                }
                //transaction.Commit();
            }
            catch (Exception)
            {
                //transaction.Rollback();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        public static IEnumerable<ProjectAssignment> GetAssignedProject(string userId)
        {
            var conn = new SqlConnection(SqlConString);
            var assignments = new List<ProjectAssignment>();
            try
            {
                var sqlQuery = string.Format("select UserId,ProjectId from DTSUserProject where UserId={0}", userId);
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
        
    }
}