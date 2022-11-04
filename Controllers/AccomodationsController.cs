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
    public class AccomodationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accomodations
        public async Task<ActionResult> Index()
        {
            return View(await db.Accomodations.ToListAsync());
        }

        // GET: Accomodations/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomodation accomodation = await db.Accomodations.FindAsync(id);
            if (accomodation == null)
            {
                return HttpNotFound();
            }
            return View(accomodation);
        }

        // GET: Accomodations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accomodations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id")] Accomodation accomodation)
        {
            if (ModelState.IsValid)
            {
                db.Accomodations.Add(accomodation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(accomodation);
        }

        // GET: Accomodations/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomodation accomodation = await db.Accomodations.FindAsync(id);
            if (accomodation == null)
            {
                return HttpNotFound();
            }
            return View(accomodation);
        }

        // POST: Accomodations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id")] Accomodation accomodation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accomodation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(accomodation);
        }

        // GET: Accomodations/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomodation accomodation = await db.Accomodations.FindAsync(id);
            if (accomodation == null)
            {
                return HttpNotFound();
            }
            return View(accomodation);
        }

        // POST: Accomodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Accomodation accomodation = await db.Accomodations.FindAsync(id);
            db.Accomodations.Remove(accomodation);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
