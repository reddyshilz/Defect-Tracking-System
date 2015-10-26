using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using DefectTracker.Models;
using DefectTracker.DataSource;

namespace DefectTracker
{
    /// <summary>
    /// This is the intermediate page to handle all webmethods for maintain defect functionality
    /// This contains methods to call db layer methods to add/edit/get defect details- By Prabasini
    /// </summary>
    public partial class ManageDefectApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static void SaveDefect(object defect)
        {
            var dbDefect = GetDefectDetails(defect);
            DefectDatasource.AddDefect(dbDefect);
        }
         [WebMethod]
        public static void GetDefectById(object defect)
        {
            var dbDefect = GetDefectDetails(defect);
            DefectDatasource.AddDefect(dbDefect);
        }
        
        [WebMethod]
        public static IEnumerable<Status> GetStatus()
        {
            var status = DefectDatasource.GetStatus();
            return status;
        }

        [WebMethod]
        public static IEnumerable<Priority> GetPriority()
        {
            var priority = DefectDatasource.GetPriority();
            return priority;
        }

        [WebMethod]
        public static IEnumerable<StageOfOrigin> GetStageOfOrigin()
        {
            var origin = DefectDatasource.GetStageOfOrigin();
            return origin;
        }
        [WebMethod]
        public static IEnumerable<DefectType> GetDefectType()
        {
            var defType = DefectDatasource.GetDefectType();
            return defType;
        }
        [WebMethod]
        public static IEnumerable<Project> GetUserProjects()
        {
            string loginId = HttpContext.Current.User.Identity.Name;
            User usr = UserDatasource.GetUserById(loginId);
            IEnumerable<Project> projectList = null; 
            if (usr.Role == "PM")
                projectList = ProjectDatasource.GetProjectsForAssignment(loginId);
            else
                projectList = UserDatasource.GetUserProjects(usr.Id); 

            return projectList;
        }
        //[WebMethod]
        //public static IEnumerable<User> PopulateAssignedTo()
        //{
        //    string loginId = HttpContext.Current.User.Identity.Name;
        //    return UserDatasource.GetUserById(loginId);        
        //}
        [WebMethod]
        public static void DeleteDefect(int defectId)
        {
            DefectDatasource.DeleteDefect(defectId);
        }
        private static Defect GetDefectDetails(object defect)
        {
            var objDefect = new Defect();
            var tmp = (Dictionary<string, object>)defect ;
            object objId = null;
            object objProjectId = null;
            object objTitle = null;
            object objDefectType = null;
            object objDescription = null;
            object objStageOfOrigin = null;
            object objDefectStatus = null;
            object objPriority = null;
            object objAssignedTo = null;
            object objHowFixed = null;

            if (tmp.TryGetValue("Id", out objId))
                objDefect.Id = int.Parse(objId.ToString());

            if (tmp.TryGetValue("ProjectId", out objProjectId))
                objDefect.ProjectId = int.Parse(objProjectId.ToString());

            if (tmp.TryGetValue("Title", out objTitle))
                objDefect.Title = objTitle.ToString();

            if (tmp.TryGetValue("DefectType", out objDefectType))
                objDefect.DefectType = objDefectType.ToString();

            if (tmp.TryGetValue("Description", out objDescription))
                objDefect.Description = objDescription.ToString();

            if (tmp.TryGetValue("StageOfOrigin", out objStageOfOrigin))
                objDefect.StageOfOrigin = objStageOfOrigin.ToString();

            if (tmp.TryGetValue("Status", out objDefectStatus))
                objDefect.Status = objDefectStatus.ToString();

            if (tmp.TryGetValue("AssignedTo", out objAssignedTo))
                objDefect.AssignedTo = objAssignedTo.ToString();

            if (tmp.TryGetValue("HowFixed", out objHowFixed))
                objDefect.HowFixed = objHowFixed.ToString();
            if (tmp.TryGetValue("Priority", out objPriority))
                objDefect.Priority = objPriority.ToString();
            
            objDefect.CreatedBy = HttpContext.Current.User.Identity.Name;

            return objDefect;
        }
    }
}