using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using DefectTracker.Models;
using DefectTracker.DataSource;

namespace DefectTracker
{
    public partial class ManageUserApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static User GetUserById(string userId)
        {
            var user = UserDatasource.GetUserById(userId);
            return user;
        }

        [WebMethod]
        public static HttpResponseMessage SaveUser(object user)
        {
            try
            {
                var dbUser = GetUser(user);
                var userDB = UserDatasource.GetUserById(dbUser.UserId);
                if (userDB.UserId == null)
                {
                    UserDatasource.AddUser(dbUser);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        ReasonPhrase = "User name already in the database"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      /*  [WebMethod]
        public static void SaveUser(object user)
        {
            var dbUser = GetUser(user);
            UserDatasource.AddUser(dbUser);
        }*/

        [WebMethod]
        public static IEnumerable<ProjectAssignment> GetAssignedProject(string userId)
        {
            return UserDatasource.GetAssignedProject(userId);
        }

        [WebMethod]
        public static void AddRemoveProject(IEnumerable<ProjectAssignment> assignments)
        {
            UserDatasource.AddRemoveProject(assignments);
        }

        [WebMethod]
        public static void DeleteUser(string userId)
        {
            UserDatasource.DeleteUser(userId);
        }
        [WebMethod]
        public static IEnumerable<Role> GetRoles()
        {
            var role = UserDatasource.GetRoles();
            return role;
        }

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