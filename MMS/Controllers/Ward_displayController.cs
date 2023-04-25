using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MMS;
using MMS.Models;



namespace MMS.Controllers
{
    public class Ward_displayController : Controller
    {
        private MMSEntities db = new MMSEntities();
       // private P3Context dbhrms = new P3Context();
        //private P2Context dbp2 = new P2Context();
        // GET: Ward_display
        public ActionResult Index()
        {
            int userid = Convert.ToInt32(Session["UserID"]);
            var roleperm = db.UserRoles.Where(p => p.RoleID == "R001").Where(p => p.UserID == userid).ToList().Count;
           string rolepm= roleperm.ToString();
            Session["wardcountper"] = rolepm;
           Session["offcount"] = db.Ward_display.Where(p => p.PwardD.Contains("Officer")).ToList().Count;
            Session["malecount"] = db.Ward_display.Where(p => p.PwardD.StartsWith("male")).ToList().Count;
            Session["femalecount"] = db.Ward_display.Where(p => p.PwardD.Contains("Female")).ToList().Count;

            return View(db.Ward_display.Where(p=>p.PwardD.Contains("Officer")).ToList());
        }
        public ActionResult Index1()
        {
            return View(db.Ward_display.Where(p => p.PwardD.StartsWith("male")).ToList());
        }
        public ActionResult Index2()
        {
            return View(db.Ward_display.Where(p => p.PwardD.Contains("Female")).ToList());
        }
        // GET: Ward_display/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward_display ward_display = db.Ward_display.Find(id);
            if (ward_display == null)
            {
                return HttpNotFound();
            }
            return View(ward_display);
        }

        // GET: Ward_display/Create
        public ActionResult Create()
        {

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationID");
            ViewBag.Ward_ID = new SelectList(db.Ward_Master, "Ward_ID", "Ward_Type");
            ViewBag.RelationshipType = new SelectList(db.RelationshipTypes, "RTypeID", "Relationship");
            ViewBag.RANK = new SelectList(db.ranks, "RANK1", "RNK_NAME");
            return View();
        }

