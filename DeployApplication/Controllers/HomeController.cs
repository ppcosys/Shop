using DeployApplication.Models;
using DeployApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using DeployApplication.DAL;

namespace DeployApplication.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext _db = new ApplicationDbContext();
        HttpContext _currentHttpContext = System.Web.HttpContext.Current;
        

        public ActionResult Index(int? id)
        {
            

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}