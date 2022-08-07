using System;
using System.Collections.Generic;

#nullable disable

namespace Electronics.Models
{
    public partial class Order
    {
        public Order()
        {
            Carts = new HashSet<Cart>();
            OrderProducts = new HashSet<OrderProduct>();
        }

        public decimal OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Quntity { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Status { get; set; }
        public decimal? UserId { get; set; }

        public virtual UserLogin User { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
