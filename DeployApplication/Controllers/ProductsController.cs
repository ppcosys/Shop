using DeployApplication.Models;
using DeployApplication.ViewModels;
using PagedList;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DeployApplication.DAL;

namespace DeployApplication.Controllers
{
    public class ProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        System.Web.HttpContext _currentHttpContext = System.Web.HttpContext.Current;


        // GET: Products
        public ActionResult Index()
        {
            var produkty = db.Products.ToList<Product>();
            ProductsViewModel vm = new ProductsViewModel();
            vm.allProducts = produkty;
            
            return View(vm);
        }
       
        public ActionResult Add(Product product, HttpPostedFileBase uploadFile)
        {
            Product addedProduct = new Product();

            try
            {
                if (uploadFile == null)
                {
                    TempData["Message"] = "Nie wybrano obrazka produktu!";

                    return RedirectToAction("Index", "Administrator");
                }
                else if (uploadFile.ContentLength > 0 || uploadFile != null)
                {
                    var fileName = Path.GetFileName(uploadFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Resources/Images"), fileName);
                    uploadFile.SaveAs(path);

                    string displayPath = "~/Resources/Images/" + fileName;

                    addedProduct.PictureFileName = displayPath;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            addedProduct.Name = product.Name;
            addedProduct.Description = product.Description;
            addedProduct.Price = product.Price;            

            db.Products.Add(addedProduct);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddProductPicture(Product product, HttpPostedFileBase uploadFile)
        {
            if (uploadFile == null)
            {
                TempData["Message"] = "Nie wybrano obrazka produktu!";

                return RedirectToAction("EditProduct", "Products", new { productId = product.Id });
            }
            else if(uploadFile.ContentLength > 0 || uploadFile != null)
            {
                var fileName = Path.GetFileName(uploadFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Resources/Images"), fileName);
                uploadFile.SaveAs(path);

                string displayPath = "~/Resources/Images/" + fileName;

                var chosenProduct = db.Products.FirstOrDefault(p => p.Id == product.Id);
                chosenProduct.PictureFileName = displayPath;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddToDbCart(int productId)
        {
            var selectedProduct = db.Products.SingleOrDefault(p => p.Id == productId);
            var sessionId = _currentHttpContext.Session.SessionID;
            string userId = null;

            if (User.Identity.IsAuthenticated)
            {
                userId = User.Identity.GetUserId().ToString();                
            }

            Cart record = new Cart();
            record.ProductId = selectedProduct.Id;
            record.SessionId = sessionId;
            record.ProductPrice = selectedProduct.Price;
            record.UserId = userId;
            
            db.Carts.Add(record);
            db.SaveChanges();

            return RedirectToAction("Index","Home");
        }

        [ChildActionOnly]
        public ActionResult ShowProducts(int? page)
        {
            ProductsViewModel vm = new ProductsViewModel();
            try
            {
                var produkty = db.Products.ToList<Product>();
                
                vm.allProducts = produkty;
                vm.PagedProductsList = produkty.ToPagedList(page ?? 1, 9);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return PartialView("_ShowProducts", vm);
        }

        
        [HttpGet]
        public ActionResult EditProduct(int productId)
        {
            var product = db.Products.Single(p => p.Id == productId);

            ViewData["Alert"] = "test";

            return View("EditProduct", product);
        }

        
        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase uploadFile)
        {
            var changedProduct = db.Products.Single(p => p.Id == product.Id);

           if (uploadFile.ContentLength > 0 || uploadFile != null)
                {
                    var fileName = Path.GetFileName(uploadFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Resources/Images"), fileName);
                    uploadFile.SaveAs(path);

                    string displayPath = "~/Resources/Images/" + fileName;

                    changedProduct.PictureFileName = displayPath;
                }
            
            changedProduct.Name = product.Name;
            changedProduct.Description = product.Description;
            changedProduct.Price = product.Price;
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        public ActionResult RemoveProductFromDb(int productId)
        {
            var productToRemove = db.Products.SingleOrDefault(p => p.Id == productId);
            db.Products.Remove(productToRemove);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult RemoveProductFromCart(int? productId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = _currentHttpContext.User.Identity.GetUserId().ToString();

                    var productToRemove = db.Carts.First(p => p.UserId == userId && p.ProductId == productId);

                    db.Carts.Remove(productToRemove);
                    db.SaveChanges();

                }
                else
                {
                    var sessionId = _currentHttpContext.Session.SessionID.ToString();
                    var productToRemove = db.Carts.First(p => p.SessionId== sessionId && p.ProductId == productId);

                    db.Carts.Remove(productToRemove);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                return Content("Wystąpił nieoczekiwany błąd");
            }
                
            return RedirectToAction("ShowCart", "Cart");
        }

    }
}