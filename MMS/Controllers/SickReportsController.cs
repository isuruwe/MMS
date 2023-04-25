using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MMS;

using System.Drawing.Imaging;

using MMS.Models;
using PagedList;
using System.Data.SqlClient;

namespace MMS.Controllers
{
    public class SickReportsController : Controller
    {
        private MMSEntities db = new MMSEntities();
      
       // private P3Context dbhrms = new P3Context();
       // private EPASContext db = new EPASContext();
        private int Userid;
        private string err;
        ImageCodecInfo df;
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
       // private P2Context dbp2 = new P2Context();
        private string base64String;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        string sqlQuery;
        private long Abdominal2;

        // GET: SickReports
        public ActionResult Index(int? page, string id, string currentFilter)
        {
            try
            {
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
                int relasiont = 0;
                string rnkname = "";
                string sname = "";
                string sno = "";
                int sv = 0;

                var result = (dynamic)null;
                var bg = (dynamic)null;
                var onePageOfProducts = (dynamic)null;
                char[] MyChar = { '/', '"', ' ' };
                string opdid = "";
                string locid = "";
                string swo = "";
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
                var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID, s.SpecialityID };

                foreach (var item in opd)
                {
                    specid = item.SpecialityID;

                }

                //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                //foreach (var item in serW)
                //{

                //    locid = item.LocationID;
                //}

                locid=(String)Session["userloc"]  ;
                if (!String.IsNullOrEmpty(id))
                {
                    id = id.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(id))
                {
                    DateTime dd = DateTime.Now.Date;
                    var isswo = from s in db.Vw_Formation.Where(p => p.DivisionID == opdid && p.LocationID == locid)
                                select new { s.DivisionName };
                    foreach (var itemswo in isswo)
                    {
                        swo = itemswo.DivisionName.ToLower();
                    }

                    var patient_Detailn = (dynamic)null;
                    DataTable oDataSet3 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "  SELECT max(j.age)age,max(j.isduty)isduty,max(j.isliveout)isliveout,max(j.islow)islow,max(j.service)service, " +
" max(k.CatPeriod)catdate,  max(f.Category_Type)cattp, " +
" COALESCE(NULLIF(max(case when c.RelationshipType = 1 " +
"  and b.Surname != '0'  then b.Surname end), ''), max(c.surname)) " +
"    sname  ,max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
"	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
"	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(a.status)  pstatus,max(j.regdate) regdate, max(h.Relationship) " +

"   relasiondet FROM[MMS].[dbo].sickreport as j with(nolock)   left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID " +
" left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
" left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID " +
" left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
" left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" where    (j.LocationID='" + locid + "') and " +
"  b.ServiceNo='" + id + "' group by a.PDID, a.CreatedDate order by regdate";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet3);
                    oSqlConnection.Close();
                    var sicdt = oDataSet3.AsEnumerable()
            .Select(dataRow => new getsickdata2
            {
                svcid = dataRow.Field<string>("sno"),
                PDID = dataRow.Field<string>("pdids"),
                isliveout = dataRow.Field<string>("isliveout"),
                fname = dataRow.Field<string>("inililes"),
                lname = dataRow.Field<string>("sname"),
                rank = dataRow.Field<string>("rnkname"),
                age = dataRow.Field<int>("age").ToString(),
                service = dataRow.Field<Double>("service").ToString(),
                isduty = dataRow.Field<string>("isduty"),
                islow = dataRow.Field<string>("islow"),
                cat = dataRow.Field<string>("cattp"),
                catdays = dataRow.Field<string>("catdate"),
                regdate = dataRow.Field<DateTime>("regdate"),
                stat = dataRow.Field<int>("pstatus").ToString()

            }).ToList();
                    Session["sickreportcount"] = sicdt.Count().ToString();
                    var pageNumber = page ?? 1;
                    onePageOfProducts = sicdt.OrderByDescending(p => p.regdate).ToPagedList(pageNumber, 10);





                   

                  
                }
                else
                {
                   
                        DateTime dd = DateTime.Now.Date;

                        //var df=      from s in db.Patient_Detail.Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate)
                        //      //       join b in db.Patients on s.PID equals b.PID into cs
                        //      //from b in cs.DefaultIfEmpty()

                        //      select new { s.PDID, s.Patient.Initials, s.Patient.Surname, s.Present_Complain, s.Patient.rank1.RNK_NAME, s.CreatedDate, s.OPD_Diagnosis, s.OPDID,s.Status1.StatusDec,s.Patient.PID };
                        var isswo = from s in db.Vw_Formation.Where(p => p.DivisionID == opdid &&p.LocationID==locid)
                                    select new { s.DivisionName };
                        foreach (var itemswo in isswo)
                        {
                          swo=  itemswo.DivisionName.ToLower();
                        }

                        var patient_Detailn= (dynamic)null;
                        if (swo.StartsWith("bw")|| swo.StartsWith("aw")||swo.StartsWith("sw"))
                        {
                        if (locid=="CBO")
                        {
                            DataTable oDataSet3 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "  SELECT max(j.age)age,max(j.isduty)isduty,max(j.isliveout)isliveout,max(j.islow)islow,max(j.service)service, " +
" max(k.CatPeriod)catdate,  max(f.Category_Type)cattp, " +
" COALESCE(NULLIF(max(case when c.RelationshipType = 1 " +
"  and b.Surname != '0'  then b.Surname end), ''), max(c.surname)) " +
"    sname  ,max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
"	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
"	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(a.status)  pstatus,max(j.regdate) regdate, max(h.Relationship) " +

   "   relasiondet FROM[MMS].[dbo].sickreport as j with(nolock)   left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID " +
" left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
" left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID " +
" left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
" left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" where convert(date, j.regdate) =CONVERT(varchar,'" + dd.Date.ToString() + "',111) " +
" and (j.LocationID='" + locid + "' or j.LocationID='AHQ') group by a.PDID, a.CreatedDate order by regdate";
                            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            oSqlConnection.Open();
                            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                            oSqlDataAdapter.Fill(oDataSet3);
                            oSqlConnection.Close();
                            var sicdt = oDataSet3.AsEnumerable()
                    .Select(dataRow => new getsickdata2
                    {
                        svcid = dataRow.Field<string>("sno"),
                        PDID = dataRow.Field<string>("pdids"),
                        isliveout = dataRow.Field<string>("isliveout"),
                        fname = dataRow.Field<string>("inililes"),
                        lname = dataRow.Field<string>("sname"),
                        rank = dataRow.Field<string>("rnkname"),
                        age = dataRow.Field<int>("age").ToString(),
                        service = dataRow.Field<Double>("service").ToString(),
                        isduty = dataRow.Field<string>("isduty"),
                        islow = dataRow.Field<string>("islow"),
                        cat = dataRow.Field<string>("cattp"),
                        catdays = dataRow.Field<string>("catdate"),
                        regdate = dataRow.Field<DateTime>("regdate"),
                        stat = dataRow.Field<int>("pstatus").ToString()

                    }).ToList();
                            var pageNumber = page ?? 1;
                            Session["sickreportcount"] = sicdt.Count().ToString();
                            onePageOfProducts = sicdt.OrderByDescending(p => p.regdate).ToPagedList(pageNumber, 10);
                        }
                        else
                        {
                            DataTable oDataSet3 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "  SELECT max(j.age)age,max(j.isduty)isduty,max(j.isliveout)isliveout,max(j.islow)islow,max(j.service)service, " +
" max(k.CatPeriod)catdate,  max(f.Category_Type)cattp, " +
" COALESCE(NULLIF(max(case when c.RelationshipType = 1 " +
"  and b.Surname != '0'  then b.Surname end), ''), max(c.surname)) " +
"    sname  ,max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
"	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
"	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(a.status)  pstatus,max(j.regdate) regdate, max(h.Relationship) " +

   "   relasiondet FROM[MMS].[dbo].sickreport as j with(nolock)   left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID " +
" left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
" left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID " +
" left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
" left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" where convert(date, j.regdate) =CONVERT(varchar,'" + dd.Date.ToString() + "',111) " +
" and j.LocationID='" + locid + "' group by a.PDID, a.CreatedDate order by regdate";
                            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            oSqlConnection.Open();
                            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                            oSqlDataAdapter.Fill(oDataSet3);
                            oSqlConnection.Close();
                            var sicdt = oDataSet3.AsEnumerable()
                    .Select(dataRow => new getsickdata2
                    {
                        svcid = dataRow.Field<string>("sno"),
                        PDID = dataRow.Field<string>("pdids"),
                        isliveout = dataRow.Field<string>("isliveout"),
                        fname = dataRow.Field<string>("inililes"),
                        lname = dataRow.Field<string>("sname"),
                        rank = dataRow.Field<string>("rnkname"),
                        age = dataRow.Field<int>("age").ToString(),
                        service = dataRow.Field<Double>("service").ToString(),
                        isduty = dataRow.Field<string>("isduty"),
                        islow = dataRow.Field<string>("islow"),
                        cat = dataRow.Field<string>("cattp"),
                        catdays = dataRow.Field<string>("catdate"),
                        regdate = dataRow.Field<DateTime>("regdate"),
                        stat = dataRow.Field<int>("pstatus").ToString()

                    }).ToList();
                            Session["sickreportcount"] = sicdt.Count().ToString();
                            var pageNumber = page ?? 1;
                            onePageOfProducts = sicdt.OrderByDescending(p => p.regdate).ToPagedList(pageNumber, 10);
                        }
                            

                      
                    }
                        else
                        {
                        DataTable oDataSet3 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "  SELECT max(j.age)age,max(j.isduty)isduty,max(j.isliveout)isliveout,max(j.islow)islow,max(j.service)service, " +
" max(k.CatPeriod)catdate,  max(f.Category_Type)cattp, " +
" COALESCE(NULLIF(max(case when c.RelationshipType = 1 " +
"  and b.Surname != '0'  then b.Surname end), ''), max(c.surname)) " +
"    sname  ,max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
"	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
"	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(a.status)  pstatus,max(j.regdate) regdate, max(h.Relationship) " +

"   relasiondet FROM[MMS].[dbo].sickreport as j with(nolock)   left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID " +
" left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
" left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID " +
" left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
" left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" where convert(date, j.regdate) =CONVERT(varchar,'" + dd.Date.ToString() + "',111) " +
" and j.LocationID='" + locid + "' and (j.opdid='"+opdid+ "' or b.Posted_Formation='" + opdid + "') group by a.PDID, a.CreatedDate order by regdate";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlConnection.Open();
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSet3);
                        oSqlConnection.Close();
                        var sicdt = oDataSet3.AsEnumerable()
                .Select(dataRow => new getsickdata2
                {
                    svcid = dataRow.Field<string>("sno"),
                    PDID = dataRow.Field<string>("pdids"),
                    isliveout = dataRow.Field<string>("isliveout"),
                    fname = dataRow.Field<string>("inililes"),
                    lname = dataRow.Field<string>("sname"),
                    rank = dataRow.Field<string>("rnkname"),
                    age = dataRow.Field<int>("age").ToString(),
                    service = dataRow.Field<Double>("service").ToString(),
                    isduty = dataRow.Field<string>("isduty"),
                    islow = dataRow.Field<string>("islow"),
                    cat = dataRow.Field<string>("cattp"),
                    catdays = dataRow.Field<string>("catdate"),
                    regdate = dataRow.Field<DateTime>("regdate"),
                    stat = dataRow.Field<int>("pstatus").ToString()

                }).ToList();
                        Session["sickreportcount"] = sicdt.Count().ToString();
                        var pageNumber = page ?? 1;
                        onePageOfProducts = sicdt.OrderByDescending(p => p.regdate).ToPagedList(pageNumber, 10);
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

        public JsonResult Submitsicknurs(string PID, string livein, string age, string tservice, string forduty, string defaulter, string loc, string div)
        {
            char[] MyChar = { '/', '"', ' ' };
            PID = PID.Trim(MyChar);
            livein = livein.Trim(MyChar);
            age = age.Trim(MyChar);
            tservice = tservice.Trim(MyChar);
            forduty = forduty.Trim(MyChar);
            defaulter = defaulter.Trim(MyChar);
            loc = loc.Trim(MyChar);
            div = div.Trim(MyChar);
            string Abdominal = "";
            string opdid = "";
            //string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            string locid ="";

            opdid = (String)Session["userlocid1"];
            locid = (String)Session["userloc"];
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}

            Patient_Detail patient_Detail = new Patient_Detail();
            IndexGeneration indi = new IndexGeneration();
            string pdid = indi.CreatePDID(PID);

            patient_Detail.PDID = pdid;
            patient_Detail.PID = PID;
            patient_Detail.OPDID = "";
            patient_Detail.Present_Complain = "";
            patient_Detail.CreatedBy = userid.ToString();
            patient_Detail.PatientCatID = 0;
            patient_Detail.SubjectID = 0;
            //patient_Detail.ModifiedBy = userid.ToString();
            //patient_Detail.ModifiedMachine = userid.ToString();
            patient_Detail.CreatedDate = DateTime.Now;
            //patient_Detail.CreatedMachine = userid.ToString();
            patient_Detail.Status = 1;



            SickReport oSickReport = new SickReport();



            oSickReport.PDID = pdid;
            oSickReport.age = Convert.ToInt32(age);
            oSickReport.isduty = forduty;
            oSickReport.isliveout = livein;
            oSickReport.islow = defaulter;
            oSickReport.regdate = DateTime.Now;
            oSickReport.service = Convert.ToDouble(tservice);
            oSickReport.OPDID = div;
            oSickReport.LocationID = loc;
            oSickReport.svcid = PID;
            oSickReport.IsMedical = 0;
            oSickReport.ISDental ="0";
            //patient_Detail.CreatedMachine = userid.ToString();
            oSickReport.createdby = userid;

            TranferDetail oTranferDetails = new TranferDetail();
            oTranferDetails.PDID = oSickReport.PDID;
            oTranferDetails.ToLoc = div;
            oTranferDetails.FromLoc = div;
            oTranferDetails.TransferDate = DateTime.Now;
            oTranferDetails.TransID = indi.CreateTransID(oSickReport.PDID);
            oTranferDetails.TransStatus = 1;



            if (ModelState.IsValid)
            {
                DateTime dd = DateTime.Now.Date;
              //  var Abdominalvar = from s in db.Patient_Detail.Where(p => p.PID == PID).Where(p => p.Status == 1).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year) select new { s.PDID };

                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select PDID from [dbo].[SickReport] with (nolock) where svcid='" + PID + "' and LocationID='" + locid + "'  and convert(date, [regDate])=CONVERT(varchar,'" + dd.ToShortDateString() + "',111) ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet);
                oSqlConnection.Close();
                var Abdominalvar = oDataSet.AsEnumerable()
        .Select(dataRow => new SickReport
        {
            PDID = dataRow.Field<string>("PDID")
           
        }).ToList();



                foreach (var item in Abdominalvar)
                {

                    Abdominal = item.PDID;

                }
                if (String.IsNullOrEmpty(Abdominal))
                {
                    try
                    {
                        db.TranferDetails.Add(oTranferDetails);
                        db.Patient_Detail.Add(patient_Detail);
                        db.SickReports.Add(oSickReport);
                        db.SaveChanges();
                        err = "Saved";
                    }
                    catch (Exception ex)
                    {
                        err = ex.ToString();

                    }
                }
                else
                {
                    err = "Already in Sick Parade!";
                }
            }

