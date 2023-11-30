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
        public Student(int studentId, string studentFirstName, string studentLastName, string studentNumber, string enrollDate)
        {
            this.StudentId = studentId;
            this.StudentFirstName = studentFirstName;
            this.StudentLastName = studentLastName;
            this.StudentNumber = studentNumber;
            this.EnrollDate = enrollDate;
        }

        /*
         * Comment out this constructor according to cummutive1 comments to remove dependency on SQL logics
        public Student(MySqlDataReader ResultSet)
        {
            this.StudentId = Convert.ToInt32(ResultSet["studentid"]);
            this.StudentFirstName = ResultSet["studentfname"].ToString();
            this.StudentLastName = ResultSet["studentlname"].ToString();
            this.StudentNumber = ResultSet["studentnumber"].ToString();
            this.EnrollDate = ResultSet["enroldate"].ToString();
        }
         */
    }
}