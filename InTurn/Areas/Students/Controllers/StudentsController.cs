using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InTurn_Model;
using System.Configuration;
using System.IO;


namespace InTurn.Areas.Students.Controllers
{
    [Authorize(Roles = "Admin, Student, Faculty")]
    public class StudentsController : Controller
    {
        private InTurnEntities db = new InTurnEntities();

        // GET: Students/Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Students/Details/5
        [Authorize(Roles = "Admin, Student, Faculty")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Students/Create
        [Authorize(Roles = "Admin, Student")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Student")]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,PhoneNum,Address,City,State,ZipCode,Email,Current,Graduate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Students/Edit/5
        [Authorize(Roles = "Admin, Student")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,PhoneNum,Address,City,State,ZipCode,Email,Current,Graduate,ImageLocation,FileName")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Students/Delete/5
        [Authorize(Roles = "Admin, Student")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Students/Delete/5
        [Authorize(Roles = "Admin, Student")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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

        public ActionResult _IndexByTag(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var category = db.Majors
                .Include(i => i.MajorID)
                .Where(i => i.MajorID.Equals(id))
                .ToArray();
            return PartialView("Index", category);
        }

        public ActionResult _IndexByName(string parm)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var name = db.Students
                .Include(i => i.LastName)
                .Where(i => i.LastName.Contains(parm))
                .ToArray();
            return PartialView("Index",name);
        }

        #region Images
        private string UploadImage(HttpPostedFile file)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
                    var imagePath = ConfigurationManager.AppSettings["ProfilePic"];
                    var mapPath = HttpContext.Server.MapPath(imagePath);
                    string path = Path.Combine(mapPath, Path.GetFileName(file.FileName));
                    string ext = Path.GetExtension(file.FileName);
                    if (allowedExtensions.Contains(ext))
                    {
                        file.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully";
                        return $"~{imagePath}/{file.FileName}";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"ERROR: { ex.Message}";
                }
            }
            return String.Empty;
        }
        #endregion
    }
}
