using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Electronics.Models
{
    public partial class AboutU
    {
        public decimal AboutUsId { get; set; }
        public string Info { get; set; }
        public string ImagePath { get; set; }
        public decimal? WebId { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }


        public virtual Website Web { get; set; }
    }
}
