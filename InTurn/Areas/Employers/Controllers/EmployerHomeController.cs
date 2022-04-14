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

namespace InTurn.Areas.Employers.Controllers
{
    [Authorize(Roles = "Admin,Employer")]
    public class EmployerHomeController : Controller
    {
        // GET: Employers/EmployerHome
    private InTurnEntities db = new InTurnEntities();

        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var aspUser = db.AspNetUsers.Find(user);
            var employer = db.Employers.FirstOrDefault(e => e.Email == aspUser.Email);
          
          
            return View(employer);
        }

        public ActionResult AppCount(int count)
        {
            foreach (var app in db.JobPostings.ToList())
            {
                int id = app.JobPostingID;
                count = db.Applications.Where(a => a.JobPostingID == id).Count();
            }
            ViewBag.AppCount = count;
            return View(count);


        }
    }
}