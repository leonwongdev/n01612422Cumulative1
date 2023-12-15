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


        // POST: api/teacherdata/addteacher
        /// <summary>
        /// This function receive a teacher object and use the data to create a new record in database.
        /// </summary>
        /// <param name="NewTeacher">A teacher object created using request body</param>
        /// <example>
        /// POST: api/teacherdata/addteacher
        /// Request body
        /// {
        ///     "fname": "John",
        ///     "lname": "Doe",
        ///     "employeeNum": "T123",
        ///     "hireDate": "2023-01-01",
        ///     "salary": 50
        /// }
        /// </example>
        [HttpPost]
        public string AddTeacher([FromBody] Teacher NewTeacher)
        {
            // Initiative: Use C# Server Side Validation to ensure that there is no missing information when a
            // teacher is added(such as a teacher name)
            // Validation
            // Prevent open db connection when the input data is not valid
            // Check if the first name is empty or null
            if (string.IsNullOrWhiteSpace(NewTeacher.fname))
            {
                return "Error: Please enter a valid First Name.";
            }

            // Check if the last name is empty or null
            if (string.IsNullOrWhiteSpace(NewTeacher.lname))
            {
                return "Error: Please enter a valid Last Name.";
            }

            // Check if the employee number is empty or null
            if (string.IsNullOrWhiteSpace(NewTeacher.employeeNum))
            {
                return "Error: Please enter a valid Employee Number.";
            }

            // Check if the hire date is empty or null
            if (string.IsNullOrWhiteSpace(NewTeacher.hireDate))
            {
                return "Error: Please enter a valid Hire Date.";
            }

            // Check if the salary is greater than zero
            if (NewTeacher.salary <= 0)
            {
                return "Error: Please enter a valid Salary greater than zero.";
            }

            // If validation passes, go on to connect the db and execute query
            MySqlConnection Conn = SchoolDbContext.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "INSERT into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@fname,@lname,@employeeNum, @hireDate, @salary)";
            cmd.Parameters.AddWithValue("@fname", NewTeacher.fname);
            cmd.Parameters.AddWithValue("@lname", NewTeacher.lname);
            cmd.Parameters.AddWithValue("@employeeNum", NewTeacher.employeeNum);
            cmd.Parameters.AddWithValue("@hireDate", NewTeacher.hireDate);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.salary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

            return "Successfully created a new teacher record in database.";
        }

        /// <summary>
        /// Deletes an Teacher from the connected MySQL Database if the ID of that teacher exists. 
        /// Maintain referential integrity by setting the related classes's teacher id to null
        /// So the classes are no longer pointing to a teacher that does not exist.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <example>POST /api/TeaacherData/DeleteTeacher/3</example>
        [HttpPost]
        public string DeleteTeacher(int id)
        {
            if (id < 0)
            {
                return "Error: Failed to delete teacher with invalid id = 0";
            }
            //Create an instance of a connection
            MySqlConnection Conn = SchoolDbContext.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for delete teacher by id.
            MySqlCommand cmdDelete = Conn.CreateCommand();

            //SQL QUERY
            cmdDelete.CommandText = "DELETE FROM Teachers WHERE teacherid=@id";
            cmdDelete.Parameters.AddWithValue("@id", id);
            cmdDelete.Prepare();

            int rowsAffectedByDelete = cmdDelete.ExecuteNonQuery();

            // Establish a new command (query) for setting the teacher id of the classes taught by this teacher to null
            // Initiative: Maintain referential integrity by making sure that any courses in the classes MySQL
            // table are no longer pointing to a teacher which no longer exists.

            if (rowsAffectedByDelete >= 1)
            {
                // Execute update query only when we make sure we succesfully deleted teacher
                MySqlCommand cmdUpdate = Conn.CreateCommand();

                //SQL QUERY
                cmdUpdate.CommandText = "UPDATE classes SET teacherid = null WHERE teacherid = @id";
                cmdUpdate.Parameters.AddWithValue("@id", id);
                cmdUpdate.Prepare();

                cmdUpdate.ExecuteNonQuery();
            }
            else
            {
                return "Failed to delete teacher with id " + id + "Please check your query";
            }


            Conn.Close();

            return "Successfully deleleted teacher with id = " + id;
        }
    }
}