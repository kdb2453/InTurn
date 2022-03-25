using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace InTurn_Model
{
    //START EMPLOYEE METADATA
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {
        private sealed class EmployeeMetaData
        {
            [Display(Name = "Employee ID")]
            public int EmployeeID { get; set; }
            [Display(Name = "Student ID")]
            public int StudentID { get; set; }
            [Display(Name = "Faculty ID")]
            public Nullable<int> FacultyID { get; set; }
            [Display(Name = "Midterm Exam Grade")]
            public string MidtermExam { get; set; }
            [Display(Name = "Final Exam Grade")]
            public string FinalExam { get; set; }
        }

    }//END EMPLOYEE METADATA

    //START STUDENT METADATA
    [MetadataType(typeof(StudentMetaData))]
    public partial class Student
    {
        private sealed class StudentMetaData
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
        }
    }//END STUDENT METADATA

    //TESTING ViewModel FOR TeacherHomeController
   /*
    public class ViewModel
    {
        public Employee employee { get; set; }
        public Student student { get; set; }
        public Education education { get; set; }
        public Degree degree { get; set; }
        public Course course { get; set; }
    }
   */


    //Application partial class for Uploading



    public partial class Application
    {

        //For uploading documents like Resume and Transcript
        public HttpPostedFileBase FileName { get; set; }
    }

}
