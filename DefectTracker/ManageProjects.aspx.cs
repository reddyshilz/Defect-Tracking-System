using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// This Page List Projects, Add new project , edit project & delete project
/// Developed By Prabasini & Shilpa
/// </summary>
public partial class ManageProjects : System.Web.UI.Page
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