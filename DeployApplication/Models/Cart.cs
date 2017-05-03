using DeployApplication.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeployApplication.Models
{
    public class Cart
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int ProductPrice { get; set; }
        public string SessionId { get; set; }
        public string UserId { get; set; }

    }
}