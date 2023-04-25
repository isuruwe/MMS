using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

using PagedList;

using MMS.Models;
using System.IO;
using System.Globalization;

using Microsoft.AspNet.Identity;

namespace MMS.Controllers
{
    public class PatientsController : Controller
    {
        public class AuthorizeSessionAttribute : AuthorizeAttribute
        {
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                var userID = httpContext.Session["UserID"];
                if (userID == null)
                {
                    var lo = httpContext.GetOwinContext().Authentication;
                    lo.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    httpContext.Response.Redirect("~/Users/login");
                    return false;
                }
                return true;
            }
        }
        private MMSEntities db = new MMSEntities();
       // private P3Context dbhrms = new P3Context();
       // private P2Context dbp2 = new P2Context();

        // GET: Patients
        [AuthorizeSession]
        public ActionResult Index(int? page, string id)
        {
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            var onePageOfProducts = (dynamic)null;
            if (!String.IsNullOrEmpty(id))
            {


                var patients = from s in db.Patients
                              
                               join z in db.RelationshipTypes on s.RelationshipType equals z.RTypeID
                                    where(s.ServiceNo.Contains(id)) orderby (s.CreatedDate) descending
                               select new getdocdetail { inililes = s.Initials, sname = s.Surname, sno = s.ServiceNo, rnkname = s.rank1.RNK_NAME, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };
                //db.Patients.Include(p => p.BloodGroup).Include(p => p.Location).Include(p => p.MBP_Category).Include(p => p.rank1).Include(p => p.RelationshipType1).Include(p => p.Service_Type1).Include(p => p.Sex_Type).Include(p => p.Status1).Include(p => p.Status2).Where(p => p.ServiceNo.Contains(id)).OrderByDescending(p => p.CreatedDate);
                var pageNumber = page ?? 1;
                onePageOfProducts = patients.ToPagedList(pageNumber, 10);

            }
            else
            {
                var patients = from s in db.Patients
                               
                               join z in db.RelationshipTypes on s.RelationshipType equals z.RTypeID
                                    orderby (s.CreatedDate) descending select new getdocdetail { inililes = s.Initials, sname = s.Surname, sno = s.ServiceNo, rnkname = s.rank1.RNK_NAME, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };
                //db.Patients.Include(p => p.BloodGroup).Include(p => p.Location).Include(p => p.MBP_Category).Include(p => p.rank1).Include(p => p.RelationshipType1).Include(p => p.Service_Type1).Include(p => p.Sex_Type).Include(p => p.Status1).Include(p => p.Status2).OrderByDescending(p => p.CreatedDate);
                var pageNumber = page ?? 1;
                onePageOfProducts = patients.ToPagedList(pageNumber, 10);
            }



            ViewBag.OnePageOfProducts = onePageOfProducts;

            return View();
        }

        // GET: Patients/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = await db.Patients.FindAsync(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create(string id)
        {
            Session["fromnurse"] = id;
            ViewBag.BGID = new SelectList(db.BloodGroups, "BGID", "BloodType");
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationID");
            ViewBag.MedCatID = new SelectList(db.MBP_Category, "MedCatID", "Category");
            ViewBag.RANK = new SelectList(db.ranks, "RANK1", "RNK_NAME");
            ViewBag.RelationshipType = new SelectList(db.RelationshipTypes, "RTypeID", "Relationship");
            ViewBag.Service_Type = new SelectList(db.Service_Type, "ServiceTypeID", "ServiceType");
            ViewBag.Sex = new SelectList(db.Sex_Type, "SxID", "SxDetail");
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec");
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec");
            return View();
        }



