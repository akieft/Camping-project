using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CampSiteC3.Models
{
    public class ForumModels
    {
        [Key]
        public int Campsite { get; set; }

        [Required(ErrorMessage = "Enter the camping place.")]
        [Range(1, 36, ErrorMessage = "Choose a existing camping site, from 1 to 36")]
        [Display(Name = "Camping staan plaats")]
        public string Camping { get; set; }

        [Required(ErrorMessage = "Enter the review.")]
        [DataType(DataType.MultilineText)]
        public string Review { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum")]
        [Required(ErrorMessage = "Enter the issued date.")]
        public DateTime IssueDate { get; set; }
    }
}
