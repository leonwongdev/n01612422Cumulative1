using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace n01612422Cumulative1.Models
{
    public class Teacher
    {
        public int id { get; set; }

        public string fname { get; set; }

        public string lname { get; set; }

        public string employeeNum { get; set; }

        public string hireDate { get; set; }

        public double salary { get; set; }

        public List<Course> courses { get; set; }

        public Teacher() { }

        public Teacher(int id, string firstName, string lastName, string employeeNumber, string hireDate, double salary)
        {
            this.id = id;
            this.fname = firstName;
            this.lname = lastName;
            this.employeeNum = employeeNumber;
            this.hireDate = hireDate;
            this.salary = salary;
        }

        // This helper function add a course to the teacher object
        // It initialize a empty course list if it has not been initialized before
        public void addCourse(Course course)
        {
            if (this.courses == null)
            {
                this.courses = new List<Course>();
            }
            this.courses.Add(course);
        }

    }
}