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
using static MMS.Controllers.DhsController;


namespace MMS.Controllers
{
    public class WardController : Controller
    {
        private MMSEntities db = new MMSEntities();
       // private P3Context dbhrms = new P3Context();
       // private EPASContext db = new EPASContext();
        private int Userid;
        private string err;
        ImageCodecInfo df;

      //  private P2Context dbp2 = new P2Context();
        private string base64String;
        private int sikcnt;
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        string sqlQuery;

        string sertno = "";

        // GET: Ward
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult view2()
        {
            return View();
        }
        public ActionResult view3()
        {
            return View();
        }
        public JsonResult GetWardTypes()
        {
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT ID,Ward_Type FROM Ward_Types ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var wardTypes = oDataSet.AsEnumerable()
    .Select(dataRow => new getwt
    {
        ID = dataRow.Field<int>("ID"),
        Ward_Type = dataRow.Field<string>("Ward_Type"),
    }).ToList();
            return Json(wardTypes, JsonRequestBehavior.AllowGet);

        }
        public class getwt
        {
            public int ID { get; set; }
           
            public string Ward_Type { get; set; }
        }
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
        //        sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
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
" where  c.ServiceNo like'%" + id + "%' AND wd.status= 15 " +

" and wd.opdid='" + opdid + "' group by a.PDID, a.CreatedDate order by crdate ";
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
            }).ToList();

                    var pageNumber = page ?? 1;
                    onePageOfProducts = lid.ToList();
                }
                else
                {
                    if (opdid.Contains("CL"))
                    {
                      
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
" where   wd.status= 15 " +
" and wd.opdid='" + opdid + "' group by a.PDID, a.CreatedDate order by crdate ";
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

        //get ward drugs details
        public ActionResult ViewWardDrugs(int? page, string id, string currentFilter)
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



      

                locid = (String)Session["userloc"];
                if (!String.IsNullOrEmpty(id))
                {
                    id = id.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(id))
                {
                    DateTime dt1 = DateTime.Now.Date;
               
                    DateTime dd = DateTime.Now.Date;
                    DataTable oDataSet4 = new DataTable();
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

"     relasiondet,  max(x.Date_Time)phdate  FROM[MMS].[dbo].[Patient_Detail] as a with(nolock) inner join [dbo].Ward_Details wd ON wd.PDID = a.PDID   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
"  left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
"  left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
"   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
"   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
 " LEFT JOIN Sick_Category sc ON a.pdid = sc.PDID  " +
  "left join CatDiagList cdl ON a.PDID = cdl.PDID " +
 "left join CatDaignosis cds ON cdl.dgid = cds.dgid " +
 "left join ward_details WDD ON a.PDID = WDD.PDID  " +
 "left join Ward_Types WT ON  WD.Ward_No=WT.Id inner join [dbo].[Ward_Drug_Prescription] as x on wd.PDID=x.PDID " +
" where  c.ServiceNo like'%" + id + "%'  " +

" and wd.opdid='" + opdid + "' group by a.PDID, a.CreatedDate order by crdate ";
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
                        sqlQuery = "      SELECT COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1   "+
 "   and b.Surname != '0'  then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),  " +
     "   max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end),   " +
    "	 max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end),   " +
    "	  max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname)) " +
            "	sname  ,max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
            "	,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
                "	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(a.CreatedDate) crdate, max(h.Relationship) " +

                        "    relasiondet,  max(x.Date_Time)phdate FROM[MMS].[dbo].[Patient_Detail] as a with(nolock) " +

                          "   inner join[dbo].Ward_Details wd ON wd.PDID = a.PDID left join[MMS].[dbo].[Patient] as c " +
                          "  on a.pid= c.pid   left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo= b.ServiceNo " +

                         "    left join " +

                         "    [MMS].[dbo].[SpouseDetails] as e on b.SNo= e.SNo    left join[MMS].[dbo].[Children] as f on " +

                           "  b.SNo= f.SNo left join[MMS].[dbo].[parents] as g on b.SNo= g.SNo    left join[MMS].[dbo]. " +

                           "  [RelationshipType] as h on h.RTypeID= c.RelationshipType LEFT join [dbo].[Ward_Drug_Prescription] as x on wd.PDID= x.PDID where " +
                           "    x.Issued= 0 " +
" and wd.opdid='" + opdid + "' group by a.PDID, a.CreatedDate order by crdate ";
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
                   // pcomoplian = dataRow.Field<string>("pcomoplian"),
                    //pstatus = dataRow.Field<int?>("pstatus").ToString(),
                    relasiont = dataRow.Field<int?>("relasiont"),
                    crdate = dataRow.Field<DateTime?>("crdate"),
                    pidp = dataRow.Field<string>("pidp"),
                    relasiondet = dataRow.Field<string>("relasiondet"),
                }).ToList();



                        ///db.Patient_Detail.Include(p => p.Patient).Where(p => p.Status == 1 || p.Status == 5).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                        //  patient_Detail = patient_Detail.GroupBy(t => t.pdids).Select(grp => grp.FirstOrDefault()).OrderByDescending(s=>s.crdate);
                        var pageNumber = page ?? 1;

                        onePageOfProducts = lid.OrderByDescending(p => p.crdate).ToPagedList(pageNumber, 10);
                    


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

        public JsonResult SavepatientWard(string suitems, string bitems, string AbdOther, string reftohosp, string planofmgt, string drughyst, string hitems, string pastmedhys, string items, string sitems, string litems, string ditems, string Present_Complain, string History_PresentComplain, string Other_Complain, string History_OtherComplain, string PDID, string pntStatus, string GClinic, string dgn1, string genex, string cardex, string cenex, string resex, string othex, string abdex, string drugins, string remarks, string wardNo, string bedNo, string mgtPlan, string OPD_Diagnosis, string PatientCom)
        {
            int wdid = 0;
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
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
            if (!String.IsNullOrEmpty(dgn1)) { AbdOther = AbdOther.Trim(MyChar); }

            if (!String.IsNullOrEmpty(OPD_Diagnosis))
            {
                OPD_Diagnosis = OPD_Diagnosis.Trim(MyChar);
                OPD_Diagnosis = Regex.Replace(OPD_Diagnosis, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(pntStatus))
            {
                pntStatus = pntStatus.Trim(MyChar);
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
            if (!String.IsNullOrEmpty(suitems))
            {
                suitems = suitems.Trim(MyChar);
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
            if (!String.IsNullOrEmpty(remarks))
            {
                remarks = remarks.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(wardNo))
            {
                wardNo = wardNo.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(bedNo))
            {
                bedNo = bedNo.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(planofmgt))
            {
                planofmgt = planofmgt.Trim(MyChar);
                planofmgt = Regex.Replace(planofmgt, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(PatientCom))
            {
                PatientCom = PatientCom.Trim(MyChar);
                PatientCom = Regex.Replace(PatientCom, @"\\t|\\n|\\r", "");
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
            if (!String.IsNullOrEmpty(genex))
            {
                genex = genex.Trim(MyChar);

                genex = Regex.Replace(genex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(cardex))
            {
                cardex = cardex.Trim(MyChar);
                cardex = Regex.Replace(cardex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(cenex))
            {
                cenex = cenex.Trim(MyChar);
                cenex = Regex.Replace(cenex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(resex))
            {
                resex = resex.Trim(MyChar);
                resex = Regex.Replace(resex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(othex))
            {
                othex = othex.Trim(MyChar);
                othex = Regex.Replace(othex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(abdex))
            {
                abdex = abdex.Trim(MyChar);
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
            var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            foreach (var item in ser)
            {
            }
            var pidvar = from s in db.Patient_Detail.Where(p => p.PDID == PDID) select new { s.PID, s.CreatedDate, s.PatientCatID, s.SubjectID, s.OPDID, s.ModifiedBy };

            foreach (var item in pidvar)
            {

                pid = item.PID;
                crdt = item.CreatedDate;
                pcat = item.PatientCatID;
                subcat = item.SubjectID;
                opdid = item.OPDID;
                modus = item.ModifiedBy;
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

            int cltyp = 0;
            int waid = 0;
            int Daid = -1;
            int mgtPlanStatus = 1;
            int patComStatus = 1;
            IndexGeneration indi = new IndexGeneration();

            if (!String.IsNullOrEmpty(wardNo))
            {
                dynamic data = JObject.Parse(wardNo);
                waid = data.ID;
            }


            Ward_Details oWard_Details = new Ward_Details();

            oWard_Details.PDID = PDID;
            oWard_Details.Ward_No = waid.ToString();
            oWard_Details.Bed_No = bedNo;
            oWard_Details.Remarks = remarks;
            oWard_Details.Status = Convert.ToInt32(pntStatus);
            oWard_Details.Created_By = userid.ToString();
            oWard_Details.Created_Date = DateTime.Now;
            oWard_Details.Diagnosis = OPD_Diagnosis;
            oWard_Details.OPDID = opdid;

            if (!String.IsNullOrEmpty(GClinic))
            {
                dynamic data = JObject.Parse(GClinic);
                cltyp = data.ClinicTypeID;
            }




            var PSIndex = db.Ward_Details.Where(p => p.PDID == PDID).ToList().Count;
            var wardId = from s in db.Ward_Details.Where(p => p.PDID == PDID) select new { s.WDID };
            if (Convert.ToInt16(PSIndex.ToString()) > 0)
            {
                foreach (var item in wardId)
                {
                    wdid = item.WDID;
                }
            }
            else
            {
                //if (!String.IsNullOrEmpty(modus))
                //{
                //    if (!modus.Trim().Equals(userid.ToString()))
                //    {
                //        PDID = indi.CreatePDID(pid);
                //    }
                //}

                db.Ward_Details.Add(oWard_Details);
                db.SaveChanges();
                wdid = oWard_Details.WDID;
            }
            Ward_Mgt_Plan wardPlan = new Ward_Mgt_Plan();
            wardPlan.PDID = wdid.ToString();
            wardPlan.Description = planofmgt;

            Ward_Patient_Complain wardComplain = new Ward_Patient_Complain();
            wardComplain.WardId = Convert.ToInt32(wdid.ToString());
            wardComplain.Description = PatientCom;
            wardComplain.Date = DateTime.Now;
            wardComplain.InvestigateBy = userid.ToString();



            if (OPD_Diagnosis != "")
            {
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " UPDATE Ward_Details SET Diagnosis = '" + OPD_Diagnosis + "' WHERE WDID = '" + wdid + "' ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
            }
            if (planofmgt == "")
            {
                mgtPlanStatus = 0;
            }
            if (PatientCom == "")
            {
                patComStatus = 0;
            }
            wardPlan.Date = DateTime.Now;
            wardPlan.InvestigateBy = userid.ToString();

            string tempWdid = "";
            var temppdid = from s in db.Ward_Mgt_Plan.Where(p => p.PDID == wardId.ToString()) select new { s.PDID };
            //foreach (var iy in temppdid)
            //{

            //    tempWdid = iy.PDID;
            //}

            if (!String.IsNullOrEmpty(dgn1))
            {
                dynamic dataDai = JObject.Parse(dgn1);
                Daid = dataDai.dgid;
            }

            if (Daid != -1)
            {
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " INSERT INTO Ward_Daignosis(dgid,PDID) VALUES('" + Daid + "','" + PDID + "') ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
            }

            else
            {
                //need to disscuss
            }

            ///////////////////////////////////////
            var objsd = (dynamic)null;
            int objcountd = 0;
            Ward_Drug_Prescription[] objDrugd = new Ward_Drug_Prescription[1000];
            if (!String.IsNullOrEmpty(suitems))
            {


                objsd = JsonConvert.DeserializeObject<List<Drugreader1>>(suitems);

                objcountd = objsd.Count;


                int i = 0;

                foreach (Drugreader1 p in objsd)
                {

                    var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.suItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                    if (oldtestcnt < 1)
                    {
                        Ward_Drug_Prescription oDrug_Prescription = new Ward_Drug_Prescription();
                        oDrug_Prescription.PDID = PDID;
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
                        objDrugd[i] = oDrug_Prescription;

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

            //-------PASTMED/
            long pastmed1 = 0;
            long cathy1 = 0;
            CatReferal oCatReferal = new CatReferal();

            oCatReferal.PDID = wdid.ToString();
            oCatReferal.PlanofMgt = planofmgt;
            oCatReferal.ReffNote = reftohosp;
            ///////////
            var objb = JsonConvert.DeserializeObject<List<breader>>(bitems);
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
                oTranferDetails.PDID = wdid.ToString();
                oTranferDetails.ToLoc = clinkid;
                oTranferDetails.FromLoc = opdid;
                oTranferDetails.TransferDate = DateTime.Now;
                oTranferDetails.TransID = indi.CreateTransID(PDID);
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

            if (pntStatus.Equals("10") || pntStatus.Equals("13"))
            {
                oTranferDetails.PDID = wdid.ToString();
                oTranferDetails.ToLoc = clinkid1;
                oTranferDetails.FromLoc = opdid;
                oTranferDetails.TransferDate = DateTime.Now;
                oTranferDetails.TransID = indi.CreateTransID(PDID);
                oTranferDetails.TransStatus = 1;
            }
            var objsv = JsonConvert.DeserializeObject<List<Vitalreader>>(items);
            int objcountv = objsv.Count;
            Ward_Vitals[] objVital = new Ward_Vitals[objcountv];
            int i1 = 0;

            foreach (Vitalreader p in objsv)
            {

                Ward_Vitals oVital = new Ward_Vitals();
                oVital.PDID = wdid.ToString();
                oVital.VID = indi.CreateVIDWard(i1, wdid.ToString());
                oVital.CreatedBy = userid.ToString();
                oVital.CreatedDate = DateTime.Now;
                oVital.LocationID = locid;
                oVital.LocID = opdid;

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
                var oldtestcnt = db.Hypersensivities.Where(d => d.HyperTypeSubID == p.hsubtype1).Where(d => d.PID == pid).ToList().Count;
                if (oldtestcnt < 1)
                {
                    Hypersensivity oHypersensivity = new Hypersensivity();
                    oHypersensivity.PID = pid;
                    oHypersensivity.HypersenseID = indi.CreateHID(j, pid);
                    oHypersensivity.HyperTypeSubID = p.htype1;
                    //oHypersensivity.RSubID = p.hrsubtype1;
                    //oHypersensivity.SeverityID = Convert.ToInt32(p.hstype1);
                    oHypersensivity.ModifiedDate = DateTime.Now;
                    oHypersensivity.HypersenseDetail = p.hstype1;


                    objHyp[j] = oHypersensivity;
                    j++;
                }
            }
     
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

                foreach (Labreader lp in objsl)
                {

                    Lab_Report oLab_Report = new Lab_Report();
                    var objl = from s in db.Lab_SubCategory.Where(f => f.CategoryID == lp.labid) select new { s.LabTestID };
                    int objcountl1 = objl.Count();
                    foreach (var q in objl)
                    {
                        var lablist = from t in db.Lab_Report.Where(pf => pf.PDID == PDID.ToString())
                                      join x in db.Lab_SubCategory on t.LabTestID equals x.LabTestID
                                      join y in db.Lab_MainCategory on x.CategoryID equals y.CategoryID

                                      select new
                                      { y.CategoryName,y.CategoryID, t.PDID, t.TestSID }; ;
                        var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();

                        var oldtestcntl = labl.ToList().Count;

                        if (oldtestcntl < 1)
                        {
                            Lab_Report oLab_Reports = new Lab_Report();
                            oLab_Reports.LabTestID = q.LabTestID;
                            oLab_Reports.RequestedLocID = locid;
                            oLab_Reports.RequestedTime = DateTime.Now;
                            oLab_Reports.PDID = PDID.ToString();
                            oLab_Reports.TestSID = PDID.ToString() + "x0";
                            oLab_Reports.Issued = "0";
                            oLab_Reports.Isemail = 0;
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
                            oLab_Reports.PDID = PDID.ToString();
                            oLab_Reports.Issued = "0";
                            oLab_Reports.IsPrint = "0";
                            oLab_Reports.Isemail = 0;
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
            Ward_Drug_Prescription[] objDrug = new Ward_Drug_Prescription[1000];
            if (!String.IsNullOrEmpty(ditems))
            {


                objs = JsonConvert.DeserializeObject<List<Drugreader>>(ditems);

                objcount = objs.Count;
                int i = 0;

                foreach (Drugreader vp in objs)
                {

                    var oldtestcntdrug = db.Ward_Drug_Prescription.Where(d => d.ItemNo == vp.dItemno).Where(d => d.PDID == wdid.ToString()).Where(d => d.issuedQuantity != "0").ToList().Count;
                    //if (oldtestcntdrug < 1)
                    //{
                        Ward_Drug_Prescription oDrug_Prescription = new Ward_Drug_Prescription();
                        oDrug_Prescription.PDID = wdid.ToString();
                        oDrug_Prescription.Ps_Index = Guid.NewGuid().ToString();
                        oDrug_Prescription.Dose = vp.dDose;
                        oDrug_Prescription.Method = vp.dMethod;
                        oDrug_Prescription.Route = vp.dRoute;
                        oDrug_Prescription.ItemNo = vp.dItemno;
                        //oDrug_Prescription.MethodType = Convert.ToInt32(p.dStockTypeID);
                        oDrug_Prescription.Duration = vp.dDuration;
                        oDrug_Prescription.RequestedLocID = locid;
                        oDrug_Prescription.LocID = opdid;
                        oDrug_Prescription.Date_Time = DateTime.Now.Date;
                        objDrug[i] = oDrug_Prescription;
                        i++;
                    //}
                }
            }
            var objsic = JsonConvert.DeserializeObject<List<Sickreader>>(sitems);
            int objcountsic = objsic.Count;
            Ward_Sick_Category[] objSick = new Ward_Sick_Category[objcountsic];
            int h = 0;

            foreach (Sickreader tp in objsic)
            {
                DateTime dt1 = DateTime.Now.Date;
                var oldtestcntSick = db.Ward_Sick_Category.Where(d => d.CatID == tp.scatid).Where(d => d.PDID == wdid.ToString()).Where(d => d.Date == dt1).ToList().Count;
                if (oldtestcntSick < 1)
                {
                    Ward_Sick_Category oSick_Category = new Ward_Sick_Category();
                    oSick_Category.PDID = wdid.ToString();
                    oSick_Category.CatIndex = indi.CreateSCID(h, wdid.ToString());
                    oSick_Category.CatPeriod = tp.sdays;
                    oSick_Category.Date = Convert.ToDateTime(DateTime.Now.ToString());
                    oSick_Category.LocID = locid;
                    oSick_Category.CatID = tp.scatid;

                    objSick[h] = oSick_Category;

                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "INSERT INTO Ward_Sick_Category(CatIndex,PDID,CatID,CatPeriod,Date,LocID)" +
                        "VALUES('" + oSick_Category.CatIndex + "','" + oSick_Category.PDID + "','" + oSick_Category.CatID + "','" + oSick_Category.CatPeriod + "','" + oSick_Category.Date + "','" + oSick_Category.LocID + "') ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlCommand.ExecuteNonQuery();
                    oSqlConnection.Close();


                    h++;
                }
            }

            //need uncomment
            if (ModelState.IsValid)
            {
                try
                {

                    if (!String.IsNullOrEmpty(modus))
                    {
                        if (mgtPlanStatus != 0)
                        {
                            db.Ward_Mgt_Plan.Add(wardPlan);
                            db.SaveChanges();
                        }
                    }
                    if (!String.IsNullOrEmpty(modus))
                    {
                        if (patComStatus != 0)
                        {
                            db.Ward_Patient_Complain.Add(wardComplain);
                            db.SaveChanges();
                        }
                    }


                    string catref = "";
                    var catrefvar = from s in db.CatReferals.Where(cp => cp.PDID == wdid.ToString()) select new { s.PDID };

                    foreach (var item in catrefvar)
                    {

                        catref = item.PDID;
                    }
                    if (!String.IsNullOrEmpty(catref))
                    {
                        //db.Entry(oCatReferal).State = EntityState.Modified;
                    }
                    else
                    {
                        db.CatReferals.Add(oCatReferal);
                    }


                    objLab_Report = objLab_Report.Where(x => x != null).ToArray();
                    db.Lab_Report.AddRange(objLab_Report);
                    objVital = objVital.Where(x => x != null).ToArray();
                    db.Ward_Vitals.AddRange(objVital);

                    objHyp = objHyp.Where(x => x != null).ToArray();
                    db.Hypersensivities.AddRange(objHyp);

                    objDrug = objDrug.Where(x => x != null).ToArray();
                    db.Ward_Drug_Prescription.AddRange(objDrug);

                    if (pntStatus == "7")
                    {
                        db.TranferDetails.Add(oTranferDetails);
                    }


                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " UPDATE Ward_Details SET Status = '" + pntStatus + "' WHERE WDID = '" + wdid + "' ";
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
                    err = ex.ToString();
                }
            }
            return Json(err, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PatientHystoryWard(string id,string id2)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            String modif = "";
            String pdetn = "<label>";
            String medyr = "";
            String pdetn2 = "";
            String dgnos = "";
            string iserror = "";
            string pid = "";
            string PDID = "";
            string wId = "";
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
                    id2 = id2.Trim(MyChar);

                    var ax1 = from s in db.Patients
                              join h in db.PersonalDetails on s.ServiceNo equals h.ServiceNo
                              join j in db.MedicalCategories on h.SNo equals j.SNo
                              
                              join z in db.RelationshipTypes on s.RelationshipType equals z.RTypeID
                                  where(s.PID == id) select new getdocdata { PID = s.PID, Initials = s.Initials, Surname = s.Surname, ServiceNo = s.ServiceNo, RNK_NAME = s.rank1.RNK_NAME, Category = j.MedicalCategory1, BloodType = h.BloodGroup, Relationship = z.Relationship, sv = s.Service_Type };


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
                                pdetn = pdetn + iteme.Relationship + " of Service No: " + iteme.ServiceNo + " <br/>";
                            }
                            else
                            {
                                pdetn = pdetn + "Service No: " + iteme.ServiceNo + "<br/>";
                            }

                            // pdetn = pdetn + "Service No: " + iteme.ServiceNo + "<br />";
                            pdetn = pdetn + iteme.RNK_NAME + "    " + iteme.Initials + " " + iteme.Surname + "<br/>";
                            pdetn = pdetn + "Blood Group: " + iteme.BloodType + "<br/>";
                            if (!string.IsNullOrEmpty(iteme.Category.ToString()))
                            {
                                pdetn = pdetn + "Catagory: " + iteme.Category + "<br/>";
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
                            pdetn = pdetn + "Drug History: " + itmd.Drughst + "<br/>";
                        }
                        if (!string.IsNullOrEmpty(itmd.PMHDetail) && itmd.Drughst != "null")
                        {
                            pdetn = pdetn + "Diagnosed Medical Conditions: " + itmd.PMHDetail + "<br/>";
                        }



                    }
                    var g5 = from s in db.Vw_MedicalBoard.Where(p => p.SNo == sertno)
                             orderby s.dateOfBoard ascending

                             select new { s.dateOfBoard, s.findingRemarks };
                    pdetn = pdetn + "Medical Board Details  <br/> ";
                    foreach (var itmg in g5)
                    {


                        pdetn = pdetn + "Date:" + itmg.dateOfBoard.ToString() + "   " + itmg.findingRemarks + "<br/>";


                    }
                    var aircrewlist = from s in db.VwAirCrews
                                      join b in db.Patients on s.SvcNo equals b.ServiceNo
                                      where b.PID == id
                                      orderby s.acmRID descending
                                      select new { s.SvcNo, s.MES, s.NextDate, s.Remarks, s.WEF, s.acmRID };
                    if (aircrewlist.Count() > 0)
                    {
                        pdetn = pdetn + "  Aircrew medical  <br/> ";
                        foreach (var itmvt in aircrewlist)
                        {
                            pdetn = pdetn + " MES:  " + itmvt.MES + " WEF: " + itmvt.WEF + " Next Date" + itmvt.NextDate + "<br/>";
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
                            pdetn = pdetn + ithyp.HypersenceMainCategory + " " + ithyp.HypersenseDetail + "<br/>";
                        }


                    }
                    pdetn = pdetn + "<br/>";

                    id2 = id2.Trim(MyChar);

                    var a5 = from s in db.Patient_Detail.Where(p => p.PDID == id2)
                             orderby s.CreatedDate ascending

                             select new { s.PDID, s.CreatedDate };
                    int i = 0;
                    string[] stringArray;
                    stringArray = new string[100];
                    foreach (var itmn in a5)
                    {


                        pdetn = pdetn + "<br/>";


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
                                pdetn = pdetn + "Date:" + itm.CreatedDate.ToString("dd/MM/yyyy") + "<br/>";
                            }
                            if (!string.IsNullOrEmpty(itm.Present_Complain) && itm.Present_Complain != "null")
                            {
                                pdetn = pdetn + "Presenting Complain:" + itm.Present_Complain + "<br/>";
                            }
                            if (!string.IsNullOrEmpty(itm.History_PresentComplain) && itm.History_PresentComplain != "null")
                            {
                                pdetn = pdetn + " History of Presenting Complain:" + itm.History_PresentComplain + "<br/>";
                            }
                            if (!string.IsNullOrEmpty(itm.ReffNote) && itm.ReffNote != "null")
                            {
                                pdetn = pdetn + " Reffaral Note:" + itm.ReffNote + "<br/>";
                            }
                            if (!string.IsNullOrEmpty(itm.PlanofMgt) && itm.PlanofMgt != "null")
                            {
                                pdetn = pdetn + "Notes:" + itm.PlanofMgt + "<br/>";
                            }
                            if (!string.IsNullOrEmpty(itm.GeneralEntries) && itm.GeneralEntries != "null")
                            {
                                pdetn = pdetn + "General Entries:" + itm.GeneralEntries + "<br/>";
                            }
                            if (!string.IsNullOrEmpty(itm.OPD_Diagnosis) && itm.OPD_Diagnosis != "null")
                            {
                                pdetn = pdetn + "Diagnosis:" + itm.OPD_Diagnosis + dgnos + "<br/>";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(dgnos) && dgnos != "null")
                                {
                                    pdetn = pdetn + "Diagnosis:" + dgnos + "<br/>";
                                }
                            }
                        }
                        int modid = Convert.ToInt32(modif);



                        var vitallist = from s in db.Vitals
                                        where s.PDID == itmn.PDID
                                        select new { s.Vital_Type.VitalType, s.VitalValues, s.Reading_Time };
                        foreach (var itmvt in vitallist)
                        {
                            pdetn = pdetn + itmvt.VitalType + "  " + itmvt.VitalValues + " " + itmvt.Reading_Time + "<br/>";
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
                                pdetn = pdetn + "Abdorminal Examination:" + itmem.s.Other + "<br/>";
                            }
                        }

                        foreach (var itmem in examd)
                        {

                            if (itmem.d != null && !string.IsNullOrEmpty(itmem.d.Other) && itmem.d.Other != "null")
                            {
                                pdetn = pdetn + "Cardio Vascular Examination:" + itmem.d.Other + "<br/>";
                            }
                        }

                        foreach (var itmem in exame)
                        {
                            if (itmem.e != null && !string.IsNullOrEmpty(itmem.e.Other) && itmem.e.Other != "null")
                            {
                                pdetn = pdetn + "Central Nervious Examination:" + itmem.e.Other + "<br/>";
                            }
                        }

                        foreach (var itmem in examf)
                        {
                            if (itmem.f != null && !string.IsNullOrEmpty(itmem.f.Other) && itmem.f.Other != "null")
                            {
                                pdetn = pdetn + "General Examination:" + itmem.f.Other + "<br/>";
                            }
                        }

                        foreach (var itmem in examg)
                        {
                            if (itmem.g != null && !string.IsNullOrEmpty(itmem.g.Other) && itmem.g.Other != "null")
                            {
                                pdetn = pdetn + "Respiratory Examination:" + itmem.g.Other + "<br/>";
                            }
                        }

                        foreach (var itmem in examh)
                        {
                            if (itmem.h != null && !string.IsNullOrEmpty(itmem.h.Other) && itmem.h.Other != "null")
                            {
                                pdetn = pdetn + "Other Examination:" + itmem.h.Other + "<br/>";
                            }
                        }
                        // var lab = from t in db.Lab_Report.Where(p => p.PDID == id)
                        //          select new
                        //          { t.Lab_SubCategory.Lab_MainCategory.CategoryName, t.Lab_SubCategory.Lab_MainCategory.CategoryID, t.PDID };
                        //var labl = lab.GroupBy(c => new { c.CategoryName, c.CategoryID }).Select(grp => grp.FirstOrDefault()).ToList();
                        var lablist = from t in db.Lab_Report.Where(p => p.PDID == itmn.PDID && p.Issued == "1")
                                      join h in db.Lab_SubCategory on t.LabTestID equals h.LabTestID
                                      join v in db.Lab_MainCategory on h.CategoryID equals v.CategoryID
                                      join x in db.Patient_Detail on t.PDID equals x.PDID
                                      join y in db.Patients on x.PID equals y.PID
                                      join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID
                                      select new
                                      { v.CategoryName, v.CategoryID, t.PDID, t.TestSID }; ;
                        var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();
                        if (labl.Count > 0)
                        {
                            pdetn = pdetn + "Lab Tests: ";
                        }
                        foreach (var itmlb in labl)
                        {
                            if (itmlb != null && !string.IsNullOrEmpty(itmlb.CategoryName))
                            {
                                pdetn = pdetn + itmlb.CategoryName + ",";
                            }

                        }
                        pdetn = pdetn + "<br/>";
                        var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                        var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
                        var serc = from s in db.Ward_Drug_Prescription.Where(p => p.PDID == itmn.PDID) join b in db.DrugMethods on s.Method equals b.MethodID orderby s.ItemNo select new { s.Ps_Index, s.ItemNo, b.MethodDetail, b.DrugMethodCount,  s.Duration, s.Dose, s.PDID, s.MethodType };
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
                        var joined = from it1 in t1 join it2 in t2 on it1.ItemNo equals it2.itemno select new { Ps_Index = it1.Ps_Index, ItemNo = it1.ItemNo, MethodDetail = it1.MethodDetail, mcnt = it1.DrugMethodCount,  Dose = it1.Dose, Duration = it1.Duration, itemdescription = it2.itemdescription, PDID = it1.PDID, mt = it1.MethodType };

                        var joined1 = from it1 in t1 join it2 in t3 on it1.ItemNo equals it2.DrugID.ToString() select new { Ps_Index = it1.Ps_Index, ItemNo = it1.ItemNo, MethodDetail = it1.MethodDetail, mcnt = it1.DrugMethodCount, Dose = it1.Dose, Duration = it1.Duration, itemdescription = it2.ItemDescription, PDID = it1.PDID, mt = it1.MethodType };
                        var u1 = joined.ToList();
                        var u2 = joined1.ToList();
                        var joined3 = u1.Concat(u2);
                        if (joined3.ToList().Count > 0)
                        {
                            pdetn = pdetn + "Prescription:<br/>";
                        }
                        foreach (var itmdr in joined3)
                        {
                            if (itmdr != null && !string.IsNullOrEmpty(itmdr.itemdescription))
                            {
                                pdetn = pdetn + itmdr.itemdescription + "  " + itmdr.MethodDetail + "     Duration:" + itmdr.Duration + " Days <br/>";
                            }
                        }
                        var siccat = from u in db.Sick_Category.Where(p => p.PDID == itmn.PDID)
                                     join b in db.Sick_CategoryType on u.CatID equals b.CatID
                                     select new
                                     { u.CatPeriod, b.Category_Type, u.Date };

                        foreach (var itmsc in siccat)
                        {
                            pdetn = pdetn + itmsc.Category_Type + "  Duration:" + itmsc.CatPeriod + "   Effective Date:" + itmsc.Date.ToString() + "<br/>";
                        }

                        var docn = from u in db.Users.Where(p => p.UserID == modid)
                                   select new
                                   { u.UserName, u.Salutation, u.FName, u.LName };

                        foreach (var itmdc in docn)
                        {
                            pdetn = pdetn + "Investigated by:" + itmdc.Salutation + " " + itmdc.FName + " " + itmdc.LName + "<br/>";
                        }


                        int dfgl = pdetn.Split('>').Length;
                        int dfgl2 = pdetn.Split('/').Length;
                        int totv = (dfgl2 - dfgl) * 4 + dfgl;

                        int slen = pdetn.Length;
                        //  if (slen > 300)
                        //  {
                        // pdetn = " <div  id=a" + io + " >" + pdetn + " </div>";
                        pdetn2 = pdetn2 + pdetn;
                        //pdetn = "";
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
                    pdetn = pdetn + "</label>";
                    var result = new { h1 = pdetn };
                    CreateDocument(servi, pdetn);
                    return Json(new { h1 = pdetn }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }       
        }
        public JsonResult PatientHystoryWardDrugs(string id, string id2)
        {
            var joined3 = (dynamic)null;
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            String modif = "";
            String pdetn = "<label>";
            String medyr = "";
            String pdetn2 = "";
            String dgnos = "";
            string iserror = "";
            string pid = "";
            string PDID = "";
            string wId = "";
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
                    id2 = id2.Trim(MyChar);           
                    var a5 = from s in db.Patient_Detail.Where(p => p.PDID == id2)
                             orderby s.CreatedDate ascending

                             select new { s.PDID, s.CreatedDate };
                    int i = 0;
                    string[] stringArray;
                    stringArray = new string[100];
                    foreach (var itmn in a5)
                    {
                        pdetn = pdetn + "<br/>";
                        var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                        var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
                        var serc = from s in db.Ward_Drug_Prescription.Where(p => p.PDID == itmn.PDID && p.Issued == 0) join b in db.DrugMethods on s.Method equals b.MethodID join c in db.DrugRoutes on s.Route equals c.RouteID orderby s.ItemNo select new { s.Ps_Index, s.ItemNo, b.MethodDetail, b.DrugMethodCount, c.RouteDetail, s.Duration, s.Dose, s.PDID, s.MethodType };
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
                        var joined = from it1 in t1 join it2 in t2 on it1.ItemNo equals it2.itemno select new { Ps_Index = it1.Ps_Index, ItemNo = it1.ItemNo, RouteDetail= it1.RouteDetail, MethodDetail = it1.MethodDetail, mcnt = it1.DrugMethodCount, Dose = it1.Dose, Duration = it1.Duration, itemdescription = it2.itemdescription, PDID = it1.PDID, mt = it1.MethodType };

                        var joined1 = from it1 in t1 join it2 in t3 on it1.ItemNo equals it2.DrugID.ToString() select new { Ps_Index = it1.Ps_Index, ItemNo = it1.ItemNo, RouteDetail = it1.RouteDetail, MethodDetail = it1.MethodDetail, mcnt = it1.DrugMethodCount, Dose = it1.Dose, Duration = it1.Duration, itemdescription = it2.ItemDescription, PDID = it1.PDID, mt = it1.MethodType };
                        var u1 = joined.ToList();
                        var u2 = joined1.ToList();
                         joined3 = u1.Concat(u2);
                        //    if (joined3.ToList().Count > 0)
                        //    {
                        //        pdetn = pdetn + "Prescription:<br/>";
                        //    }
                        //    foreach (var itmdr in joined3)
                        //    {
                        //        if (itmdr != null && !string.IsNullOrEmpty(itmdr.itemdescription))
                        //        {
                        //            pdetn = pdetn + itmdr.itemdescription + "  " + itmdr.MethodDetail + "     Duration:" + itmdr.Duration + " Days <br/>";
                        //        }
                        //    }
                        //    int dfgl = pdetn.Split('>').Length;
                        //    int dfgl2 = pdetn.Split('/').Length;
                        //    int totv = (dfgl2 - dfgl) * 4 + dfgl;

                        //    int slen = pdetn.Length;
                        //    pdetn2 = pdetn2 + pdetn;
                        //}
                        //if (Relationship == "SELF")
                        //{
                        //    pdetn = pdetn + medyr;
                        //}
                        //pdetn = pdetn + "</label>";
                        //var result = new { h1 = pdetn };
                        //CreateDocument(servi, pdetn);
                    
                   
                    }
                    return Json(new { h1 = joined3 }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }

        private void CreateDocument(string sno, string txt)
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
                object filename = @"d:\unfolder\" + sno + ".docx";
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

        public JsonResult GetOPDPatientWard(string id, string id2)
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
                if (opdid.Contains(""))
                {

                }
                //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                //foreach (var item in serW)
                //{

                //    locid = item.LocationID;
                //}
                 locid = (String)Session["userloc"];
                string iserror = "";
                string pid = "";
                int? sex = 0;
                string PDID = "";
                string svcn = "";
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
                var wdidlist = new List<getdocdata>();
                DateTime dd = DateTime.Now.Date;
                string wdid="";
                string pntStatus = "";
                try
                {
                    DataTable oDataSetwdId = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " select WDID,Remarks,Ward_No,Bed_No,Status,BHTNo,Diagnosis from ward_Details where pdId = '" + NewString + "' ";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetwdId);
                    //  oSqlConnection.Close();
                    wdidlist = oDataSetwdId.AsEnumerable()
           .Select(dataRow => new getdocdata
           {
               wId = dataRow.Field<int>("WDID"),
               remarks = dataRow.Field<string>("Remarks"),
               wardNo = dataRow.Field<string>("Ward_No"),
               bedNo = dataRow.Field<string>("Bed_No"),
               BHTNo = dataRow.Field<string>("BHTNo"),
               Diagnosis = dataRow.Field<string>("Diagnosis"),
               pntStatus = dataRow.Field<int>("Status")
           }).ToList();

                    foreach (var wddd in wdidlist)
                    {
                        wdid = wddd.wId.ToString();
                        pntStatus = wddd.pntStatus.ToString();
                    }

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
                    throw ex;
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
                        sqlQuery = "     SELECT top 1 (b.BloodGroup) bg ,(k.MedicalCategory) medcat , " +
          "    (c.Service_Type) svt, (c.Sex)sex,(c.ChildNo)chno,       " +
        "	   COALESCE(NULLIF(concat((case when c.RelationshipType = 1    and b.Surname != '0'  then b.Surname end), " +
          "     (case when c.RelationshipType = 2 then e.SpouseName  end), (case when c.RelationshipType = 5 and " +

          "      c.DateOfBirth = f.DOB  then f.ChildName  end),    (case when c.RelationshipType = 3 and g.Relationship = 'Father' " +

         "         then g.ParentName end),    (case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName end)),  " +
        "		  ''), (c.surname)) sname  ,   COALESCE(NULLIF(concat((case when c.RelationshipType = 1      then b.DateOfBirth end), " +
           "          (case when c.RelationshipType = 2 then c.DateOfBirth  end), (case when c.RelationshipType = 5    then f.DOB " +

         "             end)), ''), (c.DateOfBirth))      dob, 	(case when c.RelationshipType = 1 then b.Rank end) rnkname, " +
        "			   (c.ServiceNo)sno    ,(case when c.RelationshipType = 1 then b.Initials end)  inililes,  " +

            "		   (c.RelationshipType)relasiont     ,  (h.Relationship)relasiondet,   (m.ReqStatus)mauth   FROM[MMS].[dbo].[Patient] as " +
        "      c  with(nolock)    left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
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
                    }


                    var PersonResultList1 = new List<getdocdata>();
                    List<getdocdata> df = new List<getdocdata>();

                    try
                    {
                        DataTable oDataSet31 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();
                        sqlQuery = "   select top 1 a.Surname,a.Rank,a.Initials,c.pid FROM[MMS].[dbo].PersonalDetails as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.ServiceNo=c.ServiceNo where a.ServiceNo='" + svcn + "'  ";
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
                        sqlQuery = " select top 1 a.SpouseName as Surname,b.PID FROM [MMS].[dbo].SpouseDetails as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo where c.ServiceNo='" + svcn + "' and b.RelationshipType=2 ";
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
                            sqlQuery = "  select top 1 a.SpouseName as Surname FROM [MMS].[dbo].SpouseDetails as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo   where c.ServiceNo='" + svcn + "' ";
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
                                patient.Service_Type = 3;
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
                                sqlQuery = "  select top 1 a.SpouseName as Surname,b.PID FROM [MMS].[dbo].SpouseDetails as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo where c.ServiceNo='" + svcn + "' and b.RelationshipType=2 ";
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
                        sqlQuery = " select top 1 a.ParentName as Surname,b.PID FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo where c.ServiceNo='" + svcn + "' and b.RelationshipType=3 and a.Relationship='Father'";
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
                            sqlQuery = " select top 1 a.ParentName as Surname FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo   where c.ServiceNo='" + svcn + "'  and a.Relationship='Father'";
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
                                patient.Service_Type = 3;
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
                                sqlQuery = " select top 1 a.ParentName as Surname,b.PID FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo where c.ServiceNo='" + svcn + "' and b.RelationshipType=3 and a.Relationship='Father'";
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
                        sqlQuery = " select top 1 a.ParentName as Surname,b.PID FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo where c.ServiceNo='" + svcn + "' and b.RelationshipType=4 and a.Relationship='Mother'";
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
                            sqlQuery = " select top 1 a.ParentName as Surname FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  where c.ServiceNo='" + svcn + "' and  a.Relationship='Mother'";
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
                                patient.Service_Type = 3;
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
                                sqlQuery = " select top 1 a.ParentName as Surname,b.PID FROM [MMS].[dbo].parents as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo  left join[MMS].[dbo].Patient as b on b.ServiceNo=c.ServiceNo where c.ServiceNo='" + svcn + "' and b.RelationshipType=4 and a.Relationship='Mother'";
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
                        sqlQuery = "   select a.ChildName as Surname,a.DOB FROM [MMS].[dbo].Children as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.SNo=c.SNo   where c.ServiceNo='" + svcn + "' order by a.DOB asc ";
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
                                sqlQuery = "   select a.Surname as Surname,a.PID FROM [MMS].[dbo].Patient as a with(nolock)    where a.ServiceNo='" + svcn + "'  and  CONVERT(date, a.DateOfBirth)=CONVERT(varchar,'" + itemw.dob + "',111) and a.RelationshipType=5";
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
                                    sqlQuery = "select b.ChildName as Surname,b.DOB,a.PID FROM [MMS].[dbo].Patient as a with(nolock)   left join[MMS].[dbo].PersonalDetails as c on a.ServiceNo=c.ServiceNo  inner join [MMS].[dbo].Children as b on b.SNo=c.SNo  where a.ServiceNo='" + svcn + "' and CONVERT(date, a.DateOfBirth)=CONVERT(varchar,'" + itemw.dob + "',111) and CONVERT(date, b.DOB)=CONVERT(varchar,'" + itemw.dob + "',111) and a.RelationshipType=5 order by b.DOB asc ";
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
                                    patient.Service_Type = 3;
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
                            sqlQuery = "   select a.Surname as Surname,a.PID FROM [MMS].[dbo].Patient as a with(nolock)    where a.ServiceNo='" + svcn + "'  and   a.RelationshipType=5";
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
                    sqlQuery = "   SELECT  [VID], [VitalType],[PDID],a.[VTID],[VitalValues],[LocationID],[Reading_Time],[LocID] FROM [MMS].[dbo].[Ward_Vitals] " +
" as a inner join[MMS].[dbo].[Vital_Type] as b on a.vtid=b.vtid where a.PDID='" + wdid + "'  ";
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


                    DataTable oDataSetMgtPlan = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " SELECT WM.PlanId,WM.PDID,WM.Date,WM.Description,(Ur.FName+' '+UR.LName)AS Investigated,WM.Remarks FROM Ward_Mgt_Plan WM INNER JOIN Users UR ON WM.InvestigateBy = UR.UserID WHERE Wm.PDID='" + wdid+"'  ";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;
                    //   oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetMgtPlan);
                    // oSqlConnection.Close();
                    var mgtPlan = oDataSetMgtPlan.AsEnumerable()
            .Select(dataRow => new vitals
            {
                PlanId = (dataRow.Field<Int32>("PlanId")).ToString(),
                PDID = dataRow.Field<string>("PDID"),
                date = dataRow.Field<DateTime>("Date").ToString(),
                description = dataRow.Field<string>("Description"),
                investigated = dataRow.Field<string>("Investigated"),
                nurseRemarks = dataRow.Field<string>("Remarks")
            }).ToList();

                    DataTable oDataSetPatientCom = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " SELECT WM.WardId ,WM.Date,WM.Description,(Ur.FName+' '+UR.LName)AS Investigated,WM.Remarks FROM Ward_Patient_Complain WM INNER JOIN Users UR ON WM.InvestigateBy = UR.UserID WHERE Wm.WardId='" + wdid + "'  ";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;
                    //   oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetPatientCom);
                    // oSqlConnection.Close();
                    var PatientCom = oDataSetPatientCom.AsEnumerable()
            .Select(dataRow => new vitals
            {
                PDID = (dataRow.Field<Int32>("WardId")).ToString(),
                date = dataRow.Field<DateTime>("Date").ToString(),
                description = dataRow.Field<string>("Description"),
                investigated = dataRow.Field<string>("Investigated"),
                nurseRemarks = dataRow.Field<string>("Remarks")
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
                    string newWdid = "W" + wdid.ToString();
                    DataTable oDataSetv6 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "   SELECT e.TubeCategory,a.TestSID,e.CategoryID,a.pdid,a.testsid,e.CategoryName,max(a.Issued) Issued,CAST(a.[RequestedTime] as DATE) as RequestedTime  FROM [MMS].[dbo] " +
" .[Lab_Report] as a  inner join[MMS].[dbo].[Lab_SubCategory] as d on d.[LabTestID]=a.[LabTestID] inner join[MMS].[dbo]. " +
" [Lab_MainCategory] as e on e.CategoryID=d.CategoryID where  " +
"  a.RequestedLocID='" + locid + "' and a.PDID= '" + PDID + "'  group by e.CategoryName, a.TestSID, CAST(a.[RequestedTime] as DATE),e.CategoryID,e.TubeCategory " +
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
" ,a.Ps_Index,b.DrugMethodCount ,c.RouteDetail ,a.Duration,a.MethodType,a.Dose,a.pdid,a.ItemNo " +
"  FROM[MMS].[dbo].[Ward_Drug_Prescription] as a " +
" left join[MMS].[dbo].[DrugMethod] as b on a.Method=b.MethodID left join[MMS].[dbo].[DrugRoute] as c on a.route=c.routeid " +
" left join[MMS].[dbo].[DrugItems] as d on a.ItemNo=Convert(varchar, d.DrugID) " +
"    left join[MMS].[dbo].[EPASPharmacyItems] as e on a.ItemNo=Convert(varchar, e.[itemno]) where " +
" a.[PDID]='" + wdid + "' AND ISNULL(a.Status,1) = 1 " +
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
               date = dataRow.Field<DateTime?>("Date_Time").ToString(),
               itemdescription = dataRow.Field<string>("itmdes"),
               MethodDetail = dataRow.Field<string>("MethodDetail"),
               Ps_Index = dataRow.Field<string>("Ps_Index"),
               DrugMethodCount = dataRow.Field<decimal?>("DrugMethodCount"),
               RouteDetail = dataRow.Field<string>("RouteDetail"),
               Duration = dataRow.Field<string>("Duration"),
               MethodType = dataRow.Field<int?>("MethodType"),
               Dose = dataRow.Field<string>("Dose"),
               pdid = dataRow.Field<string>("pdid"),
               ItemNo = dataRow.Field<string>("ItemNo"),

           }).ToList();
                    }
                    catch (Exception ex)
                    {

                    }


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
                    result = new { s1 = siccat.ToList(), l1 = joined4.ToList(), d1 = joined3.ToList(), b1 = a1.ToList(), b2 = a2.ToList(), b4 = a1v.ToList(), b5 = a6.ToList(), vitval1 = vitval.ToList(), m1 = medbd1, err = iserror, u1 = df.ToList(), acrw = aircrewlist.ToList(),p1 = mgtPlan.ToList(),w1 = wdidlist.ToList(),pt = PatientCom.ToList() };
                    return Json(result, JsonRequestBehavior.AllowGet);
                    // exabd = exabd.ToList(), excdv = excdv.ToList(), exctn = exctn.ToList(), exgrl = exgrl.ToList(), exrpr = exrpr.ToList()
                }
                var result4 = new { s1 = "", l1 = "", d1 = "", b1 = "", b2 = "", b4 = "", b5 = "", vitval1 = "", err = "2", u1 = "" };
                return Json(result4, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var result3 = new { s1 = "", l1 = "", d1 = "", b1 = "", b2 = "", b4 = "", b5 = "", vitval1 = "", err = "2", u1 = "" };
                return Json(result3, JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult NurseView(int? page, string id, string currentFilter)
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
                if (!opdid.Trim().ToLower().StartsWith("opd"))
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



                   

                    locid = (String)Session["userloc"];
                    if (!String.IsNullOrEmpty(id))
                    {
                        id = id.Trim(MyChar);
                    }
                    if (!String.IsNullOrEmpty(id))
                    {
                        DateTime dt1 = DateTime.Now.Date;
                        
                        DateTime dd = DateTime.Now.Date;
                        DataTable oDataSet4 = new DataTable();
                        oSqlConnection = new SqlConnection(conStr);
                        oSqlCommand = new SqlCommand();

                        sqlQuery = "   SELECT max(cds.dgdetail)pcomoplian, COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1  " +
    "  and b.Surname != '0' " +
    " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),    " +
    "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
    "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
    "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname))  " +
    "	sname  ,max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
    "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
    "	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(WD.status)  pstatus,max(a.CreatedDate) crdate, max(h.Relationship) " +

    "     relasiondet,WT.ward_Type AS WardType,sc.CatId AS catStatus FROM[MMS].[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
    "  left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
    "  left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
    "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
    "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
    " LEFT JOIN Sick_Category sc ON a.pdid = sc.PDID  " +
    "left join CatDiagList cdl ON a.PDID = cdl.PDID " +
    "left join CatDaignosis cds ON cdl.dgid = cds.dgid " +
    "left join ward_details WD ON a.PDID = WD.PDID  " +
    "left join Ward_Types WT ON  WD.Ward_No=WT.Id " +
    " where   " +
  
    "  sc.CatId IN ('11','12') and" +

    " c.ServiceNo like  '%" + id + "%' and a.CreatedDate> DATEADD(DAY,-30,GETDATE())" +
    " and d.LocationID='" + locid + "' and (WD.status=15 or WD.status is null) group by a.PDID, a.CreatedDate,WT.ward_Type,sc.CatId  order by crdate ";
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

                            //var cdlists = from s in db.Patients.Where(p => p.ServiceNo == sno).Where(p => p.RelationshipType == 1).AsNoTracking()
                            //             orderby s.DateOfBirth
                            //             select new { s.DateOfBirth, s.PID, s.LocationID };

                            //foreach (var itemchw in cdlists)
                            //{
                            //    if (itemchw.PID == pid)
                            //    {
                            //        locid = itemchw.LocationID.ToString();
                            //    }
                            //}


                            //var patient_Detailn = from s in db.Patient_Detail.Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7 || p.Status == 5).Where(p => p.OPDID == opdid).AsNoTracking() orderby s.CreatedDate descending select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, opddiag = s.OPD_Diagnosis, relasiont = s.Patient.RelationshipType1.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = s.Patient.RelationshipType1.Relationship };

                            //DateTime dd = DateTime.Now.Date;
                            //var patient_Detail = from s in db.Patient_Detail.Where(p => p.PDID == "dfdffddf").AsNoTracking() orderby s.CreatedDate descending select new getdocdetail { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, pstatus = s.Status1.StatusDec, opddiag = s.OPD_Diagnosis, relasiont = s.Patient.RelationshipType1.RTypeID, crdate = s.CreatedDate, pidp = s.PID, relasiondet = s.Patient.RelationshipType1.Relationship };

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
                            //                 select new { s.DateOfBirth, s.PID,s.LocationID };

                            //    int hj = 1;
                            //    foreach (var itemchw in cdlist)
                            //    {
                            //        if (itemchw.PID == pid)
                            //        {
                            //            relasiondet = relasiondet + hj;
                            //            locid = itemchw.LocationID.ToString();
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
                            sqlQuery = "   SELECT max(cds.dgdetail)pcomoplian, COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1  " +
    "  and b.Surname != '0' " +
     " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),    " +
    "  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
    "   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
     "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname))  " +
    "	sname  ,max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
    "	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
    "	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(WD.status)  pstatus,max(a.CreatedDate) crdate, max(h.Relationship) " +

     "     relasiondet,WT.ward_Type AS WardType,sc.CatId AS catStatus FROM[MMS].[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
    "  left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
    "  left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
    "   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
     "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
     " LEFT JOIN Sick_Category sc ON a.pdid = sc.PDID  " +
      "left join CatDiagList cdl ON a.PDID = cdl.PDID " +
     "left join CatDaignosis cds ON cdl.dgid = cds.dgid " +
     "left join ward_details WD ON a.PDID = WD.PDID  " +
     "left join Ward_Types WT ON  WD.Ward_No=WT.Id " +
    " where    " +
    "  sc.CatId IN ('11','12') " +
    "and a.CreatedDate> DATEADD(DAY,-30,GETDATE())" +
    " and d.LocationID='" + locid + "' and (WD.status=15 or WD.status is null) group by a.PDID, a.CreatedDate,WT.ward_Type,sc.CatId  order by crdate ";
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
                        WardType = dataRow.Field<string>("WardType"),
                        catStatus = dataRow.Field<int>("catStatus"),
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
                else {
                    return RedirectToAction("Login", "Users");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
                //throw;
            }
        }

        public JsonResult SavepatientWardNurse(string bitems, string AbdOther, string reftohosp, string planofmgt, string drughyst, string hitems, string pastmedhys, string items, string sitems, string litems, string ditems, string Present_Complain, string History_PresentComplain, string Other_Complain, string History_OtherComplain, string PDID, string pntstatus, string GClinic, string dgn1, string genex, string cardex, string cenex, string resex, string othex, string abdex, string drugins, string remarks, string wardNo, string bedNo, string mgtPlan,string BHTNo,string psitemsWard)
        {
            int wdid = 0;
            int wdNo = 0;
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            String err = "";
            char[] MyChar = { '/', '"', ' ' };

            if (!String.IsNullOrEmpty(wardNo))
            {
                dynamic data = JObject.Parse(wardNo);
                wdNo = data.ID;
            }
            if (!String.IsNullOrEmpty(pastmedhys))
            {
                pastmedhys = pastmedhys.Trim(MyChar);
                pastmedhys = Regex.Replace(pastmedhys, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(items))
            {
                items = items.Trim(MyChar);
            }

            if (!String.IsNullOrEmpty(PDID))
            {
                PDID = PDID.Trim(MyChar);
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
            if (!String.IsNullOrEmpty(remarks))
            {
                remarks = remarks.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(remarks))
            {
                remarks = remarks.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(wardNo))
            {
                wardNo = wardNo.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(bedNo))
            {
                bedNo = bedNo.Trim(MyChar);
            }

            if (!String.IsNullOrEmpty(bedNo))
            {
                BHTNo = BHTNo.Trim(MyChar);
            }

            //if (!String.IsNullOrEmpty(nurseRemarks))
            //{
            //    nurseRemarks = BHTNo.Trim(MyChar);
            //}

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
            if (!String.IsNullOrEmpty(psitemsWard))
            {
                psitemsWard = psitemsWard.Trim(MyChar);
            }


            if (!String.IsNullOrEmpty(dgn1)) { AbdOther = AbdOther.Trim(MyChar); }
            if (!String.IsNullOrEmpty(genex))
            {
                genex = genex.Trim(MyChar);

                genex = Regex.Replace(genex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(cardex))
            {
                cardex = cardex.Trim(MyChar);
                cardex = Regex.Replace(cardex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(cenex))
            {
                cenex = cenex.Trim(MyChar);
                cenex = Regex.Replace(cenex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(resex))
            {
                resex = resex.Trim(MyChar);
                resex = Regex.Replace(resex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(othex))
            {
                othex = othex.Trim(MyChar);
                othex = Regex.Replace(othex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(abdex))
            {
                abdex = abdex.Trim(MyChar);
                abdex = Regex.Replace(abdex, @"\\t|\\n|\\r", "");
            }
            if (!String.IsNullOrEmpty(drugins))
            {
                drugins = drugins.Trim(MyChar);
                drugins = Regex.Replace(drugins, @"\\t|\\n|\\r", "");
            }

            string remIndex = "";
            string RemDesc = "";
            string pid = "";
            string opdid = "";
            string locid = "";
            string modus = "";
            int? pcat = 0;
            int? subcat = 0;
            DateTime crdt = DateTime.Now;
            int userid = Convert.ToInt32(Session["UserID"]);
            var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            foreach (var item in ser)
            {
            }
            var pidvar = from s in db.Patient_Detail.Where(p => p.PDID == PDID) select new { s.PID, s.CreatedDate, s.PatientCatID, s.SubjectID, s.OPDID, s.ModifiedBy };

            foreach (var item in pidvar)
            {

                pid = item.PID;
                crdt = item.CreatedDate;
                pcat = item.PatientCatID;
                subcat = item.SubjectID;
                opdid = item.OPDID;
                modus = item.ModifiedBy;
            }

            var objsRemarks = JsonConvert.DeserializeObject<List<RemarksReader>>(psitemsWard);
            foreach (RemarksReader l in objsRemarks)
            {
                remIndex = l.psindex1Ward;
                RemDesc = l.issudqnty1Ward;
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "UPDATE Ward_Mgt_Plan SET Remarks='"+ RemDesc + "',RemarksBy='"+ userid + "' WHERE PlanId='"+ remIndex + "' ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
            }
                // var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
                //opdid = (String)Session["userlocid1"];
                //  foreach (var item in opd)
                // {

                //  opdid = item.LOCID;
                //}
                var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            foreach (var item in serW)
            {

                locid = item.LocationID;
            }

            //var warId = from v in db.Clinic_Master where (v.LocationID == locid) where (v.ClinicTypeID == 20) select new { v.Clinic_ID };

            //foreach (var itemsList in warId)
            //{

            //  //  opdid = itemsList.Clinic_ID;
            //}
            opdid = (String)Session["userlocid1"];
            if (!opdid.Trim().ToLower().StartsWith("opd")) { 
            IndexGeneration indi = new IndexGeneration();
            if (!String.IsNullOrEmpty(modus))
            {
                if (!modus.Trim().Equals(userid.ToString()))
                {
                    //PDID = indi.CreatePDID(pid);
                }
            }
            Ward_Details oWard_Details = new Ward_Details();

            oWard_Details.PDID = PDID;
            oWard_Details.Ward_No = wdNo.ToString();
            oWard_Details.Bed_No = bedNo;
            oWard_Details.Remarks = remarks;
            oWard_Details.BHTNo = BHTNo;
            oWard_Details.Status = 15;
            oWard_Details.Created_By = userid.ToString();
            oWard_Details.Created_Date = DateTime.Now;
            oWard_Details.OPDID = opdid;
            //oWard_Details.nurseRemarks = nurseRemarks;

            int cltyp = 0;
            if (!String.IsNullOrEmpty(GClinic))
            {
                dynamic data = JObject.Parse(GClinic);
                cltyp = data.ClinicTypeID;
            }
            var PSIndex = db.Ward_Details.Where(p => p.PDID == PDID).ToList().Count;
            var wardId = from s in db.Ward_Details.Where(p => p.PDID == PDID) select new { s.WDID };
            if (Convert.ToInt16(PSIndex.ToString()) > 0)
            {
                foreach (var item in wardId)
                {

                    wdid = item.WDID;
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " UPDATE ward_Details SET Bed_No='"+bedNo+"',Ward_No='"+ wdNo + "',Remarks='"+remarks+"',BHTNo='"+BHTNo+"' WHERE WDID ='"+wdid+"' ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlCommand.ExecuteNonQuery();
                    oSqlConnection.Close();
                }
            }
            else
            {
                db.Ward_Details.Add(oWard_Details);
                db.SaveChanges();
                wdid = oWard_Details.WDID;
            }
            Ward_Mgt_Plan wardPlan = new Ward_Mgt_Plan();
            wardPlan.PDID = wdid.ToString();
            wardPlan.Description = planofmgt;
            wardPlan.Date = DateTime.Now;
            wardPlan.InvestigateBy = userid.ToString();

            string tempWdid = "";
            var temppdid = from s in db.Ward_Mgt_Plan.Where(p => p.PDID == wardId.ToString()) select new { s.PDID };
            //foreach (var iy in temppdid)
            //{

            //    tempWdid = iy.PDID;
            //}
            if (!String.IsNullOrEmpty(modus))
            {
                //db.Ward_Mgt_Plan.Add(wardPlan);
                //db.SaveChanges();
            }
            else
            {
                //need to disscuss
            }


            //-------PASTMED/
            long pastmed1 = 0;

            long cathy1 = 0;
            CatReferal oCatReferal = new CatReferal();

            oCatReferal.PDID = wdid.ToString();
            oCatReferal.PlanofMgt = planofmgt;
            oCatReferal.ReffNote = reftohosp;
            ///////////
            var objb = JsonConvert.DeserializeObject<List<breader>>(bitems);
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
                oTranferDetails.PDID = wdid.ToString();
                oTranferDetails.ToLoc = clinkid;
                oTranferDetails.FromLoc = opdid;
                oTranferDetails.TransferDate = DateTime.Now;
                oTranferDetails.TransID = indi.CreateTransID(PDID);
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
                oTranferDetails.PDID = wdid.ToString();
                oTranferDetails.ToLoc = clinkid1;
                oTranferDetails.FromLoc = opdid;
                oTranferDetails.TransferDate = DateTime.Now;
                oTranferDetails.TransID = indi.CreateTransID(PDID);
                oTranferDetails.TransStatus = 1;
            }
            var objsv = JsonConvert.DeserializeObject<List<Vitalreader>>(items);
            int objcountv = objsv.Count;
            Ward_Vitals[] objVital = new Ward_Vitals[objcountv];
            int i1 = 0;

            foreach (Vitalreader p in objsv)
            {

                Ward_Vitals oVital = new Ward_Vitals();
                oVital.PDID = wdid.ToString();
                oVital.VID = indi.CreateVIDWard(i1, wdid.ToString());
                oVital.CreatedBy = userid.ToString();
                oVital.CreatedDate = DateTime.Now;
                oVital.LocationID = locid;
                oVital.LocID = opdid;

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
                var oldtestcnt = db.Hypersensivities.Where(d => d.HyperTypeSubID == p.hsubtype1).Where(d => d.PID == pid).ToList().Count;
                if (oldtestcnt < 1)
                {
                    Hypersensivity oHypersensivity = new Hypersensivity();
                    oHypersensivity.PID = pid;
                    oHypersensivity.HypersenseID = indi.CreateHID(j, pid);
                    oHypersensivity.HyperTypeSubID = p.htype1;
                    //oHypersensivity.RSubID = p.hrsubtype1;
                    //oHypersensivity.SeverityID = Convert.ToInt32(p.hstype1);
                    oHypersensivity.ModifiedDate = DateTime.Now;
                    oHypersensivity.HypersenseDetail = p.hstype1;


                    objHyp[j] = oHypersensivity;
                    j++;
                }
            }
      
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

                foreach (Labreader lp in objsl)
                {

                    Lab_Report oLab_Report = new Lab_Report();
                    var objl = from s in db.Lab_SubCategory.Where(f => f.CategoryID == lp.labid) select new { s.LabTestID };
                    int objcountl1 = objl.Count();
                    foreach (var q in objl)
                    {
                        var lablist = from t in db.Lab_Report.Where(pf => pf.PDID == PDID.ToString())
                                      join x in db.Lab_SubCategory on t.LabTestID equals x.LabTestID
                                      join y in db.Lab_MainCategory on x.CategoryID equals y.CategoryID

                                      select new
                                      { y.CategoryName, y.CategoryID, t.PDID, t.TestSID }; ;
                        var labl = lablist.GroupBy(c => new { c.CategoryName, c.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).ToList();

                        var oldtestcntl = labl.ToList().Count;

                        if (oldtestcntl < 1)
                        {
                            Lab_Report oLab_Reports = new Lab_Report();
                            oLab_Reports.LabTestID = q.LabTestID;
                            oLab_Reports.RequestedLocID = locid;
                            oLab_Reports.RequestedTime = DateTime.Now;
                            oLab_Reports.PDID = PDID.ToString();
                            oLab_Reports.TestSID = PDID.ToString() + "x0";
                            oLab_Reports.Issued = "0";
                            oLab_Reports.Isemail = 0;
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
                            oLab_Reports.PDID = PDID.ToString();
                            oLab_Reports.Issued = "0";
                            oLab_Reports.IsPrint = "0";
                            oLab_Reports.Isemail = 0;
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
            Ward_Drug_Prescription[] objDrug = new Ward_Drug_Prescription[1000];
            if (!String.IsNullOrEmpty(ditems))
            {


                objs = JsonConvert.DeserializeObject<List<Drugreader>>(ditems);

                objcount = objs.Count;
                int i = 0;

                foreach (Drugreader vp in objs)
                {

                    var oldtestcntdrug = db.Ward_Drug_Prescription.Where(d => d.ItemNo == vp.dItemno).Where(d => d.PDID == wdid.ToString()).Where(d => d.Status == 1).ToList().Count;
                    if (oldtestcntdrug < 1)
                    {
                        Ward_Drug_Prescription oDrug_Prescription = new Ward_Drug_Prescription();
                        oDrug_Prescription.PDID = wdid.ToString();
                        oDrug_Prescription.Ps_Index = Guid.NewGuid().ToString();
                        oDrug_Prescription.Dose = vp.dDose;
                        oDrug_Prescription.Method = vp.dMethod;
                        oDrug_Prescription.Route = vp.dRoute;
                        oDrug_Prescription.ItemNo = vp.dItemno;
                        //oDrug_Prescription.MethodType = Convert.ToInt32(p.dStockTypeID);
                        oDrug_Prescription.Duration = vp.dDuration;
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
            Ward_Sick_Category[] objSick = new Ward_Sick_Category[objcountsic];
            int h = 0;

            foreach (Sickreader tp in objsic)
            {
                DateTime dt1 = DateTime.Now.Date;
                var oldtestcntSick = db.Ward_Sick_Category.Where(d => d.CatID == tp.scatid).Where(d => d.PDID == wdid.ToString()).Where(d => d.Date == dt1).ToList().Count;
                if (oldtestcntSick < 1)
                {
                    Ward_Sick_Category oSick_Category = new Ward_Sick_Category();
                    oSick_Category.PDID = wdid.ToString();
                    oSick_Category.CatIndex = indi.CreateSCID(h, wdid.ToString());
                    oSick_Category.CatPeriod = tp.sdays;
                    //oSick_Category.Date = Convert.ToDateTime(p.seff);
                    oSick_Category.LocID = locid;
                    oSick_Category.CatID = tp.scatid;

                    objSick[h] = oSick_Category;
                    h++;
                }
            }

                //need uncomment
                if (ModelState.IsValid)
                {
                    try
                    {


                        string catref = "";
                        var catrefvar = from s in db.CatReferals.Where(cp => cp.PDID == wdid.ToString()) select new { s.PDID };

                        foreach (var item in catrefvar)
                        {

                            catref = item.PDID;
                        }
                        if (!String.IsNullOrEmpty(catref))
                        {
                            //db.Entry(oCatReferal).State = EntityState.Modified;
                        }
                        else
                        {
                            db.CatReferals.Add(oCatReferal);
                        }


                        objVital = objVital.Where(x => x != null).ToArray();
                        db.Ward_Vitals.AddRange(objVital);
                        objHyp = objHyp.Where(x => x != null).ToArray();
                        db.Hypersensivities.AddRange(objHyp);

                        objSick = objSick.Where(x => x != null).ToArray();
                        db.Ward_Sick_Category.AddRange(objSick);
                        if (pntstatus == "7")
                        {
                            db.TranferDetails.Add(oTranferDetails);
                        }
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
                    return Json("2", JsonRequestBehavior.AllowGet);
                }
            }
            return Json(err, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IssuDrug(string id,string Duration,string pdid)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string err = "";
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);
            string pdidNew = pdid.Trim(MyChar);
            int userid = Convert.ToInt32(Session["UserID"]);
            try
            {
                string opdid = "";
                string locid = "";
                string clinicId = "";
                decimal qty = 0;
                decimal ablStock = 0;
                opdid = (String)Session["userlocid1"];
                if (opdid != null)
                {
                    var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
                    foreach (var item in clincd)
                    {

                        locid = item.LocationID;
                    }

                    var sid = from v in db.Clinic_Master where (v.SID == opdid) where (v.ClinicTypeID == 22) select new { v.Clinic_ID };
                    foreach (var item in sid)
                    {

                        clinicId = item.Clinic_ID;
                    }
                }
                var drugStock = from v in db.DrugStockMasters where (v.StoreID == clinicId) where (v.LOC == locid) where (v.ItemID == NewString) select new { v.DrugQuantity };
                foreach (var stock in drugStock)
                {

                    qty = Convert.ToDecimal(stock.DrugQuantity);
                }

                ablStock = qty - Convert.ToDecimal(Duration.ToString());
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;

                oSqlConnection = new SqlConnection(conStr);
                SqlCommand cmdCount = new SqlCommand("SELECT count(*) from DrugStockMaster WHERE ItemID = @PDID AND StoreID =@st", oSqlConnection);
                cmdCount.Parameters.AddWithValue("@PDID", NewString);
                cmdCount.Parameters.AddWithValue("@st", clinicId);
               

                    if (oSqlConnection.State == ConnectionState.Closed)
                    {
                        oSqlConnection.Open();
                    }
                    int count = (int)cmdCount.ExecuteScalar();

                oSqlConnection.Close();

                if (count > 0)
                    {
                        string sqlQuery;
                        DataTable oDataSet = new DataTable();

                        oSqlCommand = new SqlCommand();
                        sqlQuery = " UPDATE DrugStockMaster SET DrugQuantity='" + ablStock + "' WHERE ItemID='" + NewString + "' AND LOC= '" + locid + "' AND StoreID='" + clinicId + "' ";
                        oSqlCommand.Connection = oSqlConnection;
                        oSqlCommand.CommandText = sqlQuery;
                        oSqlConnection.Open();
                        oSqlCommand.ExecuteNonQuery();
                        oSqlConnection.Close();

                    string sqlQueryupdate;

                    oSqlCommand = new SqlCommand();
                    sqlQueryupdate = "   update Ward_Drug_Prescription set Issued=1 where PDID = '"+ pdidNew + "' and ItemNo='" + NewString + "' ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQueryupdate;
                    oSqlConnection.Open();
                    oSqlCommand.ExecuteNonQuery();
                    oSqlConnection.Close();
                    //  err = "Drugs Issued";
                }
                else
                {
                    string sqlQuery;
                    DataTable oDataSet = new DataTable();

                    oSqlCommand = new SqlCommand();
                    sqlQuery = " insert into DrugStockMaster ([ItemIndex],[ItemID],[MFD],[LOC],[StoreID],[DrugQuantity]) values ('"+ Guid.NewGuid().ToString() + "','" + NewString + "','"+DateTime.Now+ "','" + locid + "','" + clinicId + "','" + ablStock + "')";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlCommand.ExecuteNonQuery();
                    oSqlConnection.Close();
                  //  err = "Drugs Issued";
                }
               

                oSqlCommand = new SqlCommand();
                sqlQuery = " INSERT INTO [dbo].[DrugStockTransection]([ItemID],[IssuedTo],[BatchID],[IssuedDate],[TransectionQty],[IssuedUser],[InLoc],[OutLoc]) values ('" + NewString + "','" + locid + "','0','" + DateTime.Now + "','" + Duration + "','" + userid + "','" + opdid + "','" + clinicId + "')";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                err = "Drugs Issued";


            }
            catch (Exception ex)
                {
                    err = "Cannot Issued Drug ";
                }
            
            return Json(err, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetDrugWard()
        {
            //    try
            //    {
            //        string opdid = "";
            //        string locid = "";
            //        string clinicId = "";
            //        opdid = (String)Session["userlocid1"];
            //        if (opdid != null)
            //        {
            //            var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
            //            foreach (var item in clincd)
            //            {

            //                locid = item.LocationID;
            //            }

            //            var sid = from v in db.Clinic_Master where (v.SID == opdid) where (v.ClinicTypeID==22) select new { v.Clinic_ID };
            //            foreach (var item in sid)
            //            {

            //                clinicId = item.Clinic_ID;
            //            }
            //        }
            //        DataTable oDataSet1 = new DataTable();
            //        oSqlConnection = new SqlConnection(conStr);
            //        oSqlCommand = new SqlCommand();
            //        sqlQuery = "    SELECT a.ItemID,a.ItemIndex,COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'') " +
            //                   "   as itemdescription,a.LOC  ,a.MFD,a.StoreID ,a.DrugQuantity ,a.ExpireDate,a.ItemIndex " +
            //                   "  FROM[MMS].[dbo].[DrugStockMaster] as a left join[MMS].[dbo].[DrugItems] as d on " +
            //                   "   a.ItemID=Convert(varchar, d.DrugID)     left join[MMS].[dbo].[EPASPharmacyItems] as e on a.ItemID=Convert(varchar, " +
            //                   " e.[itemno]) where a.StoreID='" + clinicId + "' and a.LOC='" + locid + "'    order by a.ItemID";
            //        oSqlCommand.Connection = oSqlConnection;
            //        oSqlCommand.CommandText = sqlQuery;
            //        oSqlConnection.Open();
            //        oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            //        oSqlDataAdapter.Fill(oDataSet1);
            //        oSqlConnection.Close();
            //        var q2 = oDataSet1.AsEnumerable()
            //.Select(dataRow => new getdrugl
            //{
            //    itemdescription = dataRow.Field<string>("ItemDescription"),
            //    itemno = dataRow.Field<string>("ItemID").ToString()

            //}).ToList();
            //        var p2 = q2.ToList();
            //        return Json(p2, JsonRequestBehavior.AllowGet);
            //    }
            //    catch (Exception ex)
            //    {
            //        return Json("", JsonRequestBehavior.AllowGet);
            //    }
            try
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
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }




        }

        public JsonResult GetdrugalldepWard(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            int di = 0;
            try
            {
                di = Convert.ToInt32(id);
            }
            catch (Exception ex)
            {

            }
            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }

            opdid = (String)Session["userlocid1"];
            if (opdid != null)
            {
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid)  select new { v.LocationID };
                foreach (var item in clincd)
                {

                    locid = item.LocationID;
                }
            }
            //var GetDrug = db.DrugItems.Where(x=>x.DrugID== di) .Select(x => new { itemno= x.DrugID.ToString(), itemdescription= x.ItemDescription }).ToList();
            //if (GetDrug.Count() > 0)
            //{
            //    GetDrug= db.EPASPharmacyItems.Where(x => x.itemno == id).Select(x => new { itemno= x.itemno, itemdescription= x.itemdescription }).ToList();
            //}
            var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
            var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
            var serc = from s in db.DrugStockMasters.Where(p => p.ItemID == id).Where(p => p.LOC == locid) join b in db.Clinic_Master on s.StoreID equals b.Clinic_ID orderby s.ItemID select new { s.ItemID, s.ItemIndex, b.Clinic_Detail, s.MFD, s.StoreID, s.DrugQuantity, s.ExpireDate };
            int ij = 0;

            foreach (var itm in serc)
            {


                var items2 = from d in db.EPASPharmacyItems.Where(p => p.itemno == id) select new { d.itemdescription, d.itemno };
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
                }
                else
                {
                    var items3 = from d in db.DrugItems.Where(p => p.DrugID.ToString() == id) select new { d.ItemDescription, d.DrugID };
                    if (ij != 0)
                    {
                        items5 = items3.Concat(items5);

                    }
                    else
                    {

                        items5 = items3;
                    }

                }
                break;

            }
            var t1 = serc.ToList();
            var t2 = items.ToList();
            var t3 = items5.ToList();
            var joined = from it1 in t1 join it2 in t2 on it1.ItemID equals it2.itemno select new getdrugdata { ItemID = it1.ItemID, LOC = it1.Clinic_Detail, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.itemdescription };

            var joined1 = from it1 in t1 join it2 in t3 on it1.ItemID equals it2.DrugID.ToString() select new getdrugdata { ItemID = it1.ItemID, LOC = it1.Clinic_Detail, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.ItemDescription };
            var u1 = joined.ToList();
            var u2 = joined1.ToList();
            var joined3 = u1.Concat(u2);
            return Json(joined3, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetStatusWard()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select Status,StatusDec from [dbo].[Status]  ";
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
            return Json(empList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult deldrugWard(string id)
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
                sqlQuery = " UPDATE Ward_Drug_Prescription SET Status = 0 WHERE Ps_Index = '"+ NewString + "' ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                err = "Removed";
            }
            catch (Exception ex)
            {
            }
            return Json(err, JsonRequestBehavior.AllowGet);
        }
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
    public class RemarksReader
    {
        public string psindex1Ward { get; set; }
        public string issudqnty1Ward { get; set; }
    }
}