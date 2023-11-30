using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace n01612422Cumulative1.Models
{
    // This class repersent the `classes` table
    public class Course
    {
       
        public int ClassId { get; set; }
       
        public long TeacherId { get; set; }
       
        public string ClassCode { get; set; }
       
        public string StartDate { get; set; }
       
        public string FinishDate { get; set; }
       
        public string ClassName { get; set; }


        public Course()
        {
            // You can initialize default values here if needed
        }
        public Course(int classId, long teacherId, string classCode, string startDate, string finishDate, string className)
        {
            this.ClassId = classId;
            this.TeacherId = teacherId;
            this.ClassCode = classCode;
            this.StartDate = startDate;
            this.FinishDate = finishDate;
            this.ClassName = className;
        }

        /*
         * Comment out this constructor according to cummutive1 comments to remove dependency on SQL logics
        public Course(MySqlDataReader ResultSet)
        {
            this.ClassId = Convert.ToInt32(ResultSet["classid"]);
            this.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
            this.ClassCode = Convert.ToString(ResultSet["classcode"]);
            this.StartDate = Convert.ToString(ResultSet["startdate"]);
            this.FinishDate = Convert.ToString(ResultSet["finishdate"]);
            this.ClassName = Convert.ToString(ResultSet["classname"]);
        }
        */
    }
}