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
    [Authorize(Roles = "Admin, Faculty")]
    public class StudentsController : Controller
    {
        private InTurnEntities db = new InTurnEntities();

        //TRANSFERRED FROM TeacherHomeController.cs
        // GET: Teachers/Students
        public ActionResult Index()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees.OrderBy(e => e.EmployeeID), "EmployeeID", "Employee ID");
            return View(db.Employees
             .ToList());
        }

        //_IndexByDept ACTION RESULT
        public ActionResult _IndexByDept(string dept)
        {
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
