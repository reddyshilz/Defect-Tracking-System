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
    /// <summary>
    /// /// <summary>
    /// This is the intermediate page to handle all webmethods for maintain project functionality
    /// This contains methods to call db layer methods to add/edit/get project details- By Prabasini
    /// </summary>
    /// </summary>
   public partial class ManageProjectApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       /// <summary>
       /// Call the Db method to project details by project id
       /// </summary>
       /// <param name="projectId"></param>
       /// <returns></returns>
        [WebMethod]
        public static Project GetProjectById(int projectId)
        {
            var project = ProjectDatasource.Get(projectId);
            return project;
        }
       /// <summary>
       /// Call the db method to get all project under the manager to assign to other users
       /// </summary>
       /// <returns></returns>
        [WebMethod]
        public static IEnumerable<Project> GetProjectsForAssignment()
        {
            string loginId = HttpContext.Current.User.Identity.Name;
            var projects = ProjectDatasource.GetProjectsForAssignment(loginId);
            return projects;
        }
       /// <summary>
       /// Call the db method to validate whether same project exist in db 
       /// and save details to db if project not exist
       /// </summary>
       /// <param name="project"></param>
       /// <returns></returns>
        [WebMethod]
        public static HttpResponseMessage SaveProject(object project)
        {
            try
            { 
             var dbProject = GetProject(project);
             if (dbProject.Id == 0)
             {
                 if(ProjectDatasource.ProjectExist(dbProject.Name))                
                 {
                     var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                     { ReasonPhrase = "Same Project already in the database" };
                     return response;
                 }
             }
            ProjectDatasource.Add(dbProject);
            return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
                throw ex;
            }

        }
        /// <summary>
        /// Call db method to delete a project from database
        /// </summary>
        /// <param name="projectId"></param>
        [WebMethod]
        public static void DeleteProject(int projectId)
        {
            ProjectDatasource.Delete(projectId);
        }
       /// <summary>
       /// This is to pupulated the object to contains user's entered data in UI
       /// </summary>
       /// <param name="project"></param>
       /// <returns></returns>
        private static Project GetProject(object project)
        {
            var objProject = new Project();
            var tmp = (Dictionary<string, object>)project;

            object objId = null;
            object objName = null;
            object objDescription = null;
            object objStartDate = null;
            object objEndDate = null;
            object objIsActive = null;

            if (tmp.TryGetValue("Id", out objId))
                objProject.Id = string.IsNullOrEmpty(objId.ToString()) ? 0 : int.Parse(objId.ToString());

            if (tmp.TryGetValue("Name", out objName))
                objProject.Name = objName.ToString();

            if (tmp.TryGetValue("Description", out objDescription))
                objProject.Description = objDescription.ToString();

            if (tmp.TryGetValue("StartDate", out objStartDate))
                objProject.StartDate = DateTime.Parse(objStartDate.ToString());

            if (tmp.TryGetValue("EndDate", out objEndDate))
                objProject.EndDate = DateTime.Parse(objEndDate.ToString());

            if (tmp.TryGetValue("IsActive", out objIsActive))
                objProject.IsActive = bool.Parse(objIsActive.ToString());
            objProject.Owner = HttpContext.Current.User.Identity.Name;

            return objProject;
        }
    }
}