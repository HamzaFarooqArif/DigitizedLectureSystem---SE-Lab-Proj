using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attempt8.Models
{
    public class ClassViewModels
    {
        [Required]
        [Display(Name = "Class Name")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string name { get; set; }

        [Required]
        [Display(Name = "School Name")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string schoolName { get; set; }

        [Required]
        [Display(Name = "Access Code")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string classCode { get; set; }
    }

    public class ViewClassViewModels
    {
        [Required]
        [Display(Name = "School Name")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string schoolName { get; set; }

        [Required]
        [Display(Name = "Class Name")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string className { get; set; }
        [Required]
        [Display(Name = "Access Code")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string classCode { get; set; }
    }
}