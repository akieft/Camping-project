using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CampSiteC3.Models
{
    public class ActivitiesSchedule : IEnumerable
    {
        [Key]
        public Guid Id { get; set; }

        [Required, DisplayName("Activiteit") ]
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

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}