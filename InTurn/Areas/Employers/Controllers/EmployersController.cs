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
using InTurn_Model;
using Microsoft.AspNet.Identity;

namespace InTurn.Areas.Employers.Controllers
{
  
    public class EmployersController : Controller
    {
        private InTurnEntities db = new InTurnEntities();

        // GET: Employers/Employers
        [Authorize(Roles = "Admin,Employer")]
        public ActionResult Index()
        {
            return View(db.Employers.ToList());
        }

        // GET: Employers/Employers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }

        [AllowAnonymous]
        // GET: Employers/Employers/Create
        public ActionResult Create()
        {
           
            return View();
        }
        [Authorize(Roles = "Admin,Employer")]
        // POST: Employers/Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployerID,Name,PhoneNum,Address,City,State,ZipCode,Email,FileName,ImageLocation")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                db.Employers.Add(employer);
                if (employer.FileName != null)
                    employer.ImageLocation = UploadImage(employer.FileName);
                db.SaveChanges();
                return RedirectToAction("Index","EmployerHome");
            }

            return View(employer);
        }

        [Authorize(Roles = "Admin,Employer")]
        // GET: Employers/Employers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }

        // POST: Employers/Employers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployerID,Name,PhoneNum,Address,City,State,ZipCode,Email,FileName,ImageLocation")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                  db.Entry(employer).State=EntityState.Modified;
                    if (employer.FileName != null)
                       employer.ImageLocation = UploadImage(employer.FileName);
                   db.SaveChanges();
                   return RedirectToAction("Index","EmployerHome");
            }
            return View(employer);
        }

        // GET: Employers/Employers/Delete/5
        [Authorize(Roles = "Admin,Employer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }
        [Authorize(Roles = "Admin,Employer")]

        // POST: Employers/Employers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employer employer = db.Employers.Find(id);
            db.Employers.Remove(employer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Employer")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       

        #region Images
        private string UploadImage(HttpPostedFileBase file)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
                    var imagePath = ConfigurationManager.AppSettings["EmployerPic"];
                    var mapPath = HttpContext.Server.MapPath(imagePath);
                    string path = Path.Combine(mapPath, Path.GetFileName(file.FileName));
                    string ext = Path.GetExtension(file.FileName);
                    if (allowedExtensions.Contains(ext))
                    {
                        file.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully";
                        return $"{imagePath}/{file.FileName}";
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
