using DeployApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using DeployApplication.DAL;

namespace DeployApplication.ViewModels
{
    public class CartViewModel
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public string sessionId { get; set; }
        public Product product { get; set; }
        public int cartTotal { get; set; }
        public int itemsInCart { get; set; }
        public List<Product> chosenProductsList { get; set; }
        public List<Product> groupedChosenProductsList { get; set; }
        public PagedList.IPagedList<Product> PagedProductsList { get; set; }

    }
 
}