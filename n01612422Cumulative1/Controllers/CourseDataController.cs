using MySql.Data.MySqlClient;
using n01612422Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace n01612422Cumulative1.Controllers
{
    public class CourseDataController : ApiController
    {
        private SchoolDbContext SchoolDbContext = new SchoolDbContext();

        // GET api/CourseData/ListCourses
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
                Course newCourse = new Course(
                                        Convert.ToInt32(ResultSet["classid"]),
                                        Convert.ToInt32(ResultSet["teacherid"]),
                                        Convert.ToString(ResultSet["classcode"]),
                                        Convert.ToString(ResultSet["startdate"]),
                                        Convert.ToString(ResultSet["finishdate"]),
                                        Convert.ToString(ResultSet["classname"])
                                    );
                courses.Add(newCourse);
            }

            return courses;
        }
    }
}
