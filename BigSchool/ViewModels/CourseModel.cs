using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigSchool.ViewModels
{
    public class CourseModel
    {
        public IEnumerable<Following> UpcommingCourses { get; set; }
        public bool ShowAction { get; set; }
    }
}