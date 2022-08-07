using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Electronics.Models
{
    public partial class Website
    {
        public Website()
        {
            AboutUs = new HashSet<AboutU>();
            ContactUs = new HashSet<ContactU>();
            Reviews = new HashSet<Review>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal WebId { get; set; }
        public string WebName { get; set; }
        public string WebImagelogoPath { get; set; }
        public string WebImagePackgroundPath { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal? CardId { get; set; }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        [NotMapped]
        public IFormFile ImageLogo { get; set; }

        [NotMapped]
        public IFormFile ImageBackground { get; set; }

        public virtual Card Card { get; set; }
        public virtual ICollection<AboutU> AboutUs { get; set; }
        public virtual ICollection<ContactU> ContactUs { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
