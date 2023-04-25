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
    public class EmailListsController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: EmailLists
        public ActionResult Index()
        {
            return View(db.EmailLists.ToList());
        }

        // GET: EmailLists/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailList emailList = db.EmailLists.Find(id);
            if (emailList == null)
            {
                return HttpNotFound();
            }
            return View(emailList);
        }

        // GET: EmailLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SvcID,Email")] EmailList emailList)
        {
            if (ModelState.IsValid)
            {
                db.EmailLists.Add(emailList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emailList);
        }

        // GET: EmailLists/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailList emailList = db.EmailLists.Find(id);
            if (emailList == null)
            {
                return HttpNotFound();
            }
            return View(emailList);
        }

        // POST: EmailLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SvcID,Email")] EmailList emailList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emailList);
        }

        // GET: EmailLists/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailList emailList = db.EmailLists.Find(id);
            if (emailList == null)
            {
                return HttpNotFound();
            }
            return View(emailList);
        }

        // POST: EmailLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            EmailList emailList = db.EmailLists.Find(id);
            db.EmailLists.Remove(emailList);
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
