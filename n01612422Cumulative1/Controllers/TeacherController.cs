using n01612422Cumulative1.Models;
using System;
using System.Collections.Generic;
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
        /// Sample 1: GET: Teacher/List
        /// This request teacher returns all teachers in the DB.
        /// 
        /// Sample 2: GET: /Teacher/List?SearchKey=l&HiredFrom=2014-01-01&HiredTo=2015-12-30&MinSalary=&MaxSalary=
        /// This request returns teachers whose name contains "l" (SearchKey=l), 
        /// and teacher whose hireddate is between 2014-01-01 and 2015-12-30. 
        /// It will not filter by salary as both MinSalary and MaxSalary is empty.
        /// 
        /// Sample 3: GET: /Teacher/List?SearchKey=l&HiredFrom=2014-01-01&HiredTo=2015-12-30&MinSalary=60&MaxSalary=70
        /// This request returns teachers whose name contains "l" (SearchKey=l), 
        /// and teacher whose hireddate is between 2014-01-01 and 2015-12-30
        /// and teacher whose salary is within the range of 60 and 70.
        /// </summary>
        /// <param name="SearchKey">For searching in teachers' name</param>
        /// <param name="HireDate">For searching teacher by hire date</param>
        /// <param name="Salary">For searching teacher by salary</param>
        /// <returns>passing a list of teacher data to the view</returns>
        public ActionResult List(string SearchKey = null, string HiredFrom = null, String HiredTo = null, double MinSalary = -1, double MaxSalary = -1)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> teachers = controller.ListTeachers(HiredFrom, HiredTo, MinSalary, MaxSalary, SearchKey);
            return View(teachers);
        }

        //GET : /Teacher/Show/{id}
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