using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Net;
using System.Net.Http;
using System.Web.UI.WebControls;
using System.Configuration;
using DefectTracker.Models;
using DefectTracker.DataSource;
using DefectTracker.Notification;
using DefectTracker.SMSUtility;
using Email = DefectTracker.DataSource.Email;

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
        /// <summary>
        /// call the private method to get the defect details entered in UI &
        /// then call db method to save defect details in database
        /// </summary>
        /// <param name="defect"></param>
        [WebMethod]
        public static HttpResponseMessage SaveDefect(object defect)
        {
            try
            {                
                var dbDefect = GetDefectDetails(defect);

                var message=new Message();
                var notificationSystem=new DefectTrackerNotification();
                notificationSystem.AttachEngine(new EmailUtility.Email());
                notificationSystem.AttachEngine(new Sms());

                string usrRole = UserDatasource.UserRole(HttpContext.Current.User.Identity.Name);
                if (dbDefect.Id > 0)//Edit defect details
                {
                    var defOlddetails = DefectDatasource.GetDefectById(dbDefect.Id);

                    string selStatus = dbDefect.Status;
                    if (defOlddetails.Status  == "1" && usrRole == "PM")//role pm & open defect
                    { 
                        if (selStatus == "2" && dbDefect.AssignedTo == "")//PM & status=assigned
                        {
                            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                            {
                                ReasonPhrase = "Please assign to developer to fix."
                            };
                            return response;
                        }
                        else if (selStatus == "2" && dbDefect.AssignedTo != "")//ASSIGNED
                        {
                            User usr = UserDatasource.GetUserById(dbDefect.AssignedTo);
                            message.ToAddress = usr.EmailId;
                            message.Body = "Your project manager has assigned a defect to you.\n" +
                                "Please Login to DTS to see the defect & work on it.";
                            DefectDatasource.UpdateDefect(dbDefect);
                            notificationSystem.Message = message;
                            notificationSystem.Send();
                        }
                        else if (selStatus =="1" && dbDefect.AssignedTo=="")
                        {
                            DefectDatasource.UpdateDefect(dbDefect);
                        }
                        else
                        {
                            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                            {
                                ReasonPhrase = "Please change status as assigned & assign defect to user."
                            };
                            return response;
                        }
                    }
                    else if (defOlddetails.Status=="2")
                    {
                        if (selStatus == "1" || selStatus == "5")//if user is trying to change status to open from assigned
                        {
                            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                            {
                                ReasonPhrase = "It's already assigned, Can't change to Open or fixed."
                            };
                            return response;
                        }
                        else if(selStatus == "3")//Inprogress
                        {
                            DefectDatasource.UpdateDefect(dbDefect);
                        }
                        else if(selStatus == "4")//fixed
                        {
                            dbDefect.FixedBy = HttpContext.Current.User.Identity.Name;
                            User usr = UserDatasource.GetUserById(defOlddetails.CreatedBy);
                            message.ToAddress = usr.EmailId;
                            message.Body = "Defect has been fixed. \n" +
                               "Please Login to DTS to validate the defect & close it.";
                            DefectDatasource.UpdateDefect(dbDefect);
                            notificationSystem.Message = message;
                            notificationSystem.Send();
                        }                       
                        else
                        {
                            DefectDatasource.UpdateDefect(dbDefect);
                        }
                    }
                    else if (defOlddetails.Status=="3")//Inprogrss
                    {
                        //if user is trying to change status from Inprogress to open or assigned
                        if (selStatus == "1" || selStatus == "2" || selStatus == "5")
                        {
                            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                            {
                                ReasonPhrase = "It's already Inprogress, Can't change to open or assign."
                            };
                            return response;
                        }
                        else //if (selStatus == "4")//fixed
                        {
                            dbDefect.FixedBy = HttpContext.Current.User.Identity.Name;
                            User usr = UserDatasource.GetUserById(defOlddetails.CreatedBy);
                            
                            message.ToAddress = usr.EmailId;
                            message.Body = "Defect has been fixed. \n" +
                               "Please Login to DTS to validate the defect & close it.";
                            //DefectDatasource.UpdateDefect(dbDefect);
                            notificationSystem.Message = message;
                            notificationSystem.Send();
                        }

                    }
                    else if (defOlddetails.Status=="4")
                    {
                         if(selStatus == "5")//Closed
                        {
                            dbDefect.ClosedBy = HttpContext.Current.User.Identity.Name;
                            dbDefect.DateClosed = DateTime.Today;
                            DefectDatasource.UpdateDefect(dbDefect);
                        }
                         else
                         {
                             var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                             {
                                 ReasonPhrase = "It's already Inprogress, Can't change to open or assign."
                             };
                             return response;
                         }
                    }
                    else
                    {
                        if(defOlddetails.Status=="1" && selStatus=="1")
                        {
                            DefectDatasource.UpdateDefect(dbDefect);
                        }
                        else
                        {
                            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                            {
                                ReasonPhrase = "Not authorized to change status to other."
                            };
                            return response;
                        }
                    }
                   
                }
                else //log new defect to database
                {
                    Project proj = ProjectDatasource.Get(dbDefect.ProjectId);
                    User usr = UserDatasource.GetUserById(proj.Owner);
                   
                    DefectDatasource.AddDefect(dbDefect);
                    //If defect added to db then send mail to manager to assign defect
                    message.Body = "Defect created in DTS " + ConfigurationManager.AppSettings["webSiteURL"] + "  by " + dbDefect.CreatedBy + ". \n"
                        + "Please assign to somebody to lookinto it.";
                    message.ToAddress = usr.EmailId;
                    notificationSystem.Message = message;
                    notificationSystem.Send();

                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// call db method to get defect details by defectID
        /// </summary>
        /// <param name="defect"></param>
        [WebMethod]
        public static Defect GetDefectById(int defectId)
        {
            var defect = DefectDatasource.GetDefectById(defectId);
            return defect;
           
        }
        /// <summary>
        /// call the db method to get status data to populated status dropdown
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<Status> GetStatus()
        {
            var status = DefectDatasource.GetStatus();
            return status;
        }
        /// <summary>
        /// call the db method to get the priority list to populate priority dropdown
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<Priority> GetPriority()
        {
            var priority = DefectDatasource.GetPriority();
            return priority;
        }
        /// <summary>
        /// call the db method to populate stateoforigin dropdown
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<StageOfOrigin> GetStageOfOrigin()
        {
            var origin = DefectDatasource.GetStageOfOrigin();
            return origin;
        }
        /// <summary>
        /// call the db method to populate DefectType dropdown
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<DefectType> GetDefectType()
        {
            var defType = DefectDatasource.GetDefectType();
            return defType;
        }
        /// <summary>
        /// call the db method to populate project dropdown
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// call the database method to get user list for assignto drop down
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<User> GetAssignedTo()
        {
             User usr = UserDatasource.GetUserById(HttpContext.Current.User.Identity.Name);

             if (usr.Role == "PM")
             {
                 return DefectDatasource.GetAssignUserList(HttpContext.Current.User.Identity.Name);
             }
             else
             {
                 return UserDatasource.GetAllUser();
             }
        }
        /// <summary>
        /// Get details of user to pupulate assignedto dropdown
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<User> PopulateAssignedTo(int projectId)
        {            
            return DefectDatasource.GetAssignUserList(projectId);
        }
        /// <summary>
        /// Call the database method to delete defect 
        /// </summary>
        /// <param name="defectId"></param>
        [WebMethod]
        public static void DeleteDefect(int defectId)
        {
            DefectDatasource.DeleteDefect(defectId);
        }
       /// <summary>
       /// Method to read UI values & store in defect object to return
       /// </summary>
       /// <param name="defect"></param>
       /// <returns></returns>
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