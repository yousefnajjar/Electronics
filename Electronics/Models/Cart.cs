using System;
using System.Collections.Generic;

#nullable disable

namespace Electronics.Models
{
    public partial class Cart
    {
        public decimal CartId { get; set; }
        public decimal? UserId { get; set; }
        public decimal? OrderId { get; set; }
        public decimal? CardId { get; set; }

        public virtual Card Card { get; set; }
        public virtual Order Order { get; set; }
        public virtual UserLogin User { get; set; }
    }
}
