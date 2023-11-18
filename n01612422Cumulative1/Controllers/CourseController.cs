using n01612422Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace n01612422Cumulative1.Controllers
{
    public class CourseController : Controller
    {
        CourseDataController courseDataController = new CourseDataController();
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This function returns a razor page that shows a list of course(class).
        /// </summary>
        /// <returns>return a view that use a list of class / course</returns>
        /// GET: Course/List
        public ActionResult List()
        {
            IEnumerable<Course> courses = courseDataController.ListCourses();
            return View(courses);
        }
    }
}