using MySql.Data.MySqlClient;
using n01612422Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace n01612422Cumulative1.Controllers
{
    public class CourseDataController : ApiController
    {
        private SchoolDbContext SchoolDbContext = new SchoolDbContext();

        // GET api/CourseData/ListCourses
        /// <summary>
        /// This function retrieves all the row from the classes table.
        /// </summary>
        /// <returns>A list a course object</returns>
        [HttpGet]
        public List<Course> ListCourses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = SchoolDbContext.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * FROM classes;";
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Course> courses = new List<Course>();

            while (ResultSet.Read())
            {
                int classId = Convert.ToInt32(ResultSet["classid"]);
                long teacherId = ResultSet["teacherid"] is DBNull ? -1 : Convert.ToInt64(ResultSet["teacherid"]);
                string classCode = ResultSet["classcode"].ToString();
                string startDate = ResultSet["startdate"].ToString();
                string finishDate = ResultSet["finishdate"].ToString();
                string className = ResultSet["classname"].ToString();
                Course newCourse = new Course(classId, teacherId, classCode, startDate, finishDate, className);

                courses.Add(newCourse);
            }

            return courses;
        }
    }
}
