﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace n01612422Cumulative1.Models
{
    // This class represents the teachers table.
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

    }
}