using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attempt8.Models
{
    public class ResourceViewModels
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string name { get; set; }
        public int id { get; set; }
    }
}