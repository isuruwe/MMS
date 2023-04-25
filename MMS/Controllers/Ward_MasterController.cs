using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MMS.Controllers
{
    public class Ward_MasterController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: Ward_Master
        public async Task<ActionResult> Index()
        {
            var ward_Master = db.Ward_Master.Include(w => w.Location);
            return View(await ward_Master.ToListAsync());
        }

        // GET: Ward_Master/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward_Master ward_Master = await db.Ward_Master.FindAsync(id);
            if (ward_Master == null)
            {
                return HttpNotFound();
            }
            return View(ward_Master);
        }

        // GET: Ward_Master/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "STN_CODE");
            return View();
        }

        // POST: Ward_Master/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Ward_ID,CurrentBedCount,MaxPatients,Bed_Count,LocationID,Ward_Type")] Ward_Master ward_Master)
        {
            if (ModelState.IsValid)
            {
                db.Ward_Master.Add(ward_Master);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "STN_CODE", ward_Master.LocationID);
            return View(ward_Master);
        }

        // GET: Ward_Master/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward_Master ward_Master = await db.Ward_Master.FindAsync(id);
            if (ward_Master == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "STN_CODE", ward_Master.LocationID);
            return View(ward_Master);
        }

        // POST: Ward_Master/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Ward_ID,CurrentBedCount,MaxPatients,Bed_Count,LocationID,Ward_Type")] Ward_Master ward_Master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ward_Master).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "STN_CODE", ward_Master.LocationID);
            return View(ward_Master);
        }

        // GET: Ward_Master/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward_Master ward_Master = await db.Ward_Master.FindAsync(id);
            if (ward_Master == null)
            {
                return HttpNotFound();
            }
            return View(ward_Master);
        }

        // POST: Ward_Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Ward_Master ward_Master = await db.Ward_Master.FindAsync(id);
            db.Ward_Master.Remove(ward_Master);
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
