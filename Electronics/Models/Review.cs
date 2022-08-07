using System;
using System.Collections.Generic;

#nullable disable

namespace Electronics.Models
{
    public partial class Review
    {
        public decimal ReviewId { get; set; }
        public decimal? ReviewValue { get; set; }
        public decimal? WebId { get; set; }

        public virtual Website Web { get; set; }
    }
}
