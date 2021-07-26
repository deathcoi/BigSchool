using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Heading = "Add Course"
            };
            return View(viewModel);
        }
        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if(!ModelState.IsValid){
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LectureID = User.Identity.GetUserId(),
                dateTime = viewModel.GetDateTime(),
                Categoryid = viewModel.Category,
                Place = viewModel.Place
        };
            _dbContext.Course.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Mine", "Courses");
        }
        public ActionResult AfterCreate(CourseViewModel viewModel)
        {
            return RedirectToAction("Mine", "Courses");
        }
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var course = _dbContext.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecture)
                .Include(l => l.category)
                .ToList();
            var viewModel = new CourseViewModel
            {
                UpcommingCourses = course,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }
        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();

            var followingup = _dbContext.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Follower)
                .Include(a =>a.Followers)
                .Include(a=>a.Followees)
                .ToList();
            var viewModel = new CourseViewModel
            {
                //Followingup = followingup,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Course
                .Where(c => c.LectureID == userId && c.dateTime > DateTime.Now)
                .Include(l => l.Lecture)
                .Include(c => c.category)
                .ToList();
            return View(courses);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course.Single(c => c.Id == id && c.LectureID == userId);

            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Date = course.dateTime.ToString("dd/MM/yyyy"),
                Time = course.dateTime.ToString("HH:mm"),
                Category = course.Categoryid,
                Place = course.Place,
                Heading = "Edit Course",
                Id = course.Id
            };
            return View("Create", viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course.Single(c => c.Id == viewModel.Id && c.LectureID == userId);
            course.Place = viewModel.Place;
            course.dateTime = viewModel.GetDateTime();
            course.Categoryid = viewModel.Category;

            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}