using DeployApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace DeployApplication.ViewModels
{
    public class ProductsViewModel
    {
        public Product product { get; set; }
        public List<Product> allProducts { get; set; }
        public int licznikSesji { get; set; }
        public PagedList.IPagedList<Product> PagedProductsList { get; set; }
    }
}