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
    
    public class AnimatieSchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: AdminAnimatie
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AdminAnimatie()
        {
            return View(await db.AnimatieSchedules.ToListAsync());
        }

        // GET: AnimatieSchedules
        public async Task<ActionResult> Index()
        {
            return View(await db.AnimatieSchedules.ToListAsync());
        }

        // GET: AnimatieSchedules/Details/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimatieSchedule animatieSchedule = await db.AnimatieSchedules.FindAsync(id);
            if (animatieSchedule == null)
            {
                return HttpNotFound();
            }
            return View(animatieSchedule);
        }

        // GET: AnimatieSchedules/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnimatieSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Activity,Day,Date,HourFrom,HourTo,Location,Age,Description ")] AnimatieSchedule animatieSchedule)
        {
            if (ModelState.IsValid)
            {
                animatieSchedule.Id = Guid.NewGuid();
                db.AnimatieSchedules.Add(animatieSchedule);
                await db.SaveChangesAsync();
                return RedirectToAction("AdminAnimatie");
            }

            return View(animatieSchedule);
        }


        // GET: AnimatieSchedules/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimatieSchedule animatieSchedule = await db.AnimatieSchedules.FindAsync(id);
            if (animatieSchedule == null)
            {
                return HttpNotFound();
            }
            return View(animatieSchedule);
        }

        // POST: AnimatieSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Activity,Day,Date,HourFrom,HourTo,Location,Age,Description ")] AnimatieSchedule animatieSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animatieSchedule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("AdminAnimatie");
            }
            return View(animatieSchedule);
        }

        // GET: AnimatieSchedules/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimatieSchedule animatieSchedule = await db.AnimatieSchedules.FindAsync(id);
            if (animatieSchedule == null)
            {
                return HttpNotFound();
            }
            return View(animatieSchedule);
        }

        // POST: AnimatieSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            AnimatieSchedule animatieSchedule = await db.AnimatieSchedules.FindAsync(id);
            db.AnimatieSchedules.Remove(animatieSchedule);
            await db.SaveChangesAsync();
            return RedirectToAction("AdminAnimatie");
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
