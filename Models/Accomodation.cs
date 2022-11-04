using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampSiteC3.Models 
{
    /// <summary>
    /// Accomodation.
    /// </summary>
    public class Accomodation
    {
        [Key] public int Id { get; set; }

        [Required, DisplayName("Sta-plaats ")]
        public string Number { get; set; }

        public static int BestFitFor(DateTime start)
        {
            return 0;
        }
    }
}