using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace CampSiteC3.Models
{
    public class Reservation
    {
        public const int MAX_DAYS = 31;
        public const int DEFAULT_DAYS = 14;

        [Key] public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Kies je vertrekdag.")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Kies je einddag.")]
        public DateTime EndDate { get; set; }


        [Required] public string UserId { get; set; }

        [Display(Name = "Aantal volwassenen")]
        [Required(ErrorMessage = "Minimaal 1 volwassene benodigd voor reserveren."), DefaultValue(1)]
        public int NumberOfAdults { get; set; }

        [Display(Name = "Aantal kinderen")]
        [DefaultValue(0)]
        public int NumberOfKids { get; set; }

        [Display(Name = "Accomodatie nummer")]
        [Required] public int Accomodation_id { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Aantal dieren")]
        public int NumberOfAnimals { get; set; }

        [Display(Name = "Beschrijving dieren")]
        public string DescriptionAnimals { get; set; }

        [Display(Name = "Betaling")]
        [DefaultValue(false)]
        [Required] public bool isPaid { get; set; }

        public static int GetMaxDays(DateTime start)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var max = start.AddDays(31);
                var futureReservations = db.Reservations.Where((obj) => obj.StartDate > start && obj.EndDate < max)
                    .Select((obj) => obj.StartDate).ToList();
                if(futureReservations.Count == db.Accomodations.Count())
                {
                     return (int)(futureReservations.Max() - start).TotalDays;
                }
                else
                {
                    return MAX_DAYS;
                }
            }
        }
        public string Description()
        {
            return "Reservering door " + UserId + ", op de plek: " + Accomodation_id;
        }

        public int Duration()
        {
            return (EndDate - StartDate).Days;
        }

    }
}