using System;
using System.Collections.Generic;

#nullable disable

namespace Electronics.Models
{
    public partial class Card
    {
        public Card()
        {
            Carts = new HashSet<Cart>();
            Websites = new HashSet<Website>();
        }

        public decimal CardId { get; set; }
        public decimal? CardNumber { get; set; }
        public decimal? Ccv { get; set; }
        public DateTime? Expdate { get; set; }
        public decimal? Balance { get; set; }
        public decimal? UserId { get; set; }

        public virtual UserLogin User { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Website> Websites { get; set; }
    }
}
