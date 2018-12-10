using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attempt8.Models
{
    public class AnnouncementViewModels
    {
        public int id { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 40 characters.")]
        public string Text { get; set; }
    }
}