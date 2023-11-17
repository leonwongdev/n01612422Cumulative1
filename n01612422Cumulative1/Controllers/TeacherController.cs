using n01612422Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace n01612422Cumulative1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }


        //GET : /Teacher/List
        /// <summary>
        /// This controller retrieve a list of teacher data from the database and pass it to view for showing a list of teachers.
        /// </summary>
        /// <param name="SearchKey">For searching in teachers' name</param>
        /// <param name="HireDate">For searching teacher by hire date</param>
        /// <param name="Salary">For searching teacher by salary</param>
        /// <returns>passing a list of teacher data to the view</returns>
        public ActionResult List(string SearchKey = null, string HireDate = null, double Salary = -1)
        {
           TeacherDataController controller = new TeacherDataController();
           IEnumerable<Teacher> teachers = controller.ListTeachers(HireDate, Salary, SearchKey);
            return View(teachers);
        }

        //GET : /Author/Show/{id}
        /// <summary>
        /// This controller retrieve a teacher data by teacher id and pass it to view
        /// </summary>
        /// <param name="id">the id of the teacher</param>
        /// <returns>passing a teacher object to view</returns>
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher newTeacher = controller.FindTeacher(id);


            return View(newTeacher);
        }
    }
}