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

        // GET: Teachers/Students
        public ActionResult Index()
        {
            ViewBag.JobPostingID = new SelectList(db.JobPostings.OrderBy(jp => jp.JobPostingID), "JobPostingID", "Position");

            var employees = db.Employees
                .Include(e => e.JobPosting)
                .Include(e => e.Student)
                .Include(e => e.Faculty);

            return View(employees.ToList());
        }

        //_IndexByPositionName ACTION RESULT
        public ActionResult _IndexByPositionName(string search)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var positions = db.Employees
                .Include(e => e.Student)
                .Include(e => e.Faculty)
                .Include(e => e.JobPosting)
                .Where(e => e.JobPosting.Position.Contains(search))
                .ToArray();
            return PartialView("_Results", positions);
        }

        //_IndexByFinalGrade ACTION RESULT
        public ActionResult _IndexByFinalGrade(int id)
        {
            db.Configuration.ProxyCreationEnabled = false; //NO EXPLANATION, JUST HELPS PREVENT ERRORS
            var employees = db.Employees
                //.Include(c => c.Department)//RETURNS Courses AND Department USING .Include()
                //.Where(c => c.Department.DepartmentId.Equals(id))//RETURNS Courses WHERE THE DepartmentId IS THE SAME AS THE id THAT WAS PASSED TO THIS FUNCTION USING .Equals()
                .Where(e => e.FinalExam.Equals(id))
                .ToArray();
            return PartialView("_Index", employees);//RETURN THE _Index PARTIAL VIEW AND courses
        }

    }//END PUBLIC CLASS
}
