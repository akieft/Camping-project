using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CampSiteC3.Models
{
    public class AnimatieSchedule
    {
        [Key]
        public Guid Id { get; set; }

        [Required, DisplayName("Activiteit")]
        public string Activity { get; set; }

        [Required, DisplayName("Dag")]
        public string Day { get; set; }

        [Required, DisplayName("Datum")]
        public string Date { get; set; }

        [Required, DisplayName("Uur van")]
        public string HourFrom { get; set; }

        [Required, DisplayName("Uur tot")]
        public string HourTo { get; set; }

        [Required, DisplayName("Locatie")]
        public string Location { get; set; }

        [Required, DisplayName("Leeftijd")]
        public string Age { get; set; }

        [Required,DisplayName("Beschrijving")]
        public string Description { get; set; }

    }
}