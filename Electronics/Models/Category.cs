using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Electronics.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public decimal CategoryId { get; set; }
        public string CategoryName { get; set; }

       
        [NotMapped] 
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
