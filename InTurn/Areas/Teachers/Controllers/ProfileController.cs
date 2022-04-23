using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InTurn_Model;
//FOLLOWING ADDED FOR USERID
using System.Configuration;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace InTurn.Areas.Teachers.Controllers
{
    [Authorize(Roles = "Admin,Faculty")]
    public class ProfileController : Controller
    {
        private InTurnEntities db = new InTurnEntities();

        // GET: Teachers/Profile
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var aspUser = db.AspNetUsers.Find(user);
            var faculty = db.Faculties.FirstOrDefault(f => f.Email == aspUser.Email);
            return View(faculty);
        }

        // GET: Teachers/Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // GET: Teachers/Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacultyID,FirstName,LastName,Email,Location,PhoneNum,FileName,ImageLocation")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Faculties.Add(faculty);
                //PHOTO UPLOADER CODE
                if (faculty.FileName != null)
                    faculty.ImageLocation = UploadImage(faculty.FileName);
                //END PHOTO UPLOADER CODE
                db.SaveChanges();
                return RedirectToAction("Index", "TeacherHome");
            }

            return View(faculty);
        }

        // GET: Teachers/Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Teachers/Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacultyID,FirstName,LastName,Email,Location,PhoneNum,FileName,ImageLocation")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faculty).State = EntityState.Modified;
                //START CODE FOR PHOTO UPLOADER
                if (faculty.FileName != null)
                    faculty.ImageLocation = UploadImage(faculty.FileName);
                //END PHOTO UPLOADER CODE
                db.SaveChanges();
                return RedirectToAction("Index", "TeacherHome");
            }
            return View(faculty);
        }

        // GET: Teachers/Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Teachers/Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faculty faculty = db.Faculties.Find(id);
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

        //PROFILE IMAGE CODE
        private string UploadImage(HttpPostedFileBase file)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var imagePath = ConfigurationManager.AppSettings["FacultyPic"];
                    var mapPath = HttpContext.Server.MapPath(imagePath);
                    string path = Path.Combine(mapPath, Path.GetFileName(file.FileName));
                    string ext = Path.GetExtension(file.FileName);

                    if (allowedExtensions.Contains(ext))
                    {
                        file.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully!";
                        return $"{imagePath}/{file.FileName}";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"ERROR: {ex.Message}";
                }
            }
            return String.Empty;
        }//END PROFILE IMAGE CODE
    }
}
