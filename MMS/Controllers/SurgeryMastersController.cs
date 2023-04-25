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
using Newtonsoft.Json;
using PagedList;
using static MMS.Controllers.Patient_DetailController;

using System.Data.SqlClient;


namespace MMS.Controllers
{
    public class SurgeryMastersController : Controller
    {
        private MMSEntities db = new MMSEntities();
       // private EPASContext db = new EPASContext();
        private string err;
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        string sqlQuery;

        // GET: SurgeryMasters
        public ActionResult Index()
        {
            return View(db.SurgeryMasters.ToList());
        }

        // GET: SurgeryMasters/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurgeryMaster surgeryMaster = db.SurgeryMasters.Find(id);
            if (surgeryMaster == null)
            {
                return HttpNotFound();
            }
            return View(surgeryMaster);
        }
        public JsonResult Submitsurgery(string PID, string doa, string dos, string dod, string tt, string nop,
            string toa, string ind, string suitems, string attsrg, string aby, string ant, string moa, string sst, string set,
           string Catheter, string Ivline, string Epidural, string find, string prced, string drins, string matItems,
         string pomitems, string moins, string nutr, string nutri, string ditems, string spins, string nurs)
        {
            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                char[] MyChar = { '/', '"', ' ' };
                Boolean Catheter1 = false;
                Boolean Ivline1 = false;
                Boolean Epidural1 = false;

                PID = PID.Trim(MyChar);
                doa = doa.Trim(MyChar);
                dos = dos.Trim(MyChar);
                dod = dod.Trim(MyChar);
                tt = tt.Trim(MyChar);
                nop = nop.Trim(MyChar);
                toa = toa.Trim(MyChar);
                ind = ind.Trim(MyChar);
                suitems = suitems.Trim(MyChar);
                attsrg = attsrg.Trim(MyChar);
                aby = aby.Trim(MyChar);
                ant = ant.Trim(MyChar);
                moa = moa.Trim(MyChar);
                sst = sst.Trim(MyChar);
                set = set.Trim(MyChar);
                Catheter = Catheter.Trim(MyChar);
                Ivline = Ivline.Trim(MyChar);
                Epidural = Epidural.Trim(MyChar);
                find = find.Trim(MyChar);
                prced = prced.Trim(MyChar);
                drins = drins.Trim(MyChar);
                matItems = matItems.Trim(MyChar);
                pomitems = pomitems.Trim(MyChar);
                moins = moins.Trim(MyChar);
                nutr = nutr.Trim(MyChar);
                nutri = nutri.Trim(MyChar);
                ditems = ditems.Trim(MyChar);
                spins = spins.Trim(MyChar);
                nurs = nurs.Trim(MyChar);


                string Abdominal = "";
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
                IndexGeneration indi = new IndexGeneration();
                string pdid = indi.CreatePDID(PID);
                ///////////////////////////////

                SurgeryMaster oSurgeryMaster = new SurgeryMaster();
                oSurgeryMaster.PID = PID;

                oSurgeryMaster.PDID = pdid;
                oSurgeryMaster.PID = PID;
                DateTime doa1 = DateTime.Parse(doa);
                oSurgeryMaster.DateofAdmit = doa1;
                DateTime dos1 = DateTime.Parse(dos);
                oSurgeryMaster.DateofSurgery = dos1;
                if (!String.IsNullOrEmpty(dod))
                {
                    DateTime dod1 = DateTime.Parse(dod);
                    oSurgeryMaster.DateodDischarge = dod1;
                }

               
               
                oSurgeryMaster.TheaterTable = Convert.ToInt32(tt);
                var nop4 = JsonConvert.DeserializeObject<dynamic>(nop);
                oSurgeryMaster.ProcedureName = Convert.ToInt32(nop4.nprid);
                var toa4 = JsonConvert.DeserializeObject<dynamic>(toa);
                oSurgeryMaster.AnesthesiaType = Convert.ToInt32(toa4.TAID);
                var nutr4 = JsonConvert.DeserializeObject<dynamic>(nutr);
                oSurgeryMaster.Nutritionid = Convert.ToInt32(nutr4.NID);
                //  oSurgeryMaster.DateodProcedure = Convert.ToDateTime(dop);
                oSurgeryMaster.Indication = ind;
                var attsrg4 = JsonConvert.DeserializeObject<dynamic>(attsrg);
                oSurgeryMaster.Surgeon = attsrg4.modesc;
                var aby4 = JsonConvert.DeserializeObject<dynamic>(aby);
                oSurgeryMaster.AssistedBy = aby4.asidesc;
                
                oSurgeryMaster.Anesthetist = ant;
                
                oSurgeryMaster.SurgeryStart = sst;
              
                oSurgeryMaster.SurgeryEnd = set;

                if(!String.IsNullOrEmpty(Ivline))
                {
                    Ivline1 = Convert.ToBoolean(Ivline);
                }
                if (!String.IsNullOrEmpty(Catheter))
                {
                    Catheter1 = Convert.ToBoolean(Catheter);
                }
                if (!String.IsNullOrEmpty(Epidural))
                {
                    Epidural1 = Convert.ToBoolean(Epidural);
                }
                oSurgeryMaster.Catheter = Convert.ToBoolean(Catheter1);
                oSurgeryMaster.CentralIVline = Convert.ToBoolean(Ivline1);
                oSurgeryMaster.Epidural = Convert.ToBoolean(Epidural1);
                oSurgeryMaster.Findings = find;
                oSurgeryMaster.PrcedureDetail = prced;
                oSurgeryMaster.DrainsInserted = drins;
                //oSurgeryMaster.CentralIVline = Convert.ToBoolean(Ivline);
                var moa4 = JsonConvert.DeserializeObject<dynamic>(moa);
                oSurgeryMaster.AntibioticP = moa4.modesc;
                 oSurgeryMaster.Nurse= nurs;
                // oSurgeryMaster.Positioning= pos;
                // oSurgeryMaster.DurationofSurgery= dos;
                //   oSurgeryMaster.Incision= incs;
                //oSurgeryMaster.PrcedureDetail= prced;
                //oSurgeryMaster.DrainsInserted= drins;
                //  oSurgeryMaster.BloodLoss= bldlss;
                //  oSurgeryMaster.ClosureMethod= clmd;
                //  oSurgeryMaster.SutureMaterials= smu;
                oSurgeryMaster.MonitoringInstruct = moins;
                oSurgeryMaster.Nutrition = nutri;
                //oSurgeryMaster.Medi= medict;
                oSurgeryMaster.SpecialIntruct = spins;

                //////////////////////////
                var objsv9 = JsonConvert.DeserializeObject<List<matreader>>(matItems);
                int objcountv9 = objsv9.Count;
                SurgeryClosure[] objVital9 = new SurgeryClosure[objcountv9];
                int i19 = 0;

                foreach (matreader p in objsv9)
                {

                    SurgeryClosure oVital9 = new SurgeryClosure();
                    oVital9.PDID = pdid;
                    // oVital. = indi.CreateVID(i1, pdid);
                    oVital9.SuturematerialsID = Convert.ToInt32(p.tSMID);
                    oVital9.Technique =p.ttid;
                    oVital9.Noofpkts = p.tnopkt;


                    objVital9[i19] = oVital9;
                    i19++;
                    // db.Vitals.Add(oVital);

                }
                ////////////////////////////
                //////////////////////////
                var objsv = JsonConvert.DeserializeObject<List<pomreader>>(pomitems);
                int objcountv = objsv.Count;
                SurgeryPomDetail[] objVital = new SurgeryPomDetail[objcountv];
                int i1 = 0;

                foreach (pomreader p in objsv)
                {

                    SurgeryPomDetail oVital = new SurgeryPomDetail();
                    oVital.pdid = pdid;
                   // oVital. = indi.CreateVID(i1, pdid);
                    oVital.pcatid = p.tpomid;
                    oVital.pfeqid = Convert.ToInt32(p.tpomfid);
                    oVital.pduid = Convert.ToInt32(p.tpomdid);
                   

                    objVital[i1] = oVital;
                    i1++;
                    // db.Vitals.Add(oVital);

                }
                ////////////////////////////
                var objs1 = (dynamic)null;
                int objcount1 = 0;
                SurgeryAP[] objDrug1 = new SurgeryAP[1000];
                if (!String.IsNullOrEmpty(ditems))
                {


                    objs1 = JsonConvert.DeserializeObject<List<Drugreader>>(ditems);

                    objcount1 = objs1.Count;


                    int i = 0;

                    foreach (Drugreader p in objs1)
                    {

                        var oldtestcnt1 = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == pdid).Where(d => d.Issued == 0).ToList().Count;
                        if (oldtestcnt1 < 1)
                        {
                            SurgeryAP oDrug_Prescription1 = new SurgeryAP();
                            oDrug_Prescription1.PDID = pdid;
                            oDrug_Prescription1.Ps_Index = Guid.NewGuid().ToString();
                            oDrug_Prescription1.Dose = p.dDose;
                            oDrug_Prescription1.Method = p.dMethod;
                            oDrug_Prescription1.Route = p.dRoute;
                            oDrug_Prescription1.ItemNo = p.dItemno;
                            oDrug_Prescription1.GivenBy = "2";
                            oDrug_Prescription1.MethodType = Convert.ToInt32(p.dStockTypeID);
                            oDrug_Prescription1.Duration = p.dDuration;
                            oDrug_Prescription1.RequestedLocID = locid;
                            oDrug_Prescription1.LocID = opdid;
                            oDrug_Prescription1.Date_Time = DateTime.Now.Date;
                            objDrug1[i] = oDrug_Prescription1;
                            i++;
                        }

                    }
                }
                ///////////////////////////
                ////////////////////////////
                var objs = (dynamic)null;
                int objcount = 0;
                SurgeryAP[] objDrug = new SurgeryAP[1000];
                if (!String.IsNullOrEmpty(suitems))
                {


                    objs = JsonConvert.DeserializeObject<List<Drugreader1>>(suitems);

                    objcount = objs.Count;


                    int i = 0;

                    foreach (Drugreader1 p in objs)
                    {

                        var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.suItemno).Where(d => d.PDID == pdid).Where(d => d.Issued == 0).ToList().Count;
                        if (oldtestcnt < 1)
                        {
                            SurgeryAP oDrug_Prescription = new SurgeryAP();
                            oDrug_Prescription.PDID = pdid;
                            oDrug_Prescription.Ps_Index = Guid.NewGuid().ToString();
                            oDrug_Prescription.Dose = p.suDose;
                            oDrug_Prescription.Method = p.suMethod;
                            oDrug_Prescription.Route = p.suRoute;
                            oDrug_Prescription.ItemNo = p.suItemno;
                            oDrug_Prescription.GivenBy = "3";
                            oDrug_Prescription.MethodType = Convert.ToInt32(p.suStockTypeID);
                            oDrug_Prescription.Duration = p.suDuration;
                            oDrug_Prescription.RequestedLocID = locid;
                            oDrug_Prescription.LocID = opdid;
                            oDrug_Prescription.Date_Time = DateTime.Now.Date;
                            objDrug[i] = oDrug_Prescription;
                            i++;
                        }

                    }
                }
                ///////////////////////////
                Patient_Detail patient_Detail = new Patient_Detail();


                patient_Detail.PDID = pdid;
                patient_Detail.PID = PID;
                patient_Detail.OPDID = opdid;
                patient_Detail.Present_Complain = "For Surgery";
                patient_Detail.CreatedBy = userid.ToString();
                patient_Detail.PatientCatID = 3;
                patient_Detail.SubjectID = 0;
                //patient_Detail.ModifiedBy = userid.ToString();
                //patient_Detail.ModifiedMachine = userid.ToString();
                patient_Detail.CreatedDate = DateTime.Now;
                //patient_Detail.CreatedMachine = userid.ToString();
                patient_Detail.Status =2;

                TranferDetail oTranferDetails = new TranferDetail();
                oTranferDetails.PDID = patient_Detail.PDID;
                oTranferDetails.ToLoc = opdid;
                oTranferDetails.FromLoc = opdid;
                oTranferDetails.TransferDate = DateTime.Now;
                oTranferDetails.TransID = indi.CreateTransID(patient_Detail.PDID);
                oTranferDetails.TransStatus = 1;



                if (ModelState.IsValid)
                {
                    DateTime dd = DateTime.Now.Date;
                    var Abdominalvar = from s in db.Patient_Detail.Where(p => p.PID == PID).Where(p => p.Status == 1).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year) select new { s.PDID };

                    foreach (var item in Abdominalvar)
                    {

                        Abdominal = item.PDID;

                    }
                    if (String.IsNullOrEmpty(Abdominal))
                    {
                        try
                        {
                            db.SurgeryPomDetails.AddRange(objVital);
                            db.SurgeryClosures.AddRange(objVital9);
                            objDrug = objDrug.Where(x => x != null).ToArray();
                            db.SurgeryAPs.AddRange(objDrug);
                            objDrug1 = objDrug1.Where(x => x != null).ToArray();
                            db.SurgeryAPs.AddRange(objDrug1);
                            db.TranferDetails.Add(oTranferDetails);
                            db.SurgeryMasters.Add(oSurgeryMaster);
                            db.Patient_Detail.Add(patient_Detail);
                            db.SaveChanges();
                            err = "Saved";
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();

                        }
                    }
                    else
                    {
                        long catref = 0;
                        var catrefvar = from s in db.SurgeryMasters.Where(p => p.PDID == Abdominal) select new { s.SGID };

                        foreach (var item in catrefvar)
                        {

                            catref = item.SGID;
                        }
                        if (catref != 0)
                        {
                            oSurgeryMaster.SGID = catref;
                            oSurgeryMaster.PDID = Abdominal;
                            db.Entry(oSurgeryMaster).State = EntityState.Modified;
                        }
                        else
                        {
                            db.SurgeryMasters.Add(oSurgeryMaster);
                        }
                        try
                        {
                            db.SurgeryClosures.AddRange(objVital9);
                            db.SurgeryPomDetails.AddRange(objVital);
                            objDrug = objDrug.Where(x => x != null).ToArray();
                            db.SurgeryAPs.AddRange(objDrug);
                            objDrug1 = objDrug1.Where(x => x != null).ToArray();
                            db.SurgeryAPs.AddRange(objDrug1);
                            db.TranferDetails.Add(oTranferDetails);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }
                        err = "Saved!";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(err, JsonRequestBehavior.AllowGet);
        }




        public JsonResult Submitdischarge(string pdid, string doa, string dod, string dgn1, 
            string pcomp, string hpcomp, string pastmed, string pastsurg, string allgi, string mgh, string 
            suitems, string disind, string fupins, string bhtNo,string counsult, string cat,string invSummery)
        {
            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                char[] MyChar = { '/', '"', ' ' };
                Boolean Catheter1 = false;
                Boolean Ivline1 = false;
                Boolean Epidural1 = false;

                pdid = pdid.Trim(MyChar);
                doa = doa.Trim(MyChar);
                dod = dod.Trim(MyChar);
                bhtNo = bhtNo.Trim(MyChar);
                dgn1 = dgn1.Trim(MyChar);
                pcomp = pcomp.Trim(MyChar);
                hpcomp = hpcomp.Trim(MyChar);
                pastmed = pastmed.Trim(MyChar);
                suitems = suitems.Trim(MyChar);
                pastsurg = pastsurg.Trim(MyChar);
                allgi = allgi.Trim(MyChar);
                disind = disind.Trim(MyChar);
                fupins = fupins.Trim(MyChar);
                mgh = mgh.Trim(MyChar);
                counsult = counsult.Trim(MyChar);
                cat = cat.Trim(MyChar);
                invSummery = invSummery.Trim(MyChar);

                string PID = "";
                string Abdominal = "";
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
                //IndexGeneration indi = new IndexGeneration();
                //string pdid = indi.CreatePDID(PID);
                ///////////////////////////////

                DataTable oDataSet3 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT PID FROM dbo.Patient_Detail WHERE PDID ='"+pdid+"' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet3);
                //  oSqlConnection.Close();
                var a1 = oDataSet3.AsEnumerable()
        .Select(dataRow => new getdocdata
        {
            PID = dataRow.Field<string>("PID"),
        }).ToList();




                foreach (var iteme in a1)
                {
                    PID = iteme.PID;
                }

                ward_discharge oSurgeryMaster = new ward_discharge();
                oSurgeryMaster.PID = PID;

                oSurgeryMaster.PDID = pdid;
                
                DateTime doa1 = DateTime.Parse(doa);
                oSurgeryMaster.dateadmit = doa1;
              
                if (!String.IsNullOrEmpty(dod))
                {
                    DateTime dod1 = DateTime.Parse(dod);
                    oSurgeryMaster.datedischarge = dod1;
                }



                oSurgeryMaster.diagnosis = dgn1;
             
                oSurgeryMaster.dischargeins = disind;
               
                oSurgeryMaster.alergies = allgi;
               
                oSurgeryMaster.followupins = fupins;
               
                oSurgeryMaster.hispcomp = hpcomp;
              
                oSurgeryMaster.manageinhosp  = mgh;
               
                oSurgeryMaster.pastmedhis = pastmed;

                oSurgeryMaster.pastsurghis = pastsurg;

                oSurgeryMaster.presentingcomp = pcomp;
                oSurgeryMaster.ConsultantName = counsult;
                oSurgeryMaster.SickCategory = cat;



                //////////////////////////



                ////////////////////////////
                var objs = (dynamic)null;
                int objcount = 0;
                Ward_Drug_Prescription[] objDrug = new Ward_Drug_Prescription[1000];
                if (!String.IsNullOrEmpty(suitems))
                {


                    objs = JsonConvert.DeserializeObject<List<Drugreader1>>(suitems);

                    objcount = objs.Count;


                    int i = 0;

                    foreach (Drugreader1 p in objs)
                    {

                        var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.suItemno).Where(d => d.PDID == pdid).Where(d => d.Issued == 0).ToList().Count;
                        if (oldtestcnt < 1)
                        {
                            Ward_Drug_Prescription oDrug_Prescription = new Ward_Drug_Prescription();
                            oDrug_Prescription.PDID = pdid;
                            oDrug_Prescription.Ps_Index = Guid.NewGuid().ToString();
                            oDrug_Prescription.Dose = p.suDose;
                            oDrug_Prescription.Method = p.suMethod;
                            oDrug_Prescription.Route = p.suRoute;
                            oDrug_Prescription.ItemNo = p.suItemno;
                            oDrug_Prescription.GivenBy = "4";
                            oDrug_Prescription.MethodType = Convert.ToInt32(p.suStockTypeID);
                            oDrug_Prescription.Duration = p.suDuration;
                            oDrug_Prescription.RequestedLocID = locid;
                            oDrug_Prescription.LocID = opdid;
                            oDrug_Prescription.Date_Time = DateTime.Now.Date;
                            objDrug[i] = oDrug_Prescription;

                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "INSERT INTO Drug_Prescription(Ps_Index,PDID,ItemNo,Dose,Route,Method,Duration,LocId,Issued,DrugCategory,Date_time,PrescribeBy,GivenBy,RequestedLocId,MethodType)" +
                                       "VALUES('" + oDrug_Prescription.Ps_Index + "','" + oDrug_Prescription.PDID + "','" + oDrug_Prescription.ItemNo + "','" + oDrug_Prescription.Dose + "','" + oDrug_Prescription.Route + "','" + oDrug_Prescription.Method + "','" + oDrug_Prescription.Duration + "', " +
                                        "'" + oDrug_Prescription.LocID + "','" + oDrug_Prescription.Issued + "','" + oDrug_Prescription.DrugCategory + "',GETDATE(),'" + oDrug_Prescription.PrescribeBy + "','" + oDrug_Prescription.GivenBy + "','" + oDrug_Prescription.RequestedLocID + "','" + oDrug_Prescription.MethodType + "')";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            oSqlConnection.Open();
                            oSqlCommand.ExecuteNonQuery();
                            oSqlConnection.Close();

                            i++;
                        }

                    }
                }
                ///////////////////////////
                Patient_Detail patient_Detail = new Patient_Detail();


                patient_Detail.PDID = pdid;
                patient_Detail.PID = PID;
                patient_Detail.OPDID = opdid;
                patient_Detail.Present_Complain = "For Admition";
                patient_Detail.CreatedBy = userid.ToString();
                patient_Detail.PatientCatID = 3;
                patient_Detail.SubjectID = 13;
                //patient_Detail.ModifiedBy = userid.ToString();
                //patient_Detail.ModifiedMachine = userid.ToString();
                patient_Detail.CreatedDate = DateTime.Now;
                //patient_Detail.CreatedMachine = userid.ToString();
                patient_Detail.Status = 2;

                TranferDetail oTranferDetails = new TranferDetail();
                oTranferDetails.PDID = patient_Detail.PDID;
                oTranferDetails.ToLoc = opdid;
                oTranferDetails.FromLoc = opdid;
                oTranferDetails.TransferDate = DateTime.Now;
                //oTranferDetails.TransID = indi.CreateTransID(patient_Detail.PDID);
                oTranferDetails.TransStatus = 1;



                if (ModelState.IsValid)
                {
                    DateTime dd = DateTime.Now.Date;
                    var Abdominalvar = from s in db.Patient_Detail.Where(p => p.PID == PID).Where(p => p.Status == 1).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year) select new { s.PDID };

                    foreach (var item in Abdominalvar)
                    {

                        Abdominal = item.PDID;

                    }
                    if (String.IsNullOrEmpty(Abdominal))
                    {
                        try
                        {

                            //objDrug = objDrug.Where(x => x != null).ToArray();
                            //db.Ward_Drug_Prescription.AddRange(objDrug);


                            //db.TranferDetails.Add(oTranferDetails);
                            //db.ward_discharge.Add(oSurgeryMaster);
                            //db.Patient_Detail.Add(patient_Detail);

                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = " INSERT INTO ward_discharge(PID,PDID,dateadmit,datedischarge,diagnosis,presentingcomp,hispcomp,pastmedhis,pastsurghis,alergies,manageinhosp,dischargeins,followupins,BhtNo,ConsultantName,SickCategory,InvestigationSummary) " +
                                       "VALUES('" + oSurgeryMaster.PID + "','" + oSurgeryMaster.PDID + "','" + oSurgeryMaster.dateadmit + "','" + oSurgeryMaster.datedischarge + "','" + oSurgeryMaster.diagnosis + "','" + oSurgeryMaster.presentingcomp + "','" + oSurgeryMaster.hispcomp + "', " +
                                        "'" + oSurgeryMaster.pastmedhis + "','" + oSurgeryMaster.pastsurghis + "','" + oSurgeryMaster.alergies + "','" + oSurgeryMaster.manageinhosp + "','" + oSurgeryMaster.dischargeins + "','" + oSurgeryMaster.followupins + "','"+ bhtNo + "','"+ oSurgeryMaster.ConsultantName + "','"+ oSurgeryMaster.SickCategory+ "','" + invSummery + "')";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            oSqlConnection.Open();
                            oSqlCommand.ExecuteNonQuery();
                            oSqlConnection.Close();


                            db.SaveChanges();
                            err = "Saved";
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();

                        }
                    }
                    else
                    {
                        long catref = 0;
                        var catrefvar = from s in db.ward_discharge.Where(p => p.PDID == Abdominal) select new { s.WDID };

                        foreach (var item in catrefvar)
                        {

                            catref = item.WDID;
                        }
                        if (catref != 0)
                        {
                            oSurgeryMaster.WDID = catref;
                            oSurgeryMaster.PDID = Abdominal;
                            db.Entry(oSurgeryMaster).State = EntityState.Modified;
                        }
                        else
                        {
                            db.ward_discharge.Add(oSurgeryMaster);
                        }
                        try
                        {
                           
                            objDrug = objDrug.Where(x => x != null).ToArray();
                            db.Ward_Drug_Prescription.AddRange(objDrug);
                            
                            //db.TranferDetails.Add(oTranferDetails);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }
                        err = "Saved!";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(err, JsonRequestBehavior.AllowGet);
        }




        public class pomreader
        {

            public string tpomid { get; set; }
            public string tpomfid { get; set; }
            public string tpomdid { get; set; }
            
        }
        public class matreader
        {

            public string tSMID { get; set; }
            public string ttid { get; set; }
            public string tnopkt { get; set; }

        }
        // GET: SurgeryMasters/Create
        public ActionResult Create(int? page, string id, string currentFilter)
        {
            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string pid = "";
                string PDID = "";
                string Present_Complain = "";
                DateTime? creteddate;
                string fname = "";
                string inililes = "";
                string locdet = "";
                string opddiag = "";
                string pstatus = "";
                string relasiondet = "";
                int? relasiont = 0;
                string rnkname = "";
                string sname = "";
                string sno = "";
                int? sv = 0;

                var result = (dynamic)null;
                var bg = (dynamic)null;
                var onePageOfProducts = (dynamic)null;
                char[] MyChar = { '/', '"', ' ' };
                string opdid = "";
                string locid = "";
                var title = new String[100];
                int specid = 0;

                if (id != null)
                {
                    page = 1;
                    ViewBag.currentFilter = id;
                }
                else
                {
                    id = currentFilter;
                    ViewBag.currentFilter = id;
                }
                int userid = Convert.ToInt32(Session["UserID"]);
                var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID, s.FName, s.LName, s.Salutation };
                opdid = (String)Session["userlocid1"];
                foreach (var item in ser)
                {
                    Session["loginuser"] = item.Salutation + ". " + item.FName + " " + item.LName;

                }
                //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID, s.SpecialityID };

                //foreach (var item in opd)
                //{
                //    specid = item.SpecialityID;

                //}

                //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                //foreach (var item in serW)
                //{

                //    locid = item.LocationID;
                //}

                locid = (String)Session["userloc"];
                if (!String.IsNullOrEmpty(id))
                {
                    id = id.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(id))
                {
                    DateTime dt1 = DateTime.Now.Date;
                    if (opdid.Contains("cl"))
                    {
                        var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id == Convert.ToInt32(specid)).Where(p => p.event_start == dt1) select new { s.title };
                    }
                    DateTime dd = DateTime.Now.Date;
                    var patient_Detailn = from s in db.Patient_Detail.Where(p => p.Patient.ServiceNo.Contains(id)&&p.Present_Complain== "For Surgery")
                                          join d in db.Patients on s.PID equals d.PID
                                          join f in db.RelationshipTypes on d.RelationshipType equals f.RTypeID
                                          orderby s.CreatedDate descending

                                          select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = f.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = f.Relationship };

                    var patient_Detail = from s in db.Patient_Detail.Where(p => p.PDID == "fsdfsf")
                                       
                                         join y in db.Patients on s.PID equals y.PID
                                         join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID

                                         orderby s.CreatedDate descending

                                         select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };
                    List<getdocdetail> lid = patient_Detail.ToList();

                    foreach (var item in patient_Detailn)
                    {
                        pid = item.pidp;
                        PDID = item.pdids;
                        Present_Complain = item.pcomoplian;

                        creteddate = item.crdate;
                        fname = item.fname;
                        inililes = item.inililes;
                        locdet = item.locdet;
                        opddiag = item.opddiag;
                        pstatus = item.pstatus;
                        relasiondet = item.relasiondet;
                        relasiont = item.relasiont;
                        rnkname = item.rnkname;
                        sname = item.sname;
                        sno = item.sno;
                        sv = item.sv;
                        int? stype = 0;
                        string svcno = "";
                        var PersonResultList1 = from s in db.PersonalDetails

                                                where s.ServiceNo == item.sno
                                                select new getdocdetail { sv = sv, pdids = PDID, inililes = s.Initials, sname = s.Surname, sno = s.ServiceNo, rnkname = s.Rank, pcomoplian = Present_Complain, pstatus = pstatus, relasiont = relasiont, crdate = creteddate, pidp = pid, relasiondet = relasiondet };

                        //patient_Detail = patient_Detail.Concat(PersonResultList1);
                        if (PersonResultList1.Count() > 0)
                        {
                            foreach (var iteme in PersonResultList1)
                            {

                                lid.Add(iteme);
                                break;
                            }
                        }





                        else
                        {
                            var patient_Detail2n = from s in db.Patient_Detail.Where(p => p.PDID == item.pdids)
                                                   
                                                   join y in db.Patients on s.PID equals y.PID
                                                   join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID
                                                   select new getdocdetail { sv = s.Patient.Service_Type, pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };
                            //patient_Detail = patient_Detail .Concat(patient_Detail2n);


                            foreach (var iteme in patient_Detail2n)
                            {

                                lid.Add(iteme);
                            }
                        }
                    }

                    var pageNumber = page ?? 1;
                    onePageOfProducts = lid.OrderByDescending(p => p.crdate).ToPagedList(pageNumber, 10);
                }
                else
                {
                    if (opdid.Contains("CL"))
                    {
                        DateTime dt1 = DateTime.Now.Date;
                        var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id == specid).Where(p => p.event_start.Value.Day == dt1.Day && p.event_start.Value.Month == dt1.Month && p.event_start.Value.Year == dt1.Year) select new { s.title };
                        int r = 0;
                        foreach (var item in shed)
                        {

                            title[r] = item.title;
                            r++;
                        }


                        var patient_Detailn = from s in db.Patient_Detail.Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 2 || p.Status == 7 || p.Status == 5).Where(p => p.OPDID == opdid)
                                              
                                              join y in db.Patients on s.PID equals y.PID
                                              join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID
                                              orderby s.CreatedDate descending select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, opddiag = s.OPD_Diagnosis, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };

                        DateTime dd = DateTime.Now.Date;
                        var patient_Detail = from s in db.Patient_Detail.Where(p => p.PDID == "dfdffddf")
                                             
                                             join y in db.Patients on s.PID equals y.PID
                                             join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID

                                             orderby s.CreatedDate descending select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, opddiag = s.OPD_Diagnosis, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };

                        List<getdocdetail> lid = patient_Detail.ToList();

                        foreach (var item in patient_Detailn)
                        {
                            pid = item.pidp;
                            PDID = item.pdids;
                            Present_Complain = item.pcomoplian;

                            creteddate = item.crdate;
                            fname = item.fname;
                            inililes = item.inililes;
                            locdet = item.locdet;
                            opddiag = item.opddiag;
                            pstatus = item.pstatus;
                            relasiondet = item.relasiondet;
                            relasiont = item.relasiont;
                            rnkname = item.rnkname;
                            sname = item.sname;
                            sno = item.sno;
                            sv = item.sv;
                            int? stype = 0;
                            string svcno = "";
                            var PersonResultList1 = from s in db.PersonalDetails

                                                    where s.ServiceNo == item.sno
                                                    select new getdocdetail { sv = sv, pdids = PDID, inililes = s.Initials, sname = s.Surname, sno = s.ServiceNo, rnkname = s.Rank, pcomoplian = Present_Complain, pstatus = pstatus, relasiont = relasiont, crdate = creteddate, pidp = pid, relasiondet = relasiondet };

                            //patient_Detail = patient_Detail.Concat(PersonResultList1);
                            if (PersonResultList1.Count() > 0)
                            {
                                foreach (var iteme in PersonResultList1)
                                {

                                    lid.Add(iteme);
                                    break;
                                }
                            }





                            else
                            {
                                var patient_Detail2n = from s in db.Patient_Detail.Where(p => p.PDID == item.pdids)
                                                       
                                                       join y in db.Patients on s.PID equals y.PID
                                                       join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID

                                                       select new getdocdetail { sv = s.Patient.Service_Type, pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };
                                //patient_Detail = patient_Detail .Concat(patient_Detail2n);


                                foreach (var iteme in patient_Detail2n)
                                {

                                    lid.Add(iteme);
                                }
                            }
                        }


                        //db.Patient_Detail.Include(p => p.Patient).Where (p=>title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7 || p.Status == 5).Where(p => p.OPDID == opdid).OrderByDescending(p => p.CreatedDate);
                        var pageNumber = page ?? 1;
                        onePageOfProducts = lid.OrderByDescending(p => p.crdate).ToPagedList(pageNumber, 10);

                    }
                    else
                    {
                        DateTime dd = DateTime.Now.Date;

                        //var df=      from s in db.Patient_Detail.Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate)
                        //      //       join b in db.Patients on s.PID equals b.PID into cs
                        //      //from b in cs.DefaultIfEmpty()

                        //      select new { s.PDID, s.Patient.Initials, s.Patient.Surname, s.Present_Complain, s.Patient.rank1.RNK_NAME, s.CreatedDate, s.OPD_Diagnosis, s.OPDID,s.Status1.StatusDec,s.Patient.PID };

                        var patient_Detailn = from s in db.Patient_Detail.Where(p => p.PatientCatID == 3).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year)

                                           
                                              join y in db.Patients on s.PID equals y.PID
                                              join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID


                                              //  orderby s.CreatedDate descending 
                                              select new getdocdetail { sv = s.Patient.Service_Type, pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };



                        var patient_Detail = from s in db.Patient_Detail.Where(p => p.PDID == "fdfdffdfd")

                                            
                                             join y in db.Patients on s.PID equals y.PID
                                             join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID


                                             //  orderby s.CreatedDate descending 
                                             select new getdocdetail { sv = s.Patient.Service_Type, pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };
                        List<getdocdetail> lid = patient_Detail.ToList();

                        foreach (var item in patient_Detailn)
                        {
                            pid = item.pidp;
                            PDID = item.pdids;
                            Present_Complain = item.pcomoplian;

                            creteddate = item.crdate;
                            fname = item.fname;
                            inililes = item.inililes;
                            locdet = item.locdet;
                            opddiag = item.opddiag;
                            pstatus = item.pstatus;
                            relasiondet = item.relasiondet;
                            relasiont = item.relasiont;
                            rnkname = item.rnkname;
                            sname = item.sname;
                            sno = item.sno;
                            sv = item.sv;
                            int? stype = 0;
                            string svcno = "";

                            var PersonResultList1 = from s in db.PersonalDetails

                                                    where s.ServiceNo == item.sno
                                                    select new getdocdetail { sv = sv, pdids = PDID, inililes = s.Initials, sname = s.Surname, sno = s.ServiceNo, rnkname = s.Rank, pcomoplian = Present_Complain, pstatus = pstatus, relasiont = relasiont, crdate = creteddate, pidp = pid, relasiondet = relasiondet };

                            //patient_Detail = patient_Detail.Concat(PersonResultList1);
                            if (PersonResultList1.Count() > 0)
                            {
                                foreach (var iteme in PersonResultList1)
                                {

                                    lid.Add(iteme);
                                    break;
                                }
                            }





                            else
                            {
                                var patient_Detail2n = from s in db.Patient_Detail.Where(p => p.PDID == item.pdids)
                                                      
                                                       join y in db.Patients on s.PID equals y.PID
                                                       join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID

                                                       select new getdocdetail { sv = s.Patient.Service_Type, pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };
                                //patient_Detail = patient_Detail .Concat(patient_Detail2n);


                                foreach (var iteme in patient_Detail2n)
                                {

                                    lid.Add(iteme);
                                }
                            }
                        }



                        ///db.Patient_Detail.Include(p => p.Patient).Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                        //  patient_Detail = patient_Detail.GroupBy(t => t.pdids).Select(grp => grp.FirstOrDefault()).OrderByDescending(s=>s.crdate);
                        var pageNumber = page ?? 1;

                        onePageOfProducts = lid.OrderByDescending(p => p.crdate).ToPagedList(pageNumber, 10);
                    }


                }
                //var pageNumber = page ?? 1;
                // var onePageOfProducts = pOR_Header.ToPagedList(pageNumber, 10);

                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
                //throw;
            }
        }

        public ActionResult  dcard(int? page, string id, string currentFilter)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string pid = "";
                string PDID = "";
                string Present_Complain = "";
                DateTime? creteddate;
                string fname = "";
                string inililes = "";
                string locdet = "";
                string opddiag = "";
                string pstatus = "";
                string relasiondet = "";
                int? relasiont = 0;
                string rnkname = "";
                string sname = "";
                string sno = "";
                int? sv = 0;

                var result = (dynamic)null;
                var bg = (dynamic)null;
                var onePageOfProducts = (dynamic)null;
                char[] MyChar = { '/', '"', ' ' };
                string opdid = "";
                string locid = "";
                var title = new String[100];
                int specid = 0;

                if (id != null)
                {
                    page = 1;
                    ViewBag.currentFilter = id;
                }
                else
                {
                    id = currentFilter;
                    ViewBag.currentFilter = id;
                }
                int userid = Convert.ToInt32(Session["UserID"]);
                var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID, s.FName, s.LName, s.Salutation };
                opdid = (String)Session["userlocid1"];
                foreach (var item in ser)
                {
                    Session["loginuser"] = item.Salutation + ". " + item.FName + " " + item.LName;

                }
                //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID, s.SpecialityID };

                //foreach (var item in opd)
                //{
                //    specid = item.SpecialityID;

                //}

                //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                //foreach (var item in serW)
                //{

                //    locid = item.LocationID;
                //}

                locid = (String)Session["userloc"];
                if (!String.IsNullOrEmpty(id))
                {
                    id = id.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(id))
                {
                    DateTime dt1 = DateTime.Now.Date;
                    //if (opdid.Contains("cl"))
                    //{
                    //    var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id == Convert.ToInt32(specid)).Where(p => p.event_start == dt1) select new { s.title };
                    //}
                    DataTable oDataSet3 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "   SELECT max(a.Present_Complain)pcomoplian, COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1  " +
"  and b.Surname != '0' " +
" then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),    " +
"  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
"   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
"   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname))  " +
"	sname  ,max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
"	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
"	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(sc.CatId)  pstatus,max(a.CreatedDate) crdate, max(h.Relationship) " +

"     relasiondet FROM[MMS].[dbo].[Patient_Detail] as a with(nolock) inner join [dbo].Ward_Details wd ON wd.PDID = a.PDID   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
"  left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
"  left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
"   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
"   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" LEFT JOIN Sick_Category sc ON a.pdid = sc.PDID  " +
"left join CatDiagList cdl ON a.PDID = cdl.PDID " +
"left join CatDaignosis cds ON cdl.dgid = cds.dgid " +
"left join ward_details WDD ON a.PDID = WDD.PDID  " +
"left join Ward_Types WT ON  WD.Ward_No=WT.Id " +
" where   c.ServiceNo like'%" + id + "%' AND wd.status= 16 and a.PatientCatID!=2" +
"  group by a.PDID, a.CreatedDate order by crdate desc";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet3);
                    oSqlConnection.Close();
                    var lid = oDataSet3.AsEnumerable()
            .Select(dataRow => new getdocdetail
            {
                pdids = dataRow.Field<string>("pdids"),
                inililes = dataRow.Field<string>("inililes"),
                sname = dataRow.Field<string>("sname"),
                sno = dataRow.Field<string>("sno"),
                rnkname = dataRow.Field<string>("rnkname"),
                pcomoplian = dataRow.Field<string>("pcomoplian"),
                pstatus = dataRow.Field<int?>("pstatus").ToString(),
                relasiont = dataRow.Field<int?>("relasiont"),
                crdate = dataRow.Field<DateTime?>("crdate"),
                pidp = dataRow.Field<string>("pidp"),
                relasiondet = dataRow.Field<string>("relasiondet"),
            }).ToList();

                    var pageNumber = page ?? 1;
                    onePageOfProducts = lid.ToList();
                }
                else
                {
                    DataTable oDataSet3 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "   SELECT top 100 max(a.Present_Complain)pcomoplian, COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1  " +
"  and b.Surname != '0' " +
" then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),    " +
"  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
"   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
"   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname))  " +
"	sname  ,max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
"	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
"	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(sc.CatId)  pstatus,max(a.CreatedDate) crdate, max(h.Relationship) " +

"     relasiondet FROM[MMS].[dbo].[Patient_Detail] as a with(nolock) inner join [dbo].Ward_Details wd ON wd.PDID = a.PDID   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
"  left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
"  left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
"   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
"   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" LEFT JOIN Sick_Category sc ON a.pdid = sc.PDID  " +
"left join CatDiagList cdl ON a.PDID = cdl.PDID " +
"left join CatDaignosis cds ON cdl.dgid = cds.dgid " +

"left join Ward_Types WT ON  WD.Ward_No=WT.Id " +
" where   wd.status= 16 and a.PatientCatID!=2 " +
" and wd.opdid='" + opdid + "' group by a.PDID, a.CreatedDate order by crdate desc";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet3);
                    oSqlConnection.Close();
                    var lid = oDataSet3.AsEnumerable()
            .Select(dataRow => new getdocdetail
            {
                pdids = dataRow.Field<string>("pdids"),
                inililes = dataRow.Field<string>("inililes"),
                sname = dataRow.Field<string>("sname"),
                sno = dataRow.Field<string>("sno"),
                rnkname = dataRow.Field<string>("rnkname"),
                pcomoplian = dataRow.Field<string>("pcomoplian"),
                pstatus = dataRow.Field<int?>("pstatus").ToString(),
                relasiont = dataRow.Field<int?>("relasiont"),
                crdate = dataRow.Field<DateTime?>("crdate"),
                pidp = dataRow.Field<string>("pidp"),
                relasiondet = dataRow.Field<string>("relasiondet"),
            }).ToList();

                    onePageOfProducts = lid.ToList();
                    
                }

                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
                //throw;
            }

        }

        public JsonResult GetsurgatientDischarj(string id,string id2)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string opdid = "";
                string locid = "";
                int userid = Convert.ToInt32(Session["UserID"]);

                //var serW = from s in db.Clinic_Master.Where(p => p.ClinicTypeID == userid) select new { s.LocationID };

                //foreach (var item in serW)
                //{

                //    locid = item.LocationID;
                //}
                //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };

                //foreach (var item in opd)
                //{

                //    opdid = item.LOCID;
                //}
                opdid = (String)Session["userlocid1"];
                if (opdid.Contains(""))
                {

                }
                var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                foreach (var item in serW)
                {

                    locid = item.LocationID;
                }
                string mgtPlan = "";
                string catPlan = "";
                string alergy = "";
                string diagnosis = "";
                string iserror = "";
                string pid = "";
                string PDID = "";
                string tt = "";
                string nop = "";
                string toa = "";
                string dop = "";
                string ind = "";
                string attsrg = "";
                string aby = "";
                string ant = "";
                string moa = "";
                string antipr = "";
                string pos = "";
                string dos = "";
                string incs = "";
                string prced = "";
                string drins = "";
                string bldlss = "";
                string clmd = "";
                string smu = "";
                string moins = "";
                string nutri = "";
                string medict = "";
                string spins = "";

                string Present_Complain = "";
                string History_OtherComplain = "";
                string History_PresentComplain = "";
                string Other_Complain = "";
                string Category = "";
                string BloodType = "";
                string OPD_Diagnosis = "";
                string BHTNo = "";
                string Relationship = "";
                string AdmitDate ;
                string DisDate;
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                var result = (dynamic)null;
                var bg = (dynamic)null;
                var medbd = (dynamic)null;

                DataTable oDataSetv4 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "select mp.Date,mp.Description  " +
                           "from Patient_Detail pd " +
                           "inner join Ward_Details wd on pd.PDID= wd.PDID " +
                           "inner join Ward_Mgt_Plan mp on wd.WDID = mp.PDID " +
                           "where pd.pdid='" + NewString + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv4);
                // oSqlConnection.Close();
                var a6 = oDataSetv4.AsEnumerable()
        .Select(dataRow => new getexamin
        {
            Description = dataRow.Field<string>("Description"),

        }).ToList();

                foreach(var itemMgt in a6)
                {
                    mgtPlan = mgtPlan+""+itemMgt.Description+",";
                }

                DataTable oDataSetv10 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "select ST.Category_type,SC.CatPeriod,SC.Date " +
                           "from Patient_Detail pd "+
                           "inner join Sick_Category sc on pd.PDID= sc.PDID " +
                           "inner join Sick_CategoryType st ON sc.CatId = st.CatId " +
                           "where pd.pdid='" + NewString + "' ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv10);
                // oSqlConnection.Close();
                var a10 = oDataSetv10.AsEnumerable()
        .Select(dataRow => new getexamin
        {
            sickCategory = dataRow.Field<string>("Category_type"),
            catPeriod = dataRow.Field<string>("CatPeriod"),
            catDate = dataRow.Field<DateTime>("Date"),

        }).ToList();

                foreach (var catMgt in a10)
                {
                    catPlan = catPlan + "" + catMgt.sickCategory + "-"+ catMgt.catPeriod+"-"+catMgt .catDate+"\n";
                }
                //////////////////////////////////////////////
                var joined3dd = new List<druglist>();
                DataTable oDataSetvdd = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "   SELECT a.[issuedQuantity],a.[Date_Time],COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'') as itmdes,b.MethodDetail " +
