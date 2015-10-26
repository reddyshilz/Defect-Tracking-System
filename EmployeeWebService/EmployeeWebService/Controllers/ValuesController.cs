using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

namespace EmployeeWebService.Controllers
{
    /// <summary>
    /// Webapi accept loginId and return user details according to the input parameter
    /// </summary>
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private static string SqlConString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
        public EmployeeController()
        {

        }

        [HttpGet]        
        public Employee GetEmployee(string Id)
        {
            //http://localhost:20323/api/employee/Praba
            SqlConnection conn = new SqlConnection(SqlConString);
            try
            {
                Employee emp = new Employee();
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("sp_SelectEmployeeDetails", conn);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object           
                cmd.Parameters.AddWithValue("@loginId", Id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        emp.FirstName = reader["FirstName"].ToString();
                        emp.LastName = reader["LastName"].ToString();
                        emp.LoginName = reader["LoginName"].ToString();
                        emp.EmailId = reader["EmailId"].ToString();
                        emp.Role = reader["Role"].ToString();
                    }
                }
                reader.Close();

                return emp;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }
        }
    }
    /// <summary>
    /// Class to hold employee details
    /// </summary>
    public class Employee
    {
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string LoginName { get; set; }
        public string Role { get; set; }
        public DateTime DateJoined { get; set; }
    }     
}
