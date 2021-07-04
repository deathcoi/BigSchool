using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BigSchool.Models
{
    public class Course
    {
        public int Id { get; set; }
        public ApplicationUser Lecture { get; set; }
        [Required]
        public string LectureID { get; set; }
        [Required]
        [StringLength(255)]
        public string Place { get; set; }
        public DateTime dateTime { get; set; }
        public Category category { get; set; }
        public byte Categoryid {get;set;}

    }
}