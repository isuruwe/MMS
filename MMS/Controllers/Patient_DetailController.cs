using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;


using MMS.Models;
using PagedList;
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.Dynamic;
using System.ComponentModel;
using System.Web.Routing;

using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Globalization;


namespace MMS.Controllers
{
    public class Patient_DetailController : Controller
    {
        private MMSEntities db = new MMSEntities();
        //private P3Context dbhrms = new P3Context();
       // private EPASContext dbepas = new EPASContext();
        private int Userid;
        private string err;
        ImageCodecInfo df;
        
        //private P2Context dbp2 = new P2Context();
        private string base64String;
        private int sikcnt;
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        string sqlQuery;

        string sertno = "";

        // GET: Patient_Detail
        public async Task<ActionResult> Index(int? page)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            var patient_Detail = db.Patient_Detail.Include(p => p.Patient).Include(p => p.Status1);
            return View(await patient_Detail.ToListAsync());
            //try
            //{
            //    var pageNumber = page ?? 1;
            //    var onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);

            //    ViewBag.OnePageOfProducts = onePageOfProducts;

            //    return View(await patient_Detail.ToListAsync());
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            // return View();


        }

        // GET: Patient_Detail/Details/5
        public async Task<ActionResult> Details(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient_Detail patient_Detail = await db.Patient_Detail.FindAsync(id);
            if (patient_Detail == null)
            {
                return HttpNotFound();
            }
            return View(patient_Detail);
        }
        public async Task<ActionResult> genentry(int? page, string id, string currentFilter)
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

                opdid = (String)Session["userlocid1"];


                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select Salutation,FName,LName from [dbo].[Users] with (nolock) where UserID='" + userid + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet);
                oSqlConnection.Close();
                var ser = oDataSet.AsEnumerable()
        .Select(dataRow => new User
        {
            FName = dataRow.Field<string>("FName"),
            LName = dataRow.Field<string>("LName"),
            Salutation = dataRow.Field<string>("Salutation")
        }).ToList();

                foreach (var item in ser)
                {
                    Session["loginuser"] = item.Salutation + ". " + item.FName + " " + item.LName;

                }



                DataTable oDataSet1 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select SpecialityID,LOCID from [dbo].[Staff_Master] with (nolock) where UserID='" + userid + "'";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oDataSet = null;
                oSqlDataAdapter.Fill(oDataSet1);
                oSqlConnection.Close();
                var opd = oDataSet1.AsEnumerable()
        .Select(dataRow => new Staff_Master
        {
            SpecialityID = dataRow.Field<int>("SpecialityID"),
            LOCID = dataRow.Field<string>("LOCID")

        }).ToList();



                foreach (var item in opd)
                {
                    specid = item.SpecialityID;

                }

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
                        // var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id ==Convert.ToInt32(specid)).Where(p => p.event_start == dt1).AsNoTracking() select new { s.title };
                        DataTable oDataSet2 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = " select title from [dbo].[Clinic_Schedule] with (nolock) where clinic_id='" + Convert.ToInt32(specid) + "' and event_start='" + dt1 + "' ";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlConnection.Open();
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSet2);
                        oSqlConnection.Close();
                        var shed = oDataSet2.AsEnumerable()
                .Select(dataRow => new Clinic_Schedule
                {
                    title = dataRow.Field<string>("title")


                }).ToList();



                    }
                    DateTime dd = DateTime.Now.Date;
                    DataTable oDataSet4 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "    SELECT    max(a.Present_Complain )  pcomoplian  ,COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1  " +
"  and b.Surname != '0' " +
 " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),    " +
"  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
"   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
 "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname))  " +
"	sname  ,max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
"	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
"	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(a.status)  pstatus,max(a.CreatedDate) crdate, max(h.Relationship) " +

 "     relasiondet FROM[MMS].[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
"  left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
"  left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
"   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
 "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where   c.ServiceNo like'%" + id + "%'    " +

 "     group by a.PDID, c.CreatedDate order by crdate";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet4);
                    oSqlConnection.Close();
                    var lid = oDataSet4.AsEnumerable()
            .Select(dataRow => new getdocdetail
            {
                pdids = dataRow.Field<string>("pdids"),
                inililes = dataRow.Field<string>("inililes"),
                sname = dataRow.Field<string>("sname"),
                sno = dataRow.Field<string>("sno"),
                rnkname = dataRow.Field<string>("rnkname"),
                pcomoplian = dataRow.Field<string>("pcomoplian"),
                pstatus = dataRow.Field<int>("pstatus").ToString(),
                relasiont = dataRow.Field<int>("relasiont"),
                crdate = dataRow.Field<DateTime?>("crdate"),
                pidp = dataRow.Field<string>("pidp"),
                relasiondet = dataRow.Field<string>("relasiondet"),
            }).ToList();

                    var pageNumber = page ?? 1;
                    onePageOfProducts = lid.OrderByDescending(p => p.crdate).ToPagedList(pageNumber, 10);
                }
                else
                {
                    if (opdid.Contains("CL"))
                    {
                        //DateTime dt1 = DateTime.Now.Date;
                        //var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id == specid).Where(p => p.event_start.Value.Day == dt1.Day && p.event_start.Value.Month == dt1.Month && p.event_start.Value.Year == dt1.Year).AsNoTracking() select new { s.title };
                        //int r = 0;
                        //foreach (var item in shed)
                        //{

                        //    title[r] = item.title;
                        //    r++;
                        //}


                        //var patient_Detailn = from s in db.Patient_Detail.Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7 || p.Status == 5).Where(p => p.OPDID == opdid).AsNoTracking() join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID orderby s.CreatedDate descending select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, opddiag = s.OPD_Diagnosis, relasiont = s.Patient.RelationshipType1.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = s.Patient.RelationshipType1.Relationship };

                        //DateTime dd = DateTime.Now.Date;
                        //var patient_Detail = from s in db.Patient_Detail.Where(p => p.PDID == "dfdffddf").AsNoTracking() join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID orderby s.CreatedDate descending select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, opddiag = s.OPD_Diagnosis, relasiont = f.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = f.Relationship };

                        //List<getdocdetail> lid = patient_Detail.ToList();

                        //foreach (var item in patient_Detailn)
                        //{
                        //    pid = item.pidp;
                        //    PDID = item.pdids;
                        //    Present_Complain = item.pcomoplian;

                        //    creteddate = item.crdate;
                        //    fname = item.fname;
                        //    inililes = item.inililes;
                        //    locdet = item.locdet;
                        //    opddiag = item.opddiag;
                        //    pstatus = item.pstatus;
                        //    relasiondet = item.relasiondet;
                        //    relasiont = item.relasiont;
                        //    rnkname = item.rnkname;
                        //    sname = item.sname;
                        //    sno = item.sno;
                        //    sv = item.sv;
                        //    int? stype = 0;
                        //    string svcno = "";

                        //    var cdlist = from s in db.Patients.Where(p => p.ServiceNo == sno).Where(p => p.RelationshipType == 5).AsNoTracking()
                        //                 orderby s.DateOfBirth
                        //                 select new { s.DateOfBirth, s.PID };

                        //    int hj = 1;
                        //    foreach (var itemchw in cdlist)
                        //    {
                        //        if (itemchw.PID == pid)
                        //        {
                        //            relasiondet = relasiondet + hj;

                        //        }

                        //        hj++;
                        //    }


                        //    var PersonResultList1 = from s in db.PersonalDetails.AsNoTracking()

                        //                            where s.ServiceNo == item.sno
                        //                            select new getdocdetail { sv = sv, pdids = PDID, inililes = s.Initials, sname = s.Surname, sno = s.ServiceNo, rnkname = s.Rank, pcomoplian = Present_Complain, pstatus = pstatus, relasiont = relasiont, crdate = creteddate, pidp = pid, relasiondet = relasiondet };

                        //    //patient_Detail = patient_Detail.Concat(PersonResultList1);
                        //    if (PersonResultList1.Count() > 0)
                        //    {
                        //        foreach (var iteme in PersonResultList1)
                        //        {

                        //            lid.Add(iteme);
                        //            break;
                        //        }
                        //    }





                        //    else
                        //    {
                        //        var patient_Detail2n = from s in db.Patient_Detail.Where(p => p.PDID == item.pdids).AsNoTracking()
                        //                               select new getdocdetail { sv = s.Patient.Service_Type, pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = s.Patient.RelationshipType1.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = s.Patient.RelationshipType1.Relationship };
                        //        //patient_Detail = patient_Detail .Concat(patient_Detail2n);


                        //        foreach (var iteme in patient_Detail2n)
                        //        {

                        //            lid.Add(iteme);
                        //        }
                        //    }
                        //}


                        ////db.Patient_Detail.Include(p => p.Patient).Where (p=>title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7 || p.Status == 5).Where(p => p.OPDID == opdid).OrderByDescending(p => p.CreatedDate);
                        //var pageNumber = page ?? 1;
                        //onePageOfProducts = lid.OrderByDescending(p => p.crdate).ToPagedList(pageNumber, 10);

                    }
                    else
                    {
                        DateTime dd = DateTime.Now.Date;

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
"	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(a.status)  pstatus,max(a.CreatedDate) crdate, max(h.Relationship) " +

 "     relasiondet FROM[MMS].[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
"  left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
"  left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
"   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
 "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" where convert(date, a.[CreatedDate]) =CONVERT(varchar,'" + dd.ToShortDateString() + "',111)  and (a.status= 2 or a.status= 5) " +
" and a.opdid='" + opdid + "' group by a.PDID, a.CreatedDate order by crdate ";
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
                    pstatus = dataRow.Field<int>("pstatus").ToString(),
                    relasiont = dataRow.Field<int>("relasiont"),
                    crdate = dataRow.Field<DateTime?>("crdate"),
                    pidp = dataRow.Field<string>("pidp"),
                    relasiondet = dataRow.Field<string>("relasiondet"),
                }).ToList();



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


        // GET: Patient_Detail/Create
        public ActionResult Create(int? page, string id,string currentFilter, string id3)
        {
            try {
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
              
                if (id!=null){
                    page = 1;
                    ViewBag.currentFilter = id;
                }
                else
                {
                    id = currentFilter;
                    ViewBag.currentFilter = id;
                }
            int userid = Convert.ToInt32(Session["UserID"]);
            
            opdid =(String)Session["userlocid1"];

                if (!opdid.Trim().ToLower().StartsWith("dv")) { 
                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select Salutation,FName,LName from [dbo].[Users] with (nolock) where UserID='" + userid + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet);
                oSqlConnection.Close();
                var ser = oDataSet.AsEnumerable()
        .Select(dataRow => new User
        {
            FName = dataRow.Field<string>("FName"),
            LName = dataRow.Field<string>("LName"),
            Salutation = dataRow.Field<string>("Salutation")
        }).ToList();

                foreach (var item in ser)
                {
                    Session["loginuser"] = item.Salutation + ". " + item.FName + " " + item.LName;

                }



                DataTable oDataSet1 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select SpecialityID,LOCID from [dbo].[Staff_Master] with (nolock) where UserID='" + userid + "'";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oDataSet = null;
                oSqlDataAdapter.Fill(oDataSet1);
                oSqlConnection.Close();
                var opd = oDataSet1.AsEnumerable()
        .Select(dataRow => new Staff_Master
        {
            SpecialityID = dataRow.Field<int>("SpecialityID"),
            LOCID = dataRow.Field<string>("LOCID")
            
        }).ToList();



                foreach (var item in opd)
            {
                specid = item.SpecialityID;
                
            }

               

                locid = (String)Session["userloc"];
                if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }

                if (!String.IsNullOrEmpty(id3))
                {
                    id3 = id3.Trim(MyChar);
                }
                DateTime ddx = DateTime.Now.Date;
                DataTable oDataSet9x = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                if (locid == "CBO")
                {
                    sqlQuery = "     SELECT v.Description as dv,count(j.regdate)regdate,count (case when a.status !=1 then (a.status)  end) as st,max(v.DivisionID)divid " +

                          "      FROM[MMS].[dbo].sickreport as j with(nolock)  left join[MMS].[dbo].[Patient_detail] " +
                       "		  as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
                   " left join[MMS].[dbo].[RelationshipType]  " +
                   "			   as h on h.RTypeID=c.RelationshipType inner join Vw_Formation as v on j.OPDID=v.DivisionID and " +

                        "          j.LocationID=v.LocationID where convert(date, j.regdate)   =CONVERT(varchar,'" + ddx.Date.ToString() + "',111) " +

                       "             and(j.LocationID= '" + locid + "' or j.LocationID= 'AHQ') and j.IsMedical!=1 and j.ISDental!=1 group by v.Description order by regdate desc";


                }
                else
                {
                    sqlQuery = "     SELECT v.Description as dv,count(j.regdate)regdate,count (case when a.status !=1 then (a.status)  end) as st,max(v.DivisionID)divid " +

      "      FROM[MMS].[dbo].sickreport as j with(nolock)  left join[MMS].[dbo].[Patient_detail] " +
   "		  as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].[RelationshipType]  " +
"			   as h on h.RTypeID=c.RelationshipType inner join Vw_Formation as v on j.OPDID=v.DivisionID and " +

    "          j.LocationID=v.LocationID where convert(date, j.regdate)   =CONVERT(varchar,'" + ddx.Date.ToString() + "',111) " +

   "             and(j.LocationID= '" + locid + "' ) and j.IsMedical!=1 and j.ISDental!=1 group by v.Description order by regdate desc";


                }

                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet9x);
                oSqlConnection.Close();
                var sicdtr = oDataSet9x.AsEnumerable()
        .Select(dataRow => new sickparade
        {
            section = dataRow.Field<string>("dv"),
            sickcount = dataRow.Field<int?>("regdate"),
            comcount = dataRow.Field<int?>("st"),
            divid = dataRow.Field<string>("divid"),

        }).ToList();


                ViewBag.sicpr = sicdtr;
                if (!String.IsNullOrEmpty(id3))
                {
                    DateTime dd = DateTime.Now.Date;

                    DataTable oDataSet3 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "       SELECT  a.SubjectID,(a.Present_Complain)pcomoplian,CASE WHEN isnull(b.ServiceStatus,'0')='0'   then (select top 1 ServiceStatus from [MMS].[dbo].[PersonalDetails] where  " +

  "     ServiceNo = c.ServiceNo)  else b.ServiceStatus end ServiceStatus, case when isnull(b.Surname,'0')= '0' then c.Initials + ' ' + c.Surname else b.Surname end     sname   " +
"  ,CASE WHEN isnull(b.Rank,'0')='0'   then (select top 1 Rank from [MMS].[dbo].[PersonalDetails] where ServiceNo=c.ServiceNo)  else b.Rank end rnkname, " +
"  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"  , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate,(a.ModifiedDate)mdate, (h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
"   as b on c.ServiceNo=b.ServiceNo  and b.ServiceType =CASE when c.Service_Type=4 then 1 when c.Service_Type = 5 then 2 else c.Service_Type end left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" where convert(date, a.[CreatedDate]) =CONVERT(varchar,'" + dd.ToShortDateString() + "',111)  and (a.status= 2 or a.status= 5) and a.SubjectID!=1 and a.PatientCatID!=5  " +
" and a.opdid='" + opdid + "'  order by crdate desc";
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
                mdate = dataRow.Field<DateTime?>("mdate"),
                pidp = dataRow.Field<string>("pidp"),
                relasiondet = dataRow.Field<string>("relasiondet"),
                ServiceStatus = dataRow.Field<string>("ServiceStatus"),
                stype = dataRow.Field<int>("Service_Type"),
                ID = dataRow.Field<int?>("SubjectID"),
            }).ToList();



                    ///db.Patient_Detail.Include(p => p.Patient).Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                    //  patient_Detail = patient_Detail.GroupBy(t => t.pdids).Select(grp => grp.FirstOrDefault()).OrderByDescending(s=>s.crdate);
                    var pageNumber = page ?? 1;

                    onePageOfProducts = lid.ToList();
                }

              else      if (!String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Now.Date;
                if (opdid.Contains("cl"))
                {
                        // var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id ==Convert.ToInt32(specid)).Where(p => p.event_start == dt1).AsNoTracking() select new { s.title };
                        DataTable oDataSet2 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = " select title from [dbo].[Clinic_Schedule] with (nolock) where clinic_id='" + Convert.ToInt32(specid) + "' and event_start='"+ dt1 + "' ";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlConnection.Open();
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSet2);
                        oSqlConnection.Close();
                        var shed = oDataSet2.AsEnumerable()
                .Select(dataRow => new Clinic_Schedule
                {
                    title = dataRow.Field<string>("title")
                  

                }).ToList();



                    }
                  DateTime dd = DateTime.Now.Date;
                    DataTable oDataSet4 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                  
                    sqlQuery = "   SELECT  a.SubjectID,(a.Present_Complain)pcomoplian,CASE WHEN isnull(b.ServiceStatus,'0')='0'   then (select top 1 ServiceStatus from [MMS].[dbo].[PersonalDetails] where  " +

  "     ServiceNo = c.ServiceNo)  else b.ServiceStatus end ServiceStatus, case when isnull(b.Surname,'0')= '0' then c.Initials + ' ' + c.Surname else b.Surname end     sname    " +
 "  ,CASE WHEN isnull(b.Rank,'0')='0'   then (select top 1 Rank from [MMS].[dbo].[PersonalDetails] where ServiceNo=c.ServiceNo)  else b.Rank end rnkname, " +
 "  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"   , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate, (a.ModifiedDate)mdate,(h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
 "  as b on c.ServiceNo=b.ServiceNo  and b.ServiceType =CASE when c.Service_Type=4 then 1 when c.Service_Type = 5 then 2 else c.Service_Type end left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where   a.PatientCatID!=5 and c.ServiceNo= '" + id+ "' and a.status!=1  " +

 "    order by crdate desc ";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet4);
                    oSqlConnection.Close();
                    var lid = oDataSet4.AsEnumerable()
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
                mdate = dataRow.Field<DateTime?>("mdate"),
                pidp = dataRow.Field<string>("pidp"),
                relasiondet = dataRow.Field<string>("relasiondet"),
                ServiceStatus = dataRow.Field<string>("ServiceStatus"),
                ID = dataRow.Field<int?>("SubjectID"),
                stype = dataRow.Field<int>("Service_Type"),
            }).ToList();

                    var pageNumber = page ?? 1;
                 onePageOfProducts = lid.ToList();
            }
            else
            {
                if (opdid.Contains("CL"))
                {
                    //DateTime dt1 = DateTime.Now.Date;
                    //var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id == specid).Where(p => p.event_start.Value.Day == dt1.Day && p.event_start.Value.Month == dt1.Month && p.event_start.Value.Year == dt1.Year).AsNoTracking() select new { s.title };
                    //int r = 0;
                    //    foreach (var item in shed)
                    //{
                        
                    //        title[r] = item.title;
                    //    r++;
                    //  }


                    //    var patient_Detailn = from s in db.Patient_Detail.Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7 || p.Status == 5).Where(p => p.OPDID == opdid).AsNoTracking() orderby s.CreatedDate descending select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, opddiag = s.OPD_Diagnosis, relasiont = s.Patient.RelationshipType1.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = s.Patient.RelationshipType1.Relationship };

                    //    DateTime dd = DateTime.Now.Date;
                    //var patient_Detail = from s in db.Patient_Detail.Where(p =>p.PDID=="dfdffddf").AsNoTracking() orderby s.CreatedDate descending select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, opddiag = s.OPD_Diagnosis, relasiont =s.Patient.RelationshipType1.RTypeID,crdate=s.CreatedDate , pidp = s.PID, relasiondet = s.Patient.RelationshipType1.Relationship };

                    //    List<getdocdetail> lid = patient_Detail.ToList();

                    //    foreach (var item in patient_Detailn)
                    //    {
                    //        pid = item.pidp;
                    //        PDID = item.pdids;
                    //        Present_Complain = item.pcomoplian;

                    //        creteddate = item.crdate;
                    //        fname = item.fname;
                    //        inililes = item.inililes;
                    //        locdet = item.locdet;
                    //        opddiag = item.opddiag;
                    //        pstatus = item.pstatus;
                    //        relasiondet = item.relasiondet;
                    //        relasiont =item.relasiont;
                    //        rnkname = item.rnkname;
                    //        sname = item.sname;
                    //        sno = item.sno;
                    //        sv = item.sv;
                    //        int? stype = 0;
                    //        string svcno = "";

                    //        var cdlist = from s in db.Patients.Where(p => p.ServiceNo == sno).Where(p => p.RelationshipType == 5).AsNoTracking()
                    //                     orderby s.DateOfBirth
                    //                     select new { s.DateOfBirth, s.PID };

                    //        int hj = 1;
                    //        foreach (var itemchw in cdlist)
                    //        {
                    //            if (itemchw.PID == pid)
                    //            {
                    //                relasiondet = relasiondet + hj;

                    //            }

                    //            hj++;
                    //        }


                    //        var PersonResultList1 = from s in db.PersonalDetails.AsNoTracking()

                    //                                where s.ServiceNo == item.sno
                    //                                select new getdocdetail { sv = sv, pdids = PDID, inililes = s.Initials, sname = s.Surname, sno = s.ServiceNo, rnkname = s.Rank, pcomoplian = Present_Complain, pstatus = pstatus, relasiont = relasiont, crdate = creteddate, pidp = pid, relasiondet = relasiondet };

                    //        //patient_Detail = patient_Detail.Concat(PersonResultList1);
                    //        if (PersonResultList1.Count() > 0)
                    //        {
                    //            foreach (var iteme in PersonResultList1)
                    //            {

                    //                lid.Add(iteme);
                    //                break;
                    //            }
                    //        }





                    //        else
                    //        {
                    //            var patient_Detail2n = from s in db.Patient_Detail.Where(p => p.PDID == item.pdids).AsNoTracking()
                    //                                   select new getdocdetail { sv = s.Patient.Service_Type, pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = s.Patient.RelationshipType1.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = s.Patient.RelationshipType1.Relationship };
                    //            //patient_Detail = patient_Detail .Concat(patient_Detail2n);


                    //            foreach (var iteme in patient_Detail2n)
                    //            {

                    //                lid.Add(iteme);
                    //            }
                    //        }
                    //    }
                       

                    //    //db.Patient_Detail.Include(p => p.Patient).Where (p=>title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7 || p.Status == 5).Where(p => p.OPDID == opdid).OrderByDescending(p => p.CreatedDate);
                    //    var pageNumber = page ?? 1;
                    //onePageOfProducts = lid.OrderByDescending(p => p.crdate).ToPagedList(pageNumber, 10); 

                }
                else
                {
                    DateTime dd = DateTime.Now.Date;

                        DataTable oDataSet3 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "       SELECT  a.SubjectID,(a.Present_Complain)pcomoplian,CASE WHEN isnull(b.ServiceStatus,'0')='0'   then (select top 1 ServiceStatus from [MMS].[dbo].[PersonalDetails] where  " +

  "     ServiceNo = c.ServiceNo)  else b.ServiceStatus end ServiceStatus, case when isnull(b.Surname,'0')= '0' then c.Initials + ' ' + c.Surname else b.Surname end     sname   " +
 "  ,CASE WHEN isnull(b.Rank,'0')='0'   then (select top 1 Rank from [MMS].[dbo].[PersonalDetails] where ServiceNo=c.ServiceNo)  else b.Rank end rnkname, " +
 "  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
 "  , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate,(a.ModifiedDate)mdate, (h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
 "   as b on c.ServiceNo=b.ServiceNo and b.ServiceType =CASE when c.Service_Type=4 then 1 when c.Service_Type = 5 then 2 else c.Service_Type end left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" where convert(date, a.[CreatedDate]) =CONVERT(varchar,'" +dd.ToShortDateString()+ "',111)  and (a.status= 2 or a.status= 5) and a.SubjectID!=1 and a.PatientCatID!=5  " +
" and a.opdid='"+opdid+ "'  order by crdate desc ";
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
                    mdate = dataRow.Field<DateTime?>("mdate"),
                    pidp = dataRow.Field<string>("pidp"),
                    relasiondet = dataRow.Field<string>("relasiondet"),
                    ServiceStatus = dataRow.Field<string>("ServiceStatus"),
                    stype = dataRow.Field<int>("Service_Type"),
                    ID = dataRow.Field<int?>("SubjectID"),
                }).ToList();



                        ///db.Patient_Detail.Include(p => p.Patient).Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                        //  patient_Detail = patient_Detail.GroupBy(t => t.pdids).Select(grp => grp.FirstOrDefault()).OrderByDescending(s=>s.crdate);
                        var pageNumber = page ?? 1;

                        onePageOfProducts = lid.ToList();
                }


            }
                //var pageNumber = page ?? 1;
                // var onePageOfProducts = pOR_Header.ToPagedList(pageNumber, 10);
               
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
        //throw;
    }
}

        public ActionResult printpat(int? page, string id, string currentFilter)
        {
            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
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
                    var patient_Detail = from s in db.Patient_Detail.Where(p => p.Patient.ServiceNo.Contains(id))
                                         join y in db.Patients on s.PID equals y.PID
                                         join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID
                                         orderby s.CreatedDate descending

                                         select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };
                    //   patient_Detail = patient_Detail.GroupBy(t => t.pdids).Select(grp => grp.FirstOrDefault()).OrderByDescending(s => s.crdate );
                    //db.Patient_Detail.Include(p => p.Patient).Include(p => p.Status1).Where(p => p.Patient.ServiceNo.Contains(id)).OrderByDescending(p => p.CreatedDate);

                    var pageNumber = page ?? 1;
                    onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
                }
                else
                {
                    if (opdid.Contains("CL"))
                    {
                        //DateTime dt1 = DateTime.Now.Date;
                        //var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id == specid).Where(p => p.event_start.Value.Day == dt1.Day && p.event_start.Value.Month == dt1.Month && p.event_start.Value.Year == dt1.Year) select new { s.title };
                        //int r = 0;
                        //foreach (var item in shed)
                        //{

                        //    title[r] = item.title;
                        //    r++;
                        //}




                        //DateTime dd = DateTime.Now.Date;
                        //var patient_Detail = from s in db.Patient_Detail.Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7 || p.Status == 5).Where(p => p.OPDID == opdid) orderby s.CreatedDate descending select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, opddiag = s.OPD_Diagnosis, relasiont = s.Patient.RelationshipType1.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = s.Patient.RelationshipType1.Relationship };


                        ////db.Patient_Detail.Include(p => p.Patient).Where (p=>title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7 || p.Status == 5).Where(p => p.OPDID == opdid).OrderByDescending(p => p.CreatedDate);
                        //var pageNumber = page ?? 1;
                        //onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);

                    }
                    else
                    {
                        DateTime dd = DateTime.Now.Date;

                        //var df=      from s in db.Patient_Detail.Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate)
                        //      //       join b in db.Patients on s.PID equals b.PID into cs
                        //      //from b in cs.DefaultIfEmpty()

                        //      select new { s.PDID, s.Patient.Initials, s.Patient.Surname, s.Present_Complain, s.Patient.rank1.RNK_NAME, s.CreatedDate, s.OPD_Diagnosis, s.OPDID,s.Status1.StatusDec,s.Patient.PID };





                        var patient_Detail = from s in db.Patient_Detail.Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year)


                                             join y in db.Patients on s.PID equals y.PID
                                             join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID

                                             orderby s.CreatedDate descending
                                             select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = z.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = z.Relationship };

                        ///db.Patient_Detail.Include(p => p.Patient).Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                      //  patient_Detail = patient_Detail.GroupBy(t => t.pdids).Select(grp => grp.FirstOrDefault()).OrderByDescending(s=>s.crdate);
                        var pageNumber = page ?? 1;

                        onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
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
        public ActionResult NurseCreate(int? page, int? page1, string id, string id1,string id3)
        {
            try {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                var onePageOfProducts = (dynamic)null;
            var sickcate = (dynamic)null;
            char[] MyChar = { '/', '"', ' ' };
            string opdid = "";
            string locid = "";
                string swo = "";
                var title = new String[100];
            int specid = 0;
            int userid = Convert.ToInt32(Session["UserID"]);
            //var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //   // locid = item.LocationID;
            //}
            //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
            opdid = (String)Session["userlocid1"];
            //foreach (var item in opd)
            //{

            //   // opdid = item.LOCID;
            //}
                //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                //foreach (var item in serW)
                //{

                //    locid = item.LocationID;
                //}
                 locid=(String) Session["userloc"] ;
                if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
                if (!String.IsNullOrEmpty(id3))
                {
                    id3 = id3.Trim(MyChar);
                }
                DateTime ddx = DateTime.Now.Date;
                DataTable oDataSet9x = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                if (locid=="CBO")
                {
                    sqlQuery = "     SELECT v.Description as dv,count(j.regdate)regdate,count (case when a.status !=1 then (a.status)  end) as st,max(v.DivisionID)divid " +

                          "      FROM[MMS].[dbo].sickreport as j with(nolock)  left join[MMS].[dbo].[Patient_detail] " +
                       "		  as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
                   " left join[MMS].[dbo].[RelationshipType]  " +
                   "			   as h on h.RTypeID=c.RelationshipType inner join Vw_Formation as v on j.OPDID=v.DivisionID and " +

                        "          j.LocationID=v.LocationID where convert(date, j.regdate)   =CONVERT(varchar,'" + ddx.Date.ToString() + "',111) " +

                       "             and(j.LocationID= '"+locid+"' or j.LocationID= 'AHQ') and j.IsMedical!=1 and j.ISDental!=1 group by v.Description order by regdate ";


                }
                else
                {
                    sqlQuery = "     SELECT v.Description as dv,count(j.regdate)regdate,count (case when a.status !=1 then (a.status)  end) as st,max(v.DivisionID)divid " +

      "      FROM[MMS].[dbo].sickreport as j with(nolock)  left join[MMS].[dbo].[Patient_detail] " +
   "		  as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].[RelationshipType]  " +
"			   as h on h.RTypeID=c.RelationshipType inner join Vw_Formation as v on j.OPDID=v.DivisionID and " +

    "          j.LocationID=v.LocationID where convert(date, j.regdate)   =CONVERT(varchar,'" + ddx.Date.ToString() + "',111) " +

   "             and(j.LocationID= '" + locid + "' ) and j.IsMedical!=1 and j.ISDental!=1 group by v.Description order by regdate ";


                }

                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet9x);
                oSqlConnection.Close();
                var sicdtr = oDataSet9x.AsEnumerable()
        .Select(dataRow => new sickparade
        {
            section = dataRow.Field<string>("dv"),
            sickcount = dataRow.Field<int?>("regdate"),
            comcount = dataRow.Field<int?>("st"),
            divid = dataRow.Field<string>("divid"),
          
        }).ToList();


                ViewBag.sicpr = sicdtr;
                if (!String.IsNullOrEmpty(id3))
                {
                    DateTime dt1x = DateTime.Now.Date;
                    DataTable oDataSet3 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    if (locid == "CBO")
                    {

                        sqlQuery = "    SELECT v.Description as dv,b.ServiceType,(j.age)age,(j.isduty)isduty, " +
 "  (j.isliveout)isliveout,(j.islow)islow,(j.service)service,(j.IsMedical)IsMedical, " +
  " (k.CatPeriod)catdate,  (f.Category_Type)cattp,  COALESCE(NULLIF((case when c.RelationshipType = 1 " +
 "  and b.Surname != '0'  then b.Surname end), ''), (c.surname))     sname  ,(case when c.RelationshipType = 1 then b.Rank end) " +
  " rnkname, (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)   " +
 "  inililes, (c.RelationshipType)relasiont    , (c.pid)pidp, (a.pdid)pdids,(a.status) " +
 " pstatus,(j.regdate)regdate, (h.Relationship)relasiondet FROM[MMS].[dbo].sickreport as j with(nolock) " +
  " left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
 " left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type=b.ServiceType" +

 " left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
 " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType inner join Vw_Formation as v on j.OPDID=v.DivisionID and j.LocationID=v.LocationID where   " +

"  v.DivisionID='" + id3 + "' and convert(date, j.regdate) " +
"  =CONVERT(varchar,'" + dt1x.Date.ToString() + "',111) and j.IsMedical!=1 and j.ISDental!=1 and(j.LocationID= 'CBO' or j.LocationID= 'AHQ')  order by b.RankID desc ";

                    }
                   else
                    {

                        sqlQuery = "    SELECT v.Description as dv,b.ServiceType,(j.age)age,(j.isduty)isduty, " +
 "  (j.isliveout)isliveout,(j.islow)islow,(j.service)service,(j.IsMedical)IsMedical, " +
  " (k.CatPeriod)catdate,  (f.Category_Type)cattp,  COALESCE(NULLIF((case when c.RelationshipType = 1 " +
 "  and b.Surname != '0'  then b.Surname end), ''), (c.surname))     sname  ,(case when c.RelationshipType = 1 then b.Rank end) " +
  " rnkname, (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)   " +
 "  inililes, (c.RelationshipType)relasiont    , (c.pid)pidp, (a.pdid)pdids,(a.status) " +
 " pstatus,(j.regdate)regdate, (h.Relationship)relasiondet FROM[MMS].[dbo].sickreport as j with(nolock) " +
  " left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
 " left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type=b.ServiceType" +

 " left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
 " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType inner join Vw_Formation as v on j.OPDID=v.DivisionID and j.LocationID=v.LocationID where  j.IsMedical!=1 and j.ISDental!=1 and " +

"  v.DivisionID='" + id3 + "' and convert(date, j.regdate) " +
"  =CONVERT(varchar,'" + dt1x.Date.ToString() + "',111) and j.IsMedical!=1 and j.ISDental!=1  and(j.LocationID= '"+locid+"' or j.LocationID= 'AHQ') order by b.RankID desc ";

                    }
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
                svt = dataRow.Field<int?>("ServiceType"),
                rt = dataRow.Field<int?>("relasiont"),
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
                stat = dataRow.Field<int>("pstatus").ToString(),
                IsMedical = dataRow.Field<int>("IsMedical").ToString()
            }).ToList();

                    var pageNumber = page ?? 1;
                    onePageOfProducts = sicdt.ToList();


                }

            else    if (!String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Now.Date;
                if (opdid.Contains("cl"))
                {
                    var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id == Convert.ToInt32(specid)).Where(p => p.event_start == dt1) select new { s.title };
                }
                DateTime dd = DateTime.Now.Date;
                    var patient_Detailn = (dynamic)null;
                    if (locid == "CBO")
                    {
                        DataTable oDataSet3 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "    SELECT v.Description as dv,b.ServiceType,(j.age)age,(j.isduty)isduty, " +
     "  (j.isliveout)isliveout,(j.islow)islow,(j.service)service,(j.IsMedical)IsMedical, " +
      " (k.CatPeriod)catdate,  (f.Category_Type)cattp,  COALESCE(NULLIF((case when c.RelationshipType = 1 " +
     "  and b.Surname != '0'  then b.Surname end), ''), (c.surname))     sname  ,(case when c.RelationshipType = 1 then b.Rank end) " +
      " rnkname, (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)   " +
     "  inililes, (c.RelationshipType)relasiont    , (c.pid)pidp, (a.pdid)pdids,(a.status) " +
     " pstatus,(j.regdate)regdate, (h.Relationship)relasiondet FROM[MMS].[dbo].sickreport as j with(nolock) " +
      " left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
     " left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type=b.ServiceType" +

     " left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
     " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType inner join Vw_Formation as v on j.OPDID=v.DivisionID and j.LocationID=v.LocationID where (j.LocationID='" + locid + "' or j.LocationID='AHQ') and j.IsMedical!=1 and j.ISDental!=1 and " +

   "  b.ServiceNo='" + id + "'   order by b.RankID desc";
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
                    svt = dataRow.Field<int?>("ServiceType"),
                    rt = dataRow.Field<int?>("relasiont"),
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
                    stat = dataRow.Field<int>("pstatus").ToString(),
                    IsMedical = dataRow.Field<int>("IsMedical").ToString()
                }).ToList();

                        var pageNumber = page ?? 1;
                        onePageOfProducts = sicdt.ToList();

                    }
                    else
                    {
                        DataTable oDataSet3 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "    SELECT v.Description as dv,b.ServiceType,(j.age)age,(j.isduty)isduty, " +
      "  (j.isliveout)isliveout,(j.islow)islow,(j.service)service,(j.IsMedical)IsMedical, " +
       " (k.CatPeriod)catdate,  (f.Category_Type)cattp,  COALESCE(NULLIF((case when c.RelationshipType = 1 " +
      "  and b.Surname != '0'  then b.Surname end), ''), (c.surname))     sname  ,(case when c.RelationshipType = 1 then b.Rank end) " +
       " rnkname, (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)   " +
      "  inililes, (c.RelationshipType)relasiont    , (c.pid)pidp, (a.pdid)pdids,(a.status) " +
      " pstatus,(j.regdate)regdate, (h.Relationship)relasiondet FROM[MMS].[dbo].sickreport as j with(nolock) " +
       " left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
      " left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type=b.ServiceType" +

      " left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
      " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType inner join Vw_Formation as v on j.OPDID=v.DivisionID and j.LocationID=v.LocationID where j.LocationID='" + locid + "' and j.IsMedical!=1 and  j.ISDental!=1 and " +

    "  b.ServiceNo='" + id + "'    order by b.RankID desc";
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
                    svt = dataRow.Field<int?>("ServiceType"),
                    rt = dataRow.Field<int?>("relasiont"),
                   
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
                    stat = dataRow.Field<int>("pstatus").ToString(),
                    IsMedical = dataRow.Field<int>("IsMedical").ToString()
                }).ToList();
                        var pageNumber = page ?? 1;
                        onePageOfProducts = sicdt.ToList();
                    }


                  

                    // var patient_Detail = db.Patient_Detail.Include(p => p.Patient).Include(p => p.Status1).Where(p => p.Patient.ServiceNo.Contains(id)).OrderByDescending(p => p.CreatedDate);
                  
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




                    DateTime dd = DateTime.Now.Date;
                    var patient_Detail = db.Patient_Detail.Include(p => p.Patient).Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).OrderByDescending(p => p.CreatedDate);
                    var pageNumber = page ?? 1;
                    onePageOfProducts = patient_Detail.ToList();

                }
                else
                {


                    DateTime dd = DateTime.Now.Date;
                       

                        var patient_Detailn = (dynamic)null;
                        if (locid == "CBO")
                         {
                            DataTable oDataSet3 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "    SELECT v.Description as dv, b.ServiceType,(j.age)age,(j.isduty)isduty, " +
"  (j.isliveout)isliveout,(j.islow)islow,(j.service)service,(j.IsMedical)IsMedical, " +
 " (k.CatPeriod)catdate,  (f.Category_Type)cattp,  COALESCE(NULLIF((case when c.RelationshipType = 1 " +
"  and b.Surname != '0'  then b.Surname end), ''), (c.surname))     sname  ,(case when c.RelationshipType = 1 then b.Rank end) " +
 " rnkname, (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)   " +
"  inililes, (c.RelationshipType)relasiont    , (c.pid)pidp, (a.pdid)pdids,(a.status) " +
" pstatus,(j.regdate)regdate, (h.Relationship)relasiondet FROM[MMS].[dbo].sickreport as j with(nolock) " +
 " left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
" left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type=b.ServiceType " +

" left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType inner join Vw_Formation as v on j.OPDID=v.DivisionID and j.LocationID=v.LocationID where convert(date, j.regdate) " +
"  =CONVERT(varchar,'"+ dd.Date.ToString() + "',111)  and(j.LocationID= 'CBO' or j.LocationID= 'AHQ') and j.IsMedical!=1 and j.ISDental!=1  order by b.RankID desc ";
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
                        svt = dataRow.Field<int?>("ServiceType"),
                        rt = dataRow.Field<int?>("relasiont"),
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
                        stat = dataRow.Field<int>("pstatus").ToString(),
                        IsMedical = dataRow.Field<int>("IsMedical").ToString()
                    }).ToList();
                            var pageNumber = page ?? 1;
                            onePageOfProducts = sicdt.ToList();

                        }
                        else
                        {


                           

                            DataTable oDataSet3 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "    SELECT b.ServiceType,(j.age)age,(j.isduty)isduty, " +
 "  (j.isliveout)isliveout,(j.islow)islow,(j.service)service,(j.IsMedical)IsMedical, " +
  " (k.CatPeriod)catdate,  (f.Category_Type)cattp,  COALESCE(NULLIF((case when c.RelationshipType = 1 " +
 "  and b.Surname != '0'  then b.Surname end), ''), (c.surname))     sname  ,(case when c.RelationshipType = 1 then b.Rank end) " +
  " rnkname, (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)   " +
 "  inililes, (c.RelationshipType)relasiont    , (c.pid)pidp, (a.pdid)pdids,(a.status) " +
 " pstatus,(j.regdate)regdate, (h.Relationship)relasiondet FROM[MMS].[dbo].sickreport as j with(nolock) " +
  " left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
 " left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type=b.ServiceType" +

 " left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
 " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where convert(date, j.regdate) " +
 "  =CONVERT(varchar,'" + dd.Date.ToString() + "',111)  and(j.LocationID= '"+locid+ "' ) and j.IsMedical!=1  and j.ISDental!=1 order by b.RankID desc";
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
                        svt = dataRow.Field<int?>("ServiceType"),
                        rt = dataRow.Field<int?>("relasiont"),
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
                        stat = dataRow.Field<int>("pstatus").ToString(),
                        IsMedical = dataRow.Field<int>("IsMedical").ToString()
                    }).ToList();

                            var pageNumber = page ?? 1;
                            onePageOfProducts = sicdt.ToList();
                        }


                    

                    
                }
            }

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Users");
                //throw;
            }

        }

        public ActionResult purchcreate(int? page, int? page1, string id, string id1)
        {
            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                var onePageOfProducts = (dynamic)null;
                var sickcate = (dynamic)null;
                char[] MyChar = { '/', '"', ' ' };
                string opdid = "";
                string locid = "";
                string swo = "";
                var title = new String[100];
                int specid = 0;
                int userid = Convert.ToInt32(Session["UserID"]);
                //var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

                //foreach (var item in ser)
                //{

                //   // locid = item.LocationID;
                //}
                //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
                opdid = (String)Session["userlocid1"];
                //foreach (var item in opd)
                //{

                //   // opdid = item.LOCID;
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
                    var patient_Detailn = (dynamic)null;
                    if (locid == "CBO")
                    {
                        DataTable oDataSet3 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "    SELECT b.ServiceType,(j.age)age,(j.isduty)isduty, " +
     "  (j.isliveout)isliveout,(j.islow)islow,(j.service)service,(j.IsMedical)IsMedical, " +
      " (k.CatPeriod)catdate,  (f.Category_Type)cattp,  COALESCE(NULLIF((case when c.RelationshipType = 1 " +
     "  and b.Surname != '0'  then b.Surname end), ''), (c.surname))     sname  ,(case when c.RelationshipType = 1 then b.Rank end) " +
      " rnkname, (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)   " +
     "  inililes, (c.RelationshipType)relasiont    , (c.pid)pidp, (a.pdid)pdids,(a.status) " +
     " pstatus,(j.regdate)regdate, (h.Relationship)relasiondet FROM[MMS].[dbo].sickreport as j with(nolock) " +
      " left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
     " left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type=b.ServiceType" +

     " left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
     " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where (j.LocationID='" + locid + "' or j.LocationID='AHQ') and j.IsMedical!=1 and " +

   "  b.ServiceNo='" + id + "'   order by regdate";
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
                    svt = dataRow.Field<int?>("ServiceType"),
                    rt = dataRow.Field<int?>("relasiont"),
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
                    stat = dataRow.Field<int>("pstatus").ToString(),
                    IsMedical = dataRow.Field<int>("IsMedical").ToString()
                }).ToList();

                        var pageNumber = page ?? 1;
                        onePageOfProducts = sicdt.OrderByDescending(p => p.regdate).ToPagedList(pageNumber, 10);

                    }
                    else
                    {
                        DataTable oDataSet3 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "    SELECT b.ServiceType,(j.age)age,(j.isduty)isduty, " +
      "  (j.isliveout)isliveout,(j.islow)islow,(j.service)service,(j.IsMedical)IsMedical, " +
       " (k.CatPeriod)catdate,  (f.Category_Type)cattp,  COALESCE(NULLIF((case when c.RelationshipType = 1 " +
      "  and b.Surname != '0'  then b.Surname end), ''), (c.surname))     sname  ,(case when c.RelationshipType = 1 then b.Rank end) " +
       " rnkname, (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)   " +
      "  inililes, (c.RelationshipType)relasiont    , (c.pid)pidp, (a.pdid)pdids,(a.status) " +
      " pstatus,(j.regdate)regdate, (h.Relationship)relasiondet FROM[MMS].[dbo].sickreport as j with(nolock) " +
       " left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
      " left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type=b.ServiceType" +

      " left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
      " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where j.LocationID='" + locid + "' and j.IsMedical!=1 and " +

    "  b.ServiceNo='" + id + "'    order by regdate";
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
                    svt = dataRow.Field<int?>("ServiceType"),
                    rt = dataRow.Field<int?>("relasiont"),

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
                    stat = dataRow.Field<int>("pstatus").ToString(),
                    IsMedical = dataRow.Field<int>("IsMedical").ToString()
                }).ToList();
                        var pageNumber = page ?? 1;
                        onePageOfProducts = sicdt.OrderByDescending(p => p.regdate).ToPagedList(pageNumber, 10);
                    }




                    // var patient_Detail = db.Patient_Detail.Include(p => p.Patient).Include(p => p.Status1).Where(p => p.Patient.ServiceNo.Contains(id)).OrderByDescending(p => p.CreatedDate);

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




                        DateTime dd = DateTime.Now.Date;
                        var patient_Detail = db.Patient_Detail.Include(p => p.Patient).Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).OrderByDescending(p => p.CreatedDate);
                        var pageNumber = page ?? 1;
                        onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);

                    }
                    else
                    {


                        DateTime dd = DateTime.Now.Date;


                        var patient_Detailn = (dynamic)null;
                        if (locid == "CBO")
                        {
                            DataTable oDataSet3 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "    SELECT b.ServiceType,(j.age)age,(j.isduty)isduty, " +
"  (j.isliveout)isliveout,(j.islow)islow,(j.service)service,(j.IsMedical)IsMedical, " +
 " (k.CatPeriod)catdate,  (f.Category_Type)cattp,  COALESCE(NULLIF((case when c.RelationshipType = 1 " +
"  and b.Surname != '0'  then b.Surname end), ''), (c.surname))     sname  ,(case when c.RelationshipType = 1 then b.Rank end) " +
 " rnkname, (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)   " +
"  inililes, (c.RelationshipType)relasiont    , (c.pid)pidp, (a.pdid)pdids,(a.status) " +
" pstatus,(j.regdate)regdate, (h.Relationship)relasiondet FROM[MMS].[dbo].sickreport as j with(nolock) " +
 " left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
" left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type=b.ServiceType " +

" left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where convert(date, j.regdate) " +
"  =CONVERT(varchar,'" + dd.Date.ToString() + "',111)  and(j.LocationID= 'CBO' or j.LocationID= 'AHQ') and j.IsMedical!=1   order by regdate ";
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
                        svt = dataRow.Field<int?>("ServiceType"),
                        rt = dataRow.Field<int?>("relasiont"),
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
                        stat = dataRow.Field<int>("pstatus").ToString(),
                        IsMedical = dataRow.Field<int>("IsMedical").ToString()
                    }).ToList();
                            var pageNumber = page ?? 1;
                            onePageOfProducts = sicdt.OrderByDescending(p => p.regdate).ToPagedList(pageNumber, 10);

                        }
                        else
                        {




                            DataTable oDataSet3 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "    SELECT b.ServiceType,(j.age)age,(j.isduty)isduty, " +
 "  (j.isliveout)isliveout,(j.islow)islow,(j.service)service,(j.IsMedical)IsMedical, " +
  " (k.CatPeriod)catdate,  (f.Category_Type)cattp,  COALESCE(NULLIF((case when c.RelationshipType = 1 " +
 "  and b.Surname != '0'  then b.Surname end), ''), (c.surname))     sname  ,(case when c.RelationshipType = 1 then b.Rank end) " +
  " rnkname, (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)   " +
 "  inililes, (c.RelationshipType)relasiont    , (c.pid)pidp, (a.pdid)pdids,(a.status) " +
 " pstatus,(j.regdate)regdate, (h.Relationship)relasiondet FROM[MMS].[dbo].sickreport as j with(nolock) " +
  " left join[MMS].[dbo].[Patient_detail] as a on a.pdid=j.PDID left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
 " left join[MMS].[dbo].Sick_Category as k on a.PDID=k.PDID left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type=b.ServiceType" +

 " left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].Sick_CategoryType as f on k.CatID=f.CatID " +
 " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where convert(date, j.regdate) " +
 "  =CONVERT(varchar,'" + dd.Date.ToString() + "',111)  and(j.LocationID= '" + locid + "' ) and j.IsMedical!=1   order by regdate";
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
                        svt = dataRow.Field<int?>("ServiceType"),
                        rt = dataRow.Field<int?>("relasiont"),
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
                        stat = dataRow.Field<int>("pstatus").ToString(),
                        IsMedical = dataRow.Field<int>("IsMedical").ToString()
                    }).ToList();

                            var pageNumber = page ?? 1;
                            onePageOfProducts = sicdt.OrderByDescending(p => p.regdate).ToPagedList(pageNumber, 10);
                        }





                    }
                }

                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Users");
                //throw;
            }

        }
        public JsonResult viewsick1(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);
           

            var ser = from s in db.Sick_Category.Where(p => p.CatIndex == NewString) select new { s.Patient_Detail.Patient.Initials, s.Patient_Detail.Patient.ServiceNo, s.Patient_Detail.Patient.Surname, s.CatPeriod };

            return Json(ser, JsonRequestBehavior.AllowGet);
        }
      
        // POST: Patient_Detail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PDID,PID,Present_Complain,History_PresentComplain,Other_Complain,History_OtherComplain,Special_Sickness,Hypersensivity,Examination,OPD_Diagnosis,Status,CreatedBy,CreatedDate,CreatedMachine,ModifiedBy,ModifiedDate,ModifiedMachine,OPDID,DocSID,Treatment")] Patient_Detail patient_Detail)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            if (ModelState.IsValid)
            {
                db.Patient_Detail.Add(patient_Detail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OPDID = new SelectList(db.OPD_Master, "OPD_ID", "CurrentNo", patient_Detail.OPDID);
            ViewBag.PID = new SelectList(db.Patients, "PID", "LocationID", patient_Detail.PID);
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec", patient_Detail.Status);
            return View(patient_Detail);
        }

        // GET: Patient_Detail/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient_Detail patient_Detail = await db.Patient_Detail.FindAsync(id);
            if (patient_Detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OPDID = new SelectList(db.OPD_Master, "OPD_ID", "CurrentNo", patient_Detail.OPDID);
            ViewBag.PID = new SelectList(db.Patients, "PID", "LocationID", patient_Detail.PID);
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec", patient_Detail.Status);
            return View(patient_Detail);
        }

        // POST: Patient_Detail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PDID,PID,Present_Complain,History_PresentComplain,Other_Complain,History_OtherComplain,Special_Sickness,Hypersensivity,Examination,OPD_Diagnosis,Status,CreatedBy,CreatedDate,CreatedMachine,ModifiedBy,ModifiedDate,ModifiedMachine,OPDID,DocSID,Treatment")] Patient_Detail patient_Detail)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            if (ModelState.IsValid)
            {
                db.Entry(patient_Detail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OPDID = new SelectList(db.OPD_Master, "OPD_ID", "CurrentNo", patient_Detail.OPDID);
            ViewBag.PID = new SelectList(db.Patients, "PID", "LocationID", patient_Detail.PID);
            ViewBag.Status = new SelectList(db.Status, "Status1", "StatusDec", patient_Detail.Status);
            return View(patient_Detail);
        }

        // GET: Patient_Detail/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient_Detail patient_Detail = await db.Patient_Detail.FindAsync(id);
            if (patient_Detail == null)
            {
                return HttpNotFound();
            }
            return View(patient_Detail);
        }

        // POST: Patient_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            Patient_Detail patient_Detail = await db.Patient_Detail.FindAsync(id);
            db.Patient_Detail.Remove(patient_Detail);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Vital Types
        public JsonResult GetVitalTypes()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            var types = db.Vital_Type.Select(x => new { x.VTID, x.VitalType }).ToList();
            return Json(types, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetpatiantType()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            var types = db.Patient_Category.Select(x => new { x.CategoryID, x.CategoryDescription }).ToList();
            return Json(types, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetpatiantsubType()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            var types = db.Patient_SubCategory.Select(x => new { x.SubCategoryID, x.SubCategoryDescription }).ToList();
            return Json(types, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetHyperTypes()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            var Hypertypes = db.HypMainCategories.Select(x => new { x.HypersenceMainCatID, x.HypersenceMainCategory }).ToList();
            return Json(Hypertypes, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetSeverityTypes()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            var Severitytypes = db.HypersenseSeverties.Select(x => new { x.SevertyID, x.SevertyType }).ToList();
            return Json(Severitytypes, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetMethod()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select MethodID,MethodDetail from [dbo].[DrugMethod]  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var empList = oDataSet.AsEnumerable()
    .Select(dataRow => new DrugMethod
    {
        MethodID = dataRow.Field<int>("MethodID"),
         MethodDetail = dataRow.Field<string>("MethodDetail")
    }).ToList();
            // var Methods = db.DrugMethods.Select(x => new { x.MethodID, x.MethodDetail }).ToList();
            return Json(empList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Getmes()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select Category,MedCatID from [dbo].[MBP_Category]  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var empList = oDataSet.AsEnumerable()
    .Select(dataRow => new MBP_Category
    {
        Category = dataRow.Field<string>("Category"),
        MedCatID = dataRow.Field<int>("MedCatID")
    }).ToList();
            // var Methods = db.DrugMethods.Select(x => new { x.MethodID, x.MethodDetail }).ToList();
            return Json(empList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetRoute()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select RouteID,RouteDetail from [dbo].[DrugRoute]  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var empList = oDataSet.AsEnumerable()
    .Select(dataRow => new DrugRoute
    {
        RouteID = dataRow.Field<int>("RouteID"),
        RouteDetail = dataRow.Field<string>("RouteDetail")
    }).ToList();
            //var Route = db.DrugRoutes.Select(x => new { x.RouteID, x.RouteDetail }).ToList();
            return Json(empList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getdgn()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select dgid,dgdetail from [dbo].[CatDaignosis]  where dgstatus='1' ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var empList = oDataSet.AsEnumerable()
    .Select(dataRow => new CatDaignosi
    {
        dgid = dataRow.Field<string>("dgid"),
        dgdetail = dataRow.Field<string>("dgdetail")
    }).ToList();


          //  var status = db.CatDaignosis.Select(x => new { x.dgid, x.dgdetail }).ToList();
            return Json(empList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetSt()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select Status,StatusDec from [dbo].[Status] where Active=1 ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var empList = oDataSet.AsEnumerable()
    .Select(dataRow => new Status
    {
        Status1 = dataRow.Field<int>("Status"),
        StatusDec = dataRow.Field<string>("StatusDec")
    }).ToList();


            // var status = db.Status.Where(x =>x.Active==1).Select(x => new { x.Status1, x.StatusDec }).ToList();
            return Json(empList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetDrugtime()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select StockType,StockTypeID from [dbo].[DrugStockType]  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var empList = oDataSet.AsEnumerable()
    .Select(dataRow => new DrugStockType
    {
        StockType = dataRow.Field<string>("StockType"),
        StockTypeID = dataRow.Field<int>("StockTypeID")
    }).ToList();


            //var status = db.DrugStockTypes.Select(x => new { x.StockType, x.StockTypeID }).ToList();
            return Json(empList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getperdose()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select GroupID,DrugGroupName,DrugOrder from [dbo].[DrugGroup]  order by DrugOrder";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var empList = oDataSet.AsEnumerable()
    .Select(dataRow => new DrugGroup
    {
        GroupID = dataRow.Field<int>("GroupID"),
        DrugGroupName = dataRow.Field<string>("DrugGroupName"),
        DrugOrder = dataRow.Field<int>("DrugOrder")
    }).ToList();



           // var status = db.DrugGroups.Select(x => new { x.GroupID, x.DrugGroupName,x.DrugOrder }).OrderBy(x => x.DrugOrder).ToList();
            return Json(empList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetDrug()
        {try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select itemno,itemdescription from [dbo].[EPASPharmacyItems]  ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet);
                oSqlConnection.Close();
                var q1 = oDataSet.AsEnumerable()
        .Select(dataRow => new getdrugl
        {
            itemdescription = dataRow.Field<string>("itemdescription"),
            itemno = dataRow.Field<string>("itemno")

        }).ToList();

                DataTable oDataSet1 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select DrugID,ItemDescription from [dbo].[DrugItems]  ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet1);
                oSqlConnection.Close();
                var q2 = oDataSet1.AsEnumerable()
        .Select(dataRow => new getdrugl
        {
            itemdescription = dataRow.Field<string>("ItemDescription"),
            itemno = dataRow.Field<Int64>("DrugID").ToString()

        }).ToList();
                //var q1 =   from d in dbepas.EPASPharmacyItems select new getdrugl{ itemdescription=d.itemdescription, itemno=d.itemno };
                // var q2 = from f in db.DrugItems select new getdrugl{ itemdescription=f.ItemDescription, itemno=f.DrugID.ToString() };

                var p1 = q1.ToList();
                var p2 = q2.ToList();
                var joinednew = p1.Concat(p2);


                return Json(joinednew, JsonRequestBehavior.AllowGet);

                //var GetDrug = dbepas.EPASPharmacyItems.Select(x => new { x.itemno, x.itemdescription }).ToList();
                //return Json(GetDrug, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetMedkw()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select MKID,MKDetail from [dbo].[MedKeywords]  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var q1 = oDataSet.AsEnumerable()
    .Select(dataRow => new MedKeyword
    {
        MKID = dataRow.Field<long>("MKID"),
        MKDetail = dataRow.Field<string>("MKDetail")

    }).ToList();


           // var GetMedkw = db.MedKeywords.Select(x => new { x.MKID, x.MKDetail }).ToList();
            return Json(q1, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Getlablist()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select CategoryID,CategoryName,TubeCategory from [dbo].[Lab_MainCategory]  where status =1 ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var q1 = oDataSet.AsEnumerable()
    .Select(dataRow => new Lab_MainCategory
    {
        CategoryID = dataRow.Field<string>("CategoryID"),
        CategoryName = dataRow.Field<string>("CategoryName"),
         TubeCategory = dataRow.Field<int?>("TubeCategory")
    }).ToList();


          //  var Getlablist = db.Lab_MainCategory.Select(x => new { x.CategoryID, x.CategoryName }).ToList();
            return Json(q1, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getsmslablist()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
           
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select Catid,CatName from [dbo].[Lab_sms_Cat]  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var q1 = oDataSet.AsEnumerable()
    .Select(dataRow => new Lab_sms_Cat
    {
        Catid = dataRow.Field<int>("Catid"),
        CatName = dataRow.Field<string>("CatName")

    }).ToList();



           // var Getlablist = db.Lab_sms_Cat.Select(x => new { x.Catid, x.CatName }).ToList();
            return Json(q1, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetClinics()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select ClinicTypeID,ClinicDetails from [dbo].[Clinic_Type]  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var q1 = oDataSet.AsEnumerable()
    .Select(dataRow => new Clinic_Type
    {
        ClinicTypeID = dataRow.Field<int>("ClinicTypeID"),
        ClinicDetails = dataRow.Field<string>("ClinicDetails")

    }).ToList();

          //  var GetClinic = db.Clinic_Type.Select(x => new { x.ClinicTypeID, x.ClinicDetails }).ToList();
            return Json(q1, JsonRequestBehavior.AllowGet);

        }
        
        public JsonResult GetSicktype()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select CatID,Category from [dbo].[Sick_Type]  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var q1 = oDataSet.AsEnumerable()
    .Select(dataRow => new Sick_Type
    {
        CatID = dataRow.Field<int>("CatID"),
        Category = dataRow.Field<string>("Category")

    }).ToList();


           // var GetSicktype = db.Sick_Type.Select(x => new { x.CatID, x.Category }).ToList();
            return Json(q1, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetHyperReactType()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select HypRMainID,HypRMainDetail from [dbo].[HypRMainCategory]  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var q1 = oDataSet.AsEnumerable()
    .Select(dataRow => new HypRMainCategory
    {
        HypRMainID = dataRow.Field<string>("HypRMainID"),
        HypRMainDetail = dataRow.Field<string>("HypRMainDetail")

    }).ToList();


          //  var ReactType = db.HypRMainCategories.Select(x => new { x.HypRMainID, x.HypRMainDetail }).ToList();
            return Json(q1, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetHyperSubType(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);

            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select HyperType,HyperTypeID,HyperSubType from [dbo].[HypersensivityType] where HyperType='"+NewString+"' ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var q1 = oDataSet.AsEnumerable()
    .Select(dataRow => new HypersensivityType
    {
        HyperType = dataRow.Field<string>("HyperType"),
        HyperTypeID = dataRow.Field<string>("HyperTypeID"),
        HyperSubType = dataRow.Field<string>("HyperSubType")
    }).ToList();



           // var HyperSubtypes = from s in db.HypersensivityTypes.Where(p => p.HyperType == NewString) select new { s.HyperType, s.HyperTypeID, s.HyperSubType };
            return Json(q1, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetReactSubType(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);
          

            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select ReactID,RSubcategory,RCategory from [dbo].[HypersenseReaction] where RCategory='" + NewString + "' ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var q1 = oDataSet.AsEnumerable()
    .Select(dataRow => new HypersenseReaction
    {
        ReactID = dataRow.Field<int>("ReactID"),
        RSubcategory = dataRow.Field<string>("RSubcategory"),
        RCategory = dataRow.Field<string>("RCategory")
    }).ToList();





            //  var HyperSubtypes = from s in db.HypersenseReactions.Where(p => p.RCategory == NewString) select new { s.ReactID, s.RSubcategory, s.RCategory };
            return Json(q1, JsonRequestBehavior.AllowGet);

        }
        public JsonResult delPatient(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string err = "";
            char[] MyChar = { '/', '"', ' ' };
            try {
                string NewString = id.Trim(MyChar);
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "update  Patient_Detail  set status='8' where pdid='" + NewString + "'   and ModifiedBy is null;";
                 
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                //oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                //oSqlDataAdapter.Fill(oDataSet);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();


                err = "Deleted";




            }
            catch (Exception ex)
            {
                err = "Cannot delete";
            }
            return Json(err, JsonRequestBehavior.AllowGet);

        }
        public JsonResult delregdrug(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string err = "";
            char[] MyChar = { '/', '"', ' ' };
            try
            {
                string NewString = id.Trim(MyChar);
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "delete from Drug_Regular where Ps_Index='" + NewString + "'  ;";
                   
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                //oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                //oSqlDataAdapter.Fill(oDataSet);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();


                err = "Deleted";




            }
            catch (Exception ex)
            {
                err = "Cannot delete";
            }
            return Json(err, JsonRequestBehavior.AllowGet);

        }
        public JsonResult deldrug(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string err = "";
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);

            try
            {
               
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "delete from Drug_Prescription where Ps_Index='" + NewString + "' and Issued=0";
                  
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                //oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                //oSqlDataAdapter.Fill(oDataSet);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();


                err = "Deleted";




            }
            catch (Exception ex)
            {
                err = "Cannot delete Drug already issued";
            }




            //var Abdominalvar = from s in db.Drug_Prescription join b in db.Patient_Detail on s.PDID equals b.PDID where(s.Issued==0 && b.Status==1&& s.Ps_Index == NewString) select new { s.PDID };

            //if (Abdominalvar.Count() > 0)
            //{
            //    Drug_Prescription patient_Detail = db.Drug_Prescription.Find(NewString);
            //    db.Drug_Prescription.Remove(patient_Detail);
            //    db.SaveChanges();
            //    err = "Deleted";
            //}
            //else
            //{
            //    err = " or status inactive cannot delete!";
            //}


            return Json(err, JsonRequestBehavior.AllowGet);

        }

        public class Vitalreader
        {

            public string vid { get; set; }
            public string amount { get; set; }

        }
        public class Drugreader
        {

            public string dDose { get; set; }
            public int dRoute { get; set; }
            public int dMethod { get; set; }
            public string dDuration { get; set; }
            public string dItemno { get; set; }
            public string dStockTypeID { get; set; }
            public string disregdr { get; set; }
        }
        public class Drugreader1
        {

            public string suDose { get; set; }
            public int suRoute { get; set; }
            public int suMethod { get; set; }
            public string suDuration { get; set; }
            public string suItemno { get; set; }
            public string suStockTypeID { get; set; }
        }
        public class Labreader
        {

            public string labid { get; set; }
          
            public string labcat { get; set; }
            public string tubcat { get; set; }

        }
        public class Sickreader
        {

            public int scatid { get; set; }
            public string scategory { get; set; }
            public string sdays { get; set; }
            public string seff { get; set; }

        }
        public class labreader
        {

            public string LabTestID { get; set; }


        }
        public class breader
        {

            public string dgid { get; set; }
            public string dgdet { get; set; }
            

        }
        public class Hypreader
        {

            public string htype { get; set; }
            public string htype1 { get; set; }
            public string hsubtype { get; set; }
            public string hsubtype1 { get; set; }
            public string hrtype { get; set; }
            public string hrtype1 { get; set; }
            public string hrsubtype { get; set; }
            public int hrsubtype1 { get; set; }
            public string hstype { get; set; }
            public string hstype1 { get; set; }

            public string HDetail { get; set; }
        }
        public JsonResult Submitgenen(string genent, string PID)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };

            genent = genent.Trim(MyChar);
            PID = PID.Trim(MyChar);


            string Abdominal = "";
            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            foreach (var item in ser)
            {

                // locid = item.LocationID;
            }
            // var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
            opdid = (String)Session["userlocid1"];
            locid = (String)Session["userloc"];
            //foreach (var item in opd)
            //{

            //    //opdid = item.LOCID;
            //}
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
            patient_Detail.Present_Complain = "";
            patient_Detail.CreatedBy = userid.ToString();
            patient_Detail.PatientCatID = 4;
            patient_Detail.SubjectID = 0;
            //patient_Detail.ModifiedBy = userid.ToString();
            //patient_Detail.ModifiedMachine = userid.ToString();
            patient_Detail.CreatedDate = DateTime.Now;
            //patient_Detail.CreatedMachine = userid.ToString();
            patient_Detail.Status = 2;
            patient_Detail.GeneralEntries = genent;
            patient_Detail.ModifiedBy= userid.ToString();
            TranferDetail oTranferDetails = new TranferDetail();
            oTranferDetails.PDID = patient_Detail.PDID;
            oTranferDetails.ToLoc = opdid;
            oTranferDetails.FromLoc = opdid;
            oTranferDetails.TransferDate = DateTime.Now;
            oTranferDetails.TransID = indi.CreateTransID(patient_Detail.PDID);
            oTranferDetails.TransStatus = 1;



            if (ModelState.IsValid)
            {

                try
                {
                    db.TranferDetails.Add(oTranferDetails);

                    db.Patient_Detail.Add(patient_Detail);
                    db.SaveChanges();
                    err = "Saved";
                }
                catch (Exception ex)
                {
                    ex.ToString();

                }



                return Json(err, JsonRequestBehavior.AllowGet);
            }
            return Json(err, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Submitpatient(string SubCategoryID, string CategoryID, string items, string hitems, string Present_Complain, string PID, string HDetail)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            string NewString = items.Trim(MyChar);
            string NewString5 = SubCategoryID.Trim(MyChar);
            string NewString6 = CategoryID.Trim(MyChar);
            string NewString1 = hitems.Trim(MyChar);
            string NewString2 = Present_Complain.Trim(MyChar);
            string NewString3 = PID.Trim(MyChar);
            string NewString4 = "";
            if (!String.IsNullOrEmpty(HDetail))
            {
                NewString4 = HDetail.Trim(MyChar);
            }

            string Abdominal = "";
            string opdid = "";
            string locid = "";
            IndexGeneration indi = new IndexGeneration();
            int userid = Convert.ToInt32(Session["UserID"]);
            //var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //   // locid = item.LocationID;
            //}
           // var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
            opdid = (String)Session["userlocid1"];
            locid = (String)Session["userloc"];
            //foreach (var item in opd)
            //{

            //    //opdid = item.LOCID;
            //}
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}
            //////////////////////
            if (!String.IsNullOrEmpty(opdid))
            {



                DateTime dd = DateTime.Now.Date;
                var Abdominalvar = from s in db.Patient_Detail.Where(p => p.PID == NewString3).Where(p => p.Status == 1).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year) select new { s.PDID };

                foreach (var item in Abdominalvar)
                {

                    Abdominal = item.PDID;

                }
                if (String.IsNullOrEmpty(Abdominal))
                {
                    if (locid == "CBO")
                    {
                        var sicd = from s in db.SickReports.Where(p => p.LocationID == locid || p.LocationID == "AHQ").Where(p => p.regdate.Value.Day == dd.Day && p.regdate.Value.Month == dd.Month && p.regdate.Value.Year == dd.Year)
                                   join b in db.Patients on s.svcid equals b.PID
                                into sc
                                   from b in sc.DefaultIfEmpty().Where(p => p.PID == NewString3)
                                   join c in db.Sick_Category on s.PDID equals c.PDID
                               into sd
                                   from c in sd.DefaultIfEmpty()
                                   join d in db.PersonalDetails on b.ServiceNo equals d.ServiceNo
                              into se
                                   from d in se.DefaultIfEmpty()
                                   orderby s.regdate descending
                                   select new getsickdata { svcid = b.ServiceNo, PDID = s.PDID, isliveout = s.isliveout, fname = d.Initials, lname = d.Surname, rank = d.Rank, age = s.age, service = s.service, isduty = s.isduty, islow = s.islow, cat = "", catdays = c.CatPeriod, regdate = s.regdate };

                        foreach (var item in sicd)
                        {
                            Abdominal = item.PDID;
                        }
                    }
                    else
                    {
                        var sicd = from s in db.SickReports.Where(p => p.LocationID == locid).Where(p => p.regdate.Value.Day == dd.Day && p.regdate.Value.Month == dd.Month && p.regdate.Value.Year == dd.Year)
                                   join b in db.Patients on s.svcid equals b.PID
                                into sc
                                   from b in sc.DefaultIfEmpty().Where(p => p.PID == NewString3)
                                   join c in db.Sick_Category on s.PDID equals c.PDID
                               into sd
                                   from c in sd.DefaultIfEmpty()
                                   join d in db.PersonalDetails on b.ServiceNo equals d.ServiceNo
                              into se
                                   from d in se.DefaultIfEmpty()
                                   orderby s.regdate descending
                                   select new getsickdata { svcid = b.ServiceNo, PDID = s.PDID, isliveout = s.isliveout, fname = d.Initials, lname = d.Surname, rank = d.Rank, age = s.age, service = s.service, isduty = s.isduty, islow = s.islow, cat = "", catdays = c.CatPeriod, regdate = s.regdate };

                        foreach (var item in sicd)
                        {
                            Abdominal = item.PDID;
                        }
                    }
                }

                if (String.IsNullOrEmpty(Abdominal))
                {

                    Abdominal = indi.CreatePDID(NewString3);

                }
                //////////////////////

                Patient_Detail patient_Detail = new Patient_Detail();
                //IndexGeneration indi = new IndexGeneration();
                // string pdid= indi.CreatePDID(NewString3);

                patient_Detail.PDID = Abdominal;
                patient_Detail.PID = NewString3;
                patient_Detail.OPDID = opdid;
                patient_Detail.Present_Complain = NewString2;
                patient_Detail.CreatedBy = userid.ToString();
                patient_Detail.PatientCatID = Convert.ToInt32(NewString6);
                patient_Detail.SubjectID = Convert.ToInt32(NewString5);
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
                oTranferDetails.TransID = indi.CreateTransID(patient_Detail.PDID);
                oTranferDetails.TransStatus = 1;

                var objs = JsonConvert.DeserializeObject<List<Vitalreader>>(items);
                int objcount = objs.Count;
                Vital[] objVital = new Vital[objcount];
                int i = 0;

                foreach (Vitalreader p in objs)
                {

                    Vital oVital = new Vital();
                    oVital.PDID = patient_Detail.PDID;
                    oVital.VID = indi.CreateVID(i, patient_Detail.PDID);
                    oVital.CreatedBy = userid.ToString();
                    oVital.CreatedDate = DateTime.Now;
                    oVital.LocationID = locid;
                    oVital.LocID = opdid;
                    oVital.PDID = patient_Detail.PDID;
                    oVital.Reading_Time = DateTime.Now;
                    oVital.ModifiedDate = DateTime.Now.Date;
                    oVital.VTID = Convert.ToInt32(p.vid);
                    oVital.VitalValues = p.amount;
                    oVital.Status = 1;

                    objVital[i] = oVital;
                    i++;
                    // db.Vitals.Add(oVital);

                }

                var objh = JsonConvert.DeserializeObject<List<Hypreader>>(hitems);
                int objhcount = objh.Count;
                Hypersensivity[] objHyp = new Hypersensivity[objhcount];
                int j = 0;

                foreach (Hypreader p in objh)
                {
                    var oldtestcnt = db.Hypersensivities.Where(d => d.HyperTypeSubID == p.hsubtype1).Where(d => d.PID == NewString3).ToList().Count;
                    if (oldtestcnt < 1)
                    {
                        Hypersensivity oHypersensivity = new Hypersensivity();
                        oHypersensivity.PID = NewString3;
                        oHypersensivity.HypersenseID = indi.CreateHID(j, NewString3);
                        oHypersensivity.HyperTypeSubID = p.htype1;
                        //oHypersensivity.RSubID = p.hrsubtype1;
                        //oHypersensivity.SeverityID = Convert.ToInt32(p.hstype1);
                        oHypersensivity.ModifiedDate = DateTime.Now;
                        oHypersensivity.HypersenseDetail = p.hstype1;


                        objHyp[j] = oHypersensivity;
                        j++;
                    }
                    // db.Vitals.Add(oVital);

                }

                if (ModelState.IsValid)
                {


                    try
                    {
                        db.TranferDetails.Add(oTranferDetails);
                        db.Vitals.AddRange(objVital);
                        objHyp = objHyp.Where(x => x != null).ToArray();
                        db.Hypersensivities.AddRange(objHyp);
                        db.Patient_Detail.Add(patient_Detail);
                        db.SaveChanges();
                        err = "Saved";
                    }
                    catch (Exception ex)
                    {



                        ex.ToString();
                        try
                        {
                            db.Entry(patient_Detail).State = EntityState.Modified;
                            db.TranferDetails.Add(oTranferDetails);
                            db.Vitals.AddRange(objVital);
                            objHyp = objHyp.Where(x => x != null).ToArray();
                            db.Hypersensivities.AddRange(objHyp);
                            db.SaveChanges();
                            err = "Saved";
                        }
                        catch (Exception Ex)
                        {
                        }
                    }





                    // err = "Already in Sick Parade!";

                }

                return Json(err, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json("2", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult view1()
        {
            string pdid = Request.QueryString["opdid"];
            ViewBag.opdid = pdid;
            return View();
        }
        public ActionResult view2()
        {
            string pdid = Request.QueryString["opdid"];
            ViewBag.opdid = pdid;
            return View();
        }
        public ActionResult view3()
        {
            string pdid = Request.QueryString["opdid"];
            ViewBag.opdid = pdid;
            return View();
        }
        public ActionResult mednurse(int? page, string id, string currentFilter)
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

            opdid = (String)Session["userlocid1"];
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select Salutation,FName,LName from [dbo].[Users] with (nolock) where UserID='" + userid + "' ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var ser = oDataSet.AsEnumerable()
    .Select(dataRow => new User
    {
        FName = dataRow.Field<string>("FName"),
        LName = dataRow.Field<string>("LName"),
        Salutation = dataRow.Field<string>("Salutation")
    }).ToList();

            foreach (var item in ser)
            {
                Session["loginuser"] = item.Salutation + ". " + item.FName + " " + item.LName;

            }



            DataTable oDataSet1 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select SpecialityID,LOCID from [dbo].[Staff_Master] with (nolock) where UserID='" + userid + "'";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oDataSet = null;
            oSqlDataAdapter.Fill(oDataSet1);
            oSqlConnection.Close();
            var opd = oDataSet1.AsEnumerable()
    .Select(dataRow => new Staff_Master
    {
        SpecialityID = dataRow.Field<int>("SpecialityID"),
        LOCID = dataRow.Field<string>("LOCID")

    }).ToList();



            foreach (var item in opd)
            {
                specid = item.SpecialityID;

            }

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
                DateTime dd = DateTime.Now.Date;
                DataTable oDataSet4 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery =" SELECT    max(c.Present_Complain)  pcomoplian  ,COALESCE(NULLIF(max(case when a.RelationshipType = 1 " +
"  then b.Surname end), ''), " +
"	 max(a.surname))  sname  ,max(case when a.RelationshipType = 1 then b.Rank  end) rnkname, max(a.ServiceNo)  sno   , " +
  " max(case when a.RelationshipType = 1 then b.Initials  end)  inililes, max(a.RelationshipType) relasiont  , max(a.pid) " +
"   pidp, max(c.pdid)  pdids,max(c.status)  pstatus, max(i.status)  msstatus,max(c.CreatedDate) crdate, max(h.Relationship) relasiondet FROM[MMS]. " +
 "  [dbo].[Patient] as a with(nolock)  left join[MMS].[dbo].[PersonalDetails] as b on a.ServiceNo=b.ServiceNo and a.Service_Type=b.ServiceType left join[MMS]. " +
  " [dbo].[Patient_Detail] as c on a.pid=c.pid left join[MMS].[dbo].[Clinic_Master] as d on c.OPDID=d.Clinic_ID " +
    " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=a.RelationshipType " +
   "   inner join[MMS].[dbo].[MedicalScreen] as i on i.pdid = c.pdid  "+
" where   a.ServiceNo like'%" + id + "%' " +
"  group by c.PDID, c.CreatedDate order by c.CreatedDate ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet4);
                oSqlConnection.Close();
                var lid = oDataSet4.AsEnumerable()
        .Select(dataRow => new getdocdetail
        {
            pdids = dataRow.Field<string>("pdids"),
            inililes = dataRow.Field<string>("inililes"),
            sname = dataRow.Field<string>("sname"),
            sno = dataRow.Field<string>("sno"),
            rnkname = dataRow.Field<string>("rnkname"),
            pcomoplian = dataRow.Field<string>("pcomoplian"),
            pstatus = dataRow.Field<int>("pstatus").ToString(),
            relasiont = dataRow.Field<int>("relasiont"),
            crdate = dataRow.Field<DateTime?>("crdate"),
            pidp = dataRow.Field<string>("pidp"),
            relasiondet = dataRow.Field<string>("relasiondet"),
        }).ToList();

                var pageNumber = page ?? 1;
                onePageOfProducts = lid.OrderByDescending(p => p.crdate).ToPagedList(pageNumber, 10);

            }
            else
            {
                DateTime dd = DateTime.Now.Date;

                DataTable oDataSet3 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "   SELECT    max(c.Present_Complain )  pcomoplian  ,COALESCE(NULLIF(max(case when a.RelationshipType = 1 " +
 "  then b.Surname end), ''), " +
"	 max(a.surname))  sname  ,max(case when a.RelationshipType = 1 then b.Rank  end) rnkname, max(a.ServiceNo)  sno   , " +
    " max(case when a.RelationshipType = 1 then b.Initials  end)  inililes, max(a.RelationshipType) relasiont  , max(a.pid) " +
  "   pidp, max(c.pdid)  pdids,max(c.status)  pstatus, max(i.status)  msstatus,max(c.CreatedDate) crdate, max(h.Relationship) relasiondet FROM[MMS]. " +
   "  [dbo].[Patient] as a with(nolock)  left join[MMS].[dbo].[PersonalDetails] as b on a.ServiceNo=b.ServiceNo and a.Service_Type=b.ServiceType left join[MMS]. " +
	" [dbo].[Patient_Detail] as c on a.pid=c.pid left join[MMS].[dbo].[Clinic_Master] as d on c.OPDID=d.Clinic_ID "+
      " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=a.RelationshipType " +
       "   inner join[MMS].[dbo].[MedicalScreen] as i on i.pdid = c.pdid  " +
    " where convert(date, c.[CreatedDate]) =CONVERT(varchar,'" + dd.ToShortDateString() + "',111)   and PatientCatID=2 " +
    " and c.opdid='" + opdid + "' group by c.PDID, c.CreatedDate order by c.CreatedDate ";
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
            pstatus = dataRow.Field<int>("pstatus").ToString(),
            relasiont = dataRow.Field<int>("relasiont"),
            crdate = dataRow.Field<DateTime?>("crdate"),
            pidp = dataRow.Field<string>("pidp"),
            relasiondet = dataRow.Field<string>("relasiondet"),
        }).ToList();



                ///db.Patient_Detail.Include(p => p.Patient).Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                //  patient_Detail = patient_Detail.GroupBy(t => t.pdids).Select(grp => grp.FirstOrDefault()).OrderByDescending(s=>s.crdate);
                var pageNumber = page ?? 1;

                onePageOfProducts = lid.OrderByDescending(p => p.crdate).ToPagedList(pageNumber, 10);
            }
               
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }
        public ActionResult medpft()
        {
            return View();
        }
        public ActionResult meddoc(int? page, string id, string currentFilter)
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

            opdid = (String)Session["userlocid1"];


            if (!opdid.Trim().ToLower().StartsWith("dv"))
            {

                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select Salutation,FName,LName from [dbo].[Users] with (nolock) where UserID='" + userid + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet);
                oSqlConnection.Close();
                var ser = oDataSet.AsEnumerable()
        .Select(dataRow => new User
        {
            FName = dataRow.Field<string>("FName"),
            LName = dataRow.Field<string>("LName"),
            Salutation = dataRow.Field<string>("Salutation")
        }).ToList();

                foreach (var item in ser)
                {
                    Session["loginuser"] = item.Salutation + ". " + item.FName + " " + item.LName;

                }



                DataTable oDataSet1 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select SpecialityID,LOCID from [dbo].[Staff_Master] with (nolock) where UserID='" + userid + "'";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oDataSet = null;
                oSqlDataAdapter.Fill(oDataSet1);
                oSqlConnection.Close();
                var opd = oDataSet1.AsEnumerable()
        .Select(dataRow => new Staff_Master
        {
            SpecialityID = dataRow.Field<int>("SpecialityID"),
            LOCID = dataRow.Field<string>("LOCID")

        }).ToList();



                foreach (var item in opd)
                {
                    specid = item.SpecialityID;

                }

                //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                //foreach (var item in serW)
                //{

                //    locid = item.LocationID;
                //}

                Session["userlocid2"] = locid;
                if (!String.IsNullOrEmpty(id))
                {
                    id = id.Trim(MyChar);
                }

                if (!String.IsNullOrEmpty(id))
                {
                    DateTime dd = DateTime.Now.Date;
                    DataTable oDataSet4 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " SELECT    max(c.Present_Complain)  pcomoplian  ,COALESCE(NULLIF(max(case when a.RelationshipType = 1 " +
    "  then b.Surname end), ''), " +
    "	 max(a.surname))  sname  ,max(case when a.RelationshipType = 1 then b.Rank  end) rnkname, max(a.ServiceNo)  sno   , " +
      " max(case when a.RelationshipType = 1 then b.Initials  end)  inililes, max(a.RelationshipType) relasiont  ,max(i.msyear) yr, max(a.pid) " +
    "   pidp, max(c.pdid)  pdids,max(c.status)  pstatus, max(i.status)  msstatus,max(c.CreatedDate) crdate, max(h.Relationship) relasiondet FROM[MMS]. " +
     "  [dbo].[Patient] as a with(nolock)  left join[MMS].[dbo].[PersonalDetails] as b on a.ServiceNo=b.ServiceNo and a.Service_Type=b.ServiceType left join[MMS]. " +
      " [dbo].[Patient_Detail] as c on a.pid=c.pid left join[MMS].[dbo].[Clinic_Master] as d on c.OPDID=d.Clinic_ID " +
        " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=a.RelationshipType " +
       "   inner join[MMS].[dbo].[MedicalScreen] as i on i.pdid = c.pdid  " +
    " where   a.ServiceNo like'%" + id + "%'   " +
    "  group by c.PDID, c.CreatedDate order by c.CreatedDate desc";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet4);
                    oSqlConnection.Close();
                    var lid = oDataSet4.AsEnumerable()
            .Select(dataRow => new getdocdetail
            {
                pdids = dataRow.Field<string>("pdids"),
                inililes = dataRow.Field<string>("inililes"),
                sname = dataRow.Field<string>("sname"),
                sno = dataRow.Field<string>("sno"),
                rnkname = dataRow.Field<string>("rnkname"),
                pcomoplian = dataRow.Field<string>("pcomoplian"),
                pstatus = dataRow.Field<int>("pstatus").ToString(),
                relasiont = dataRow.Field<int>("relasiont"),
                crdate = dataRow.Field<DateTime?>("crdate"),
                pidp = dataRow.Field<string>("pidp"),
                relasiondet = dataRow.Field<string>("relasiondet"),
                sv = dataRow.Field<int?>("yr"),
            }).ToList();

                    var pageNumber = page ?? 1;
                    onePageOfProducts = lid.ToList();

                }
                else
                {
                    DateTime dd = DateTime.Now.Date;

                    DataTable oDataSet3 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "   SELECT    max(c.Present_Complain )  pcomoplian  ,COALESCE(NULLIF(max(case when a.RelationshipType = 1 " +
     "  then b.Surname end), ''), " +
    "	 max(a.surname))  sname  ,max(case when a.RelationshipType = 1 then b.Rank  end) rnkname, max(a.ServiceNo)  sno   , " +
        " max(case when a.RelationshipType = 1 then b.Initials  end)  inililes, max(a.RelationshipType) relasiont  ,max(i.msyear) yr, max(a.pid) " +
      "   pidp, max(c.pdid)  pdids,max(c.status)  pstatus, max(i.status)  msstatus,max(c.CreatedDate) crdate, max(h.Relationship) relasiondet FROM[MMS]. " +
       "  [dbo].[Patient] as a with(nolock)  left join[MMS].[dbo].[PersonalDetails] as b on a.ServiceNo=b.ServiceNo and a.Service_Type=b.ServiceType left join[MMS]. " +
        " [dbo].[Patient_Detail] as c on a.pid=c.pid left join[MMS].[dbo].[Clinic_Master] as d on c.OPDID=d.Clinic_ID " +
          " left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=a.RelationshipType " +
           "   inner join[MMS].[dbo].[MedicalScreen] as i on i.pdid = c.pdid  " +
        " where convert(date, c.[CreatedDate]) =CONVERT(varchar,'" + dd.ToShortDateString() + "',111)   and PatientCatID=2 " +
        " and c.opdid='" + opdid + "' and i.status=1 group by c.PDID, c.CreatedDate order by c.CreatedDate desc";
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
                pstatus = dataRow.Field<int>("pstatus").ToString(),
                relasiont = dataRow.Field<int>("relasiont"),
                crdate = dataRow.Field<DateTime?>("crdate"),
                pidp = dataRow.Field<string>("pidp"),
                relasiondet = dataRow.Field<string>("relasiondet"),
                sv = dataRow.Field<int?>("yr"),
            }).ToList();



                    ///db.Patient_Detail.Include(p => p.Patient).Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                    //  patient_Detail = patient_Detail.GroupBy(t => t.pdids).Select(grp => grp.FirstOrDefault()).OrderByDescending(s=>s.crdate);
                    var pageNumber = page ?? 1;

                    onePageOfProducts = lid.ToList();
                }

                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public JsonResult Submitlabpatient(string SubCategoryID, string CategoryID, string litems, string Present_Complain, string PID)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            
         //   string NewString2 = Present_Complain.Trim(MyChar);
            string NewString3 = PID.Trim(MyChar);

            if (!String.IsNullOrEmpty(litems))
            {
                litems = litems.Trim(MyChar);
            }

            string NewString5 = SubCategoryID.Trim(MyChar);
            string NewString6 = CategoryID.Trim(MyChar);
            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            foreach (var item in ser)
            {

               // locid = item.LocationID;
            }
            //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
            opdid = (String)Session["userlocid1"];
            //foreach (var item in opd)
            //{

            //    //opdid = item.LOCID;
            //}
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}
            locid = (String)Session["userloc"];
            Patient_Detail patient_Detail = new Patient_Detail();
            IndexGeneration indi = new IndexGeneration();
            patient_Detail.PDID = indi.CreatePDID(NewString3);
            patient_Detail.PID = NewString3;
            patient_Detail.OPDID = opdid;
            patient_Detail.PatientCatID = Convert.ToInt32(NewString6);
            patient_Detail.SubjectID = Convert.ToInt32(NewString5);
            patient_Detail.Present_Complain = "For Investigation";
            patient_Detail.CreatedBy = userid.ToString();
            //patient_Detail.ModifiedDate= DateTime.Now;
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
            string Abdominal = "";
            string Abdominal1 = "";
            DateTime dd = DateTime.Now.Date;
            if (opdid.Trim().ToLower().Contains("opd"))
            {
                var Abdominalvar = from s in db.Patient_Detail.Where(p => p.PID == NewString3).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year) select new { s.PDID };

                foreach (var item in Abdominalvar)
                {

                    Abdominal = item.PDID;
                    if (String.IsNullOrEmpty(Abdominal))
                    {
                        Abdominal = patient_Detail.PDID;
                    }
                }
            }
            else
            {
                var Abdominalvar1 = from s in db.Patient_Detail.Where(p => p.PID == NewString3).Where(p => p.OPDID == opdid).Where(p => p.Status != 2).Where(p => p.CreatedDate.Month== dd.Month&&p.CreatedDate.Year==dd.Year) select new { s.PDID };

                foreach (var item in Abdominalvar1)
                {

                    Abdominal = item.PDID;
                    if (String.IsNullOrEmpty(Abdominal))
                    {
                        Abdominal = patient_Detail.PDID;
                    }
                }

            }
            var objsl = (dynamic)null;
            int objcountl = 0;
            Lab_Report[] objLab_Report = new Lab_Report[250];
            Lab_Report[] objlab = new Lab_Report[1000];
            if (!String.IsNullOrEmpty(litems))
            {


                objsl = JsonConvert.DeserializeObject<List<Labreader>>(litems);



                int i = 0;
                try
                {
                    foreach (Labreader p in objsl)
                    {

                        Lab_Report oLab_Report = new Lab_Report();
                        var objl = from s in db.Lab_SubCategory.Where(f => f.CategoryID == p.labid) select new { s.LabTestID };
                        int objcountl1 = objl.Count();
                        foreach (var q in objl)
                        {


                            var lablist = from t in db.Lab_Report.Where(pf => pf.PDID == Abdominal)
                                          join x in db.Lab_SubCategory on t.LabTestID equals x.LabTestID
                                          join y in db.Lab_MainCategory on x.CategoryID equals y.CategoryID


                                          select new
                                          { y.CategoryName, y.CategoryID, t.PDID, t.TestSID }; ;
                            var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();


                            var tf = labl.Count;
                            if (tf < 1)
                            {
                                Lab_Report oLab_Reports = new Lab_Report();
                                oLab_Reports.LabTestID = q.LabTestID;
                                oLab_Reports.RequestedLocID = locid;
                                oLab_Reports.RequestedTime = DateTime.Now;
                                if (!String.IsNullOrEmpty(Abdominal))
                                {
                                    oLab_Reports.PDID = Abdominal;
                                    oLab_Reports.TestSID = Abdominal + "x0";
                                }
                                else
                                {
                                    oLab_Reports.PDID = patient_Detail.PDID;
                                    oLab_Reports.TestSID = patient_Detail.PDID + "x0";
                                }
                                oLab_Reports.Issued = "0";
                                oLab_Reports.IsPrint = "0";
                                oLab_Reports.Isemail = 0;
                                if (!String.IsNullOrEmpty(Abdominal))
                                {
                                    oLab_Reports.Lab_Index = Guid.NewGuid().ToString();
                                }
                                else
                                {
                                    oLab_Reports.Lab_Index = Guid.NewGuid().ToString();
                                }
                                objLab_Report[i] = oLab_Reports;
                                i++;

                            }
                            else
                            {
                                Lab_Report oLab_Reports = new Lab_Report();
                                oLab_Reports.LabTestID = q.LabTestID;
                                oLab_Reports.RequestedLocID = locid;
                                oLab_Reports.RequestedTime = DateTime.Now;
                                if (!String.IsNullOrEmpty(Abdominal))
                                {
                                    oLab_Reports.PDID = Abdominal;
                                    oLab_Reports.TestSID = Abdominal + "x" + tf;
                                }
                                else
                                {
                                    oLab_Reports.PDID = patient_Detail.PDID;
                                    oLab_Reports.TestSID = patient_Detail.PDID + "x" + tf;
                                }
                                oLab_Reports.Issued = "0";
                                oLab_Reports.IsPrint = "0";
                                oLab_Reports.Isemail = 0;
                                if (!String.IsNullOrEmpty(Abdominal))
                                {
                                    oLab_Reports.Lab_Index = Guid.NewGuid().ToString();
                                }
                                else
                                {
                                    oLab_Reports.Lab_Index = Guid.NewGuid().ToString();
                                }
                                objLab_Report[i] = oLab_Reports;
                                i++;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }


            if (ModelState.IsValid)
            {
                try
                {

                   
                   
                    if (String.IsNullOrEmpty(Abdominal))
                    {
                        db.TranferDetails.Add(oTranferDetails);
                        objLab_Report = objLab_Report.Where(x => x != null).ToArray();
                        db.Lab_Report.AddRange(objLab_Report);

                        db.Patient_Detail.Add(patient_Detail);
                        db.SaveChanges();
                        err = "Saved";
                    }
                    else
                    {
                        objLab_Report = objLab_Report.Where(x => x != null).ToArray();
                        db.Lab_Report.AddRange(objLab_Report);
                        db.SaveChanges();
                        err = "Already in Sick Parade ";
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();

                }
            }

            return Json(err, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Savepatient(string bitems, string AbdOther, string reftohosp, string planofmgt, string drughyst, string hitems,string pastmedhys, string items,string sitems, string litems, string ditems, string Present_Complain, string History_PresentComplain, string Other_Complain, string History_OtherComplain, string OPD_Diagnosis,  string PDID,string pntstatus,string GClinic, string dgn1, string genex, string cardex, string cenex, string resex, string othex, string abdex, string drugins)
        {
             
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            oSqlConnection = new SqlConnection(conStr);
            String err = "";
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(pastmedhys))
            {
                pastmedhys = pastmedhys.Trim(MyChar);
                pastmedhys = Regex.Replace(pastmedhys, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(items))
            {
                items = items.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(sitems))
            {
                sitems = sitems.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(litems))
            {
                litems = litems.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(ditems))
            {
                ditems = ditems.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(hitems))
            {
                hitems = hitems.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(Present_Complain))
            {
                Present_Complain = Present_Complain.Trim(MyChar);
                Present_Complain = Regex.Replace(Present_Complain, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(History_PresentComplain))
            {
                History_PresentComplain = History_PresentComplain.Trim(MyChar);
                History_PresentComplain = Regex.Replace(History_PresentComplain, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(Other_Complain))
            {
                Other_Complain = Other_Complain.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(History_OtherComplain))
            {
                History_OtherComplain = History_OtherComplain.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(OPD_Diagnosis))
            {
                OPD_Diagnosis = OPD_Diagnosis.Trim(MyChar);
                OPD_Diagnosis = Regex.Replace(OPD_Diagnosis, @"\\t|\\n|\\r", "");
            }
           
            if (!String.IsNullOrEmpty(planofmgt))
            {
                planofmgt = planofmgt.Trim(MyChar);
                planofmgt = Regex.Replace(planofmgt, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(drughyst))
            {
                drughyst = drughyst.Trim(MyChar);
                drughyst = Regex.Replace(drughyst, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(reftohosp))
            {
                reftohosp = reftohosp.Trim(MyChar);
                reftohosp = Regex.Replace(reftohosp, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(bitems))
            {
                bitems = bitems.Trim(MyChar);
            }
           
            if (!String.IsNullOrEmpty(PDID)) { PDID = PDID.Trim(MyChar); }
           
            if (!String.IsNullOrEmpty(dgn1)) { AbdOther = AbdOther.Trim(MyChar); }
            if (!String.IsNullOrEmpty(genex)) { genex = genex.Trim(MyChar);

                genex = Regex.Replace(genex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(cardex)) { cardex = cardex.Trim(MyChar);
                cardex = Regex.Replace(cardex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(cenex)) { cenex = cenex.Trim(MyChar);
                cenex = Regex.Replace(cenex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(resex)) { resex = resex.Trim(MyChar);
                resex = Regex.Replace(resex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(othex)) { othex = othex.Trim(MyChar);
                othex = Regex.Replace(othex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(abdex)) { abdex = abdex.Trim(MyChar);
                abdex = Regex.Replace(abdex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(drugins))
            {
                drugins = drugins.Trim(MyChar);
                drugins = Regex.Replace(drugins, @"\\t|\\n|\\r", "");
            }

            string pid = "";
            string opdid = "";
            string locid = "";
            string modus = "";
            int? pcat = 0;
            int? subcat = 0;
            DateTime crdt = DateTime.Now;
            int userid = Convert.ToInt32(Session["UserID"]);
            //var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //   // locid = item.LocationID;
            //}
            //var pidvar = from s in db.Patient_Detail.Where(p => p.PDID == PDID) select new { s.PID, s.CreatedDate, s.PatientCatID, s.SubjectID, s.OPDID ,s.ModifiedBy };

            //foreach (var item in pidvar)
            //{

            //    pid = item.PID;
            //    crdt = item.CreatedDate;
            //    pcat = item.PatientCatID;
            //    subcat = item.SubjectID;
            //    opdid = item.OPDID;
            //    modus = item.ModifiedBy;
            //}

            DataTable oDataSetsp1 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "    select * FROM [MMS].[dbo].[Patient_Detail] where PDID='" + PDID + "'   ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            if (oSqlConnection.State == ConnectionState.Closed)
            {
                oSqlConnection.Open();
            }
             
            oSqlCommand.CommandTimeout = 120;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetsp1);
            foreach (DataRow row in oDataSetsp1.Rows)
            {
                pid= row["PID"].ToString();
                crdt =(DateTime) row["CreatedDate"];
                pcat = (int?)row["PatientCatID"];
                subcat = (int?)row["SubjectID"];
                opdid = row["OPDID"].ToString();
                modus = row["ModifiedBy"].ToString();
            }



            //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
            ////opdid = (String)Session["userlocid1"];
            //foreach (var item in opd)
            //{

            //  //  opdid = item.LOCID;
            //}
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}
            locid = (String)Session["userloc"];
            IndexGeneration indi = new IndexGeneration();
            if (!String.IsNullOrEmpty(modus)) {
                if (!modus.Trim().Equals(userid.ToString()))
                {
                    PDID = indi.CreatePDID(pid);
                }
            }
            Patient_Detail patient_Detail = new Patient_Detail();

            patient_Detail.PDID = PDID;
            patient_Detail.Present_Complain = Present_Complain;
            patient_Detail.History_PresentComplain = History_PresentComplain;
            patient_Detail.Other_Complain = Other_Complain;
            patient_Detail.History_OtherComplain = History_OtherComplain;
            patient_Detail.OPD_Diagnosis = OPD_Diagnosis;
            patient_Detail.ModifiedDate = DateTime.Now;
            patient_Detail.ModifiedBy = userid.ToString();
            patient_Detail.CreatedBy = userid.ToString();
            patient_Detail.PID = pid;
            patient_Detail.PatientCatID = pcat;
            patient_Detail.SubjectID = subcat;
            //string daignosisid = "";
            //if (!String.IsNullOrEmpty(dgn1))
            //{
            //    dynamic data = JObject.Parse(dgn1);
            //    daignosisid = data.dgid;
            //}

            patient_Detail.Examination = drugins;
            patient_Detail.OPDID = opdid;
           // patient_Detail.DaignosisID = daignosisid;
            patient_Detail.CreatedDate = crdt;
            patient_Detail.Status = Convert.ToInt32(pntstatus);
            int cltyp = 0;
            if(!String.IsNullOrEmpty(GClinic))
            {
                dynamic data = JObject.Parse(GClinic);
                cltyp = data.ClinicTypeID;
            }

            //-------PASTMED/
            long pastmed1 = 0;
            PastMedHistory oPastMedHistory = new PastMedHistory();
            //var pastmedvar1 = from s in db.PastMedHistories.Where(p => p.PID == pid) select new { s.PMHID };

            //foreach (var item in pastmedvar1)
            //{

            //    pastmed1 = item.PMHID;
              
            //}
            DataTable oDataSetsp2 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            if (oSqlConnection.State == ConnectionState.Closed)
            {
                oSqlConnection.Open();
            }
            oSqlCommand = new SqlCommand();
            sqlQuery = "    select * FROM [MMS].[dbo].[PastMedHistory] where PID='" + pid + "'   ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            //  oSqlConnection.Open();
            oSqlCommand.CommandTimeout = 120;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetsp2);
            foreach (DataRow row in oDataSetsp2.Rows)
            {
                pastmed1 =(long) row["PMHID"];
               
            }


            oPastMedHistory.PMHID = pastmed1;
            oPastMedHistory.PID = pid;
            oPastMedHistory.PMHDetail = pastmedhys;
            oPastMedHistory.Drughst = drughyst;
            ///////////

            long cathy1 = 0;
            CatReferal oCatReferal = new CatReferal();
            //var cathyvar1 = from s in db.CatReferals.Where(p => p.PDID == PDID) select new { s.reffid };

            //foreach (var item in cathyvar1)
            //{

            //    cathy1 = item.reffid;
            //    oCatReferal.reffid = cathy1;
            //}
            DataTable oDataSetsp3 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            if (oSqlConnection.State == ConnectionState.Closed)
            {
                oSqlConnection.Open();
            }
            oSqlCommand = new SqlCommand();
            sqlQuery = "    select * FROM [MMS].[dbo].[CatReferals] where PDID='" + PDID + "'   ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            //  oSqlConnection.Open();
            oSqlCommand.CommandTimeout = 120;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetsp3);
            foreach (DataRow row in oDataSetsp3.Rows)
            {
                cathy1 = (long)row["reffid"];

            }
            oCatReferal.reffid = cathy1;
            oCatReferal.PDID = PDID;
            oCatReferal.PlanofMgt = planofmgt;
            oCatReferal.ReffNote = reftohosp;
            ///////////
            var objb = JsonConvert.DeserializeObject<List<breader>>(bitems);
            int objbcount = objb.Count;
            CatDiagList[] objcatd = new CatDiagList[objbcount];
            int j1 = 0;

            foreach (breader p in objb)
            {
               
                    CatDiagList oCatDiagList = new CatDiagList();
                    oCatDiagList.dgid = p.dgid;
                    oCatDiagList.PDID = PDID;



                    objcatd[j1] = oCatDiagList;
                    j1++;
                
                // db.Vitals.Add(oVital);

            }

            ////////////
            //cltyp= p.ClinicTypeID;

            //  var clincd = from v in db.Clinic_Master where (v.ClinicTypeID == cltyp) where (v.LocationID == locid) select new { v.Clinic_ID };
            string clinkid = "";
            DataTable oDataSetsp4 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            if (oSqlConnection.State == ConnectionState.Closed)
            {
                oSqlConnection.Open();
            }
            oSqlCommand = new SqlCommand();
            sqlQuery = "    select * FROM [MMS].[dbo].[Clinic_Master] where ClinicTypeID='" + cltyp + "' AND LocationID='" + locid + "'  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            //  oSqlConnection.Open();
            oSqlCommand.CommandTimeout = 120;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetsp4);
            foreach (DataRow row in oDataSetsp4.Rows)
            {
                clinkid = row["Clinic_ID"].ToString();

            }



           
            Patient_Detail patient_Detail2 = new Patient_Detail();
            TranferDetail oTranferDetails = new TranferDetail();
            //if (clincd != null) {
            //    foreach (var p in clincd)
            //    {
            //        clinkid = p.Clinic_ID;
            //    } }
                if (!String.IsNullOrEmpty(clinkid)){ 
                
                //patient_Detail2.PDID = indi.CreatePDID(patient_Detail.PID);
                //patient_Detail2.PID = patient_Detail.PID;
                //patient_Detail2.OPDID = clinkid;
                //patient_Detail2.Present_Complain = patient_Detail.Present_Complain;
                //patient_Detail2.CreatedBy = userid.ToString();
                ////patient_Detail.ModifiedDate= DateTime.Now;
                ////patient_Detail.ModifiedBy = userid.ToString();
                ////patient_Detail.ModifiedMachine = userid.ToString();
                //patient_Detail2.CreatedDate = DateTime.Now;
                ////patient_Detail.CreatedMachine = userid.ToString();
                //patient_Detail2.Status = 1;
                //db.Patient_Detail.Add(patient_Detail2);

                oTranferDetails.PDID = patient_Detail.PDID;
                oTranferDetails.ToLoc = clinkid;
                oTranferDetails.FromLoc = opdid;
                oTranferDetails.TransferDate = DateTime.Now;
                oTranferDetails.TransID = indi.CreateTransID(patient_Detail.PDID);
                oTranferDetails.TransStatus = 1;
            }
            // var clincd1 = from v in db.Clinic_Master where (v.ClinicTypeID == 20) where (v.LocationID == locid) select new { v.Clinic_ID };

            //if (clincd1 != null)
            //{
            //    foreach (var p in clincd1)
            //    {
            //        clinkid1 = p.Clinic_ID;
            //    }
            //}
            string clinkid1 = "";
            DataTable oDataSetsp5 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            if (oSqlConnection.State == ConnectionState.Closed)
            {
                oSqlConnection.Open();
            }
            oSqlCommand = new SqlCommand();
            sqlQuery = "    select * FROM [MMS].[dbo].[Clinic_Master] where ClinicTypeID='20' AND LocationID='" + locid + "'  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            //  oSqlConnection.Open();
            oSqlCommand.CommandTimeout = 120;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetsp5);
            foreach (DataRow row in oDataSetsp5.Rows)
            {
                clinkid1 = row["Clinic_ID"].ToString();

            }



            if (pntstatus.Equals("10")|| pntstatus.Equals("13"))
            {

                //patient_Detail2.PDID = indi.CreatePDID(patient_Detail.PID);
                //patient_Detail2.PID = patient_Detail.PID;
                //patient_Detail2.OPDID = clinkid1;
                //patient_Detail2.Present_Complain = patient_Detail.Present_Complain;
                //patient_Detail2.CreatedBy = userid.ToString();
                ////patient_Detail.ModifiedDate= DateTime.Now;
                ////patient_Detail.ModifiedBy = userid.ToString();
                ////patient_Detail.ModifiedMachine = userid.ToString();
                //patient_Detail2.CreatedDate = DateTime.Now;
                ////patient_Detail.CreatedMachine = userid.ToString();
                //patient_Detail2.Status = 1;
                //db.Patient_Detail.Add(patient_Detail2);

                oTranferDetails.PDID = patient_Detail.PDID;
                oTranferDetails.ToLoc = clinkid1;
                oTranferDetails.FromLoc = opdid;
                oTranferDetails.TransferDate = DateTime.Now;
                oTranferDetails.TransID = indi.CreateTransID(patient_Detail.PDID);
                oTranferDetails.TransStatus = 1;
            }
            var objsv = JsonConvert.DeserializeObject<List<Vitalreader>>(items);
            int objcountv = objsv.Count;
            Vital[] objVital = new Vital[objcountv];
            int i1 = 0;

            foreach (Vitalreader p in objsv)
            {

                Vital oVital = new Vital();
                oVital.PDID = patient_Detail.PDID;
                oVital.VID = indi.CreateVID(i1, patient_Detail.PDID);
                oVital.CreatedBy = userid.ToString();
                oVital.CreatedDate = DateTime.Now;
                oVital.LocationID = locid;
                oVital.LocID = opdid;
                oVital.PDID = patient_Detail.PDID;
                oVital.Reading_Time = DateTime.Now;
                oVital.ModifiedDate = DateTime.Now.Date;
                oVital.VTID = Convert.ToInt32(p.vid);
                oVital.VitalValues = p.amount;
                oVital.Status = 1;

                objVital[i1] = oVital;
                i1++;
                // db.Vitals.Add(oVital);

            }
            var objh = JsonConvert.DeserializeObject<List<Hypreader>>(hitems);
            int objhcount = objh.Count;
            Hypersensivity[] objHyp = new Hypersensivity[objhcount];
            int j = 0;

            foreach (Hypreader p in objh)
            {
               // var oldtestcnt = db.Hypersensivities.Where(d => d.HyperTypeSubID == p.hsubtype1).Where(d => d.PID == patient_Detail.PID).ToList().Count;

                DataTable oDataSetsp6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                oSqlCommand = new SqlCommand();
                sqlQuery = "    select * FROM [MMS].[dbo].[Hypersensivity] where HyperTypeSubID='"+ p.hsubtype1 + "' AND PID='" + patient_Detail.PID + "'  ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                //  oSqlConnection.Open();
                oSqlCommand.CommandTimeout = 120;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetsp6);
                //foreach (DataRow row in oDataSetsp6.Rows)
                //{
                //    clinkid1 = row["Clinic_ID"].ToString();

                //}





                if (oDataSetsp6.Rows.Count < 1)
                {
                    Hypersensivity oHypersensivity = new Hypersensivity();
                    oHypersensivity.PID = patient_Detail.PID; 
                    oHypersensivity.HypersenseID = indi.CreateHID(j, patient_Detail.PID);
                    oHypersensivity.HyperTypeSubID = p.htype1;
                    //oHypersensivity.RSubID = p.hrsubtype1;
                    //oHypersensivity.SeverityID = Convert.ToInt32(p.hstype1);
                    oHypersensivity.ModifiedDate = DateTime.Now;
                    oHypersensivity.HypersenseDetail = p.hstype1;


                    objHyp[j] = oHypersensivity;
                    j++;
                }
                // db.Vitals.Add(oVital);

            }



            String TransID = "";
            String PDID1 = "";
            String ToLoc = "";
            String FromLoc = "";
            DateTime TransferDate = DateTime.Now;
            // var trand= from v in db.TranferDetails where (v.PDID == patient_Detail.PDID) where (v.ToLoc == opdid) select new { v.TransID,v.PDID,v.ToLoc,v.FromLoc,v.TransferDate };
            DataTable oDataSetsp7 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            if (oSqlConnection.State == ConnectionState.Closed)
            {
                oSqlConnection.Open();
            }
            oSqlCommand = new SqlCommand();
            sqlQuery = "    select * FROM [MMS].[dbo].[TranferDetails] where PDID='" + patient_Detail.PDID + "' AND ToLoc='" + opdid + "'  ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            //  oSqlConnection.Open();
            oSqlCommand.CommandTimeout = 120;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetsp7);
            foreach (DataRow row in oDataSetsp7.Rows)
            {
                TransID = row["TransID"].ToString();
                PDID1 = row["PDID"].ToString();
                ToLoc = row["ToLoc"].ToString();
                FromLoc = row["FromLoc"].ToString();
                TransferDate = (DateTime)row["TransferDate"];

            }




           
            //foreach (var p in trand)
            //{
            //    TransID = p.TransID;
            //    PDID1 = p.PDID;
            //    ToLoc = p.ToLoc;
            //    FromLoc = p.FromLoc;
            //    TransferDate = (DateTime)p.TransferDate;
            //}
            TranferDetail oTranferDetails1 = new TranferDetail();
            oTranferDetails1.TransID = TransID;
            oTranferDetails1.TransStatus = 0;
            oTranferDetails1.ToLoc = ToLoc;
            oTranferDetails1.PDID = patient_Detail.PDID;
            oTranferDetails1.FromLoc = FromLoc;
            oTranferDetails1.TransferDate = TransferDate;


            ExamineGeneral oExamineGeneral = new ExamineGeneral();
            oExamineGeneral.Other = genex;
            oExamineGeneral.PDID = PDID;


            ExamineAbdominal oExamineAbdominal = new ExamineAbdominal();
            oExamineAbdominal.PDID = PDID;
            oExamineAbdominal.Other = abdex;


            ExamineCardiovascular oExamineCardiovascular = new ExamineCardiovascular();
            oExamineCardiovascular.PDID = PDID;
            oExamineCardiovascular.Other = cardex;

              ExamineCentralNervou oExamineCNervous = new ExamineCentralNervou();
            oExamineCNervous.PDID = PDID;
            oExamineCNervous.Other = cenex;

             ExamineRespiratory oExamineRespiratory = new ExamineRespiratory();
            oExamineRespiratory.PDID = PDID;
            oExamineRespiratory.Other = resex;

            ExamineOther oExamineOther = new ExamineOther();
            oExamineOther.PDID = PDID;
            oExamineOther.Other = othex;
            int g = 0;

            #region lab
        
           
            var objsl = (dynamic)null;
            int objcountl = 0;
            Lab_Report[] objLab_Report = new Lab_Report[250];
            Lab_Report[] objlab = new Lab_Report[1000];
            if (!String.IsNullOrEmpty(litems))
            {


                objsl = JsonConvert.DeserializeObject<List<Labreader>>(litems);



                int i = 0;

                foreach (Labreader p in objsl)
                {

                    Lab_Report oLab_Report = new Lab_Report();
                    var objl = from s in db.Lab_SubCategory.Where(f => f.CategoryID == p.labid) select new { s.LabTestID };
                    DataTable oDataSetsp8 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    if (oSqlConnection.State == ConnectionState.Closed)
                    {
                        oSqlConnection.Open();
                    }
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "    select * FROM [MMS].[dbo].[Lab_SubCategory] where CategoryID='" + p.labid + "'  ";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    //  oSqlConnection.Open();
                    oSqlCommand.CommandTimeout = 120;
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetsp8);
                    //foreach (DataRow row in oDataSetsp8.Rows)
                    //{
                    //    TransID = row["TransID"].ToString();
                    //    PDID1 = row["PDID"].ToString();
                    //    ToLoc = row["ToLoc"].ToString();
                    //    FromLoc = row["FromLoc"].ToString();
                    //    TransferDate = (DateTime)row["TransferDate"];

                    //}



                    //int objcountl1 = objl.Count();
                    foreach (DataRow row in oDataSetsp8.Rows) {



                        //var lablist = from t in db.Lab_Report.Where(pf => pf.PDID == PDID)
                        //              select new
                        //              { t.Lab_SubCategory.Lab_MainCategory.CategoryName, t.Lab_SubCategory.Lab_MainCategory.CategoryID, t.PDID, t.TestSID }; ;
                        //var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();

                        //var oldtestcntl = labl.ToList().Count;

                        SqlCommand cmdCount = new SqlCommand("SELECT count(*) from Lab_Report WHERE PDID = @PDID", oSqlConnection);
                        cmdCount.Parameters.AddWithValue("@PDID", PDID);
                      try {

                            if (oSqlConnection.State == ConnectionState.Closed)
                            {
                                oSqlConnection.Open();
                            }
                            int count = (int)cmdCount.ExecuteScalar();
                        


                        if (count < 1)
                    {
                        Lab_Report oLab_Reports = new Lab_Report();
                        oLab_Reports.LabTestID = row["LabTestID"].ToString();
                        oLab_Reports.RequestedLocID = locid;
                        oLab_Reports.RequestedTime = DateTime.Now;
                        oLab_Reports.PDID = PDID;
                            oLab_Reports.TestSID = PDID + "x0";
                        oLab_Reports.Issued = "0";
                            oLab_Reports.Isemail = 0;
                            oLab_Reports.IsPrint = "0";
                        oLab_Reports.Lab_Index = Guid.NewGuid().ToString();
                            objLab_Report[i] = oLab_Reports;
                        i++;

                        }else
                        {
                            Lab_Report oLab_Reports = new Lab_Report();
                            oLab_Reports.LabTestID = row["LabTestID"].ToString();
                            oLab_Reports.RequestedLocID = locid;
                            oLab_Reports.RequestedTime = DateTime.Now;
                            oLab_Reports.PDID = PDID;
                            oLab_Reports.Issued = "0";
                            oLab_Reports.IsPrint = "0";
                            oLab_Reports.Isemail = 0;
                            oLab_Reports.TestSID = PDID + "x"+ count;
                            oLab_Reports.Lab_Index = Guid.NewGuid().ToString();
                            objLab_Report[i] = oLab_Reports;
                            i++;
                        }
                    }
                        catch (Exception ex)
                    {

                    }
                } }
            }
            #endregion
            var objs =(dynamic)null;
            int objcount = 0;
            Drug_Prescription[] objDrug = new Drug_Prescription[1000];
            Drug_Regular[] objDrugreg = new Drug_Regular[1000];
            if (!String.IsNullOrEmpty(ditems))
            {


                 objs = JsonConvert.DeserializeObject<List<Drugreader>>(ditems);

                objcount = objs.Count;
            
            
            int i = 0;
                int r = 0;
                foreach (Drugreader p in objs)
            {
               
              //  var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                    DataTable oDataSetsp9 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    if (oSqlConnection.State == ConnectionState.Closed)
                    {
                        oSqlConnection.Open();
                    }
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "    select * FROM [MMS].[dbo].[Drug_Prescription] where ItemNo='" + p.dItemno + "' AND PDID='" + PDID + "' AND Issued=0 ";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    //  oSqlConnection.Open();
                    oSqlCommand.CommandTimeout = 120;
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetsp9);
                    //foreach (DataRow row in oDataSetsp9.Rows)
                    //{
                    //    TransID = row["TransID"].ToString();
                    //    PDID1 = row["PDID"].ToString();
                    //    ToLoc = row["ToLoc"].ToString();
                    //    FromLoc = row["FromLoc"].ToString();
                    //    TransferDate = (DateTime)row["TransferDate"];

                    //}




                    if (oDataSetsp9.Rows.Count < 1)
                {
                    Drug_Prescription oDrug_Prescription = new Drug_Prescription();
                    oDrug_Prescription.PDID = PDID;
                    oDrug_Prescription.Ps_Index = Guid.NewGuid().ToString();
                    oDrug_Prescription.Dose = p.dDose;
                    oDrug_Prescription.Method = p.dMethod;
                    oDrug_Prescription.Route = p.dRoute;
                    oDrug_Prescription.ItemNo = p.dItemno;
                        oDrug_Prescription.MethodType =Convert.ToInt32(p.dStockTypeID);
                        oDrug_Prescription.Duration = p.dDuration;
                        oDrug_Prescription.RequestedLocID = locid;
                    oDrug_Prescription.LocID = opdid;
                    oDrug_Prescription.Date_Time = DateTime.Now.Date;
                        oDrug_Prescription.Issued =0;
                        objDrug[i] = oDrug_Prescription;
                    i++;
                }
                    try
                    {
                        //var oldtestcntd = db.Drug_Regular.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == pid).ToList().Count;

                        DataTable oDataSetsp10 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        if (oSqlConnection.State == ConnectionState.Closed)
                        {
                            oSqlConnection.Open();
                        }
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "    select * FROM [MMS].[dbo].[Drug_Regular] where ItemNo='" + p.dItemno + "' AND PDID='" + pid + "' ";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        //  oSqlConnection.Open();
                        oSqlCommand.CommandTimeout = 120;
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSetsp10);
                        //foreach (DataRow row in oDataSetsp10.Rows)
                        //{
                        //    TransID = row["TransID"].ToString();
                        //    PDID1 = row["PDID"].ToString();
                        //    ToLoc = row["ToLoc"].ToString();
                        //    FromLoc = row["FromLoc"].ToString();
                        //    TransferDate = (DateTime)row["TransferDate"];

                        //}




                        if (p.disregdr != null)
                        {

                            if (oDataSetsp10.Rows.Count < 1 && p.disregdr.ToLower().Equals("true"))
                            {
                                Drug_Regular oDrug_Regular = new Drug_Regular();
                                oDrug_Regular.PDID = pid;
                                oDrug_Regular.Ps_Index = Guid.NewGuid().ToString();
                                oDrug_Regular.Dose = p.dDose;
                                oDrug_Regular.Method = p.dMethod;
                                oDrug_Regular.Route = p.dRoute;
                                oDrug_Regular.ItemNo = p.dItemno;
                                oDrug_Regular.MethodType = Convert.ToInt32(p.dStockTypeID);
                                oDrug_Regular.Duration = p.dDuration;
                                oDrug_Regular.RequestedLocID = locid;
                                oDrug_Regular.LocID = opdid;
                                oDrug_Regular.Date_Time = DateTime.Now.Date;
                                oDrug_Regular.Issued = 0;
                                objDrugreg[r] = oDrug_Regular;
                                r++;
                            }

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            var objsic = JsonConvert.DeserializeObject<List<Sickreader>>(sitems);
                int objcountsic = objsic.Count;
                Sick_Category[] objSick = new Sick_Category[objcountsic];
                int h = 0;

                foreach (Sickreader p in objsic)
                {
                    DateTime dt1 = DateTime.Now.Date;
                  //  var oldtestcnt = db.Sick_Category.Where(d => d.CatID == p.scatid).Where(d => d.PDID == PDID).Where(d => d.Date == dt1).ToList().Count;
                DataTable oDataSetsp11 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                oSqlCommand = new SqlCommand();
                sqlQuery = "    select * FROM [MMS].[dbo].[Sick_Category] where CatID='" + p.scatid + "' AND PDID='" + PDID + "' AND Date='"+ dt1 + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                //  oSqlConnection.Open();
                oSqlCommand.CommandTimeout = 120;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetsp11);
                //foreach (DataRow row in oDataSetsp11.Rows)
                //{
                //    TransID = row["TransID"].ToString();
                //    PDID1 = row["PDID"].ToString();
                //    ToLoc = row["ToLoc"].ToString();
                //    FromLoc = row["FromLoc"].ToString();
                //    TransferDate = (DateTime)row["TransferDate"];

                //}




                if (oDataSetsp11.Rows.Count < 1)
                    {
                        Sick_Category oSick_Category = new Sick_Category();
                        oSick_Category.PDID = PDID;
                        oSick_Category.CatIndex = indi.CreateSCID(h, PDID);
                        oSick_Category.CatPeriod = p.sdays;
                        oSick_Category.Date =Convert.ToDateTime(p.seff);
                        oSick_Category.LocID = locid;
                        oSick_Category.CatID = p.scatid;

                        objSick[h] = oSick_Category;
                        h++;
                    }

                }
           
            //if (ModelState.IsValid)
            //{
                try
                {
                   //tring othexr = "";
                    //var othexrv = from s in db.ExamineOthers.Where(p => p.PDID == PDID) select new { s.PDID };
                SqlCommand cmdCount = new SqlCommand("SELECT count(*) from ExamineOther WHERE PDID = @PDID", oSqlConnection);
                cmdCount.Parameters.AddWithValue("@PDID", PDID);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                int count = (int)cmdCount.ExecuteScalar();
                SqlCommand command;

                if (count > 0)
                {
                    command = new SqlCommand("UPDATE ExamineOther SET Other = @tracking WHERE PDID = @order", oSqlConnection);
                }
                else
                {
                    command = new SqlCommand("INSERT into ExamineOther (PDID, Other) VALUES (@order, @tracking)", oSqlConnection);
                }

                command.Parameters.AddWithValue("@order", oExamineOther.PDID);
                command.Parameters.AddWithValue("@tracking", oExamineOther.Other);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                int rowsUpdated = command.ExecuteNonQuery();

               

                //foreach (var item in othexrv)
                //    {

                //        othexr = item.PDID;
                //    }
                //    if (!String.IsNullOrEmpty(othexr))
                //    {
                //        db.Entry(oExamineOther).State = EntityState.Modified;
                //    }
                //    else
                //    {
                //        db.ExamineOthers.Add(oExamineOther);
                //    }
                //string General = "";
                //var Generalvar = from s in db.ExamineGenerals.Where(p => p.PDID == PDID) select new { s.PDID };

                //foreach (var item in Generalvar)
                //{

                //    General = item.PDID;
                //}
                //if (!String.IsNullOrEmpty(General))
                //{
                //    db.Entry(oExamineGeneral).State = EntityState.Modified;
                //}
                //else
                //{
                //    db.ExamineGenerals.Add(oExamineGeneral);
                //}
                cmdCount = new SqlCommand("SELECT count(*) from ExamineGeneral WHERE PDID = @PDID", oSqlConnection);
                cmdCount.Parameters.AddWithValue("@PDID", PDID);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                count = (int)cmdCount.ExecuteScalar();
               

                if (count > 0)
                {
                    command = new SqlCommand("UPDATE ExamineGeneral SET Other = @tracking WHERE PDID = @order", oSqlConnection);
                }
                else
                {
                    command = new SqlCommand("INSERT into ExamineGeneral (PDID, Other) VALUES (@order, @tracking)", oSqlConnection);
                }

                command.Parameters.AddWithValue("@order", oExamineGeneral.PDID);
                command.Parameters.AddWithValue("@tracking", oExamineGeneral.Other);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                rowsUpdated = command.ExecuteNonQuery();




                //string Abdominal = "";
                //    var Abdominalvar = from s in db.ExamineAbdominals.Where(p => p.PDID == PDID) select new { s.PDID };

                //    foreach (var item in Abdominalvar)
                //    {

                //        Abdominal = item.PDID;
                //    }
                //    if (!String.IsNullOrEmpty(Abdominal))
                //    {
                //        db.Entry(oExamineAbdominal).State = EntityState.Modified;
                //    }
                //    else
                //    {
                //        db.ExamineAbdominals.Add(oExamineAbdominal);
                //    }

                cmdCount = new SqlCommand("SELECT count(*) from ExamineAbdominal WHERE PDID = @PDID", oSqlConnection);
                cmdCount.Parameters.AddWithValue("@PDID", PDID);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                count = (int)cmdCount.ExecuteScalar();


                if (count > 0)
                {
                    command = new SqlCommand("UPDATE ExamineAbdominal SET Other = @tracking WHERE PDID = @order", oSqlConnection);
                }
                else
                {
                    command = new SqlCommand("INSERT into ExamineAbdominal (PDID, Other) VALUES (@order, @tracking)", oSqlConnection);
                }

                command.Parameters.AddWithValue("@order", oExamineAbdominal.PDID);
                command.Parameters.AddWithValue("@tracking", oExamineAbdominal.Other);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                rowsUpdated = command.ExecuteNonQuery();



                //string Cardiovascular = "";
                //    var Cardiovascularvar = from s in db.ExamineCardiovasculars.Where(p => p.PDID == PDID) select new { s.PDID };

                //    foreach (var item in Cardiovascularvar)
                //    {

                //        Cardiovascular = item.PDID;
                //    }
                //    if (!String.IsNullOrEmpty(Cardiovascular))
                //    {
                //        db.Entry(oExamineCardiovascular).State = EntityState.Modified;
                //    }
                //    else
                //    {
                //        db.ExamineCardiovasculars.Add(oExamineCardiovascular);
                //    }

                cmdCount = new SqlCommand("SELECT count(*) from ExamineCardiovascular WHERE PDID = @PDID", oSqlConnection);
                cmdCount.Parameters.AddWithValue("@PDID", PDID);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                count = (int)cmdCount.ExecuteScalar();


                if (count > 0)
                {
                    command = new SqlCommand("UPDATE ExamineCardiovascular SET Other = @tracking WHERE PDID = @order", oSqlConnection);
                }
                else
                {
                    command = new SqlCommand("INSERT into ExamineCardiovascular (PDID, Other) VALUES (@order, @tracking)", oSqlConnection);
                }

                command.Parameters.AddWithValue("@order", oExamineCardiovascular.PDID);
                command.Parameters.AddWithValue("@tracking", oExamineCardiovascular.Other);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                rowsUpdated = command.ExecuteNonQuery();



                //string CNervous = "";
                //    var CNervousvar = from s in db.ExamineCentralNervous.Where(p => p.PDID == PDID) select new { s.PDID };

                //    foreach (var item in CNervousvar)
                //    {

                //        CNervous = item.PDID;
                //    }
                //    if (!String.IsNullOrEmpty(CNervous))
                //    {
                //        db.Entry(oExamineCNervous).State = EntityState.Modified;
                //    }
                //    else
                //    {
                //        db.ExamineCentralNervous.Add(oExamineCNervous);
                //    }

                cmdCount = new SqlCommand("SELECT count(*) from ExamineCentralNervous WHERE PDID = @PDID", oSqlConnection);
                cmdCount.Parameters.AddWithValue("@PDID", PDID);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                count = (int)cmdCount.ExecuteScalar();


                if (count > 0)
                {
                    command = new SqlCommand("UPDATE ExamineCentralNervous SET Other = @tracking WHERE PDID = @order", oSqlConnection);
                }
                else
                {
                    command = new SqlCommand("INSERT into ExamineCentralNervous (PDID, Other) VALUES (@order, @tracking)", oSqlConnection);
                }

                command.Parameters.AddWithValue("@order", oExamineCNervous.PDID);
                command.Parameters.AddWithValue("@tracking", oExamineCNervous.Other);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                rowsUpdated = command.ExecuteNonQuery();



                //string Respiratory = "";
                //    var Respiratoryvar = from s in db.ExamineCardiovasculars.Where(p => p.PDID == PDID) select new { s.PDID };

                //    foreach (var item in Respiratoryvar)
                //    {

                //        Respiratory = item.PDID;
                //    }
                //    if (!String.IsNullOrEmpty(Respiratory))
                //    {
                //        db.Entry(oExamineRespiratory).State = EntityState.Modified;
                //    }
                //    else
                //    {
                //        db.ExamineRespiratories.Add(oExamineRespiratory);
                //    }

                cmdCount = new SqlCommand("SELECT count(*) from ExamineRespiratory WHERE PDID = @PDID", oSqlConnection);
                cmdCount.Parameters.AddWithValue("@PDID", PDID);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                count = (int)cmdCount.ExecuteScalar();


                if (count > 0)
                {
                    command = new SqlCommand("UPDATE ExamineRespiratory SET Other = @tracking WHERE PDID = @order", oSqlConnection);
                }
                else
                {
                    command = new SqlCommand("INSERT into ExamineRespiratory (PDID, Other) VALUES (@order, @tracking)", oSqlConnection);
                }

                command.Parameters.AddWithValue("@order", oExamineRespiratory.PDID);
                command.Parameters.AddWithValue("@tracking", oExamineRespiratory.Other);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                rowsUpdated = command.ExecuteNonQuery();



                ///////---------------
                //string pastmed = "";
                //    var pastmedvar = from s in db.PastMedHistories.Where(p => p.PID == pid) select new { s.PID };

                //    foreach (var item in pastmedvar)
                //    {

                //        pastmed = item.PID;
                //    }
                //    if (!String.IsNullOrEmpty(pastmed))
                //    {
                //        db.Entry(oPastMedHistory).State = EntityState.Modified;
                //    }
                //    else
                //    {
                //        db.PastMedHistories.Add(oPastMedHistory);
                //    }



                cmdCount = new SqlCommand("SELECT count(*) from PastMedHistory WHERE PID = @PDID", oSqlConnection);
                cmdCount.Parameters.AddWithValue("@PDID", pid);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                count = (int)cmdCount.ExecuteScalar();


                if (count > 0)
                {
                    command = new SqlCommand("UPDATE PastMedHistory SET PMHDetail = @PMHDetail,PID=@PID,Drughst=@Drughst WHERE PMHID = @PMHID", oSqlConnection);
                }
                else
                {
                    command = new SqlCommand("INSERT into PastMedHistory (PID, PMHDetail,Drughst) VALUES (@PID, @PMHDetail,@Drughst)", oSqlConnection);
                }

                command.Parameters.AddWithValue("@PID", oPastMedHistory.PID);
                command.Parameters.AddWithValue("@PMHDetail", oPastMedHistory.PMHDetail);
                command.Parameters.AddWithValue("@PMHID", oPastMedHistory.PMHID);
                command.Parameters.AddWithValue("@Drughst", oPastMedHistory.Drughst);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                rowsUpdated = command.ExecuteNonQuery();


                ///------------------

                //string catref = "";
                //    var catrefvar = from s in db.CatReferals.Where(p => p.PDID == PDID) select new { s.PDID };

                //    foreach (var item in catrefvar)
                //    {

                //        catref = item.PDID;
                //    }
                //    if (!String.IsNullOrEmpty(catref))
                //    {
                //        db.Entry(oCatReferal).State = EntityState.Modified;
                //    }
                //    else
                //    {
                //        db.CatReferals.Add(oCatReferal);
                //    }

                cmdCount = new SqlCommand("SELECT count(*) from CatReferals WHERE PDID = @PDID", oSqlConnection);
                cmdCount.Parameters.AddWithValue("@PDID", PDID);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                count = (int)cmdCount.ExecuteScalar();


                if (count > 0)
                {
                    command = new SqlCommand("UPDATE CatReferals SET PDID = @PDID,PlanofMgt=@PlanofMgt,ReffNote=@ReffNote WHERE reffid = @reffid", oSqlConnection);
                }
                else
                {
                    command = new SqlCommand("INSERT into CatReferals (PDID, PlanofMgt,ReffNote) VALUES (@PDID, @PlanofMgt,@ReffNote)", oSqlConnection);
                }

                command.Parameters.AddWithValue("@reffid", oCatReferal.reffid);
                command.Parameters.AddWithValue("@PDID", oCatReferal.PDID);
                command.Parameters.AddWithValue("@PlanofMgt", oCatReferal.PlanofMgt);
                command.Parameters.AddWithValue("@ReffNote", oCatReferal.ReffNote);
                if (oSqlConnection.State == ConnectionState.Closed)
                {
                    oSqlConnection.Open();
                }
                rowsUpdated = command.ExecuteNonQuery();




                ///-------------------
                if (!String.IsNullOrEmpty(modus))
                    {
                        if (modus.Trim().Equals(userid.ToString()))
                        {
                        //db.Entry(patient_Detail).State = EntityState.Modified;
                        command = new SqlCommand("UPDATE Patient_Detail SET Present_Complain = @Present_Complain,History_PresentComplain=@History_PresentComplain "+
                        "    ,Other_Complain=@Other_Complain,History_OtherComplain=@History_OtherComplain,OPD_Diagnosis=@OPD_Diagnosis,ModifiedDate=@ModifiedDate,ModifiedBy=@ModifiedBy " +
                        ", CreatedBy=@CreatedBy,PID=@PID,PatientCatID=@PatientCatID,SubjectID=@SubjectID,Examination=@Examination,OPDID=@OPDID,CreatedDate=@CreatedDate,Status=@Status WHERE PDID = @PDID", oSqlConnection);
                    }
                        else
                        {
                        //db.Patient_Detail.Add(patient_Detail);
                        command = new SqlCommand("INSERT into Patient_Detail (PDID, Present_Complain,History_PresentComplain,Other_Complain,History_OtherComplain,OPD_Diagnosis,ModifiedDate "+
                       " ,  ModifiedBy ,CreatedBy ,PID,PatientCatID,SubjectID,Examination,OPDID,CreatedDate,Status) VALUES (@PDID, @Present_Complain,@History_PresentComplain,@Other_Complain,@History_OtherComplain,@OPD_Diagnosis,@ModifiedDate " +
                       " ,  @ModifiedBy ,@CreatedBy ,@PID,@PatientCatID,@SubjectID,@Examination,@OPDID,@CreatedDate,@Status)", oSqlConnection);
                    }
                    }
                    else
                    {
                    // db.Entry(patient_Detail).State = EntityState.Modified;
                    command = new SqlCommand("UPDATE Patient_Detail SET Present_Complain = @Present_Complain,History_PresentComplain=@History_PresentComplain " +
                        "    ,Other_Complain=@Other_Complain,History_OtherComplain=@History_OtherComplain,OPD_Diagnosis=@OPD_Diagnosis,ModifiedDate=@ModifiedDate,ModifiedBy=@ModifiedBy, " +
                        " CreatedBy=@CreatedBy,PID=@PID,PatientCatID=@PatientCatID,SubjectID=@SubjectID,Examination=@Examination,OPDID=@OPDID,CreatedDate=@CreatedDate,Status=@Status WHERE PDID = @PDID", oSqlConnection);
                }
                command.Parameters.AddWithValue("@PDID", patient_Detail.PDID);
                command.Parameters.AddWithValue("@Present_Complain", patient_Detail.Present_Complain);
                command.Parameters.AddWithValue("@History_PresentComplain", patient_Detail.History_PresentComplain);
                command.Parameters.AddWithValue("@Other_Complain", patient_Detail.Other_Complain);
                command.Parameters.AddWithValue("@History_OtherComplain", patient_Detail.History_OtherComplain);
                command.Parameters.AddWithValue("@OPD_Diagnosis", patient_Detail.OPD_Diagnosis);
                command.Parameters.AddWithValue("@ModifiedDate", patient_Detail.ModifiedDate);
                command.Parameters.AddWithValue("@ModifiedBy", patient_Detail.ModifiedBy);
                command.Parameters.AddWithValue("@CreatedBy", patient_Detail.CreatedBy);
                command.Parameters.AddWithValue("@PID", patient_Detail.PID);
                command.Parameters.AddWithValue("@PatientCatID", patient_Detail.PatientCatID);
                command.Parameters.AddWithValue("@SubjectID", patient_Detail.SubjectID);
                command.Parameters.AddWithValue("@Examination ", patient_Detail.Examination);
                command.Parameters.AddWithValue("@OPDID", patient_Detail.OPDID);
                command.Parameters.AddWithValue("@CreatedDate", patient_Detail.CreatedDate);
                command.Parameters.AddWithValue("@Status", patient_Detail.Status);

                rowsUpdated = command.ExecuteNonQuery();
                if (oSqlConnection.State == ConnectionState.Open)
                {
                    oSqlConnection.Close();
                }
                //20171114//////////////////////////////////////////////
                objLab_Report = objLab_Report.Where(x => x != null).ToArray();
                using (SqlConnection oConnection = new SqlConnection(conStr))
                    {
                       
                    if (oConnection.State == ConnectionState.Closed)
                    {
                        oConnection.Open();
                    }

                    using (SqlTransaction oTransaction = oConnection.BeginTransaction())
                        {
                            using (SqlCommand oCommand = oConnection.CreateCommand())
                            {
                                oCommand.Transaction = oTransaction;
                                oCommand.CommandType = CommandType.Text;
                                oCommand.CommandText = "INSERT INTO Lab_Report (RequestedLocID, RequestedTime,PDID,Issued,IsPrint,Isemail,TestSID,Lab_Index,LabTestID) VALUES (@RequestedLocID, @RequestedTime,@PDID,@Issued,@IsPrint,@Isemail,@TestSID,@Lab_Index,@LabTestID);";
                                oCommand.Parameters.Add(new SqlParameter("@RequestedLocID", SqlDbType.VarChar));
                                oCommand.Parameters.Add(new SqlParameter("@RequestedTime", SqlDbType.DateTime));
                            oCommand.Parameters.Add(new SqlParameter("@PDID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Issued", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@IsPrint", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Isemail", SqlDbType.Int));
                            oCommand.Parameters.Add(new SqlParameter("@TestSID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Lab_Index", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@LabTestID", SqlDbType.VarChar));
                            try
                                {
                                    foreach (var oSetting in objLab_Report)
                                    {
                                    
                                        oCommand.Parameters[0].Value = oSetting.RequestedLocID;
                                        oCommand.Parameters[1].Value = oSetting.RequestedTime;
                                    oCommand.Parameters[2].Value = oSetting.PDID;
                                    oCommand.Parameters[3].Value = oSetting.Issued;
                                    oCommand.Parameters[4].Value = oSetting.IsPrint;
                                    oCommand.Parameters[5].Value = oSetting.Isemail;
                                    oCommand.Parameters[6].Value = oSetting.TestSID;
                                    oCommand.Parameters[7].Value = oSetting.Lab_Index;
                                    oCommand.Parameters[8].Value = oSetting.LabTestID;

                                    if (oCommand.ExecuteNonQuery() != 1)
                                        {
                                            //'handled as needed, 
                                            //' but this snippet will throw an exception to force a rollback
                                            //throw new InvalidProgramException();
                                        }
                                    }
                                    oTransaction.Commit();
                                }
                                catch (Exception)
                                {
                                    oTransaction.Rollback();
                                    throw;
                                }
                            }
                        }
                    }



                ///////////////////////////////////////////
                objVital = objVital.Where(x => x != null).ToArray();
                using (SqlConnection oConnection = new SqlConnection(conStr))
                {
                    if (oConnection.State == ConnectionState.Closed)
                    {
                        oConnection.Open();
                    }
                    using (SqlTransaction oTransaction = oConnection.BeginTransaction())
                    {
                        using (SqlCommand oCommand = oConnection.CreateCommand())
                        {
                            oCommand.Transaction = oTransaction;
                            oCommand.CommandType = CommandType.Text;
                            oCommand.CommandText = "INSERT INTO Vitals (PDID, VID,CreatedBy,CreatedDate,LocationID,LocID,Reading_Time,ModifiedDate,VTID,VitalValues,Status) VALUES (@PDID, @VID,@CreatedBy,@CreatedDate,@LocationID,@LocID,@Reading_Time,@ModifiedDate,@VTID,@VitalValues,@Status);";
                            oCommand.Parameters.Add(new SqlParameter("@PDID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@VID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NChar));
                            oCommand.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime));
                            oCommand.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@LocID", SqlDbType.Char));
                            oCommand.Parameters.Add(new SqlParameter("@Reading_Time", SqlDbType.DateTime));
                            oCommand.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.DateTime));
                            oCommand.Parameters.Add(new SqlParameter("@VTID", SqlDbType.Int));
                            oCommand.Parameters.Add(new SqlParameter("@VitalValues", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int));
                         
                            try
                            {
                                foreach (var oSetting in objVital)
                                {

                                    oCommand.Parameters[0].Value = oSetting.PDID;
                                    oCommand.Parameters[1].Value = oSetting.VID;
                                    oCommand.Parameters[2].Value = oSetting.CreatedBy;
                                    oCommand.Parameters[3].Value = oSetting.CreatedDate;
                                    oCommand.Parameters[4].Value = oSetting.LocationID;
                                    oCommand.Parameters[5].Value = oSetting.LocID;
                                    oCommand.Parameters[6].Value = oSetting.Reading_Time;
                                    oCommand.Parameters[7].Value = oSetting.ModifiedDate;
                                    oCommand.Parameters[8].Value = oSetting.VTID;
                                    oCommand.Parameters[9].Value = oSetting.VitalValues;
                                    oCommand.Parameters[10].Value = oSetting.Status;
                                 
                                    if (oCommand.ExecuteNonQuery() != 1)
                                    {
                                        //'handled as needed, 
                                        //' but this snippet will throw an exception to force a rollback
                                        //throw new InvalidProgramException();
                                    }
                                }
                                oTransaction.Commit();
                            }
                            catch (Exception)
                            {
                                oTransaction.Rollback();
                                throw;
                            }
                        }
                    }
                }
                //////////////////////////////////////////////////////////////
                objHyp = objHyp.Where(x => x != null).ToArray();
                using (SqlConnection oConnection = new SqlConnection(conStr))
                {
                    if (oConnection.State == ConnectionState.Closed)
                    {
                        oConnection.Open();
                    }
                    using (SqlTransaction oTransaction = oConnection.BeginTransaction())
                    {
                        using (SqlCommand oCommand = oConnection.CreateCommand())
                        {
                            oCommand.Transaction = oTransaction;
                            oCommand.CommandType = CommandType.Text;
                            oCommand.CommandText = "INSERT INTO Hypersensivity (PID, HypersenseID,HyperTypeSubID,RSubID,SeverityID,ModifiedDate,HypersenseDetail) VALUES (@PID, @HypersenseID,@HyperTypeSubID,@RSubID,@SeverityID,@ModifiedDate,@HypersenseDetail);";
                            oCommand.Parameters.Add(new SqlParameter("@PID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@HypersenseID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@HyperTypeSubID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@RSubID", SqlDbType.Int));
                            oCommand.Parameters.Add(new SqlParameter("@SeverityID", SqlDbType.Int));
                            oCommand.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.DateTime));
                            oCommand.Parameters.Add(new SqlParameter("@HypersenseDetail", SqlDbType.VarChar));
                           

                            try
                            {
                                foreach (var oSetting in objHyp)
                                {

                                    oCommand.Parameters[0].Value = oSetting.PID;
                                    oCommand.Parameters[1].Value = oSetting.HypersenseID;
                                    oCommand.Parameters[2].Value = oSetting.HyperTypeSubID;
                                    oCommand.Parameters[3].Value = oSetting.HyperTypeSubID;
                                    oCommand.Parameters[4].Value = oSetting.RSubID;
                                    oCommand.Parameters[5].Value = oSetting.SeverityID;
                                    oCommand.Parameters[6].Value = oSetting.HypersenseDetail;
                                   

                                    if (oCommand.ExecuteNonQuery() != 1)
                                    {
                                        //'handled as needed, 
                                        //' but this snippet will throw an exception to force a rollback
                                        //throw new InvalidProgramException();
                                    }
                                }
                                oTransaction.Commit();
                            }
                            catch (Exception)
                            {
                                oTransaction.Rollback();
                                throw;
                            }
                        }
                    }
                }

                //////////////////////////////////////////////////////////////
                objDrug = objDrug.Where(x => x != null).ToArray();
                using (SqlConnection oConnection = new SqlConnection(conStr))
                {
                    if (oConnection.State == ConnectionState.Closed)
                    {
                        oConnection.Open();
                    }
                    using (SqlTransaction oTransaction = oConnection.BeginTransaction())
                    {
                        using (SqlCommand oCommand = oConnection.CreateCommand())
                        {
                            oCommand.Transaction = oTransaction;
                            oCommand.CommandType = CommandType.Text;
                            oCommand.CommandText = "INSERT INTO Drug_Prescription (PDID, Ps_Index,Dose,Method,Route,ItemNo,MethodType,Duration,RequestedLocID,LocID,Date_Time,Issued) VALUES (@PDID, @Ps_Index,@Dose,@Method,@Route,@ItemNo,@MethodType,@Duration,@RequestedLocID,@LocID,@Date_Time,@Issued);";
                            oCommand.Parameters.Add(new SqlParameter("@PDID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Ps_Index", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Dose", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Method", SqlDbType.Int));
                            oCommand.Parameters.Add(new SqlParameter("@Route", SqlDbType.Int));
                            oCommand.Parameters.Add(new SqlParameter("@ItemNo", SqlDbType.NVarChar));
                            oCommand.Parameters.Add(new SqlParameter("@MethodType", SqlDbType.Int));
                            oCommand.Parameters.Add(new SqlParameter("@Duration", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@RequestedLocID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@LocID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Date_Time", SqlDbType.DateTime));
                            oCommand.Parameters.Add(new SqlParameter("@Issued", SqlDbType.Int));
                            try
                            {
                                foreach (var oSetting in objDrug)
                                {

                                    oCommand.Parameters[0].Value = oSetting.PDID;
                                    oCommand.Parameters[1].Value = oSetting.Ps_Index;
                                    oCommand.Parameters[2].Value = oSetting.Dose;
                                    oCommand.Parameters[3].Value = oSetting.Method;
                                    oCommand.Parameters[4].Value = oSetting.Route;
                                    oCommand.Parameters[5].Value = oSetting.ItemNo;
                                    oCommand.Parameters[6].Value = oSetting.MethodType;
                                    oCommand.Parameters[7].Value = oSetting.Duration;
                                    oCommand.Parameters[8].Value = oSetting.RequestedLocID;
                                    oCommand.Parameters[9].Value = oSetting.LocID;
                                    oCommand.Parameters[10].Value = oSetting.Date_Time;
                                    oCommand.Parameters[11].Value = oSetting.Issued;

                                    if (oCommand.ExecuteNonQuery() != 1)
                                    {
                                        //'handled as needed, 
                                        //' but this snippet will throw an exception to force a rollback
                                        //throw new InvalidProgramException();
                                    }
                                }
                                oTransaction.Commit();
                            }
                            catch (Exception)
                            {
                                oTransaction.Rollback();
                                throw;
                            }
                        }
                    }
                }

                //////////////////////////////////////////////////////////////
                objDrugreg = objDrugreg.Where(x => x != null).ToArray();
                using (SqlConnection oConnection = new SqlConnection(conStr))
                {
                    if (oConnection.State == ConnectionState.Closed)
                    {
                        oConnection.Open();
                    }
                    using (SqlTransaction oTransaction = oConnection.BeginTransaction())
                    {
                        using (SqlCommand oCommand = oConnection.CreateCommand())
                        {
                            oCommand.Transaction = oTransaction;
                            oCommand.CommandType = CommandType.Text;
                            oCommand.CommandText = "INSERT INTO Drug_Regular (PDID, Ps_Index,Dose,Method,Route,ItemNo,MethodType,Duration,RequestedLocID,LocID,Date_Time,Issued) VALUES (@PDID, @Ps_Index,@Dose,@Method,@Route,@ItemNo,@MethodType,@Duration,@RequestedLocID,@LocID,@Date_Time,@Issued);";
                            oCommand.Parameters.Add(new SqlParameter("@PDID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Ps_Index", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Dose", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Method", SqlDbType.Int));
                            oCommand.Parameters.Add(new SqlParameter("@Route", SqlDbType.Int));
                            oCommand.Parameters.Add(new SqlParameter("@ItemNo", SqlDbType.NVarChar));
                            oCommand.Parameters.Add(new SqlParameter("@MethodType", SqlDbType.Int));
                            oCommand.Parameters.Add(new SqlParameter("@Duration", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@RequestedLocID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@LocID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Date_Time", SqlDbType.DateTime));
                            oCommand.Parameters.Add(new SqlParameter("@Issued", SqlDbType.Int));

                            try
                            {
                                foreach (var oSetting in objDrugreg)
                                {

                                    oCommand.Parameters[0].Value = oSetting.PDID;
                                    oCommand.Parameters[1].Value = oSetting.Ps_Index;
                                    oCommand.Parameters[2].Value = oSetting.Dose;
                                    oCommand.Parameters[3].Value = oSetting.Method;
                                    oCommand.Parameters[4].Value = oSetting.Route;
                                    oCommand.Parameters[5].Value = oSetting.ItemNo;
                                    oCommand.Parameters[6].Value = oSetting.MethodType;
                                    oCommand.Parameters[7].Value = oSetting.Duration;
                                    oCommand.Parameters[8].Value = oSetting.RequestedLocID;
                                    oCommand.Parameters[9].Value = oSetting.LocID;
                                    oCommand.Parameters[10].Value = oSetting.Date_Time;
                                    oCommand.Parameters[11].Value = oSetting.Issued;

                                    if (oCommand.ExecuteNonQuery() != 1)
                                    {
                                        //'handled as needed, 
                                        //' but this snippet will throw an exception to force a rollback
                                        //throw new InvalidProgramException();
                                    }
                                }
                                oTransaction.Commit();
                            }
                            catch (Exception)
                            {
                                oTransaction.Rollback();
                                throw;
                            }
                        }
                    }
                }

                //////////////////////////////////////////////////////////////
                objcatd = objcatd.Where(x => x != null).ToArray();
                using (SqlConnection oConnection = new SqlConnection(conStr))
                {
                    if (oConnection.State == ConnectionState.Closed)
                    {
                        oConnection.Open();
                    }
                    using (SqlTransaction oTransaction = oConnection.BeginTransaction())
                    {
                        using (SqlCommand oCommand = oConnection.CreateCommand())
                        {
                            oCommand.Transaction = oTransaction;
                            oCommand.CommandType = CommandType.Text;
                            if(modus != null)
                                {
                                if (modus.Trim().Equals(userid.ToString()))
                                {
                                    oCommand.CommandText = "UPDATE CatDiagList set dgid=@dgid  WHERE PDID=@PDID;";
                                }
                                else
                                {
                                    oCommand.CommandText = "INSERT INTO CatDiagList (dgid, PDID) VALUES (@dgid, @PDID);";
                                }
                            }
                            oCommand.Parameters.Add(new SqlParameter("@dgid", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@PDID", SqlDbType.VarChar));
                          

                            try
                            {
                                foreach (var oSetting in objcatd)
                                {

                                    oCommand.Parameters[0].Value = oSetting.dgid;
                                    oCommand.Parameters[1].Value = oSetting.PDID;
                                    


                                    if (oCommand.ExecuteNonQuery() != 1)
                                    {
                                        //'handled as needed, 
                                        //' but this snippet will throw an exception to force a rollback
                                        //throw new InvalidProgramException();
                                    }
                                }
                                oTransaction.Commit();
                            }
                            catch (Exception)
                            {
                                oTransaction.Rollback();
                                throw;
                            }
                        }
                    }
                }

                //////////////////////////////////////////////////////////////
                objSick = objSick.Where(x => x != null).ToArray();
                using (SqlConnection oConnection = new SqlConnection(conStr))
                {
                    if (oConnection.State == ConnectionState.Closed)
                    {
                        oConnection.Open();
                    }
                    using (SqlTransaction oTransaction = oConnection.BeginTransaction())
                    {
                        using (SqlCommand oCommand = oConnection.CreateCommand())
                        {
                            oCommand.Transaction = oTransaction;
                            oCommand.CommandType = CommandType.Text;
                            oCommand.CommandText = "INSERT INTO Sick_Category (PDID, CatIndex,CatPeriod,Date,LocID,CatID) VALUES (@PDID, @CatIndex,@CatPeriod,@Date,@LocID,@CatID);";
                            oCommand.Parameters.Add(new SqlParameter("@PDID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@CatIndex", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@CatPeriod", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime));
                            oCommand.Parameters.Add(new SqlParameter("@LocID", SqlDbType.VarChar));
                            oCommand.Parameters.Add(new SqlParameter("@CatID", SqlDbType.Int));

                            try
                            {
                                foreach (var oSetting in objSick)
                                {

                                    oCommand.Parameters[0].Value = oSetting.PDID;
                                    oCommand.Parameters[1].Value = oSetting.CatIndex;
                                    oCommand.Parameters[2].Value = oSetting.CatPeriod;
                                    if (oSetting.CatPeriod==null)
                                    {
                                        oCommand.Parameters[2].Value ="";
                                    }
                                    
                                    oCommand.Parameters[3].Value = oSetting.Date;
                                    oCommand.Parameters[4].Value = oSetting.LocID;
                                    oCommand.Parameters[5].Value = oSetting.CatID;


                                    if (oCommand.ExecuteNonQuery() != 1)
                                    {
                                        //'handled as needed, 
                                        //' but this snippet will throw an exception to force a rollback
                                        //throw new InvalidProgramException();
                                    }
                                }
                                oTransaction.Commit();
                            }
                            catch (Exception we)
                            {
                                oTransaction.Rollback();
                                throw;
                            }
                        }
                    }
                }

                //////////////////////////////////////////////////////////////
                //objLab_Report = objLab_Report.Where(x => x != null).ToArray();
                //    db.Lab_Report.AddRange(objLab_Report);
                //db.Vitals.AddRange(objVital);

                // db.Hypersensivities.AddRange(objHyp);

                // db.Drug_Prescription.AddRange(objDrug);

                //  db.Drug_Regular.AddRange(objDrugreg);


                // db.CatDiagLists.AddRange(objcatd);
               
                 //   db.Sick_Category.AddRange(objSick);
                    //if (pntstatus=="7")
                    //{
                    //    db.TranferDetails.Add(oTranferDetails);
                    //    db.Entry(oTranferDetails1).State = EntityState.Modified;
                    //}
                    //db.SaveChanges();
                    err = "Saved";


                    //db.SaveChanges();
                    // db.Vitals.AddRange(objVital);

                    //db.Patient_Detail.Add(patient_Detail);

                }
                catch (Exception ex)
                {
                    err=ex.ToString();

                }
           // }

            return Json(err, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult loadchildimg(string id)
        //{
        //    db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
        //    string imageDataURL = "";
        //    try
        //    {
              

               




        //        df = GetEncoderInfo("image/jpeg");
        //    char[] MyChar = { '/', '"', ' ' };
        //    string NewString = id.Trim(MyChar);
        //    var ser = from s in dbhrms.ServicePersonnelProfiles.Where(p => p.ServiceNo == NewString) select new { s.ProfilePicture };
        //    byte[] imageByteData;
            
        //    foreach (var item in ser)
        //    {
        //        imageByteData = item.ProfilePicture;
        //        MemoryStream ms;
        //        using (var ms1 = new MemoryStream(imageByteData))
        //        {
        //            Image fg = Image.FromStream(ms1);
        //            using (var img = fg)
        //            {
        //                ms = compress(img, 30, df);
        //            }
        //        }



        //        using (var compressedImage = Image.FromStream(ms))
        //        {
        //            imageByteData = ImageToByteArray(compressedImage);
        //        }


                
        //        string imageBase64Data = Convert.ToBase64String(imageByteData);
        //                base64String = imageBase64Data;
                    
        //            imageDataURL = string.Format("data:image/png;base64,{0}", base64String);
        //        ViewBag.ImageData = imageDataURL;
                   
        //    }
        //    return Json(imageDataURL, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return null;
        //}
        public JsonResult loadchildimg1(string id)
        {
            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                String locid = (String)Session["userloc"];
                String ips = "";
                var iplist = from s in db.ImagePaths
                                           //join b in dbhrms.ranks on s.Rank equals b.RANK1
                                       where s.locid == locid
                             select new { s.filepath };
                foreach (var item in iplist)
                {
                    ips = item.filepath;
                }
                df = GetEncoderInfo("image/jpeg");
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                byte[] imageByteData;
                string imageDataURL = "";
                int? stype = 0;
                string svcno = "";
                var img = new List<Vw_PsnlImageP3>();
                try
                {
                    DataTable oDataSet31 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "    select * FROM [MMS].[dbo].[Vw_PsnlImageP2] where SerNo='" + NewString + "'   ";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    //  oSqlConnection.Open();
                    oSqlCommand.CommandTimeout = 120;
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet31);
                    //  oSqlConnection.Close();
                    img = oDataSet31.AsEnumerable()
           .Select(dataRow => new Vw_PsnlImageP3
           {

               SerNo = dataRow.Field<string>("SerNo"),
               ProfilePicture = dataRow.Field<byte[]>("ProfilePicture"),
               

           }).ToList();


                    if (img.Count() < 1)
                    {
                        DataTable oDataSet32 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "   select * FROM [MMS].[dbo].[Vw_PsnlImageP3] where SerNo='" + NewString + "'  ";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        //  oSqlConnection.Open();
                        oSqlCommand.CommandTimeout = 120;
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSet32);
                        //  oSqlConnection.Close();
                         img = oDataSet32.AsEnumerable()
                     .Select(dataRow => new Vw_PsnlImageP3
                     {

                         SerNo = dataRow.Field<string>("SerNo"),
                         ProfilePicture = dataRow.Field<byte[]>("ProfilePicture"),

                     }).ToList();
                    }
                }
                catch (Exception ex)
                {

                }



                //var PersonResultList1 = from s in dbp2.ServicePersonnelProfiles

                //                        where s.ServiceNo == NewString
                //                        select new { s.Service_Type };
                //foreach (var item in PersonResultList1)
                //{
                //    stype = item.Service_Type;
                //    break;
                //}
                //if (stype == 0)
                //{
                //    var PersonResultList = from s in dbhrms.ServicePersonnelProfiles
                //                               //join b in dbhrms.ranks on s.Rank equals b.RANK1
                //                           where s.ActiveNo == NewString
                //                           select new { s.Service_Type };

                //    foreach (var item in PersonResultList)
                //    {
                //        stype = item.Service_Type;
                //    }
                   
                //}

                //if (stype != 0)
                //{
                //    if (stype == 1001)
                //    {
                        
                //        svcno = Regex.Match(NewString, @"\d+").Value;
                //        svcno = "100" + svcno;
                //    }
                //    else if (stype == 1002)    // Officer Woman
                //    {
                //        svcno = Regex.Match(NewString, @"\d+").Value;
                //        svcno = "101" + svcno;
                       
                //    }
                //    else if (stype == 1003|| stype == 1004 )
                //    {
                //        string[] NewString1 = NewString.Split('/');
                //        svcno = "200" + NewString1[1];
                //    }
                //    else if (stype == 1005)
                //    {
                        
                //        svcno = Regex.Match(NewString, @"\d+").Value;
                //        svcno = "102" + svcno;
                //    }
                //    else if (stype == 1006)
                //    {
                //       string[] NewString1 = NewString.Split('/');
                //        svcno = "103" + NewString1[1];
                //    }
                //    else if (stype == 1007 || stype == 1008)   //Vol.Svc No
                //    {
                //        svcno = Regex.Match(NewString, @"\d+").Value;
                //        svcno = "202" + svcno;
                      
                //    }
                //}


                try {
                    foreach (var item in img)
                    {
                        Byte[] outputBytes;
                        var jpegQuality = 50;
                        Image image;
                        using (var inputStream = new MemoryStream(item.ProfilePicture))
                        {
                            image = Image.FromStream(inputStream);
                            var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                              .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                            var encoderParameters = new EncoderParameters(1);
                            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, jpegQuality);
                           
                            using (var outputStream = new MemoryStream())
                            {
                                image.Save(outputStream, jpegEncoder, encoderParameters);
                                outputBytes = outputStream.ToArray();
                            }
                        }


                        base64String = Convert.ToBase64String(outputBytes);
                    }
                    //using (WebClient client2 = new WebClient())
                    //{



                    //    byte[] imageBytes = client2.DownloadData(ips + svcno + ".jpg");

                    //    // Convert byte[] to Base64 String
                    //    base64String = Convert.ToBase64String(imageBytes);

                    //}

                    //using (Image image = Image.FromFile(ips+ svcno + ".jpg"))
                    //{
                    //    using (MemoryStream m = new MemoryStream())
                    //    {
                    //        image.Save(m, image.RawFormat);
                    //        byte[] imageBytes = m.ToArray();

                    //        // Convert byte[] to Base64 String
                    //        base64String = Convert.ToBase64String(imageBytes);

                    //    }
                    //}
                }
                catch (Exception ex)
            {
                

                    return Json(string.Format("data:image/png;base64,{0}", ""), JsonRequestBehavior.AllowGet);
                }

            //if (String.IsNullOrEmpty(base64String))
            //    {



            //        var ser = from s in db.Patients.Where(p => p.ServiceNo == NewString) select new { s.ProfilePicture };

            //        foreach (var item in ser)
            //        {
            //            imageByteData = item.ProfilePicture;
            //            MemoryStream ms;
            //            using (var ms1 = new MemoryStream(imageByteData))
            //            {
            //                Image fg = Image.FromStream(ms1);
            //                using (var img = fg)
            //                {
            //                    ms = compress(img, 30, df);
            //                }
            //            }



            //            using (var compressedImage = Image.FromStream(ms))
            //            {
            //                imageByteData = ImageToByteArray(compressedImage);
            //            }

            //            string imageBase64Data = Convert.ToBase64String(imageByteData);
            //            base64String = imageBase64Data;

            //        }

            //    }
                imageDataURL = string.Format("data:image/png;base64,{0}", base64String);
                ViewBag.ImageData = imageDataURL;

                return Json(imageDataURL, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
              

                return Json(string.Format("data:image/png;base64,{0}", ""), JsonRequestBehavior.AllowGet);
            }
           

        }
        public JsonResult loadimgbystp(string stp, string id )
        {
            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                String locid = (String)Session["userloc"];
                String ips = "";
                var iplist = from s in db.ImagePaths
                                 //join b in dbhrms.ranks on s.Rank equals b.RANK1
                             where s.locid == locid
                             select new { s.filepath };
                foreach (var item in iplist)
                {
                    ips = item.filepath;
                }
                df = GetEncoderInfo("image/jpeg");
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                byte[] imageByteData;
                string imageDataURL = "";
                int? stype = 0;
                string svcno = "";
                stp = stp.Trim(MyChar);
                var img = new List<Vw_PsnlImageP3>();
                if (stp=="1") {
                    
                        DataTable oDataSet31 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "    select * FROM [MMS].[dbo].[Vw_PsnlImageP2] where SerNo='" + NewString + "'   ";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        //  oSqlConnection.Open();
                        oSqlCommand.CommandTimeout = 120;
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSet31);
                        //  oSqlConnection.Close();
                        img = oDataSet31.AsEnumerable()
               .Select(dataRow => new Vw_PsnlImageP3
               {

                   SerNo = dataRow.Field<string>("SerNo"),
                   ProfilePicture = dataRow.Field<byte[]>("ProfilePicture"),


               }).ToList();

                    }
                    if (stp == "2")
                    {
                        DataTable oDataSet32 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "   select * FROM [MMS].[dbo].[Vw_PsnlImageP3] where SerNo='" + NewString + "'  ";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        //  oSqlConnection.Open();
                        oSqlCommand.CommandTimeout = 120;
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSet32);
                        //  oSqlConnection.Close();
                        img = oDataSet32.AsEnumerable()
                    .Select(dataRow => new Vw_PsnlImageP3
                    {

                        SerNo = dataRow.Field<string>("SerNo"),
                        ProfilePicture = dataRow.Field<byte[]>("ProfilePicture"),

                    }).ToList();
                    }
                



               


                try
                {
                    foreach (var item in img)
                    {
                        Byte[] outputBytes;
                        var jpegQuality = 50;
                        Image image;
                        using (var inputStream = new MemoryStream(item.ProfilePicture))
                        {
                            image = Image.FromStream(inputStream);
                            var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                              .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                            var encoderParameters = new EncoderParameters(1);
                            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, jpegQuality);

                            using (var outputStream = new MemoryStream())
                            {
                                image.Save(outputStream, jpegEncoder, encoderParameters);
                                outputBytes = outputStream.ToArray();
                            }
                        }


                        base64String = Convert.ToBase64String(outputBytes);
                    }
                   
                }
                catch (Exception ex)
                {


                    return Json(string.Format("data:image/png;base64,{0}", ""), JsonRequestBehavior.AllowGet);
                }

             
                imageDataURL = string.Format("data:image/png;base64,{0}", base64String);
                ViewBag.ImageData = imageDataURL;

                return Json(imageDataURL, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {


                return Json(string.Format("data:image/png;base64,{0}", ""), JsonRequestBehavior.AllowGet);
            }


        }
        // GET: Vital Types
        public JsonResult GetRelationships()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            var types = db.RelationshipTypes.Select(x => new { x.RTypeID, x.Relationship }).ToList();
            return Json(types, JsonRequestBehavior.AllowGet);

        }
        private MemoryStream compress(System.Drawing.Image img, long quality, ImageCodecInfo codec)
        {
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);

            var ms = new MemoryStream();
            img.Save(ms, codec, parameters);
            return ms;
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public JsonResult GetPatientsick3(string id, string relet)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string opdid = (String)Session["userlocid1"];
                if (opdid.Contains("cl"))
                {
                }
                string pid = "";
                string svc = "";
                string iserror = "";
                string medcat = "";
                string sexc = "";
                string relationp = "";
                int svt = 0;
                string svty = "";
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                char[] MyChar1 = { '/', '"', ' ' };
                string imageDataURL = "";
                string locid = (String)Session["userloc"];
                int NewString1 = Convert.ToInt32(relet.Trim(MyChar1));
                List<getnursedata> nlist = new List<getnursedata>();
                DateTime dd = DateTime.Now.Date;

                string blackno = "";


                //////////////////////////////////////////////////////////
                DataTable oDataSetix = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();

                sqlQuery = "SELECT * FROM [MMS].[dbo].[Vw_BlackListManagement] where [Service No]='" + NewString + "'  ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(oDataSetix);
                oSqlConnection.Close();
                var opd2ix = oDataSetix.AsEnumerable()
        .Select(dataRow => new getnursedata
        {
            PID = dataRow.Field<string>("Service No"),


        }).ToList();
                foreach (var i in opd2ix)
                {
                    blackno = i.PID;
                }
                /////////////////////////////////////////////////




                var ser = from s in db.Patient_Detail.Where(p => p.PDID == NewString) join b in db.Patients on s.PID equals b.PID where(b.RelationshipType == NewString1)
                          join y in db.Patients on s.PID equals y.PID
                          join w in db.PersonalDetails on y.ServiceNo equals w.ServiceNo
                          join x in db.MedicalCategories on w.SNo equals x.SNo
                          
                          select new getnursedata { PID = s.PID, RNK_NAME = "", Initials = b.Initials, Surname = b.Surname, SxDetail =w.Gender, Category = x.MedicalCategory1, Relationship = b.RelationshipType.ToString(), sv = b.Service_Type, dob = b.DateOfBirth,sno=b.ServiceNo };
                foreach (var item in ser)
                {

                    pid = item.PID;
                    medcat = item.Category;
                    sexc = item.SxDetail;
                    svt = item.sv;
                    relationp = item.Relationship;
                    svc = item.sno;
                    if (item.Relationship == "1")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.MedicalCategories on s.SNo equals b.SNo
                                                into sc
                                                from b in sc.DefaultIfEmpty()
                                                where s.ServiceNo == svc
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = s.Initials, Surname = s.Surname, SxDetail = s.Gender, Category = b.MedicalCategory1, Relationship = relationp, sv = svt, svt = s.ServiceStatus, dob = s.DateOfBirth, sno =svc };
                        foreach (var itemx in PersonResultList1)
                        {
                            svty = itemx.svt;
                            break;
                        }


                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }






                    }
                    if (item.Relationship.ToLower() == "2")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.SpouseDetails on s.SNo equals b.SNo
                                                where s.ServiceNo == svc
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.SpouseName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt, sno = svc };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "3")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo
                                                where s.ServiceNo == svc && b.Relationship == "Father"
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ParentName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt, sno = svc };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "4")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo

                                                where s.ServiceNo == svc && b.Relationship == "Mother"
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ParentName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt, sno = svc };
                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "5")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.Children on s.SNo equals b.SNo

                                                where s.ServiceNo == svc && b.DOB == item.dob
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ChildName, SxDetail = b.Gender, Category = "", Relationship = relationp, sv = svt, dob = b.DOB, sno = svc };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());

                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }


                    }
                    if (nlist.Count() < 1)
                    {

                        nlist.Add(item);
                    }


                }
                List<meddata> sicd = new List<meddata>();
                try
                {
                    DataTable oDataSet3 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " SELECT *  FROM [MMS].[dbo].[MedicalScreen] as a left outer join [MMS].[dbo].[MedicalStatus] as b on  "+
" b.ServiceNo = '"+svc+"' and b.ExamYear = a.msyear where pdid = '"+NewString+"'";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet3);
                    //  oSqlConnection.Close();
                     sicd = oDataSet3.AsEnumerable()
           .Select(dataRow => new meddata
           {
               pdid = dataRow.Field<string>("pdid"),
               msage = dataRow.Field<string>("msage"),

               msbmi = dataRow.Field<string>("msbmi"),

               msbp = dataRow.Field<string>("msbp"),
               msexecg = dataRow.Field<string>("msexecg"),
               msfbs = dataRow.Field<string>("msfbs"),

               mshbac = dataRow.Field<string>("mshbac"),
               msheight = dataRow.Field<string>("msheight"),
               mstotalc = dataRow.Field<string>("mstotalc"),
               msusugar = dataRow.Field<string>("msusugar"),
               msvision = dataRow.Field<string>("msvision"),
               msweight = dataRow.Field<string>("msweight"),
               msyear = dataRow.Field<int?>("msyear"),
               msid = dataRow.Field<long>("msid"),
               dentalst = dataRow.Field<int?>("MedicalStatus"),

               msspecs = dataRow.Field<string>("msspecs"),
               pftsession= dataRow.Field<int>("pftsession"),

           }).ToList();
                    sikcnt = sicd.Count();
                }
                catch (Exception ex)
                {
                 string eee= ex.ToString();
                }
            
                             
                    
                
                if (String.IsNullOrEmpty(pid))
                {
                    iserror = "1";
                }


                else if ( sikcnt < 1)
                {


                    iserror = "4";



                }
                else
                {






                    if (!String.IsNullOrEmpty(blackno))

                    {
                        iserror = "5";
                    }



                    var result = new { serv = nlist.ToList(), serv1 = sicd, imgd = imageDataURL, err = iserror };
                    // return Json(result, JsonRequestBehavior.AllowGet);

                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;

                    return jsonResult;
                }
                var result2 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = iserror };
                return Json(result2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result3 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = "2" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPatientmst(string id, string relet,string id2)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string opdid = (String)Session["userlocid1"];
                if (opdid.Contains("cl"))
                {
                }
                string pid = "";
                string iserror = "";
                string medcat = "";
                string sexc = "";
                string relationp = "";
                int svt = 0;
                string svty = "";
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                int yerr =Convert.ToInt32( id2.Trim(MyChar));
                char[] MyChar1 = { '/', '"', ' ' };
                string imageDataURL = "";
                string locid = (String)Session["userloc"];
                int NewString1 = Convert.ToInt32(relet.Trim(MyChar1));
                List<getnursedata> nlist = new List<getnursedata>();
                DateTime dd = DateTime.Now.Date;

                var ser = from s in db.Patients.Where(p => p.ServiceNo == NewString).Where(p => p.RelationshipType == NewString1)
                          join w in db.PersonalDetails on s.ServiceNo equals w.ServiceNo
                          join x in db.MedicalCategories on w.SNo equals x.SNo
                          select new getnursedata { PID = s.PID, RNK_NAME = "", Initials = s.Initials, Surname = s.Surname, SxDetail = w.Gender, Category = x.MedicalCategory1, Relationship = s.RelationshipType.ToString(), sv = s.Service_Type, dob = s.DateOfBirth };
                foreach (var item in ser)
                {

                    pid = item.PID;
                    medcat = item.Category;
                    sexc = item.SxDetail;
                    svt = item.sv;
                    relationp = item.Relationship;

                    if (item.Relationship == "1")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.MedicalCategories on s.SNo equals b.SNo
                                                into sc
                                                from b in sc.DefaultIfEmpty()
                                                where s.ServiceNo == NewString
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = s.Initials, Surname = s.Surname, SxDetail = s.Gender, Category = b.MedicalCategory1, Relationship = relationp, sv = svt, svt = s.ServiceStatus, dob = s.DateOfBirth };
                        foreach (var itemx in PersonResultList1)
                        {
                            svty = itemx.svt;
                            break;
                        }


                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }






                    }
                    if (item.Relationship.ToLower() == "2")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.SpouseDetails on s.SNo equals b.SNo
                                                where s.ServiceNo == NewString
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.SpouseName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "3")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo
                                                where s.ServiceNo == NewString && b.Relationship == "Father"
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ParentName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "4")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo

                                                where s.ServiceNo == NewString && b.Relationship == "Mother"
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ParentName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };
                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "5")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.Children on s.SNo equals b.SNo

                                                where s.ServiceNo == NewString && b.DOB == item.dob
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ChildName, SxDetail = b.Gender, Category = "", Relationship = relationp, sv = svt, dob = b.DOB };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());

                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }


                    }
                    if (nlist.Count() < 1)
                    {

                        nlist.Add(item);
                    }


                }


                var sicd = from s in db.MedicalScreens.Where(p => p.msyear == yerr)
                           join b in db.Patient_Detail on s.pdid equals b.PDID
                        into sc
                           from b in sc.DefaultIfEmpty()
                           join c in db.Patients on b.PID equals c.PID
                       into sd
                           from c in sd.DefaultIfEmpty()
                           join d in db.MedicalStatus on c.ServiceNo equals d.ServiceNo
                     into sf
                           from d in sf.DefaultIfEmpty()

                           .Where(p => p.ServiceNo == NewString)

                           orderby s.createddate descending
                           select new meddata { pdid = s.pdid, msage = s.msage, msbmi = d.BMI_Value.ToString(), msbp = s.msbp, msexecg = s.msexecg, msfbs = s.msfbs, mshbac = s.mshbac, msheight = d.Height.ToString(), mstotalc = s.mstotalc, msusugar = s.msusugar, msvision = s.msvision, msweight = d.Weight.ToString(), msyear = s.msyear, msid = s.msid, dentalst = d.MedicalStatus };
                sikcnt = sicd.Count();

                if (String.IsNullOrEmpty(pid))
                {
                    iserror = "1";
                }


                else if (sikcnt < 1)
                {


                    iserror = "4";



                }
                else
                {










                    var result = new { serv = nlist.ToList(), serv1 = sicd, imgd = imageDataURL, err = iserror };
                    // return Json(result, JsonRequestBehavior.AllowGet);

                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;

                    return jsonResult;
                }
                var result2 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = iserror };
                return Json(result2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result3 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = "2" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPatientmedical(string st, string id, string relet)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string opdid = (String)Session["userlocid1"];
                if (opdid.Contains("cl"))
                {
                }
                string pid = "";
                string iserror = "";
                string medcat = "";
                string sexc = "";
                string relationp = "";
                int svt = 0;
                string svty = "";
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                 st = st.Trim(MyChar);
                int stp = Convert.ToInt32(st);
                char[] MyChar1 = { '/', '"', ' ' };
                string imageDataURL = "";
                string locid = (String)Session["userloc"];
                int NewString1 = Convert.ToInt32(relet.Trim(MyChar1));
                List<getnursedata> nlist = new List<getnursedata>();
                DateTime dd = DateTime.Now.Date;
                DateTime SysDate = DateTime.Now;
                int month = SysDate.Month;
                int year = SysDate.Year;

                if (month > 6)
                {
                    year = year + 1;
                }

                var PersonResultList2 = from c in db.MedicalStatus
                                        where c.ServiceNo == NewString && c.ExamYear == year
                                        select new { c.BMI_Value, c.Height,c.Weight,c.MedicalStatus };

                var ser = from s in db.Patients.Where(p => p.ServiceNo == NewString).Where(p => p.RelationshipType == NewString1&&p.Service_Type== stp)

                          join w in db.PersonalDetails on s.ServiceNo equals w.ServiceNo
                          join x in db.MedicalCategories on w.SNo equals x.SNo
                          select new getnursedata { PID = s.PID, RNK_NAME = "", Initials = s.Initials, Surname = s.Surname, SxDetail = w.Gender, Category = x.MedicalCategory1, Relationship = s.RelationshipType.ToString(), sv = s.Service_Type, dob = s.DateOfBirth };
                foreach (var item in ser)
                {

                    pid = item.PID;
                    medcat = item.Category;
                    sexc = item.SxDetail;
                    svt = item.sv;
                    relationp = item.Relationship;

                    
                    if (item.Relationship == "1")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.MedicalCategories on s.SNo equals b.SNo
                                                into df
                                                from b in df.DefaultIfEmpty()
                                              
                                                where s.ServiceNo == NewString && s.ServiceType==stp

                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = s.Initials, Surname = s.Surname, SxDetail = s.Gender, Category = b.MedicalCategory1, Relationship = relationp, sv = svt, svt = s.ServiceStatus, dob = s.DateOfBirth };
                        foreach (var itemx in PersonResultList1)
                        {
                            svty = itemx.svt;
                            break;
                        }


                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }






                    }
                    if (item.Relationship.ToLower() == "2")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.SpouseDetails on s.SNo equals b.SNo
                                                where s.ServiceNo == NewString
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.SpouseName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "3")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo
                                                where s.ServiceNo == NewString && b.Relationship == "Father"
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ParentName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "4")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo

                                                where s.ServiceNo == NewString && b.Relationship == "Mother"
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ParentName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };
                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "5")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.Children on s.SNo equals b.SNo

                                                where s.ServiceNo == NewString && b.DOB == item.dob
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ChildName, SxDetail = b.Gender, Category = "", Relationship = relationp, sv = svt, dob = b.DOB };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());

                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }


                    }
                    if (nlist.Count() < 1)
                    {

                        nlist.Add(item);
                    }


                }

                if (locid == "CBO")
                {
                    db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DataTable oDataSet = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "SELECT count(*) tt  FROM [MMS].[dbo].[SickReport] where (LocationID='ahq' or LocationID='cbo') and svcid='"+pid+"' "+
" and convert(date, regdate)= convert(varchar, '" + DateTime.Now.Date.ToString() + "', 111)";

                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet);
                    oSqlConnection.Close();
                   
                     sikcnt =Convert.ToInt32( oDataSet.Rows[0]["tt"]);
                }
                else
                {
                    db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DataTable oDataSet = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "SELECT count(*) tt  FROM [MMS].[dbo].[SickReport] where (LocationID='"+locid+"' ) and svcid='" + pid + "' " +
" and convert(date, regdate)= convert(varchar, '"+DateTime.Now.Date.ToString()+"', 111)";

                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet);
                    oSqlConnection.Close();

                    sikcnt = Convert.ToInt32(oDataSet.Rows[0]["tt"]);
                }
                if (String.IsNullOrEmpty(pid))
                {
                    iserror = "1";
                }


                else if (svty == "Serving" && relationp == "1" && sikcnt < 1)
                {


                    iserror = "4";



                }
                else
                {
                    DateTime drt = new DateTime();
                    try
                    {
//                        db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
//                        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
//                        string sqlQuery;
//                        DataTable oDataSet = new DataTable();
//                        oSqlConnection = new SqlConnection(conStr);
//                        oSqlCommand = new SqlCommand();
//                        sqlQuery = "select TOP 1 * FROM [dbo].[Lab_Report] as a inner join [dbo].[Patient_Detail] as b on a.pdid=b.pdid "+
//" where b.pid = '"+pid+"'  ORDER BY[RequestedTime] DESC";

//                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
//                        oSqlCommand.Connection = oSqlConnection;
//                        oSqlCommand.CommandText = sqlQuery;
//                        oSqlConnection.Open();
//                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
//                        oSqlDataAdapter.Fill(oDataSet);
//                        oSqlConnection.Close();
//                        var empList = oDataSet.AsEnumerable()
//                .Select(dataRow => new getlabdata
//                {
//                    reqdate = dataRow.Field<DateTime>("RequestedTime"),
                   

//                }).ToList();

//                        foreach (var itm in empList)
//                        {
//                            drt = itm.reqdate;
//                        }

                    }
                    catch (Exception ex)
                    {
                       
                    }
                    drt = DateTime.Now;

                    //var lablist = from t in db.Lab_Report.Where(p => p.IssuedTime.Value.Day == drt.Day && p.IssuedTime.Value.Month == drt.Month && p.IssuedTime.Value.Year == drt.Year)
                    //              join b in db.Patient_Detail on t.PDID equals b.PDID into sd
                    //              from b in sd.DefaultIfEmpty().Where(p => p.PID == pid)
                    //              select new
                    //              { t.Lab_SubCategory.Lab_MainCategory.CategoryName, t.Lab_SubCategory.Lab_MainCategory.TubeCategory, t.PDID, t.TestSID, t.RequestedTime, t.Issued }; ;
                    //var labl = lablist.GroupBy(c => new { c.CategoryName, c.TubeCategory, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();





                    var result = new { serv = nlist.ToList(), serv1 = "", l1 = "", imgd = imageDataURL, err = iserror,meddata= PersonResultList2.ToList() };
                    // return Json(result, JsonRequestBehavior.AllowGet);

                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;

                    return jsonResult;
                }
                var result2 = new { serv = "", serv1 = "", l1 = "",imgd = string.Format("data:image/png;base64,{0}", ""), err = iserror };
                return Json(result2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result3 = new { serv = "", serv1 = "", l1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = "2" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetPatientslab(string stp, string id, string relet)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string opdid = (String)Session["userlocid1"];
                if (opdid.Contains("cl"))
                {
                }
                string blackno = "";
                string pid = "";
                string pid1 = "";
                DateTime? dtb = new DateTime();
                string iserror = "";
                string medcat = "";
                string sexc = "";
                string relationp = "";
                int svt = 0;
                string svty = "";
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                char[] MyChar1 = { '/', '"', ' ' };
                string imageDataURL = "";
                string locid = (String)Session["userloc"];
                int NewString1 = Convert.ToInt32(relet.Trim(MyChar1));
                List<getnursedata> nlist = new List<getnursedata>();
                DateTime dd = DateTime.Now.Date;
                stp = stp.Trim(MyChar);

                //////////////////////////////////////////////////////////
                DataTable oDataSetix = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                //  sqlQuery = "SELECT pid,DateOfBirth  FROM [MMS].[dbo].[Patient] where ServiceNo='" + NewString + "' and RelationshipType='" + NewString1 + "' and Service_Type='"+stp+"' ";

                sqlQuery = "SELECT * FROM [MMS].[dbo].[Vw_BlackListManagement] where [Service No]='" + NewString + "'  ";

                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(oDataSetix);
                oSqlConnection.Close();
                var opd2ix = oDataSetix.AsEnumerable()
        .Select(dataRow => new getnursedata
        {
            PID = dataRow.Field<string>("Service No"),


        }).ToList();
                foreach (var i in opd2ix)
                {
                    blackno = i.PID;
                }
                /////////////////////////////////////////////////

               
                //////////////////////////////////////////////////////////
                DataTable oDataSet2x = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();

                sqlQuery = "SELECT pid,DateOfBirth  FROM [MMS].[dbo].[Patient] where ServiceNo='" + NewString + "' and RelationshipType='" + NewString1 + "' and Service_Type='" + stp + "' ";


                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(oDataSet2x);
                oSqlConnection.Close();
                var opd2x = oDataSet2x.AsEnumerable()
        .Select(dataRow => new getnursedata
        {
            PID = dataRow.Field<string>("pid"),
            dob = dataRow.Field<DateTime?>("DateOfBirth"),

        }).ToList();
                foreach (var i in opd2x)
                {
                    pid1 = i.PID;
                }
                DataTable oDataSet3x = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                if (NewString1 == 1)
                {
                    sqlQuery = "SELECT Surname as sname ,DateOfBirth as dob ,Gender as sex  FROM [MMS].[dbo].[PersonalDetails] where ServiceNo='" + NewString + "'and ServiceType='" + stp + "' ";
                }
                if (NewString1 == 2)
                {
                    sqlQuery = "SELECT a.SpouseName as sname ,b.DateOfBirth as dob,b.Gender as sex  FROM [MMS].[dbo].[SpouseDetails] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where  b.ServiceNo='" + NewString + "'and b.ServiceType='" + stp + "' ";
                }
                if (NewString1 == 3)
                {
                    sqlQuery = "SELECT a.ParentName as sname ,b.DateOfBirth as dob,b.Gender as sex  FROM [dbo].[parents] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where b.ServiceNo='" + NewString + "' and b.ServiceType='" + stp + "' and a.Relationship='Father'";
                }
                if (NewString1 == 4)
                {
                    sqlQuery = "SELECT a.ParentName as sname ,b.DateOfBirth as dob,b.Gender as sex FROM [dbo].[parents] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where b.ServiceNo='" + NewString + "' and b.ServiceType='" + stp + "' and a.Relationship='Mother'";
                }
                if (NewString1 == 5)
                {
                    sqlQuery = "SELECT a.ChildName as sname,a.DOB as dob,a.Gender as sex  FROM [dbo].[Children] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where b.ServiceNo='" + NewString + "' and b.ServiceType='" + stp + "' ";
                }
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(oDataSet3x);
                oSqlConnection.Close();
                var opd3x = oDataSet3x.AsEnumerable()
        .Select(dataRow => new getnursedata
        {

            Surname = dataRow.Field<string>("sname"),
            dob = dataRow.Field<DateTime?>("dob"),
            SxDetail = dataRow.Field<string>("sex"),
        }).ToList();
                int cvb = 1;
                foreach (var i in opd3x)
                {
                    dtb = i.dob;
                    int df = 0;
                    foreach (var iw in opd2x)
                    {
                        if (dtb == iw.dob)
                        {
                            pid1 = iw.PID;
                            df = 1;
                        }
                    }
                    if (NewString1 == 5 && df == 0)
                    {
                        try
                        {
                            IndexGeneration indi = new IndexGeneration();
                            Patient patient = new Patient();
                            //iserror = "1";
                            if (stp == "1")
                            {
                                patient.PID = "O" + indi.CreatePID(NewString1, NewString);
                            }
                            else
                            {
                                patient.PID = indi.CreatePID(NewString1, NewString);
                            }

                            patient.RelationshipType = NewString1;
                            patient.Initials = "";
                            patient.Surname = i.Surname;
                            patient.DateOfBirth = i.dob;
                            patient.RANK = 1;
                            patient.ServiceNo = NewString;
                            patient.Service_Type = Convert.ToInt32(stp);
                            patient.LocationID = locid;
                            patient.CreatedDate = DateTime.Now.Date;
                            patient.Status = 1;
                            patient.ChildNo = cvb;
                            if (i.SxDetail.Equals("F"))
                            {
                                patient.Sex = 2;
                            }
                            else
                            {
                                patient.Sex = 1;
                            }

                            db.Patients.Add(patient);
                            db.SaveChanges();
                            df = 0;
                        }
                        catch (Exception ex) { }
                    }
                    if (NewString1 != 5 && String.IsNullOrEmpty(pid1) && opd3x.Count() > 0)
                    {
                        try
                        {
                            IndexGeneration indi = new IndexGeneration();
                            Patient patient = new Patient();
                            //iserror = "1";
                            if (stp == "1")
                            {
                                patient.PID = "O" + indi.CreatePID(NewString1, NewString);
                            }
                            else
                            {
                                patient.PID = indi.CreatePID(NewString1, NewString);
                            }

                            patient.RelationshipType = NewString1;
                            patient.Initials = "";
                            patient.Surname = i.Surname;
                            patient.DateOfBirth = i.dob;
                            patient.RANK = 1;
                            patient.ServiceNo = NewString;
                            patient.Service_Type = Convert.ToInt32(stp);
                            patient.LocationID = locid;
                            patient.CreatedDate = DateTime.Now.Date;
                            patient.Status = 1;
                            patient.ChildNo = cvb;
                            if (i.SxDetail.Equals("F"))
                            {
                                patient.Sex = 2;
                            }
                            else
                            {
                                patient.Sex = 1;
                            }

                            db.Patients.Add(patient);
                            db.SaveChanges();
                            df = 0;
                        }
                        catch (Exception ex) { }
                    }
                    cvb++;

                }
                ////////////////////////////
                /////////////////////////////////////////////////////////






                DataTable oDataSet1 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                if (stp == "1" && NewString1 == 1)
                {
                    sqlQuery = "SELECT  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1   " +
                    "  and b.Surname != '0' " +
                    " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),     " +
                    "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
                    "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
                    "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) sname  ,  " +
                    "  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1      then b.DateOfBirth end), " +
                    "   max(case when c.RelationshipType = 2 then c.DateOfBirth  end), " +
                    "   max(case when c.RelationshipType = 5    then f.DOB  end)), ''), max(c.DateOfBirth))      dob, " +
                    "	max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
                    "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
                    "	  , max(c.pid)  pidp,  max(h.Relationship)  relasiondet ,  max(k.MedicalCategory) mc, max(c.Service_Type) sv, " +
                    "	max (b.Gender) sx ,max(b.ServiceStatus) serv" +
                    "      FROM[MMS].[dbo].[Patient] as c " +
                    "   left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and b.ServiceType=c.Service_Type " +
                    "    left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
                    "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                    "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                    "    left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
                     "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and b.ServiceType='" + stp + "' group by c.PID ";

                }
                if (stp == "2" && NewString1 == 1)
                {
                    sqlQuery = "SELECT  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1   " +
                    "  and b.Surname != '0' " +
                    " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),     " +
                    "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
                    "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
                    "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) sname  ,  " +
                    "  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1      then b.DateOfBirth end), " +
                    "   max(case when c.RelationshipType = 2 then c.DateOfBirth  end), " +
                    "   max(case when c.RelationshipType = 5    then f.DOB  end)), ''), max(c.DateOfBirth))      dob, " +
                    "	max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
                    "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
                    "	  , max(c.pid)  pidp,  max(h.Relationship)  relasiondet ,  max(k.MedicalCategory) mc, max(c.Service_Type) sv, " +
                    "	max (b.Gender) sx ,max(b.ServiceStatus) serv" +
                    "      FROM[MMS].[dbo].[Patient] as c " +
                    "   left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
                    "    left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
                    "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                    "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                    "    left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
                     "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and b.ServiceType='" + stp + "' group by c.PID ";

                }

                if (stp != "1" && stp != "2")
                {
                    sqlQuery = "SELECT c.Surname sname,c.DateOfBirth dob,r.RNK_NAME rnkname ,c.ServiceNo sno,c.Initials inililes,c.RelationshipType relasiont,c.PID pidp " +
" ,h.Relationship relasiondet, k.Category mc, c.Service_Type sv, d.SxDetail sx, h.Relationship serv " +
" from[dbo].[Patient] as c " +
" left join [dbo].[ranks] as r on r.RANK=c.RANK left join [dbo].[MBP_Category] as k on k.MedCatID=c.MedCatID  left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType left join [dbo].[Sex_Type] as d on c.Sex=d.SxID " +
                     "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and c.Service_Type='" + stp + "'  ";

                }
                if ((stp == "1" || stp == "2") && NewString1 != 1)
                {
                    sqlQuery = "SELECT  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1   " +
                    "  and b.Surname != '0' " +
                    " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),     " +
                    "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB and c.childno=f.Child_No then f.ChildName  end), " +
                    "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
                    "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) sname  ,  " +
                    "  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1      then b.DateOfBirth end), " +
                    "   max(case when c.RelationshipType = 2 then c.DateOfBirth  end), " +
                    "   max(case when c.RelationshipType = 5    then f.DOB  end)), ''), max(c.DateOfBirth))      dob, " +
                    "	max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
                    "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
                    "	  , max(c.pid)  pidp,  max(h.Relationship)  relasiondet ,  max(k.MedicalCategory) mc, max(c.Service_Type) sv, " +
                    "	 max(case when c.Sex = 1 then 'MALE' ELSE 'FEMALE' end) sx ,max(b.ServiceStatus) serv" +
                    "      FROM[MMS].[dbo].[Patient] as c " +
                    "   left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type= b.ServiceType  " +
                    "    left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
                    "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                    "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                    "    left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
                     "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and b.ServiceType='" + stp + "' group by c.PID ";

                }

                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(oDataSet1);
                oSqlConnection.Close();
                var opd = oDataSet1.AsEnumerable()
        .Select(dataRow => new getnursedata
        {
            PID = dataRow.Field<string>("pidp"),
            RNK_NAME = dataRow.Field<string>("rnkname"),
            Initials = dataRow.Field<string>("inililes"),
            Surname = dataRow.Field<string>("sname"),
            SxDetail = dataRow.Field<string>("sx"),
            Category = dataRow.Field<string>("mc"),
            Relationship = dataRow.Field<int>("relasiont").ToString(),
            sv = dataRow.Field<int>("sv"),
            svt = dataRow.Field<string>("serv"),
            dob = dataRow.Field<DateTime?>("dob"),
            Service_Type = dataRow.Field<int>("relasiont").ToString()

        }).ToList();

                foreach (var iyt in opd)
                {
                    svty = iyt.svt;
                    relationp = iyt.Service_Type;
                    pid = iyt.PID;
                }
                //if (locid == "CBO")
                //{
                //var sicd = from s in db.SickReports.Where(p => p.LocationID == locid|| p.LocationID=="AHQ").Where(p => p.regdate.Value.Day == dd.Day && p.regdate.Value.Month == dd.Month && p.regdate.Value.Year == dd.Year)
                //           join b in db.Patients on s.svcid equals b.PID
                //        into sc
                //           from b in sc.DefaultIfEmpty()
                //           join c in db.Sick_Category on s.PDID equals c.PDID
                //       into sd
                //           from c in sd.DefaultIfEmpty()
                //           join d in db.PersonalDetails on b.ServiceNo equals d.ServiceNo
                //      into se
                //           from d in se.DefaultIfEmpty().Where(p => p.ServiceNo == NewString)
                //           orderby s.regdate descending
                //           select new getsickdata { svcid = b.ServiceNo, PDID = s.PDID, isliveout = s.isliveout, fname = d.Initials, lname = d.Surname, rank = d.Rank, age = s.age, service = s.service, isduty = s.isduty, islow = s.islow, cat = c.Sick_CategoryType.Category_Type, catdays = c.CatPeriod, regdate = s.regdate };
                DataTable oDataSet2 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT svcid  FROM [MMS].[dbo].[SickReport] where svcid='" + pid + "' and convert(date,regdate)=convert(varchar,'" + DateTime.Now.Date.ToString() + "',111) ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(oDataSet2);
                oSqlConnection.Close();
                var opd2 = oDataSet2.AsEnumerable()
        .Select(dataRow => new getnursedata
        {
            PID = dataRow.Field<string>("svcid"),


        }).ToList();
                sikcnt = opd2.Count();
                //}
                //else
                //{
                //    //var sicd = from s in db.SickReports.Where(p => p.LocationID == locid).Where(p => p.regdate.Value.Day == dd.Day && p.regdate.Value.Month == dd.Month && p.regdate.Value.Year == dd.Year)
                //    //           join b in db.Patients on s.svcid equals b.PID
                //    //        into sc
                //    //           from b in sc.DefaultIfEmpty()
                //    //           join c in db.Sick_Category on s.PDID equals c.PDID
                //    //       into sd
                //    //           from c in sd.DefaultIfEmpty()
                //    //           join d in db.PersonalDetails on b.ServiceNo equals d.ServiceNo
                //    //      into se
                //    //           from d in se.DefaultIfEmpty().Where(p => p.ServiceNo == NewString)
                //    //           orderby s.regdate descending
                //    //           select new getsickdata { svcid = b.ServiceNo, PDID = s.PDID, isliveout = s.isliveout, fname = d.Initials, lname = d.Surname, rank = d.Rank, age = s.age, service = s.service, isduty = s.isduty, islow = s.islow, cat = c.Sick_CategoryType.Category_Type, catdays = c.CatPeriod, regdate = s.regdate };
                //    sikcnt = 0;
                //}


                //if (opd3x.Count < 1 && !NewString.ToLower().StartsWith("ex") && !NewString.ToLower().StartsWith("c") && NewString.Length < 10)
                //{
                //    iserror = "3";
                //}
                if (opd3x.Count < 1 && (stp == "1" || stp == "2"))
                {
                    iserror = "3";
                }

                else if (String.IsNullOrEmpty(pid))
                {
                    iserror = "1";
                }
                

                //else if (svty == "Serving" && relationp == "1" && sikcnt < 1)
                //{


                //    iserror = "4";



                //}
                else
                {
                    if (!String.IsNullOrEmpty(blackno))

                    {
                        iserror = "5";
                    }

                    var a2 = from t in db.Hypersensivities.Where(w => w.PID == pid) select new { t.HypMainCategory.HypersenceMainCategory, t.HypMainCategory.HypersenceMainCatID, t.HypersenseDetail };







                    var result = new { serv = opd.ToList(), serv1 = a2.ToList(), imgd = imageDataURL, err = iserror };
                    // return Json(result, JsonRequestBehavior.AllowGet);

                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;

                    return jsonResult;
                }
                var result2 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = iserror };
                return Json(result2, JsonRequestBehavior.AllowGet);

            
            }
            catch (Exception ex)
            {
                var result3 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = "2" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetPatients(string stp, string id, string relet)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string opdid = (String)Session["userlocid1"];
                if (!String.IsNullOrEmpty(opdid))
                {

                    string ServiceStatus = "";
                    string pid = "";
                    string blackno = "";
                    string pid1 = "";
                    DateTime? dtb = new DateTime();
                    string iserror = "";
                    string medcat = "";
                    string sexc = "";
                    string relationp = "";
                    int svt = 0;
                    string svty = "";
                    char[] MyChar = { '/', '"', ' ' };
                    string NewString = id.Trim(MyChar);
                    char[] MyChar1 = { '/', '"', ' ' };
                    string imageDataURL = "";
                    string locid = (String)Session["userloc"];
                    int NewString1 = Convert.ToInt32(relet.Trim(MyChar1));
                    List<getnursedata> nlist = new List<getnursedata>();
                    DateTime dd = DateTime.Now.Date;
                    stp = stp.Trim(MyChar);

                    //////////////////////////////////////////////////////////
                    DataTable oDataSetix = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    //  sqlQuery = "SELECT pid,DateOfBirth  FROM [MMS].[dbo].[Patient] where ServiceNo='" + NewString + "' and RelationshipType='" + NewString1 + "' and Service_Type='"+stp+"' ";

                    sqlQuery = "SELECT * FROM [MMS].[dbo].[Vw_BlackListManagement] where [Service No]='" + NewString + "'  ";

                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(oDataSetix);
                    oSqlConnection.Close();
                    var opd2ix = oDataSetix.AsEnumerable()
            .Select(dataRow => new getnursedata
            {
                PID = dataRow.Field<string>("Service No"),


            }).ToList();
                    foreach (var i in opd2ix)
                    {
                        blackno = i.PID;
                    }
                    /////////////////////////////////////////////////




                    //////////////////////////////////////////////////////////
                    DataTable oDataSet2x = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    //  sqlQuery = "SELECT pid,DateOfBirth  FROM [MMS].[dbo].[Patient] where ServiceNo='" + NewString + "' and RelationshipType='" + NewString1 + "' and Service_Type='"+stp+"' ";

                    sqlQuery = "SELECT pid,DateOfBirth  FROM [MMS].[dbo].[Patient] where ServiceNo='" + NewString + "' and RelationshipType='" + NewString1 + "' and Service_Type='" + stp + "' ";

                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(oDataSet2x);
                    oSqlConnection.Close();
                    var opd2x = oDataSet2x.AsEnumerable()
            .Select(dataRow => new getnursedata
            {
                PID = dataRow.Field<string>("pid"),
                dob = dataRow.Field<DateTime?>("DateOfBirth"),

            }).ToList();
                    foreach (var i in opd2x)
                    {
                        pid1 = i.PID;
                    }
                    /////////////////////////////////////////////////
                    DataTable oDataSet2xy = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    //  sqlQuery = "SELECT pid,DateOfBirth  FROM [MMS].[dbo].[Patient] where ServiceNo='" + NewString + "' and RelationshipType='" + NewString1 + "' and Service_Type='"+stp+"' ";

                    sqlQuery = "SELECT ServiceStatus  FROM [MMS].[dbo].[PersonalDetails] where ServiceNo='" + NewString + "' and ServiceType =CASE when " + stp + "=4 then 1 when " + stp + "= 5 then 2 else '" + stp + "' end ";

                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(oDataSet2xy);
                    oSqlConnection.Close();
                    var opd2xy = oDataSet2xy.AsEnumerable()
            .Select(dataRow => new getnursedata
            {
                PID = dataRow.Field<string>("ServiceStatus"),


            }).ToList();
                    foreach (var i in opd2xy)
                    {
                        ServiceStatus = i.PID;
                    }

                    ////////////////////////////////
                    DataTable oDataSet3x = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    if (NewString1 == 1)
                    {
                        sqlQuery = "SELECT Surname as sname ,DateOfBirth as dob ,Gender as sex  FROM [MMS].[dbo].[PersonalDetails] where ServiceNo='" + NewString + "'and ServiceType='" + stp + "' ";
                    }
                    if (NewString1 == 2)
                    {
                        sqlQuery = "SELECT a.SpouseName as sname ,b.DateOfBirth as dob,b.Gender as sex  FROM [MMS].[dbo].[SpouseDetails] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where  b.ServiceNo='" + NewString + "'and b.ServiceType='" + stp + "' ";
                    }
                    if (NewString1 == 3)
                    {
                        sqlQuery = "SELECT a.ParentName as sname ,b.DateOfBirth as dob,b.Gender as sex  FROM [dbo].[parents] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where b.ServiceNo='" + NewString + "' and b.ServiceType='" + stp + "' and a.Relationship='Father'";
                    }
                    if (NewString1 == 4)
                    {
                        sqlQuery = "SELECT a.ParentName as sname ,b.DateOfBirth as dob,b.Gender as sex FROM [dbo].[parents] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where b.ServiceNo='" + NewString + "' and b.ServiceType='" + stp + "' and a.Relationship='Mother'";
                    }
                    if (NewString1 == 5)
                    {
                        sqlQuery = "SELECT a.ChildName as sname,a.DOB as dob,a.Gender as sex  FROM [dbo].[Children] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where b.ServiceNo='" + NewString + "' and b.ServiceType='" + stp + "' ";
                    }
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(oDataSet3x);
                    oSqlConnection.Close();
                    var opd3x = oDataSet3x.AsEnumerable()
            .Select(dataRow => new getnursedata
            {

                Surname = dataRow.Field<string>("sname"),
                dob = dataRow.Field<DateTime?>("dob"),
                SxDetail = dataRow.Field<string>("sex"),
            }).ToList();
                    int cvb = 1;
                    foreach (var i in opd3x)
                    {
                        dtb = i.dob;
                        int df = 0;
                        foreach (var iw in opd2x)
                        {
                            if (dtb == iw.dob)
                            {
                                df = 1;
                            }
                        }
                        if (NewString1 == 5 && df == 0)
                        {
                            IndexGeneration indi = new IndexGeneration();
                            Patient patient = new Patient();
                            //iserror = "1";
                            if (stp == "1")
                            {
                                patient.PID = "O" + indi.CreatePID(NewString1, NewString);
                            }
                            else
                            {
                                patient.PID = indi.CreatePID(NewString1, NewString);
                            }

                            patient.RelationshipType = NewString1;
                            patient.Initials = "";
                            patient.Surname = i.Surname;
                            patient.DateOfBirth = i.dob;
                            patient.RANK = 1;
                            patient.ServiceNo = NewString;
                            patient.Service_Type = Convert.ToInt32(stp);
                            patient.LocationID = locid;
                            patient.CreatedDate = DateTime.Now.Date;
                            patient.Status = 1;
                            patient.ChildNo = cvb;
                            if (i.SxDetail.Equals("F"))
                            {
                                patient.Sex = 2;
                            }
                            else
                            {
                                patient.Sex = 1;
                            }

                            db.Patients.Add(patient);
                            db.SaveChanges();
                            df = 0;
                        }
                        if (NewString1 != 5 && String.IsNullOrEmpty(pid1) && opd3x.Count() > 0)
                        {
                            IndexGeneration indi = new IndexGeneration();
                            Patient patient = new Patient();
                            //iserror = "1";
                            if (stp == "1")
                            {
                                patient.PID = "O" + indi.CreatePID(NewString1, NewString);
                            }
                            else
                            {
                                patient.PID = indi.CreatePID(NewString1, NewString);
                            }

                            patient.RelationshipType = NewString1;
                            patient.Initials = "";
                            patient.Surname = i.Surname;
                            patient.DateOfBirth = i.dob;
                            patient.RANK = 1;
                            patient.ServiceNo = NewString;
                            patient.Service_Type = Convert.ToInt32(stp);
                            patient.LocationID = locid;
                            patient.CreatedDate = DateTime.Now.Date;
                            patient.Status = 1;
                            patient.ChildNo = cvb;
                            if (i.SxDetail.Equals("F"))
                            {
                                patient.Sex = 2;
                            }
                            else
                            {
                                patient.Sex = 1;
                            }

                            db.Patients.Add(patient);
                            db.SaveChanges();
                            df = 0;
                        }
                        cvb++;

                    }
                    ////////////////////////////
                    /////////////////////////////////////////////////////////






                    DataTable oDataSet1 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    if (stp == "1" && NewString1 == 1)
                    {
                        sqlQuery = "SELECT  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1   " +
                        "  and b.Surname != '0' " +
                        " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),     " +
                        "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
                        "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
                        "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) sname  ,  " +
                        "  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1      then b.DateOfBirth end), " +
                        "   max(case when c.RelationshipType = 2 then c.DateOfBirth  end), " +
                        "   max(case when c.RelationshipType = 5    then f.DOB  end)), ''), max(c.DateOfBirth))      dob, " +
                        "	max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
                        "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
                        "	  , max(c.pid)  pidp,  max(h.Relationship)  relasiondet ,  max(k.MedicalCategory) mc, max(c.Service_Type) sv, " +
                        "	max (b.Gender) sx ,max(b.ServiceStatus) serv" +
                        "      FROM[MMS].[dbo].[Patient] as c " +
                        "   left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and b.ServiceType=c.Service_Type " +
                        "    left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
                        "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                        "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                        "    left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
                         "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and b.ServiceType='" + stp + "' group by c.PID ";

                    }
                    if (stp == "2" && NewString1 == 1)
                    {
                        sqlQuery = "SELECT  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1   " +
                        "  and b.Surname != '0' " +
                        " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),     " +
                        "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
                        "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
                        "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) sname  ,  " +
                        "  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1      then b.DateOfBirth end), " +
                        "   max(case when c.RelationshipType = 2 then c.DateOfBirth  end), " +
                        "   max(case when c.RelationshipType = 5    then f.DOB  end)), ''), max(c.DateOfBirth))      dob, " +
                        "	max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
                        "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
                        "	  , max(c.pid)  pidp,  max(h.Relationship)  relasiondet ,  max(k.MedicalCategory) mc, max(c.Service_Type) sv, " +
                        "	max (b.Gender) sx ,max(b.ServiceStatus) serv" +
                        "      FROM[MMS].[dbo].[Patient] as c " +
                        "   left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
                        "    left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
                        "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                        "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                        "    left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
                         "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and b.ServiceType='" + stp + "' group by c.PID ";

                    }

                    if (stp != "1" && stp != "2")
                    {
                        //                   sqlQuery = "SELECT c.Surname sname,c.DateOfBirth dob,r.RNK_NAME rnkname ,c.ServiceNo sno,c.Initials inililes,c.RelationshipType relasiont,c.PID pidp " +
                        //" ,h.Relationship relasiondet, k.Category mc, c.Service_Type sv, d.SxDetail sx, h.Relationship serv " +
                        //" from[dbo].[Patient] as c " +
                        //" left join [dbo].[ranks] as r on r.RANK=c.RANK left join [dbo].[MBP_Category] as k on k.MedCatID=c.MedCatID  left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType left join [dbo].[Sex_Type] as d on c.Sex=d.SxID " +
                        //                     "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and c.Service_Type='" + stp + "'  ";
                        sqlQuery = "SELECT c.Surname sname,c.DateOfBirth dob,r.RNK_NAME rnkname ,c.ServiceNo sno,c.Initials inililes,c.RelationshipType relasiont,c.PID pidp " +
    " ,h.Relationship relasiondet, k.Category mc, c.Service_Type sv, d.SxDetail sx, h.Relationship serv " +
    " from[dbo].[Patient] as c " +
    " left join [dbo].[ranks] as r on r.RANK=c.RANK left join [dbo].[MBP_Category] as k on k.MedCatID=c.MedCatID  left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType left join [dbo].[Sex_Type] as d on c.Sex=d.SxID " +
                         "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and c.Service_Type='" + stp + "'  ";

                    }
                    if ((stp == "1" || stp == "2") && NewString1 != 1)
                    {
                        sqlQuery = "SELECT  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1   " +
                        "  and b.Surname != '0' " +
                        " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),     " +
                        "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB and c.childno=f.Child_No then f.ChildName  end), " +
                        "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
                        "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) sname  ,  " +
                        "  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1      then b.DateOfBirth end), " +
                        "   max(case when c.RelationshipType = 2 then c.DateOfBirth  end), " +
                        "   max(case when c.RelationshipType = 5    then f.DOB  end)), ''), max(c.DateOfBirth))      dob, " +
                        "	max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
                        "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
                        "	  , max(c.pid)  pidp,  max(h.Relationship)  relasiondet ,  max(k.MedicalCategory) mc, max(c.Service_Type) sv, " +
                        "	 max(case when c.Sex = 1 then 'MALE' ELSE 'FEMALE' end) sx ,max(b.ServiceStatus) serv" +
                        "      FROM[MMS].[dbo].[Patient] as c " +
                        "   left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type= b.ServiceType  " +
                        "    left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
                        "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                        "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                        "    left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
                         "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and b.ServiceType='" + stp + "' group by c.PID ";

                    }

                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(oDataSet1);
                    oSqlConnection.Close();
                    var opd = oDataSet1.AsEnumerable()
            .Select(dataRow => new getnursedata
            {
                PID = dataRow.Field<string>("pidp"),
                RNK_NAME = dataRow.Field<string>("rnkname"),
                Initials = dataRow.Field<string>("inililes"),
                Surname = dataRow.Field<string>("sname"),
                SxDetail = dataRow.Field<string>("sx"),
                Category = dataRow.Field<string>("mc"),
                Relationship = dataRow.Field<int>("relasiont").ToString(),
                sv = dataRow.Field<int>("sv"),
                svt = dataRow.Field<string>("serv"),
                dob = dataRow.Field<DateTime?>("dob"),
                Service_Type = dataRow.Field<int>("relasiont").ToString(),
                retiredStatus = ServiceStatus,

            }).ToList();

                    foreach (var iyt in opd)
                    {
                        svty = iyt.svt;
                        relationp = iyt.Service_Type;
                        pid = iyt.PID;
                    }
                    //if (locid == "CBO")
                    //{
                    //var sicd = from s in db.SickReports.Where(p => p.LocationID == locid|| p.LocationID=="AHQ").Where(p => p.regdate.Value.Day == dd.Day && p.regdate.Value.Month == dd.Month && p.regdate.Value.Year == dd.Year)
                    //           join b in db.Patients on s.svcid equals b.PID
                    //        into sc
                    //           from b in sc.DefaultIfEmpty()
                    //           join c in db.Sick_Category on s.PDID equals c.PDID
                    //       into sd
                    //           from c in sd.DefaultIfEmpty()
                    //           join d in db.PersonalDetails on b.ServiceNo equals d.ServiceNo
                    //      into se
                    //           from d in se.DefaultIfEmpty().Where(p => p.ServiceNo == NewString)
                    //           orderby s.regdate descending
                    //           select new getsickdata { svcid = b.ServiceNo, PDID = s.PDID, isliveout = s.isliveout, fname = d.Initials, lname = d.Surname, rank = d.Rank, age = s.age, service = s.service, isduty = s.isduty, islow = s.islow, cat = c.Sick_CategoryType.Category_Type, catdays = c.CatPeriod, regdate = s.regdate };
                    DataTable oDataSet2 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "SELECT svcid  FROM [MMS].[dbo].[SickReport] where svcid='" + pid + "' and convert(date,regdate)=convert(varchar,'" + DateTime.Now.Date.ToString() + "',111) ";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(oDataSet2);
                    oSqlConnection.Close();
                    var opd2 = oDataSet2.AsEnumerable()
            .Select(dataRow => new getnursedata
            {
                PID = dataRow.Field<string>("svcid"),


            }).ToList();
                    sikcnt = opd2.Count();
                    //}
                    //else
                    //{
                    //    //var sicd = from s in db.SickReports.Where(p => p.LocationID == locid).Where(p => p.regdate.Value.Day == dd.Day && p.regdate.Value.Month == dd.Month && p.regdate.Value.Year == dd.Year)
                    //    //           join b in db.Patients on s.svcid equals b.PID
                    //    //        into sc
                    //    //           from b in sc.DefaultIfEmpty()
                    //    //           join c in db.Sick_Category on s.PDID equals c.PDID
                    //    //       into sd
                    //    //           from c in sd.DefaultIfEmpty()
                    //    //           join d in db.PersonalDetails on b.ServiceNo equals d.ServiceNo
                    //    //      into se
                    //    //           from d in se.DefaultIfEmpty().Where(p => p.ServiceNo == NewString)
                    //    //           orderby s.regdate descending
                    //    //           select new getsickdata { svcid = b.ServiceNo, PDID = s.PDID, isliveout = s.isliveout, fname = d.Initials, lname = d.Surname, rank = d.Rank, age = s.age, service = s.service, isduty = s.isduty, islow = s.islow, cat = c.Sick_CategoryType.Category_Type, catdays = c.CatPeriod, regdate = s.regdate };
                    //    sikcnt = 0;
                    //}
                    if (opd3x.Count < 1 && (stp == "1" || stp == "2"))
                    {
                        iserror = "3";
                    }

                    else if (String.IsNullOrEmpty(pid))
                    {
                        iserror = "1";
                    }


                    else if (svty == "Serving" && relationp == "1" && sikcnt < 1)
                    {


                        iserror = "4";



                    }
                    else if (ServiceStatus != "Serving" && (relationp != "1" && relationp != "2"))
                    {


                        iserror = "6";



                    }

                    else
                    {

                        if (!String.IsNullOrEmpty(blackno))
                        {
                            iserror = "5";
                        }
                        var a2 = from t in db.Hypersensivities.Where(w => w.PID == pid) select new { t.HypMainCategory.HypersenceMainCategory, t.HypMainCategory.HypersenceMainCatID, t.HypersenseDetail };







                        var result = new { serv = opd.ToList(), serv1 = a2.ToList(), imgd = imageDataURL, err = iserror };
                        // return Json(result, JsonRequestBehavior.AllowGet);

                        var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;

                        return jsonResult;
                    }
                    var result2 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = iserror };
                    return Json(result2, JsonRequestBehavior.AllowGet);

                }
                else {
                    var result3 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = "2" };
                    return Json(result3, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var result3 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = "2" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetPatientpurch(string stp, string id, string relet)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string opdid = (String)Session["userlocid1"];
                if (opdid.Contains("cl"))
                {
                }
                string pid = "";
                string pid1 = "";
                DateTime? dtb = new DateTime();
                string iserror = "";
                string medcat = "";
                string sexc = "";
                string relationp = "";
                int svt = 0;
                string svty = "";
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                char[] MyChar1 = { '/', '"', ' ' };
                string imageDataURL = "";
                string locid = (String)Session["userloc"];
                int NewString1 = Convert.ToInt32(relet.Trim(MyChar1));
                List<getnursedata> nlist = new List<getnursedata>();
                DateTime dd = DateTime.Now.Date;
                stp = stp.Trim(MyChar);

                //////////////////////////////////////////////////////////
                DataTable oDataSet2x = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT pid,DateOfBirth  FROM [MMS].[dbo].[Patient] where ServiceNo='" + NewString + "' and RelationshipType='" + NewString1 + "' and Service_Type='" + stp + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(oDataSet2x);
                oSqlConnection.Close();
                var opd2x = oDataSet2x.AsEnumerable()
        .Select(dataRow => new getnursedata
        {
            PID = dataRow.Field<string>("pid"),
            dob = dataRow.Field<DateTime?>("DateOfBirth"),

        }).ToList();
                foreach (var i in opd2x)
                {
                    pid1 = i.PID;
                }
                DataTable oDataSet3x = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                if (NewString1 == 1)
                {
                    sqlQuery = "SELECT Surname as sname ,DateOfBirth as dob ,Gender as sex  FROM [MMS].[dbo].[PersonalDetails] where ServiceNo='" + NewString + "'and ServiceType='" + stp + "' ";
                }
                if (NewString1 == 2)
                {
                    sqlQuery = "SELECT a.SpouseName as sname ,b.DateOfBirth as dob,b.Gender as sex  FROM [MMS].[dbo].[SpouseDetails] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where  b.ServiceNo='" + NewString + "'and b.ServiceType='" + stp + "' ";
                }
                if (NewString1 == 3)
                {
                    sqlQuery = "SELECT a.ParentName as sname ,b.DateOfBirth as dob,b.Gender as sex  FROM [dbo].[parents] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where b.ServiceNo='" + NewString + "' and b.ServiceType='" + stp + "' and a.Relationship='Father'";
                }
                if (NewString1 == 4)
                {
                    sqlQuery = "SELECT a.ParentName as sname ,b.DateOfBirth as dob,b.Gender as sex FROM [dbo].[parents] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where b.ServiceNo='" + NewString + "' and b.ServiceType='" + stp + "' and a.Relationship='Mother'";
                }
                if (NewString1 == 5)
                {
                    sqlQuery = "SELECT a.ChildName as sname,a.DOB as dob,a.Gender as sex  FROM [dbo].[Children] as a inner join [MMS].[dbo].[PersonalDetails] as b on a.SNo=b.SNo where b.ServiceNo='" + NewString + "' and b.ServiceType='" + stp + "' ";
                }
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(oDataSet3x);
                oSqlConnection.Close();
                var opd3x = oDataSet3x.AsEnumerable()
        .Select(dataRow => new getnursedata
        {

            Surname = dataRow.Field<string>("sname"),
            dob = dataRow.Field<DateTime?>("dob"),
            SxDetail = dataRow.Field<string>("sex"),
        }).ToList();
                int cvb = 1;
                foreach (var i in opd3x)
                {
                    dtb = i.dob;
                    int df = 0;
                    foreach (var iw in opd2x)
                    {
                        if (dtb == iw.dob)
                        {
                            df = 1;
                        }
                    }
                    if (NewString1 == 5 && df == 0)
                    {
                        IndexGeneration indi = new IndexGeneration();
                        Patient patient = new Patient();
                        //iserror = "1";
                        if (stp == "1")
                        {
                            patient.PID = "O" + indi.CreatePID(NewString1, NewString);
                        }
                        else
                        {
                            patient.PID = indi.CreatePID(NewString1, NewString);
                        }

                        patient.RelationshipType = NewString1;
                        patient.Initials = "";
                        patient.Surname = i.Surname;
                        patient.DateOfBirth = i.dob;
                        patient.RANK = 1;
                        patient.ServiceNo = NewString;
                        patient.Service_Type = Convert.ToInt32(stp);
                        patient.LocationID = locid;
                        patient.CreatedDate = DateTime.Now.Date;
                        patient.Status = 1;
                        patient.ChildNo = cvb;
                        if (i.SxDetail.Equals("F"))
                        {
                            patient.Sex = 2;
                        }
                        else
                        {
                            patient.Sex = 1;
                        }

                        db.Patients.Add(patient);
                        db.SaveChanges();
                        df = 0;
                    }
                    if (NewString1 != 5 && String.IsNullOrEmpty(pid1) && opd3x.Count() > 0)
                    {
                        IndexGeneration indi = new IndexGeneration();
                        Patient patient = new Patient();
                        //iserror = "1";
                        if (stp == "1")
                        {
                            patient.PID = "O" + indi.CreatePID(NewString1,  NewString);
                        }
                        else
                        {
                            patient.PID = indi.CreatePID(NewString1, NewString);
                        }

                        patient.RelationshipType = NewString1;
                        patient.Initials = "";
                        patient.Surname = i.Surname;
                        patient.DateOfBirth = i.dob;
                        patient.RANK = 1;
                        patient.ServiceNo = NewString;
                        patient.Service_Type = Convert.ToInt32(stp);
                        patient.LocationID = locid;
                        patient.CreatedDate = DateTime.Now.Date;
                        patient.Status = 1;
                        patient.ChildNo = cvb;
                        if (i.SxDetail.Equals("F"))
                        {
                            patient.Sex = 2;
                        }
                        else
                        {
                            patient.Sex = 1;
                        }

                        db.Patients.Add(patient);
                        db.SaveChanges();
                        df = 0;
                    }
                    cvb++;

                }
                ////////////////////////////
                /////////////////////////////////////////////////////////






                DataTable oDataSet1 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                if (stp == "1" && NewString1 == 1)
                {
                    sqlQuery = "SELECT  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1   " +
                    "  and b.Surname != '0' " +
                    " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),     " +
                    "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
                    "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
                    "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) sname  ,  " +
                    "  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1      then b.DateOfBirth end), " +
                    "   max(case when c.RelationshipType = 2 then c.DateOfBirth  end), " +
                    "   max(case when c.RelationshipType = 5    then f.DOB  end)), ''), max(c.DateOfBirth))      dob, " +
                    "	max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
                    "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
                    "	  , max(c.pid)  pidp,  max(h.Relationship)  relasiondet ,  max(k.MedicalCategory) mc, max(c.Service_Type) sv, " +
                    "	max (b.Gender) sx ,max(b.ServiceStatus) serv" +
                    "      FROM[MMS].[dbo].[Patient] as c " +
                    "   left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and b.ServiceType=c.Service_Type " +
                    "    left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
                    "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                    "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                    "    left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
                     "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and b.ServiceType='" + stp + "' group by c.PID ";

                }
                if (stp == "2" && NewString1 == 1)
                {
                    sqlQuery = "SELECT  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1   " +
                    "  and b.Surname != '0' " +
                    " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),     " +
                    "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
                    "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
                    "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) sname  ,  " +
                    "  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1      then b.DateOfBirth end), " +
                    "   max(case when c.RelationshipType = 2 then c.DateOfBirth  end), " +
                    "   max(case when c.RelationshipType = 5    then f.DOB  end)), ''), max(c.DateOfBirth))      dob, " +
                    "	max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
                    "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
                    "	  , max(c.pid)  pidp,  max(h.Relationship)  relasiondet ,  max(k.MedicalCategory) mc, max(c.Service_Type) sv, " +
                    "	max (b.Gender) sx ,max(b.ServiceStatus) serv" +
                    "      FROM[MMS].[dbo].[Patient] as c " +
                    "   left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
                    "    left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
                    "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                    "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                    "    left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
                     "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and b.ServiceType='" + stp + "' group by c.PID ";

                }

                if (stp == "3")
                {
                    sqlQuery = "SELECT c.Surname sname,c.DateOfBirth dob,r.RNK_NAME rnkname ,c.ServiceNo sno,c.Initials inililes,c.RelationshipType relasiont,c.PID pidp " +
" ,h.Relationship relasiondet, k.Category mc, c.Service_Type sv, d.SxDetail sx, h.Relationship serv " +
" from[dbo].[Patient] as c " +
" left join [dbo].[ranks] as r on r.RANK=c.RANK left join [dbo].[MBP_Category] as k on k.MedCatID=c.MedCatID  left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType left join [dbo].[Sex_Type] as d on c.Sex=d.SxID " +
                     "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and c.Service_Type='" + stp + "'  ";

                }
                if (stp != "3" && NewString1 != 1)
                {
                    sqlQuery = "SELECT  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1   " +
                    "  and b.Surname != '0' " +
                    " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),     " +
                    "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
                    "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
                    "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) sname  ,  " +
                    "  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1      then b.DateOfBirth end), " +
                    "   max(case when c.RelationshipType = 2 then c.DateOfBirth  end), " +
                    "   max(case when c.RelationshipType = 5    then f.DOB  end)), ''), max(c.DateOfBirth))      dob, " +
                    "	max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
                    "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
                    "	  , max(c.pid)  pidp,  max(h.Relationship)  relasiondet ,  max(k.MedicalCategory) mc, max(c.Service_Type) sv, " +
                    "	 max(case when c.Sex = 1 then 'MALE' ELSE 'FEMALE' end) sx ,max(b.ServiceStatus) serv" +
                    "      FROM[MMS].[dbo].[Patient] as c " +
                    "   left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type= b.ServiceType  " +
                    "    left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
                    "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                    "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                    "    left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
                     "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' and b.ServiceType='" + stp + "' group by c.PID ";

                }

                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(oDataSet1);
                oSqlConnection.Close();
                var opd = oDataSet1.AsEnumerable()
        .Select(dataRow => new getnursedata
        {
            PID = dataRow.Field<string>("pidp"),
            RNK_NAME = dataRow.Field<string>("rnkname"),
            Initials = dataRow.Field<string>("inililes"),
            Surname = dataRow.Field<string>("sname"),
            SxDetail = dataRow.Field<string>("sx"),
            Category = dataRow.Field<string>("mc"),
            Relationship = dataRow.Field<int>("relasiont").ToString(),
            sv = dataRow.Field<int>("sv"),
            svt = dataRow.Field<string>("serv"),
            dob = dataRow.Field<DateTime?>("dob"),
            Service_Type = dataRow.Field<int>("relasiont").ToString()

        }).ToList();

                foreach (var iyt in opd)
                {
                    svty = iyt.svt;
                    relationp = iyt.Service_Type;
                    pid = iyt.PID;
                }
                //if (locid == "CBO")
                //{
                //var sicd = from s in db.SickReports.Where(p => p.LocationID == locid|| p.LocationID=="AHQ").Where(p => p.regdate.Value.Day == dd.Day && p.regdate.Value.Month == dd.Month && p.regdate.Value.Year == dd.Year)
                //           join b in db.Patients on s.svcid equals b.PID
                //        into sc
                //           from b in sc.DefaultIfEmpty()
                //           join c in db.Sick_Category on s.PDID equals c.PDID
                //       into sd
                //           from c in sd.DefaultIfEmpty()
                //           join d in db.PersonalDetails on b.ServiceNo equals d.ServiceNo
                //      into se
                //           from d in se.DefaultIfEmpty().Where(p => p.ServiceNo == NewString)
                //           orderby s.regdate descending
                //           select new getsickdata { svcid = b.ServiceNo, PDID = s.PDID, isliveout = s.isliveout, fname = d.Initials, lname = d.Surname, rank = d.Rank, age = s.age, service = s.service, isduty = s.isduty, islow = s.islow, cat = c.Sick_CategoryType.Category_Type, catdays = c.CatPeriod, regdate = s.regdate };
                DataTable oDataSet2 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT svcid  FROM [MMS].[dbo].[SickReport] where svcid='" + pid + "' and convert(date,regdate)=convert(varchar,'" + DateTime.Now.Date.ToString() + "',111) ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(oDataSet2);
                oSqlConnection.Close();
                var opd2 = oDataSet2.AsEnumerable()
        .Select(dataRow => new getnursedata
        {
            PID = dataRow.Field<string>("svcid"),


        }).ToList();
                sikcnt = opd2.Count();
                //}
                //else
                //{
                //    //var sicd = from s in db.SickReports.Where(p => p.LocationID == locid).Where(p => p.regdate.Value.Day == dd.Day && p.regdate.Value.Month == dd.Month && p.regdate.Value.Year == dd.Year)
                //    //           join b in db.Patients on s.svcid equals b.PID
                //    //        into sc
                //    //           from b in sc.DefaultIfEmpty()
                //    //           join c in db.Sick_Category on s.PDID equals c.PDID
                //    //       into sd
                //    //           from c in sd.DefaultIfEmpty()
                //    //           join d in db.PersonalDetails on b.ServiceNo equals d.ServiceNo
                //    //      into se
                //    //           from d in se.DefaultIfEmpty().Where(p => p.ServiceNo == NewString)
                //    //           orderby s.regdate descending
                //    //           select new getsickdata { svcid = b.ServiceNo, PDID = s.PDID, isliveout = s.isliveout, fname = d.Initials, lname = d.Surname, rank = d.Rank, age = s.age, service = s.service, isduty = s.isduty, islow = s.islow, cat = c.Sick_CategoryType.Category_Type, catdays = c.CatPeriod, regdate = s.regdate };
                //    sikcnt = 0;
                //}
                if (String.IsNullOrEmpty(pid))
                {
                    iserror = "1";
                }


                //else if (svty == "Serving" && relationp == "1" && sikcnt < 1)
                //{


                //    iserror = "4";



                //}
                else
                {


                    var a2 = from t in db.Hypersensivities.Where(w => w.PID == pid) select new { t.HypMainCategory.HypersenceMainCategory, t.HypMainCategory.HypersenceMainCatID, t.HypersenseDetail };







                    var result = new { serv = opd.ToList(), serv1 = a2.ToList(), imgd = imageDataURL, err = iserror };
                    // return Json(result, JsonRequestBehavior.AllowGet);

                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;

                    return jsonResult;
                }
                var result2 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = iserror };
                return Json(result2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result3 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = "2" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetPatientsall(string st, string id, string relet)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string opdid = (String)Session["userlocid1"];
                if (opdid.Contains("cl"))
                {
                }
                string pid = "";
                string iserror = "";
                string medcat = "";
                string sexc = "";
                string relationp = "";
                int svt = 0;
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                st = st.Trim(MyChar);
                char[] MyChar1 = { '/', '"', ' ' };
                string imageDataURL = "";
                string locid = (String)Session["userloc"];
                int NewString1 = Convert.ToInt32(relet.Trim(MyChar1));
                List<getnursedata> nlist = new List<getnursedata>();
                DateTime dd = DateTime.Now.Date;
                int stp = Convert.ToInt32(st);
                var ser = from s in db.Patients
                          join x in db.PersonalDetails on s.ServiceNo equals x.ServiceNo
                          join y in db.MedicalCategories on x.SNo equals y.SNo
                          join z in db.RelationshipTypes on s.RelationshipType equals z.RTypeID
                          where (s.ServiceNo == NewString)where(s.Service_Type == stp)where(s.RelationshipType == NewString1)

                          select new getnursedata { PID = s.PID, RNK_NAME = "", Initials = s.Initials, Surname = s.Surname, SxDetail =x.Gender, Category = y.MedicalCategory1, Relationship = s.RelationshipType.ToString(), sv = s.Service_Type, dob = s.DateOfBirth };
                foreach (var item in ser)
                {

                    pid = item.PID;
                    medcat = item.Category;
                    sexc = item.SxDetail;
                    svt = item.sv;
                    relationp = item.Relationship;

                    if (item.Relationship == "1")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.MedicalCategories on s.SNo equals b.SNo
                                                into sc
                                                from b in sc.DefaultIfEmpty()
                                                where s.ServiceNo == NewString && s.ServiceType== svt
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = s.Initials, Surname = s.Surname, SxDetail = s.Gender, Category = b.MedicalCategory1, Relationship = relationp, sv = svt };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }






                    }
                    if (item.Relationship.ToLower() == "2")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.SpouseDetails on s.SNo equals b.SNo 
                                                where s.ServiceNo == NewString && s.ServiceType == svt
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.SpouseName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "3")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo
                                                where s.ServiceNo == NewString && b.Relationship == "Father" && s.ServiceType == svt
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ParentName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "4")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo

                                                where s.ServiceNo == NewString && b.Relationship == "Mother" && s.ServiceType == svt
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ParentName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };
                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());
                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }





                    }
                    if (item.Relationship.ToLower() == "5")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.Children on s.SNo equals b.SNo

                                                where s.ServiceNo == NewString && b.DOB == item.dob && s.ServiceType == svt
                                                select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ChildName, SxDetail = b.Gender, Category = "", Relationship = relationp, sv = svt, dob = b.DOB };

                        if (PersonResultList1.Count() > 0)
                        {
                            nlist.Add(PersonResultList1.First());

                        }
                        else
                        {
                            iserror = "3";
                            nlist.Add(item);
                        }


                    }
                    if (nlist.Count() < 1)
                    {

                        nlist.Add(item);
                    }


                }

               
                if (String.IsNullOrEmpty(pid))
                {
                    iserror = "1";
                }


               
                else
                {


                    var a2 = from t in db.Hypersensivities.Where(w => w.PID == pid) select new { t.HypMainCategory.HypersenceMainCategory, t.HypMainCategory.HypersenceMainCatID, t.HypersenseDetail };


                    var a3 = from t in db.PastMedHistories.Where(w => w.PID == pid) select new { t.PMHDetail,t.Drughst };




                    var result = new { serv = nlist.ToList(), serv1 = a2.ToList(), serv2 = a3.ToList(), imgd = imageDataURL, err = iserror };
                    // return Json(result, JsonRequestBehavior.AllowGet);

                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;

                    return jsonResult;
                }
                var result2 = new { serv = "", serv1 = "", serv2 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = iserror };
                return Json(result2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result3 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = "2" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetPatientsick(string st, string id, string relet)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string opdid = (String)Session["userlocid1"];
                string locid = (String)Session["userloc"];
                if (!String.IsNullOrEmpty(opdid))
                {

                    string pid = "";
                    string iserror = "";
                    string medcat = "";
                    string fn = "";
                    string ln = "";
                    string sexc = "";
                    string rnk = "";
                    DateTime? dob = null;
                    string relationp = "";
                    int svt = 0;
                    char[] MyChar = { '/', '"', ' ' };
                    string NewString = id.Trim(MyChar);
                    char[] MyChar1 = { '/', '"', ' ' };
                    string imageDataURL = "";
                    int NewString1 = Convert.ToInt32(relet.Trim(MyChar1));
                    List<getnursedata> nlist = new List<getnursedata>();
                    st = st.Trim(MyChar);
                    int st1 = Convert.ToInt32(st);
                    string blackno = "";


                    //////////////////////////////////////////////////////////
                    DataTable oDataSetix = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();

                    sqlQuery = "SELECT * FROM [MMS].[dbo].[Vw_BlackListManagement] where [Service No]='" + NewString + "'  ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(oDataSetix);
                    oSqlConnection.Close();
                    var opd2ix = oDataSetix.AsEnumerable()
            .Select(dataRow => new getnursedata
            {
                PID = dataRow.Field<string>("Service No"),


            }).ToList();
                    foreach (var i in opd2ix)
                    {
                        blackno = i.PID;
                    }
                    /////////////////////////////////////////////////




                    var ser = from s in db.Patients.Where(p => p.ServiceNo == NewString).Where(p => p.RelationshipType == NewString1).Where(p => p.Service_Type == st1)
                   

                              select new getnursedata { PID = s.PID, RNK_NAME = "", Initials = s.Initials, Surname = s.Surname, SxDetail = "", Category = "", Relationship = s.RelationshipType.ToString(), sv = s.Service_Type, dob = s.DateOfBirth };




                    foreach (var item in ser)
                    {

                        pid = item.PID;

                        sexc = item.SxDetail;

                        relationp = item.Relationship;

                       



                    }
                    var PersonResultList1 = from s in db.PersonalDetails
                                            join b in db.MedicalCategories on s.SNo equals b.SNo
                                            into sc
                                            from b in sc.DefaultIfEmpty()
                                            where s.ServiceNo == NewString && s.ServiceType == st1
                                            select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = s.Initials, Surname = s.Surname, SxDetail = s.Gender, Category = b.MedicalCategory1, Relationship = relationp, sv = s.ServiceType, dob = s.DateOfBirth, enl = s.DateOfEnlist };

                    if (PersonResultList1.Count() > 0)
                    {
                        nlist.Add(PersonResultList1.First());
                    }
                    foreach (var itemx in PersonResultList1)
                    {
                        svt = itemx.sv;
                        dob = itemx.dob;
                        fn = itemx.Initials;
                        ln = itemx.Surname;
                        medcat = itemx.Category;
                        rnk = itemx.RNK_NAME;
                        sexc = itemx.SxDetail;
                    }

                    if (String.IsNullOrEmpty(pid) && PersonResultList1.Count() > 0)
                    {
                        IndexGeneration indi = new IndexGeneration();
                        Patient patient = new Patient();
                        //iserror = "1";
                        if (st1 == 1)
                        {
                            patient.PID = indi.CreatePID(1, "O" + NewString);
                        }
                        else
                        {
                            patient.PID = indi.CreatePID(1, NewString);
                        }

                        patient.RelationshipType = 1;
                        patient.Initials = fn;
                        patient.Surname = ln;
                        patient.DateOfBirth = dob;
                        patient.RANK = 1;
                        patient.ServiceNo = NewString;
                        patient.Service_Type = svt;
                        patient.LocationID = locid;
                        patient.CreatedDate = DateTime.Now.Date;
                        patient.Status = 1;
                        patient.ChildNo = 1;
                        db.Patients.Add(patient);
                        db.SaveChanges();
                        var a2 = from t in db.Hypersensivities.Where(w => w.PID == pid) select new { t.HypMainCategory.HypersenceMainCategory, t.HypMainCategory.HypersenceMainCatID, t.HypersenseDetail };



                        if (!String.IsNullOrEmpty(blackno))

                        {
                            iserror = "5";
                        }



                        var result = new { serv = nlist.ToList(), serv1 = a2.ToList(), imgd = imageDataURL, err = iserror };
                        // return Json(result, JsonRequestBehavior.AllowGet);

                        var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;

                        return jsonResult;
                    }
                    else
                    {


                        var a2 = from t in db.Hypersensivities.Where(w => w.PID == pid) select new { t.HypMainCategory.HypersenceMainCategory, t.HypMainCategory.HypersenceMainCatID, t.HypersenseDetail };


                        if (!String.IsNullOrEmpty(blackno))

                        {
                            iserror = "5";
                        }




                        var result = new { serv = nlist.ToList(), serv1 = a2.ToList(), imgd = imageDataURL, err = iserror };
                        // return Json(result, JsonRequestBehavior.AllowGet);

                        var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;

                        return jsonResult;
                    }
                    var result2 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = iserror };
                    return Json(result2, JsonRequestBehavior.AllowGet);
                }
                else {
                    var result2 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = "2" };
                    return Json(result2, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                var result3 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = "1" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Getuser(string id, string relet)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                string opdid = (String)Session["userlocid1"];
                string locid = (String)Session["userloc"];
                if (opdid.Contains("cl"))
                {
                }
                string pid = "";
                string iserror = "";
                string medcat = "";
                string fn = "";
                string ln = "";
                string sexc = "";
                DateTime? dob = null;
                string relationp = "";
                int svt = 0;
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                char[] MyChar1 = { '/', '"', ' ' };
                string imageDataURL = "";
                int NewString1 = Convert.ToInt32(relet.Trim(MyChar1));
                List<getnursedata> nlist = new List<getnursedata>();



                var ser = from s in db.Patients.Where(p => p.ServiceNo == NewString).Where(p => p.RelationshipType == NewString1)
                           join x in db.PersonalDetails on s.ServiceNo equals x.ServiceNo
                              join y in db.MedicalCategories on x.SNo equals y.SNo
                              join z in db.RelationshipTypes on s.RelationshipType equals z.RTypeID
                          select new getnursedata { PID = s.PID, RNK_NAME = "", Initials = s.Initials, Surname = s.Surname, SxDetail = x.Gender, Category = y.MedicalCategory1, Relationship = s.RelationshipType.ToString(), sv = s.Service_Type, dob = s.DateOfBirth };




                foreach (var item in ser)
                {

                    pid = item.PID;

                    sexc = item.SxDetail;

                    relationp = item.Relationship;

                    //    if (item.Relationship == "1" )
                    //    {

                    //        //else
                    //        //{
                    //        //    iserror = "3";
                    //        //    nlist.Add(item);
                    //        //}






                    //    }
                    //if (item.Relationship.ToLower() == "2")
                    //{
                    //    var PersonResultList1 = from s in db.PersonalDetails
                    //                            join b in db.SpouseDetails on s.SNo equals b.SNo
                    //                            where s.ServiceNo == NewString
                    //                            select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.SpouseName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };

                    //    if (PersonResultList1.Count() > 0)
                    //    {
                    //        nlist.Add(PersonResultList1.First());
                    //    }
                    //    else
                    //    {
                    //        iserror = "3";
                    //        nlist.Add(item);
                    //    }





                    //}
                    //if (item.Relationship.ToLower() == "3")
                    //{
                    //    var PersonResultList1 = from s in db.PersonalDetails
                    //                            join b in db.parents on s.SNo equals b.SNo
                    //                            where s.ServiceNo == NewString && b.Relationship == "Father"
                    //                            select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ParentName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };

                    //    if (PersonResultList1.Count() > 0)
                    //    {
                    //        nlist.Add(PersonResultList1.First());
                    //    }
                    //    else
                    //    {
                    //        iserror = "3";
                    //        nlist.Add(item);
                    //    }





                    //}
                    //if (item.Relationship.ToLower() == "4")
                    //{
                    //    var PersonResultList1 = from s in db.PersonalDetails
                    //                            join b in db.parents on s.SNo equals b.SNo

                    //                            where s.ServiceNo == NewString && b.Relationship == "Mother"
                    //                            select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ParentName, SxDetail = sexc, Category = "", Relationship = relationp, sv = svt };
                    //    if (PersonResultList1.Count() > 0)
                    //    {
                    //        nlist.Add(PersonResultList1.First());
                    //    }
                    //    else
                    //    {
                    //        iserror = "3";
                    //        nlist.Add(item);
                    //    }





                    //}
                    //if (item.Relationship.ToLower() == "5")
                    //{
                    //    var PersonResultList1 = from s in db.PersonalDetails
                    //                            join b in db.Children on s.SNo equals b.SNo

                    //                            where s.ServiceNo == NewString && b.DOB ==item.dob
                    //                            select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = "", Surname = b.ChildName, SxDetail = b.Gender, Category = "", Relationship = relationp, sv = svt,dob=b.DOB };

                    //    if (PersonResultList1.Count() > 0)
                    //    {
                    //        nlist.Add(PersonResultList1.First());

                    //    }
                    //    else
                    //    {
                    //        iserror = "3";
                    //        nlist.Add(item);
                    //    }


                    //}



                }
                var PersonResultList1 = from s in db.PersonalDetails
                                        join b in db.MedicalCategories on s.SNo equals b.SNo
                                        into sc
                                        from b in sc.DefaultIfEmpty()
                                        where s.ServiceNo == NewString
                                        select new getnursedata { PID = pid, RNK_NAME = s.Rank, Initials = s.Initials, Surname = s.Surname, SxDetail = s.Gender, Category = b.MedicalCategory1, Relationship = relationp, sv = s.ServiceType, dob = s.DateOfBirth, enl = s.DateOfEnlist };

                if (PersonResultList1.Count() > 0)
                {
                    nlist.Add(PersonResultList1.First());
                }
                foreach (var itemx in PersonResultList1)
                {
                    svt = itemx.sv;
                    dob = itemx.dob;
                    fn = itemx.Initials;
                    ln = itemx.Surname;
                    medcat = itemx.Category;
                }

                


                  //  var a2 = from t in db.Hypersensivities.Where(w => w.PID == pid) select new { t.HypMainCategory.HypersenceMainCategory, t.HypMainCategory.HypersenceMainCatID, t.HypersenseDetail };







                    var result = new { serv = nlist.ToList(), serv1 = "", imgd = imageDataURL, err = iserror };
                    // return Json(result, JsonRequestBehavior.AllowGet);

                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;

                    return jsonResult;
                
               
            }
            catch (Exception ex)
            {
                var result3 = new { serv = "", serv1 = "", imgd = string.Format("data:image/png;base64,{0}", ""), err = "2" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetOPDPatient(string id)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 100000;");
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
                if (!String.IsNullOrEmpty(opdid))
                {


                    var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                    foreach (var item in serW)
                    {

                        locid = item.LocationID;
                    }

                    string iserror = "";
                    string pid = "";
                    int? sex = 0;
                    string PDID = "";
                    string svcn = "";
                    string sertp = "";
                    string Initials = "";
                    string Surname = "";
                    string dtb = "";
                    string Present_Complain = "";
                    string History_OtherComplain = "";
                    string History_PresentComplain = "";
                    string Other_Complain = "";
                    string Category = "";
                    DateTime? dob = new DateTime();
                    string BloodType = "";
                    string OPD_Diagnosis = "";
                    string Relationship = "";
                    char[] MyChar = { '/', '"', ' ' };
                    string NewString = id.Trim(MyChar);
                    var result = (dynamic)null;
                    var bg = (dynamic)null;
                    var medbd = (dynamic)null;
                    var a1 = new List<getdocdata>();
                    var a1v = new List<getdocdata>();
                    DateTime dd = DateTime.Now.Date;
                    try
                    {
                        DataTable oDataSet3 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "     SELECT (a.Present_Complain)pcomoplian,(a.History_PresentComplain) hp,(a.Examination) ho,   " +
     "   (a.Other_Complain) oc,(a.OPD_Diagnosis)opddgns , " +

        "  (a.OPDID)OPDID, (a.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate " +

       "     FROM[MMS].[dbo].[Patient_Detail] as a with(nolock) " +

     "            left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
    " where   a.pdid='" + NewString + "' ";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlCommand.CommandTimeout = 120;
                        oSqlConnection.Open();
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSet3);
                        //  oSqlConnection.Close();
                        a1v = oDataSet3.AsEnumerable()
               .Select(dataRow => new getdocdata
               {
                   PID = dataRow.Field<string>("pidp"),
                   PDID = dataRow.Field<string>("pdids"),

                   Present_Complain = dataRow.Field<string>("pcomoplian"),

                   History_OtherComplain = dataRow.Field<string>("ho"),
                   History_PresentComplain = dataRow.Field<string>("hp"),
                   Other_Complain = dataRow.Field<string>("oc"),

                   OPD_Diagnosis = dataRow.Field<string>("opddgns"),

               }).ToList();
                    }
                    catch (Exception ex)
                    {

                    }






                    foreach (var itemv in a1v)
                    {
                        pid = itemv.PID;
                        PDID = itemv.PDID;
                        //svcn = item.ServiceNo;
                        //Present_Complain = item.Present_Complain;
                        //History_OtherComplain = item.History_OtherComplain;
                        //History_PresentComplain = item.History_PresentComplain;
                        //Other_Complain = item.Other_Complain;
                        //Category = item.Category;
                        //BloodType = item.BloodType;
                        //OPD_Diagnosis = item.OPD_Diagnosis;
                        //Relationship = item.Relationship;
                        //dob = item.dob;
                        int? stype = 0;
                        string svcno = "";

                        try
                        {
                            DataTable oDataSet3new = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "     SELECT top 1 (b.BloodGroup) bg ,(k.MedicalCategory) medcat ,b.Surname,f.ChildName,e.SpouseName, g.ParentName,  " +
              "    (c.Service_Type) svt, (c.Sex)sex,(c.ChildNo)chno,       " +
            "	   COALESCE(NULLIF(concat((case when c.RelationshipType = 1    and b.Surname != '0'  then b.Surname end), " +
              "     (case when c.RelationshipType = 2 then e.SpouseName  end), (case when c.RelationshipType = 5 and " +

              "      c.DateOfBirth = f.DOB  then f.ChildName  end),    (case when c.RelationshipType = 3 and g.Relationship = 'Father' " +

             "         then g.ParentName end),    (case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName end)),  " +
            "		  ''), (c.surname)) sname  ,   COALESCE(NULLIF(concat((case when c.RelationshipType = 1      then b.DateOfBirth end), " +
               "          (case when c.RelationshipType = 2 then c.DateOfBirth  end), (case when c.RelationshipType = 5    then f.DOB " +

             "             end)), ''), (c.DateOfBirth))      dob, 	(CASE WHEN isnull(b.Rank,'0')='0' and c.RelationshipType = 1   then (select top 1 Rank from [MMS].[dbo].[PersonalDetails] where ServiceNo=c.ServiceNo) when  isnull(b.Rank,'0')!='0' and c.RelationshipType = 1   then  b.Rank end )rnkname, " +
            "			   (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)  inililes,  " +

                "		   (c.RelationshipType)relasiont     ,  (h.Relationship)relasiondet,   (m.ReqStatus)mauth   FROM[MMS].[dbo].[Patient] as " +
            "      c  with(nolock)    left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo and c.Service_Type= b.ServiceType " +
            " left join[MMS].[dbo].[SpouseDetails]  " +
            "			   as e on b.SNo=e.SNo left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents]  " +
            "			   as g on b.SNo=g.SNo left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
            " left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo left join " +
            " [MMS].[dbo].Vw_MedicalBoardAuthority as  m on b.SNo = m.SNo where   c.pid='" + pid + "'  ";
                            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            oSqlCommand.CommandTimeout = 120;
                            oSqlConnection.Open();
                            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                            oSqlDataAdapter.Fill(oDataSet3new);
                            //  oSqlConnection.Close();
                            a1 = oDataSet3new.AsEnumerable()
                   .Select(dataRow => new getdocdata
                   {
                       childname = dataRow.Field<string>("ChildName"),
                       farthername = dataRow.Field<string>("ParentName"),
                       spousename = dataRow.Field<string>("SpouseName"),
                       selfname = dataRow.Field<string>("Surname"),

                       Initials = dataRow.Field<string>("inililes"),
                       Surname = dataRow.Field<string>("sname"),
                       ServiceNo = dataRow.Field<string>("sno"),

                       RNK_NAME = dataRow.Field<string>("rnkname"),

                       Category = dataRow.Field<string>("medcat"),
                       BloodType = dataRow.Field<string>("bg"),

                       Relationship = dataRow.Field<string>("relasiondet"),
                       sv = dataRow.Field<int?>("svt"),
                       sex = dataRow.Field<int?>("sex"),
                       chno = dataRow.Field<int?>("chno"),
                       dob = dataRow.Field<DateTime?>("dob"),
                       mauth = dataRow.Field<int?>("mauth"),
                   }).ToList();
                        }
                        catch (Exception ex)
                        {

                        }

                        foreach (var item in a1)
                        {
                            svcn = item.ServiceNo;
                            dob = item.dob;
                            Relationship = item.Relationship;
                            Initials = item.Initials;
                            Surname = item.Surname;
                            sertp = item.sv.ToString();
                        }


                        var PersonResultList1 = new List<getdocdata>();
                        List<getdocdata> df = new List<getdocdata>();

                        try
                        {
                            DataTable oDataSet31 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "   select a.Surname,a.Rank,a.Initials,c.pid FROM[MMS].[dbo].PersonalDetails as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.ServiceNo=c.ServiceNo and c.Service_Type= a.ServiceType where a.ServiceNo='" + svcn + "' and a.ServiceType='" + sertp + "' and c.RelationshipType=1 ";
                            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            //  oSqlConnection.Open();
                            oSqlCommand.CommandTimeout = 120;
                            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                            oSqlDataAdapter.Fill(oDataSet31);
                            //  oSqlConnection.Close();
                            PersonResultList1 = oDataSet31.AsEnumerable()
                   .Select(dataRow => new getdocdata
                   {

                       Initials = dataRow.Field<string>("Initials"),
                       Surname = dataRow.Field<string>("Surname"),
                       PID = dataRow.Field<string>("pid"),
                       RNK_NAME = dataRow.Field<string>("Rank").ToString(),
                       Relationship = "SELF",

                   }).ToList();


                            if (PersonResultList1.Count() > 0)
                            {
                                df.Add(PersonResultList1.First());
                            }
                        }
                        catch (Exception ex)
                        {

                        }






                        try
                        {
                            DataTable oDataSet32 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = " select top 1 a.SpouseName as Surname,b.PID FROM [MMS].[dbo].SpouseDetails as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo and b.Service_Type= c.ServiceType where c.ServiceNo='" + svcn + "' and b.RelationshipType=2 and c.ServiceType='" + sertp + "' ";
                            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            oSqlCommand.CommandTimeout = 120;
                            //  oSqlConnection.Open();
                            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                            oSqlDataAdapter.Fill(oDataSet32);
                            //   oSqlConnection.Close();
                            PersonResultList1 = oDataSet32.AsEnumerable()
                   .Select(dataRow => new getdocdata
                   {

                       Surname = dataRow.Field<string>("Surname"),
                       PID = dataRow.Field<string>("PID"),
                       Relationship = "Spouse",
                   }).ToList();

                        }
                        catch (Exception ex)
                        {
                        }
                        if (PersonResultList1.Count() > 0)
                        {
                            df.Add(PersonResultList1.First());
                        }
                        else
                        {

                            try
                            {
                                DataTable oDataSet35 = new DataTable();
                                oSqlConnection = new SqlConnection(conStr);
                                oSqlCommand = new SqlCommand();
                                sqlQuery = "  select top 1 a.SpouseName as Surname FROM [MMS].[dbo].SpouseDetails as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo   where c.ServiceNo='" + svcn + "' and c.ServiceType='" + sertp + "' ";
                                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                                oSqlCommand.Connection = oSqlConnection;
                                oSqlCommand.CommandText = sqlQuery;
                                oSqlCommand.CommandTimeout = 120;
                                //   oSqlConnection.Open();
                                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                                oSqlDataAdapter.Fill(oDataSet35);
                                //    oSqlConnection.Close();
                                PersonResultList1 = oDataSet35.AsEnumerable()
                       .Select(dataRow => new getdocdata
                       {

                           Surname = dataRow.Field<string>("Surname"),

                           Relationship = "Spouse",
                       }).ToList();
                            }
                            catch (Exception ex)
                            {

                            }
                            if (PersonResultList1.Count() > 0)
                            {
                                try
                                {
                                    IndexGeneration indi = new IndexGeneration();
                                    Patient patient = new Patient();
                                    //iserror = "1";
                                    patient.PID = indi.CreatePID(2, svcn);
                                    patient.RelationshipType = 2;
                                    patient.Initials = Initials;
                                    patient.Surname = Surname;
                                    patient.DateOfBirth = dob;
                                    patient.RANK = 1;
                                    patient.ServiceNo = svcn;
                                    patient.Service_Type = Convert.ToInt32(sertp);
                                    patient.LocationID = locid;
                                    patient.CreatedDate = DateTime.Now.Date;
                                    patient.Status = 1;
                                    patient.ChildNo = 1;
                                    db.Patients.Add(patient);
                                    db.SaveChanges();
                                }
                                catch (Exception ex)
                                {

                                }
                                try
                                {
                                    DataTable oDataSet35x = new DataTable();
                                    oSqlConnection = new SqlConnection(conStr);
                                    oSqlCommand = new SqlCommand();
                                    sqlQuery = "  select top 1 a.SpouseName as Surname,b.PID FROM [MMS].[dbo].SpouseDetails as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo where c.ServiceNo='" + svcn + "' and b.RelationshipType=2 and c.ServiceType='" + sertp + "' ";
                                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                                    oSqlCommand.Connection = oSqlConnection;
                                    oSqlCommand.CommandText = sqlQuery;
                                    //      oSqlConnection.Open();
                                    oSqlCommand.CommandTimeout = 120;
                                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                                    oSqlDataAdapter.Fill(oDataSet35x);
                                    //     oSqlConnection.Close();
                                    PersonResultList1 = oDataSet35x.AsEnumerable()
                           .Select(dataRow => new getdocdata
                           {

                               Surname = dataRow.Field<string>("Surname"),
                               PID = dataRow.Field<string>("PID"),
                               Relationship = "Spouse",
                           }).ToList();

                                    if (PersonResultList1.Count() > 0)
                                    {
                                        df.Add(PersonResultList1.First());
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                        }





                        try
                        {
                            DataTable oDataSet33 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = " select top 1 a.ParentName as Surname,b.PID FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo where c.ServiceNo='" + svcn + "' and b.RelationshipType=3 and a.Relationship='Father' and c.ServiceType='" + sertp + "' ";
                            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            //  oSqlConnection.Open();
                            oSqlCommand.CommandTimeout = 120;
                            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                            oSqlDataAdapter.Fill(oDataSet33);
                            //  oSqlConnection.Close();
                            PersonResultList1 = oDataSet33.AsEnumerable()
                   .Select(dataRow => new getdocdata
                   {

                       Surname = dataRow.Field<string>("Surname"),
                       PID = dataRow.Field<string>("PID"),
                       Relationship = "Father",
                   }).ToList();
                        }
                        catch (Exception ex)
                        {

                        }
                        if (PersonResultList1.Count() > 0)
                        {
                            df.Add(PersonResultList1.First());
                        }
                        else
                        {

                            try
                            {
                                DataTable oDataSet34 = new DataTable();
                                oSqlConnection = new SqlConnection(conStr);
                                oSqlCommand = new SqlCommand();
                                sqlQuery = " select top 1 a.ParentName as Surname FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo   where c.ServiceNo='" + svcn + "'  and a.Relationship='Father' and c.ServiceType='" + sertp + "' ";
                                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                                oSqlCommand.Connection = oSqlConnection;
                                oSqlCommand.CommandText = sqlQuery;
                                //   oSqlConnection.Open();
                                oSqlCommand.CommandTimeout = 120;
                                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                                oSqlDataAdapter.Fill(oDataSet34);
                                //   oSqlConnection.Close();
                                PersonResultList1 = oDataSet34.AsEnumerable()
                       .Select(dataRow => new getdocdata
                       {

                           Surname = dataRow.Field<string>("Surname"),

                       }).ToList();
                            }
                            catch (Exception ex)
                            {
                            }
                            if (PersonResultList1.Count() > 0)
                            {
                                try
                                {
                                    IndexGeneration indi = new IndexGeneration();
                                    Patient patient = new Patient();
                                    //iserror = "1";
                                    patient.PID = indi.CreatePID(3, svcn);
                                    patient.RelationshipType = 3;
                                    patient.Initials = Initials;
                                    patient.Surname = Surname;
                                    patient.DateOfBirth = dob;
                                    patient.RANK = 1;
                                    patient.ServiceNo = svcn;
                                    patient.Service_Type = Convert.ToInt32(sertp);
                                    patient.LocationID = locid;
                                    patient.CreatedDate = DateTime.Now.Date;
                                    patient.Status = 1;
                                    patient.ChildNo = 1;
                                    db.Patients.Add(patient);
                                    db.SaveChanges();
                                }
                                catch (Exception ex)
                                {

                                }
                                try
                                {


                                    DataTable oDataSet34x = new DataTable();
                                    oSqlConnection = new SqlConnection(conStr);
                                    oSqlCommand = new SqlCommand();
                                    sqlQuery = " select top 1 a.ParentName as Surname,b.PID FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo where c.ServiceNo='" + svcn + "' and b.RelationshipType=3 and a.Relationship='Father' and c.ServiceType='" + sertp + "' ";
                                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                                    oSqlCommand.Connection = oSqlConnection;
                                    oSqlCommand.CommandText = sqlQuery;
                                    //     oSqlConnection.Open();
                                    oSqlCommand.CommandTimeout = 120;
                                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                                    oSqlDataAdapter.Fill(oDataSet34x);
                                    //   oSqlConnection.Close();
                                    PersonResultList1 = oDataSet34x.AsEnumerable()
                           .Select(dataRow => new getdocdata
                           {

                               Surname = dataRow.Field<string>("Surname"),
                               PID = dataRow.Field<string>("PID"),
                               Relationship = "Father",
                           }).ToList();
                                }
                                catch (Exception ex)
                                {

                                }
                                if (PersonResultList1.Count() > 0)
                                {
                                    df.Add(PersonResultList1.First());
                                }
                            }


                        }





                        try
                        {
                            DataTable oDataSet36 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = " select top 1 a.ParentName as Surname,b.PID FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo where c.ServiceNo='" + svcn + "' and b.RelationshipType=4 and a.Relationship='Mother' and c.ServiceType='" + sertp + "' ";
                            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            oSqlCommand.CommandTimeout = 120;
                            //   oSqlConnection.Open();
                            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                            oSqlDataAdapter.Fill(oDataSet36);
                            //   oSqlConnection.Close();
                            PersonResultList1 = oDataSet36.AsEnumerable()
                   .Select(dataRow => new getdocdata
                   {

                       Surname = dataRow.Field<string>("Surname"),
                       PID = dataRow.Field<string>("PID"),
                       Relationship = "Mother",
                   }).ToList();
                        }
                        catch (Exception ex)
                        {

                        }
                        if (PersonResultList1.Count() > 0)
                        {
                            df.Add(PersonResultList1.First());
                        }
                        else
                        {

                            try
                            {
                                DataTable oDataSet37 = new DataTable();
                                oSqlConnection = new SqlConnection(conStr);
                                oSqlCommand = new SqlCommand();
                                sqlQuery = " select top 1 a.ParentName as Surname FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  where c.ServiceNo='" + svcn + "' and  a.Relationship='Mother' and c.ServiceType='" + sertp + "' ";
                                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                                oSqlCommand.Connection = oSqlConnection;
                                oSqlCommand.CommandText = sqlQuery;
                                oSqlCommand.CommandTimeout = 120;
                                //  oSqlConnection.Open();
                                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                                oSqlDataAdapter.Fill(oDataSet37);
                                //   oSqlConnection.Close();
                                PersonResultList1 = oDataSet37.AsEnumerable()
                       .Select(dataRow => new getdocdata
                       {

                           Surname = dataRow.Field<string>("Surname"),

                       }).ToList();
                            }
                            catch (Exception ex)
                            {

                            }
                            if (PersonResultList1.Count() > 0)
                            {
                                try
                                {
                                    IndexGeneration indi = new IndexGeneration();
                                    Patient patient = new Patient();
                                    //iserror = "1";
                                    patient.PID = indi.CreatePID(4, svcn);
                                    patient.RelationshipType = 4;
                                    patient.Initials = Initials;
                                    patient.Surname = Surname;
                                    patient.DateOfBirth = dob;
                                    patient.RANK = 1;
                                    patient.ServiceNo = svcn;
                                    patient.Service_Type = Convert.ToInt32(sertp);
                                    patient.LocationID = locid;
                                    patient.CreatedDate = DateTime.Now.Date;
                                    patient.Status = 1;
                                    patient.ChildNo = 1;
                                    db.Patients.Add(patient);
                                    db.SaveChanges();
                                }
                                catch (Exception ex)
                                {

                                }
                                try
                                {
                                    DataTable oDataSet37x = new DataTable();
                                    oSqlConnection = new SqlConnection(conStr);
                                    oSqlCommand = new SqlCommand();
                                    sqlQuery = " select top 1 a.ParentName as Surname,b.PID FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo where c.ServiceNo='" + svcn + "' and b.RelationshipType=4 and a.Relationship='Mother' and c.ServiceType='" + sertp + "' ";
                                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                                    oSqlCommand.Connection = oSqlConnection;
                                    oSqlCommand.CommandText = sqlQuery;
                                    oSqlCommand.CommandTimeout = 120;
                                    //     oSqlConnection.Open();
                                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                                    oSqlDataAdapter.Fill(oDataSet37x);
                                    //   oSqlConnection.Close();
                                    PersonResultList1 = oDataSet37x.AsEnumerable()
                           .Select(dataRow => new getdocdata
                           {

                               Surname = dataRow.Field<string>("Surname"),
                               PID = dataRow.Field<string>("PID"),
                               Relationship = "Mother",
                           }).ToList();
                                }
                                catch (Exception ex) { }
                                if (PersonResultList1.Count() > 0)
                                {

                                    df.Add(PersonResultList1.First());
                                }
                            }


                        }







                        //////////////////////////////////////////////////

                        //var cdlist = from s in db.Patients.Where(p => p.ServiceNo == item.ServiceNo).Where(p => p.RelationshipType == 5)
                        //           orderby s.DateOfBirth  select new { s.DateOfBirth, s.PID };

                        int hj = 1;
                        //    foreach (var itemchw in cdlist)
                        //    {
                        //if (itemchw.PID == itemvb.PID)
                        //        {

                        //            else
                        //            {
                        //                var PersonResultList2 = from s in db.Patients.Where(p => p.PID == itemvb.PID) select new getdocdata { PID = s.PID, Initials = "", Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = "", Category = "", BloodType = s.BloodGroup.BloodType, Relationship = "Child " + hj, sv = s.Service_Type };
                        //                df.Add(PersonResultList2.First());
                        //            }
                        //        }
                        //        hj++;
                        //    }
                        var cdli = new List<getdocdata>();
                        try
                        {
                            DataTable oDataSet38 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "   select a.ChildName as Surname,a.DOB FROM [MMS].[dbo].Children as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo   where c.ServiceNo='" + svcn + "' and c.ServiceType='" + sertp + "'  order by a.DOB asc ";
                            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            oSqlCommand.CommandTimeout = 120;
                            //   oSqlConnection.Open();
                            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                            oSqlDataAdapter.Fill(oDataSet38);
                            //  oSqlConnection.Close();
                            cdli = oDataSet38.AsEnumerable()
                   .Select(dataRow => new getdocdata
                   {

                       Surname = dataRow.Field<string>("Surname"),
                       dob = dataRow.Field<DateTime?>("DOB"),
                   }).ToList();
                        }
                        catch (Exception ex)
                        {

                        }
                        int hjc = 0;
                        if (cdli.Count() > 0)
                        {
                            foreach (var itemw in cdli)
                            {
                                var cdli2 = new List<getdocdata>();
                                try
                                {
                                    DataTable oDataSet39 = new DataTable();
                                    oSqlConnection = new SqlConnection(conStr);
                                    oSqlCommand = new SqlCommand();
                                    sqlQuery = "   select a.Surname as Surname,a.PID FROM [MMS].[dbo].Patient as a with(nolock)    where a.ServiceNo='" + svcn + "'  and  CONVERT(date, a.DateOfBirth)=CONVERT(varchar,'" + itemw.dob + "',111) and a.RelationshipType=5 and a.Service_Type='" + sertp + "'  ";
                                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                                    oSqlCommand.Connection = oSqlConnection;
                                    oSqlCommand.CommandText = sqlQuery;
                                    oSqlCommand.CommandTimeout = 120;
                                    //   oSqlConnection.Open();
                                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                                    oSqlDataAdapter.Fill(oDataSet39);
                                    // oSqlConnection.Close();
                                    cdli2 = oDataSet39.AsEnumerable()
                           .Select(dataRow => new getdocdata
                           {

                               Surname = dataRow.Field<string>("Surname"),
                               PID = dataRow.Field<string>("PID"),
                           }).ToList();

                                }
                                catch (Exception ex)
                                {

                                }
                                if (cdli2.Count() > 0)
                                {
                                    var cdli3 = new List<getdocdata>();
                                    try
                                    {
                                        DataTable oDataSet39x = new DataTable();
                                        oSqlConnection = new SqlConnection(conStr);
                                        oSqlCommand = new SqlCommand();
                                        sqlQuery = "select b.ChildName as Surname,b.DOB,a.PID FROM [MMS].[dbo].Patient as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.ServiceNo=c.ServiceNo  inner join [MMS].[dbo].Children as b on b.SNo=c.SNo  where a.ServiceNo='" + svcn + "' and CONVERT(date, a.DateOfBirth)=CONVERT(varchar,'" + itemw.dob + "',111) and CONVERT(date, b.DOB)=CONVERT(varchar,'" + itemw.dob + "',111) and a.RelationshipType=5 and c.ServiceType='" + sertp + "'  order by b.DOB asc ";
                                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                                        oSqlCommand.Connection = oSqlConnection;
                                        oSqlCommand.CommandText = sqlQuery;
                                        oSqlCommand.CommandTimeout = 120;
                                        //  oSqlConnection.Open();
                                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                                        oSqlDataAdapter.Fill(oDataSet39x);
                                        //  oSqlConnection.Close();
                                        cdli3 = oDataSet39x.AsEnumerable()
                               .Select(dataRow => new getdocdata
                               {

                                   Surname = dataRow.Field<string>("Surname"),
                                   PID = dataRow.Field<string>("PID"),
                                   Relationship = "Child" + (hjc + 1).ToString(),
                               }).ToList();
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    if (cdli3.Count() > 0)
                                    {

                                        df.Add(cdli3.First());
                                        hjc++;
                                    }


                                }
                                else
                                {
                                    try
                                    {
                                        IndexGeneration indi = new IndexGeneration();
                                        Patient patient = new Patient();
                                        //iserror = "1";
                                        patient.PID = indi.CreatePID(5, svcn);
                                        patient.RelationshipType = 5;
                                        patient.Initials = Initials;
                                        patient.Surname = itemw.Surname;
                                        patient.DateOfBirth = itemw.dob;
                                        patient.RANK = 1;
                                        patient.ServiceNo = svcn;
                                        patient.Service_Type = Convert.ToInt32(sertp);
                                        patient.LocationID = locid;
                                        patient.CreatedDate = DateTime.Now.Date;
                                        patient.Status = 1;
                                        patient.ChildNo = hjc + 1;
                                        db.Patients.Add(patient);
                                        db.SaveChanges();
                                    }
                                    catch (Exception ex) { }
                                    var cdli4 = new List<getdocdata>();
                                    try
                                    {
                                        DataTable oDataSet39x2 = new DataTable();
                                        oSqlConnection = new SqlConnection(conStr);
                                        oSqlCommand = new SqlCommand();
                                        sqlQuery = "select b.ChildName as Surname,b.DOB,a.PID FROM [MMS].[dbo].Patient as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.ServiceNo=c.ServiceNo  inner join [MMS].[dbo].Children as b on b.SNo=c.SNo  where a.ServiceNo='" + svcn + "' and CONVERT(date, a.DateOfBirth)=CONVERT(varchar,'" + itemw.dob + "',111) and CONVERT(date, b.DOB)=CONVERT(varchar,'" + itemw.dob + "',111) and a.RelationshipType=5 order by b.DOB asc ";
                                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                                        oSqlCommand.Connection = oSqlConnection;
                                        oSqlCommand.CommandText = sqlQuery;
                                        oSqlCommand.CommandTimeout = 120;
                                        // oSqlConnection.Open();
                                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                                        oSqlDataAdapter.Fill(oDataSet39x2);
                                        //oSqlConnection.Close();
                                        cdli4 = oDataSet39x2.AsEnumerable()
                               .Select(dataRow => new getdocdata
                               {

                                   Surname = dataRow.Field<string>("Surname"),
                                   PID = dataRow.Field<string>("PID"),
                                   Relationship = "Child" + (hjc + 1).ToString(),
                               }).ToList();
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    if (cdli4.Count() > 0)
                                    {

                                        df.Add(cdli4.First());
                                        hjc++;
                                    }



                                }


                            }
                        }
                        else
                        {
                            try
                            {
                                DataTable oDataSet39c = new DataTable();
                                oSqlConnection = new SqlConnection(conStr);
                                oSqlCommand = new SqlCommand();
                                sqlQuery = "   select a.Surname as Surname,a.PID FROM [MMS].[dbo].Patient as a with(nolock)    where a.ServiceNo='" + svcn + "'  and   a.RelationshipType=5 and a.ServiceType='" + sertp + "' ";
                                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                                oSqlCommand.Connection = oSqlConnection;
                                oSqlCommand.CommandText = sqlQuery;
                                oSqlCommand.CommandTimeout = 120;
                                //   oSqlConnection.Open();
                                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                                oSqlDataAdapter.Fill(oDataSet39c);
                                // oSqlConnection.Close();
                                var cdli2v = oDataSet39c.AsEnumerable()
                        .Select(dataRow => new getdocdata
                        {

                            Surname = dataRow.Field<string>("Surname"),
                            PID = dataRow.Field<string>("PID"),
                            Relationship = "Child" + (hjc + 1).ToString(),
                        }).ToList();

                                if (cdli2v.Count() > 0)
                                {
                                    foreach (var itemb in cdli2v)
                                    {
                                        df.Add(itemb);
                                        hjc++;
                                    }


                                }
                            }
                            catch (Exception ex) { }


                        }

                        ////////////////////////////////////////////////////////
                        var rrr = (dynamic)null;


                        try
                        {
                            DataTable oDataSet31n = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "   select a.pid,a.Initials,a.Surname,a.Rank,h.Relationship FROM[MMS].[dbo].Patient as a with(nolock) left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=a.RelationshipType  where a.ServiceNo='" + svcn + "' ";
                            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            //  oSqlConnection.Open();
                            oSqlCommand.CommandTimeout = 120;
                            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                            oSqlDataAdapter.Fill(oDataSet31n);
                            //  oSqlConnection.Close();
                            PersonResultList1 = oDataSet31n.AsEnumerable()
                   .Select(dataRow => new getdocdata
                   {

                       Initials = dataRow.Field<string>("Initials"),
                       Surname = dataRow.Field<string>("Surname"),
                       PID = dataRow.Field<string>("pid"),
                       RNK_NAME = dataRow.Field<int>("Rank").ToString(),
                       Relationship = dataRow.Field<string>("Relationship").ToString(),
                       wardNo = "1",
                   }).ToList();

                            var rr = PersonResultList1.Where(p => !df.Any(p2 => p2.PID == p.PID)).ToList();
                            rrr = df.Concat(rr);
                            //foreach (var itemb in PersonResultList1)
                            //{
                            //    foreach (var itemb1 in df)
                            //{



                            //        string rrrt = "";
                            //        if (itemb1.Relationship.ToLower().Equals("self"))
                            //        {
                            //            rrrt = "1";
                            //        }
                            //        if (itemb1.Relationship.ToLower().Equals("spouse"))
                            //        {
                            //            rrrt = "2";
                            //        }
                            //        if (itemb1.Relationship.ToLower().Equals("father"))
                            //        {
                            //            rrrt = "3";
                            //        }
                            //        if (itemb1.Relationship.ToLower().Equals("mother"))
                            //        {
                            //            rrrt = "4";
                            //        }
                            //        if (itemb1.Relationship.ToLower().Equals("child1")|| itemb1.Relationship.ToLower().Equals("child2")||itemb1.Relationship.ToLower().Equals("child3"))
                            //        {
                            //            rrrt = "5";
                            //        }
                            //        if (rrrt.Equals(itemb.Relationship))
                            //        {
                            //            break;
                            //        }
                            //        //if (String.IsNullOrEmpty( rrrt)&& !String.IsNullOrEmpty(itemb.Relationship))
                            //        //{
                            //        //    df.Add(itemb);
                            //        //}
                            //    }

                            //}
                        }
                        catch (Exception ex)
                        {

                        }

                        ////////////////////////////////////////////////////////////
                        DataTable oDataSetv4 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "    select(l.PMHDetail) PMHDetail,(l.Drughst)Drughst " +
      " ,(n.PlanofMgt)PlanofMgt,(n.ReffNote)ReffNote,(o.Other)abdx,(p.Other)cardx,(q.Other)cenx,(r.Other)genx,  " +
    "   (s.Other)othx,(t.Other)respx " +
     "  from MMS.[dbo].[Patient_Detail] as a left join[MMS].[dbo].[PastMedHistory] as l on l.pid = a.pid left join[MMS].[dbo].[CatReferals] as n on n.pdid=a.pdid " +
    " left join[MMS].[dbo].[ExamineAbdominal]as o on o.pdid=a.pdid left join[MMS].[dbo].[ExamineCardiovascular] as p on p.pdid=  " +
    "	  a.pdid left join[MMS].[dbo].[ExamineCentralNervous] as q on q.pdid=a.pdid left join[MMS].[dbo].[ExamineGeneral]as r on r.pdid=a.pdid " +
    " left join[MMS].[dbo].[ExamineOther] as s on s.pdid=a.pdid left join[MMS].[dbo].[ExamineRespiratory]as t on t.pdid=a.pdid " +
    " where a.pdid='" + NewString + "' ";
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

                    PMHDetail = dataRow.Field<string>("PMHDetail"),
                    Drughst = dataRow.Field<string>("Drughst"),
                    PlanofMgt = dataRow.Field<string>("PlanofMgt"),
                    ReffNote = dataRow.Field<string>("ReffNote"),
                    abdx = dataRow.Field<string>("abdx"),
                    cardx = dataRow.Field<string>("cardx"),
                    cenx = dataRow.Field<string>("cenx"),
                    genx = dataRow.Field<string>("genx"),
                    othx = dataRow.Field<string>("othx"),
                    respx = dataRow.Field<string>("respx"),

                }).ToList();


                        //var a5 = from s in db.Patient_Detail.Where(p => p.PID == item.PID)
                        //         join b in db.Clinic_Master on s.OPDID equals b.Clinic_ID into cs
                        //         from b in cs.DefaultIfEmpty()
                        //         join c in db.CatDaignosis on s.DaignosisID equals c.dgid into com
                        //         from c in com.DefaultIfEmpty()

                        //           select new { s.PDID, s.CreatedDate, b.Clinic_Detail, c.dgdetail };
                        //var a6 = from s in db.Patient_Detail.Where(p => p.PDID == NewString)
                        //         join b in db.Clinic_Master on s.OPDID equals b.Clinic_ID into cs
                        //         from b in cs.DefaultIfEmpty()
                        //         join c in db.CatDaignosis on s.DaignosisID equals c.dgid into com
                        //         from c in com.DefaultIfEmpty()
                        //         join d in db.PastMedHistories on s.PID equals d.PID into com1
                        //         from d in com1.DefaultIfEmpty()
                        //         join e in db.CatReferals on s.PDID equals e.PDID into com2
                        //         from e in com2.DefaultIfEmpty()
                        //         join f in db.ExamineAbdominals on s.PDID equals f.PDID into com3
                        //         from f in com3.DefaultIfEmpty()
                        //         join g in db.ExamineCardiovasculars on s.PDID equals g.PDID into com4
                        //         from g in com4.DefaultIfEmpty()
                        //         join h in db.ExamineCentralNervous on s.PDID equals h.PDID into com5
                        //         from h in com5.DefaultIfEmpty()
                        //         join i in db.ExamineGenerals on s.PDID equals i.PDID into com6
                        //         from i in com6.DefaultIfEmpty()
                        //         join j in db.ExamineOthers on s.PDID equals j.PDID into com7
                        //         from j in com7.DefaultIfEmpty()
                        //         join k in db.ExamineRespiratories on s.PDID equals k.PDID into com8
                        //         from k in com8.DefaultIfEmpty()
                        //         select new { s.PDID, s.Patient.Initials, s.Patient.Surname, s.Present_Complain, s.Patient.rank1.RNK_NAME, s.CreatedDate, s.OPD_Diagnosis, s.OPDID, b.Clinic_Detail, c.dgdetail, d.PMHDetail, d.Drughst, e.PlanofMgt, e.ReffNote ,abdx=f.Other,cardx=g.Other,cenx=h.Other,genx=i.Other,othx=j.Other,respx=k.Other};

                        //var a2 = from t in db.Hypersensivities.Where(w => w.PID == item.PID) select new { t.HypMainCategory.HypersenceMainCategory, t.HypMainCategory.HypersenceMainCatID, t.HypersenseDetail };

                        DataTable oDataSetv1 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "   SELECT HypersenceMainCatID,HypersenseDetail,HypersenceMainCategory FROM [MMS].[dbo].[Hypersensivity] as a inner join " +
    " [MMS].[dbo].[HypMainCategory] as b on a.[HyperTypeSubID]=b.HypersenceMainCatID where a.PID='" + itemv.PID + "'  ";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlCommand.CommandTimeout = 120;
                        //   oSqlConnection.Open();
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSetv1);
                        // oSqlConnection.Close();
                        var a2 = oDataSetv1.AsEnumerable()
                .Select(dataRow => new Hypersensivityy
                {

                    HypersenceMainCatID = dataRow.Field<string>("HypersenceMainCatID"),
                    HypersenseDetail = dataRow.Field<string>("HypersenseDetail"),
                    HypersenceMainCategory = dataRow.Field<string>("HypersenceMainCategory"),
                }).ToList();


                        DataTable oDataSetv2 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "   SELECT  [VID], [VitalType],[PDID],a.[VTID],[VitalValues],[LocationID],[Reading_Time],[LocID] FROM [MMS].[dbo].[Vitals] " +
    " as a inner join[MMS].[dbo].[Vital_Type] as b on a.vtid=b.vtid where a.PDID='" + NewString + "'  ";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlCommand.CommandTimeout = 120;
                        //   oSqlConnection.Open();
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSetv2);
                        // oSqlConnection.Close();
                        var vitval = oDataSetv2.AsEnumerable()
                .Select(dataRow => new vitals
                {

                    VID = dataRow.Field<string>("VID"),
                    PDID = dataRow.Field<string>("PDID"),
                    Reading_Time = dataRow.Field<DateTime>("Reading_Time"),
                    VitalType = dataRow.Field<string>("VitalType"),
                    VitalValues = dataRow.Field<string>("VitalValues"),


                }).ToList();

                        // var vitval = from u in db.Vitals.Where(r => r.PDID == NewString) select new { u.VID, u.PDID, u.Reading_Time, u.Vital_Type.ReadingUnit, u.Vital_Type.VitalType, u.VitalValues };

                        DataTable oDataSetv3 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "   SELECT [acmRID],[MES],[SvcNo],[WEF],[NextDate],[Remarks] FROM [MMS].[dbo].[VwAirCrew] where SvcNo='" + svcn + "' order by [acmRID] desc ";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlCommand.CommandTimeout = 120;
                        //   oSqlConnection.Open();
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSetv3);
                        // oSqlConnection.Close();
                        var aircrewlist = oDataSetv3.AsEnumerable()
                .Select(dataRow => new VwAirCrew
                {

                    SvcNo = dataRow.Field<string>("SvcNo"),
                    MES = dataRow.Field<string>("MES"),
                    NextDate = dataRow.Field<DateTime?>("NextDate"),
                    Remarks = dataRow.Field<string>("Remarks"),
                    WEF = dataRow.Field<DateTime?>("WEF"),
                    acmRID = dataRow.Field<int>("acmRID"),

                }).ToList();
                        //var aircrewlist = from s in db.VwAirCrews
                        //                  join b in db.Patients on s.SvcNo equals b.ServiceNo
                        //                  where b.ServiceNo == svcn
                        //                  orderby s.acmRID descending
                        //                  select new { s.SvcNo, s.MES, s.NextDate,s.Remarks,s.WEF,s.acmRID };


                        var siccat = from u in db.Sick_Category.Where(p => p.PDID == NewString)
                                     join b in db.Sick_CategoryType on u.CatID equals b.CatID
                                     select new
                                     { u.CatPeriod, b.Category_Type, u.Date };


                        //var lab = from t in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.Patient_Detail.PDID == NewString)
                        //          select new
                        //          { t.Lab_SubCategory.Lab_MainCategory.CategoryName, t.Lab_SubCategory.Lab_MainCategory.CategoryID, t.PDID };
                        //var labl = lab.GroupBy(c => new { c.CategoryName, c.CategoryID }).Select(grp => grp.FirstOrDefault()).ToList();
                        //var lablist = from t in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.PDID == NewString)
                        //              select new
                        //              { t.Lab_SubCategory.Lab_MainCategory.CategoryName, t.Lab_SubCategory.Lab_MainCategory.CategoryID, t.PDID, t.TestSID, t.RequestedTime,t.Issued }; ;
                        //var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();

                        DataTable oDataSetv6 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "   SELECT e.TubeCategory,a.TestSID,e.CategoryID,a.pdid,a.testsid,e.CategoryName,max(a.Issued) Issued,CAST(a.[RequestedTime] as DATE) as RequestedTime  FROM [MMS].[dbo] " +
    " .[Lab_Report] as a  inner join[MMS].[dbo].[Lab_SubCategory] as d on d.[LabTestID]=a.[LabTestID] inner join[MMS].[dbo]. " +
    " [Lab_MainCategory] as e on e.CategoryID=d.CategoryID where  " +
    "    a.PDID= '" + NewString + "'  group by e.CategoryName, a.TestSID, CAST(a.[RequestedTime] as DATE),e.CategoryID,e.TubeCategory " +
    " ,a.pdid";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlCommand.CommandTimeout = 120;
                        //   oSqlConnection.Open();
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(oDataSetv6);
                        // oSqlConnection.Close();
                        var labl = oDataSetv6.AsEnumerable()
                .Select(dataRow => new lablist
                {

                    TestSID = dataRow.Field<string>("TestSID"),
                    CategoryID = dataRow.Field<string>("CategoryID"),
                    pdid = dataRow.Field<string>("pdid"),
                    testsid = dataRow.Field<string>("testsid"),
                    CategoryName = dataRow.Field<string>("CategoryName"),
                    Issued = dataRow.Field<string>("Issued"),
                    RequestedTime = dataRow.Field<DateTime?>("RequestedTime"),
                    TubeCategory = dataRow.Field<int?>("TubeCategory"),
                }).ToList();

                        var i1 = ""; var i2 = ""; int? i3 = 0;
                        List<lablist> temp = new List<lablist>();
                        List<lablist> temp2 = new List<lablist>();
                        List<lablist> temp3 = new List<lablist>();
                        string lnm = "";
                        int cvb = 0;
                        foreach (var item in labl)
                        {

                            if (i1.Equals(item.TestSID) && i3.Equals(item.TubeCategory))
                            {
                                if (temp.Any(x => x.testsid == item.testsid && x.TubeCategory == item.TubeCategory))
                                {
                                    temp.RemoveAt(temp.Count - 1);
                                }
                                else if (temp2.Any(x => x.testsid == item.testsid && x.TubeCategory == item.TubeCategory))
                                {
                                    temp2.RemoveAt(temp2.Count - 1);
                                }
                                lnm = lnm + "/" + item.CategoryName;
                                item.CategoryName = lnm;
                                temp2.Add(item);
                            }
                            else
                            {
                                lnm = "";
                                cvb++;
                                temp.Add(item);

                                lnm = lnm + "/" + item.CategoryName;
                            }
                            i1 = item.testsid;
                            // i2 = item.se;
                            i3 = item.TubeCategory;

                        }

                        var joined4 = temp.Concat(temp2);



                        var joined3 = new List<druglist>();
                        DataTable oDataSetv7 = new DataTable();
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
                        oSqlDataAdapter.Fill(oDataSetv7);
                        // oSqlConnection.Close();
                        try
                        {
                            joined3 = oDataSetv7.AsEnumerable()
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
                        ////////////////////////////////////////////////////////////
                        var regdrlist = new List<druglist>();
                        DataTable redrdst = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "   SELECT a.[issuedQuantity],a.[Date_Time],COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'') as itmdes,b.MethodDetail " +
    " ,a.Ps_Index,b.DrugMethodCount ,c.RouteDetail ,a.Duration,a.MethodType,a.Dose,a.pdid,a.Route,a.Method,a.ItemNo" +
    "  FROM[MMS].[dbo].[Drug_Regular] as a " +
    " left join[MMS].[dbo].[DrugMethod] as b on a.Method=b.MethodID left join[MMS].[dbo].[DrugRoute] as c on a.route=c.routeid " +
    " left join[MMS].[dbo].[DrugItems] as d on a.ItemNo=Convert(varchar, d.DrugID) " +
    "    left join[MMS].[dbo].[EPASPharmacyItems] as e on a.ItemNo=Convert(varchar, e.[itemno]) where " +
    " a.[PDID]='" + pid + "'   " +
    " order by a.PDID";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlCommand.CommandTimeout = 120;
                        //   oSqlConnection.Open();
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                        oSqlDataAdapter.Fill(redrdst);
                        // oSqlConnection.Close();
                        try
                        {
                            regdrlist = redrdst.AsEnumerable()
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
                   Route = dataRow.Field<int?>("Route"),
                   Method = dataRow.Field<int?>("Method"),
                   ItemNo = dataRow.Field<string>("ItemNo"),
               }).ToList();
                        }
                        catch (Exception ex)
                        {

                        }




                        //////////////////////////////////////////////////////////////

                        var medbd1 = new List<MBPS_View>();
                        if (Relationship.ToLower() == "self")
                        {
                            DataTable oDataSetv8 = new DataTable();
                            oSqlConnection = new SqlConnection(conStr);
                            oSqlCommand = new SqlCommand();
                            sqlQuery = "   select * from [dbo].[Vw_MedicalBoard] as a left join [dbo].PersonalDetails as b on a.SNo=b.SNo where b.ServiceNo='" + svcn + "' order by dateOfBoard desc";
                            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            oSqlCommand.Connection = oSqlConnection;
                            oSqlCommand.CommandText = sqlQuery;
                            oSqlCommand.CommandTimeout = 120;
                            //   oSqlConnection.Open();
                            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                            oSqlDataAdapter.Fill(oDataSetv8);
                            // oSqlConnection.Close();
                            medbd1 = oDataSetv8.AsEnumerable()
                   .Select(dataRow => new MBPS_View
                   {

                       CategoryName = dataRow.Field<string>("current_MedCat"),
                       DateOfNextMedical = dataRow.Field<DateTime?>("NextMB"),

                   }).ToList();
                            // medbd = db.MBPS_View.Where(p => p.Ser_No == item.ServiceNo).OrderByDescending(p => p.DateOfBoard).ToList();
                        }

                        oSqlConnection.Close();
                        result = new { s1 = siccat.ToList(), l1 = joined4.ToList(), d1 = joined3.ToList(), b1 = a1.ToList(), b2 = a2.ToList(), b4 = a1v.ToList(), b5 = a6.ToList(), vitval1 = vitval.ToList(), m1 = medbd1, err = iserror, u1 = rrr, acrw = aircrewlist.ToList(), rdrl = regdrlist.ToList() };
                        return Json(result, JsonRequestBehavior.AllowGet);
                        // exabd = exabd.ToList(), excdv = excdv.ToList(), exctn = exctn.ToList(), exgrl = exgrl.ToList(), exrpr = exrpr.ToList()
                    }
                    var result4 = new { s1 = "", l1 = "", d1 = "", b1 = "", b2 = "", b4 = "", b5 = "", vitval1 = "", err = "2", u1 = "", rdrl = "" };
                    return Json(result4, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result4 = new { s1 = "", l1 = "", d1 = "", b1 = "", b2 = "", b4 = "", b5 = "", vitval1 = "", err = "2", u1 = "", rdrl = "" };
                    return Json(result4, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                var result3 = new { s1 = "", l1 = "", d1 = "", b1 = "", b2 = "", b4 = "", b5 = "", vitval1 = "", err = "2", u1 = "", rdrl ="" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }

           
           
        }
        // GET: /Get Service Personnel Details/
        //public JsonResult GetServicePersonnel(string id)
        //{
        //    db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
        //    char[] MyChar = { '/', '"', ' ' };


        //    if (id != null || id.Length > 6)
        //    {

        //        id = id.Trim(MyChar);

        //        //id = id.Substring(1, 5);
        //        var PersonResultList = from s in dbhrms.View_1250
        //                                   //join b in dbhrms.ranks on s.Rank equals b.RANK1
        //                               where s.ActiveNo == id
        //                               select new { s.RNK_NAME, s.Surname, s.Posted_Location, s.Sex };
        //        return Json(PersonResultList, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Json(JsonRequestBehavior.AllowGet);

        //}

        public JsonResult PatientHystorygvs(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            String modif = "";
            String pdetn = "";
            String medyr = "";
            String pdetn2 = "";
            String dgnos = "";
            string iserror = "";
            string pid = "";
            string PDID = "";
            string retype = "";
            string Category = "";
            string BloodType = "";

            string Relationship = "";
            int io = 1;
            int i2 = 1;
            try
            {
                if (id != null)
                {

                    id = id.Trim(MyChar);

                  //  var ax1 = from s in db.Patients.Where(p => p.ServiceNo == id).Where(p => p.RelationshipType==1) select new getdocdata { PID = s.PID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = s.rank1.RNK_NAME, Category = s.MBP_Category.Category, BloodType = s.BloodGroup.BloodType, Relationship = s.RelationshipType1.Relationship, sv = s.Service_Type };


                    //foreach (var item in ax1)
                    //{
                       // pid = item.PID;
                      //  PDID = item.PDID;

                      //  Category = item.Category;
                     //   BloodType = item.BloodType;

                     //   Relationship = item.Relationship;
                        int? stype = 0;
                        string svcno = "";


                        if (Relationship == "SELF")
                        {
                            var ptmed = from s in db.VwAllPaidClaimGVS  where (s.ServiceNo == id) orderby (s.DateSorted) ascending select new  { TotalAmount = s.TotalAmount, Description = s.Description, ServiceNo = s.ServiceNo, DateSorted = s.DateSorted, BeneficiaryName = s.BeneficiaryName };
                            if (ptmed.Count() > 0)
                            {
                                pdetn = pdetn + "GVS Claims: <br />";
                            }
                            foreach (var itmed in ptmed)
                            {

                                if (!string.IsNullOrEmpty(itmed.ServiceNo) && itmed.ServiceNo != "null")
                                {
                                    pdetn = pdetn + "<b>Beneficiary Name </b>" + itmed.BeneficiaryName + "<br /> <b>Description :</b> " + itmed.Description + "<br /><b> Amount:</b> " + itmed.TotalAmount + "<br /> <b>Date: </b>" + itmed.DateSorted + "<br /><hr >";
                                }

                                int slen = pdetn.Length;
                                if (slen > 400)
                                {
                                    pdetn = " <div  id=a" + io + " >" + pdetn + " </div>";
                                    pdetn2 = pdetn2 + pdetn;
                                    pdetn = "";
                                    io++;
                                }
                                else
                                {
                                    //pdetn = pdetn;
                                    if (ptmed.Count() == 1)
                                    {
                                        pdetn2 = pdetn2 + " <div id=a" + io + ">" + pdetn + " </div>";
                                    }
                                    else if (ptmed.Count() == i2)
                                    {
                                        pdetn2 = pdetn2 + " <div id=a" + io + ">" + pdetn + " </div>";
                                        //  pdetn2 = pdetn2 + pdetn;
                                    }

                                }
                                i2++;
                            }

                            //  pdetn = pdetn + medyr;
                        }







                        //  var pdet = string.Join(",", a1);

                        // return Json(result, JsonRequestBehavior.AllowGet);
                   // }


                    // pdetn2 = pdetn2 + " <div id=a" + io + ">" + pdetn + " </div>";
                    var result = new { h1 = pdetn2 };

                    return Json(new { h1 = pdetn2 }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PatientHystoryclaim(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            String modif = "";
            String pdetn = "";
            String medyr = "";
            String pdetn2 = "";
            String dgnos = "";
            string iserror = "";
            string pid = "";
            string PDID = "";
            string retype = "";
            string Category = "";
            string BloodType = "";

            string Relationship = "";
            int io = 1;
            int i2 = 1;
            try
            {
                if (id != null)
                {

                    id = id.Trim(MyChar);

                    //var ax1 = from s in db.Patients.Where(p => p.ServiceNo == id) select new getdocdata { PID = s.PID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = s.rank1.RNK_NAME, Category = s.MBP_Category.Category, BloodType = s.BloodGroup.BloodType, Relationship = s.RelationshipType1.Relationship, sv = s.Service_Type };


                    //foreach (var item in ax1)
                    //{
                        //pid = item.PID;
                        //PDID = item.PDID;

                        //Category = item.Category;
                        //BloodType = item.BloodType;

                        //Relationship = item.Relationship;
                        int? stype = 0;
                        string svcno = "";


                      //  if (Relationship == "SELF")
                        //{
                            var ptmed = from s in db.VwCWFMCS join b in db.PersonalDetails on s.claSno equals b.ServiceNo join c in db.claim_catagory on s.claCatCode equals c.mc_catid  where (s.claSno == id && s.IsProcessed==1) orderby (s.claSaveDate) ascending select new getclaimdata {   serviceno = b.ServiceNo,Surname=b.Surname,initials=b.Initials, crdate = s.claSaveDate,  catagory = c.mc_catdetail,   claimamnt = s.claBillAmount.ToString() };
                            if (ptmed.Count() > 0)
                            {
                                pdetn = pdetn + "Medical Claims: <br />";
                            }
                            foreach (var itmed in ptmed)
                            {

                                if (!string.IsNullOrEmpty(itmed.serviceno) && itmed.serviceno != "null")
                                {
                                    pdetn = pdetn + "<b>Name </b>" + itmed.initials+""+ itmed.Surname + "<br />  Amount:</b> " + itmed.claimamnt + "<br /><b> Issued Date:</b> " + itmed.crdate + "<br /> <b> Catagory:</b> " + itmed.catagory + "<hr >";
                                }

                                int slen = pdetn.Length;
                                if (slen > 400)
                                {
                                    pdetn = " <div  id=a" + io + " >" + pdetn + " </div>";
                                    pdetn2 = pdetn2 + pdetn;
                                    pdetn = "";
                                    io++;
                                }
                                else
                                {
                                    //pdetn = pdetn;
                                    if (ptmed.Count()==1)
                                    {
                                        pdetn2 = pdetn2 + " <div id=a" + io + ">" + pdetn + " </div>";
                                    }
                                    else if(ptmed.Count()==i2)
                                    {
                                        pdetn2 = pdetn2 + " <div id=a" + io + ">" + pdetn + " </div>";
                                        //  pdetn2 = pdetn2 + pdetn;
                                    }
                                    
                                }
                                i2++;
                            }
                            
                            //  pdetn = pdetn + medyr;
                       // }






                       
                        //  var pdet = string.Join(",", a1);

                        // return Json(result, JsonRequestBehavior.AllowGet);
                   // }


                   // pdetn2 = pdetn2 + " <div id=a" + io + ">" + pdetn + " </div>";
                    var result = new { h1 = pdetn2 };

                    return Json(new { h1 = pdetn2 }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PatientHystorypft(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            String modif = "";
            String pdetn = "";
            String medyr = "";
            String pdetn2 = "";
            String dgnos = "";
            string iserror = "";
            string pid = "";
            string PDID = "";
            string retype = "";
            string Category = "";
            string BloodType = "";

            string Relationship = "";
            int io = 1;
            try
            {
                if (id != null)
                {

                    id = id.Trim(MyChar);

                    var ax1 = from s in db.Patients.Where(p => p.PID == id)
                              join x in db.PersonalDetails on s.ServiceNo equals x.ServiceNo
                              join y in db.MedicalCategories on x.SNo equals y.SNo
                              join z in db.RelationshipTypes on s.RelationshipType equals z.RTypeID
                              select new getdocdata { PID = s.PID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = s.rank1.RNK_NAME, Category =y.MedicalCategory1, BloodType = x.BloodGroup, Relationship = z.Relationship, sv = s.Service_Type };


                    foreach (var item in ax1)
                    {
                        pid = item.PID;
                        PDID = item.PDID;

                        Category = item.Category;
                        BloodType = item.BloodType;

                        Relationship = item.Relationship;
                        int? stype = 0;
                        string svcno = "";


                        if (Relationship == "SELF")
                        {
                            var ptmed = from s in db.MedicalStatus.Where(p => p.ServiceNo == item.ServiceNo)


                                        select new { s.ServiceNo, s.ExamYear, s.MedicalStatus };
                            if (ptmed.Count() > 0)
                            {
                                medyr = medyr + "PT Test Medical: ";
                            }
                            foreach (var itmed in ptmed)
                            {

                                if (!string.IsNullOrEmpty(itmed.ServiceNo) && itmed.ServiceNo != "null")
                                {
                                    medyr = medyr + itmed.ExamYear + " " + itmed.MedicalStatus + "<br />";
                                }


                            }

                            pdetn = pdetn + medyr;
                        }






                        int slen = pdetn.Length;
                        if (slen > 400)
                        {
                            pdetn = " <div  id=a" + io + " >" + pdetn + " </div>";
                            pdetn2 = pdetn2 + pdetn;
                            pdetn = "";
                            io++;
                        }
                        else
                        {
                            pdetn = pdetn + " <hr>";

                        }
                        //  var pdet = string.Join(",", a1);

                        // return Json(result, JsonRequestBehavior.AllowGet);
                    }
                   

                    pdetn2 = pdetn2 + " <div id=a" + io + ">" + pdetn + " </div>";
                    var result = new { h1 = pdetn2 };

                    return Json(new { h1 = pdetn2 }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PatientHystorynew(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            String modif = "";
            String pdetn = "";
            String medyr = "";
            String pdetn2 = "";
            String dgnos = "";
            string iserror = "";
            string pid = "";
            string PDID = "";
            string retype = "";
            string Category = "";
            string BloodType = "";
            string servi = "";
            string Relationship = "";
            int io = 1;
            try
            {
                if (id != null)
                {

                    id = id.Trim(MyChar);

                    var ax1 = from s in db.Patients.Where(p => p.PID == id)
                              join x in db.PersonalDetails on s.ServiceNo equals x.ServiceNo
                              join y in db.MedicalCategories on x.SNo equals y.SNo
                              join z in db.RelationshipTypes on s.RelationshipType equals z.RTypeID
                              select new getdocdata { PID = s.PID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = s.rank1.RNK_NAME, Category = y.MedicalCategory1, BloodType = x.BloodGroup, Relationship = z.Relationship, sv = s.Service_Type };


                    foreach (var item in ax1)
                    {
                        pid = item.PID;
                        PDID = item.PDID;

                        Category = item.Category;
                        BloodType = item.BloodType;

                        Relationship = item.Relationship;
                        int? stype = 0;
                        string svcno = "";

                        DataTable oDataSet3 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "   SELECT top 1 max(a.Present_Complain)pcomoplian,max(a.History_PresentComplain) hp,max(a.Examination) ho, " +
        "   max(a.Other_Complain) oc ,max(b.BloodGroup) bg ,max(k.MedicalCategory) medcat ,max(a.OPD_Diagnosis) opddgns , " +
        " max(c.Service_Type) svt, max(c.Sex) sex,max(c.ChildNo ) chno," +
                      "      COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1  " +
        "  and b.Surname != '0' " +
        " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),    " +
        "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
        "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
        "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) sname  , " +
        "  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1      then b.DateOfBirth end), " +
         "  max(case when c.RelationshipType = 2 then c.DateOfBirth  end), " +
        "   max(case when c.RelationshipType = 5    then f.DOB  end)), ''), max(c.DateOfBirth))      dob, " +


        "	max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno, max(b.SNo)  seno " +
        "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
        "	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(a.status)  pstatus,max(a.CreatedDate) crdate, max(h.Relationship) " +

        "     relasiondet FROM[MMS].[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
        "  left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
        "  left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
        "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
        "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
        "   left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
        " where   a.pid='" + item.PID + "' group by a.PDID, a.CreatedDate order by crdate ";
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
                    PID = dataRow.Field<string>("pidp"),
                    PDID = dataRow.Field<string>("pdids"),
                    seno = dataRow.Field<string>("seno"),
                    Initials = dataRow.Field<string>("inililes"),
                    Surname = dataRow.Field<string>("sname"),
                    ServiceNo = dataRow.Field<string>("sno"),
                    Present_Complain = dataRow.Field<string>("pcomoplian"),
                    RNK_NAME = dataRow.Field<string>("rnkname"),
                    History_OtherComplain = dataRow.Field<string>("ho"),
                    History_PresentComplain = dataRow.Field<string>("hp"),
                    Other_Complain = dataRow.Field<string>("oc"),
                    Category = dataRow.Field<string>("medcat"),
                    BloodType = dataRow.Field<string>("bg"),
                    OPD_Diagnosis = dataRow.Field<string>("opddgns"),
                    Relationship = dataRow.Field<string>("relasiondet"),
                    sv = dataRow.Field<int?>("svt"),
                    sex = dataRow.Field<int?>("sex"),
                    chno = dataRow.Field<int?>("chno"),
                    dob = dataRow.Field<DateTime?>("dob"),

                }).ToList();




                        foreach (var iteme in a1)
                        {
                            sertno = iteme.seno;
                            servi = iteme.ServiceNo;
                            if (iteme.Relationship != "SELF")
                            {
                                pdetn = pdetn + iteme.Relationship + " of Service No: " + iteme.ServiceNo + " \n";
                            }
                            else
                            {
                                pdetn = pdetn + "Service No: " + iteme.ServiceNo + "\n";
                            }

                            // pdetn = pdetn + "Service No: " + iteme.ServiceNo + "<br />";
                            pdetn = pdetn + iteme.RNK_NAME + "    " + iteme.Initials + " " + iteme.Surname + "\n";
                            pdetn = pdetn + "Blood Group: " + iteme.BloodType + "\n";
                            if (!string.IsNullOrEmpty(iteme.Category.ToString()))
                            {
                                pdetn = pdetn + "Catagory: " + iteme.Category + "\n";
                            }
                            //var ptmed = from s in db.MedicalStatus.Where(p => p.ServiceNo == iteme.ServiceNo)


                            //            select new { s.ServiceNo, s.ExamYear, s.MedicalName };
                            //if (ptmed.Count() > 0)
                            //{
                            //    medyr = medyr + "PT Test Medical: ";
                            //}
                            //foreach (var itmed in ptmed)
                            //{

                            //    if (!string.IsNullOrEmpty(itmed.ServiceNo) && itmed.ServiceNo != "null")
                            //    {
                            //        medyr = medyr + itmed.ExamYear + " " + itmed.MedicalName + "<br />";
                            //    }


                            //}
                            break;
                        }

                    }

                    var pmdh = from s in db.PastMedHistories.Where(p => p.PID == id)


                               select new { s.PID, s.PMHDetail, s.Drughst };
                    foreach (var itmd in pmdh)
                    {
                        if (!string.IsNullOrEmpty(itmd.Drughst) && itmd.Drughst != "null")
                        {
                            pdetn = pdetn + "Drug History: " + itmd.Drughst + "\n";
                        }
                        if (!string.IsNullOrEmpty(itmd.PMHDetail) && itmd.Drughst != "null")
                        {
                            pdetn = pdetn + "Diagnosed Medical Conditions: " + itmd.PMHDetail + "\n";
                        }



                    }
                    var g5 = from s in db.Vw_MedicalBoard.Where(p => p.SNo == sertno)
                             orderby s.dateOfBoard ascending

                             select new { s.dateOfBoard, s.findingRemarks };
                    pdetn = pdetn + "Medical Board Details  \n ";
                    foreach (var itmg in g5)
                    {

                       
                        pdetn = pdetn + "Date:" + itmg.dateOfBoard.ToString() + "   " + itmg.findingRemarks + "\n";
                         

                    }
                    var aircrewlist = from s in db.VwAirCrews
                                      join b in db.Patients on s.SvcNo equals b.ServiceNo
                                      where b.PID == id
                                      orderby s.acmRID descending
                                      select new { s.SvcNo, s.MES, s.NextDate, s.Remarks, s.WEF, s.acmRID };
                    if (aircrewlist.Count() > 0)
                    {
                        pdetn = pdetn + "  Aircrew medical  \n ";
                        foreach (var itmvt in aircrewlist)
                        {
                            pdetn = pdetn + " MES:  " + itmvt.MES + " WEF: " + itmvt.WEF + " Next Date" + itmvt.NextDate + "\n";
                        }
                    }


                    var hyp = from s in db.Hypersensivities.Where(p => p.PID == id)


                              select new { s.PID, s.HypMainCategory.HypersenceMainCategory, s.HypersenseDetail };
                    if (hyp.Count() > 0)
                    {
                        pdetn = pdetn + "Allergies: ";
                    }

                    foreach (var ithyp in hyp)
                    {

                        if (!string.IsNullOrEmpty(ithyp.HypersenceMainCategory) && ithyp.HypersenceMainCategory != "null")
                        {
                            pdetn = pdetn + ithyp.HypersenceMainCategory + " " + ithyp.HypersenseDetail + "\n";
                        }


                    }
                    pdetn = pdetn + "\n";



                    var a5 = from s in db.Patient_Detail.Where(p => p.PID == id)
                             orderby s.CreatedDate ascending

                             select new { s.PDID, s.CreatedDate };
                    int i = 0;
                    string[] stringArray;
                    stringArray = new string[100];
                    foreach (var itmn in a5)
                    {

                     
                        pdetn = pdetn + "\n";


                        dgnos = "";
                        var dicat = from u in db.CatDiagLists.Where(p => p.PDID == itmn.PDID)
                                    join d in db.CatDaignosis on u.dgid equals d.dgid into com
                                    from d in com.DefaultIfEmpty()
                                    select new
                                    { u.PDID, d.dgdetail };
                        foreach (var itmdi in dicat)
                        {
                            dgnos = dgnos + "," + itmdi.dgdetail;
                        }
                        var a1 = from s in db.Patient_Detail.Where(p => p.PDID == itmn.PDID)

                                 join e in db.CatReferals on s.PDID equals e.PDID into com2
                                 from e in com2.DefaultIfEmpty()
                                 select new { s.PID, s.PDID, s.Patient.Initials, s.Patient.Surname, s.Present_Complain, s.History_OtherComplain, s.History_PresentComplain, s.Other_Complain, s.OPD_Diagnosis, e.PlanofMgt, e.ReffNote, s.ModifiedBy, s.CreatedDate, s.GeneralEntries };
                        foreach (var itm in a1)
                        {
                            modif = itm.ModifiedBy;
                            if (!string.IsNullOrEmpty(itm.CreatedDate.ToString()))
                            {
                                pdetn = pdetn + "Date:" + itm.CreatedDate.ToString("dd/MM/yyyy") + "\n";
                            }
                            if (!string.IsNullOrEmpty(itm.Present_Complain) && itm.Present_Complain != "null")
                            {
                                pdetn = pdetn + "Presenting Complain:" + itm.Present_Complain + "\n";
                            }
                            if (!string.IsNullOrEmpty(itm.History_PresentComplain) && itm.History_PresentComplain != "null")
                            {
                                pdetn = pdetn + " History of Presenting Complain:" + itm.History_PresentComplain + "\n";
                            }
                            if (!string.IsNullOrEmpty(itm.ReffNote) && itm.ReffNote != "null")
                            {
                                pdetn = pdetn + " Reffaral Note:" + itm.ReffNote + "\n";
                            }
                            if (!string.IsNullOrEmpty(itm.PlanofMgt) && itm.PlanofMgt != "null")
                            {
                                pdetn = pdetn + "Notes:" + itm.PlanofMgt + "\n";
                            }
                            if (!string.IsNullOrEmpty(itm.GeneralEntries) && itm.GeneralEntries != "null")
                            {
                                pdetn = pdetn + "General Entries:" + itm.GeneralEntries + "\n";
                            }
                            if (!string.IsNullOrEmpty(itm.OPD_Diagnosis) && itm.OPD_Diagnosis != "null")
                            {
                                pdetn = pdetn + "Diagnosis:" + itm.OPD_Diagnosis + dgnos + "\n";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(dgnos) && dgnos != "null")
                                {
                                    pdetn = pdetn + "Diagnosis:" + dgnos + "\n";
                                }
                            }
                        }
                        int modid = Convert.ToInt32(modif);

                        var vitallist = from s in db.Vitals
                                        where s.PDID == itmn.PDID
                                        select new { s.Vital_Type.VitalType, s.VitalValues, s.Reading_Time };
                        foreach (var itmvt in vitallist)
                        {
                            pdetn = pdetn + itmvt.VitalType + "  " + itmvt.VitalValues + " " + itmvt.Reading_Time + "\n";
                        }




                        var exams = from s in db.ExamineAbdominals
                                    where s.PDID == itmn.PDID
                                    select new { s };
                        var examd = from d in db.ExamineCardiovasculars
                                    where d.PDID == itmn.PDID
                                    select new { d };

                        var exame = from e in db.ExamineCentralNervous
                                    where e.PDID == itmn.PDID
                                    select new { e };
                        var examf = from f in db.ExamineGenerals
                                    where f.PDID == itmn.PDID
                                    select new { f };
                        var examg = from g in db.ExamineRespiratories
                                    where g.PDID == itmn.PDID
                                    select new { g };

                        var examh = from h in db.ExamineOthers
                                    where h.PDID == itmn.PDID
                                    select new { h };

                        foreach (var itmem in exams)
                        {
                            if (itmem.s != null && !string.IsNullOrEmpty(itmem.s.Other) && itmem.s.Other != "null")
                            {
                                pdetn = pdetn + "Abdorminal Examination:" + itmem.s.Other + "\n";
                            }
                        }

                        foreach (var itmem in examd)
                        {

                            if (itmem.d != null && !string.IsNullOrEmpty(itmem.d.Other) && itmem.d.Other != "null")
                            {
                                pdetn = pdetn + "Cardio Vascular Examination:" + itmem.d.Other + "\n";
                            }
                        }

                        foreach (var itmem in exame)
                        {
                            if (itmem.e != null && !string.IsNullOrEmpty(itmem.e.Other) && itmem.e.Other != "null")
                            {
                                pdetn = pdetn + "Central Nervious Examination:" + itmem.e.Other + "\n";
                            }
                        }

                        foreach (var itmem in examf)
                        {
                            if (itmem.f != null && !string.IsNullOrEmpty(itmem.f.Other) && itmem.f.Other != "null")
                            {
                                pdetn = pdetn + "General Examination:" + itmem.f.Other + "\n";
                            }
                        }

                        foreach (var itmem in examg)
                        {
                            if (itmem.g != null && !string.IsNullOrEmpty(itmem.g.Other) && itmem.g.Other != "null")
                            {
                                pdetn = pdetn + "Respiratory Examination:" + itmem.g.Other + "\n";
                            }
                        }

                        foreach (var itmem in examh)
                        {
                            if (itmem.h != null && !string.IsNullOrEmpty(itmem.h.Other) && itmem.h.Other != "null")
                            {
                                pdetn = pdetn + "Other Examination:" + itmem.h.Other + "\n";
                            }
                        }
                        // var lab = from t in db.Lab_Report.Where(p => p.PDID == id)
                        //          select new
                        //          { t.Lab_SubCategory.Lab_MainCategory.CategoryName, t.Lab_SubCategory.Lab_MainCategory.CategoryID, t.PDID };
                        //var labl = lab.GroupBy(c => new { c.CategoryName, c.CategoryID }).Select(grp => grp.FirstOrDefault()).ToList();
                        var lablist = from t in db.Lab_Report.Where(p => p.PDID == itmn.PDID && p.Issued == "1")
                                      join x in db.Lab_SubCategory on t.LabTestID equals x.LabTestID
                                      join y in db.Lab_MainCategory on x.CategoryID equals y.CategoryID
                                     

                                      select new
                                      { y.CategoryName, y.CategoryID, t.PDID, t.TestSID }; ;
                        var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();
                        if (labl.Count > 0)
                        {
                            pdetn = pdetn + "Lab Tests: ";
                        }
                        foreach (var itmlb in labl)
                        {
                            if (itmlb != null && !string.IsNullOrEmpty(itmlb.CategoryName))
                            {
                                pdetn = pdetn +  itmlb.CategoryName + ",";
                            }

                        }
                        pdetn = pdetn + "\n";
                        var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                        var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
                        var serc = from s in db.Drug_Prescription.Where(p => p.PDID == itmn.PDID && p.issuedQuantity != "0") orderby s.ItemNo select new { s.Ps_Index, s.ItemNo, s.DrugMethod.MethodDetail, s.DrugMethod.DrugMethodCount, s.DrugRoute.RouteDetail, s.Duration, s.Dose, s.PDID, s.MethodType };
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
                        if (joined3.ToList().Count > 0)
                        {
                            pdetn = pdetn + "Prescription:\n";
                        }
                        foreach (var itmdr in joined3)
                        {
                            if (itmdr != null && !string.IsNullOrEmpty(itmdr.itemdescription))
                            {
                                pdetn = pdetn + itmdr.itemdescription + "  " + itmdr.MethodDetail + "  " + itmdr.RouteDetail + "   Duration:" + itmdr.Duration + " Days \n";
                            }
                        }
                        var siccat = from u in db.Sick_Category.Where(p => p.PDID == itmn.PDID)
                                     join b in db.Sick_CategoryType on u.CatID equals b.CatID
                                     select new
                                     { u.CatPeriod, b.Category_Type, u.Date };

                        foreach (var itmsc in siccat)
                        {
                            pdetn = pdetn + itmsc.Category_Type + "  Duration:" + itmsc.CatPeriod + "   Effective Date:" + itmsc.Date.ToString() + "\n";
                        }

                        var docn = from u in db.Users.Where(p => p.UserID == modid)
                                   select new
                                   { u.UserName, u.Salutation, u.FName, u.LName };

                        foreach (var itmdc in docn)
                        {
                            pdetn = pdetn + "Investigated by:" + itmdc.Salutation + " " + itmdc.FName + " " + itmdc.LName + "\n";
                        }


                        int dfgl = pdetn.Split('>').Length;
                        int dfgl2 = pdetn.Split('/').Length;
                        int totv = (dfgl2 - dfgl) * 4 + dfgl;

                        int slen = pdetn.Length;
                      //  if (slen > 300)
                      //  {
                           // pdetn = " <div  id=a" + io + " >" + pdetn + " </div>";
                            pdetn2 = pdetn2 + pdetn;
                        pdetn = "";
                          //  pdetn = "";
                          //  io++;
                          //  }
                          //else
                          // {
                          //     pdetn = pdetn + " \n";

                        //  }
                        //  var pdet = string.Join(",", a1);

                        // return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    if (Relationship == "SELF")
                    {
                        pdetn = pdetn + medyr;
                    }

                    //pdetn2 = pdetn2 + " <div id=a" + io + ">" + pdetn + " </div>";
                    var result = new { h1 = pdetn2 };
                    CreateDocument(servi, pdetn2);
                    return Json(new { h1 = pdetn2 }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }

        private void CreateDocument(string sno,string txt)
        {
            try
            {
                //Create an instance for word app  
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application  
                winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.  
                winword.Visible = false;

                //Create a missing variable for missing value  
                object missing = System.Reflection.Missing.Value;

                //Create a new document  
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

               

               

                //adding text to document  
                document.Content.SetRange(0, 0);
                document.Content.Text = txt + Environment.NewLine;

             

                //Save the document  
                object filename = @"d:\unfolder\"+ sno + ".docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);
                winword = null;
               
            }
            catch (Exception ex)
            {
               
            }
        }

        public JsonResult PatientHystorynewb(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            String modif = "";
            String pdetn = "";
            String medyr = "";
            String pdetn2 = "";
            String dgnos = "";
            string iserror = "";
            string svcid = "";
            string pid = "";
            string PDID = "";
            string retype = "";
            string Category = "";
            string BloodType = "";
            string rnk = "";
            string Relationship = "";
            int io = 1;
            try
            {
                    if (id != null)
                    {
                    id = id.Trim(MyChar);
                    DataTable oDataSet1 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = " select serviceno from [dbo].[Patient] with (nolock) where pid='" + id + "'";
                        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlConnection.Open();
                        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                        oSqlDataAdapter.Fill(oDataSet1);
                        oSqlConnection.Close();
                        var opd = oDataSet1.AsEnumerable()
                .Select(dataRow => new Patient
                {
                    ServiceNo = dataRow.Field<string>("ServiceNo")


                }).ToList();



                        foreach (var item in opd)
                        {
                            svcid = item.ServiceNo;

                        }



                        String locid = (String)Session["userloc"];

                        if (!locid.ToLower().Equals("cbo") && ((svcid == "01496") || (svcid == "01557") || (svcid == "01443") || (svcid == "01503") || (svcid == "01560") || (svcid == "01668") || (svcid == "01641") || (svcid == "01644") || (svcid == "01829") || (svcid == "01877") || (svcid == "01642") || (svcid == "01503")))
                        {
                        return Json(JsonRequestBehavior.AllowGet);
                    }
                        else
                        {




                          

                            DataSet dst1 = new DataSet();
                            oSqlConnection = new SqlConnection(conStr);

                            SqlCommand command = oSqlConnection.CreateCommand();
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "gethistory";
                            command.Parameters.AddWithValue("@pid", id);
                            command.CommandTimeout = 20000;
                            oSqlDataAdapter = new SqlDataAdapter(command);
                            oSqlDataAdapter.Fill(dst1);



                            foreach (DataRow row in dst1.Tables[15].Rows)
                            {

                                pid = row["PID"].ToString();
                                PDID = row["PDID"].ToString();

                                Category = row["MedicalCategory"].ToString();
                                BloodType = row["BloodGroup"].ToString();

                                Relationship = row["Relationship"].ToString();
                                int? stype = 0;
                                string svcno = row["ServiceNo"].ToString();

                                rnk = row["rnkname"].ToString();




                                sertno = row["sno"].ToString();
                                if (Relationship != "SELF")
                                {
                                    pdetn = pdetn + Relationship + " of Service No: " + svcno + "<br />";
                                }
                                else
                                {
                                    pdetn = pdetn + "Service No: " + svcno + "<br />";
                                }


                                pdetn = pdetn + rnk + "    " + row["inililes"].ToString() + " " + row["sname"].ToString() + "<br />";
                                pdetn = pdetn + "Blood Group: " + BloodType + "<br />";
                                if (!string.IsNullOrEmpty(Category.ToString()))
                                {
                                    pdetn = pdetn + "Catagory: " + Category + "<br />";
                                }

                                break;


                            }


                            foreach (DataRow row in dst1.Tables[0].Rows)
                            {
                                if (!string.IsNullOrEmpty(row["Drughst"].ToString()) && row["Drughst"].ToString() != "null")
                                {
                                    pdetn = pdetn + "Drug History: " + row["Drughst"].ToString() + "<br />";
                                }
                                if (!string.IsNullOrEmpty(row["PMHDetail"].ToString()) && row["PMHDetail"].ToString() != "null")
                                {
                                    pdetn = pdetn + "Diagnosed Medical Conditions: " + row["PMHDetail"].ToString() + "<br />";
                                }



                            }




                            int alg11 = 0;
                            foreach (DataRow row in dst1.Tables[2].Rows)
                            {
                                if (alg11 == 0)
                                {
                                    pdetn = pdetn + "Allergies: ";
                                    alg11 = 1;
                                }

                                if (!string.IsNullOrEmpty(row["HypersenceMainCategory"].ToString()) && row["HypersenceMainCategory"].ToString() != "null")
                                {
                                    pdetn = pdetn + row["HypersenceMainCategory"].ToString() + " " + row["HypersenseDetail"].ToString() + "<br />";
                                }


                            }




                            pdetn = pdetn + "<br />";



                            string mbddt = "";
                            int i = 0;
                            int ig = 0;
                            int ig1 = 0;
                            int acm11 = 0;
                            int ir = 1;
                            string[] stringArray;
                            stringArray = new string[10000];
                            foreach (DataRow row in dst1.Tables[15].Rows)
                            {
                                pdetn = pdetn + "";
                                DateTime enteredDate = new DateTime();

                                try
                                {
                                    enteredDate = DateTime.Parse(mbddt, CultureInfo.InvariantCulture);
                                }
                                catch (Exception ex)
                                {

                                }
                                if (Relationship == "SELF")
                                {
                                    foreach (DataRow row1 in dst1.Tables[3].Rows)
                                    {
                                        if (enteredDate <= DateTime.Parse(row1["DateOfBoard"].ToString(), CultureInfo.InvariantCulture) && DateTime.Parse(row1["DateOfBoard"].ToString(), CultureInfo.InvariantCulture) <= DateTime.Parse(row["CreatedDate"].ToString(), CultureInfo.InvariantCulture))

                                        {
                                            pdetn = pdetn + "<div style=color:red>" + row1["findingRemarks"].ToString() + "  Catagory:" + row1["current_MedCat"].ToString() + ",   Effective Date:" + DateTime.Parse((row1["wefDate"]).ToString()).ToString("dd/MM/yyyy") + ",   Date of Board:" + DateTime.Parse((row1["dateOfBoard"]).ToString()).ToString("dd/MM/yyyy") + " Next Date:" + DateTime.Parse((row1["NextMB"]).ToString()).ToString("dd / MM / yyyy") + "</div><br />";
                                            ig++;
                                        }
                                        else if (DateTime.Parse(row1["DateOfBoard"].ToString(), CultureInfo.InvariantCulture) > DateTime.Parse(row["CreatedDate"].ToString(), CultureInfo.InvariantCulture) && dst1.Tables[15].Rows.Count == ir)
                                        {
                                            pdetn = pdetn + "<div style=color:red>" + row1["findingRemarks"].ToString() + "  Catagory:" + row1["current_MedCat"].ToString() + ",   Effective Date:" + DateTime.Parse((row1["wefDate"]).ToString()).ToString("dd/MM/yyyy") + ",   Date of Board:" + DateTime.Parse((row1["dateOfBoard"]).ToString()).ToString("dd/MM/yyyy") + " Next Date:" + DateTime.Parse((row1["NextMB"]).ToString()).ToString("dd / MM / yyyy") + "</div><br />";

                                        }


                                    }
                                }
                                ////////////////////////////////////////////////
                                ///

                                foreach (DataRow row1 in dst1.Tables[16].Rows)
                                {
                                    try
                                    {
                                        if (enteredDate <= DateTime.Parse(row1["dateadmit"].ToString(), CultureInfo.InvariantCulture) && DateTime.Parse(row1["dateadmit"].ToString(), CultureInfo.InvariantCulture) <= DateTime.Parse(row["CreatedDate"].ToString(), CultureInfo.InvariantCulture))

                                        {

                                            if (!string.IsNullOrEmpty(row1["dateadmit"].ToString()))
                                            {
                                                pdetn = pdetn + "Ward Admision Date:" + DateTime.Parse((row1["dateadmit"]).ToString()).ToString("dd/MM/yyyy") + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["datedischarge"].ToString()))
                                            {
                                                pdetn = pdetn + "Discharge Date:" + DateTime.Parse((row1["datedischarge"]).ToString()).ToString("dd/MM/yyyy") + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["diagnosis"].ToString()))
                                            {
                                                pdetn = pdetn + "Diagnosis:" + (row1["diagnosis"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["presentingcomp"].ToString()))
                                            {
                                                pdetn = pdetn + "Presenting Complain:" + (row1["presentingcomp"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["hispcomp"].ToString()))
                                            {
                                                pdetn = pdetn + "History of Presenting Complain:" + (row1["hispcomp"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["pastsurghis"].ToString()))
                                            {
                                                pdetn = pdetn + "Past Surgeries:" + (row1["pastsurghis"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["pastmedhis"].ToString()))
                                            {
                                                pdetn = pdetn + "Past Medical History:" + (row1["pastmedhis"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["alergies"].ToString()))
                                            {
                                                pdetn = pdetn + "Allergies:" + (row1["alergies"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["manageinhosp"].ToString()))
                                            {
                                                pdetn = pdetn + "Management in Hospital:" + (row1["manageinhosp"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["dischargeins"].ToString()))
                                            {
                                                pdetn = pdetn + "Discharge Instructions:" + (row1["dischargeins"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["followupins"].ToString()))
                                            {
                                                pdetn = pdetn + "Folowup Instructions:" + (row1["followupins"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["ConsultantName"].ToString()))
                                            {
                                                pdetn = pdetn + "Consultant Name:" + (row1["ConsultantName"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["SickCategory"].ToString()))
                                            {
                                                pdetn = pdetn + "Sick Category:" + (row1["SickCategory"]).ToString() + "<br />";
                                            }
                                        pdetn = pdetn + "<a target=\"_blank\" href='../SurgeryMasters/View2?pdid=" + row1["PDID"] + "' >Diagnosis Card</a><br />";

                                        ig1++;
                                        }
                                        else if (DateTime.Parse(row1["dateadmit"].ToString(), CultureInfo.InvariantCulture) > DateTime.Parse(row["CreatedDate"].ToString(), CultureInfo.InvariantCulture) && dst1.Tables[15].Rows.Count == ir)
                                        {


                                            if (!string.IsNullOrEmpty(row1["dateadmit"].ToString()))
                                            {
                                                pdetn = pdetn + "Admition Date:" + DateTime.Parse((row1["dateadmit"]).ToString()).ToString("dd/MM/yyyy") + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["datedischarge"].ToString()))
                                            {
                                                pdetn = pdetn + "Discharge Date:" + DateTime.Parse((row1["datedischarge"]).ToString()).ToString("dd/MM/yyyy") + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["diagnosis"].ToString()))
                                            {
                                                pdetn = pdetn + "Diagnosis:" + (row1["diagnosis"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["presentingcomp"].ToString()))
                                            {
                                                pdetn = pdetn + "Presenting Complain:" + (row1["presentingcomp"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["hispcomp"].ToString()))
                                            {
                                                pdetn = pdetn + "History of Presenting Complain:" + (row1["hispcomp"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["pastsurghis"].ToString()))
                                            {
                                                pdetn = pdetn + "Past Surgeries:" + (row1["pastsurghis"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["pastmedhis"].ToString()))
                                            {
                                                pdetn = pdetn + "Past Medical History:" + (row1["pastmedhis"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["alergies"].ToString()))
                                            {
                                                pdetn = pdetn + "Allergies:" + (row1["alergies"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["manageinhosp"].ToString()))
                                            {
                                                pdetn = pdetn + "Management in Hospital:" + (row1["manageinhosp"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["dischargeins"].ToString()))
                                            {
                                                pdetn = pdetn + "Discharge Instructions:" + (row1["dischargeins"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["followupins"].ToString()))
                                            {
                                                pdetn = pdetn + "Folowup Instructions:" + (row1["followupins"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["ConsultantName"].ToString()))
                                            {
                                                pdetn = pdetn + "Consultant Name:" + (row1["ConsultantName"]).ToString() + "<br />";
                                            }
                                            if (!string.IsNullOrEmpty(row1["SickCategory"].ToString()))
                                            {
                                                pdetn = pdetn + "Sick Category:" + (row1["SickCategory"]).ToString() + "<br />";
                                            }
                                        pdetn = pdetn + "<a target=\"_blank\" href='../SurgeryMasters/View2?pdid=" + row1["PDID"] + "' >Diagnosis Card</a><br />";

                                    }


                                }
                                    catch (Exception ex)
                                    {

                                    }

                                }
                                //foreach (DataRow row1 in dst1.Tables[17].Rows)
                                //{
                                //    try
                                //    {
                                //        if (enteredDate <= DateTime.Parse(row1["dateadmit"].ToString(), CultureInfo.InvariantCulture) && DateTime.Parse(row1["dateadmit"].ToString(), CultureInfo.InvariantCulture) <= DateTime.Parse(row["CreatedDate"].ToString(), CultureInfo.InvariantCulture))

                                //        {

                                //            if (!string.IsNullOrEmpty(row1["DrugName"].ToString()))
                                //            {
                                //                pdetn = pdetn + " " + DateTime.Parse((row1["DrugName"]).ToString()).ToString("dd/MM/yyyy") + "<br />";
                                //            }
                                //            if (!string.IsNullOrEmpty(row1["method"].ToString()))
                                //            {
                                //                pdetn = pdetn + " " + DateTime.Parse((row1["method"]).ToString()).ToString("dd/MM/yyyy") + "<br />";
                                //            }
                                //            if (!string.IsNullOrEmpty(row1["route"].ToString()))
                                //            {
                                //                pdetn = pdetn + " " + (row1["route"]).ToString() + "<br />";
                                //            }
                                //            if (!string.IsNullOrEmpty(row1["dose"].ToString()))
                                //            {
                                //                pdetn = pdetn + " " + (row1["dose"]).ToString() + "<br />";
                                //            }
                                //            if (!string.IsNullOrEmpty(row1["duration"].ToString()))
                                //            {
                                //                pdetn = pdetn + " " + (row1["duration"]).ToString() + "<br />";
                                //            }

                                //            ig1++;
                                //        }
                                //        else if (DateTime.Parse(row1["dateadmit"].ToString(), CultureInfo.InvariantCulture) > DateTime.Parse(row["CreatedDate"].ToString(), CultureInfo.InvariantCulture) && dst1.Tables[15].Rows.Count == ir)
                                //        {


                                //            if (!string.IsNullOrEmpty(row1["DrugName"].ToString()))
                                //            {
                                //                pdetn = pdetn + " " + DateTime.Parse((row1["DrugName"]).ToString()).ToString("dd/MM/yyyy") + "<br />";
                                //            }
                                //            if (!string.IsNullOrEmpty(row1["method"].ToString()))
                                //            {
                                //                pdetn = pdetn + " " + DateTime.Parse((row1["method"]).ToString()).ToString("dd/MM/yyyy") + "<br />";
                                //            }
                                //            if (!string.IsNullOrEmpty(row1["route"].ToString()))
                                //            {
                                //                pdetn = pdetn + " " + (row1["route"]).ToString() + "<br />";
                                //            }
                                //            if (!string.IsNullOrEmpty(row1["dose"].ToString()))
                                //            {
                                //                pdetn = pdetn + " " + (row1["dose"]).ToString() + "<br />";
                                //            }
                                //            if (!string.IsNullOrEmpty(row1["duration"].ToString()))
                                //            {
                                //                pdetn = pdetn + " " + (row1["duration"]).ToString() + "<br />";
                                //            }
                                //        }
                                //    }
                                //    catch (Exception ex)
                                //    {

                                //    }

                                //}
                                ///////////////////////////////////////////////
                                mbddt = row["CreatedDate"].ToString();

                                pdetn = pdetn + "";

                                if (Relationship == "SELF")
                                {
                                    foreach (DataRow row1 in dst1.Tables[1].Rows)
                                    {

                                        if (enteredDate <= DateTime.Parse(row1["WEF"].ToString(), CultureInfo.InvariantCulture) && DateTime.Parse(row1["WEF"].ToString(), CultureInfo.InvariantCulture) <= DateTime.Parse(row["CreatedDate"].ToString(), CultureInfo.InvariantCulture))

                                        {
                                            pdetn = pdetn + "<div style=color:blue> Aircrew medical  MES:  " + row1["MES"].ToString() + " WEF: " + DateTime.Parse((row1["WEF"]).ToString()).ToString("dd/MM/yyyy") + " Next Date" + DateTime.Parse((row1["NextDate"]).ToString()).ToString("dd/MM/yyyy") + "</div><br />";
                                            acm11++;
                                        }
                                        else if (DateTime.Parse(row1["WEF"].ToString(), CultureInfo.InvariantCulture) > DateTime.Parse(row["CreatedDate"].ToString(), CultureInfo.InvariantCulture) && dst1.Tables[15].Rows.Count == ir)
                                        {
                                            pdetn = pdetn + "<div style=color:blue> Aircrew medical MES:  " + row1["MES"].ToString() + " WEF: " + DateTime.Parse((row1["WEF"]).ToString()).ToString("dd/MM/yyyy") + " Next Date" + DateTime.Parse((row1["NextDate"]).ToString()).ToString("dd/MM/yyyy") + "</div><br />";

                                        }



                                    }
                                }
                                pdetn = pdetn + "";




                                dgnos = "";
                                if (!string.IsNullOrEmpty(row["Diagnosis"].ToString()) && row["Diagnosis"].ToString().ToLower() != "null")
                                {
                                    dgnos = dgnos + row["Diagnosis"].ToString();
                                }
                                modif = "";
                                if (!string.IsNullOrEmpty(row["CreatedDate"].ToString()))
                                {
                                    pdetn = pdetn + "Date:" + DateTime.Parse((row["CreatedDate"]).ToString()).ToString("dd/MM/yyyy") + "<br />";
                                }
                                if (!string.IsNullOrEmpty(row["Present_Complain"].ToString()) && row["Present_Complain"].ToString() != "null")
                                {
                                    pdetn = pdetn + "Presenting Complain:" + row["Present_Complain"].ToString() + "<br />";
                                }
                                if (!string.IsNullOrEmpty(row["History_PresentComplain"].ToString()) && row["History_PresentComplain"].ToString() != "null")
                                {
                                    pdetn = pdetn + " History of Presenting Complain:" + row["History_PresentComplain"].ToString() + "<br />";
                                }
                                if (!string.IsNullOrEmpty(row["refn"].ToString()) && row["refn"].ToString() != "null")
                                {
                                    pdetn = pdetn + " Reffaral Note:" + row["refn"].ToString() + "<br />";
                                }
                                if (!string.IsNullOrEmpty(row["planofm"].ToString()) && row["planofm"].ToString() != "null")
                                {
                                    pdetn = pdetn + "Notes:" + row["planofm"].ToString() + "<br />";
                                }
                                if (!string.IsNullOrEmpty(row["GeneralEntries"].ToString()) && row["GeneralEntries"].ToString() != "null")
                                {
                                    pdetn = pdetn + "General Entries:" + row["GeneralEntries"].ToString() + "<br />";
                                }
                                if (!string.IsNullOrEmpty(row["OPD_Diagnosis"].ToString()) && row["OPD_Diagnosis"].ToString() != "null")
                                {
                                    pdetn = pdetn + "Diagnosis:" + row["OPD_Diagnosis"].ToString() + dgnos + "<br />";
                                }

                                //else
                                //{
                                //    if (!string.IsNullOrEmpty(dgnos) && dgnos != "null")
                                //    {
                                //        pdetn = pdetn + "Diagnosis:" + dgnos + "<br />";
                                //    }
                                //}

                                //  int modid = Convert.ToInt32(modif);


                                foreach (DataRow row1 in dst1.Tables[4].Rows)
                                {
                                    if (row1["PDID"].ToString().Equals(row["PDID"].ToString()))
                                    {
                                        pdetn = pdetn + row1["VitalType"].ToString() + "  " + row1["VitalValues"].ToString() + " " + DateTime.Parse((row1["Reading_Time"]).ToString()).ToString("dd/MM/yyyy") + "<br />";
                                    }
                                }






                                foreach (DataRow row1 in dst1.Tables[5].Rows)
                                {
                                    if (row1["PDID"].ToString().Equals(row["PDID"].ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(row1["Other"].ToString()) && row1["Other"].ToString() != "null")
                                        {
                                            pdetn = pdetn + "Abdorminal Examination:" + row1["Other"].ToString() + "<br />";
                                        }
                                    }
                                }

                                foreach (DataRow row1 in dst1.Tables[6].Rows)
                                {

                                    if (row1["PDID"].ToString().Equals(row["PDID"].ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(row1["Other"].ToString()) && row1["Other"].ToString() != "null")
                                        {
                                            pdetn = pdetn + "Cardio Vascular Examination:" + row1["Other"].ToString() + "<br />";
                                        }
                                    }
                                }

                                foreach (DataRow row1 in dst1.Tables[7].Rows)
                                {
                                    if (row1["PDID"].ToString().Equals(row["PDID"].ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(row1["Other"].ToString()) && row1["Other"].ToString() != "null")
                                        {
                                            pdetn = pdetn + "Central Nervious Examination:" + row1["Other"].ToString() + "<br />";
                                        }
                                    }
                                }

                                foreach (DataRow row1 in dst1.Tables[8].Rows)
                                {
                                    if (row1["PDID"].ToString().Equals(row["PDID"].ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(row1["Other"].ToString()) && row1["Other"].ToString() != "null")
                                        {
                                            pdetn = pdetn + "General Examination:" + row1["Other"].ToString() + "<br />";
                                        }
                                    }
                                }

                                foreach (DataRow row1 in dst1.Tables[10].Rows)
                                {
                                    if (row1["PDID"].ToString().Equals(row["PDID"].ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(row1["Other"].ToString()) && row1["Other"].ToString() != "null")
                                        {
                                            pdetn = pdetn + "Respiratory Examination:" + row1["Other"].ToString() + "<br />";
                                        }
                                    }
                                }

                                foreach (DataRow row1 in dst1.Tables[9].Rows)
                                {
                                    if (row1["PDID"].ToString().Equals(row["PDID"].ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(row1["Other"].ToString()) && row1["Other"].ToString() != "null")
                                        {
                                            pdetn = pdetn + "Other Examination:" + row1["Other"].ToString() + "<br />";
                                        }
                                    }
                                }
                                int l11 = 0;
                                foreach (DataRow row1 in dst1.Tables[11].Rows)
                                {
                                    if (row1["PDID"].ToString().Equals(row["PDID"].ToString()))
                                    {
                                        if (l11 == 0)
                                        {
                                            pdetn = pdetn + "Lab Tests: ";
                                            l11 = 1;
                                        }
                                        if (row1["CategoryName"].ToString() != null && !string.IsNullOrEmpty(row1["CategoryName"].ToString()))
                                        {
                                            pdetn = pdetn + "<a href='#contact3' onclick=\"vwlb('" + row1["TestSID"].ToString() + "','" + row1["TubeCategory"].ToString() + "');\">" + row1["CategoryName"].ToString() + "</a>,";
                                        }
                                    }
                                }
                                pdetn = pdetn + "";

                                int d11 = 0;
                                foreach (DataRow row1 in dst1.Tables[12].Rows)
                                {
                                    if (row1["PDID"].ToString().Equals(row["PDID"].ToString()))
                                    {
                                        if (d11 == 0)
                                        {
                                            pdetn = pdetn + "<br />Prescription:<br />";
                                            d11 = 1;
                                        }
                                        if (row1["Expr1"].ToString() != null && !string.IsNullOrEmpty(row1["Expr1"].ToString()))
                                        {
                                            pdetn = pdetn + row1["Expr1"].ToString() + "  " + row1["MethodDetail"].ToString() + "  " + row1["RouteDetail"].ToString() + "   Duration:" + row1["Duration"].ToString() + " Days <br />";
                                        }
                                    }
                                }
                                pdetn = pdetn + "";
                                int s11 = 0;
                                foreach (DataRow row1 in dst1.Tables[13].Rows)
                                {
                                    if (row1["PDID"].ToString().Equals(row["PDID"].ToString()))
                                    {
                                        if (s11 == 0)
                                        {
                                            pdetn = pdetn + "Category:<br />";
                                            s11 = 1;
                                        }
                                        pdetn = pdetn + row1["Category_Type"].ToString() + "  Duration:" + row1["CatPeriod"].ToString() + "   Effective Date:" + DateTime.Parse((row1["Date"]).ToString()).ToString("dd/MM/yyyy") + "<br />";
                                    }
                                }


                                pdetn = pdetn + "";
                                foreach (DataRow row1 in dst1.Tables[14].Rows)
                                {
                                    if (row1["UserID"].ToString().Equals(row["modid"].ToString()))
                                    {
                                        pdetn = pdetn + "Investigated by:" + row1["Salutation"].ToString() + " " + row1["FName"].ToString() + " " + row1["LName"].ToString() + "<br />";
                                    }
                                }
                                pdetn = pdetn + "";

                                try
                                {
                                    int dfgl = pdetn.Split('>').Length;
                                    int dfgl2 = pdetn.Split('/').Length;
                                    int totv = (dfgl2 - dfgl) * 4 + dfgl;

                                    int slen = pdetn.Trim().Count(char.IsLetter);
                                    if (slen > 300)
                                    {
                                        pdetn = " <div  id=a" + io + " >" + pdetn + " </div>";
                                        pdetn2 = pdetn2.Trim() + pdetn.Trim();
                                        pdetn = "";
                                        io++;
                                    }
                                    else
                                    {
                                        pdetn = pdetn + " <br/>";

                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                                //  var pdet = string.Join(",", a1);


                                ir++;
                                // return Json(result, JsonRequestBehavior.AllowGet);
                            }
                            if (Relationship == "SELF")
                            {
                                pdetn = pdetn + medyr;
                            }

                            pdetn2 = pdetn2 + " <div id=a" + io + ">" + pdetn + " </div>";
                            var result = new { h1 = pdetn2.Trim() };

                            return Json(new { h1 = pdetn2.Trim() }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(JsonRequestBehavior.AllowGet);
                    }
           
        }
            catch (Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PatientHystory(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            String modif = "";
            String pdetn = "";
            String medyr = "";
            String pdetn2 = "";
            String dgnos = "";
            string iserror = "";
            string pid = "";
            string PDID = "";
            string retype = "";
            string Category = "";
            string BloodType = "";
        
            string Relationship = "";
            int io = 1;
            try {
                if (id != null)
                {
                    
                    id = id.Trim(MyChar);

                    //DataTable oDataSetsp10 = new DataTable();
                    //oSqlConnection = new SqlConnection(conStr);
                    //if (oSqlConnection.State == ConnectionState.Closed)
                    //{
                    //    oSqlConnection.Open();
                    //}
                    //oSqlCommand = new SqlCommand();
                    //sqlQuery = "    select * FROM [MMS].[dbo].[Drug_Regular] where ItemNo='" + p.dItemno + "' AND PDID='" + pid + "' ";
                    //// sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    //oSqlCommand.Connection = oSqlConnection;
                    //oSqlCommand.CommandText = sqlQuery;
                    ////  oSqlConnection.Open();
                    //oSqlCommand.CommandTimeout = 120;
                    //oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    //oSqlDataAdapter.Fill(oDataSetsp10);



                    var ax1 = from s in db.Patients.Where(p => p.PID == id)
                              join x in db.PersonalDetails on s.ServiceNo equals x.ServiceNo
                              join y in db.MedicalCategories on x.SNo equals y.SNo
                              join z in db.RelationshipTypes on s.RelationshipType equals z.RTypeID

                              select new getdocdata { PID = s.PID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = s.rank1.RNK_NAME,Category = y.MedicalCategory1, BloodType = x.BloodGroup,  Relationship = z.Relationship, sv = s.Service_Type };


                    foreach (var item in ax1)
                    {
                        pid = item.PID;
                        PDID = item.PDID;

                        Category = item.Category;
                        BloodType = item.BloodType;

                        Relationship = item.Relationship;
                        int? stype = 0;
                        string svcno = "";

                        DataTable oDataSet3 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "   SELECT max(a.Present_Complain)pcomoplian,max(a.History_PresentComplain) hp,max(a.Examination) ho, " +
        "   max(a.Other_Complain) oc ,max(b.BloodGroup) bg ,max(k.MedicalCategory) medcat ,max(a.OPD_Diagnosis) opddgns , " +
        " max(c.Service_Type) svt, max(c.Sex) sex,max(c.ChildNo ) chno," +
                      "      COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1  " +
        "  and b.Surname != '0' " +
        " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),    " +
        "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
        "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
        "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) sname  , " +
        "  COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1      then b.DateOfBirth end), " +
         "  max(case when c.RelationshipType = 2 then c.DateOfBirth  end), " +
        "   max(case when c.RelationshipType = 5    then f.DOB  end)), ''), max(c.DateOfBirth))      dob, " +


        "	max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno, max(b.SNo)  seno " +
        "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
        "	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(a.status)  pstatus,max(a.CreatedDate) crdate, max(h.Relationship) " +

        "     relasiondet FROM[MMS].[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
        "  left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
        "  left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
        "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
        "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
        "   left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
        " where   a.pid='" + item.PID + "' group by a.PDID, a.CreatedDate order by crdate ";
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
                    PID = dataRow.Field<string>("pidp"),
                    PDID = dataRow.Field<string>("pdids"),
                    seno = dataRow.Field<string>("seno"),
                    Initials = dataRow.Field<string>("inililes"),
                    Surname = dataRow.Field<string>("sname"),
                    ServiceNo = dataRow.Field<string>("sno"),
                    Present_Complain = dataRow.Field<string>("pcomoplian"),
                    RNK_NAME = dataRow.Field<string>("rnkname"),
                    History_OtherComplain = dataRow.Field<string>("ho"),
                    History_PresentComplain = dataRow.Field<string>("hp"),
                    Other_Complain = dataRow.Field<string>("oc"),
                    Category = dataRow.Field<string>("medcat"),
                    BloodType = dataRow.Field<string>("bg"),
                    OPD_Diagnosis = dataRow.Field<string>("opddgns"),
                    Relationship = dataRow.Field<string>("relasiondet"),
                    sv = dataRow.Field<int?>("svt"),
                    sex = dataRow.Field<int?>("sex"),
                    chno = dataRow.Field<int?>("chno"),
                    dob = dataRow.Field<DateTime?>("dob"),

                }).ToList();



                      
                        foreach (var iteme in a1)
                        {
                            sertno = iteme.seno;
                            if (iteme.Relationship != "SELF")
                            {
                                pdetn = pdetn + iteme.Relationship + " of Service No: " + iteme.ServiceNo + "<br />";
                            }
                            else
                            {
                                pdetn = pdetn + "Service No: " + iteme.ServiceNo + "<br />";
                            }

                            // pdetn = pdetn + "Service No: " + iteme.ServiceNo + "<br />";
                            pdetn = pdetn + iteme.RNK_NAME + "    " + iteme.Initials + " " + iteme.Surname + "<br />";
                            pdetn = pdetn + "Blood Group: " + iteme.BloodType + "<br />";
                            if (!string.IsNullOrEmpty(iteme.Category.ToString()))
                            {
                                pdetn = pdetn + "Catagory: " + iteme.Category + "<br />";
                            }
                            //var ptmed = from s in db.MedicalStatus.Where(p => p.ServiceNo == iteme.ServiceNo)


                            //            select new { s.ServiceNo, s.ExamYear, s.MedicalName };
                            //if (ptmed.Count() > 0)
                            //{
                            //    medyr = medyr + "PT Test Medical: ";
                            //}
                            //foreach (var itmed in ptmed)
                            //{

                            //    if (!string.IsNullOrEmpty(itmed.ServiceNo) && itmed.ServiceNo != "null")
                            //    {
                            //        medyr = medyr + itmed.ExamYear + " " + itmed.MedicalName + "<br />";
                            //    }


                            //}
                            break;
                        }

                    }
                       
                        var pmdh = from s in db.PastMedHistories.Where(p => p.PID == id)
                                 

                                 select new { s.PID, s.PMHDetail,s.Drughst };
                        foreach (var itmd in pmdh)
                        {
                            if (!string.IsNullOrEmpty(itmd.Drughst)&& itmd.Drughst!="null")
                            {
                                pdetn = pdetn + "Drug History: " + itmd.Drughst + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itmd.PMHDetail) && itmd.Drughst != "null")
                            {
                                pdetn = pdetn + "Diagnosed Medical Conditions: " + itmd.PMHDetail + "<br />";
                            }

                        

                    }
                    var aircrewlist = from s in db.VwAirCrews
                                      join b in db.Patients on s.SvcNo equals b.ServiceNo
                                      where b.PID == id orderby s.acmRID descending
                                      select new { s.SvcNo, s.MES, s.NextDate,s.Remarks,s.WEF,s.acmRID };
                    if (aircrewlist.Count()>0) {
                        pdetn = pdetn + "  Aircrew medical  <br/> ";
                        foreach (var itmvt in aircrewlist)
                        {
                            pdetn = pdetn + " MES:  " + itmvt.MES + " WEF: " + itmvt.WEF + " Next Date" + itmvt.NextDate + "<br />";
                        }
                    }


                    var hyp = from s in db.Hypersensivities.Where(p => p.PID == id)


                               select new { s.PID, s.HypMainCategory.HypersenceMainCategory, s.HypersenseDetail };
                   if(hyp.Count() > 0)
                    {
                        pdetn = pdetn + "Allergies: ";
                    }
                  
                    foreach (var ithyp in hyp)
                    {
                       
                        if (!string.IsNullOrEmpty(ithyp.HypersenceMainCategory) && ithyp.HypersenceMainCategory != "null")
                        {
                            pdetn = pdetn  + ithyp.HypersenceMainCategory +" "+ ithyp.HypersenseDetail + "<br />";
                        }
                       

                    }

                    var g5 = from s in db.Vw_MedicalBoard.Where(p => p.SNo == sertno)
                             orderby s.dateOfBoard ascending

                             select new { s.dateOfBoard, s.findingRemarks };
                    foreach (var itmg in g5)
                    {
                        //if (itmn.CreatedDate > itmg.dateOfBoard)
                        //{

                        //    string stringToCheck = itmg.dateOfBoard + "" + itmg.findingRemarks;


                        //    if (i == 0)
                        //    {
                        //        stringArray[i] = stringToCheck;
                        //        pdetn = pdetn + "Date:" + itmg.dateOfBoard.ToString() + "   " + itmg.findingRemarks + "<br />";
                        //    }


                        //    foreach (string x in stringArray)
                        //    {
                        //        if (!string.IsNullOrEmpty(x))
                        //        {

                        //            if (!stringToCheck.Contains(x))
                        //            {
                        //                stringArray[i] = stringToCheck;
                        //                pdetn = pdetn + "Date:" + itmg.dateOfBoard.ToString() + "   " + itmg.findingRemarks + "<br />";
                        //            }
                        //        }
                        //    }
                        //    i++;

                        //}
                        pdetn = pdetn + "Date:" + itmg.dateOfBoard.ToString() + "   " + itmg.findingRemarks + "<br />";
                    }

                    pdetn = pdetn + "<br />";

                  

                    var a5 = from s in db.Patient_Detail.Where(p => p.PID == id)
                             orderby s.CreatedDate ascending

                             select new { s.PDID, s.CreatedDate };
                    int i = 0;
                    string[] stringArray;
                    stringArray = new string[10000];
                    foreach (var itmn in a5)
                    {
                        
                        
                        pdetn = pdetn + "<br />";


                                               dgnos = "";
                        var dicat = from u in db.CatDiagLists.Where(p => p.PDID == itmn.PDID)
                                    join d in db.CatDaignosis on u.dgid equals d.dgid into com
                                    from d in com.DefaultIfEmpty()
                                    select new
                                    { u.PDID, d.dgdetail };
                        foreach (var itmdi in dicat)
                        {
                            dgnos = dgnos +","+ itmdi.dgdetail;
                        }
                            var a1 = from s in db.Patient_Detail.Where(p => p.PDID == itmn.PDID)

                                 join e in db.CatReferals on s.PDID equals e.PDID into com2
                                 from e in com2.DefaultIfEmpty()
                                 select new { s.PID, s.PDID, s.Patient.Initials, s.Patient.Surname, s.Present_Complain, s.History_OtherComplain, s.History_PresentComplain, s.Other_Complain, s.OPD_Diagnosis, e.PlanofMgt, e.ReffNote, s.ModifiedBy, s.CreatedDate,s.GeneralEntries };
                        foreach (var itm in a1)
                        {
                            modif = itm.ModifiedBy;
                            if (!string.IsNullOrEmpty(itm.CreatedDate.ToString()))
                            {
                                pdetn = pdetn + "Date:" + itm.CreatedDate.ToString("dd/MM/yyyy") + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itm.Present_Complain) && itm.Present_Complain != "null")
                            {
                                pdetn = pdetn+ "Presenting Complain:" + itm.Present_Complain + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itm.History_PresentComplain) && itm.History_PresentComplain != "null")
                            {
                                pdetn = pdetn + " History of Presenting Complain:" + itm.History_PresentComplain + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itm.ReffNote) && itm.ReffNote != "null")
                            {
                                pdetn = pdetn + " Reffaral Note:" + itm.ReffNote + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itm.PlanofMgt)&&itm.PlanofMgt != "null")
                            {
                                pdetn = pdetn + "Notes:" + itm.PlanofMgt + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itm.GeneralEntries) && itm.GeneralEntries != "null")
                            {
                                pdetn = pdetn + "General Entries:" + itm.GeneralEntries + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itm.OPD_Diagnosis) && itm.OPD_Diagnosis != "null")
                            {
                                pdetn = pdetn + "Diagnosis:" + itm.OPD_Diagnosis +dgnos+ "<br />";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(dgnos) && dgnos != "null")
                                {
                                    pdetn = pdetn + "Diagnosis:" + dgnos + "<br />";
                                }
                            }
                        }
                        int modid = Convert.ToInt32(modif);
                       
                        var vitallist = from s in db.Vitals
                                        where s.PDID == itmn.PDID
                                        select new { s.Vital_Type.VitalType, s.VitalValues, s.Reading_Time };
                        foreach (var itmvt in vitallist)
                        {
                            pdetn = pdetn + itmvt.VitalType + "  " + itmvt.VitalValues + " " + itmvt.Reading_Time + "<br />";
                        }

                        


                        var exams = from s in db.ExamineAbdominals
                                       where s.PDID == itmn.PDID
                                    select new { s};
                        var examd = from d in db.ExamineCardiovasculars
                                       where d.PDID == itmn.PDID
                                    select new { d};

                        var exame = from e in db.ExamineCentralNervous
                                                where e.PDID == itmn.PDID
                                    select new {  e };
                        var examf = from f in db.ExamineGenerals
                                    where f.PDID == itmn.PDID
                                    select new { f };
                        var examg = from g in db.ExamineRespiratories
                                    where g.PDID == itmn.PDID
                                    select new { g};

                        var examh = from h in db.ExamineOthers
                                    where h.PDID == itmn.PDID
                                    select new { h };

                        foreach (var itmem in exams)
                        {
                            if (itmem.s != null && !string.IsNullOrEmpty(itmem.s.Other) && itmem.s.Other != "null")
                            {
                                pdetn = pdetn + "Abdorminal Examination:" + itmem.s.Other + "<br />";
                            }
                        }

                        foreach (var itmem in examd)
                        {

                            if (itmem.d != null && !string.IsNullOrEmpty(itmem.d.Other) && itmem.d.Other != "null")
                            {
                                pdetn = pdetn + "Cardio Vascular Examination:" + itmem.d.Other + "<br />";
                            }
                        }

                        foreach (var itmem in exame)
                        {
                            if (itmem.e != null && !string.IsNullOrEmpty(itmem.e.Other) && itmem.e.Other != "null")
                            {
                                pdetn = pdetn + "Central Nervious Examination:" + itmem.e.Other + "<br />";
                            }
                        }

                        foreach (var itmem in examf)
                        {
                            if (itmem.f != null && !string.IsNullOrEmpty(itmem.f.Other) && itmem.f.Other != "null")
                            {
                                pdetn = pdetn + "General Examination:" + itmem.f.Other + "<br />";
                            }
                        }

                        foreach (var itmem in examg)
                        {
                            if (itmem.g != null && !string.IsNullOrEmpty(itmem.g.Other) && itmem.g.Other != "null")
                            {
                                pdetn = pdetn + "Respiratory Examination:" + itmem.g.Other + "<br />";
                            }
                        }

                        foreach (var itmem in examh)
                        {
                            if (itmem.h != null && !string.IsNullOrEmpty(itmem.h.Other) && itmem.h.Other != "null")
                            {
                                pdetn = pdetn + "Other Examination:" + itmem.h.Other + "<br />";
                            }
                        }
                        // var lab = from t in db.Lab_Report.Where(p => p.PDID == id)
                        //          select new
                        //          { t.Lab_SubCategory.Lab_MainCategory.CategoryName, t.Lab_SubCategory.Lab_MainCategory.CategoryID, t.PDID };
                        //var labl = lab.GroupBy(c => new { c.CategoryName, c.CategoryID }).Select(grp => grp.FirstOrDefault()).ToList();
                        var lablist = from t in db.Lab_Report.Where(p => p.PDID == itmn.PDID&&p.Issued=="1")

                                      join x in db.Lab_SubCategory on t.LabTestID equals x.LabTestID
                                      join y in db.Lab_MainCategory on x.CategoryID equals y.CategoryID

                                      select new
                                      { y.CategoryName, y.CategoryID, t.PDID, t.TestSID }; ;
                        var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();
                        if (labl.Count > 0)
                        {
                            pdetn = pdetn + "Lab Tests: ";
                        }
                        foreach (var itmlb in labl)
                        {
                            if (itmlb != null && !string.IsNullOrEmpty(itmlb.CategoryName))
                            {
                                pdetn = pdetn + "<a href='#contact3' onclick=\"vwlb('" + itmlb.TestSID + "','" + itmlb.CategoryID + "');\">" + itmlb.CategoryName + "</a>,";
                            }

                        }
                        pdetn = pdetn + "<br />";
                        var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                        var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
                        var serc = from s in db.Drug_Prescription.Where(p => p.PDID == itmn.PDID && p.issuedQuantity != "0") orderby s.ItemNo select new { s.Ps_Index, s.ItemNo, s.DrugMethod.MethodDetail, s.DrugMethod.DrugMethodCount, s.DrugRoute.RouteDetail, s.Duration, s.Dose, s.PDID, s.MethodType };
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
                        if (joined3.ToList().Count > 0)
                        {
                            pdetn = pdetn + "<br />Prescription:<br />";
                        }
                        foreach (var itmdr in joined3)
                        {
                            if (itmdr != null && !string.IsNullOrEmpty(itmdr.itemdescription))
                            {
                                pdetn = pdetn + itmdr.itemdescription + "  " + itmdr.MethodDetail + "  "+ itmdr.RouteDetail+"   Duration:"+ itmdr .Duration+ " Days <br />";
                            }
                        }
                        var siccat = from u in db.Sick_Category.Where(p => p.PDID == itmn.PDID)
                                     join b in db.Sick_CategoryType on u.CatID equals b.CatID
                                     select new
                                     { u.CatPeriod, b.Category_Type,u.Date };

                        foreach (var itmsc in siccat)
                        {
                            pdetn = pdetn + itmsc.Category_Type + "  Duration:" + itmsc.CatPeriod + "   Effective Date:" + itmsc.Date.ToString() + "<br />";
                        }

                            var docn = from u in db.Users.Where(p => p.UserID == modid)
                                   select new
                                   { u.UserName, u.Salutation, u.FName, u.LName };

                        foreach (var itmdc in docn)
                        {
                            pdetn = pdetn + "Investigated by:" + itmdc.Salutation + " " + itmdc.FName + " " + itmdc.LName + "<br />";
                        }

                        try
                        {
                            int dfgl = pdetn.Split('>').Length;
                            int dfgl2 = pdetn.Split('/').Length;
                            int totv = (dfgl2 - dfgl) * 4 + dfgl;

                            int slen = pdetn.Length;
                            if (slen > 300)
                            {
                                pdetn = " <div  id=a" + io + " >" + pdetn + " </div>";
                                pdetn2 = pdetn2 + pdetn;
                                pdetn = "";
                                io++;
                            }
                            else
                            {
                                pdetn = pdetn + " <br/>";

                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        //  var pdet = string.Join(",", a1);
                      
                        // return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    if (Relationship == "SELF")
                    {
                        pdetn = pdetn + medyr;
                    }
                    
                    pdetn2 =  pdetn2+ " <div id=a"+io+">" + pdetn + " </div>";
                    var result = new { h1 = pdetn2 };

                    return Json(new { h1 = pdetn2 }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PatientHystoryfp(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            String modif = "";
            String pdetn = "";
            String medyr = "";
            String pdetn2 = "";
            String dgnos = "";
            string iserror = "";
            string pid = "";
            string PDID = "";
            string retype = "";
            string Category = "";
            string BloodType = "";

            string Relationship = "";
            int io = 1;
            try
            {
                if (id != null)
                {

                    id = id.Trim(MyChar);

                    var ax1 = from s in db.Patients.Where(p => p.PID == id)
                              join x in db.PersonalDetails on s.ServiceNo equals x.ServiceNo
                              join y in db.MedicalCategories on x.SNo equals y.SNo
                              join z in db.RelationshipTypes on s.RelationshipType equals z.RTypeID
                              select new getdocdata { PID = s.PID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = s.rank1.RNK_NAME, Category = y.MedicalCategory1, BloodType = x.BloodGroup, Relationship = z.Relationship, sv = s.Service_Type,dob=s.DateOfBirth };


                    foreach (var item in ax1)
                    {
                        pid = item.PID;
                        PDID = item.PDID;

                        Category = item.Category;
                        BloodType = item.BloodType;

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
                                                    select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = s.Rank, Category = b.MedicalCategory1, BloodType = s.BloodGroup, Relationship = Relationship, dob = s.DateOfBirth };

                            if (PersonResultList1.Count() > 0)
                            {
                                ax1 = PersonResultList1;
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
                                                    select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.SpouseName, ServiceNo = s.ServiceNo, RNK_NAME = "", Category = "", BloodType = BloodType, Relationship = Relationship, dob = item.dob };

                            if (PersonResultList1.Count() > 0)
                            {
                                ax1 = PersonResultList1;
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
                                                    select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.ParentName, ServiceNo = s.ServiceNo, RNK_NAME = "Mr.", Category = "", BloodType = BloodType, Relationship = Relationship, dob =item.dob };

                            if (PersonResultList1.Count() > 0)
                            {
                                ax1 = PersonResultList1;
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
                                                    select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.ParentName, ServiceNo = s.ServiceNo, RNK_NAME = "Mrs.", Category = "", BloodType = BloodType, Relationship = Relationship, dob = item.dob };
                            if (PersonResultList1.Count() > 0)
                            {
                                ax1 = PersonResultList1;
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
                        if (item.Relationship.ToLower() == "child")
                        {
                            var cdlist = from s in db.Patients.Where(p => p.ServiceNo == item.ServiceNo).Where(p => p.RelationshipType == 5)
                                         orderby s.DateOfBirth
                                         select new { s.DateOfBirth, s.PID };

                            int hj = 1;
                            foreach (var itemchw in cdlist)
                            {
                                if (itemchw.PID == item.PID)
                                {
                                    var PersonResultList1 = from s in db.PersonalDetails
                                                            join b in db.Children on s.SNo equals b.SNo

                                                            where s.ServiceNo == item.ServiceNo && b.DOB == item.dob
                                                            select new getdocdata { PID = pid, Initials = "", Surname = b.ChildName, ServiceNo = s.ServiceNo, RNK_NAME = "", Category = "", BloodType = item.BloodType, Relationship = "Child " + hj, sv = item.sv, dob = b.DOB };

                                    if (PersonResultList1.Count() > 0)
                                    {
                                        ax1 = PersonResultList1;

                                    }
                                    else
                                    {
                                        iserror = "3";

                                    }
                                }
                                hj++;
                            }

                        }
                    }
                    foreach (var iteme in ax1)
                    {
                        if (iteme.Relationship != "SELF")
                        {
                            pdetn = pdetn + iteme.Relationship + " of Service No: " + iteme.ServiceNo + "<br />";
                        }
                        else
                        {
                            pdetn = pdetn + "Service No: " + iteme.ServiceNo + "<br />";
                        }

                        pdetn = pdetn + iteme.RNK_NAME + "    " + iteme.Initials + " " + iteme.Surname + "<br />";
                        pdetn = pdetn + "Blood Group: " + iteme.BloodType + "<br />";
                        if (!string.IsNullOrEmpty(iteme.Category.ToString()))
                        {
                            pdetn = pdetn + "Catagory: " + iteme.Category + "<br />";
                        }
                        if (!string.IsNullOrEmpty(iteme.dob.ToString()))
                        {
                            var today = DateTime.Today;

                            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                            var b = (iteme.dob.Value.Year * 100 + iteme.dob.Value.Month) * 100 + iteme.dob.Value.Day;

                          


                            pdetn = pdetn + "Age: " + ((a - b) / 10000).ToString() + "<br />";
                        }
                        //var ptmed = from s in db.MedicalStatus.Where(p => p.ServiceNo == iteme.ServiceNo)


                        //            select new { s.ServiceNo, s.ExamYear, s.MedicalName };
                        //if (ptmed.Count() > 0)
                        //{
                        //    medyr = medyr + "PT Test Medical: ";
                        //}
                        //foreach (var itmed in ptmed)
                        //{

                        //    if (!string.IsNullOrEmpty(itmed.ServiceNo) && itmed.ServiceNo != "null")
                        //    {
                        //        medyr = medyr + itmed.ExamYear + " " + itmed.MedicalName + "<br />";
                        //    }


                        //}
                        break;
                    }
                    var pmdh = from s in db.PastMedHistories.Where(p => p.PID == id)


                               select new { s.PID, s.PMHDetail, s.Drughst };
                    foreach (var itmd in pmdh)
                    {
                        if (!string.IsNullOrEmpty(itmd.Drughst) && itmd.Drughst != "null")
                        {
                            pdetn = pdetn + "Drug History: " + itmd.Drughst + "<br />";
                        }
                        if (!string.IsNullOrEmpty(itmd.PMHDetail) && itmd.Drughst != "null")
                        {
                            pdetn = pdetn + "Diagnosed Medical Conditions: " + itmd.PMHDetail + "<br />";
                        }



                    }



                    var hyp = from s in db.Hypersensivities.Where(p => p.PID == id)


                              select new { s.PID, s.HypMainCategory.HypersenceMainCategory, s.HypersenseDetail };
                    if (hyp.Count() > 0)
                    {
                        pdetn = pdetn + "Allergies: ";
                    }

                    foreach (var ithyp in hyp)
                    {

                        if (!string.IsNullOrEmpty(ithyp.HypersenceMainCategory) && ithyp.HypersenceMainCategory != "null")
                        {
                            pdetn = pdetn + ithyp.HypersenceMainCategory + " " + ithyp.HypersenseDetail + "<br />";
                        }


                    }
                    pdetn = pdetn + "<br />";


                    ///////////////////////////////////////////////////////////////////////////////////////
                    var medexe = from s in db.MedicalScreens
                                 join b in db.Patient_Detail on s.pdid equals b.PDID
                                 join c in db.Users on s.modifieduser equals c.UserID.ToString()
                                 where (b.PID== id&& s.status==2)

                                 select new {
                                     c.LName,
                                     c.FName,
                                     c.Salutation,
                                     s.msid,
                                     s.pdid,
                                     s.msage,
                                     s.msheight,
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
                                     s.msmes,
                                     s.msbp,
                                     s.msfitness,
                                     s.msreason
                                 };
                    if (medexe.Count() > 0)
                    {
                        pdetn = pdetn + "Medical Examination: ";
                    }

                    pdetn = pdetn + "<div style=\"overflow-x:auto;\"><table class=\"table table-striped\" > <thead><tr><th>Year</th><th>Station</th><th>Age</th><th>Height</th><th>Weight</th><th>BMI</th><th>BP</th><th>Vision</th><th>HB1AC</th><th>Urine Sugar</th><th>FBS</th><th>Total Cholestrol</th><th> EX ECG</th><th>MES</th><th>Fit/Unfit for PFT</th><th>Unfit Reason</th><th>Medical Done by</th> </tr></thead> <tbody> ";
                   
                    foreach (var itmedc in medexe)
                    {string fitpft="";
                        if (itmedc.msfitness==1)
                        {
                            fitpft = "Fit";
                        }
                        else
                        {
                            fitpft = "Unfit";
                        }




                        pdetn = pdetn + "<tr><td>" + itmedc.msyear.ToString() + "</td><td>" + itmedc.msstation + " </td><td> " + itmedc.msage +
                            " </td><td>" + itmedc.msheight + " </td><td>" + itmedc.msweight + "</td><td>" + itmedc.msbmi + " </td><td>" + itmedc.msbp + " </td><td>" + itmedc.msvision +
                            " </td><td>" + itmedc.mshbac + " </td><td>" + itmedc.msusugar + " </td><td>" + itmedc.msfbs + " </td><td>" + itmedc.mstotalc + "</td><td>" + itmedc.msexecg +
                            " </td><td>" + itmedc.msmes + " </td><td>" + fitpft + "  </td><td>" + itmedc.msreason + "  </td><td>" + itmedc.Salutation+". "+ itmedc.FName +" "+ itmedc.LName+ "  </td></tr>";
                        
                    }
                    pdetn = pdetn + " </tbody></table></div><br />";
                    ///////////////////////////////////////////////////////////////////////////

                    //  var pdet = string.Join(",", a1);

                    // return Json(result, JsonRequestBehavior.AllowGet);

                    if (Relationship == "SELF")
                    {
                        pdetn = pdetn + medyr;
                    }

                    pdetn2 = pdetn2 + " <div id=a" + io + ">" + pdetn + " </div>";
                    var result = new { h1 = pdetn2 };

                    return Json(new { h1 = pdetn2 }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }
       
        public JsonResult PatientHystory1(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            String modif = "";
            String pdetn = "";
            String medyr = "";
            String pdetn2 = "";
            String dgnos = "";
            string iserror = "";
            string pid = "";
            string PDID = "";
            string retype = "";
            string Category = "";
            string BloodType = "";

            string Relationship = "";

            int io = 1;
            try
            {
                if (id != null)
                {

                    id = id.Trim(MyChar);
                    var a5 = from s in db.Patient_Detail.Where(p => p.PID == id)
                             orderby s.CreatedDate ascending

                             select new { s.PDID, s.CreatedDate };


                    var ax1 = from s in db.Patients.Where(p => p.PID == id)
                              join x in db.PersonalDetails on s.ServiceNo equals x.ServiceNo

                              join y in db.MedicalCategories on x.SNo equals y.SNo
                              join z in db.RelationshipTypes on s.RelationshipType equals z.RTypeID
                              select new getdocdata { PID = s.PID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = s.rank1.RNK_NAME, Category = y.MedicalCategory1, BloodType = x.BloodGroup, Relationship = z.Relationship, sv = s.Service_Type };


                    foreach (var item in ax1)
                    {
                        pid = item.PID;
                        PDID = item.PDID;

                        Category = item.Category;
                        BloodType = item.BloodType;

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
                                                    select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = s.Rank, Category = b.MedicalCategory1, BloodType = s.BloodGroup, Relationship = Relationship };

                            if (PersonResultList1.Count() > 0)
                            {
                                ax1 = PersonResultList1;
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
                                                    select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.SpouseName, ServiceNo = s.ServiceNo, RNK_NAME = "", Category = "", BloodType = BloodType, Relationship = Relationship };

                            if (PersonResultList1.Count() > 0)
                            {
                                ax1 = PersonResultList1;
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
                                                    select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.ParentName, ServiceNo = s.ServiceNo, RNK_NAME = "Mr.", Category = "", BloodType = BloodType, Relationship = Relationship };

                            if (PersonResultList1.Count() > 0)
                            {
                                ax1 = PersonResultList1;
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
                                                    select new getdocdata { Service_Type = s.ServiceType, PID = pid, PDID = PDID, Initials = "", Surname = b.ParentName, ServiceNo = s.ServiceNo, RNK_NAME = "Mrs.", Category = "", BloodType = BloodType, Relationship = Relationship };
                            if (PersonResultList1.Count() > 0)
                            {
                                ax1 = PersonResultList1;
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
                        if (item.Relationship.ToLower() == "child")
                        {
                            var PersonResultList1 = from s in db.Patients.Where(p => p.PID == id)
                                                    join x in db.PersonalDetails on s.ServiceNo equals x.ServiceNo

                                                    join y in db.RelationshipTypes on s.RelationshipType equals y.RTypeID
                                                    select new getdocdata { PID = s.PID, Initials = "", Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = "", Category = "", BloodType =x.BloodGroup, Relationship =y.Relationship, sv = s.Service_Type };
                            if (PersonResultList1.Count() > 0)
                            {
                                ax1 = PersonResultList1;
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
                    }
                        foreach (var iteme in ax1)
                        {
                        if (iteme.Relationship != "SELF")
                        {
                            pdetn = pdetn + iteme.Relationship + " of Service No: " + iteme.ServiceNo + "<br />";
                        }
                        else
                        {
                            pdetn = pdetn + "Service No: " + iteme.ServiceNo + "<br />";
                        }

                       // pdetn = pdetn + "Service No: " + iteme.ServiceNo + "<br />";
                            pdetn = pdetn + iteme.RNK_NAME + "    " + iteme.Initials + " " + iteme.Surname + "<br />";
                            pdetn = pdetn + "Blood Group: " + iteme.BloodType + "<br />";
                            if (!string.IsNullOrEmpty(iteme.Category.ToString()))
                            {
                                pdetn = pdetn + "Catagory: " + iteme.Category + "<br />";
                            }
                            var ptmed = from s in db.MedicalStatus.Where(p => p.ServiceNo == iteme.ServiceNo)


                                        select new { s.ServiceNo, s.ExamYear, s.MedicalStatus };
                            if (ptmed.Count() > 0)
                            {
                                medyr = medyr + "PT Test Medical: ";
                            }
                            foreach (var itmed in ptmed)
                            {

                                if (!string.IsNullOrEmpty(itmed.ServiceNo) && itmed.ServiceNo != "null")
                                {
                                    medyr = medyr + itmed.ExamYear + " " + itmed.MedicalStatus + "<br />";
                                }


                            }
                            break;
                        }
                        var pmdh = from s in db.PastMedHistories.Where(p => p.PID == id)


                                   select new { s.PID, s.PMHDetail, s.Drughst };
                        foreach (var itmd in pmdh)
                        {
                            if (!string.IsNullOrEmpty(itmd.Drughst) && itmd.Drughst != "null")
                            {
                                pdetn = pdetn + "Drug History: " + itmd.Drughst + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itmd.PMHDetail) && itmd.Drughst != "null")
                            {
                                pdetn = pdetn + "Diagnosed Medical Conditions: " + itmd.PMHDetail + "<br />";
                            }



                        }



                        var hyp = from s in db.Hypersensivities.Where(p => p.PID == id)


                                  select new { s.PID, s.HypMainCategory.HypersenceMainCategory, s.HypersenseDetail };
                        if (hyp.Count() > 0)
                        {
                            pdetn = pdetn + "Allergies: ";
                        }

                        foreach (var ithyp in hyp)
                        {

                            if (!string.IsNullOrEmpty(ithyp.HypersenceMainCategory) && ithyp.HypersenceMainCategory != "null")
                            {
                                pdetn = pdetn + ithyp.HypersenceMainCategory + " " + ithyp.HypersenseDetail + "<br />";
                            }


                        }
                        pdetn = pdetn + "<br />";
                        foreach (var itmn in a5)
                    {
                        dgnos = "";
                        var dicat = from u in db.CatDiagLists.Where(p => p.PDID == itmn.PDID)
                                    join d in db.CatDaignosis on u.dgid equals d.dgid into com
                                    from d in com.DefaultIfEmpty()
                                    select new
                                    { u.PDID, d.dgdetail };
                        foreach (var itmdi in dicat)
                        {
                            dgnos = dgnos + "," + itmdi.dgdetail;
                        }
                        var a1 = from s in db.Patient_Detail.Where(p => p.PDID == itmn.PDID)

                                 join e in db.CatReferals on s.PDID equals e.PDID into com2
                                 from e in com2.DefaultIfEmpty()
                                 select new { s.PID, s.PDID, s.Patient.Initials, s.Patient.Surname, s.Present_Complain, s.History_OtherComplain, s.History_PresentComplain, s.Other_Complain, s.OPD_Diagnosis, e.PlanofMgt, e.ReffNote, s.ModifiedBy, s.CreatedDate };
                        foreach (var itm in a1)
                        {
                            modif = itm.ModifiedBy;
                            if (!string.IsNullOrEmpty(itm.CreatedDate.ToString()))
                            {
                                pdetn = pdetn + "Date:" + itm.CreatedDate + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itm.Present_Complain) && itm.Present_Complain != "null")
                            {
                                pdetn = pdetn + "Presenting Complain:" + itm.Present_Complain + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itm.History_PresentComplain) && itm.History_PresentComplain != "null")
                            {
                                pdetn = pdetn + " History of Presenting Complain:" + itm.History_PresentComplain + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itm.ReffNote) && itm.ReffNote != "null")
                            {
                                pdetn = pdetn + " Reffaral Note:" + itm.ReffNote + "<br />";
                            }
                            if (!string.IsNullOrEmpty(itm.PlanofMgt) && itm.PlanofMgt != "null")
                            {
                                pdetn = pdetn + "Notes:" + itm.PlanofMgt + "<br />";
                            }

                            if (!string.IsNullOrEmpty(itm.OPD_Diagnosis) && itm.OPD_Diagnosis != "null")
                            {
                                pdetn = pdetn + "Diagnosis:" + itm.OPD_Diagnosis + dgnos + "<br />";
                            }
                            else
                            {
                                pdetn = pdetn + "Diagnosis:" + dgnos + "<br />";
                            }
                        }
                        int modid = Convert.ToInt32(modif);

                        var vitallist = from s in db.Vitals
                                        where s.PDID == itmn.PDID
                                        select new { s.Vital_Type.VitalType, s.VitalValues, s.Reading_Time };
                        foreach (var itmvt in vitallist)
                        {
                            pdetn = pdetn + itmvt.VitalType + "  " + itmvt.VitalValues + " " + itmvt.Reading_Time + "<br />";
                        }

                        var exams = from s in db.ExamineAbdominals
                                    where s.PDID == itmn.PDID
                                    select new { s };
                        var examd = from d in db.ExamineCardiovasculars
                                    where d.PDID == itmn.PDID
                                    select new { d };

                        var exame = from e in db.ExamineCentralNervous
                                    where e.PDID == itmn.PDID
                                    select new { e };
                        var examf = from f in db.ExamineGenerals
                                    where f.PDID == itmn.PDID
                                    select new { f };
                        var examg = from g in db.ExamineRespiratories
                                    where g.PDID == itmn.PDID
                                    select new { g };

                        var examh = from h in db.ExamineOthers
                                    where h.PDID == itmn.PDID
                                    select new { h };

                        foreach (var itmem in exams)
                        {
                            if (itmem.s != null && !string.IsNullOrEmpty(itmem.s.Other) && itmem.s.Other != "null")
                            {
                                pdetn = pdetn + "Abdorminal Examination:" + itmem.s.Other + "<br />";
                            }
                        }

                        foreach (var itmem in examd)
                        {

                            if (itmem.d != null && !string.IsNullOrEmpty(itmem.d.Other) && itmem.d.Other != "null")
                            {
                                pdetn = pdetn + "Cardio Vascular Examination:" + itmem.d.Other + "<br />";
                            }
                        }

                        foreach (var itmem in exame)
                        {
                            if (itmem.e != null && !string.IsNullOrEmpty(itmem.e.Other) && itmem.e.Other != "null")
                            {
                                pdetn = pdetn + "Central Nervious Examination:" + itmem.e.Other + "<br />";
                            }
                        }

                        foreach (var itmem in examf)
                        {
                            if (itmem.f != null && !string.IsNullOrEmpty(itmem.f.Other) && itmem.f.Other != "null")
                            {
                                pdetn = pdetn + "General Examination:" + itmem.f.Other + "<br />";
                            }
                        }

                        foreach (var itmem in examg)
                        {
                            if (itmem.g != null && !string.IsNullOrEmpty(itmem.g.Other) && itmem.g.Other != "null")
                            {
                                pdetn = pdetn + "Respiratory Examination:" + itmem.g.Other + "<br />";
                            }
                        }

                        foreach (var itmem in examh)
                        {
                            if (itmem.h != null && !string.IsNullOrEmpty(itmem.h.Other) && itmem.h.Other != "null")
                            {
                                pdetn = pdetn + "Other Examination:" + itmem.h.Other + "<br />";
                            }
                        }
                        // var lab = from t in db.Lab_Report.Where(p => p.PDID == id)
                        //          select new
                        //          { t.Lab_SubCategory.Lab_MainCategory.CategoryName, t.Lab_SubCategory.Lab_MainCategory.CategoryID, t.PDID };
                        //var labl = lab.GroupBy(c => new { c.CategoryName, c.CategoryID }).Select(grp => grp.FirstOrDefault()).ToList();
                        var lablist = from t in db.Lab_Report.Where(p => p.PDID == itmn.PDID&& p.Issued=="1")
                                      join x in db.Lab_SubCategory on t.LabTestID equals x.LabTestID
                                      join y in db.Lab_MainCategory on x.CategoryID equals y.CategoryID


                                      select new
                                      { y.CategoryName, y.CategoryID, t.PDID, t.TestSID }; ;
                        var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();
                        if (labl.Count > 0)
                        {
                            pdetn = pdetn + "Lab Tests:<br />";
                        }
                        foreach (var itmlb in labl)
                        {
                            if (itmlb != null && !string.IsNullOrEmpty(itmlb.CategoryName))
                            {
                                pdetn = pdetn + "<a href='#contact3' onclick=\"vwlb('" + itmlb.TestSID+"','" + itmlb.CategoryID + "');\">"+ itmlb.CategoryName + "</a><br />";
                            }

                        }

                        var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                        var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
                        var serc = from s in db.Drug_Prescription.Where(p => p.PDID == itmn.PDID && p.issuedQuantity != "0") orderby s.ItemNo select new { s.Ps_Index, s.ItemNo, s.DrugMethod.MethodDetail, s.DrugMethod.DrugMethodCount, s.DrugRoute.RouteDetail, s.Duration, s.Dose, s.PDID, s.MethodType };
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
                        if (joined3.ToList().Count > 0)
                        {
                            pdetn = pdetn + "Prescription:<br />";
                        }
                        foreach (var itmdr in joined3)
                        {
                            if (itmdr != null && !string.IsNullOrEmpty(itmdr.itemdescription))
                            {
                                pdetn = pdetn + itmdr.itemdescription + "  " + itmdr.MethodDetail+ "  " + itmdr.RouteDetail + "   Duration:" + itmdr.Duration + " Days <br />";
                            }
                        }
                        var siccat = from u in db.Sick_Category.Where(p => p.PDID == itmn.PDID)
                                     join b in db.Sick_CategoryType on u.CatID equals b.CatID
                                     select new
                                     { u.CatPeriod, b.Category_Type, u.Date };

                        foreach (var itmsc in siccat)
                        {
                            pdetn = pdetn + itmsc.Category_Type + "  Duration:" + itmsc.CatPeriod + "   Effective Date:" + itmsc.Date.ToString() + "<br />";
                        }

                        var docn = from u in db.Users.Where(p => p.UserID == modid)
                                   select new
                                   { u.UserName, u.Salutation, u.FName, u.LName };

                        foreach (var itmdc in docn)
                        {
                            pdetn = pdetn + "Investigated by:" + itmdc.Salutation + " " + itmdc.FName + " " + itmdc.LName + "<br />";
                        }



                        int slen = pdetn.Length;
                        if (slen > 1500)
                        {
                            pdetn = " <div  id=a" + io + " >" + pdetn + " </div>";
                            pdetn2 = pdetn2 + pdetn;
                            pdetn = "";
                            io++;
                        }
                        else
                        {
                            pdetn = pdetn + " <hr>";

                        }
                        //  var pdet = string.Join(",", a1);

                        // return Json(result, JsonRequestBehavior.AllowGet);
                    }

                    pdetn2 = pdetn2 + " <div id=a" + io + ">" + pdetn + " </div>";
                    var result = new { h1 = pdetn2 };

                    return Json(new { h1 = pdetn2 }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }
        public string EpisodCreate(PatientViewModel role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    #region Get UserID
                    if (Session["UserID"] != null)
                    {
                        Userid = (int)Session["UserID"];
                    }
                    #endregion


                    #region Insert Data

                    #region Add POR Data

                    Patient_Detail dtheader = new Patient_Detail();
                    dtheader.PDID = "1" + role.PDID;
                    dtheader.PID = role.PID;
                    dtheader.Present_Complain = role.Present_Complain;
                    dtheader.Status = 1;
                    dtheader.CreatedDate = DateTime.Now;
                    dtheader.CreatedBy = Userid.ToString();
                    dtheader.OPDID = "OP1";

                    db.Patient_Detail.Add(dtheader);

                    #endregion

                    #region Add VitalData



                    #endregion

                    db.SaveChanges();
                    #endregion
                    return "Successfully Created";
                }
                catch (Exception ex)
                {
                    return "Error";
                    throw;
                }

            }
            return "Error";
            //return View();
        }

    }
}
