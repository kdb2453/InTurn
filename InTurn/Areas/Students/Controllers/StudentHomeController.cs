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
    public class StudentHomeController : Controller
    {
        // GET: Students/StudentHome
        private InTurnEntities db = new InTurnEntities();
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var aspUser = db.AspNetUsers.Find(user);
            var student = db.Students.FirstOrDefault(s => s.Email == aspUser.Email);
            return View(student);
        }
    }
}