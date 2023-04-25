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
namespace MMS.Controllers
{
    public class DhsController : Controller
    {

        private int sikcnt;
        private MMSEntities db = new MMSEntities();
           // private P3Context dbhrms = new P3Context();
            //private EPASContext db = new EPASContext();
            private int Userid;
            private string err;
            ImageCodecInfo df;
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        string sqlQuery;
       // private P2Context dbp2 = new P2Context();
            private string base64String;

            // GET: Patient_Detail
            public async Task<ActionResult> Index(int? page)
            {
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

        public ActionResult drugav()
        {
            return view();
        }

        // GET: Patient_Detail/Create
        public ActionResult Create(int? page, string id, string currentFilter, string id3)
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



        //        DataTable oDataSet1 = new DataTable();
        //        oSqlConnection = new SqlConnection(conStr);
        //        oSqlCommand = new SqlCommand();
        //        sqlQuery = " select SpecialityID,LOCID from [dbo].[Staff_Master] with (nolock) where UserID='" + userid + "'";
        //        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
        //        oSqlCommand.Connection = oSqlConnection;
        //        oSqlCommand.CommandText = sqlQuery;
        //        oSqlConnection.Open();
        //        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
        //        oDataSet = null;
        //        oSqlDataAdapter.Fill(oDataSet1);
        //        oSqlConnection.Close();
        //        var opd = oDataSet1.AsEnumerable()
        //.Select(dataRow => new Staff_Master
        //{
        //    SpecialityID = dataRow.Field<int>("SpecialityID"),
        //    LOCID = dataRow.Field<string>("LOCID")

        //}).ToList();



        //        foreach (var item in opd)
        //        {
        //            specid = item.SpecialityID;

        //        }

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
                    sqlQuery = "       SELECT  (a.Present_Complain)pcomoplian,CASE WHEN isnull(b.ServiceStatus,'0')='0'   then (select top 1 ServiceStatus from [MMS].[dbo].[PersonalDetails] where  " +

  "     ServiceNo = c.ServiceNo)  else b.ServiceStatus end ServiceStatus, case when isnull(b.Surname,'0')= '0' then c.Initials + ' ' + c.Surname else b.Surname end     sname   " +
"  ,CASE WHEN isnull(b.Rank,'0')='0'   then (select top 1 Rank from [MMS].[dbo].[PersonalDetails] where ServiceNo=c.ServiceNo)  else b.Rank end rnkname, " +
"  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"  , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate,(a.ModifiedDate)mdate, (h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
"   as b on c.ServiceNo=b.ServiceNo  and b.ServiceType =CASE when c.Service_Type=4 then 1 when c.Service_Type = 5 then 2 else c.Service_Type end left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" where convert(date, a.[CreatedDate]) =CONVERT(varchar,'" + dd.ToShortDateString() + "',111)  and (a.status= 2 or a.status= 5) and a.PatientCatID!=2and a.PatientCatID!=5  " +
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
            }).ToList();



                    ///db.Patient_Detail.Include(p => p.Patient).Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                    //  patient_Detail = patient_Detail.GroupBy(t => t.pdids).Select(grp => grp.FirstOrDefault()).OrderByDescending(s=>s.crdate);
                    var pageNumber = page ?? 1;

                    onePageOfProducts = lid.ToList();
                }


