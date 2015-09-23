using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// This class holds member variables for project  -Developed by Prabasini & Rekha
/// </summary>
public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }   
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

}
/// <summary>
/// ProjectDatasource contains method to addproject, editproject, delete project & populate project list
/// </summary>
public static class ProjectDatasource
{
    private static readonly Dictionary<int, Project> ProjectData = new Dictionary<int, Project>();
    private static int _idCounter = 1;
    static ProjectDatasource()
    {
        if (ProjectData.Count == 0)
        {
            for (int i = 0; i < 15; i++)
            {
                Project project = new Project()
                {
                    Id = _idCounter++,
                    Name = "Project - " + i.ToString(),
                    Description = "Project Description - " + i.ToString(),
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(60),                   
                    IsActive = true
                };
                ProjectData.Add(project.Id, project);
            }
        }
    }

    public static IEnumerable<Project> GetAll()
    {
        return ProjectData.Values.ToList();
    }


    public static Project Get(int id)
    {
        Project project = null;
        if (ProjectData.ContainsKey(id))
        {
            project = ProjectData[id];
        }
        return project;
    }

    public static void Add(Project project)
    {
        if (ProjectData.ContainsKey(project.Id))
        {
            ProjectData[project.Id] = project;
        }
        else
        {
            project.Id = _idCounter++;
            ProjectData.Add(project.Id, project);
        }
    }

    public static void Delete(int id)
    {
        if (ProjectData.ContainsKey(id))
        {
            ProjectData.Remove(id);
        }
    }

}