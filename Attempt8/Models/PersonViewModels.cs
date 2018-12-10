using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attempt8.Models
{
    public class PersonViewModels
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only")]
        [Display(Name = "Full Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Your email"), EmailAddress]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required]
        [Display(Name = "Are you a Teacher?")]
        public bool isTeacher { get; set; }
    }

    public class LoginViewModels
    {
        [Required]
        [Display(Name = "Your email"), EmailAddress]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }

    public class AddClassViewModels
    {
        [Required]
        [Display(Name = "Select Class")]
        public string className { get; set; }
    }


}