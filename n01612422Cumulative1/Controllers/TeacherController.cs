using n01612422Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace n01612422Cumulative1.Controllers
{
    public class TeacherController : Controller
    {

        TeacherDataController teacherDataController = new TeacherDataController();

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
        /// <example>
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
        /// </example>
        public ActionResult List(string SearchKey = null, string HiredFrom = null, String HiredTo = null, double MinSalary = -1, double MaxSalary = -1)
        {

            IEnumerable<Teacher> teachers = teacherDataController.ListTeachers(HiredFrom, HiredTo, MinSalary, MaxSalary, SearchKey);
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
            Teacher newTeacher = teacherDataController.FindTeacher(id);


            return View(newTeacher);
        }


        // GET: /Teacher/New
        /// <summary>
        /// This functions serve the /Teacher/New.cshtml.
        /// The view displays a form to get the data of new teacher.
        /// The data will be used to create a new record in database.
        /// </summary>
        /// <returns>Serve the /Teacher/New.cshtml</returns>
        public ActionResult New()
        {
            return View();
        }

        // GET: /Teacher/New_Ajax
        /// <summary>
        /// This functions serve the /Teacher/New_Ajax.cshtml.
        /// The view displays a form to get the data of new teacher.
        /// The data will be used to create a new record in database using AJAX.
        /// </summary>
        /// <returns>Serve the /Teacher/New_Ajax.cshtml</returns>
        public ActionResult New_Ajax()
        {
            return View();
        }

        /// <summary>
        /// This function receive the form data from POST /Teacher/Create
        /// Then pass the data to TeacherDataController for inserting record to database.
        /// </summary>
        /// <param name="NewTeacher">Teacher instance created by binding form data</param>
        /// <returns>Redirect to the List.cshtml so user can see the updated list</returns>
        [HttpPost]
        public ActionResult Create(Teacher NewTeacher)
        {
            teacherDataController.AddTeacher(NewTeacher);
            return RedirectToAction("List");
        }

        // GET: Teacher/deelteconfirm/{id}
        /// <summary>
        /// This function retrieves the teacher data and the related courses by id
        /// Then display the info to user to ask for user's confirmation before delete the teacher in DB.
        /// </summary>
        /// <param name="id">The id of the teacher to be deleted</param>
        /// <returns>Display a page that shows the teacher info and classes</returns>
        public ActionResult DeleteConfirm(int id)
        {

            return View(teacherDataController.FindTeacher(id));
        }

        //POST : /Teacher/Delete/{id}
        /// <summary>
        /// This function deletes teacher by id and set the teacherid of the related courses to null
        /// Then redirect user back to list page.
        /// </summary>
        /// <param name="id">The id of the teacher to be deleted</param>
        /// <returns>Redirect to List page</returns>
        /// <example>POST /Teacher/Delete/3</example>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            teacherDataController.DeleteTeacher(id);
            return RedirectToAction("List");
        }


        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the author and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/5</example>
        public ActionResult Update(int id)
        {
            try
            {
                Teacher SelectedTeacher = teacherDataController.FindTeacher(id);
                return View(SelectedTeacher);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }


        /// <summary>
        /// Receives a POST request containing information about an existing author in the system, with new values. Conveys this information to the API, and redirects to the "Author Show" page of our updated author.
        /// </summary>
        /// <param name="id">Id of the Author to update</param>
        /// <param name="fname">The updated first name of the teacher</param>
        /// <param name="lname">The updated last name of the teacher</param>
        /// <param name="employeeNum">The updated employee of the teacher.</param>
        /// <param name="hireDate">The updated hire date of the teacher.</param>
        /// <param name="salary">The updated salary of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"fname":"Leon",
        ///	"lname":"Wong",
        ///	"employeeNum":"T111",
        ///	"hireDate":"2023-12-15",
        ///	"salary":100
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string fname, string lname, string employeeNum, string hireDate, double salary)
        {
            try
            {
                Teacher teacher = new Teacher(id, fname, lname, employeeNum, hireDate, salary);
                teacherDataController.UpdateTeacher(id, teacher);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }


            return RedirectToAction("Show/" + id);
        }
    }
}