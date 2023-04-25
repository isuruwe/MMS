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
    public class OPD_MasterController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: OPD_Master
        public async Task<ActionResult> Index()
        {
            var oPD_Master = db.OPD_Master.Include(o => o.Location);
            return View(await oPD_Master.ToListAsync());
        }

        // GET: OPD_Master/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPD_Master oPD_Master = await db.OPD_Master.FindAsync(id);
            if (oPD_Master == null)
            {
                return HttpNotFound();
            }
            return View(oPD_Master);
        }

        // GET: OPD_Master/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
            return View();
        }

        // POST: OPD_Master/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OPD_ID,CurrentNo,MaxPatients,OPD_Detail,LocationID")] OPD_Master oPD_Master)
        {
            if (ModelState.IsValid)
            {
                db.OPD_Master.Add(oPD_Master);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "STN_CODE", oPD_Master.LocationID);
            return View(oPD_Master);
        }

        // GET: OPD_Master/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPD_Master oPD_Master = await db.OPD_Master.FindAsync(id);
            if (oPD_Master == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "STN_CODE", oPD_Master.LocationID);
            return View(oPD_Master);
        }

        // POST: OPD_Master/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OPD_ID,CurrentNo,MaxPatients,OPD_Detail,LocationID")] OPD_Master oPD_Master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oPD_Master).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "STN_CODE", oPD_Master.LocationID);
            return View(oPD_Master);
        }

        // GET: OPD_Master/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPD_Master oPD_Master = await db.OPD_Master.FindAsync(id);
            if (oPD_Master == null)
            {
                return HttpNotFound();
            }
            return View(oPD_Master);
        }

        // POST: OPD_Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            OPD_Master oPD_Master = await db.OPD_Master.FindAsync(id);
            db.OPD_Master.Remove(oPD_Master);
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