            return Json(err, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Submitsick(string PID, string livein, string age, string tservice, string forduty, string defaulter,string  tmedexam,string formed)
        {
            bool istimeok = false;
            char[] MyChar = { '/', '"', ' ' };
            PID = PID.Trim(MyChar);
            livein = livein.Trim(MyChar);
            age = age.Trim(MyChar);
            tservice = tservice.Trim(MyChar);
            forduty = forduty.Trim(MyChar);
            defaulter = defaulter.Trim(MyChar);
            tmedexam = tmedexam.Trim(MyChar);
            formed = formed.Trim(MyChar);
            if (tmedexam.Equals("true"))
            {
                tmedexam = "1";
            }
            else
            {
                tmedexam = "0";
            }
            if (formed.Equals("dental"))
            {
                istimeok = true;
                formed = "1";
            }
            else
            {
                formed = "0";
            }
            string Abdominal = "";
            string opdid = "";
            string locid = "";
            string isofficer = "";
            int userid = Convert.ToInt32(Session["UserID"]);


            opdid = (String)Session["userlocid1"];
            locid = (String)Session["userloc"];
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}

            ///////////////////////////////////////////////

            DataTable oDataSet2 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT top 1 ServiceType  FROM [MMS].[dbo].[Users] as a left join [MMS].[dbo].[PersonalDetails] as b on a.UserName=b.ServiceNo where [UserID]='" + userid + "'";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(oDataSet2);
            oSqlConnection.Close();
            foreach (DataRow row in oDataSet2.Rows)
            {
                isofficer= row["ServiceType"].ToString();
               



            }


            ///////////////////////////////////////////////



            Patient_Detail patient_Detail = new Patient_Detail();
            
            IndexGeneration indi = new IndexGeneration();
            string pdid = indi.CreatePDID(PID);

            patient_Detail.PDID = pdid;
            patient_Detail.PID = PID;
            patient_Detail.OPDID = "";
            patient_Detail.Present_Complain = "";
            patient_Detail.CreatedBy = userid.ToString();
            if (formed=="1")
            {
                patient_Detail.PatientCatID =5;
            }
            else
            {
                patient_Detail.PatientCatID = 0;
            }
           
            patient_Detail.SubjectID = 0;
            //patient_Detail.ModifiedBy = userid.ToString();
            //patient_Detail.ModifiedMachine = userid.ToString();
            patient_Detail.CreatedDate = DateTime.Now;
            //patient_Detail.CreatedMachine = userid.ToString();
            patient_Detail.Status = 1;



            SickReport oSickReport = new SickReport();
          
           

            oSickReport.PDID = pdid;
            oSickReport.age = Convert.ToInt32(age);
            oSickReport.isduty = forduty;
            oSickReport.isliveout = livein;
            oSickReport.islow = defaulter;
            oSickReport.regdate = DateTime.Now;
            oSickReport.service = Convert.ToDouble(tservice);
            oSickReport.OPDID = opdid;
            oSickReport.LocationID = locid;
            oSickReport.svcid = PID;
            oSickReport.IsMedical =Convert.ToInt32( tmedexam);
            //patient_Detail.CreatedMachine = userid.ToString();
            oSickReport.createdby = userid;
            oSickReport.ISDental = formed;
            TranferDetail oTranferDetails = new TranferDetail();
            oTranferDetails.PDID = oSickReport.PDID;
            oTranferDetails.ToLoc = opdid;
            oTranferDetails.FromLoc = opdid;
            oTranferDetails.TransferDate = DateTime.Now;
            oTranferDetails.TransID = indi.CreateTransID(oSickReport.PDID);
            oTranferDetails.TransStatus = 1;

            TimeSpan start = new TimeSpan(4, 0, 0); //10 o'clock
            TimeSpan end = new TimeSpan(8, 30, 0); //12 o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;


            if (isofficer.Equals("1"))
            {
                istimeok = true;
            }
            if ((now > start) && (now < end))
            {
                istimeok = true;
            }

            if (ModelState.IsValid)
            {
                //DateTime dd = DateTime.Now.Date;
                // var Abdominalvar = from s in db.Patient_Detail.Where(p => p.PID == PID).Where(p => p.Status == 1).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year) select new { s.PDID };
                DateTime dd = DateTime.Now.Date;
                //  var Abdominalvar = from s in db.Patient_Detail.Where(p => p.PID == PID).Where(p => p.Status == 1).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year) select new { s.PDID };

                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select PDID from [dbo].[SickReport] with (nolock) where svcid='" + PID + "' and LocationID='" + locid + "'  and convert(date, [regDate])=CONVERT(varchar,'" + dd.ToShortDateString() + "',111) ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet);
                oSqlConnection.Close();
                var Abdominalvar = oDataSet.AsEnumerable()
        .Select(dataRow => new SickReport
        {
            PDID = dataRow.Field<string>("PDID")

        }).ToList();

                foreach (var item in Abdominalvar)
                {

                    Abdominal = item.PDID;

                }
                if (tmedexam.Equals("1"))
                {
                    istimeok = true;
                }
                if (formed.Equals("1"))
                {
                    istimeok = true;
                }
                
                 if (istimeok == false)
                {
                    err = "Time has passed to register sick parade. Special sick report to be raised !";
                }
                else if ( string.IsNullOrEmpty(locid)|| string.IsNullOrEmpty(opdid))
                {
                    err = "Error Occured Please Login Again!";
                }
                else if(!String.IsNullOrEmpty(Abdominal))
                {
                    err = "Already in Sick Parade!";
                }
                else
                {
                    try
                    {
                        db.TranferDetails.Add(oTranferDetails);
                        db.Patient_Detail.Add(patient_Detail);
                        db.SickReports.Add(oSickReport);
                        db.SaveChanges();
                        err = "Saved";
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();

                    }
                }
            }

