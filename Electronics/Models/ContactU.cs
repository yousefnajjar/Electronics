using System;
using System.Collections.Generic;

#nullable disable

namespace Electronics.Models
{
    public partial class ContactU
    {
        public decimal ContactUsId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public decimal? WebId { get; set; }

        public virtual Website Web { get; set; }
    }
}
