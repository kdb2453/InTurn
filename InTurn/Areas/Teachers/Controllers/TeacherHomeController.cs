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
    [Authorize(Roles = "Admin, Faculty")]
    public class TeacherHomeController : Controller
    {
        private InTurnEntities db = new InTurnEntities();

        //GET: Teachers/TeacherHome
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var aspUser = db.AspNetUsers.Find(user);
            var faculty = db.Faculties.FirstOrDefault(f => f.Email == aspUser.Email);
            return View(faculty);
        }
    }
}