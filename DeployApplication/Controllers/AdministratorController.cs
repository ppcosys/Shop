using DeployApplication.DAL;
using DeployApplication.Models;
using DeployApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeployApplication.Controllers
{
    
    public class AdministratorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        System.Web.HttpContext currentHttpContext = System.Web.HttpContext.Current;


        
        public ActionResult Index()
        {
            var produkty = db.Products.ToList<Product>();
            ProductsViewModel vm = new ProductsViewModel();
            vm.allProducts = produkty;

            return View(vm);
        }



    }
}