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
    public class StudentsController : Controller
    {
        private InTurnEntities db = new InTurnEntities();

        // GET: Teachers/Students
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.JobPosting).Include(e => e.Student).Include(e => e.Faculty);
            return View(employees.ToList());
        }
    }//END PUBLIC CLASS
}