        // GET: Patients/Create
        public ActionResult Createnon(string id)
        {
            Session["fromnurse"] = id;
            ViewBag.BGID = new SelectList(db.BloodGroups, "BGID", "BloodType");
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationID");
            ViewBag.MedCatID = new SelectList(db.MBP_Category, "MedCatID", "Category");
            ViewBag.RANK = new SelectList(db.ranks, "RANK1", "RNK_NAME");
            ViewBag.RelationshipType = new SelectList(db.RelationshipTypes, "RTypeID", "Relationship");
            ViewBag.Service_Type = new SelectList(db.Service_Type, "ServiceTypeID", "ServiceType");
            ViewBag.Sex = new SelectList(db.Sex_Type, "SxID", "SxDetail");
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec");
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec");
            return View();
        }
        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createnon([Bind(Include = "PID,LocationID,Service_Type,ServiceNo,RANK,RelationshipType,ChildNo,Surname,Initials,DateOfBirth,Sex,BGID,MedCatID,Status,CreatedBy,CreatedDate,CreatedMachine,ModifiedBy,ModifiedDate,ModifiedMachine")] Patient patient, [Bind(Include = "PID,LocationID,Service_Type,ServiceNo,RANK,RelationshipType,ChildNo,Surname,Initials,DateOfBirth,Sex,BGID,MedCatID,Status,CreatedBy,CreatedDate,CreatedMachine,ModifiedBy,ModifiedDate,ModifiedMachine")] PatientModel patientm, HttpPostedFileBase file)
        {
            IndexGeneration indi = new IndexGeneration();

            //FormCollection oFormCollection,
            //Patient ooPatient = new Patient();
            //TryValidateModel(ooPatient);

            ModelState.Remove("DateOfBirth");
            ModelState.Remove("Sex");
            ModelState.Remove("RANK");
            if (ModelState.IsValid)
            {
                byte[] bytes = null;
                var sextype = patientm.Sex;
                if (file != null)
                {


                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        bytes = binaryReader.ReadBytes(file.ContentLength);
                    }

                    //patient.ProfilePicture = bytes;
                }
                //var PersonResultList = from s in dbhrms.ServicePersonnelProfiles
                //                           //join b in dbhrms.ranks on s.Rank equals b.RANK1
                //                       where s.ActiveNo == patient.ServiceNo
                //                       select new { s.ProfilePicture };

                //foreach (var item in PersonResultList)
                //{
                //    if (bytes == null)
                //    {
                //      //  patient.ProfilePicture = item.ProfilePicture;
                //    }
                //}
                //if (patient.ProfilePicture == null)
                //{
                //    if (bytes == null)
                //    {
                //        var PersonResultList1 = from s in dbp2.ServicePersonnelProfiles
                //                                join b in dbp2.F373_Image on s.SNo equals b.SvcID
                //                                where s.ServiceNo == patient.ServiceNo
                //                                select new { b.PersonalImage };
                //        foreach (var item in PersonResultList1)
                //        {
                //            patient.ProfilePicture = item.PersonalImage;
                //        }
                //    }
                //}

                patient.PID = indi.CreatePID(patient.RelationshipType.Value, patient.ServiceNo);
                var ranktype = patientm.RANK;
                // string rnktype = ranktype.Substring(7, ranktype.Length-7);
                string dobtype = patientm.DateOfBirth;
                DateTime dt = DateTime.ParseExact(dobtype, "yyyy-MM-dd", null);
                patient.DateOfBirth = dt;
                patient.RANK = Convert.ToInt32(ranktype);
                patient.CreatedDate = DateTime.Now.Date;
                patient.Sex = Convert.ToInt32(sextype);
                patient.Service_Type = Convert.ToInt32(patientm.Service_Type);
                patient.ChildNo = patientm.ChildNo;


                var chList = from s in db.Patients
                                 //join b in dbhrms.ranks on s.Rank equals b.RANK1
                             where s.ServiceNo == patient.ServiceNo && s.RelationshipType == 5 && s.ChildNo == patientm.ChildNo || s.DateOfBirth==dt
                             select new { s.ChildNo };

                int phListcnt = 0;
                if (patientm.RelationshipType != 5)
                {
                    var phList = from s in db.Patients
                                     //join b in dbhrms.ranks on s.Rank equals b.RANK1
                                 where s.ServiceNo == patient.ServiceNo && s.RelationshipType == patientm.RelationshipType
                                 select new { s.PID };
                    phListcnt = phList.Count();
                }
                db.Patients.Add(patient);
                try
                {
                    if (chList.Count() > 0 && patientm.RelationshipType == 5)
                    {
                        ModelState.AddModelError("CustomError", "Child Already Registered!");
                    }
                    else if (phListcnt > 0)
                    {
                        ModelState.AddModelError("CustomError", "Patient Already Registered!");
                    }
                    else
                    {
                        db.SaveChanges();
                        if (Session["fromnurse"] != null && Session["fromnurse"].ToString() == "1")
                        {
                            Session["fromnurse"] = "0";
                            return RedirectToAction("NurseCreate", "Patient_Detail", "");
                        }
                        else
                        {
                            return RedirectToAction("Createnon");
                        }
                    }


                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CustomError", "Already Registered!");

                }



            }

