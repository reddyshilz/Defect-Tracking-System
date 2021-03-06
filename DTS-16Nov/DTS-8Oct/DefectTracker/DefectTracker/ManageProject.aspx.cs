﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DefectTracker.DataSource;
using DefectTracker.Models;

namespace DefectTracker
{
    public partial class ManageProject : System.Web.UI.Page
    {
        public IEnumerable<Project> Projects = new List<Project>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Projects = ProjectDatasource.GetAll();
            }
        }
    }
}