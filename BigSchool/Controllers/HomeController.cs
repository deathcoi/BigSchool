using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BigSchool.ViewModels;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public HomeController()
        {
            dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomming = dbContext.Course.Include(c => c.Lecture).Include(c => c.category).Where(c => c.dateTime > DateTime.Now);
            var viewModel = new ViewModelsss
            {
                UpcommingCourses = upcomming,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(upcomming);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}