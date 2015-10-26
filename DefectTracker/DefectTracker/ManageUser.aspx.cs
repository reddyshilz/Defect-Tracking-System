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
    public partial class ManageUser : System.Web.UI.Page
    {
        public IEnumerable<User> UserList = new List<User>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserList = UserDatasource.GetAllUser();
            }
        }
    }
}