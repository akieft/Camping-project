using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Web.Mvc;
using CampSiteC3.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;


namespace CampSiteC3.Controllers
{
    public class ReservationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string test;

        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        private List<Reservation> SetSolidBooked()
        {
            var listOfDays = new Dictionary<DateTime, bool>();
            for (var i = 1; i <= 12; i++)
            {
                var daysOfMonth = DateTime.DaysInMonth(DateTime.Now.Year, i);
                for (var j = 1; j <= daysOfMonth; j++)
                {
                    var currentDate = new DateTime(DateTime.Now.Year, i, j);

                    if (38 > db.Reservations.Count(p => p.StartDate <= currentDate && p.EndDate >= currentDate))
                    {
                        listOfDays[currentDate] = true;
                    }
                }
            }

            //Parallel.For(1, 13, (i, state) =>
            //{
            //    var daysOfMonth = DateTime.DaysInMonth(DateTime.Now.Year, i);
            //    for (var j = 1; j <= daysOfMonth; j++)
            //    {
            //        var currentDate = new DateTime(DateTime.Now.Year, i, j);

            //        if (38 > db.Reservations.Count(p => p.StartDate <= currentDate && p.EndDate >= currentDate))
            //        {
            //            listOfDays[currentDate] = true;
            //        }
            //    }
            //});

            List<Reservation> SolidBooked = new List<Reservation>();

            foreach (var Day in listOfDays)
            {
                if (Day.Value && Day.Key > DateTime.Today)
                {
                    SolidBooked.Add(new Reservation() { StartDate = Day.Key });
                }
            }

            return SolidBooked;
        }

        public JsonResult GetReservations()
        {
            var data = SetSolidBooked();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Reservation/List
        [Authorize]
        public ActionResult List()
        {
            using (var db = new ApplicationDbContext())
            {
                var uid = User.Identity.GetUserId();
                var reservations = db.Reservations.ToList();

                if (!User.IsInRole("Mod"))
                {
                    reservations = db.Reservations.Where(r => r.UserId == uid).ToList();
                }

                return View(reservations);
            }
        }

        // GET: Reservation/View/{id}
        [Authorize]
        public ActionResult View(int id)
        {
            using (var db = new ApplicationDbContext()) 
                return View(db.Reservations.Find(id));
        }

        // GET: Reservation/Create
        public ActionResult Create(DateTime? date)
            {
            using (var db = new ApplicationDbContext())
            {
                var r = new Reservation();
                ViewBag.MaxDays = Reservation.GetMaxDays(date.HasValue ? date.Value : DateTime.Now);
                if(date.HasValue)
                {
                    $"Date has value: {date.Value}".Dump(); 
                } else
                {
                    $"Date does not have value, setting to {DateTime.Now}".Dump();
                }
                ViewBag.DefaultDays = (ViewBag.MaxDays < Reservation.DEFAULT_DAYS) ? ViewBag.DefaultDays : Reservation.DEFAULT_DAYS;
                return View(r); 
            }
            }

        // POST: Reservation/Create
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,Subject,Description,StartDate,EndDate,UserId,AccomodationId,NumberOfAdults,NumberOfKids,NumberOfAnimals,DescriptionAnimals,isPaid")] Reservation reservation)
        {
            using (var db = new ApplicationDbContext())
            {
                var r = reservation;
                r.EndDate = r.StartDate.AddDays(int.Parse(Request.Params["duration"]));
                r.UserId = db.Users.Where((obj) => obj.Email == User.Identity.Name).First().Id;
                reservation.isPaid = false;
                ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                r = db.Reservations.Add(r);
                await db.SaveChangesAsync();
                var obj = await db.Entry(r).GetDatabaseValuesAsync();
                // TODO: Add logic to determine accomodation id.
                return Redirect($"/Reservation/View/{obj["Id"]}")
                
;
            } else
                return View(reservation);
            }
        }

        // GET: Reservation/Edit/5
        public ActionResult Edit(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var reservation = db.Reservations.Find(id);
                if (reservation.UserId == User.Identity.GetUserId())
                {
                    return View(reservation);
                }
                else
                {
                    return RedirectToAction("List");
                }
            }
        }

        // POST: Reservation/Edit/5
        [HttpPost]
        public ActionResult Edit(Reservation reservation)
        {
            //try
            //{
                using (var db = new ApplicationDbContext())
                {
                    var old = db.Reservations.Where(r => r.Id == reservation.Id).FirstOrDefault();
                    db.Reservations.Remove(old);
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                }

                return RedirectToAction("List");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(int id)
        {

            using (var db = new ApplicationDbContext())
            {
                var reservation = db.Reservations.Find(id);
                if (reservation.UserId == User.Identity.GetUserId())
                {
                    return View(reservation);
                }
                else
                {
                    return RedirectToAction("List");
                }
            }
        }

        // POST: Reservation/Delete/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                using (var db = new ApplicationDbContext())
                {
                    var reservation = db.Reservations.Find(id);
                    db.Reservations.Remove(reservation);
                    db.SaveChanges();
                }

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
    }
}