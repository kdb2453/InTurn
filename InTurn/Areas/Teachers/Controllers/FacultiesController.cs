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
    public class FacultiesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext(); -- UNSURE IF THIS SHOULD BE USED INSTEAD OF InTurnEntities db IN THE LINE BELOW; CHOSE THIS AS THE Data Context Class SINCE THE OTHER OPTION WAS RETURNING AN ERROR/WOULDN'T BUILD
        private InTurnEntities db = new InTurnEntities();

        // GET: Faculty/Faculties
        public ActionResult Index()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees.OrderBy(e => e.EmployeeID), "EmployeeID", "Employee Name");
            return View(db.Employees.ToList());
            //return View(db.Faculties.ToList());
        }

        //_IndexByTag ACTION RESULT
        public ActionResult _IndexByTag(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var results = db.Employees
                .Include(r => r.Student)
                .Where(r => r.Student.StudentID.Equals(id))
                .ToArray();
            return PartialView("_Results", results);
        }

        // GET: Faculty/Faculties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InTurn_Model.Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // GET: Faculty/Faculties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Faculty/Faculties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacultyID,FirstName,LastName,Email,Location,PhoneNum")] InTurn_Model.Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Faculties.Add(faculty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faculty);
        }

        // GET: Faculty/Faculties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InTurn_Model.Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Faculty/Faculties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacultyID,FirstName,LastName,Email,Location,PhoneNum")] InTurn_Model.Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faculty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faculty);
        }

        // GET: Faculty/Faculties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InTurn_Model.Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Faculty/Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InTurn_Model.Faculty faculty = db.Faculties.Find(id);
            db.Faculties.Remove(faculty);
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
