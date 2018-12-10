using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Attempt8.Models
{
    public class PostViewModels
    {
        public int id { get; set; }

        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Summary")]
        [StringLength(10, ErrorMessage = "Cannot be longer than 10 characters.")]
        public string Summary { get; set; }

        [Display(Name = "Details")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string details { get; set; }
        public byte[] image { get; set; }
    }
}