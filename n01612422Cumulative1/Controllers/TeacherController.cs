﻿using n01612422Cumulative1.Models;
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
    }
}