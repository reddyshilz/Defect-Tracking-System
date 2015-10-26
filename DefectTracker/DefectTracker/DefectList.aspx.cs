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
    public partial class DefectList : System.Web.UI.Page
    {
        public IEnumerable<Defect> defectList = new List<Defect>();

        protected void Page_Load(object sender, EventArgs e)
        {
            string roleOfLogin = Session["Role"].ToString();
            string loginId = HttpContext.Current.User.Identity.Name;
            if (!IsPostBack)
            {
                defectList = DefectDatasource.GetAllDefectList(loginId, roleOfLogin);
            }
        }
                
    }
}