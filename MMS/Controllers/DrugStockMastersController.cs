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
    public class DrugStockMastersController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: DrugStockMasters
        public ActionResult Index()
        {
            return View(db.DrugStockMasters.ToList());
        }

        // GET: DrugStockMasters/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugStockMaster drugStockMaster = db.DrugStockMasters.Find(id);
            if (drugStockMaster == null)
            {
                return HttpNotFound();
            }
            return View(drugStockMaster);
        }

        // GET: DrugStockMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DrugStockMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemIndex,ItemID,BatchID,ExpireDate,MFD,LOC,StoreID,DrugQuantity")] DrugStockMaster drugStockMaster)
        {
            if (ModelState.IsValid)
            {
                db.DrugStockMasters.Add(drugStockMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drugStockMaster);
        }

        // GET: DrugStockMasters/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugStockMaster drugStockMaster = db.DrugStockMasters.Find(id);
            if (drugStockMaster == null)
            {
                return HttpNotFound();
            }
            return View(drugStockMaster);
        }

        // POST: DrugStockMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemIndex,ItemID,BatchID,ExpireDate,MFD,LOC,StoreID,DrugQuantity")] DrugStockMaster drugStockMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drugStockMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drugStockMaster);
        }

        // GET: DrugStockMasters/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugStockMaster drugStockMaster = db.DrugStockMasters.Find(id);
            if (drugStockMaster == null)
            {
                return HttpNotFound();
            }
            return View(drugStockMaster);
        }

        // POST: DrugStockMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DrugStockMaster drugStockMaster = db.DrugStockMasters.Find(id);
            db.DrugStockMasters.Remove(drugStockMaster);
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
