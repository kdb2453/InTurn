using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InTurn.Areas.Employers.Controllers
{
    [Authorize(Roles = "Admin,Employer")]
    public class EmployerHomeController : Controller
    {
        // GET: Employers/EmployerHome
      
        
        public ActionResult Index()
        {
            return View();
        }
    }
}