using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Http;
using DefectTracker.Models;
using DefectTracker.DataSource;

namespace DefectTracker
{
    /// <summary>
    /// This is the intermediate page to handle all webmethods for maintain user functionality
    /// This contains methods to call db layer methods to add/edit/get user details- By Prabasini & Rekha
    /// </summary>
    public partial class ManageUserApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Call the db method to get user details based on their Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public static User GetUserById(int userId)
        {
            var user = UserDatasource.GetUserById(userId);
            return user;
        }
        /// <summary>
        /// call db method to check if user exist in db else add to database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [WebMethod]
        public static HttpResponseMessage SaveUser(object user)
        {
            try
            {
                var dbUser = GetUser(user);
                if(dbUser.Id ==0)
                {
                    if (IsValidEmail(dbUser.EmailId))
                    { 
                        var userDB = UserDatasource.GetUserById(dbUser.UserId);

                        if (userDB.UserId != null  && userDB.UserId.ToUpper() == dbUser.UserId.ToUpper())
                        {
                            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                            {
                                ReasonPhrase = "User name already in the database"
                            };
                            return response;
                        }
                    }
                    else
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            ReasonPhrase = "User Not exist in Webapi."
                        };
                        return response;
                    }
                }
               
            UserDatasource.AddUser(dbUser);
            return new HttpResponseMessage(HttpStatusCode.OK);               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        /// <summary>
        /// call the db method to get the allocated project for a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<ProjectAssignment> GetAssignedProject(string userId)
        {
            return UserDatasource.GetAssignedProject(userId);
        }
        /// <summary>
        /// call db method to add or remove allocated project for a particular user
        /// </summary>
        /// <param name="assignments"></param>
        [WebMethod]
        public static void AddRemoveProject(IEnumerable<ProjectAssignment> assignments)
        {
            UserDatasource.AddRemoveProject(assignments);
        }
        /// <summary>
        /// call the db method to delete user from db
        /// </summary>
        /// <param name="userId"></param>
        [WebMethod]
        public static void DeleteUser(string userId)
        {
            UserDatasource.DeleteUser(userId);
        }
        /// <summary>
        /// call the db methods to get roles available in db to populate role dropdown
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<Role> GetRoles()
        {
            var role = UserDatasource.GetRoles();
            return role;
        }
        /// <summary>
        /// method to read UI for allocated project screen
        /// </summary>
        /// <param name="assignments"></param>
        /// <returns></returns>
        private static IEnumerable<ProjectAssignment> GetProjectAssignments(object[] assignments)
        {
            if (assignments == null)
            {
                return null;
            }
            var passignmetns = new List<ProjectAssignment>();
            foreach (var assignment in assignments)
            {
                var passignment = new ProjectAssignment();
                var tmp = (Dictionary<string, object>)assignment;
                object objUserId = null;
                object objProjectId = null;
                if (tmp.TryGetValue("UserId", out objUserId))
                    passignment.UserId = int.Parse(objUserId.ToString());

                if (tmp.TryGetValue("ProjectId", out objProjectId))
                    passignment.ProjectId = int.Parse(objProjectId.ToString());
                passignmetns.Add(passignment);
            }
            return passignmetns;
        }
        /// <summary>
        /// Regular expression to validate emailid
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static bool IsValidEmail(string text)
        {
            string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                             + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
            return Regex.IsMatch(text, MatchEmailPattern);
        }
        /// <summary>
        /// private method to read user entered details in Ui & return the obj with data
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static User GetUser(object user)
        {
            var objUser = new User();
            var tmp = (Dictionary<string, object>)user;
            object objId = null;
            object objUserId = null;
            object objFirstName = null;
            object objLastName = null;
            object objEmailId = null;
            object objRoleId = null;

            if (tmp.TryGetValue("Id", out objId))
                objUser.Id = int.Parse(objId.ToString());

            if (tmp.TryGetValue("UserId", out objUserId))
                objUser.UserId = objUserId.ToString();

            if (tmp.TryGetValue("FirstName", out objFirstName))
                objUser.FirstName = objFirstName.ToString();

            if (tmp.TryGetValue("LastName", out objLastName))
                objUser.LastName = objLastName.ToString();

            if (tmp.TryGetValue("EmailId", out objEmailId))
                objUser.EmailId = objEmailId.ToString();

            if (tmp.TryGetValue("RoleId", out objRoleId))
                objUser.RoleId = int.Parse(objRoleId.ToString());
            objUser.Password = UserDatasource.PasswordGenerator();
            return objUser;
        }
    }
}