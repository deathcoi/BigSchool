using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using BigSchool.DTOs;

namespace BigSchool.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext dbContext;
        
        public AttendancesController()
        {
            dbContext = new ApplicationDbContext();

        }
        [HttpPost]
        public IHttpActionResult Attend([FromBody] AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId)) //attendanceDto.CourseId))
                return BadRequest("The Attendance already exists !");
            var attendance = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = User.Identity.GetUserId()
            };
            dbContext.Attendances.Add(attendance);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
