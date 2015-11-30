using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DefectTracker.Models
{
    /// <summary>
    /// Class to hold defect details
    /// </summary>
    public class Defect
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string DefectType { get; set; }       
        public string Description { get; set; }
        public string StageOfOrigin { get; set; }      
        public string Priority { get; set; }       
        public string Status { get; set; }       
        public string AssignedTo { get; set; }
        public string HowFixed { get; set; }
        public string FixedBy { get; set; }
        public string CreatedBy { get; set; }
        public string ClosedBy { get; set; }        
        public DateTime DateCreated { get; set; }
        public DateTime DateClosed { get; set; }
        public bool IsActive { get; set; }
    }
    /// <summary>
    /// Defect status data
    /// </summary>
    public class Status
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
    /// <summary>
    /// Defect priority info
    /// </summary>
    public class Priority
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
    /// <summary>
    /// Defect stage of origin info
    /// </summary>
    public class StageOfOrigin
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
    /// <summary>
    /// Class to hold defect Type info
    /// </summary>
    public class DefectType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}