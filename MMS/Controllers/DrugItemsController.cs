using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MMS;
using Newtonsoft.Json;
using PagedList;

namespace MMS.Controllers
{
    public class DrugItemsController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: DrugItems
        public ActionResult Index()
        {
            return View(db.DrugItems.ToList());
        }

        // GET: DrugItems/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugItem drugItem = db.DrugItems.Find(id);
            if (drugItem == null)
            {
                return HttpNotFound();
            }
            return View(drugItem);
        }

        // GET: DrugItems/Create
        public ActionResult Create(int? page)
        {
            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //    locid = item.LocationID;
            //}
            opdid = (String)Session["userlocid1"];
            var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
            foreach (var item in clincd)
            {

                locid = item.LocationID;
            }
            var onePageOfProducts = (dynamic)null;
           
            var lablist = db.DrugItems.Where(p => p.LocationID == locid);
            IEnumerable<DrugItem> filist = lablist.GroupBy(c => new { c.DrugID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.DrugID);
            var pageNumber = page ?? 1;
            onePageOfProducts = filist.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }

        // POST: DrugItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,ItemShortDescription,ItemDescription,UOF,TypeOfForm,DrugGroup,StorageCodition,Status,Remarks,StockQuantity,LocationID,DrugID")] DrugItem drugItem)
        {
            if (ModelState.IsValid)
            {
                db.DrugItems.Add(drugItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drugItem);
        }
        public JsonResult saveditem(string itemdescription, string qnty, string dordno)
        {
          
                    return Json("", JsonRequestBehavior.AllowGet);
        }
        // GET: DrugItems/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugItem drugItem = db.DrugItems.Find(id);
            if (drugItem == null)
            {
                return HttpNotFound();
            }
            return View(drugItem);
        }
        public class Drugreader
        {

            public string itemno { get; set; }
            public int dRoute { get; set; }
            public int dMethod { get; set; }
            public string dDuration { get; set; }
            public string dItemno { get; set; }

        }
        // POST: DrugItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,ItemShortDescription,ItemDescription,UOF,TypeOfForm,DrugGroup,StorageCodition,Status,Remarks,StockQuantity,LocationID,DrugID")] DrugItem drugItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drugItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drugItem);
        }

        // GET: DrugItems/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugItem drugItem = db.DrugItems.Find(id);
            if (drugItem == null)
            {
                return HttpNotFound();
            }
            return View(drugItem);
        }

        // POST: DrugItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DrugItem drugItem = db.DrugItems.Find(id);
            db.DrugItems.Remove(drugItem);
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
