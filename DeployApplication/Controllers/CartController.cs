using DeployApplication.Models;
using DeployApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using DeployApplication.DAL;

namespace DeployApplication.Controllers
{
    public class CartController : Controller
    {

        ApplicationDbContext _db = new ApplicationDbContext();

        HttpContext _currentHttpContext = System.Web.HttpContext.Current;

        

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        //wyświetlanie pełnej zawartości koszyka
        public ActionResult ShowCart(int? page)
        {

            var thisSessionId = _currentHttpContext.Session.SessionID;

            //Jeżeli user zalogowany - pobiera Product Id za pomocą userId 
            List<int> cartProductsIdListFromIdentity = new List<int>();
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId().ToString();

                cartProductsIdListFromIdentity = _db.Carts.Where(p => p.UserId ==
                    userId).Select(p => p.ProductId).ToList();
            }


            //Jeżeli user nie jest zalogowany - pobiera Product Id za pomocą sessionId 
            List<int> cartProductsIdListFromSession = _db.Carts.Where(p => p.SessionId ==
                thisSessionId).Select(p => p.ProductId).ToList();

            //Inicjalizacja listy produktów wybranych do koszyka
            List<Product> cartProducts = new List<Product>();

            if (cartProductsIdListFromSession.Count == 0 &&
                cartProductsIdListFromIdentity.Count == 0)
            {
                return View("EmptyCart");
            }
            else if (cartProductsIdListFromSession.Count > 0 &&
                cartProductsIdListFromIdentity.Count == 0)
            {
                foreach (var id in cartProductsIdListFromSession)
                {
                    var chosenProduct =
                        (from p in _db.Products
                         where p.Id == id
                         select p).Single();

                    cartProducts.Add(chosenProduct);
                }
            }
            else if (cartProductsIdListFromIdentity.Count != 0)
            {
                foreach (var id in cartProductsIdListFromIdentity)
                {
                    var chosenProduct =
                        (from p in _db.Products
                         where p.Id == id
                         select p).Single();

                    cartProducts.Add(chosenProduct);
                }
            }


            int CartTotal = cartProducts.Sum(item => item.Price);

            int ItemsInCart = cartProducts.Count;


            //grupowanie produktów wg ProductId
            var groupedCartProducts = cartProducts
                .GroupBy(produkt => produkt.Id)
                .Select(p => new
                {
                    productId = p.Key,
                    price = p.Sum(i => i.Price)
                }).ToList();

            //TESTY
            List<Product> testGroupCartProduct = new List<Product>(); 
            foreach(var p in groupedCartProducts)
            {
                
                Product product = new Product();
                product.Id = p.productId;
                product.Price = p.price;
                testGroupCartProduct.Add(product);
            }

            ViewData["GroupedProducts"] = groupedCartProducts;

            // Model do widoku:
            CartViewModel vm = new CartViewModel()
            {

                chosenProductsList = cartProducts,
                PagedProductsList = cartProducts.ToPagedList(page ?? 1, 3),
                cartTotal = CartTotal,
                itemsInCart = ItemsInCart
            };

            return View("ShowCart", vm);
        }

        //wyświetlanie częściowej zawartości koszyka 
        //dla mini-panelu na stronie głównej
        public ActionResult ShowCartMiniPanel()
        {
            List<int> listaIdProduktow = new List<int>();

            if (User.Identity.IsAuthenticated)
            {
                var userId = _currentHttpContext.User.Identity.GetUserId().ToString();

                listaIdProduktow = _db.Carts.Where(p => p.UserId ==
                userId).Select(p => p.ProductId).ToList();
            }
            else
            {

                var sessionId = _currentHttpContext.Session.SessionID;

                listaIdProduktow = _db.Carts.Where(p => p.SessionId ==
                sessionId).Select(p => p.ProductId).ToList();
            }

            

            List<Product> wybraneProdukty = new List<Product>();

            



            foreach (var item in listaIdProduktow)
            {
                var wybranyProdukt =
                    (from p in _db.Products
                     where p.Id == item
                     select p).Single();

                wybraneProdukty.Add(wybranyProdukt);
            }

            int CartTotal = wybraneProdukty.Sum(item => item.Price);

            int ItemsInCart = wybraneProdukty.Count;

            // Model do widoku:
            CartViewModel vm = new CartViewModel()
            {
                chosenProductsList = wybraneProdukty,               
                cartTotal = CartTotal,
                itemsInCart = ItemsInCart

            };

            return PartialView("_ShowCartMiniPanel", vm);
        }

        
    }
}