using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InTurn_Model;

namespace InTurn.Areas.Teachers.Controllers
{
    [Authorize(Roles = "Admin, Faculty")]
    public class TeacherHomeController : Controller
    {
        private InTurnEntities db = new InTurnEntities();

        //GET: Teachers/TeacherHome
        public ActionResult Index()
        {
            return View();
        }
    }
}