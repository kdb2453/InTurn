using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using InTurn.Data;
using InTurn_Model;

namespace InTurn.Areas.Students.Controllers
{
    [Authorize(Roles = "Admin,Student")]
    public class JobPostingsController : Controller
    {
        private InTurnEntities db = new InTurnEntities();

        // GET: Students/JobPostings
        public ActionResult Index()
        {
            var jobPostings = db.JobPostings.Include(j => j.Employer);
            return View(jobPostings.ToList());
        }

        // GET: Students/JobPostings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPosting jobPosting = db.JobPostings.Find(id);
            if (jobPosting == null)
            {
                return HttpNotFound();
            }
            return View(jobPosting);
        }

        // GET: Students/JobPostings/Apply
        public ActionResult Apply()

        {
            
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "Position");
           
            
            return View();
        }

        // POST: Employers/Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply([Bind(Include = "ApplicationID,StudentID,JobPostingID,Resume,AppStatus,FileName")] Application application)
        {

           
            {
                if (ModelState.IsValid)
                {
                    db.Applications.Add(application);
                    application.AppStatus = (AppStatus?)1;
                    if (application.FileName != null)
                        application.Resume = UploadFile(application.FileName);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", application.StudentID);
            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "Position", application.JobPostingID);
            return View(application);
        }

            // GET: Students/JobPostings/Edit/5
            public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPosting jobPosting = db.JobPostings.Find(id);
            if (jobPosting == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployerID = new SelectList(db.Employers, "EmployerID", "Name", jobPosting.EmployerID);
            return View(jobPosting);
        }

        // POST: Students/JobPostings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobPostingID,EmployerID,Position,Desc,Wage,Location,JobType,TimeType,Days,Hours")] JobPosting jobPosting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobPosting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployerID = new SelectList(db.Employers, "EmployerID", "Name", jobPosting.EmployerID);
            return View(jobPosting);
        }

        // GET: Students/JobPostings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPosting jobPosting = db.JobPostings.Find(id);
            if (jobPosting == null)
            {
                return HttpNotFound();
            }
            return View(jobPosting);
        }

        // POST: Students/JobPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobPosting jobPosting = db.JobPostings.Find(id);
            db.JobPostings.Remove(jobPosting);
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


        #region Files


        // Method for uploading Resume and Transcript
        private string UploadFile(HttpPostedFileBase file)
        {

            if (Request.Files.Count > 0)
                try
                {
                    var allowedExtensions = new[] { ".pdf", ".docx" };
                    var filePath = ConfigurationManager.AppSettings["ApplicationFile"];
                    var mapPath = HttpContext.Server.MapPath(filePath);
                    string path = Path.Combine(mapPath, Path.GetFileName(file.FileName));
                    var ext = Path.GetExtension(file.FileName);
                    if (allowedExtensions.Contains(ext))
                    {
                        file.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully";
                        return $"{filePath}/{file.FileName}";
                    }

                    else
                    {
                        ViewBag.Message = "Please use either a PDF or Word Document";
                    }
                }


                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR" + ex.Message.ToString();
                }
            return String.Empty;
        }




        #endregion

    }
}
