using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attempt8.Models
{
    public class EditViewModels
    {
        [Required]
        [Display(Name = "Summary")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string summary { get; set; }

        [Required]
        [Display(Name = "Details")]
        [StringLength(140, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string details { get; set; }

        public byte[] image { get; set; }
    }
}