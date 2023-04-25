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
    public class Clinic_ScheduleController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: Clinic_Schedule
        public ActionResult Index()
        {
            return View(db.Clinic_Schedule.ToList());
        }

        // GET: Clinic_Schedule/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic_Schedule clinic_Schedule = db.Clinic_Schedule.Find(id);
            if (clinic_Schedule == null)
            {
                return HttpNotFound();
            }
            return View(clinic_Schedule);
        }

        // GET: Clinic_Schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clinic_Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "event_id,title,description,event_start,event_end,all_day")] Clinic_Schedule clinic_Schedule)
        {
            if (ModelState.IsValid)
            {
                db.Clinic_Schedule.Add(clinic_Schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clinic_Schedule);
        }

        // GET: Clinic_Schedule/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic_Schedule clinic_Schedule = db.Clinic_Schedule.Find(id);
            if (clinic_Schedule == null)
            {
                return HttpNotFound();
            }
            return View(clinic_Schedule);
        }

        // POST: Clinic_Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "event_id,title,description,event_start,event_end,all_day")] Clinic_Schedule clinic_Schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clinic_Schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clinic_Schedule);
        }

        // GET: Clinic_Schedule/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic_Schedule clinic_Schedule = db.Clinic_Schedule.Find(id);
            if (clinic_Schedule == null)
            {
                return HttpNotFound();
            }
            return View(clinic_Schedule);
        }

        // POST: Clinic_Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Clinic_Schedule clinic_Schedule = db.Clinic_Schedule.Find(id);
            db.Clinic_Schedule.Remove(clinic_Schedule);
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
