using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DefectTracker.DataSource;
using DefectTracker.Models;
 
namespace DefectTracker
{
    public partial class DefectReport : System.Web.UI.Page
    {
        public IEnumerable<Defect> DefectList = new List<Defect>();
        /// <summary>
        /// 1st time page load this method prepopulate the dropdown to 
        /// set the criteria to generate report. By default the page will load with all
        /// defects -- by Prabasini 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string roleOfLogin = Session["Role"].ToString();
                string loginId = HttpContext.Current.User.Identity.Name;
                if (!IsPostBack)
                {
                    PopulateProject();
                    PopulateDefectType();
                    PopulatePriority();
                    PopulateStatus();
                    PopulateCreatedBy();
                    DefectList = DefectDatasource.GetAllDefectList(loginId, roleOfLogin);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// this event will fire when click on generate report button
        /// It will call the method to get defect list based on user selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSearchClick(object sender, EventArgs e)
        {
            try
            {
                int projectId = 0;
                int defectTypeId = 0;
                int defectPriorityId = 0;
                int statusId = 0;
                string createdBy = "";
                //string loginId = HttpContext.Current.User.Identity.Name;

                if (ddlProject.SelectedValue != "0")
                    projectId = int.Parse(ddlProject.SelectedValue);
                if (ddlPriority.SelectedValue != "0")
                    defectPriorityId = int.Parse(ddlPriority.SelectedValue);
                if (ddlStatus.SelectedValue != "0")
                    statusId = int.Parse(ddlStatus.SelectedValue);
                if (ddlType.SelectedValue != "0")
                    defectTypeId = int.Parse(ddlType.SelectedValue);
                if (CreatedBy.SelectedValue != "0")
                    createdBy = CreatedBy.SelectedValue;

                DefectList = ReportDatasource.GetDefectListBasedOnFilter(projectId, defectTypeId, defectPriorityId, statusId, createdBy);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// this event fired on onclick of reset button. It reset the
        /// report search criteria.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnResetClick(object sender, EventArgs e)
        {
            try
            {
                string roleOfLogin = Session["Role"].ToString();
                string loginId = HttpContext.Current.User.Identity.Name;

                ddlPriority.ClearSelection();
                ddlProject.ClearSelection();
                ddlStatus.ClearSelection();
                ddlType.ClearSelection();
                CreatedBy.ClearSelection();
                DefectList = DefectDatasource.GetAllDefectList(loginId, roleOfLogin);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// this method get the list of defect type & bind the defect status dropdown 
        /// </summary>
        private void PopulateStatus()
        {
            try
            { 
                var defectStatus = DefectDatasource.GetStatus();
                ddlStatus.Items.Add(new ListItem("Select DefectStatus", "0"));
                ddlStatus.AppendDataBoundItems = true;
                ddlStatus.DataSource = defectStatus;
                ddlStatus.DataTextField = "Description";
                ddlStatus.DataValueField = "Id";
                ddlStatus.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// this method get the list of priority & bind to priority dropdown
        /// </summary>
        private void PopulatePriority()
        {
            try
            {
                var defectPriority = DefectDatasource.GetPriority();
                ddlPriority.Items.Add(new ListItem("Select Priority", "0"));
                ddlPriority.AppendDataBoundItems = true;
                ddlPriority.DataSource = defectPriority;
                ddlPriority.DataTextField = "Description";
                ddlPriority.DataValueField = "Id";
                ddlPriority.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// this method get the list of defect type & bind the defect type dropdown 
        /// </summary>
        private void PopulateDefectType()
        {
            try
            {
                var defectType = DefectDatasource.GetDefectType();
                ddlType.Items.Add(new ListItem("Select DefectType", "0"));
                ddlType.AppendDataBoundItems = true;
                ddlType.DataSource = defectType;
                ddlType.DataTextField = "Description";
                ddlType.DataValueField = "Id";
                ddlType.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// this method get the list of user to populate created by dropdown 
        /// </summary>
        private void PopulateCreatedBy()
        {
            try
            {
                var userlist = DefectDatasource.GetAssignUserList(HttpContext.Current.User.Identity.Name);
                CreatedBy.Items.Add(new ListItem("Select User", "0"));
                CreatedBy.AppendDataBoundItems = true;
                CreatedBy.DataSource = userlist;
                CreatedBy.DataTextField = "UserId";
                CreatedBy.DataValueField = "UserId";
                CreatedBy.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// This method call the db method to get project list & bind the project drop down
        /// </summary>
        private void PopulateProject()
        {
            try
            {
                string loginId = HttpContext.Current.User.Identity.Name;

                IEnumerable<Project> projectList = ProjectDatasource.GetProjectsForAssignment(loginId);
                ddlProject.Items.Add(new ListItem("Select Project", "0"));
                ddlProject.AppendDataBoundItems = true;
                ddlProject.DataSource = projectList;
                ddlProject.DataTextField = "Name";
                ddlProject.DataValueField = "Id";
                ddlProject.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
               
    }
}