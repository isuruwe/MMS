using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MMS.Models;
using System.Data.SqlClient;
using PagedList;

namespace MMS.Controllers
{
    public class userfeedbacksController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: userfeedbacks
        public ActionResult Index()
        {
            return View(db.userfeedbacks.ToList());
        }

        // GET: userfeedbacks/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userfeedback userfeedback = db.userfeedbacks.Find(id);
            if (userfeedback == null)
            {
                return HttpNotFound();
            }
            return View(userfeedback);
        }

        // GET: userfeedbacks/Create
        public ActionResult Create(int? page)
        {
            SqlConnection oSqlConnection;
            SqlCommand oSqlCommand;
            SqlDataAdapter oSqlDataAdapter;
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DateTime dd = DateTime.Now.Date;
            DataTable oDataSet4 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();

            sqlQuery = "    SELECT  * FROM userfeedback as a inner join Users as b on a.userid=b.UserID order by createdate desc";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet4);
            oSqlConnection.Close();
            var lid = oDataSet4.AsEnumerable()
    .Select(dataRow => new userfeedbacks
    {
        userid = dataRow.Field<String>("UserName"),
        comment = dataRow.Field<string>("comment"),
        createdate = dataRow.Field<DateTime?>("createdate"),

    }).ToList();

            var pageNumber = page ?? 1;
            var onePageOfProducts = (dynamic)null;
            onePageOfProducts = lid.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }

        // POST: userfeedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fid,userid,comment")] userfeedback userfeedback)
        {
            if (ModelState.IsValid)
            {
                userfeedback.userid = Convert.ToInt32(Session["UserID"]);
                userfeedback.createdate = DateTime.Now;
                db.userfeedbacks.Add(userfeedback);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(userfeedback);
        }

        // GET: userfeedbacks/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userfeedback userfeedback = db.userfeedbacks.Find(id);
            if (userfeedback == null)
            {
                return HttpNotFound();
            }
            return View(userfeedback);
        }

        // POST: userfeedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fid,userid,comment")] userfeedback userfeedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userfeedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userfeedback);
        }

        // GET: userfeedbacks/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userfeedback userfeedback = db.userfeedbacks.Find(id);
            if (userfeedback == null)
            {
                return HttpNotFound();
            }
            return View(userfeedback);
        }

        // POST: userfeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            userfeedback userfeedback = db.userfeedbacks.Find(id);
            db.userfeedbacks.Remove(userfeedback);
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
