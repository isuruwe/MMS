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
    public class Staff_MasterController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: Staff_Master
        public async Task<ActionResult> Index()
        {
            var staff_Master = db.Staff_Master.Include(s => s.Job_Category).Include(s => s.Service_Type1).Include(s => s.SpecialityID);
            return View(await staff_Master.ToListAsync());
        }

        // GET: Staff_Master/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff_Master staff_Master = await db.Staff_Master.FindAsync(id);
            if (staff_Master == null)
            {
                return HttpNotFound();
            }
            return View(staff_Master);
        }

        // GET: Staff_Master/Create
        public ActionResult Create()
        {
            ViewBag.Job_CategoryID = new SelectList(db.Job_Category, "Job_CategoryID", "Job_Category1");
            ViewBag.Service_Type = new SelectList(db.Service_Type, "ServiceTypeID", "ServiceType");
            ViewBag.ServiceNo = new SelectList(db.ServicePersonnelProfiles, "ServiceNo", "SvcID");
            ViewBag.SpecialityID = new SelectList(db.Speciality_Type, "SpecialityID", "Speciality");
            return View();
        }

        // POST: Staff_Master/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SID,LocationID,Service_Type,ServiceNo,SpecialityID,Rank_ID,Job_CategoryID,Surname,Initials,ProfilePicture,Status,CreatedBy,CreatedDate,CreatedMachine,ModifiedBy,ModifiedDate,ModifiedMachine,LOCID")] Staff_Master staff_Master)
        {
            if (ModelState.IsValid)
            {
                db.Staff_Master.Add(staff_Master);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Job_CategoryID = new SelectList(db.Job_Category, "Job_CategoryID", "Job_Category1", staff_Master.Job_CategoryID);
            ViewBag.Service_Type = new SelectList(db.Service_Type, "ServiceTypeID", "ServiceType", staff_Master.Service_Type);
            ViewBag.ServiceNo = new SelectList(db.ServicePersonnelProfiles, "ServiceNo", "SvcID", staff_Master.ServiceNo);
            ViewBag.SpecialityID = new SelectList(db.Speciality_Type, "SpecialityID", "Speciality", staff_Master.SpecialityID);
            return View(staff_Master);
        }

        // GET: Staff_Master/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff_Master staff_Master = await db.Staff_Master.FindAsync(id);
            if (staff_Master == null)
            {
                return HttpNotFound();
            }
            ViewBag.Job_CategoryID = new SelectList(db.Job_Category, "Job_CategoryID", "Job_Category1", staff_Master.Job_CategoryID);
            ViewBag.Service_Type = new SelectList(db.Service_Type, "ServiceTypeID", "ServiceType", staff_Master.Service_Type);
            ViewBag.ServiceNo = new SelectList(db.ServicePersonnelProfiles, "ServiceNo", "SvcID", staff_Master.ServiceNo);
            ViewBag.SpecialityID = new SelectList(db.Speciality_Type, "SpecialityID", "Speciality", staff_Master.SpecialityID);
            return View(staff_Master);
        }

        // POST: Staff_Master/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SID,LocationID,Service_Type,ServiceNo,SpecialityID,Rank_ID,Job_CategoryID,Surname,Initials,ProfilePicture,Status,CreatedBy,CreatedDate,CreatedMachine,ModifiedBy,ModifiedDate,ModifiedMachine,LOCID")] Staff_Master staff_Master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff_Master).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Job_CategoryID = new SelectList(db.Job_Category, "Job_CategoryID", "Job_Category1", staff_Master.Job_CategoryID);
            ViewBag.Service_Type = new SelectList(db.Service_Type, "ServiceTypeID", "ServiceType", staff_Master.Service_Type);
            ViewBag.ServiceNo = new SelectList(db.ServicePersonnelProfiles, "ServiceNo", "SvcID", staff_Master.ServiceNo);
            ViewBag.SpecialityID = new SelectList(db.Speciality_Type, "SpecialityID", "Speciality", staff_Master.SpecialityID);
            return View(staff_Master);
        }

        // GET: Staff_Master/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff_Master staff_Master = await db.Staff_Master.FindAsync(id);
            if (staff_Master == null)
            {
                return HttpNotFound();
            }
            return View(staff_Master);
        }

        // POST: Staff_Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Staff_Master staff_Master = await db.Staff_Master.FindAsync(id);
            db.Staff_Master.Remove(staff_Master);
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
