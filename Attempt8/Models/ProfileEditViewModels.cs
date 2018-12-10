using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attempt8.Models
{
    public class ProfileEditViewModels
    {
    
        public int id { get; set; }
        public byte[] ProfilePicture { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Display(Name = "Relationship Status")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string RelationshipStatus { get; set; }

        [Display(Name = "Designation")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string Designation { get; set; }

        [Display(Name = "Personal Info")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string PersonalInfo { get; set; }

        [Display(Name = "Full Name")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string Name { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }
        public Nullable<int> NumberOfClassesEnrolled { get; set; }
    }
}