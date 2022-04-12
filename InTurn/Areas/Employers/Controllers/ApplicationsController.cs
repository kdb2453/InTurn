using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InTurn_Model;
using Microsoft.AspNet.Identity;

namespace InTurn.Areas.Employers
{
    [Authorize(Roles ="Admin,Employer")]
    public class ApplicationsController : Controller
    {
        private InTurnEntities db = new InTurnEntities();

        // GET: Employers/Applications
        public ActionResult Index()
        {
            var applications = db.Applications.Include(a => a.Student).Include(a => a.JobPosting);
            return View(applications.ToList());
        }

        // GET: Employers/Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

      


        // GET: Employers/Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", application.StudentID);
            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "Position", application.JobPostingID);
            return View(application);
        }

        // POST: Employers/Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Application application)
        {
            if (ModelState.IsValid)
            {
                var existedApplication = db.Applications.FirstOrDefault(i => i.ApplicationID == application.ApplicationID);
                if (existedApplication != null)
                {
                    existedApplication.AppStatus = application.AppStatus;
                    db.Entry(existedApplication).Property(i => i.AppStatus).IsModified = true;
                    var result = db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
                return View(application);
            }
            return View(application);
        }
        // GET: Employers/Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Employers/Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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

        public ActionResult Onboard (int id)
        {


            var application = db.Applications.
                Include(a=>a.JobPosting.Employer).
                Include(a=>a.Student)
                .FirstOrDefault(a => a.ApplicationID == id);
         

            return View(application);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Onboard([Bind(Include = "ApplicationID,JobPostingID,StudentID")] Application application)
        {


            {
                if (ModelState.IsValid)
                {
                    db.Employees.Add(new Employee()
                    {
                        Student = application.Student,
                        JobPosting = application.JobPosting,
                        Employer = application.JobPosting.Employer

                    });
                    db.SaveChanges();
                    return RedirectToAction("Details", "Employees");
                }
            }

            return View(application);
        }

        //Download//

        public ActionResult Downloads()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("/FileUploads"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*"); List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            return View(items);
        }

        public FileResult Download(string file)
        {

            var FileVirtualPath = "/FileUploads" + file;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }
    }
}
