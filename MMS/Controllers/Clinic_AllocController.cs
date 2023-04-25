using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MMS.Controllers
{
    public class Clinic_AllocController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: Clinic_Alloc
        public ActionResult Index()
        {
            var clinic_Alloc = db.Clinic_Alloc.Include(c => c.Clinic_Master).Include(c => c.Patient_Detail);
            return View(clinic_Alloc.ToList());
        }

        // GET: Clinic_Alloc/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic_Alloc clinic_Alloc = db.Clinic_Alloc.Find(id);
            if (clinic_Alloc == null)
            {
                return HttpNotFound();
            }
            return View(clinic_Alloc);
        }

        // GET: Clinic_Alloc/Create
        public ActionResult Create()
        {
            ViewBag.ClinicID = new SelectList(db.Clinic_Master, "Clinic_ID", "CurrentNo");
            ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID");
            return View();
        }

        // POST: Clinic_Alloc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Clinic_Index,PDID,ClinicID,Date,Status,Clinic_Diagnosis")] Clinic_Alloc clinic_Alloc)
        {
            if (ModelState.IsValid)
            {
                db.Clinic_Alloc.Add(clinic_Alloc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClinicID = new SelectList(db.Clinic_Master, "Clinic_ID", "CurrentNo", clinic_Alloc.ClinicID);
            ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID", clinic_Alloc.PDID);
            return View(clinic_Alloc);
        }

        // GET: Clinic_Alloc/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic_Alloc clinic_Alloc = db.Clinic_Alloc.Find(id);
            if (clinic_Alloc == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClinicID = new SelectList(db.Clinic_Master, "Clinic_ID", "CurrentNo", clinic_Alloc.ClinicID);
            ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID", clinic_Alloc.PDID);
            return View(clinic_Alloc);
        }

        // POST: Clinic_Alloc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Clinic_Index,PDID,ClinicID,Date,Status,Clinic_Diagnosis")] Clinic_Alloc clinic_Alloc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clinic_Alloc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClinicID = new SelectList(db.Clinic_Master, "Clinic_ID", "CurrentNo", clinic_Alloc.ClinicID);
            ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID", clinic_Alloc.PDID);
            return View(clinic_Alloc);
        }

        // GET: Clinic_Alloc/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic_Alloc clinic_Alloc = db.Clinic_Alloc.Find(id);
            if (clinic_Alloc == null)
            {
                return HttpNotFound();
            }
            return View(clinic_Alloc);
        }

        // POST: Clinic_Alloc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Clinic_Alloc clinic_Alloc = db.Clinic_Alloc.Find(id);
            db.Clinic_Alloc.Remove(clinic_Alloc);
            db.SaveChanges();
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