        // POST: Ward_display/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ward_ID,RANK,WPID,Pname,Prank,Pwardno,Pbedno,PwardD,Pdiagnosis,Padmitdate,PserviceNo")] Ward_display ward_display, [Bind(Include = "Ward_ID,RANK,WPID,Pname,Prank,Pwardno,Pbedno,PwardD,Pdiagnosis,Padmitdate,PserviceNo,RelationshipType")] Ward_displaymodel ward_display1)
        {
            Patient patient=new Patient();
            if (ModelState.IsValid)
            {
                ward_display.Padmitdate = ward_display1.Padmitdate;
                ward_display.Pbedno = ward_display1.Pbedno;
                ward_display.Pdiagnosis = ward_display1.Pdiagnosis;
                ward_display.Pname = ward_display1.Pname;
                ward_display.PserviceNo = ward_display1.PserviceNo;






                String General = "";
                int rnd = Convert.ToInt32(ward_display1.RANK);
                var Generalvar = from s in db.ranks.Where(p => p.RANK1 ==rnd) select new { s.RNK_NAME };

                foreach (var item in Generalvar)
                {

                    General = item.RNK_NAME;
                }

                ward_display.Prank = General;

                String General1 = "";
                var Generalvar1 = from s in db.Ward_Master.Where(p => p.Ward_ID == ward_display1.Ward_ID) select new { s.Ward_Type };

                foreach (var item in Generalvar1)
                {

                    General1 = item.Ward_Type;
                }

                ward_display.PwardD = General1;
                ward_display.Pwardno = ward_display1.Pwardno;

                IndexGeneration indi = new IndexGeneration();

                //FormCollection oFormCollection,
                //Patient ooPatient = new Patient();
                //TryValidateModel(ooPatient);

             
                
                    byte[] bytes = null;
                  //  var sextype = patientm.Sex;
                  
                    var PersonResultList = from s in db.PersonalDetails
                                               //join b in dbhrms.ranks on s.Rank equals b.RANK1
                                           
                                           where s.ServiceNo == ward_display1.PserviceNo
                                           select new { s.DateOfBirth,s.Gender };

                    foreach (var item in PersonResultList)
                    {
                        if (bytes == null)
                        {
                            //patient.ProfilePicture = item.ProfilePicture;
                        patient.Sex = Convert.ToInt32(item.Gender);
                        patient.DateOfBirth = item.DateOfBirth;
                        //patient.LocationID = item.Posted_Location;
                        patient.MedCatID = 1;
                    }
                    }
///////////////////////////////////////////////
                var PersonResultList3 = from s in db.PersonalDetails
                                           //join b in dbhrms.ranks on s.Rank equals b.RANK1
                                      
                                       where s.ServiceNo == ward_display1.PserviceNo
                                       select new {  s.BloodGroup };

                foreach (var item in PersonResultList3)
                {
                    if (bytes == null)
                    {
                        
                       // patient.BGID = item.BloodGroup;
                      
                    }
                }

                //////////////////////////////
                if (1 == 1)
                    {
                        if (bytes == null)
                        {
                            var PersonResultList1 = from s in db.PersonalDetails
                                                    join b in db.Vw_PsnlImageP3 on s.SNo equals b.SNo
                                                   
                                                    where s.ServiceNo == ward_display1.PserviceNo
                                                    select new { b.ProfilePicture, s.DateOfBirth, s.Gender};
                            foreach (var item in PersonResultList1)
                            {
                                //patient.ProfilePicture = item.PersonalImage;
                            patient.Sex = Convert.ToInt32(item.Gender);
                            patient.DateOfBirth = item.DateOfBirth;
                            //patient.LocationID = item.Posted_Location;
                            patient.MedCatID = 1;
                        }
                        }

                    //////////////////////////
                    var PersonResultList4 = from s in db.PersonalDetails
                                                //join b in dbhrms.ranks on s.Rank equals b.RANK1

                                            where s.ServiceNo == ward_display1.PserviceNo
                                            select new { s.BloodGroup };

                    foreach (var item in PersonResultList4)
                    {
                        if (bytes == null)
                        {

                           // patient.BGID = item.BloodGroup;

                        }
                    }
                    //////////////////////////
                }
               
                patient.ServiceNo = ward_display1.PserviceNo;
                    patient.PID = indi.CreatePID(ward_display1.RelationshipType.Value, patient.ServiceNo);
                patient.RelationshipType = ward_display1.RelationshipType.Value;
                    // string rnktype = ranktype.Substring(7, ranktype.Length-7);

                patient.RANK = Convert.ToInt32(rnd);
                    patient.CreatedDate = DateTime.Now;
                patient.Surname= ward_display1.Pname;
                patient.Status = 1;
                /////////////////////////////////////////////////////
                string opdid = "";
                string locid = "";
                int userid = Convert.ToInt32(Session["UserID"]);
                var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

                foreach (var item in ser)
                {

                    // locid = item.LocationID;
                }
                var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
                opdid = (String)Session["userlocid1"];
                foreach (var item in opd)
                {

                    //opdid = item.LOCID;
                }
                var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                foreach (var item in serW)
                {

                    locid = item.LocationID;
                }
                Patient_Detail patient_Detail = new Patient_Detail();
               
                patient_Detail.PDID = indi.CreatePDID(patient.PID);
                patient_Detail.PID = patient.PID;
                patient_Detail.OPDID = opdid;
                patient_Detail.Present_Complain = "Admit";
                patient_Detail.CreatedBy = userid.ToString();
                //patient_Detail.ModifiedDate= DateTime.Now;
                //patient_Detail.ModifiedBy = userid.ToString();
                //patient_Detail.ModifiedMachine = userid.ToString();
                patient_Detail.CreatedDate = DateTime.Now;
                //patient_Detail.CreatedMachine = userid.ToString();
                patient_Detail.Status = 1;

                TranferDetail oTranferDetails = new TranferDetail();
                oTranferDetails.PDID = patient_Detail.PDID;
                oTranferDetails.ToLoc = opdid;
                oTranferDetails.FromLoc = opdid;
                oTranferDetails.TransferDate = DateTime.Now.Date;
                oTranferDetails.TransID = indi.CreateTransID(patient_Detail.PDID);
                oTranferDetails.TransStatus = 1;
                string Abdominal = "";
                string Abdominal1 = "";
                DateTime dd = DateTime.Now.Date;
               
               
                    var Abdominalvar1 = from s in db.Patient_Detail.Where(p => p.PID == patient.PID).Where(p => p.OPDID == opdid).Where(p => p.Status != 2) select new { s.PDID };

                    foreach (var item in Abdominalvar1)
                    {

                        Abdominal = item.PDID;
                        if (String.IsNullOrEmpty(Abdominal))
                        {
                            Abdominal = patient_Detail.PDID;
                        }
                    }
                if (String.IsNullOrEmpty(Abdominal))
                {
                    db.TranferDetails.Add(oTranferDetails);


                    db.Patient_Detail.Add(patient_Detail);

                }


                ///////////////////////

                var oldtestcnt = db.Patients.Where(d => d.PID == patient.PID).ToList().Count;
                if (oldtestcnt < 1)
                {
                    db.Patients.Add(patient);
                }

                  
                db.Ward_display.Add(ward_display);
              
                try
                    {
                        db.SaveChanges();
                    ModelState.AddModelError("CustomError", "Added!");

                }
                    catch (Exception ex)
                    {
                    //Session["errormsv"] = "Incorrect Service No!";
                    ModelState.AddModelError("CustomError", "Incorrect Service No!");
                }





                  
              //  return RedirectToAction("Create");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationID");
            ViewBag.Ward_ID = new SelectList(db.Ward_Master, "Ward_ID", "Ward_Type");
            ViewBag.RelationshipType = new SelectList(db.RelationshipTypes, "RTypeID", "Relationship");
            ViewBag.RANK = new SelectList(db.ranks, "RANK1", "RNK_NAME");
            return View();
        }

        // GET: Ward_display/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward_display ward_display = db.Ward_display.Find(id);
            if (ward_display == null)
            {
                return HttpNotFound();
            }
            return View(ward_display);
        }

        // POST: Ward_display/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WPID,Pname,Prank,Pwardno,Pbedno,PwardD,Pdiagnosis,Padmitdate,Pserviceno")] Ward_display ward_display)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ward_display).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ward_display);
        }

        // GET: Ward_display/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward_display ward_display = db.Ward_display.Find(id);
            if (ward_display == null)
            {
                return HttpNotFound();
            }
            return View(ward_display);
        }

        // POST: Ward_display/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Ward_display ward_display = db.Ward_display.Find(id);
            db.Ward_display.Remove(ward_display);
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
