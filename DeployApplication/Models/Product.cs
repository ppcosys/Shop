using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeployApplication.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        [DisplayName("Cena")]
        public int Price { get; set; }
        public string PictureFileName { get; set; }

        public List<Product> Products { get; set; }
    }
}