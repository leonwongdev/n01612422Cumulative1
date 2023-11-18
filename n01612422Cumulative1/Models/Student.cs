using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace n01612422Cumulative1.Models
{
    // This class represents the students table
    public class Student
    {
        public int StudentId { get; set; }

        public string StudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public string StudentNumber { get; set; }

        public string EnrollDate { get; set; }

        public Student()
        {

        }

        public Student(MySqlDataReader ResultSet)
        {
            this.StudentId = Convert.ToInt32(ResultSet["studentid"]);
            this.StudentFirstName = ResultSet["studentfname"].ToString();
            this.StudentLastName = ResultSet["studentlname"].ToString();
            this.StudentNumber = ResultSet["studentnumber"].ToString();
            this.EnrollDate = ResultSet["enroldate"].ToString();
        }
    }
}