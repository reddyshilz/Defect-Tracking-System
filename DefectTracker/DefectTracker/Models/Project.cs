﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DefectTracker.Models
{
    /// <summary>
    /// Define project class with properties
    /// </summary>
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
    /// <summary>
    /// User class to hold user details
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
    /// <summary>
    /// Role class to hold Role info
    /// </summary>
    public class Role
    {
        public int RoleId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
    public class ProjectAssignment
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}