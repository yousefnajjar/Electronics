using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Electronics.Models
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            Cards = new HashSet<Card>();
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public decimal UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string UserEmail { get; set; }
        //[NotMapped]
        //public string Positon { get; set; }
        public string UserPassword { get; set; }
        public string Address { get; set; }
        public string UserImagepath { get; set; }
        public decimal? RoleId { get; set; }
        public decimal Salary { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
