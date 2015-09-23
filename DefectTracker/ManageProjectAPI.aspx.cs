using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// ManageProjectAPI contains all webmethos to Manage project(GetProjectDetails, Save project, delete project)
/// Developed By Prabasini and Shilpa
/// </summary>
public partial class ManageProjectAPI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static Project GetProjectById(int projectId)
    {
        var project = ProjectDatasource.Get(projectId);
        return project;
    }


    [WebMethod]
    public static void SaveProject(object project)
    {
        var dbProject = GetProject(project);
        ProjectDatasource.Add(dbProject);
    }

    [WebMethod]
    public static void DeleteProject(int projectId)
    {
        ProjectDatasource.Delete(projectId);
    }

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

        return objProject;
    }
}