using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace n01612422Cumulative1.Models
{
    // This class represents the teachers table.
    public class Teacher
    {
        public int id { get; set; }

        public string fname { get; set; }

        public string lname { get; set; }

        public string employeeNum { get; set; }

        public string hireDate
        {
            get;
            set;
        }

        public double salary { get; set; }

        public List<Course> courses { get; set; }

        public Teacher() { }

        /*
         * Comment out this constructor according to cummutive1 comments to remove dependency on SQL logics
        public Teacher(MySqlDataReader ResultSet)
        {
            this.id = Convert.ToInt32(ResultSet["teacherid"]);
            this.fname = ResultSet["teacherfname"].ToString();
            this.lname = ResultSet["teacherlname"].ToString();
            this.employeeNum = ResultSet["employeenumber"].ToString();
            this.hireDate = ResultSet["hireDate"].ToString();
            this.salary = Convert.ToDouble(ResultSet["salary"]);
        }
        */

        public Teacher(int id, string fname, string lname, string employeeNum, string hireDate, double salary)
        {
            this.id = id;
            this.fname = fname;
            this.lname = lname;
            this.employeeNum = employeeNum;
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

        // Server side validation following Blog_poject_7 example
        public bool IsValid()
        {
            bool valid = true;

            if (String.IsNullOrWhiteSpace(this.fname)
                || String.IsNullOrWhiteSpace(this.lname)
                || String.IsNullOrWhiteSpace(this.employeeNum)
                || String.IsNullOrWhiteSpace(this.hireDate)
                || this.salary <= 0
                )
            {
                //Base validation to check if the fields are entered.
                valid = false;
            }
            else
            {
                //Validation for fields to make sure they meet server constraints
                if (this.fname.Length < 2 || this.fname.Length > 255) valid = false;
                if (this.lname.Length < 2 || this.lname.Length > 255) valid = false;
                if (this.employeeNum.Length < 2 || this.employeeNum.Length > 255) valid = false;
                if (this.hireDate.Length < 2 || this.hireDate.Length > 255) valid = false;

            }
            Debug.WriteLine("The Teacher model validity is : " + valid);

            return valid;
        }

        public string getFormattedHireDate()
        {
            // Convert the hire date from string to yyyy-MM-dd format so that it can be use for rendering the default date on the form
            DateTime dateValue = DateTime.Parse(this.hireDate);
            string formattedDate = dateValue.ToString("yyyy-MM-dd");


            return formattedDate;
        }
    }
}