                else if (!String.IsNullOrEmpty(id))
                {
                    DateTime dt1 = DateTime.Now.Date;
                //    if (opdid.Contains("cl"))
                //    {
                //        // var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id ==Convert.ToInt32(specid)).Where(p => p.event_start == dt1).AsNoTracking() select new { s.title };
                //        DataTable oDataSet2 = new DataTable();
                //        oSqlConnection = new SqlConnection(conStr);
                //        oSqlCommand = new SqlCommand();
                //        sqlQuery = " select title from [dbo].[Clinic_Schedule] with (nolock) where clinic_id='" + Convert.ToInt32(specid) + "' and event_start='" + dt1 + "' ";
                //        // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                //        oSqlCommand.Connection = oSqlConnection;
                //        oSqlCommand.CommandText = sqlQuery;
                //        oSqlConnection.Open();
                //        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                //        oSqlDataAdapter.Fill(oDataSet2);
                //        oSqlConnection.Close();
                //        var shed = oDataSet2.AsEnumerable()
                //.Select(dataRow => new Clinic_Schedule
                //{
                //    title = dataRow.Field<string>("title")


                //}).ToList();



                //    }
                    DateTime dd = DateTime.Now.Date;
                    DataTable oDataSet4 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();


                    sqlQuery = "   SELECT  (a.Present_Complain)pcomoplian,CASE WHEN isnull(b.ServiceStatus,'0')='0'   then (select top 1 ServiceStatus from [MMS].[dbo].[PersonalDetails] where  " +

  "     ServiceNo = c.ServiceNo)  else b.ServiceStatus end ServiceStatus, case when isnull(b.Surname,'0')= '0' then c.Initials + ' ' + c.Surname else b.Surname end     sname    " +
 "  ,CASE WHEN isnull(b.Rank,'0')='0'   then (select top 1 Rank from [MMS].[dbo].[PersonalDetails] where ServiceNo=c.ServiceNo)  else b.Rank end rnkname, " +
 "  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"   , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate, (a.ModifiedDate)mdate,(h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
 "  as b on c.ServiceNo=b.ServiceNo  and b.ServiceType =CASE when c.Service_Type=4 then 1 when c.Service_Type = 5 then 2 else c.Service_Type end left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where   a.PatientCatID!=5 and c.ServiceNo= '" + id + "'   " +

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
                pidp = dataRow.Field<string>("pidp"),
                relasiondet = dataRow.Field<string>("relasiondet"),
                ServiceStatus = dataRow.Field<string>("ServiceStatus"),
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
                        //foreach (var item in shed)
                        //{

                        //    title[r] = item.title;
                        //    r++;
                        //}


                        //var patient_Detailn = from s in db.Patient_Detail.Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7 || p.Status == 5).Where(p => p.OPDID == opdid).AsNoTracking() join h in db.Patients on s.PID equals h.PID join f in db.RelationshipTypes on h.RelationshipType equals f.RTypeID orderby s.CreatedDate descending select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, opddiag = s.OPD_Diagnosis, relasiont = f.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = f.Relationship };

                        //DateTime dd = DateTime.Now.Date;
                        //var patient_Detail = from s in db.Patient_Detail.Where(p => p.PDID == "dfdffddf").AsNoTracking() join h in db.Patients on s.PID equals h.PID join f in db.RelationshipTypes on h.RelationshipType equals f.RTypeID orderby s.CreatedDate descending select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, opddiag = s.OPD_Diagnosis, relasiont = f.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = f.Relationship };

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
                        //                               join h in db.Patients on s.PID equals h.PID
                        //                               join f in db.RelationshipTypes on h.RelationshipType equals f.RTypeID
                        //                               select new getdocdetail { sv = s.Patient.Service_Type, pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, relasiont = f.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = f.Relationship };
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
                        sqlQuery = "       SELECT  (a.Present_Complain)pcomoplian,CASE WHEN isnull(b.ServiceStatus,'0')='0'   then (select top 1 ServiceStatus from [MMS].[dbo].[PersonalDetails] where  " +

"     ServiceNo = c.ServiceNo)  else b.ServiceStatus end ServiceStatus, case when isnull(b.Surname,'0')= '0' then c.Initials + ' ' + c.Surname else b.Surname end     sname   " +
"  ,CASE WHEN isnull(b.Rank,'0')='0'   then (select top 1 Rank from [MMS].[dbo].[PersonalDetails] where ServiceNo=c.ServiceNo)  else b.Rank end rnkname, " +
"  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"  , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate,(a.ModifiedDate)mdate, (h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
"   as b on c.ServiceNo=b.ServiceNo  and b.ServiceType =CASE when c.Service_Type=4 then 1 when c.Service_Type = 5 then 2 else c.Service_Type end left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" where convert(date, a.[CreatedDate]) =CONVERT(varchar,'" + dd.ToShortDateString() + "',111)  and a.PatientCatID!=5  " +
" and a.opdid='" + opdid + "'  order by crdate desc ";
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
                    ServiceStatus = dataRow.Field<string>("ServiceStatus"),
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
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
                //throw;
            }
        }



        public ActionResult NurseCreate(int? page, int? page1, string id, string id1)
            {
                try
                {
                    var onePageOfProducts = (dynamic)null;
                    var sickcate = (dynamic)null;
                    char[] MyChar = { '/', '"', ' ' };
                    string opdid = "";
                    string locid = "";
                    var title = new String[100];
                    int specid = 0;
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

                        // opdid = item.LOCID;
                    }
                    var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                    foreach (var item in serW)
                    {

                        locid = item.LocationID;
                    }
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
                        var patient_Detail = db.Patient_Detail.Include(p => p.Patient).Include(p => p.Status1).Where(p => p.Patient.ServiceNo.Contains(id)).OrderByDescending(p => p.CreatedDate);
                        var pageNumber = page ?? 1;
                        onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
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
                            var patient_Detail = db.Patient_Detail.Include(p => p.Patient).Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                            var pageNumber = page ?? 1;
                            onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
                        }
                    }
                    if (!String.IsNullOrEmpty(id1))
                    {
                        DateTime dd1 = DateTime.Now.Date;
                        var sickdet = db.Sick_Category.Include(p => p.Patient_Detail.Patient).Where(p => p.Patient_Detail.OPDID == opdid).Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id1)).OrderByDescending(p => p.Date);
                        var pageNumber1 = page1 ?? 1;
                        sickcate = sickdet.ToPagedList(pageNumber1, 10);
                        ViewBag.sickcate = sickcate;
                    }
                    else
                    {
                        DateTime dd1 = DateTime.Now.Date;
                        var sickdet = db.Sick_Category.Include(p => p.Patient_Detail.Patient).Where(p => p.Patient_Detail.OPDID == opdid).Where(p => p.Date.Value.Day == dd1.Day && p.Date.Value.Month == dd1.Month && p.Date.Value.Year == dd1.Year).OrderByDescending(p => p.Date);
                        var pageNumber1 = page1 ?? 1;
                        sickcate = sickdet.ToPagedList(pageNumber1, 10);
                        ViewBag.sickcate = sickcate;
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
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);


                var ser = from s in db.Sick_Category.Where(p => p.CatIndex == NewString) join b in db.Sick_CategoryType on s.CatID equals b.CatID select new { s.Patient_Detail.Patient.Initials, s.Patient_Detail.Patient.ServiceNo, s.Patient_Detail.Patient.Surname, b.Category_Type, s.CatPeriod };

                return Json(ser, JsonRequestBehavior.AllowGet);
            }
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

        public ActionResult uplfile()
        {
             return View();
        }



        public async Task<ActionResult> Edit(string id)
            {
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
        public ActionResult view()
        {

            return View();
        }
        public ActionResult view3()
        {

            return View();
        }
        public ActionResult view4()
        {

            return View();
        }
        public ActionResult view5()
        {

            return View();
        }
        public ActionResult view6()
        {

            return View();
        }
        public ActionResult view7()
        {

            return View();
        }
        public ActionResult view8()
        {

            return View();
        }
        public ActionResult view9()
        {

            return View();
        }
        public ActionResult view10()
        {

            return View();
        }
        public ActionResult view11()
        {

            return View();
        }

        // POST: Patient_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> DeleteConfirmed(string id)
            {
                Patient_Detail patient_Detail = await db.Patient_Detail.FindAsync(id);
                db.Patient_Detail.Remove(patient_Detail);
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

            // GET: Vital Types
            public JsonResult GetVitalTypes()
            {
                var types = db.Vital_Type.Select(x => new { x.VTID, x.VitalType }).ToList();
                return Json(types, JsonRequestBehavior.AllowGet);

            }
            public JsonResult GetHyperTypes()
            {
                var Hypertypes = db.HypMainCategories.Select(x => new { x.HypersenceMainCatID, x.HypersenceMainCategory }).ToList();
                return Json(Hypertypes, JsonRequestBehavior.AllowGet);

            }
            public JsonResult GetSeverityTypes()
            {
                var Severitytypes = db.HypersenseSeverties.Select(x => new { x.SevertyID, x.SevertyType }).ToList();
                return Json(Severitytypes, JsonRequestBehavior.AllowGet);

            }
            public JsonResult GetMethod()
            {
                var Methods = db.DrugMethods.Select(x => new { x.MethodID, x.MethodDetail }).ToList();
                return Json(Methods, JsonRequestBehavior.AllowGet);

            }
            public JsonResult GetRoute()
            {
                var Route = db.DrugRoutes.Select(x => new { x.RouteID, x.RouteDetail }).ToList();
                return Json(Route, JsonRequestBehavior.AllowGet);

            }
            public JsonResult Getdgn()
            {
                var status = db.CatDaignosis.Select(x => new { x.dgid, x.dgdetail }).ToList();
                return Json(status, JsonRequestBehavior.AllowGet);

            }
            public JsonResult GetSt()
            {
                var status = db.Status.Select(x => new { x.Status1, x.StatusDec }).ToList();
                return Json(status, JsonRequestBehavior.AllowGet);

            }
            public JsonResult GetDrug()
            {
                var GetDrug = db.EPASPharmacyItems.Select(x => new { x.itemno, x.itemdescription }).ToList();
                return Json(GetDrug, JsonRequestBehavior.AllowGet);

            }
            public JsonResult GetMedkw()
            {
                var GetMedkw = db.MedKeywords.Select(x => new { x.MKID, x.MKDetail }).ToList();
                return Json(GetMedkw, JsonRequestBehavior.AllowGet);

            }

            public JsonResult Getlablist()
            {
                var Getlablist = db.Lab_MainCategory.Select(x => new { x.CategoryID, x.CategoryName }).ToList();
                return Json(Getlablist, JsonRequestBehavior.AllowGet);

            }
            public JsonResult Getsmslablist()
            {
                var Getlablist = db.Lab_sms_Cat.Select(x => new { x.Catid, x.CatName }).ToList();
                return Json(Getlablist, JsonRequestBehavior.AllowGet);

            }
            public JsonResult GetClinics()
            {
                var GetClinic = db.Clinic_Type.Select(x => new { x.ClinicTypeID, x.ClinicDetails }).ToList();
                return Json(GetClinic, JsonRequestBehavior.AllowGet);

            }

            public JsonResult GetSicktype()
            {
                var GetSicktype = db.Sick_Type.Select(x => new { x.CatID, x.Category }).ToList();
                return Json(GetSicktype, JsonRequestBehavior.AllowGet);

            }
            public JsonResult GetHyperReactType()
            {
                var ReactType = db.HypRMainCategories.Select(x => new { x.HypRMainID, x.HypRMainDetail }).ToList();
                return Json(ReactType, JsonRequestBehavior.AllowGet);

            }
            public JsonResult GetHyperSubType(string id)
            {
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                var HyperSubtypes = from s in db.HypersensivityTypes.Where(p => p.HyperType == NewString) select new { s.HyperType, s.HyperTypeID, s.HyperSubType };
                return Json(HyperSubtypes, JsonRequestBehavior.AllowGet);

            }
            public JsonResult GetReactSubType(string id)
            {
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                var HyperSubtypes = from s in db.HypersenseReactions.Where(p => p.RCategory == NewString) select new { s.ReactID, s.RSubcategory, s.RCategory };
                return Json(HyperSubtypes, JsonRequestBehavior.AllowGet);

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

            }
            public class Labreader
            {

                public string labid { get; set; }

                public string labcat { get; set; }


            }
            public class Sickreader
            {

                public int scatid { get; set; }
                public string scategory { get; set; }
                public string sdays { get; set; }


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
            public JsonResult Submitpatient(string items, string hitems, string Present_Complain, string PID, string HDetail)
            {
                char[] MyChar = { '/', '"', ' ' };
                string NewString = items.Trim(MyChar);
                string NewString1 = hitems.Trim(MyChar);
                string NewString2 = Present_Complain.Trim(MyChar);
                string NewString3 = PID.Trim(MyChar);
                string NewString4 = "";
                if (!String.IsNullOrEmpty(HDetail))
                {
                    NewString4 = HDetail.Trim(MyChar);
                }


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
                IndexGeneration indi = new IndexGeneration();
                patient_Detail.PDID = indi.CreatePDID(NewString3);
                patient_Detail.PID = NewString3;
                patient_Detail.OPDID = opdid;
                patient_Detail.Present_Complain = NewString2;
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
                    oVital.CreatedDate = DateTime.Now.Date;
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

                    }
                }

                return Json(err, JsonRequestBehavior.AllowGet);
            }

            public JsonResult Submitlabpatient(string litems, string Present_Complain, string PID)
            {
                char[] MyChar = { '/', '"', ' ' };

                //   string NewString2 = Present_Complain.Trim(MyChar);
                string NewString3 = PID.Trim(MyChar);

                if (!String.IsNullOrEmpty(litems))
                {
                    litems = litems.Trim(MyChar);
                }


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
                patient_Detail.Present_Complain = "For Investigation";
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
                    var Abdominalvar1 = from s in db.Patient_Detail.Where(p => p.PID == NewString3).Where(p => p.OPDID == opdid).Where(p => p.Status != 2) select new { s.PDID };

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

                    foreach (Labreader p in objsl)
                    {

                        Lab_Report oLab_Report = new Lab_Report();
                        var objl = from s in db.Lab_SubCategory.Where(f => f.CategoryID == p.labid) select new { s.LabTestID };
                        int objcountl1 = objl.Count();
                        foreach (var q in objl)
                        {


                            var lablist = from t in db.Lab_Report 
                                          join h in db.Lab_SubCategory on t.LabTestID equals h.LabTestID
                                          join v in db.Lab_MainCategory on h.CategoryID equals v.CategoryID
                                          where (t.PDID == Abdominal)
                                          
                                          select new
                                          { v.CategoryName, h.CategoryID, t.PDID, t.TestSID }; ;
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
                                }
                                else
                                {
                                    oLab_Reports.PDID = patient_Detail.PDID;
                                }
                                oLab_Reports.Issued = "0";
                                oLab_Reports.IsPrint = "0";
                                oLab_Reports.TestSID = patient_Detail.PDID + "x0";
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
                                }
                                else
                                {
                                    oLab_Reports.PDID = patient_Detail.PDID;
                                }
                                oLab_Reports.Issued = "0";
                                oLab_Reports.IsPrint = "0";
                                oLab_Reports.TestSID = patient_Detail.PDID + "x" + tf;
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


            public JsonResult Savepatient(string bitems, string reftohosp, string planofmgt, string drughyst, string hitems, string pastmedhys, string items, string sitems, string litems, string ditems, string Present_Complain, string History_PresentComplain, string Other_Complain, string History_OtherComplain, string OPD_Diagnosis, string Alert, string Confused, string Drowsy, string Unconscious, string IsPain, string IsPainno, string PainScore, string PainLoc, string Lean, string Average, string Obese, string Dyspnoea, string Cyanosis, string Pallor, string Icterus, string Arcus, string Xanthomata, string warm, string Clammy, string Oedema, string Clubbing, string Rashes, string Ulcers, string Wounds, string Tattoos, string Moles, string Navi, string Scars, string SkinDetails, string Carius, string OralUlcers, string Pulsevolume, string RhythmReg, string RhythmIreg, string JVP, string ApexBeat, string HeartSounds, string Murmurs, string CardioOther, string Carotidbruit, string BrachialLeft, string RadialLeft, string FemoralLeft, string DorsalisLeft, string TibialLeft, string BrachialRight, string RadialRight, string FemoralRight, string DorsalisRight, string TibialRight, string SpO2, string PFR, string Trachea, string Apex, string ChestMovement, string Percussion, string Auscultation, string OiNotIndicated, string OiNotIndicatedtxt, string NvAlert, string NvConfused, string NvDrowsy, string NvUnconscious, string NvRational, string NvOriented, string NvMentalConfused, string NvNotIndicated, string CranialNerves, string MotorSystem, string SensorySystem, string NvReflexes, string NvGcs, string NvNormal, string NvAphasia, string NvDysarthria, string Distension, string FreeFluid, string Liver, string Livertxt, string Spleen, string Spleentxt, string PulsatileLumps, string OtherLumps, string OtherLumpstxt, string BowelAbsent, string BowelSluggish, string BowelNormal, string BowelExaggereted, string AbdOther, string PDID, string pntstatus, string GClinic, string dgn1)
            {
                String err = "";
                char[] MyChar = { '/', '"', ' ' };
                if (!String.IsNullOrEmpty(pastmedhys))
                {
                    pastmedhys = pastmedhys.Trim(MyChar);
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
                }
                if (!String.IsNullOrEmpty(History_PresentComplain))
                {
                    History_PresentComplain = History_PresentComplain.Trim(MyChar);
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
                }
                if (!String.IsNullOrEmpty(Alert))
                {
                    Alert = Alert.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(Confused))
                {
                    Confused = Confused.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(Drowsy))
                {
                    Drowsy = Drowsy.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(Unconscious))
                {
                    Unconscious = Unconscious.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(planofmgt))
                {
                    planofmgt = planofmgt.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(drughyst))
                {
                    drughyst = drughyst.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(reftohosp))
                {
                    reftohosp = reftohosp.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(bitems))
                {
                    bitems = bitems.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(IsPain)) { IsPain = IsPain.Trim(MyChar); }
                if (!String.IsNullOrEmpty(PDID)) { PDID = PDID.Trim(MyChar); }
                if (!String.IsNullOrEmpty(IsPainno)) { IsPainno = IsPainno.Trim(MyChar); }
                if (!String.IsNullOrEmpty(PainScore)) { PainScore = PainScore.Trim(MyChar); }
                if (!String.IsNullOrEmpty(PainLoc)) { PainLoc = PainLoc.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Lean)) { Lean = Lean.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Average)) { Average = Average.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Obese)) { Obese = Obese.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Dyspnoea)) { Dyspnoea = Dyspnoea.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Cyanosis)) { Cyanosis = Cyanosis.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Pallor)) { Pallor = Pallor.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Icterus)) { Icterus = Icterus.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Arcus)) { Arcus = Arcus.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Xanthomata)) { Xanthomata = Xanthomata.Trim(MyChar); }
                if (!String.IsNullOrEmpty(warm)) { warm = warm.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Clammy)) { Clammy = Clammy.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Oedema)) { Oedema = Oedema.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Clubbing)) { Clubbing = Clubbing.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Rashes)) { Rashes = Rashes.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Ulcers)) { Ulcers = Ulcers.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Wounds)) { Wounds = Wounds.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Tattoos)) { Tattoos = Tattoos.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Moles)) { Moles = Moles.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Navi)) { Navi = Navi.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Scars)) { Scars = Scars.Trim(MyChar); }
                if (!String.IsNullOrEmpty(SkinDetails)) { SkinDetails = SkinDetails.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Carius)) { Carius = Carius.Trim(MyChar); }
                if (!String.IsNullOrEmpty(OralUlcers)) { OralUlcers = OralUlcers.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Pulsevolume)) { Pulsevolume = Pulsevolume.Trim(MyChar); }
                if (!String.IsNullOrEmpty(RhythmReg)) { RhythmReg = RhythmReg.Trim(MyChar); }
                if (!String.IsNullOrEmpty(RhythmIreg)) { RhythmIreg = RhythmIreg.Trim(MyChar); }
                if (!String.IsNullOrEmpty(JVP)) { JVP = JVP.Trim(MyChar); }
                if (!String.IsNullOrEmpty(ApexBeat)) { ApexBeat = ApexBeat.Trim(MyChar); }
                if (!String.IsNullOrEmpty(HeartSounds)) { HeartSounds = HeartSounds.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Murmurs)) { Murmurs = Murmurs.Trim(MyChar); }
                if (!String.IsNullOrEmpty(CardioOther)) { CardioOther = CardioOther.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Carotidbruit)) { Carotidbruit = Carotidbruit.Trim(MyChar); }
                if (!String.IsNullOrEmpty(BrachialLeft)) { BrachialLeft = BrachialLeft.Trim(MyChar); }
                if (!String.IsNullOrEmpty(RadialLeft)) { RadialLeft = RadialLeft.Trim(MyChar); }
                if (!String.IsNullOrEmpty(FemoralLeft)) { FemoralLeft = FemoralLeft.Trim(MyChar); }
                if (!String.IsNullOrEmpty(DorsalisLeft)) { DorsalisLeft = DorsalisLeft.Trim(MyChar); }
                if (!String.IsNullOrEmpty(TibialLeft)) { TibialLeft = TibialLeft.Trim(MyChar); }
                if (!String.IsNullOrEmpty(BrachialRight)) { BrachialRight = BrachialRight.Trim(MyChar); }
                if (!String.IsNullOrEmpty(RadialRight)) { RadialRight = RadialRight.Trim(MyChar); }
                if (!String.IsNullOrEmpty(FemoralRight)) { FemoralRight = FemoralRight.Trim(MyChar); }
                if (!String.IsNullOrEmpty(DorsalisRight)) { DorsalisRight = DorsalisRight.Trim(MyChar); }
                if (!String.IsNullOrEmpty(TibialRight)) { TibialRight = TibialRight.Trim(MyChar); }
                if (!String.IsNullOrEmpty(SpO2)) { SpO2 = SpO2.Trim(MyChar); }
                if (!String.IsNullOrEmpty(PFR)) { PFR = PFR.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Trachea)) { Trachea = Trachea.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Apex)) { Apex = Apex.Trim(MyChar); }
                if (!String.IsNullOrEmpty(ChestMovement)) { ChestMovement = ChestMovement.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Percussion)) { Percussion = Percussion.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Auscultation)) { Auscultation = Auscultation.Trim(MyChar); }
                if (!String.IsNullOrEmpty(OiNotIndicated)) { OiNotIndicated = OiNotIndicated.Trim(MyChar); }
                if (!String.IsNullOrEmpty(OiNotIndicatedtxt)) { OiNotIndicatedtxt = OiNotIndicatedtxt.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvAlert)) { NvAlert = NvAlert.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvConfused)) { NvConfused = NvConfused.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvDrowsy)) { NvDrowsy = NvDrowsy.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvUnconscious)) { NvUnconscious = NvUnconscious.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvRational)) { NvRational = NvRational.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvOriented)) { NvOriented = NvOriented.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvMentalConfused)) { NvMentalConfused = NvMentalConfused.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvNotIndicated)) { NvNotIndicated = NvNotIndicated.Trim(MyChar); }
                if (!String.IsNullOrEmpty(CranialNerves)) { CranialNerves = CranialNerves.Trim(MyChar); }
                if (!String.IsNullOrEmpty(MotorSystem)) { MotorSystem = MotorSystem.Trim(MyChar); }
                if (!String.IsNullOrEmpty(SensorySystem)) { SensorySystem = SensorySystem.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvReflexes)) { NvReflexes = NvReflexes.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvGcs)) { NvGcs = NvGcs.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvNormal)) { NvNormal = NvNormal.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvAphasia)) { NvAphasia = NvAphasia.Trim(MyChar); }
                if (!String.IsNullOrEmpty(NvDysarthria)) { NvDysarthria = NvDysarthria.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Distension)) { Distension = Distension.Trim(MyChar); }
                if (!String.IsNullOrEmpty(FreeFluid)) { FreeFluid = FreeFluid.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Liver)) { Liver = Liver.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Livertxt)) { Livertxt = Livertxt.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Spleen)) { Spleen = Spleen.Trim(MyChar); }
                if (!String.IsNullOrEmpty(Spleentxt)) { Spleentxt = Spleentxt.Trim(MyChar); }
                if (!String.IsNullOrEmpty(PulsatileLumps)) { PulsatileLumps = PulsatileLumps.Trim(MyChar); }
                if (!String.IsNullOrEmpty(OtherLumps)) { OtherLumps = OtherLumps.Trim(MyChar); }
                if (!String.IsNullOrEmpty(OtherLumpstxt)) { OtherLumpstxt = OtherLumpstxt.Trim(MyChar); }
                if (!String.IsNullOrEmpty(BowelAbsent)) { BowelAbsent = BowelAbsent.Trim(MyChar); }
                if (!String.IsNullOrEmpty(BowelSluggish)) { BowelSluggish = BowelSluggish.Trim(MyChar); }
                if (!String.IsNullOrEmpty(BowelNormal)) { BowelNormal = BowelNormal.Trim(MyChar); }
                if (!String.IsNullOrEmpty(BowelExaggereted)) { BowelExaggereted = BowelExaggereted.Trim(MyChar); }
                if (!String.IsNullOrEmpty(AbdOther)) { AbdOther = AbdOther.Trim(MyChar); }
                if (!String.IsNullOrEmpty(dgn1)) { AbdOther = AbdOther.Trim(MyChar); }
                string pid = "";
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

                    //  opdid = item.LOCID;
                }
                var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                foreach (var item in serW)
                {

                    locid = item.LocationID;
                }
                var pidvar = from s in db.Patient_Detail.Where(p => p.PDID == PDID) select new { s.PID };

                foreach (var item in pidvar)
                {

                    pid = item.PID;
                }
                IndexGeneration indi = new IndexGeneration();
                Patient_Detail patient_Detail = new Patient_Detail();

                patient_Detail.PDID = PDID;
                patient_Detail.Present_Complain = Present_Complain;
                patient_Detail.History_PresentComplain = History_PresentComplain;
                patient_Detail.Other_Complain = Other_Complain;
                patient_Detail.History_OtherComplain = History_OtherComplain;
                patient_Detail.OPD_Diagnosis = OPD_Diagnosis;
                patient_Detail.ModifiedDate = DateTime.Now.Date;
                patient_Detail.ModifiedBy = userid.ToString();
                patient_Detail.CreatedBy = userid.ToString();
                patient_Detail.PID = pid;
                //string daignosisid = "";
                //if (!String.IsNullOrEmpty(dgn1))
                //{
                //    dynamic data = JObject.Parse(dgn1);
                //    daignosisid = data.dgid;
                //}


                patient_Detail.OPDID = opdid;
                // patient_Detail.DaignosisID = daignosisid;
                patient_Detail.CreatedDate = DateTime.Now.Date;
                patient_Detail.Status = Convert.ToInt32(pntstatus);
                int cltyp = 0;
                if (!String.IsNullOrEmpty(GClinic))
                {
                    dynamic data = JObject.Parse(GClinic);
                    cltyp = data.ClinicTypeID;
                }

                //-------PASTMED/
                long pastmed1 = 0;
                PastMedHistory oPastMedHistory = new PastMedHistory();
                var pastmedvar1 = from s in db.PastMedHistories.Where(p => p.PID == pid) select new { s.PMHID };

                foreach (var item in pastmedvar1)
                {

                    pastmed1 = item.PMHID;
                    oPastMedHistory.PMHID = pastmed1;
                }




                oPastMedHistory.PID = pid;
                oPastMedHistory.PMHDetail = pastmedhys;
                oPastMedHistory.Drughst = drughyst;
                ///////////

                long cathy1 = 0;
                CatReferal oCatReferal = new CatReferal();
                var cathyvar1 = from s in db.CatReferals.Where(p => p.PDID == PDID) select new { s.reffid };

                foreach (var item in cathyvar1)
                {

                    cathy1 = item.reffid;
                    oCatReferal.reffid = cathy1;
                }


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

                var clincd = from v in db.Clinic_Master where (v.ClinicTypeID == cltyp) where (v.LocationID == locid) select new { v.Clinic_ID };
                string clinkid = "";
                Patient_Detail patient_Detail2 = new Patient_Detail();
                TranferDetail oTranferDetails = new TranferDetail();
                if (clincd != null)
                {
                    foreach (var p in clincd)
                    {
                        clinkid = p.Clinic_ID;
                    }
                }
                if (!String.IsNullOrEmpty(clinkid))
                {

                    patient_Detail2.PDID = indi.CreatePDID(patient_Detail.PID);
                    patient_Detail2.PID = patient_Detail.PID;
                    patient_Detail2.OPDID = clinkid;
                    patient_Detail2.Present_Complain = patient_Detail.Present_Complain;
                    patient_Detail2.CreatedBy = userid.ToString();
                    //patient_Detail.ModifiedDate= DateTime.Now;
                    //patient_Detail.ModifiedBy = userid.ToString();
                    //patient_Detail.ModifiedMachine = userid.ToString();
                    patient_Detail2.CreatedDate = DateTime.Now.Date;
                    //patient_Detail.CreatedMachine = userid.ToString();
                    patient_Detail2.Status = 1;
                    db.Patient_Detail.Add(patient_Detail2);

                    oTranferDetails.PDID = patient_Detail.PDID;
                    oTranferDetails.ToLoc = clinkid;
                    oTranferDetails.FromLoc = opdid;
                    oTranferDetails.TransferDate = DateTime.Now;
                    oTranferDetails.TransID = indi.CreateTransID(patient_Detail.PDID);
                    oTranferDetails.TransStatus = 1;
                }
                var clincd1 = from v in db.Clinic_Master where (v.ClinicTypeID == 20) where (v.LocationID == locid) select new { v.Clinic_ID };
                string clinkid1 = "";
                if (clincd1 != null)
                {
                    foreach (var p in clincd1)
                    {
                        clinkid1 = p.Clinic_ID;
                    }
                }

                if (pntstatus.Equals("10") || pntstatus.Equals("13"))
                {

                    patient_Detail2.PDID = indi.CreatePDID(patient_Detail.PID);
                    patient_Detail2.PID = patient_Detail.PID;
                    patient_Detail2.OPDID = clinkid1;
                    patient_Detail2.Present_Complain = patient_Detail.Present_Complain;
                    patient_Detail2.CreatedBy = userid.ToString();
                    //patient_Detail.ModifiedDate= DateTime.Now;
                    //patient_Detail.ModifiedBy = userid.ToString();
                    //patient_Detail.ModifiedMachine = userid.ToString();
                    patient_Detail2.CreatedDate = DateTime.Now.Date;
                    //patient_Detail.CreatedMachine = userid.ToString();
                    patient_Detail2.Status = 1;
                    db.Patient_Detail.Add(patient_Detail2);

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
                    oVital.CreatedDate = DateTime.Now.Date;
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
                    var oldtestcnt = db.Hypersensivities.Where(d => d.HyperTypeSubID == p.hsubtype1).Where(d => d.PID == patient_Detail.PID).ToList().Count;
                    if (oldtestcnt < 1)
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




                var trand = from v in db.TranferDetails where (v.PDID == patient_Detail.PDID) where (v.ToLoc == opdid) select new { v.TransID, v.PDID, v.ToLoc, v.FromLoc, v.TransferDate };
                String TransID = "";
                String PDID1 = "";
                String ToLoc = "";
                String FromLoc = "";
                DateTime TransferDate = DateTime.Now;
                foreach (var p in trand)
                {
                    TransID = p.TransID;
                    PDID1 = p.PDID;
                    ToLoc = p.ToLoc;
                    FromLoc = p.FromLoc;
                    TransferDate = (DateTime)p.TransferDate;
                }
                TranferDetail oTranferDetails1 = new TranferDetail();
                oTranferDetails1.TransID = TransID;
                oTranferDetails1.TransStatus = 0;
                oTranferDetails1.ToLoc = ToLoc;
                oTranferDetails1.PDID = patient_Detail.PDID;
                oTranferDetails1.FromLoc = FromLoc;
                oTranferDetails1.TransferDate = TransferDate;


                ExamineGeneral oExamineGeneral = new ExamineGeneral();
                oExamineGeneral.Alert = Alert;
                oExamineGeneral.Arcus = Arcus;
                oExamineGeneral.BuidLean = Lean;
                oExamineGeneral.BuildAverage = Average;
                oExamineGeneral.BuildObese = Obese;
                oExamineGeneral.CariousTeech = Carius;
                oExamineGeneral.Clubbing = Clubbing;
                oExamineGeneral.ColdandClammy = Clammy;
                oExamineGeneral.Confused = Confused;
                oExamineGeneral.Cyanosis = Cyanosis;
                oExamineGeneral.Drowsy = Drowsy;
                oExamineGeneral.Dyspnoea = Dyspnoea;
                oExamineGeneral.ExtremitiesWarm = warm;
                oExamineGeneral.Icterus = Icterus;
                oExamineGeneral.IsPain = IsPain;
                oExamineGeneral.OralUlcers = OralUlcers;
                oExamineGeneral.PainLocation = PainLoc;
                oExamineGeneral.PainScore = PainScore;
                oExamineGeneral.Pallor = Pallor;
                oExamineGeneral.PedalOedema = Oedema;
                oExamineGeneral.SkinDetails = SkinDetails;
                oExamineGeneral.SkinMoles = Moles;
                oExamineGeneral.SkinNavei = Navi;
                oExamineGeneral.SkinRashes = Rashes;
                oExamineGeneral.SkinScars = Scars;
                oExamineGeneral.SkinTattoos = Tattoos;
                oExamineGeneral.SkinUlcers = Ulcers;
                oExamineGeneral.SkinWounds = Wounds;
                oExamineGeneral.Unconscius = Unconscious;
                oExamineGeneral.Xanthomata = Xanthomata;
                oExamineGeneral.PDID = PDID;


                ExamineAbdominal oExamineAbdominal = new ExamineAbdominal();
                oExamineAbdominal.PDID = PDID;
                oExamineAbdominal.BowelSoundsAbsent = BowelAbsent;
                oExamineAbdominal.BowelSoundsExaggerated = BowelExaggereted;
                oExamineAbdominal.BowelSoundsNormal = BowelNormal;
                oExamineAbdominal.BowelSoundsSluggish = BowelSluggish;
                oExamineAbdominal.Distension = Distension;
                oExamineAbdominal.FreeFluid = FreeFluid;
                oExamineAbdominal.Liver = Liver;
                oExamineAbdominal.Other = AbdOther;
                oExamineAbdominal.OtherLumps = OtherLumps;
                oExamineAbdominal.PulsatileLumps = PulsatileLumps;
                oExamineAbdominal.Spleen = Spleen;


                ExamineCardiovascular oExamineCardiovascular = new ExamineCardiovascular();
                oExamineCardiovascular.PDID = PDID;
                oExamineCardiovascular.ApexBeat = ApexBeat;
                oExamineCardiovascular.BrachialLeft = BrachialLeft;
                oExamineCardiovascular.BrachialRight = BrachialRight;
                oExamineCardiovascular.CarotidBruit = Carotidbruit;
                oExamineCardiovascular.DorsalisPedisLeft = DorsalisLeft;
                oExamineCardiovascular.DorsalisPedisRight = DorsalisRight;
                oExamineCardiovascular.FemoralLeft = FemoralLeft;
                oExamineCardiovascular.FemoralRight = FemoralRight;
                oExamineCardiovascular.HeartSounds = HeartSounds;
                oExamineCardiovascular.JVPcmH2O = JVP;
                oExamineCardiovascular.Murmurs = Murmurs;
                oExamineCardiovascular.Other = CardioOther;
                oExamineCardiovascular.PosteriorTibialLeft = TibialLeft;
                oExamineCardiovascular.PosteriorTibialRight = TibialRight;
                if (RhythmIreg == "false")
                {
                    oExamineCardiovascular.PulseRhythmIregular = "true";
                    oExamineCardiovascular.PulseRhythmRegular = "false";
                }
                else
                {
                    oExamineCardiovascular.PulseRhythmIregular = "false";
                    oExamineCardiovascular.PulseRhythmRegular = RhythmReg;
                }

                oExamineCardiovascular.PulseVolume = Pulsevolume;
                oExamineCardiovascular.RadialLeft = RadialLeft;
                oExamineCardiovascular.RadialRight = RadialRight;

                ExamineCentralNervou oExamineCNervous = new ExamineCentralNervou();
                oExamineCNervous.PDID = PDID;
                oExamineCNervous.ConciousnessAlert = NvAlert;
                oExamineCNervous.ConciousnessConfused = NvConfused;
                oExamineCNervous.ConciousnessDrowsy = NvDrowsy;
                oExamineCNervous.ConciousnessUnconscious = NvUnconscious;
                oExamineCNervous.CranialNerves = CranialNerves;
                oExamineCNervous.GCS = NvGcs;
                oExamineCNervous.MentalStateConfused = NvMentalConfused;
                oExamineCNervous.MentalStateOriented = NvOriented;
                oExamineCNervous.MentalStateRational = NvRational;
                oExamineCNervous.MotorSystem = MotorSystem;
                oExamineCNervous.NotClinicallyIndicated = NvNotIndicated;
                oExamineCNervous.Reflexes = NvReflexes;
                oExamineCNervous.SensorySystem = SensorySystem;
                oExamineCNervous.SpeechAphasia = NvAphasia;
                oExamineCNervous.SpeechDysarthria = NvDysarthria;
                oExamineCNervous.SpeechNormal = NvNormal;

                ExamineRespiratory oExamineRespiratory = new ExamineRespiratory();
                oExamineRespiratory.PDID = PDID;
                oExamineRespiratory.Auscultation = Auscultation;
                oExamineRespiratory.ChestMovement = ChestMovement;
                oExamineRespiratory.MediastinalShiftApex = Apex;
                oExamineRespiratory.MediastinalShiftTrachea = Trachea;
                oExamineRespiratory.PercussionNote = Percussion;
                oExamineRespiratory.PFR_LMin = PFR;
                oExamineRespiratory.SpO2_FiO2 = SpO2;



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
                        int objcountl1 = objl.Count();
                        foreach (var q in objl)
                        {



                            var lablist = from t in db.Lab_Report where(t.PDID == PDID)
                                          join o in db.Lab_SubCategory on t.LabTestID equals o.LabTestID
                                          join v in db.Lab_MainCategory on o.CategoryID equals v.CategoryID
                                          select new
                                          { v.CategoryName,o.CategoryID, t.PDID, t.TestSID }; ;
                            var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();

                            var oldtestcntl = labl.ToList().Count;

                            if (oldtestcntl < 1)
                            {
                                Lab_Report oLab_Reports = new Lab_Report();
                                oLab_Reports.LabTestID = q.LabTestID;
                                oLab_Reports.RequestedLocID = locid;
                                oLab_Reports.RequestedTime = DateTime.Now;
                                oLab_Reports.PDID = PDID;
                                oLab_Reports.TestSID = PDID + "x0";
                                oLab_Reports.Issued = "0";
                                oLab_Reports.IsPrint = "0";
                                oLab_Reports.Lab_Index = Guid.NewGuid().ToString();
                                objLab_Report[i] = oLab_Reports;
                                i++;

                            }
                            else
                            {
                                Lab_Report oLab_Reports = new Lab_Report();
                                oLab_Reports.LabTestID = q.LabTestID;
                                oLab_Reports.RequestedLocID = locid;
                                oLab_Reports.RequestedTime = DateTime.Now;
                                oLab_Reports.PDID = PDID;
                                oLab_Reports.Issued = "0";
                                oLab_Reports.IsPrint = "0";
                                oLab_Reports.TestSID = PDID + "x" + oldtestcntl;
                                oLab_Reports.Lab_Index = Guid.NewGuid().ToString();
                                objLab_Report[i] = oLab_Reports;
                                i++;
                            }

                        }
                    }
                }
                #endregion
                var objs = (dynamic)null;
                int objcount = 0;
                Drug_Prescription[] objDrug = new Drug_Prescription[1000];
                if (!String.IsNullOrEmpty(ditems))
                {


                    objs = JsonConvert.DeserializeObject<List<Drugreader>>(ditems);

                    objcount = objs.Count;


                    int i = 0;

                    foreach (Drugreader p in objs)
                    {

                        var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                        if (oldtestcnt < 1)
                        {
                            Drug_Prescription oDrug_Prescription = new Drug_Prescription();
                            oDrug_Prescription.PDID = PDID;
                            oDrug_Prescription.Ps_Index = Guid.NewGuid().ToString();
                            oDrug_Prescription.Dose = p.dDose;
                            oDrug_Prescription.Method = p.dMethod;
                            oDrug_Prescription.Route = p.dRoute;
                            oDrug_Prescription.ItemNo = p.dItemno;
                            oDrug_Prescription.Duration = p.dDuration;
                            oDrug_Prescription.RequestedLocID = locid;
                            oDrug_Prescription.LocID = opdid;
                            oDrug_Prescription.Date_Time = DateTime.Now.Date;
                            objDrug[i] = oDrug_Prescription;
                            i++;
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
                    var oldtestcnt = db.Sick_Category.Where(d => d.CatID == p.scatid).Where(d => d.PDID == PDID).Where(d => d.Date == dt1).ToList().Count;
                    if (oldtestcnt < 1)
                    {
                        Sick_Category oSick_Category = new Sick_Category();
                        oSick_Category.PDID = PDID;
                        oSick_Category.CatIndex = indi.CreateSCID(h, PDID);
                        oSick_Category.CatPeriod = p.sdays;
                        oSick_Category.Date = DateTime.Now.Date;
                        oSick_Category.LocID = locid;
                        oSick_Category.CatID = p.scatid;

                        objSick[h] = oSick_Category;
                        h++;
                    }

                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        string General = "";
                        var Generalvar = from s in db.ExamineGenerals.Where(p => p.PDID == PDID) select new { s.PDID };

                        foreach (var item in Generalvar)
                        {

                            General = item.PDID;
                        }
                        if (!String.IsNullOrEmpty(General))
                        {
                            db.Entry(oExamineGeneral).State = EntityState.Modified;
                        }
                        else
                        {
                            db.ExamineGenerals.Add(oExamineGeneral);
                        }
                        string Abdominal = "";
                        var Abdominalvar = from s in db.ExamineAbdominals.Where(p => p.PDID == PDID) select new { s.PDID };

                        foreach (var item in Abdominalvar)
                        {

                            Abdominal = item.PDID;
                        }
                        if (!String.IsNullOrEmpty(Abdominal))
                        {
                            db.Entry(oExamineAbdominal).State = EntityState.Modified;
                        }
                        else
                        {
                            db.ExamineAbdominals.Add(oExamineAbdominal);
                        }

                        string Cardiovascular = "";
                        var Cardiovascularvar = from s in db.ExamineCardiovasculars.Where(p => p.PDID == PDID) select new { s.PDID };

                        foreach (var item in Cardiovascularvar)
                        {

                            Cardiovascular = item.PDID;
                        }
                        if (!String.IsNullOrEmpty(Cardiovascular))
                        {
                            db.Entry(oExamineCardiovascular).State = EntityState.Modified;
                        }
                        else
                        {
                            db.ExamineCardiovasculars.Add(oExamineCardiovascular);
                        }

                        string CNervous = "";
                        var CNervousvar = from s in db.ExamineCardiovasculars.Where(p => p.PDID == PDID) select new { s.PDID };

                        foreach (var item in CNervousvar)
                        {

                            CNervous = item.PDID;
                        }
                        if (!String.IsNullOrEmpty(CNervous))
                        {
                            db.Entry(oExamineCNervous).State = EntityState.Modified;
                        }
                        else
                        {
                            db.ExamineCentralNervous.Add(oExamineCNervous);
                        }

                        string Respiratory = "";
                        var Respiratoryvar = from s in db.ExamineCardiovasculars.Where(p => p.PDID == PDID) select new { s.PDID };

                        foreach (var item in Respiratoryvar)
                        {

                            Respiratory = item.PDID;
                        }
                        if (!String.IsNullOrEmpty(Respiratory))
                        {
                            db.Entry(oExamineRespiratory).State = EntityState.Modified;
                        }
                        else
                        {
                            db.ExamineRespiratories.Add(oExamineRespiratory);
                        }

                        ///////---------------
                        string pastmed = "";
                        var pastmedvar = from s in db.PastMedHistories.Where(p => p.PID == pid) select new { s.PID };

                        foreach (var item in pastmedvar)
                        {

                            pastmed = item.PID;
                        }
                        if (!String.IsNullOrEmpty(pastmed))
                        {
                            db.Entry(oPastMedHistory).State = EntityState.Modified;
                        }
                        else
                        {
                            db.PastMedHistories.Add(oPastMedHistory);
                        }

                        ///------------------

                        string catref = "";
                        var catrefvar = from s in db.CatReferals.Where(p => p.PDID == PDID) select new { s.PDID };

                        foreach (var item in catrefvar)
                        {

                            catref = item.PDID;
                        }
                        if (!String.IsNullOrEmpty(catref))
                        {
                            db.Entry(oCatReferal).State = EntityState.Modified;
                        }
                        else
                        {
                            db.CatReferals.Add(oCatReferal);
                        }

                        ///-------------------
                        db.Entry(patient_Detail).State = EntityState.Modified;


                        //20171114
                        objLab_Report = objLab_Report.Where(x => x != null).ToArray();
                        db.Lab_Report.AddRange(objLab_Report);
                        db.Vitals.AddRange(objVital);
                        objHyp = objHyp.Where(x => x != null).ToArray();
                        db.Hypersensivities.AddRange(objHyp);
                        objDrug = objDrug.Where(x => x != null).ToArray();
                        db.Drug_Prescription.AddRange(objDrug);

                        objcatd = objcatd.Where(x => x != null).ToArray();
                        db.CatDiagLists.AddRange(objcatd);
                        objSick = objSick.Where(x => x != null).ToArray();
                        db.Sick_Category.AddRange(objSick);
                        if (pntstatus == "7")
                        {
                            db.TranferDetails.Add(oTranferDetails);
                            db.Entry(oTranferDetails1).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                        err = "Saved";


                        //db.SaveChanges();
                        // db.Vitals.AddRange(objVital);

                        //db.Patient_Detail.Add(patient_Detail);

                    }
                    catch (Exception ex)
                    {
                        err = ex.ToString();

                    }
                }

                return Json(err, JsonRequestBehavior.AllowGet);
            }
            public JsonResult loadchildimg(string id)
            {
                string imageDataURL = "";
                try
                {







                    df = GetEncoderInfo("image/jpeg");
                    char[] MyChar = { '/', '"', ' ' };
                    string NewString = id.Trim(MyChar);
                    var ser = from s in db.ServicePersonnelProfiles.Where(p => p.ServiceNo == NewString) select new { s.ProfilePicture };
                    byte[] imageByteData;

                    foreach (var item in ser)
                    {
                        imageByteData = item.ProfilePicture;
                        MemoryStream ms;
                        using (var ms1 = new MemoryStream(imageByteData))
                        {
                            Image fg = Image.FromStream(ms1);
                            using (var img = fg)
                            {
                                ms = compress(img, 30, df);
                            }
                        }



                        using (var compressedImage = Image.FromStream(ms))
                        {
                            imageByteData = ImageToByteArray(compressedImage);
                        }



                        string imageBase64Data = Convert.ToBase64String(imageByteData);
                        base64String = imageBase64Data;

                        imageDataURL = string.Format("data:image/png;base64,{0}", base64String);
                        ViewBag.ImageData = imageDataURL;

                    }
                    return Json(imageDataURL, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                }
                return null;
            }
            //public JsonResult loadchildimg1(string id)
            //{
            //    try
            //    {
            //        String locid = (String)Session["userlocid2"];
            //        String ips = "";
            //        var iplist = from s in db.ImagePaths
            //                         //join b in dbhrms.ranks on s.Rank equals b.RANK1
            //                     where s.locid == locid
            //                     select new { s.filepath };
            //        foreach (var item in iplist)
            //        {
            //            ips = item.filepath;
            //        }
            //        df = GetEncoderInfo("image/jpeg");
            //        char[] MyChar = { '/', '"', ' ' };
            //        string NewString = id.Trim(MyChar);
            //        byte[] imageByteData;
            //        string imageDataURL = "";
            //        int? stype = 0;
            //        string svcno = "";
            //        var PersonResultList1 = from s in dbp2.ServicePersonnelProfiles

            //                                where s.ServiceNo == NewString && (s.Service_Type == 1001 || s.Service_Type == 1002 || s.Service_Type == 1003 || s.Service_Type == 1004)
            //                                select new { s.Service_Type };
            //        foreach (var item in PersonResultList1)
            //        {
            //            stype = item.Service_Type;
            //        }
            //        if (stype == 0)
            //        {
            //            var PersonResultList = from s in dbhrms.ServicePersonnelProfiles
            //                                       //join b in dbhrms.ranks on s.Rank equals b.RANK1
            //                                   where s.ActiveNo == NewString
            //                                   select new { s.Service_Type };

            //            foreach (var item in PersonResultList)
            //            {
            //                stype = item.Service_Type;
            //            }

            //        }

            //        if (stype != 0)
            //        {
            //            if (stype == 1001)
            //            {

            //                svcno = Regex.Match(NewString, @"\d+").Value;
            //                svcno = "100" + svcno;
            //            }
            //            else if (stype == 1002)    // Officer Woman
            //            {
            //                svcno = Regex.Match(NewString, @"\d+").Value;
            //                svcno = "101" + svcno;

            //            }
            //            else if (stype == 1003 || stype == 1004)
            //            {
            //                string[] NewString1 = NewString.Split('/');
            //                svcno = "200" + NewString1[1];
            //            }
            //            else if (stype == 1005)
            //            {

            //                svcno = Regex.Match(NewString, @"\d+").Value;
            //                svcno = "102" + svcno;
            //            }
            //            else if (stype == 1006)
            //            {
            //                string[] NewString1 = NewString.Split('/');
            //                svcno = "103" + NewString1[1];
            //            }
            //            else if (stype == 1007 || stype == 1008)   //Vol.Svc No
            //            {
            //                svcno = Regex.Match(NewString, @"\d+").Value;
            //                svcno = "202" + svcno;

            //            }
            //        }


            //        try
            //        {
            //        using (WebClient client2 = new WebClient())
            //        {



            //            byte[] imageBytes = client2.DownloadData(ips + svcno + ".jpg");

            //            // Convert byte[] to Base64 String
            //            base64String = Convert.ToBase64String(imageBytes);

            //        }
            //        //using (Image image = Image.FromFile(ips + svcno + ".jpg"))
            //        //{
            //        //    using (MemoryStream m = new MemoryStream())
            //        //    {
            //        //        image.Save(m, image.RawFormat);
            //        //        byte[] imageBytes = m.ToArray();

            //        //        // Convert byte[] to Base64 String
            //        //        base64String = Convert.ToBase64String(imageBytes);

            //        //    }
            //        //}
            //    }
            //        catch (Exception ex)
            //        {

            //        }

            //        if (String.IsNullOrEmpty(base64String))
            //        {



            //            var ser = from s in db.ServicePersonnelProfiles.Where(p => p.ServiceNo == NewString) select new { s.ProfilePicture };

            //            foreach (var item in ser)
            //            {
            //                imageByteData = item.ProfilePicture;
            //                MemoryStream ms;
            //                using (var ms1 = new MemoryStream(imageByteData))
            //                {
            //                    Image fg = Image.FromStream(ms1);
            //                    using (var img = fg)
            //                    {
            //                        ms = compress(img, 30, df);
            //                    }
            //                }



            //                using (var compressedImage = Image.FromStream(ms))
            //                {
            //                    imageByteData = ImageToByteArray(compressedImage);
            //                }

            //                string imageBase64Data = Convert.ToBase64String(imageByteData);
            //                base64String = imageBase64Data;

            //            }

            //        }
            //        imageDataURL = string.Format("data:image/png;base64,{0}", base64String);
            //        ViewBag.ImageData = imageDataURL;

            //        return Json(imageDataURL, JsonRequestBehavior.AllowGet);
            //    }
            //    catch (Exception ex)
            //    {
            //        return Json("", JsonRequestBehavior.AllowGet);
            //    }


            //}
            // GET: Vital Types
            public JsonResult GetRelationships()
            {
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
        public JsonResult GetPatients(string id, string relet)
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
                char[] MyChar1 = { '/', '"', ' ' };
                string imageDataURL = "";
                string locid = (String)Session["userloc"];
                int NewString1 = Convert.ToInt32(relet.Trim(MyChar1));
                List<getnursedata> nlist = new List<getnursedata>();
                DateTime dd = DateTime.Now.Date;


                DataTable oDataSet1 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
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
"	  max(case when c.Sex = 1 then 'MALE' ELSE 'FEMALE' end) sx ,max(b.ServiceStatus) serv" +
"      FROM[MMS].[dbo].[Patient] as c " +
"   left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
"    left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
"   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
"   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
"    left join[MMS].[dbo].[MedicalCategory] as k on b.SNo=k.SNo " +
 "   where   c.serviceno='" + NewString + "' and c.RelationshipType='" + NewString1 + "' group by c.PID ";
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


                else if (svty == "Serving" && relationp == "1" && sikcnt < 1)
                {


                    iserror = "4";



                }
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


        public JsonResult GetOPDPatient(string id)
            {

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
                var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                foreach (var item in serW)
                {

                    locid = item.LocationID;
                }


                string pid = "";
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                var result = (dynamic)null;
                var bg = (dynamic)null;

                var a1 = from s in db.Patient_Detail.Where(p => p.PDID == NewString)
                         join d in db.Patients on s.PID equals d.PID
                         join f in db.PersonalDetails on d.ServiceNo equals f.ServiceNo
                         join j in db.MedicalCategories on f.SNo equals j.SNo
                         join k in db.RelationshipTypes on d.RelationshipType equals k.RTypeID
                         select new { s.PID, s.PDID, s.Patient.Initials, s.Patient.Surname, s.Patient.ServiceNo, s.Present_Complain, s.Patient.rank1.RNK_NAME, s.History_OtherComplain, s.History_PresentComplain, s.Other_Complain, j.MedicalCategory1, f.BloodGroup, s.OPD_Diagnosis, k.Relationship };


                foreach (var item in a1)
                {
                    pid = item.PID;

                    var a5 = from s in db.Patient_Detail.Where(p => p.PID == item.PID)
                             join b in db.Clinic_Master on s.OPDID equals b.Clinic_ID into cs
                             from b in cs.DefaultIfEmpty()
                             join c in db.CatDaignosis on s.DaignosisID equals c.dgid into com
                             from c in com.DefaultIfEmpty()

                             select new { s.PDID, s.CreatedDate, b.Clinic_Detail, c.dgdetail };
                    var a6 = from s in db.Patient_Detail.Where(p => p.PDID == NewString)
                             join b in db.Clinic_Master on s.OPDID equals b.Clinic_ID into cs
                             from b in cs.DefaultIfEmpty()
                             join c in db.CatDaignosis on s.DaignosisID equals c.dgid into com
                             from c in com.DefaultIfEmpty()
                             join d in db.PastMedHistories on s.PID equals d.PID into com1
                             from d in com1.DefaultIfEmpty()
                             join e in db.CatReferals on s.PDID equals e.PDID into com2
                             from e in com2.DefaultIfEmpty()
                             select new { s.PDID, s.Patient.Initials, s.Patient.Surname, s.Present_Complain, s.Patient.rank1.RNK_NAME, s.CreatedDate, s.OPD_Diagnosis, s.OPDID, b.Clinic_Detail, c.dgdetail, d.PMHDetail, d.Drughst, e.PlanofMgt, e.ReffNote };

                    var a2 = from t in db.Hypersensivities.Where(w => w.PID == item.PID) select new { t.HypMainCategory.HypersenceMainCategory, t.HypMainCategory.HypersenceMainCatID, t.HypersenseDetail };
                    int i = 0;



                    var vitval = from u in db.Vitals.Where(r => r.PDID == NewString) select new { u.VID, u.PDID, u.Reading_Time, u.Vital_Type.ReadingUnit, u.Vital_Type.VitalType, u.VitalValues };

                    var a3 = from u in db.Vitals.Where(r => r.PDID == NewString) select new { u.VID, u.PDID, u.Reading_Time, u.Vital_Type.ReadingUnit, u.Vital_Type.VitalType, u.VitalValues };
                    //foreach (var item1 in a5)
                    //{
                    //    var ser = from s in db.Patient_Detail.Where(p => p.PID == pid)
                    //              join b in db.Drug_Prescription on s.PDID equals b.PDID into cs
                    //              from b in cs.DefaultIfEmpty()
                    //              join c in db.Vitals on s.PDID equals c.PDID into com
                    //              from c in com.DefaultIfEmpty()
                    //              join d in db.Hypersensivities on s.PID equals d.PID into dom
                    //              from d in dom.DefaultIfEmpty()
                    //              join e in db.Lab_Report on s.PDID equals e.PDID into eom
                    //              from e in eom.DefaultIfEmpty()

                    //              select new { s.PDID, s.Patient.Initials, s.CreatedDate, s.PID, s.Patient.Surname,s.Patient.rank1.RNK_NAME, s.Other_Complain, s.Present_Complain, c.Vital_Type.VitalType, c.Vital_Type.ReadingUnit, c.VitalValues, d.HypersenseDetail, e.Lab_SubCategory.SubCategoryID, e.testResult };
                    //    var ser2 = ser.GroupBy(x => x.PDID).Select(grp => grp.FirstOrDefault()).ToList();










                    //}

                    var a4 = from u in db.Vitals.Where(r => r.PDID == NewString) select new { u.VID, u.PDID, u.Reading_Time, u.Vital_Type.ReadingUnit, u.Vital_Type.VitalType, u.VitalValues };
                    if (i != 0)
                    {
                        a3 = a4.Concat(a3);

                    }
                    else
                    {

                        a3 = a4;
                    }
                    i++;
                    var siccat = from u in db.Sick_Category.Where(p => p.PDID == NewString)
                                 join b in db.Sick_CategoryType on u.CatID equals b.CatID
                                 select new
                                 { u.CatPeriod, b.Category_Type };


                    //var lab = from t in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.Patient_Detail.PDID == NewString)
                    //          select new
                    //          { t.Lab_SubCategory.Lab_MainCategory.CategoryName, t.Lab_SubCategory.Lab_MainCategory.CategoryID, t.PDID };
                    //var labl = lab.GroupBy(c => new { c.CategoryName, c.CategoryID }).Select(grp => grp.FirstOrDefault()).ToList();
                    var lablist = from t in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.PDID == NewString)
                                  join h in db.Lab_SubCategory on t.LabTestID equals h.LabTestID
                                  join v in db.Lab_MainCategory on h.CategoryID equals v.CategoryID
                                  select new
                                  {v.CategoryName, h.CategoryID, t.PDID, t.TestSID }; ;
                    var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();

                    var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                    var serc = from s in db.Drug_Prescription.Where(p => p.PDID == NewString) orderby s.ItemNo select new { s.Ps_Index, s.ItemNo, s.DrugMethod.MethodDetail, s.DrugRoute.RouteDetail, s.Duration, s.Dose, s.PDID };
                    int ij = 0;

                    foreach (var itm in serc)
                    {


                        var items2 = from d in db.EPASPharmacyItems.Where(p => p.itemno == itm.ItemNo) select new { d.itemdescription, d.itemno };
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
                    var t1 = serc.ToList();
                    var t2 = items.ToList();

                    var joined = from it1 in t1 join it2 in t2 on it1.ItemNo equals it2.itemno select new { it1.ItemNo, it1.MethodDetail, it1.RouteDetail, it1.Dose, it1.Duration, it2.itemdescription, it1.PDID };

                    var exabd = db.ExamineAbdominals.Where(p => p.PDID == NewString);
                    var excdv = db.ExamineCardiovasculars.Where(p => p.PDID == NewString);
                    var exctn = db.ExamineCentralNervous.Where(p => p.PDID == NewString);
                    var exgrl = db.ExamineGenerals.Where(p => p.PDID == NewString);
                    var exrpr = db.ExamineRespiratories.Where(p => p.PDID == NewString);


                    result = new { s1 = siccat.ToList(), l1 = labl.ToList(), d1 = joined.ToList(), b1 = a1.ToList(), b2 = a2.ToList(), b3 = a3.ToList(), b4 = a5.ToList(), b5 = a6.ToList(), exabd = exabd.ToList(), excdv = excdv.ToList(), exctn = exctn.ToList(), exgrl = exgrl.ToList(), exrpr = exrpr.ToList(), vitval1 = vitval.ToList() };
                }



                //var ser = from s in db.Patient_Detail.Where(p => p.PID == pid)
                //          join b in db.Drug_Prescription on s.PDID equals b.PDID into cs from b in cs.DefaultIfEmpty()
                //          join c in db.Vitals on s.PDID equals c.PDID into com from c in com.DefaultIfEmpty()
                //          join d in db.Hypersensivities on s.PID equals d.PID into dom
                //          from d in dom.DefaultIfEmpty()
                //          join e in db.Lab_Report on s.PDID equals e.PDID into eom
                //          from e in eom.DefaultIfEmpty()
                //          select new { s.PID,   s.Other_Complain, s.Present_Complain, c.Vital_Type.VitalType,c.Vital_Type.ReadingUnit,c.VitalValues };

                //var temp = from p in db.Patient_Detail.Where(p => p.PDID == NewString)
                //           group p by p.PDID into pg
                //           join bp in db.Hypersensivities on pg.FirstOrDefault().PID equals bp.PID
                //           join cp in db.Vitals on pg.FirstOrDefault().PDID equals cp.PDID
                //           select new { pg.FirstOrDefault().Present_Complain, pg.FirstOrDefault().Patient.Surname, bp.HypersensivityType, cp.Vital_Type, cp.VitalValues };
                //var temp = from p in db.Patient_Detail.Where(p => p.PDID == NewString)
                //           join bp in db.Hypersensivities on p.PID equals bp.PID
                //           join cp in db.Vitals on p.PDID equals cp.PDID
                //           select new { p.Present_Complain, p.Patient.Surname, bp.HypersensivityType , cp.Vital_Type };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            // GET: /Get Service Personnel Details/
            //public JsonResult GetServicePersonnel(string id)
            //{
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
            public JsonResult PatientHystory(string id)
            {
                char[] MyChar = { '/', '"', ' ' };
                String modif = "";

                if (id != null)
                {

                    id = id.Trim(MyChar);

                    var siccat = from u in db.Sick_Category.Where(p => p.PDID == id)
                                 join b in db.Sick_CategoryType on u.CatID equals b.CatID
                                 select new
                                 { u.CatPeriod,b.Category_Type };
                    var dicat = from u in db.CatDiagLists.Where(p => p.PDID == id)
                                join d in db.CatDaignosis on u.dgid equals d.dgid into com
                                from d in com.DefaultIfEmpty()
                                select new
                                { u.PDID, d.dgdetail };
                    var a1 = from s in db.Patient_Detail.Where(p => p.PDID == id)
                             join d in db.PastMedHistories on s.PID equals d.PID into com1
                             from d in com1.DefaultIfEmpty()
                             join e in db.CatReferals on s.PDID equals e.PDID into com2
                             from e in com2.DefaultIfEmpty()
                             select new { s.PID, s.PDID, s.Patient.Initials, s.Patient.Surname, s.Present_Complain, s.History_OtherComplain, s.History_PresentComplain, s.Other_Complain, s.OPD_Diagnosis, d.PMHDetail, e.PlanofMgt, e.ReffNote, e.DrugHistory, s.ModifiedBy };
                    foreach (var itm in a1)
                    {
                        modif = itm.ModifiedBy;
                    }
                    int modid = Convert.ToInt32(modif);
                    var docn = from u in db.Users.Where(p => p.UserID == modid)
                               select new
                               { u.UserName, u.Salutation, u.FName, u.LName };
                    var vitallist = from s in db.Vitals
                                    where s.PDID == id
                                    select new { s.Vital_Type.VitalType, s.VitalValues, s.Reading_Time };
                    var examlist = from s in db.ExamineAbdominals
                                   join d in db.ExamineCardiovasculars on s.PDID equals d.PDID into com
                                   from d in com.DefaultIfEmpty()
                                   join e in db.ExamineCentralNervous on s.PDID equals e.PDID into con
                                   from e in con.DefaultIfEmpty()
                                   join f in db.ExamineGenerals on s.PDID equals f.PDID into coo
                                   from f in coo.DefaultIfEmpty()
                                   join g in db.ExamineRespiratories on s.PDID equals g.PDID into cop
                                   from g in cop.DefaultIfEmpty()
                                   join h in db.VisualExaminations on s.PDID equals h.PDID into coq
                                   from h in coq.DefaultIfEmpty()
                                   where s.PDID == id
                                   select new { s, d, e, f, g, h };
                    // var lab = from t in db.Lab_Report.Where(p => p.PDID == id)
                    //          select new
                    //          { t.Lab_SubCategory.Lab_MainCategory.CategoryName, t.Lab_SubCategory.Lab_MainCategory.CategoryID, t.PDID };
                    //var labl = lab.GroupBy(c => new { c.CategoryName, c.CategoryID }).Select(grp => grp.FirstOrDefault()).ToList();
                    var lablist = from t in db.Lab_Report.Where(p => p.PDID == id)
                                  join h in db.Lab_SubCategory on t.LabTestID equals h.LabTestID.ToString()
                                  join v in db.Lab_MainCategory on h.CategoryID equals v.CategoryID
                                  select new
                                  { v.CategoryName,v.CategoryID, t.PDID, t.TestSID }; ;
                    var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();
                    var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                    var serc = from s in db.Drug_Prescription
                               join d in db.Clinic_Master on s.LocID equals d.Clinic_ID into com
                               from d in com.DefaultIfEmpty()
                               join e in db.Clinic_Master on s.LocID equals e.Clinic_ID into cn
                               from e in cn.DefaultIfEmpty()
                               where (s.PDID == id)
                               orderby s.ItemNo
                               select new { s.Ps_Index, s.ItemNo, s.DrugMethod.MethodDetail, s.DrugRoute.RouteDetail, s.Duration, s.Dose, s.PDID, d.Clinic_Detail };
                    int ij = 0;

                    foreach (var itm in serc)
                    {


                        var items2 = from d in db.EPASPharmacyItems.Where(p => p.itemno == itm.ItemNo) select new { d.itemdescription, d.itemno };
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
                    var t1 = serc.ToList();
                    var t2 = items.ToList();

                    var joined = from it1 in t1 join it2 in t2 on it1.ItemNo equals it2.itemno select new { it1.ItemNo, it1.MethodDetail, it1.RouteDetail, it1.Dose, it1.Duration, it2.itemdescription, it1.PDID, it1.Clinic_Detail };

                    var result = new { h1 = siccat.ToList(), h2 = labl.ToList(), h3 = joined.ToList(), h4 = a1.ToList(), h5 = vitallist.ToList(), h6 = examlist.ToList(), h7 = docn.ToList(), h8 = dicat.ToList() };

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(JsonRequestBehavior.AllowGet);

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