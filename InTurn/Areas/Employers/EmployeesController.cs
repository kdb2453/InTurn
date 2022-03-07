using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InTurn_Model;

namespace InTurn.Areas.Employers
{
    public class EmployeesController : Controller
    {
        private InTurnEntities db = new InTurnEntities();

        // GET: Employers/Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.JobPosting).Include(e => e.Student).Include(e => e.Faculty);
            return View(employees.ToList());
        }

        // GET: Employers/Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employers/Employees/Create
        public ActionResult Create()
        {
            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "Position");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            ViewBag.FacultyID = new SelectList(db.Faculties, "FacultyID", "FirstName");
            return View();
        }

        // POST: Employers/Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,StudentID,FacultyID,JobPostingID,MidtermExam,FinalExam")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "Position", employee.JobPostingID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", employee.StudentID);
            ViewBag.FacultyID = new SelectList(db.Faculties, "FacultyID", "FirstName", employee.FacultyID);
            return View(employee);
        }

        // GET: Employers/Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "Position", employee.JobPostingID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", employee.StudentID);
            ViewBag.FacultyID = new SelectList(db.Faculties, "FacultyID", "FirstName", employee.FacultyID);
            return View(employee);
        }

        // POST: Employers/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,StudentID,FacultyID,JobPostingID,MidtermExam,FinalExam")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "Position", employee.JobPostingID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", employee.StudentID);
            ViewBag.FacultyID = new SelectList(db.Faculties, "FacultyID", "FirstName", employee.FacultyID);
            return View(employee);
        }

        // GET: Employers/Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employers/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
