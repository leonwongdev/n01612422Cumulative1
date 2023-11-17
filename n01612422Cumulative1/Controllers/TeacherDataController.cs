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
        public IEnumerable<Teacher> ListTeachers(string HireDate, double Salary, string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = SchoolDbContext.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY

            string cmdText = "SELECT * FROM teachers WHERE (LOWER(teacherfname) LIKE LOWER(@key) OR LOWER(teacherlname) LIKE LOWER(@key) OR LOWER(concat(teacherfname, ' ', teacherlname)) LIKE LOWER(@key))";

            if (HireDate != null && HireDate != "")
            {
                // if HireDate and Salary is not specified then do not use it in filter;
                cmdText += "AND hiredate = @hiredate";
                cmd.Parameters.AddWithValue("@hiredate", HireDate);
            }
            if (Salary != -1)
            {
                cmdText += " AND salary=@salary";
                cmd.Parameters.AddWithValue("@salary", Salary);
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

                Teacher NewTeacher = new Teacher(
                            Convert.ToInt32(ResultSet["teacherid"]),
                            ResultSet["teacherfname"].ToString(),
                            ResultSet["teacherlname"].ToString(),
                            ResultSet["employeenumber"].ToString(),
                            ResultSet["hireDate"].ToString(),
                            Convert.ToDouble(ResultSet["salary"])
                    );

                //Add the Author Name to the List
                Teachers.Add(NewTeacher);
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

                    newTeacher = new Teacher(
                            Convert.ToInt32(ResultSet["teacherid"]),
                            ResultSet["teacherfname"].ToString(),
                            ResultSet["teacherlname"].ToString(),
                            ResultSet["employeenumber"].ToString(),
                            ResultSet["hireDate"].ToString(),
                            Convert.ToDouble(ResultSet["salary"])
                        );

                }
                // assign course data to the teacher
                if (ResultSet["classid"] != DBNull.Value)
                {
                    Course newCourse = new Course(
                       Convert.ToInt32(ResultSet["classid"]),
                       Convert.ToInt32(ResultSet["teacherid"]),
                       Convert.ToString(ResultSet["classcode"]),
                       Convert.ToString(ResultSet["startdate"]),
                       Convert.ToString(ResultSet["finishdate"]),
                       Convert.ToString(ResultSet["classname"])
                   );
                    newTeacher.addCourse(newCourse);
                }
               
                
            }


            return newTeacher;
        }

    }
}
