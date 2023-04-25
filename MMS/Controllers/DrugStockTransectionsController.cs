using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MMS;

namespace MMS.Controllers
{
    public class DrugStockTransectionsController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: DrugStockTransections
        public ActionResult Index()
        {
            return View(db.DrugStockTransections.ToList());
        }

        // GET: DrugStockTransections/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugStockTransection drugStockTransection = db.DrugStockTransections.Find(id);
            if (drugStockTransection == null)
            {
                return HttpNotFound();
            }
            return View(drugStockTransection);
        }

        // GET: DrugStockTransections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DrugStockTransections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransectionID,StockID,ItemID,IssuedTo,IssuedDate,BatchID,TransectionQty,IssuedUser")] DrugStockTransection drugStockTransection)
        {
            if (ModelState.IsValid)
            {
                db.DrugStockTransections.Add(drugStockTransection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drugStockTransection);
        }

        // GET: DrugStockTransections/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugStockTransection drugStockTransection = db.DrugStockTransections.Find(id);
            if (drugStockTransection == null)
            {
                return HttpNotFound();
            }
            return View(drugStockTransection);
        }

        // POST: DrugStockTransections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransectionID,StockID,ItemID,IssuedTo,IssuedDate,BatchID,TransectionQty,IssuedUser")] DrugStockTransection drugStockTransection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drugStockTransection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drugStockTransection);
        }

        // GET: DrugStockTransections/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugStockTransection drugStockTransection = db.DrugStockTransections.Find(id);
            if (drugStockTransection == null)
            {
                return HttpNotFound();
            }
            return View(drugStockTransection);
        }

        // POST: DrugStockTransections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DrugStockTransection drugStockTransection = db.DrugStockTransections.Find(id);
            db.DrugStockTransections.Remove(drugStockTransection);
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
