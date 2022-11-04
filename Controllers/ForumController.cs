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
    public class ForumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Forum
        public async Task<ActionResult> Index()
        {
            return View(await db.ForumModels.ToListAsync());
        }

        [Authorize]
        // GET: Forum/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumModels forumModels = await db.ForumModels.FindAsync(id);
            if (forumModels == null)
            {
                return HttpNotFound();
            }
            return View(forumModels);
        }

        [Authorize]
        // GET: Forum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Campsite,Camping,Review,IssueDate")] ForumModels forumModels)
        {
            if (ModelState.IsValid)
            {
                db.ForumModels.Add(forumModels);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(forumModels);
        }

        [Authorize]
        // GET: Forum/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumModels forumModels = await db.ForumModels.FindAsync(id);
            if (forumModels == null)
            {
                return HttpNotFound();
            }
            return View(forumModels);
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Campsite,Camping,Review,IssueDate")] ForumModels forumModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forumModels).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(forumModels);
        }

        [Authorize]
        // GET: Forum/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumModels forumModels = await db.ForumModels.FindAsync(id);
            if (forumModels == null)
            {
                return HttpNotFound();
            }
            return View(forumModels);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ForumModels forumModels = await db.ForumModels.FindAsync(id);
            db.ForumModels.Remove(forumModels);
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
