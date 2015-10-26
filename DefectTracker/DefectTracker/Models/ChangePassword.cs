using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DefectTracker.Models
{
    public class ChangePassword
    {
        public string RegisteredUserName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

   
}