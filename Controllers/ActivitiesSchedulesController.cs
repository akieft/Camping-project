using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CampSiteC3.Models;

namespace CampSiteC3.Controllers
{
    public class ActivitiesSchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminActivities
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AdminActivities()
        {
            return View(await db.ActivitiesSchedules.ToListAsync());
        }
        // GET: ActivitiesSchedules
        public async Task<ActionResult> Index()
        {
            return View(await db.ActivitiesSchedules.ToListAsync());
        }

        // GET: ActivitiesSchedules/Details/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitiesSchedule activitiesSchedule = await db.ActivitiesSchedules.FindAsync(id);
            if (activitiesSchedule == null)
            {
                return HttpNotFound();
            }
            return View(activitiesSchedule);
        }

        // GET: ActivitiesSchedules/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivitiesSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Activity,Day,Date,HourFrom,HourTo,Location")] ActivitiesSchedule activitiesSchedule)
        {
            if (ModelState.IsValid)
            {
                activitiesSchedule.Id = Guid.NewGuid();
                db.ActivitiesSchedules.Add(activitiesSchedule);
                await db.SaveChangesAsync();
                return RedirectToAction("AdminActivities");
            }

            return View(activitiesSchedule);
        }

        // GET: ActivitiesSchedules/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitiesSchedule activitiesSchedule = await db.ActivitiesSchedules.FindAsync(id);
            if (activitiesSchedule == null)
            {
                return HttpNotFound();
            }
            return View(activitiesSchedule);
        }

        // POST: ActivitiesSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Activity,Day,Date,HourFrom,HourTo,Location")] ActivitiesSchedule activitiesSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activitiesSchedule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("AdminActivities");
            }
            return View(activitiesSchedule);
        }

        // GET: ActivitiesSchedules/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitiesSchedule activitiesSchedule = await db.ActivitiesSchedules.FindAsync(id);
            if (activitiesSchedule == null)
            {
                return HttpNotFound();
            }
            return View(activitiesSchedule);
        }

        // POST: ActivitiesSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            ActivitiesSchedule activitiesSchedule = await db.ActivitiesSchedules.FindAsync(id);
            db.ActivitiesSchedules.Remove(activitiesSchedule);
            await db.SaveChangesAsync();
            return RedirectToAction("AdminActivities");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
