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
    /// <summary>
    /// Main page for manage project, list projects & call the modal pop ups to add / edit /delete functionality
    /// By Prabasini, shilpa, Rekha
    /// </summary>
    public partial class ManageProject : System.Web.UI.Page
    {
        public IEnumerable<Project> Projects = new List<Project>();
        /// <summary>
        /// on page load return the project list under the loggedin user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Projects = ProjectDatasource.GetAll(HttpContext.Current.User.Identity.Name);               
            }
        }
    }
}