" ,a.Ps_Index,b.DrugMethodCount ,c.RouteDetail ,a.Duration,a.MethodType,a.Dose,a.pdid,a.Issued " +
"  FROM[MMS].[dbo].[Drug_Prescription] as a " +
" left join[MMS].[dbo].[DrugMethod] as b on a.Method=b.MethodID left join[MMS].[dbo].[DrugRoute] as c on a.route=c.routeid " +
" left join[MMS].[dbo].[DrugItems] as d on a.ItemNo=Convert(varchar, d.DrugID) " +
"    left join[MMS].[dbo].[EPASPharmacyItems] as e on a.ItemNo=Convert(varchar, e.[itemno]) where " +
" a.[PDID]='" + NewString + "'    " +
" order by a.PDID";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetvdd);
                // oSqlConnection.Close();
                try
                {
                    joined3dd = oDataSetvdd.AsEnumerable()
       .Select(dataRow => new druglist
       {

           issuedQuantity = dataRow.Field<string>("issuedQuantity"),
           Date_Time = dataRow.Field<DateTime?>("Date_Time"),
           itemdescription = dataRow.Field<string>("itmdes"),
           MethodDetail = dataRow.Field<string>("MethodDetail"),
           Ps_Index = dataRow.Field<string>("Ps_Index"),
           DrugMethodCount = dataRow.Field<decimal?>("DrugMethodCount"),
           RouteDetail = dataRow.Field<string>("RouteDetail"),
           Duration = dataRow.Field<string>("Duration"),
           MethodType = dataRow.Field<int?>("MethodType"),
           Dose = dataRow.Field<string>("Dose"),
           pdid = dataRow.Field<string>("pdid"),
           Issued = dataRow.Field<int?>("Issued").ToString(),
       }).ToList();
                }
                catch (Exception ex)
                {

                }



                //////////////////////////////////////////////////
                DataTable oDataSetv8 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "select mc.HypersenceMainCategory,ht.HyperSubType  " +
                           "from Hypersensivity al " +
                           "inner join Patient_Detail pd on al.PID = pd.PID " +
                           "inner join HypersensivityType ht on al.HyperTypeSubID = ht.HyperTypeID " +
                           "inner join HypMainCategory mc on ht.HyperType = mc.HypersenceMainCatID " +
                           "where pd.pdid='" + NewString + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv4);
                // oSqlConnection.Close();
                var a8 = oDataSetv4.AsEnumerable()
        .Select(dataRow => new getexamin
        {
            HypersenceMainCategory = dataRow.Field<string>("HypersenceMainCategory"),
            HyperSubType = dataRow.Field<string>("HyperSubType"),

        }).ToList();

                foreach (var itemal in a8)
                {
                    alergy = alergy + "" + itemal.HypersenceMainCategory + "-"+ itemal.HyperSubType + ",";
                }

                var a1 = from b in db.Ward_Details.Where(p => p.PDID == NewString && p.Status == 16)
                         join s in db.Patient_Detail on b.PDID equals s.PDID
                        join c in db.CatDiagLists on s.PDID equals c.PDID  into cs from c in cs .DefaultIfEmpty()
                         join d in db.CatDaignosis on c.dgid equals d.dgid into ds
                         from d in ds.DefaultIfEmpty()

                         join y in db.Patients on s.PID equals y.PID
                         join z in db.PersonalDetails on y.ServiceNo equals z.ServiceNo
                        
                         join x in db.MedicalCategories on z.SNo equals x.SNo

                         join w in db.RelationshipTypes on y.RelationshipType equals w.RTypeID

                        

                         select new getdocdata {BHTNo = b.BHTNo ,DisDate = b.Modify_Date.ToString() ,AdmitDate = b.Created_Date.ToString(), PID = s.PID, PDID = s.PDID, Initials = s.Patient.Initials, Surname = s.Patient.Surname, ServiceNo = s.Patient.ServiceNo, Present_Complain = s.Present_Complain, RNK_NAME = s.Patient.rank1.RNK_NAME, History_OtherComplain = s.Examination, History_PresentComplain = s.History_PresentComplain, Other_Complain = s.Other_Complain, Category = x.MedicalCategory1, BloodType = z.BloodGroup, OPD_Diagnosis = d.dgdetail, Relationship = w.Relationship, sv = s.Patient.Service_Type };


                foreach (var item in a1)
                {
                    string svcno = "";
                    DisDate = DateTime.Now.ToString();
                    AdmitDate =item.AdmitDate.ToString();
                    pid = item.PID;
                    PDID = item.PDID;
                    Present_Complain = item.Present_Complain;
                    History_OtherComplain = item.History_OtherComplain;
                    History_PresentComplain = item.History_PresentComplain;
                    Other_Complain = item.Other_Complain;
                    Category = item.Category;
                    BloodType = item.BloodType;
                    OPD_Diagnosis = item.OPD_Diagnosis;
                    Relationship = item.Relationship;
                    svcno = item.ServiceNo;
                    BHTNo = item.BHTNo;
                    int? stype = 0;
                    
                    if (item.Relationship == "SELF" && item.sv != 3)
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.MedicalCategories on s.SNo equals b.SNo
                                                into sc
                                                from b in sc.DefaultIfEmpty()
                                                where s.ServiceNo == item.ServiceNo
                                                select new getdocdata {BHTNo = BHTNo, DisDate = DisDate, AdmitDate = AdmitDate, Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, Present_Complain = Present_Complain, RNK_NAME = s.Rank, History_OtherComplain = History_OtherComplain, History_PresentComplain = History_PresentComplain, Other_Complain = Other_Complain, Category = b.MedicalCategory1, BloodType = s.BloodGroup, OPD_Diagnosis = OPD_Diagnosis, Relationship = Relationship };

                        if (PersonResultList1.Count() > 0)
                        {
                            a1 = PersonResultList1;
                        }
                        else
                        {
                            iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    if (item.Relationship.ToLower() == "spouse")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.SpouseDetails on s.SNo equals b.SNo
                                                where s.ServiceNo == item.ServiceNo
                                                select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.SpouseName, ServiceNo = s.ServiceNo, Present_Complain = Present_Complain, RNK_NAME = s.Rank, History_OtherComplain = History_OtherComplain, History_PresentComplain = History_PresentComplain, Other_Complain = Other_Complain, Category = "", BloodType = BloodType, OPD_Diagnosis = OPD_Diagnosis, Relationship = Relationship };

                        if (PersonResultList1.Count() > 0)
                        {
                            a1 = PersonResultList1;
                        }
                        else
                        {
                            iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    if (item.Relationship.ToLower() == "father")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo
                                                where s.ServiceNo == item.ServiceNo && b.Relationship == "Father"
                                                select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.ParentName, ServiceNo = s.ServiceNo, Present_Complain = Present_Complain, RNK_NAME = s.Rank, History_OtherComplain = History_OtherComplain, History_PresentComplain = History_PresentComplain, Other_Complain = Other_Complain, Category = "", BloodType = BloodType, OPD_Diagnosis = OPD_Diagnosis, Relationship = Relationship };

                        if (PersonResultList1.Count() > 0)
                        {
                            a1 = PersonResultList1;
                        }
                        else
                        {
                            iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    if (item.Relationship.ToLower() == "mother")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo

                                                where s.ServiceNo == item.ServiceNo && b.Relationship == "Mother"
                                                select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.ParentName, ServiceNo = s.ServiceNo, Present_Complain = Present_Complain, RNK_NAME = s.Rank, History_OtherComplain = History_OtherComplain, History_PresentComplain = History_PresentComplain, Other_Complain = Other_Complain, Category = "", BloodType = BloodType, OPD_Diagnosis = OPD_Diagnosis, Relationship = Relationship };
                        if (PersonResultList1.Count() > 0)
                        {
                            a1 = PersonResultList1;
                        }
                        else
                        {
                            iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    try
                    {
                        var a5 = from s in db.SurgeryMasters.Where(p => p.PDID == NewString)
                             join b in db.SurgeryTypes on s.AnesthesiaType equals b.TAID into cs
                             from b in cs.DefaultIfEmpty()
                             join c in db.SurgeryNutritions on s.Nutritionid equals c.NID into cs1
                             from c in cs1.DefaultIfEmpty()
                             join d in db.SurgeryNProcedures on s.ProcedureName equals d.nprid into cs2
                             from d in cs2.DefaultIfEmpty()


                             select new { s.PID, s.TheaterTable, s.ProcedureName, b.TADescription, s.Indication, s.Surgeon, s.AssistedBy, s.Anesthetist, s.AntibioticP, s.PrcedureDetail, s.DrainsInserted, s.MonitoringInstruct, s.Nutrition, s.SpecialIntruct };
                    var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                    var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
                    var serc = from s in db.Drug_Prescription.Where(p => p.PDID == NewString && p.issuedQuantity != "0" && p.DrugCategory == 2) orderby s.ItemNo select new { s.Ps_Index, s.ItemNo, s.DrugMethod.MethodDetail, s.DrugMethod.DrugMethodCount, s.DrugRoute.RouteDetail, s.Duration, s.Dose, s.PDID, s.MethodType };
                    int ij = 0;

                    foreach (var itm in serc)
                    {


                        var items2 = from d in db.EPASPharmacyItems.Where(p => p.itemno == itm.ItemNo) select new { d.itemdescription, d.itemno };
                        if (items2.Count() > 0)
                        {
                            if (ij != 0)
                            {
                                items = items2.Concat(items);

                            }
                            else
                            {

                                items = items2;
                            }
                            ij++;
                        }
                        else
                        {
                            var items3 = from d in db.DrugItems.Where(p => p.DrugID.ToString() == itm.ItemNo) select new { d.ItemDescription, d.DrugID };
                            if (ij != 0)
                            {
                                items5 = items3.Concat(items5);

                            }
                            else
                            {

                                items5 = items3;
                            }
                            ij++;
                        }


                    }
                  
                        DataTable oDataSetv18 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "SELECT TOP(1) CASE WHEN WN.dgid = 0 THEN WD.Diagnosis ELSE CD.dgdetail END AS Diagnosis " +
                                   "FROM ward_Details WD  " +
                                   "LEFT JOIN Ward_Daignosis WN ON WD.PDID = WN.PDID " +
                                   "INNER JOIN CatDaignosis CD ON WN.dgid = cd.dgid " +
                                   "WHERE WD.PDID = '" + NewString + "' " +
                                   "ORDER BY WN.CatDiagID DESC ";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlCommand.CommandTimeout = 120;
                        //   oSqlConnection.Open();
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSetv18);
                        // oSqlConnection.Close();
                        var a18 = (oDataSetv18).AsEnumerable()
                .Select(dataRow => new getexamin
                {
                    diagnosis = dataRow.Field<string>("Diagnosis"),
                }).ToList();

                        foreach (var itm in a18)
                        {
                            diagnosis = itm.diagnosis;
                        }

                        var t1 = serc.ToList();
                        var t2 = items.ToList();
                        var t3 = items5.ToList();
                        var joined = from it1 in t1 join it2 in t2 on it1.ItemNo equals it2.itemno select new { Ps_Index = it1.Ps_Index, ItemNo = it1.ItemNo, MethodDetail = it1.MethodDetail, mcnt = it1.DrugMethodCount, RouteDetail = it1.RouteDetail, Dose = it1.Dose, Duration = it1.Duration, itemdescription = it2.itemdescription, PDID = it1.PDID, mt = it1.MethodType };

                        var joined1 = from it1 in t1 join it2 in t3 on it1.ItemNo equals it2.DrugID.ToString() select new { Ps_Index = it1.Ps_Index, ItemNo = it1.ItemNo, MethodDetail = it1.MethodDetail, mcnt = it1.DrugMethodCount, RouteDetail = it1.RouteDetail, Dose = it1.Dose, Duration = it1.Duration, itemdescription = it2.ItemDescription, PDID = it1.PDID, mt = it1.MethodType };
                        var u1 = joined.ToList();
                        var u2 = joined1.ToList();
                        var joined3 = u1.Concat(u2);


                        result = new { s1 = a1.ToList(), l1 = a5.ToList(), d1 = joined3.ToList(), err = iserror, k1 = mgtPlan, m1 = alergy, c1 = catPlan, da = diagnosis,dd=joined3dd };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                var result4 = new { s1 = "", l1 = "", err = "2" };
                return Json(result4, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var result3 = new { s1 = "", l1 = "", d1 = "", b1 = "", b2 = "", b4 = "", b5 = "", vitval1 = "", err = "2",a6="" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }



        }

        public JsonResult Getsurgatient(string id)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string opdid = "";
                string locid = "";
                int userid = Convert.ToInt32(Session["UserID"]);

                //var serW = from s in db.Clinic_Master.Where(p => p.ClinicTypeID == userid) select new { s.LocationID };

                //foreach (var item in serW)
                //{

                //    locid = item.LocationID;
                //}
                //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };

                //foreach (var item in opd)
                //{

                //    opdid = item.LOCID;
                //}
                opdid = (String)Session["userlocid1"];
                if (opdid.Contains(""))
                {

                }
                var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                foreach (var item in serW)
                {

                    locid = item.LocationID;
                }

                string iserror = "";
                string pid = "";
                string PDID = "";
                string tt = "";
                string nop = "";
                string toa = "";
                string dop = "";
                string ind = "";
                string attsrg = "";
                string aby = "";
                string ant = "";
                string moa = "";
                string antipr = "";
                string pos = "";
                string dos = "";
                string incs = "";
                string prced = "";
                string drins = "";
                string bldlss = "";
                string clmd = "";
                string smu = "";
                string moins = "";
                string nutri = "";
                string medict = "";
                string spins = "";

                string Present_Complain = "";
                string History_OtherComplain = "";
                string History_PresentComplain = "";
                string Other_Complain = "";
                string Category = "";
                string BloodType = "";
                string OPD_Diagnosis = "";
                string Relationship = "";
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                var result = (dynamic)null;
                var bg = (dynamic)null;
                var medbd = (dynamic)null;
                var a1 = from s in db.Patient_Detail.Where(p => p.PDID == NewString)

                         join z in db.Patients on s.PID equals z.PID

                       
                         join x in db.PersonalDetails on z.ServiceNo equals x.ServiceNo

                         join y in db.MedicalCategories on x.SNo equals y.SNo

                        
                         join w in db.RelationshipTypes on z.RelationshipType equals w.RTypeID

                         select new getdocdata { PID = s.PID, PDID = s.PDID, Initials = s.Patient.Initials, Surname = s.Patient.Surname, ServiceNo = s.Patient.ServiceNo, Present_Complain = s.Present_Complain, RNK_NAME = s.Patient.rank1.RNK_NAME, History_OtherComplain = s.Examination, History_PresentComplain = s.History_PresentComplain, Other_Complain = s.Other_Complain, Category = y.MedicalCategory1, BloodType = x.BloodGroup, OPD_Diagnosis = s.OPD_Diagnosis, Relationship = w.Relationship, sv = s.Patient.Service_Type };


                foreach (var item in a1)
                {
                    pid = item.PID;
                    PDID = item.PDID;
                    Present_Complain = item.Present_Complain;
                    History_OtherComplain = item.History_OtherComplain;
                    History_PresentComplain = item.History_PresentComplain;
                    Other_Complain = item.Other_Complain;
                    Category = item.Category;
                    BloodType = item.BloodType;
                    OPD_Diagnosis = item.OPD_Diagnosis;
                    Relationship = item.Relationship;
                    int? stype = 0;
                    string svcno = "";
                    if (item.Relationship == "SELF" && item.sv != 3)
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.MedicalCategories on s.SNo equals b.SNo
                                                into sc
                                                from b in sc.DefaultIfEmpty()
                                                where s.ServiceNo == item.ServiceNo
                                                select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, Present_Complain = Present_Complain, RNK_NAME = s.Rank, History_OtherComplain = History_OtherComplain, History_PresentComplain = History_PresentComplain, Other_Complain = Other_Complain, Category = b.MedicalCategory1, BloodType = s.BloodGroup, OPD_Diagnosis = OPD_Diagnosis, Relationship = Relationship };

                        if (PersonResultList1.Count() > 0)
                        {
                            a1 = PersonResultList1;
                        }
                        else
                        {
                            iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    if (item.Relationship.ToLower() == "spouse")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.SpouseDetails on s.SNo equals b.SNo
                                                where s.ServiceNo == item.ServiceNo
                                                select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.SpouseName, ServiceNo = s.ServiceNo, Present_Complain = Present_Complain, RNK_NAME = s.Rank, History_OtherComplain = History_OtherComplain, History_PresentComplain = History_PresentComplain, Other_Complain = Other_Complain, Category = "", BloodType = BloodType, OPD_Diagnosis = OPD_Diagnosis, Relationship = Relationship };

                        if (PersonResultList1.Count() > 0)
                        {
                            a1 = PersonResultList1;
                        }
                        else
                        {
                            iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    if (item.Relationship.ToLower() == "father")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo
                                                where s.ServiceNo == item.ServiceNo && b.Relationship == "Father"
                                                select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.ParentName, ServiceNo = s.ServiceNo, Present_Complain = Present_Complain, RNK_NAME = s.Rank, History_OtherComplain = History_OtherComplain, History_PresentComplain = History_PresentComplain, Other_Complain = Other_Complain, Category = "", BloodType = BloodType, OPD_Diagnosis = OPD_Diagnosis, Relationship = Relationship };

                        if (PersonResultList1.Count() > 0)
                        {
                            a1 = PersonResultList1;
                        }
                        else
                        {
                            iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    if (item.Relationship.ToLower() == "mother")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo

                                                where s.ServiceNo == item.ServiceNo && b.Relationship == "Mother"
                                                select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.ParentName, ServiceNo = s.ServiceNo, Present_Complain = Present_Complain, RNK_NAME = s.Rank, History_OtherComplain = History_OtherComplain, History_PresentComplain = History_PresentComplain, Other_Complain = Other_Complain, Category = "", BloodType = BloodType, OPD_Diagnosis = OPD_Diagnosis, Relationship = Relationship };
                        if (PersonResultList1.Count() > 0)
                        {
                            a1 = PersonResultList1;
                        }
                        else
                        {
                            iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    var a5 = from s in db.SurgeryMasters.Where(p => p.PDID == NewString)
                             join b in db.SurgeryTypes on s.AnesthesiaType equals b.TAID into cs
                             from b in cs.DefaultIfEmpty()
                             join c in db.SurgeryNutritions on s.Nutritionid equals c.NID into cs1
                             from c in cs1.DefaultIfEmpty()
                             join d in db.SurgeryNProcedures on s.ProcedureName equals d.nprid into cs2
                             from d in cs2.DefaultIfEmpty()
                             

                             select new {s.PID, s.TheaterTable, s.ProcedureName, b.TADescription,  s.Indication, s.Surgeon,s.AssistedBy, s.Anesthetist, s.AntibioticP,   s.PrcedureDetail, s.DrainsInserted,   s.MonitoringInstruct, s.Nutrition, s.SpecialIntruct };
                    var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                    var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
                    var serc = from s in db.Drug_Prescription.Where(p => p.PDID == NewString && p.issuedQuantity != "0"&&p.DrugCategory==2) orderby s.ItemNo select new { s.Ps_Index, s.ItemNo, s.DrugMethod.MethodDetail, s.DrugMethod.DrugMethodCount, s.DrugRoute.RouteDetail, s.Duration, s.Dose, s.PDID, s.MethodType };
                    int ij = 0;

                    foreach (var itm in serc)
                    {


                        var items2 = from d in db.EPASPharmacyItems.Where(p => p.itemno == itm.ItemNo) select new { d.itemdescription, d.itemno };
                        if (items2.Count() > 0)
                        {
                            if (ij != 0)
                            {
                                items = items2.Concat(items);

                            }
                            else
                            {

                                items = items2;
                            }
                            ij++;
                        }
                        else
                        {
                            var items3 = from d in db.DrugItems.Where(p => p.DrugID.ToString() == itm.ItemNo) select new { d.ItemDescription, d.DrugID };
                            if (ij != 0)
                            {
                                items5 = items3.Concat(items5);

                            }
                            else
                            {

                                items5 = items3;
                            }
                            ij++;
                        }


                    }
                    var t1 = serc.ToList();
                    var t2 = items.ToList();
                    var t3 = items5.ToList();
                    var joined = from it1 in t1 join it2 in t2 on it1.ItemNo equals it2.itemno select new { Ps_Index = it1.Ps_Index, ItemNo = it1.ItemNo, MethodDetail = it1.MethodDetail, mcnt = it1.DrugMethodCount, RouteDetail = it1.RouteDetail, Dose = it1.Dose, Duration = it1.Duration, itemdescription = it2.itemdescription, PDID = it1.PDID, mt = it1.MethodType };

                    var joined1 = from it1 in t1 join it2 in t3 on it1.ItemNo equals it2.DrugID.ToString() select new { Ps_Index = it1.Ps_Index, ItemNo = it1.ItemNo, MethodDetail = it1.MethodDetail, mcnt = it1.DrugMethodCount, RouteDetail = it1.RouteDetail, Dose = it1.Dose, Duration = it1.Duration, itemdescription = it2.ItemDescription, PDID = it1.PDID, mt = it1.MethodType };
                    var u1 = joined.ToList();
                    var u2 = joined1.ToList();
                    var joined3 = u1.Concat(u2);


                    result = new { s1 = a1.ToList(), l1 = a5.ToList(),d1= joined3.ToList(), err = iserror };
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                var result4 = new { s1 = "", l1 = "", err = "2" };
                return Json(result4, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var result3 = new { s1 = "", l1 = "", d1 = "", b1 = "", b2 = "", b4 = "", b5 = "", vitval1 = "", err = "2" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }



        }
        // POST: SurgeryMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SGID,PID,PDID,TheaterTable,ProcedureName,AnesthesiaType,DateodProcedure,Indication,Surgeon,AssistedBy,Anesthetist,MOAnesthesia,AntibioticP,Positioning,DurationofSurgery,Incision,PrcedureDetail,DrainsInserted,BloodLoss,ClosureMethod,SutureMaterials,MonitoringInstruct,Nutrition,SpecialIntruct")] SurgeryMaster surgeryMaster)
        {
            if (ModelState.IsValid)
            {
                db.SurgeryMasters.Add(surgeryMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(surgeryMaster);
        }


        public JsonResult Getsnp()
        {
            var Route = db.SurgeryNProcedures.Select(x => new { x.nprid, x.nprDescription,x.nprlDescription }).ToList();
            return Json(Route, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getsut()
        {
            var Route = db.SurgerySutureMs.Select(x => new { x.SMID, x.Suturematerials }).ToList();
            return Json(Route, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getatsur()
        {
            var Route = db.Surgeoms.Select(x => new { x.moid, x.modesc }).ToList();
            return Json(Route, JsonRequestBehavior.AllowGet);

        }
        public ActionResult view()
        {
            string pdid = Request.QueryString["pdid"];
            ViewBag.pdid = pdid;
            return View();
        }
        public ActionResult view2()
        {
            string pdid = Request.QueryString["pdid"];
            ViewBag.pdid = pdid;
            return View();
        }
        public JsonResult Getsurmo()
        {
            var Route = db.SurgeeryMoes.Select(x => new { x.moid, x.modesc }).ToList();
            return Json(Route, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getsurasi()
        {
            var Route = db.SurgeryAssists.Select(x => new { x.asid, x.asidesc }).ToList();
            return Json(Route, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getsurfeq()
        {
            var Route = db.SurgeryFrequencies.Select(x => new { x.pomfid, x.pomfdesc }).ToList();
            return Json(Route, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getsurpdur()
        {
            var Route = db.SurgeryPDurations.Select(x => new { x.pomdid, x.pomduration }).ToList();
            return Json(Route, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getsurpom()
        {
            var Route = db.SurgeryPoms.Select(x => new { x.pomid, x.pomdesc }).ToList();
            return Json(Route, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getsurtecniq()
        {
            var Route = db.SurgeryTechniqs.Select(x => new { x.tid, x.tdesc }).ToList();
            return Json(Route, JsonRequestBehavior.AllowGet);

        }
        
        public JsonResult Getnut()
        {
            var Route = db.SurgeryNutritions.Select(x => new { x.NID, x.NutDesc }).ToList();
            return Json(Route, JsonRequestBehavior.AllowGet);

        }
        
        public JsonResult Getsta()
        {
            var Route = db.SurgeryTypes.Select(x => new { x.TAID, x.TADescription }).ToList();
            return Json(Route, JsonRequestBehavior.AllowGet);

        }
        
        // GET: SurgeryMasters/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurgeryMaster surgeryMaster = db.SurgeryMasters.Find(id);
            if (surgeryMaster == null)
            {
                return HttpNotFound();
            }
            return View(surgeryMaster);
        }

        // POST: SurgeryMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SGID,PID,PDID,TheaterTable,ProcedureName,AnesthesiaType,DateodProcedure,Indication,Surgeon,AssistedBy,Anesthetist,MOAnesthesia,AntibioticP,Positioning,DurationofSurgery,Incision,PrcedureDetail,DrainsInserted,BloodLoss,ClosureMethod,SutureMaterials,MonitoringInstruct,Nutrition,SpecialIntruct")] SurgeryMaster surgeryMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surgeryMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(surgeryMaster);
        }

        // GET: SurgeryMasters/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurgeryMaster surgeryMaster = db.SurgeryMasters.Find(id);
            if (surgeryMaster == null)
            {
                return HttpNotFound();
            }
            return View(surgeryMaster);
        }

        // POST: SurgeryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SurgeryMaster surgeryMaster = db.SurgeryMasters.Find(id);
            db.SurgeryMasters.Remove(surgeryMaster);
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
