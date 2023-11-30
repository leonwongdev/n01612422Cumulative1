using MySql.Data.MySqlClient;
using n01612422Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01612422Cumulative1.Controllers
{
    public class StudentDataController : ApiController
    {
        private SchoolDbContext SchoolDbContext = new SchoolDbContext();

        // GET api/StudentData/ListStudents
        /// <summary>
        /// This class retrieves all the row from students table.
        /// </summary>
        /// <returns>A list of student object</returns>
        [HttpGet]
        public List<Student> ListStudents()
        {
            //Create an instance of a connection
            MySqlConnection Conn = SchoolDbContext.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * FROM students;";
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Student> students = new List<Student>();

            while (ResultSet.Read())
            {
                int studentId = Convert.ToInt32(ResultSet["studentid"]);
                string studentFirstName = ResultSet["studentfname"].ToString();
                string studentLastName = ResultSet["studentlname"].ToString();
                string studentNumber = ResultSet["studentnumber"].ToString();
                string enrollDate = ResultSet["enroldate"].ToString();

                // Now, create a new Student object using the constructor that takes primitive data types
                Student newStudent = new Student(studentId, studentFirstName, studentLastName, studentNumber, enrollDate);
                students.Add(newStudent);
            }

            return students;
        }
    }
}
