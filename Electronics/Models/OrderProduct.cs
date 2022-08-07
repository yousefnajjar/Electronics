using System;
using System.Collections.Generic;

#nullable disable

namespace Electronics.Models
{
    public partial class OrderProduct
    {
        public decimal OrderProductId { get; set; }
        public decimal? OrderId { get; set; }
        public decimal? ProductId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
