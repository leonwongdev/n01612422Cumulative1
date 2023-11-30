using MySql.Data.MySqlClient;
using n01612422Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace n01612422Cumulative1.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext SchoolDbContext = new SchoolDbContext();

        //This Controller Will access the teacher table of our teacher database.
        // http://localhost:58519/api/TeacherData/ListTeachers?SearchKey=&HireDate=2016-08-05&Salary=55.3
        /// <summary>
        /// Returns a list of Teachers in the system
        /// This function will also handle searching by name, hire date and salary.
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teacher objects.
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public IEnumerable<Teacher> ListTeachers(string HiredFrom, string HiredTo, double MinSalary, double MaxSalary, string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = SchoolDbContext.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY

            string cmdText = "SELECT * FROM teachers WHERE (LOWER(teacherfname) LIKE LOWER(@key) OR LOWER(teacherlname) LIKE LOWER(@key) OR LOWER(concat(teacherfname, ' ', teacherlname)) LIKE LOWER(@key))";

            // Validation form value before adding filter to SQL query
            if (HiredFrom != null && HiredFrom != "" && HiredTo != null && HiredTo != "")
            {
                // if HireDate and Salary is not specified then do not use it in filter;
                cmdText += "AND (hiredate BETWEEN @HiredFrom AND @HiredTo)";
                cmd.Parameters.AddWithValue("@HiredFrom", HiredFrom);
                cmd.Parameters.AddWithValue("@HiredTo", HiredTo);
            }
            if (MaxSalary != -1 && MinSalary != -1)
            {
                cmdText += " AND (salary BETWEEN @MinSalary AND @MaxSalary)";
                cmd.Parameters.AddWithValue("@MinSalary", MinSalary);
                cmd.Parameters.AddWithValue("@MaxSalary", MaxSalary);
            }
            cmd.CommandText = cmdText;
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Authors
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Add the Author Name to the List
                int id = Convert.ToInt32(ResultSet["teacherid"]);
                string fname = ResultSet["teacherfname"].ToString();
                string lname = ResultSet["teacherlname"].ToString();
                string employeeNum = ResultSet["employeenumber"].ToString();
                string hireDate = ResultSet["hireDate"].ToString();
                double salary = Convert.ToDouble(ResultSet["salary"]);


                Teacher newTeacher = new Teacher(id, fname, lname, employeeNum, hireDate, salary);
                Teachers.Add(newTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return Teachers;
        }

        /// <summary>
        /// Returns an individual teacher from the database by specifying the primary key teacherid
        /// </summary>
        /// <param name="id">the teacher's ID in the database</param>
        /// <returns>An teacher object</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {

            //Create an instance of a connection
            MySqlConnection Conn = SchoolDbContext.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select t.*, c.* from Teachers t LEFT JOIN classes c ON t.teacherid = c.teacherid where t.teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            Teacher newTeacher = null;

            while (ResultSet.Read())
            {
                // assign the teacher info to the teacher object if there is no teacher object created
                if (newTeacher == null)
                {

                    int teacherId = Convert.ToInt32(ResultSet["teacherid"]);
                    string fname = ResultSet["teacherfname"].ToString();
                    string lname = ResultSet["teacherlname"].ToString();
                    string employeeNum = ResultSet["employeenumber"].ToString();
                    string hireDate = ResultSet["hireDate"].ToString();
                    double salary = Convert.ToDouble(ResultSet["salary"]);


                    newTeacher = new Teacher(teacherId, fname, lname, employeeNum, hireDate, salary);
                    //newTeacher = new Teacher(ResultSet);

                }
                // assign course data to the teacher
                if (ResultSet["classid"] != DBNull.Value)
                {
                    int classId = Convert.ToInt32(ResultSet["classid"]);
                    long teacherId = Convert.ToInt64(ResultSet["teacherid"]);
                    string classCode = ResultSet["classcode"].ToString();
                    string startDate = ResultSet["startdate"].ToString();
                    string finishDate = ResultSet["finishdate"].ToString();
                    string className = ResultSet["classname"].ToString();

                    Course newCourse = new Course(classId, teacherId, classCode, startDate, finishDate, className);

                    newTeacher.addCourse(newCourse);
                }


            }


            return newTeacher;
        }

    }
}
