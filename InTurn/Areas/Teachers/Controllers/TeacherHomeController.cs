using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InTurn_Model;

namespace InTurn.Areas.Teachers.Controllers
{
    [Authorize(Roles = "Admin,Employer")]
    public class TeacherHomeController : Controller
    {
        private InTurnEntities db = new InTurnEntities();

        // GET: Teachers/TeacherHome
        public ActionResult Index()
        {
           ViewBag.EmployeeID = new SelectList(db.Employees.OrderBy(e => e.EmployeeID), "EmployeeID", "Employee ID");
            return View(db.Employees
                .Include("Student.Educations.Degree.Course")
                .ToList());

            //TESTING TABLE LINKING FOR Faculty Homepage TABLE -- NON-FUNCTIONING CURRENTLY//
            /*
            List<Employee> employees = db.Employees.ToList();
            List<Student> students = db.Students.ToList();
            List<Education> educations = db.Educations.ToList();
            List<Degree> degrees = db.Degrees.ToList();
            List<Course> courses = db.Courses.ToList();

            var list = from e in employees
                       join s in students on e.StudentID equals s.StudentID into table1
                       from s in table1.ToList()
                       join edu in educations on s.StudentID equals edu.StudentID into table2
                       from edu in table2.ToList()
                       join d in degrees on edu.DegreeID equals d.DegreeID into table3
                       from d in table3.ToList()
                       join c in courses on d.CourseID equals c.CourseID into table4
                       from c in table4.ToList()
                       select new ViewModel
                       {
                           employee = e,
                           student = s,
                           education = edu,
                           degree = d,
                           course = c
                       };
            return View(list);
            */
        }

        //_IndexByDept ACTION RESULT
        public ActionResult _IndexByDept(string dept)
        {
            //var ids = dept.Split('|'); // SPLITS ids INTO SEPARATE ARRAYS; MIGHT HAVE TO DO THE SAME FOR .js VARIABLES 
            db.Configuration.ProxyCreationEnabled = false;
            var courses = db.Courses
                .Where(c => c.Dept.Equals(dept))
                .ToArray();
            return PartialView("_Results", courses);
        }

        //_IndexByCourseName ACTION RESULT
        public ActionResult _IndexByCourseName(string search)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var courses = db.Courses
                .Where(c => c.Name.Contains(search))
                .ToArray();
            return PartialView("_Results", courses);
        }
    }
}