            return Json(err, JsonRequestBehavior.AllowGet);
        }
        // GET: SickReports/Details/5

        public JsonResult Submitmedex(string PID, string age, string hght, string wght, string bmi, string bp, string vsion, string hbac, string usugar, string fbs, string tcol, string execg,string tri,string ldl,string yrr, string vsion2, string sess1)
        {
            char[] MyChar = { '/', '"', ' ' };
            PID = PID.Trim(MyChar);
            hght = hght.Trim(MyChar);
            age = age.Trim(MyChar);
            wght = wght.Trim(MyChar);
            bmi = bmi.Trim(MyChar);
            bp = bp.Trim(MyChar);
            vsion = vsion.Trim(MyChar);
            vsion2 = vsion2.Trim(MyChar);
            hbac = hbac.Trim(MyChar);
            usugar = usugar.Trim(MyChar);
            fbs = fbs.Trim(MyChar);
            tcol = tcol.Trim(MyChar);
            execg = execg.Trim(MyChar);
            ldl = ldl.Trim(MyChar);
            tri = tri.Trim(MyChar);
            yrr = yrr.Trim(MyChar);
            sess1 = sess1.Trim(MyChar);
            string Abdominal = "";
            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);


            opdid = (String)Session["userlocid1"];
            locid = (String)Session["userloc"];
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}

            Patient_Detail patient_Detail = new Patient_Detail();
            IndexGeneration indi = new IndexGeneration();
            string pdid = indi.CreatePDID(PID);

            patient_Detail.PDID = pdid;
            patient_Detail.PID = PID;
            patient_Detail.OPDID = opdid;
            patient_Detail.Present_Complain = "Medical Examination";
            patient_Detail.CreatedBy = userid.ToString();
            patient_Detail.PatientCatID = 2;
            patient_Detail.SubjectID = 1;
            //patient_Detail.ModifiedBy = userid.ToString();
            //patient_Detail.ModifiedMachine = userid.ToString();
            patient_Detail.CreatedDate = DateTime.Now;
            //patient_Detail.CreatedMachine = userid.ToString();
            patient_Detail.Status = 2;



            MedicalScreen oSickReport = new MedicalScreen();



            oSickReport.pdid = pdid;
            oSickReport.msage = age;
            oSickReport.msheight = hght;
            oSickReport.msweight = wght;
            oSickReport.msbmi = bmi;
            oSickReport.createddate = DateTime.Now;
            oSickReport.msbp = bp;
            oSickReport.msvision = vsion;
            if (vsion2.Contains("REundefined"))
            {
                oSickReport.msspecs ="";
            }
            else
            {
                oSickReport.msspecs = vsion2;
            }
            
            oSickReport.mshbac = hbac;
            oSickReport.msusugar = usugar;
            //patient_Detail.CreatedMachine = userid.ToString();
            oSickReport.createduser = userid.ToString();
            oSickReport.msfbs = fbs;
            oSickReport.mstotalc = tcol;
            oSickReport.msexecg = execg;
            oSickReport.msstation = locid;
            oSickReport.status = 1;
            oSickReport.msyear =Convert.ToInt32(yrr);
            oSickReport.msldl = ldl;
            oSickReport.mstrigliceried = tri;
            oSickReport.pftsession = Convert.ToInt32(sess1);


            TranferDetail oTranferDetails = new TranferDetail();
            oTranferDetails.PDID = oSickReport.pdid;
            oTranferDetails.ToLoc = opdid;
            oTranferDetails.FromLoc = opdid;
            oTranferDetails.TransferDate = DateTime.Now;
            oTranferDetails.TransID = indi.CreateTransID(oSickReport.pdid);
            oTranferDetails.TransStatus = 1;



            if (ModelState.IsValid)
            {
                DateTime dd = DateTime.Now.Date;
                //if (locid == "CBO")
                //{
                //    var sicd = from s in db.SickReports.Where(p => p.LocationID == locid || p.LocationID == "AHQ").Where(p => p.regdate.Value.Day == dd.Day && p.regdate.Value.Month == dd.Month && p.regdate.Value.Year == dd.Year)
                //             .Where(p => p.svcid == PID)

                //               orderby s.regdate descending
                //               select new getsickdata { PDID = s.PDID };

                //    foreach (var item in sicd)
                //    {
                //      //  patient_Detail.PDID = item.PDID;
                //        //oSickReport.pdid = item.PDID;
                //    }
                //}
                //else
                //{
                //    var sicd = from s in db.SickReports.Where(p => p.LocationID == locid).Where(p => p.regdate.Value.Day == dd.Day && p.regdate.Value.Month == dd.Month && p.regdate.Value.Year == dd.Year)
                //             .Where(p => p.svcid == PID)
                //               orderby s.regdate descending
                //               select new getsickdata {  PDID = s.PDID };

                //    foreach (var item in sicd)
                //    {
                //       // patient_Detail.PDID = item.PDID;
                //      //  oSickReport.pdid = item.PDID;
                //    }
                //}

                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  select a.PDID,a.msid,a.[msyear] ,a.[msstation],a.[msage],a.[msheight],a.[msweight],a.[msbmi],a.[msbp],a.[msvision],a.[mshbac],a.[msusugar] " +
    "  ,a.[msfbs],a.[mstotalc],a.[msexecg],a.[msmes],a.[msfitness],a.[createddate],a.[createduser],a.[modifieduser],a.[modifieddate],a.[status] " +
   "   ,a.[msldl],a.[mstrigliceried],a.[msreason] from [dbo].[MedicalScreen] as a with (nolock) inner join [dbo].[Patient_Detail] as b on a.pdid=b.pdid  where b.pid='" + PID + "' and a.msyear="+ Convert.ToInt32(yrr) + " and a.pftsession='" + sess1 + "'";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet);
                oSqlConnection.Close();
                var Abdominalvar = oDataSet.AsEnumerable()
        .Select(dataRow => new MedicalScreen
        {
            pdid = dataRow.Field<string>("PDID"),
            msid = dataRow.Field<long>("msid"),
            msyear = dataRow.Field<int?>("msyear"),
            msstation = dataRow.Field<string>("msstation"),
            msage = dataRow.Field<string>("msage"),
            msheight = dataRow.Field<string>("msheight"),
            msweight = dataRow.Field<string>("msweight"),
            msbmi = dataRow.Field<string>("msbmi"),
            msbp = dataRow.Field<string>("msbp"),
            msvision = dataRow.Field<string>("msvision"),
            mshbac = dataRow.Field<string>("mshbac"),
            msusugar = dataRow.Field<string>("msusugar"),
            msfbs = dataRow.Field<string>("msfbs"),
            mstotalc = dataRow.Field<string>("mstotalc"),
            msexecg = dataRow.Field<string>("msexecg"),
            msmes = dataRow.Field<string>("msmes"),
            msfitness = dataRow.Field<int?>("msfitness"),
            createddate = dataRow.Field<DateTime?>("createddate"),
            createduser = dataRow.Field<string>("createduser"),
            modifieduser = dataRow.Field<string>("modifieduser"),
            modifieddate = dataRow.Field<DateTime?>("modifieddate"),
            status = dataRow.Field<int?>("status"),
            msldl = dataRow.Field<string>("msldl"),
            mstrigliceried = dataRow.Field<string>("mstrigliceried"),
            msreason = dataRow.Field<string>("msreason"),
        }).ToList();

                DataTable oDataSet1 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  select PDID from [dbo].[Patient_Detail]  where pid='" + PID + "' and  convert(date, [CreatedDate]) =CONVERT(varchar,'" + dd.ToShortDateString() + "',111)  ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet1);
                oSqlConnection.Close();
                var Abdominalvar1 = oDataSet1.AsEnumerable()
        .Select(dataRow => new MedicalScreen
        {
            pdid = dataRow.Field<string>("PDID"),
           

        }).ToList();

                foreach (var item in Abdominalvar1)
                {

                   
                  
                    oSickReport.pdid= item.pdid;
                    patient_Detail.PDID= item.pdid;
                }
                foreach (var item in Abdominalvar)
                {
                    oSickReport.pdid = item.pdid;
                    oSickReport.msage = age;
                    oSickReport.msheight = hght;
                    oSickReport.msweight = wght;
                    oSickReport.msbmi = bmi;
                    oSickReport.createddate = DateTime.Now;
                    oSickReport.msbp = bp;
                    oSickReport.msvision = vsion;
                    oSickReport.mshbac = hbac;
                    oSickReport.msusugar = usugar;
                    
                    oSickReport.createduser = userid.ToString();
                    oSickReport.msfbs = fbs;
                    oSickReport.mstotalc = tcol;
                    oSickReport.msexecg = execg;
                    oSickReport.msstation = locid;
                    oSickReport.status = 1;
                    oSickReport.msyear = Convert.ToInt32(yrr);
                    oSickReport.msldl = ldl;
                    oSickReport.mstrigliceried = tri;
                    Abdominal = item.pdid;
                    oSickReport.pftsession = Convert.ToInt32(sess1);


                }
                if (!String.IsNullOrEmpty(Abdominal))
                {
                    try
                    {
                        //db.TranferDetails.Add(oTranferDetails);
                        //db.Entry(patient_Detail).State = EntityState.Modified;
                        //db.Entry(oSickReport).State = EntityState.Modified;
                      
                        //db.SaveChanges();
                        err = "Already Saved for this year and session!";
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();

                    }
                }
                else
                {
                    try
                    {
                      
                        db.TranferDetails.Add(oTranferDetails);
                        db.Entry(patient_Detail).State = EntityState.Modified;

                        db.MedicalScreens.Add(oSickReport);
                        db.SaveChanges();
                        err = "Saved";
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();

                    }


                    //err = "Already in Sick Parade!";
                }
            }

            return Json(err, JsonRequestBehavior.AllowGet);
        }
        // GET: SickReports/Details/5
        public JsonResult Submitmedex2(string PID, string bp,  string medmes, string fit, long msid, string resn, string tcol, string fbs)
        {
            char[] MyChar = { '/', '"', ' ' };
            PID = PID.Trim(MyChar);
           
            bp = bp.Trim(MyChar);
            medmes = medmes.Trim(MyChar);
            fit = fit.Trim(MyChar);
            resn = resn.Trim(MyChar);
            fbs = fbs.Trim(MyChar);
            tcol = tcol.Trim(MyChar);
            // msid = msid.Trim(MyChar);



            string Abdominal = "";
            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);


            opdid = (String)Session["userlocid1"];
            locid = (String)Session["userloc"];
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}

            Patient_Detail patient_Detail = new Patient_Detail();
            IndexGeneration indi = new IndexGeneration();
            string pdid = indi.CreatePDID(PID);

            patient_Detail.PDID = pdid;
            patient_Detail.PID = PID;
            patient_Detail.OPDID = opdid;
            patient_Detail.Present_Complain = "Medical Examination";
            patient_Detail.CreatedBy = userid.ToString();
            patient_Detail.PatientCatID = 2;
            patient_Detail.SubjectID = 1;
            patient_Detail.ModifiedBy = userid.ToString();
            //patient_Detail.ModifiedMachine = userid.ToString();
            patient_Detail.CreatedDate = DateTime.Now;
            //patient_Detail.CreatedMachine = userid.ToString();
            patient_Detail.Status = 8;



            MedicalScreen oSickReport = new MedicalScreen();
            try
            {
                var meds = from s in db.MedicalScreens.Where(p => p.msid == msid)
                           select new
                           {
                               s.msid,
                               s.pdid,
                               s.msage,
                               s.msheight
,
                               s.msweight,
                               s.msbmi,
                               s.createddate,
                               s.msvision,
                               s.mshbac,
                               s.msusugar,
                               s.createduser,
                               s.msfbs,
                               s.mstotalc,
                               s.msexecg,
                               s.msstation,
                               s.msyear,
                               s.msspecs,
                               s.pftsession

                           };
                foreach (var item in meds)
                {
                    oSickReport.pdid = item.pdid;
                    oSickReport.msage = item.msage;
                    oSickReport.msheight = item.msheight;
                    oSickReport.msweight = item.msweight;
                    oSickReport.msbmi = item.msbmi;
                    oSickReport.createddate = item.createddate;
                    oSickReport.msbp = bp;
                    oSickReport.msvision = item.msvision;
                    oSickReport.mshbac = item.mshbac;
                    oSickReport.msusugar = item.msusugar;
                    oSickReport.msmes = medmes;
                    oSickReport.msfitness = Convert.ToInt32(fit);
                    //patient_Detail.CreatedMachine = userid.ToString();
                    oSickReport.createduser = item.createduser;
                    oSickReport.msfbs = item.msfbs;
                    oSickReport.mstotalc = item.mstotalc;
                    oSickReport.msexecg = item.msexecg;
                    oSickReport.msstation = item.msstation;
                    oSickReport.status = 2;
                    oSickReport.msyear = item.msyear;
                    oSickReport.msid = item.msid;
                    oSickReport.modifieduser = userid.ToString();
                    oSickReport.modifieddate = DateTime.Now;
                    oSickReport.msreason = resn;
                    oSickReport.msspecs = item.msspecs;
                    oSickReport.mstotalc = tcol;
                    oSickReport.msfbs = fbs;
                    oSickReport.pftsession = item.pftsession;
                }

            }
            catch (Exception ex)
            {

            }

            TranferDetail oTranferDetails = new TranferDetail();
            oTranferDetails.PDID = oSickReport.pdid;
            oTranferDetails.ToLoc = opdid;
            oTranferDetails.FromLoc = opdid;
            oTranferDetails.TransferDate = DateTime.Now;
            oTranferDetails.TransID = indi.CreateTransID(oSickReport.pdid);
            oTranferDetails.TransStatus = 1;



            if (ModelState.IsValid)
            {
               

               
                    try
                    {
                        db.TranferDetails.Add(oTranferDetails);
                    
                    db.Entry(oSickReport).State = EntityState.Modified;

                    db.SaveChanges();
                        err = "Saved";
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();

                    }
                }
               
            

            return Json(err, JsonRequestBehavior.AllowGet);
        }
        // GET: SickReports/Details/5

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SickReport sickReport = db.SickReports.Find(id);
            if (sickReport == null)
            {
                return HttpNotFound();
            }
            return View(sickReport);
        }

        // GET: SickReports/Create
        public ActionResult Create()
        {
            return View();
        }
        public JsonResult getmsts(string id, string relet,string sess1)
        {
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(relet))
            {
                relet = relet.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(sess1))
            {
                sess1 = sess1.Trim(MyChar);
            }
            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT    COALESCE(NULLIF(max(case when a.RelationshipType = 1 " +
    "  then b.Surname end), ''), " +
    "	 max(a.surname))  sname  ,max(case when a.RelationshipType = 1 then b.Rank  end) rnkname, max(a.ServiceNo)  sno   , " +
      " max(case when a.RelationshipType = 1 then b.Initials  end)  inililes   " +
    "   , max(i.msfitness)  msstatus,max(i.msreason)  reason,max(i.createddate) crd,max(msmes) msmes  FROM[MMS]. " +
     "  [dbo].[Patient] as a with(nolock)  left join[MMS].[dbo].[PersonalDetails] as b on a.ServiceNo=b.ServiceNo and a.Service_Type=b.ServiceType left join[MMS]. " +
      " [dbo].[Patient_Detail] as c on a.pid=c.pid " +

       "   inner join[MMS].[dbo].[MedicalScreen] as i on i.pdid = c.pdid  " +
    " where   a.ServiceNo ='" + id + "' and i.msyear='" + relet + "' and i.pftsession='"+sess1+"'";

                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet);
                oSqlConnection.Close();
                var empList = oDataSet.AsEnumerable()
        .Select(dataRow => new getdocdetail
        {
            pdids = dataRow.Field<string>("reason"),
            inililes = dataRow.Field<string>("inililes"),
            sname = dataRow.Field<string>("sname"),
            sno = dataRow.Field<string>("sno"),
            rnkname = dataRow.Field<string>("rnkname"),
            sv = dataRow.Field<int?>("msstatus"),
            locdet = dataRow.Field<string>("msmes"),
            crdate = dataRow.Field<DateTime?>("crd"),

        }).ToList();
                return Json(empList.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(err, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult viewmsts()
        {
           
            return View();
        }
        // POST: SickReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PDID,svcid,ismarried,isliveout,age,service,isduty,islow,regdate,createdby,modifiedby,modifieddate")] SickReport sickReport)
        {
            if (ModelState.IsValid)
            {
                db.SickReports.Add(sickReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sickReport);
        }

        // GET: SickReports/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SickReport sickReport = db.SickReports.Find(id);
            if (sickReport == null)
            {
                return HttpNotFound();
            }
            return View(sickReport);
        }

        // POST: SickReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PDID,svcid,ismarried,isliveout,age,service,isduty,islow,regdate,createdby,modifiedby,modifieddate")] SickReport sickReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sickReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sickReport);
        }

        // GET: SickReports/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SickReport sickReport = db.SickReports.Find(id);
            if (sickReport == null)
            {
                return HttpNotFound();
            }
            return View(sickReport);
        }

        // POST: SickReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SickReport sickReport = db.SickReports.Find(id);
            db.SickReports.Remove(sickReport);
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
