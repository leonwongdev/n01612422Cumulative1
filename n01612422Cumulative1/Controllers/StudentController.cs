using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace n01612422Cumulative1.Controllers
{
    public class StudentController : Controller
    {
        StudentDataController studentDataController = new StudentDataController();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This function gets a list of student data from students table and generates a page to list it.
        /// </summary>
        /// <returns></returns>
        /// GET: Student/List
        public ActionResult List() { 
            
            return View(studentDataController.ListStudents());
        }
    }
}