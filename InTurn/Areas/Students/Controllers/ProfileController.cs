using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Net;
using System.Configuration;
using System.Data.Entity;
using System.Web.Mvc;
using InTurn_Model;

namespace InTurn.Areas.Students.Controllers
{
    [Authorize(Roles = "Admin,Student")]
    public class ProfileController : Controller
    {
        // GET: Students/Profile
        public ActionResult Index()
        {
            return View();
        }

        // GET: Students/Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Students/Profile/Edit
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName, LastName, PhoneNum, Address, City, Sate, ZipCode, Email, Current, Graduate, ImageLocation, FileName")] Student profile)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(profile);
                if (profile.FileName != null)
                    profile.ImageLocation = UploadImage(profile.FileName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FirstName, LastName, PhoneNum, Address, City, Sate, ZipCode, Email, Current, Graduate, ImageLocation, FileName")] Student profile)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(profile);
                if (profile.FileName != null)
                    profile.ImageLocation = UploadImage(profile.FileName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        #region Images
        private string UploadImage(HttpPostedFileBase file)
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