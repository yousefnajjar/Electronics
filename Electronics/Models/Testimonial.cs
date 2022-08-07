using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Electronics.Models
{
    public partial class Testimonial
    {
        public decimal TestimonialsId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string ImagePath { get; set; }
        public string Feedback { get; set; }
        public decimal? WebId { get; set; }


        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual Website Web { get; set; }
    }
}