            ViewBag.BGID = new SelectList(db.BloodGroups, "BGID", "BloodType", patient.BGID);
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationID");
            ViewBag.MedCatID = new SelectList(db.MBP_Category, "MedCatID", "Category", patient.MedCatID);
            ViewBag.RANK = new SelectList(db.ranks, "RANK1", "RNK_NAME", patient.RANK);
            ViewBag.RelationshipType = new SelectList(db.RelationshipTypes, "RTypeID", "Relationship", patient.RelationshipType);
            ViewBag.Service_Type = new SelectList(db.Service_Type, "ServiceTypeID", "ServiceType", patient.Service_Type);
            ViewBag.ServiceNo = new SelectList(db.ServicePersonnelProfiles, "ServiceNo", "SvcID", patient.ServiceNo);
            ViewBag.Sex = new SelectList(db.Sex_Type, "SxID", "SxDetail", patient.Sex);
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec", patient.Status);
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec", patient.Status);


            return View(patient);

        }


        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PID,LocationID,Service_Type,ServiceNo,RANK,RelationshipType,ChildNo,Surname,Initials,DateOfBirth,Sex,BGID,MedCatID,Status,CreatedBy,CreatedDate,CreatedMachine,ModifiedBy,ModifiedDate,ModifiedMachine")] Patient patient, [Bind(Include = "PID,LocationID,Service_Type,ServiceNo,RANK,RelationshipType,ChildNo,Surname,Initials,DateOfBirth,Sex,BGID,MedCatID,Status,CreatedBy,CreatedDate,CreatedMachine,ModifiedBy,ModifiedDate,ModifiedMachine")] PatientModel patientm, HttpPostedFileBase file)
        {
            IndexGeneration indi = new IndexGeneration();

            //FormCollection oFormCollection,
            //Patient ooPatient = new Patient();
            //TryValidateModel(ooPatient);

            ModelState.Remove("DateOfBirth");
            ModelState.Remove("Sex");
            ModelState.Remove("RANK");
            if (ModelState.IsValid)
            {

                var sextype = patientm.Sex;
                //if (file != null)
                //{

                //    byte[] bytes = null;
                //    using (var binaryReader = new BinaryReader(file.InputStream))
                //    {
                //        bytes = binaryReader.ReadBytes(file.ContentLength);
                //    }

                //    //patient.ProfilePicture = bytes;
                //}
                //var PersonResultList = from s in dbhrms.ServicePersonnelProfiles
                //                           //join b in dbhrms.ranks on s.Rank equals b.RANK1
                //                       where s.ActiveNo == patient.ServiceNo
                //                       select new { s.ProfilePicture };

                //foreach (var item in PersonResultList)
                //{
                //    patient.ProfilePicture = item.ProfilePicture;
                //}
                //if (patient.ProfilePicture == null)
                //{
                //    var PersonResultList1 = from s in dbp2.ServicePersonnelProfiles
                //                            join b in dbp2.F373_Image on s.SNo equals b.SvcID
                //                            where s.ServiceNo == patient.ServiceNo
                //                            select new { b.PersonalImage };
                //    foreach (var item in PersonResultList1)
                //    {
                //        patient.ProfilePicture = item.PersonalImage;
                //    }
                //}

                patient.PID = indi.CreatePID(patient.RelationshipType.Value, patient.ServiceNo);
                var ranktype = patientm.RANK;
                // string rnktype = ranktype.Substring(7, ranktype.Length-7);
                string dobtype = patientm.DateOfBirth;
                DateTime dt = DateTime.ParseExact(dobtype, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                patient.DateOfBirth = dt;
                patient.RANK = Convert.ToInt32(ranktype);
                patient.CreatedDate = DateTime.Now.Date;
                patient.Sex = Convert.ToInt32(sextype);
                patient.ChildNo = patientm.ChildNo;

                var chList = from s in db.Patients
                                 //join b in dbhrms.ranks on s.Rank equals b.RANK1
                             where s.ServiceNo == patient.ServiceNo && s.RelationshipType == 5 && s.ChildNo == patientm.ChildNo || s.DateOfBirth== dt
                             select new { s.ChildNo };

                int phListcnt = 0;
                if (patientm.RelationshipType != 5)
                {
                    var phList = from s in db.Patients
                                     //join b in dbhrms.ranks on s.Rank equals b.RANK1
                                 where s.ServiceNo == patient.ServiceNo && s.RelationshipType == patientm.RelationshipType
                                 select new { s.PID };
                    phListcnt = phList.Count();
                }
                db.Patients.Add(patient);
                try
                {
                    if (chList.Count() > 0 && patientm.RelationshipType == 5)
                    {
                        ModelState.AddModelError("CustomError", "Child Already Registered!");
                        return RedirectToAction("Create");
                    }
                    else if (phListcnt > 0)
                    {
                        ModelState.AddModelError("CustomError", "Patient Already Registered!");
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        db.SaveChanges();
                        if (Session["fromnurse"] != null && Session["fromnurse"].ToString() == "1")
                        {
                            Session["fromnurse"] = "0";
                            return RedirectToAction("NurseCreate", "Patient_Detail", "");
                        }
                        else
                        {
                            return RedirectToAction("Create");
                        }
                    }



                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CustomError", "Already Registered!");

                }



            }

            ViewBag.BGID = new SelectList(db.BloodGroups, "BGID", "BloodType", patient.BGID);
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationID");
            ViewBag.MedCatID = new SelectList(db.MBP_Category, "MedCatID", "Category", patient.MedCatID);
            ViewBag.RANK = new SelectList(db.ranks, "RANK1", "RNK_NAME", patient.RANK);
            ViewBag.RelationshipType = new SelectList(db.RelationshipTypes, "RTypeID", "Relationship", patient.RelationshipType);
            ViewBag.Service_Type = new SelectList(db.Service_Type, "ServiceTypeID", "ServiceType", patient.Service_Type);
            ViewBag.ServiceNo = new SelectList(db.ServicePersonnelProfiles, "ServiceNo", "SvcID", patient.ServiceNo);
            ViewBag.Sex = new SelectList(db.Sex_Type, "SxID", "SxDetail", patient.Sex);
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec", patient.Status);
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec", patient.Status);


            return View(patient);

        }
        // GET: Patients/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = await db.Patients.FindAsync(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            Session["psvc"] = id.ToString();
            ViewBag.psvc = id.ToString();
            ViewBag.BGID = new SelectList(db.BloodGroups, "BGID", "BloodType", patient.BGID);
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationID", patient.LocationID);
            ViewBag.MedCatID = new SelectList(db.MBP_Category, "MedCatID", "Category", patient.MedCatID);
            ViewBag.RANK = new SelectList(db.ranks, "RANK1", "RNK_NAME", patient.RANK);
            ViewBag.RelationshipType = new SelectList(db.RelationshipTypes, "RTypeID", "Relationship", patient.RelationshipType);
            ViewBag.Service_Type = new SelectList(db.Service_Type, "ServiceTypeID", "ServiceType", patient.Service_Type);
            ViewBag.ServiceNo = new SelectList(db.ServicePersonnelProfiles, "ServiceNo", "SvcID", patient.ServiceNo);
            ViewBag.Sex = new SelectList(db.Sex_Type, "SxID", "SxDetail", patient.Sex);
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec", patient.Status);
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec", patient.Status);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PID,LocationID,Service_Type,ServiceNo,RANK,RelationshipType,Surname,Initials,DateOfBirth,Sex,BGID,MedCatID")] Patient patient, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //if (file != null)
                //{

                //    //byte[] bytes = null;
                //    //using (var binaryReader = new BinaryReader(file.InputStream))
                //    //{
                //    //    bytes = binaryReader.ReadBytes(file.ContentLength);
                //    //}

                //    //patient.ProfilePicture = bytes;
                //}
                //else
                //{
                //    var PersonResultList = from s in db.Patients
                //                               //join b in dbhrms.ranks on s.Rank equals b.RANK1
                //                           where s.PID == patient.PID
                //                           select new { s.ProfilePicture };

                //    foreach (var item in PersonResultList)
                //    {
                //        patient.ProfilePicture = item.ProfilePicture;
                //    }


                //}
                patient.Status = 1;
                patient.CreatedDate = DateTime.Now.Date;
                int userid = Convert.ToInt32(Session["UserID"]);
                patient.CreatedBy = userid.ToString();

                db.Entry(patient).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BGID = new SelectList(db.BloodGroups, "BGID", "BloodType", patient.BGID);
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "STN_CODE", patient.LocationID);
            ViewBag.MedCatID = new SelectList(db.MBP_Category, "MedCatID", "Category", patient.MedCatID);
            ViewBag.RANK = new SelectList(db.ranks, "RANK1", "RNK_NAME", patient.RANK);
            ViewBag.RelationshipType = new SelectList(db.RelationshipTypes, "RelationshipTypeID", "RelationshipType1", patient.RelationshipType);
            ViewBag.Service_Type = new SelectList(db.Service_Type, "ServiceTypeID", "ServiceType", patient.Service_Type);
            ViewBag.ServiceNo = new SelectList(db.ServicePersonnelProfiles, "ServiceNo", "SvcID", patient.ServiceNo);
            ViewBag.Sex = new SelectList(db.Sex_Type, "SxID", "SxDetail", patient.Sex);
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec", patient.Status);
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec", patient.Status);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = await db.Patients.FindAsync(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        public JsonResult LoadServicePerson(string id)
        {
            char[] MyChar = { '/', '"', ' ' };

            var result = (dynamic)null;
            var result1 = (dynamic)null;
            var result2 = (dynamic)null;
            if (id != null || id.Length > 6)
            {
                try
                {
                    id = id.Trim(MyChar);

                    //id = id.Substring(1, 5);
                    var PersonResultList = from s in db.PersonalDetails
                                               //join b in dbhrms.ranks on s.Rank equals b.RANK1
                                           where s.ServiceNo == id
                                           select new { s.ServiceNo,  s.DateOfBirth, s.Rank, s.Surname, s.Initials, s.Gender };
                    result = PersonResultList;

                   
                    var PersonResultList2 = from s in db.PersonalDetails
                                            join b in db.Children on s.SNo equals b.SNo
                                            where s.ServiceNo == id
                                            orderby b.DOB
                                            select new { b.ChildName, b.DOB, b.Gender };

                    result2 = new { result = result, result1 = PersonResultList2 };
                }
                catch (Exception ex)
                {


                }
                return Json(result2, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(JsonRequestBehavior.AllowGet);




            //char[] MyChar = { '/', '"', ' ' };
            //string NewString = id.Trim(MyChar);
            //var spr = db.ServicePersonnelProfiles.Find(NewString);
            //var spr1 = spr.rank1.RNK_NAME + spr.Surname + spr.Initials;
            //spr.OtherNames = spr.rank1.RNK_NAME;
            // var sp=db.ServicePersonnelProfiles.Where(p=>p.ServiceNo==id).Select(p=>new RANK= p.Rank)
            // Do  your validation
            // var list = JsonConvert.SerializeObject(spr, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });


            //return Json(spr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Getservicenoedt(string id)
        {
            char[] MyChar = { '/', '"', ' ' };
            string svc = "";
            var result = (dynamic)null;
            var result1 = (dynamic)null;
            var result2 = (dynamic)null;
            if (id != null || id.Length > 6)
            {
                try
                {
                    id = id.Trim(MyChar);

                    //id = id.Substring(1, 5);
                    var PersonResultList = from s in db.Patients
                                               //join b in dbhrms.ranks on s.Rank equals b.RANK1
                                           where s.PID == id
                                           select new { s.ServiceNo, s.rank1.RNK_NAME, s.DateOfBirth, s.RANK, s.Surname, s.Initials, s.LocationID, s.Sex };
                    result = PersonResultList;
                    foreach (var item in PersonResultList)
                    {

                        svc = item.ServiceNo;
                    }

                    var PersonResultList2 = from s in db.PersonalDetails
                                            join b in db.Children on s.SNo equals b.SNo
                                            where s.ServiceNo == svc
                                            select new { b.ChildName, b.DOB };

                    result2 = new { result = result, result1 = PersonResultList2 };
                }
                catch (Exception ex)
                {


                }
                return Json(result2, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(JsonRequestBehavior.AllowGet);




            //char[] MyChar = { '/', '"', ' ' };
            //string NewString = id.Trim(MyChar);
            //var spr = db.ServicePersonnelProfiles.Find(NewString);
            //var spr1 = spr.rank1.RNK_NAME + spr.Surname + spr.Initials;
            //spr.OtherNames = spr.rank1.RNK_NAME;
            // var sp=db.ServicePersonnelProfiles.Where(p=>p.ServiceNo==id).Select(p=>new RANK= p.Rank)
            // Do  your validation
            // var list = JsonConvert.SerializeObject(spr, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });


            //return Json(spr, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadRanks()
        {
            //char[] MyChar = { '/', '"', ' ' };
            //string NewString = id.Trim(MyChar);

            var rnks = from s in db.ranks
                       select new { s.RANK1, s.RNK_NAME };
            return Json(rnks.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadRelationtype()
        {
            //char[] MyChar = { '/', '"', ' ' };
            //string NewString = id.Trim(MyChar);

            var Relat = from s in db.RelationshipTypes
                        select new { s.RTypeID, s.Relationship };
            return Json(Relat.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadSex()
        {
            //char[] MyChar = { '/', '"', ' ' };
            //string NewString = id.Trim(MyChar);

            var LSex = from s in db.Sex_Type
                       select new { s.SxID, s.SxDetail };
            return Json(LSex.ToList(), JsonRequestBehavior.AllowGet);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Patient patient = await db.Patients.FindAsync(id);
            db.Patients.Remove(patient);
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
