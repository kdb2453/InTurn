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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
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
        public ActionResult Apply(int id)

        {
            var user= User.Identity.GetUserId();
            var aspUser = db.AspNetUsers.Find(user);
            var contact = db.Students.FirstOrDefault(s => s.Email == aspUser.Email);
            var jobPosting = db.JobPostings.Find(id);
            var employer = db.Employers.Find(id);
            ViewBag.Student = contact;
            ViewBag.JobPosting = jobPosting;
            ViewBag.Employer= employer;
           
            
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
                    return RedirectToAction("Index","Applications");
                }
            }
            

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", application.StudentID);
            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "Position", application.JobPostingID);
            return View(application);
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
