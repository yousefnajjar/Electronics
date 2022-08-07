using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Electronics.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public decimal ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? ProductQuntity { get; set; }
        public string IsAvailable { get; set; }
        public string ImagePath { get; set; }
        public decimal? CategoryId { get; set; }

     
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
