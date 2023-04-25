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
using PagedList;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace MMS.Controllers
{
    public class claim_detailController : Controller
    {
        private MMSEntities db = new MMSEntities();
        private string err;
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        private string sqlQuery;

        // GET: claim_detail
        public ActionResult Index()
        {
            return View(db.claim_detail.ToList());
        }

        // GET: claim_detail/Details/5
        public ActionResult Details(int? page, int? page1, string id, string id1)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            var onePageOfProducts = (dynamic)null;
            var sickcate = (dynamic)null;
            char[] MyChar = { '/', '"', ' ' };
            string opdid = "";
            string locid = "";
            var title = new String[100];
            int specid = 0;
            string id2 = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            opdid = (String)Session["userlocid1"];
            var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            foreach (var item in serW)
            {

                locid = item.LocationID;
            }
            locid=(String)Session["userloc"];
            var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };

            foreach (var item in opd)
            {

                // opdid = item.LOCID;
            }

            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id1))
            {
                id1 = id1.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id1))
            {
                Session["dtvalu"] = id1;
            }
            if (Session["dtvalu"] != null)
            {
                if (String.IsNullOrEmpty(id))
                {
                    id2 = Session["dtvalu"].ToString();
                }
            }

            if (!String.IsNullOrEmpty(id2) && !String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Parse(id2);
                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and batchid like'%tsi%'  group by batchid order by batchid desc";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {


         svcid = dataRow.Field<string>("batchid"),




     }).ToList();

                ViewBag.gtclbatch = joined3;
                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (b.ServiceNo.Contains(id) && s.created_date.Value.Day == dt1.Day && s.created_date.Value.Month == dt1.Month && s.created_date.Value.Year == dt1.Year) orderby (s.created_date) descending select new getclaimdata { regno = s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.Doc_Amount.ToString(), status = s.Status.ToString() };
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
            }
            else if (!String.IsNullOrEmpty(id2))
            {
                DateTime dt1 = DateTime.Parse(id2);
                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and batchid like'%tsi%'  group by batchid order by batchid desc";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {


         svcid = dataRow.Field<string>("batchid"),




     }).ToList();

                ViewBag.gtclbatch = joined3;
                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (s.loc == locid && s.created_date.Value.Day == dt1.Day && s.created_date.Value.Month == dt1.Month && s.created_date.Value.Year == dt1.Year) orderby (s.created_date) descending select new getclaimdata { regno = s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.Doc_Amount .ToString(), status = s.Status.ToString() };
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
            }
            else if (!String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Now.Date;
                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and batchid like'%tsi%'  group by batchid order by batchid desc";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {


         svcid = dataRow.Field<string>("batchid"),




     }).ToList();

                ViewBag.gtclbatch = joined3;
                if (opdid.Contains("cl"))
                {
                    var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id == Convert.ToInt32(specid)).Where(p => p.event_start == dt1) select new { s.title };
                }
                DateTime dd = DateTime.Now.Date;
                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (b.ServiceNo.Contains(id)&&s.loc==locid ) orderby (s.created_date) descending select new getclaimdata { regno = s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
                //db.Patient_Detail.Include(p => p.Patient).Include(p => p.Status1).Where(p => p.Patient.ServiceNo.Contains(id)).OrderByDescending(p => p.CreatedDate);
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);
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
                   // var patient_Detail = from s in db.Patient_Detail where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).OrderByDescending(p => p.CreatedDate) select new getpatietdata { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, hyscompllain = s.History_OtherComplain, opddiag = s.OPD_Diagnosis, relasiont = s.Patient.RelationshipType1.Relationship };


                    // db.Patient_Detail.Include(p => p.Patient).OrderByDescending(p => p.CreatedDate);
                    var pageNumber = page ?? 1;
                    onePageOfProducts = db.Patient_Detail.ToArray().ToPagedList(pageNumber, 10);

                }
                else
                {
                    DateTime dd = DateTime.Now.Date;
                    DataTable oDataSetv7 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and batchid like'%tsi%'  group by batchid order by batchid desc";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;
                    //   oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetv7);
                    // oSqlConnection.Close();

                    var joined3 = oDataSetv7.AsEnumerable()
         .Select(dataRow => new getsickdata2
         {


             svcid = dataRow.Field<string>("batchid"),




         }).ToList();

                    ViewBag.gtclbatch = joined3;
                    //db.Patient_Detail.Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                    var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (s.loc == locid && s.Status == 2) orderby (s.created_date) descending select new getclaimdata { regno=s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.Doc_Amount.ToString(), status = s.Status.ToString() };

                    var pageNumber = page ?? 1;
                    onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);
                }
            }

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }

        public ActionResult getclbatch(string id)
        {
            Session["bid"] = id;
            //           string opdid = (String)Session["userlocid1"];
            //           DataTable oDataSetv7 = new DataTable();
            //           oSqlConnection = new SqlConnection(conStr);
            //           oSqlCommand = new SqlCommand();
            //           sqlQuery = "   SELECT max(a.[stcheckdate])stcheckdate,max(b.Clinic_Detail)Clinic_Detail,MAX(a.batchid)batchid " +
            //" FROM[MMS].[dbo].[DrugStockCheck] as a inner join[dbo].[Clinic_Master] as b on a.storeid=b.Clinic_ID WHERE " +
            //" B.LocationID=(SELECT LocationID FROM[dbo].[Clinic_Master] " +
            //"       WHERE Clinic_ID = '" + opdid + "') group by batchid order by stcheckdate desc";
            //           // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            //           oSqlCommand.Connection = oSqlConnection;
            //           oSqlCommand.CommandText = sqlQuery;
            //           oSqlCommand.CommandTimeout = 120;
            //           //   oSqlConnection.Open();
            //           oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            //           oSqlDataAdapter.Fill(oDataSetv7);
            //           // oSqlConnection.Close();

            //           var joined3 = oDataSetv7.AsEnumerable()
            //.Select(dataRow => new getsickdata2
            //{

            //    modifieddate = dataRow.Field<DateTime>("stcheckdate"),
            //    PDID = dataRow.Field<string>("Clinic_Detail"),
            //    svcid = dataRow.Field<string>("batchid"),




            //}).ToList();

            //           ViewBag.gtck = joined3;
                       return View();
        }


        // GET: claim_detail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: claim_detail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "claim_id,pid,claim_date,mcat_id,dgid,created_date,loc")] claim_detail claim_detail)
        {
            if (ModelState.IsValid)
            {
                db.claim_detail.Add(claim_detail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(claim_detail);
        }

        [HttpPost]
       
        public ActionResult view1()
        {
            

            return View();
        }
        public JsonResult getbalance(string catid, string svc)
        {try
            {
                char[] MyChar = { '/', '"', ' ' };

                string catid1 = catid.Trim(MyChar);
                string svc1 = svc.Trim(MyChar);

                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT a.Balance,b.mc_catdetail FROM [MMS].[dbo].[View_PersonalLimits] as a left join [dbo].[claim_catagory] as b on a.claCatCode=b.mc_catid where a.claCatCode='" + catid1 + "' and a.claSno='" + svc1 + "' and a.claClaimYear='"+DateTime.Now.Year+"'";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {


         svcid = dataRow.Field<decimal>("Balance").ToString(),
         cat = dataRow.Field<string>("mc_catdetail"),



     }).ToList();
                return Json(joined3, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
          

        }

        public JsonResult getbalancetsi(string svc)
        {
            try
            {
                char[] MyChar = { '/', '"', ' ' };

               // string catid1 = catid.Trim(MyChar);
                string svc1 = svc.Trim(MyChar);

                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT d.Balance,b.mc_catdetail FROM  [dbo].[claim_detail]  as a left join [dbo].[Patient] as c on c.pid=a.pid left join [dbo].[View_PersonalLimits]  as d on d.claSno=c.ServiceNo left join [dbo].[claim_catagory] as b on "+
 " b.mc_catid = d.claCatCode   where "+
  " a.RegisterNo = '"+ svc1 + "' and d.claClaimYear = '"+DateTime.Now.Year+"'";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {


         svcid = dataRow.Field<decimal>("Balance").ToString(),
         cat = dataRow.Field<string>("mc_catdetail"),



     }).ToList();
                return Json(joined3, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult Getclmcat()
        {
            var status = db.claim_catagory.Select(x => new { x.mc_catid, x.mc_catdetail }).ToList();
            return Json(status, JsonRequestBehavior.AllowGet);

        }
        // GET: claim_detail/Edit/5
        public ActionResult Edit(int? page, int? page1, string id, string id1)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            var onePageOfProducts = (dynamic)null;
            var sickcate = (dynamic)null;
            char[] MyChar = { '/', '"', ' ' };
            string opdid = "";
            string locid = "";
            var title = new String[100];
            int specid = 0;
            string id2 = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            opdid = (String)Session["userlocid1"];
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}
            locid=(String) Session["userloc"];
            //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
           
            //foreach (var item in opd)
            //{

            //    // opdid = item.LOCID;
            //}

            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id1))
            {
                id1 = id1.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id1))
            {
                Session["dtvalu"] = id1;
            }
            if (Session["dtvalu"] != null)
            {
                if (String.IsNullOrEmpty(id))
                {
                    id2 = Session["dtvalu"].ToString();
                }
            }

            if (!String.IsNullOrEmpty(id2) && !String.IsNullOrEmpty(id))
            {
                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and (batchid like'%mo' or batchid like'%mogvs')  group by batchid order by batchid desc";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {


         svcid = dataRow.Field<string>("batchid"),




     }).ToList();

                ViewBag.gtclbatch = joined3;
                DateTime dt1 = DateTime.Parse(id2);
              
                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where(b.ServiceNo.Contains(id) && s.created_date.Value.Day == dt1.Day && s.created_date.Value.Month == dt1.Month && s.created_date.Value.Year == dt1.Year) orderby (s.created_date) descending select new getclaimdata { IsDHSConfirmed = s.IsDHSConfirmed, regno =s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.Doc_Amount.ToString(), status = s.Status.ToString() };
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
            }
            else if (!String.IsNullOrEmpty(id2))
            {
                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and (batchid like'%mo' or batchid like'%mogvs')  group by batchid order by batchid desc";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {


         svcid = dataRow.Field<string>("batchid"),




     }).ToList();

                ViewBag.gtclbatch = joined3;
                DateTime dt1 = DateTime.Parse(id2);
                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (s.loc == locid && s.created_date.Value.Day == dt1.Day && s.created_date.Value.Month == dt1.Month && s.created_date.Value.Year == dt1.Year) orderby (s.created_date) descending select new getclaimdata { IsDHSConfirmed = s.IsDHSConfirmed, regno = s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.Doc_Amount.ToString(), status = s.Status.ToString() };
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
            }
           else if (!String.IsNullOrEmpty(id))
            {
                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and (batchid like'%mo' or batchid like'%mogvs')  group by batchid order by batchid desc";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {


         svcid = dataRow.Field<string>("batchid"),




     }).ToList();

                ViewBag.gtclbatch = joined3;
                DateTime dt1 = DateTime.Now.Date;
                if (opdid.Contains("cl"))
                {
                    var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id == Convert.ToInt32(specid)).Where(p => p.event_start == dt1) select new { s.title };
                }
                DateTime dd = DateTime.Now.Date;
                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (b.ServiceNo.Contains(id)) orderby (s.created_date) descending select new getclaimdata { IsDHSConfirmed = s.IsDHSConfirmed, regno = s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = s.dgid, relasiont = f.Relationship, loc = s.loc, claimamnt = s.Doc_Amount.ToString(), status = s.Status.ToString() };
                //db.Patient_Detail.Include(p => p.Patient).Include(p => p.Status1).Where(p => p.Patient.ServiceNo.Contains(id)).OrderByDescending(p => p.CreatedDate);
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);
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
                 //   var patient_Detail = from s in db.Patient_Detail.Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).OrderByDescending(p => p.CreatedDate) select new getpatietdata { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, hyscompllain = s.History_OtherComplain, opddiag = s.OPD_Diagnosis, relasiont = f.Relationship };


                    // db.Patient_Detail.Include(p => p.Patient).OrderByDescending(p => p.CreatedDate);
                    var pageNumber = page ?? 1;
                   // onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);

                }
                else
                {

                    //string opdid = (String)Session["userlocid1"];
                    DataTable oDataSetv7 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'"+locid+ "%' and (batchid like'%mo' or batchid like'%mogvs')  group by batchid order by batchid desc";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;
                    //   oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetv7);
                    // oSqlConnection.Close();

                    var joined3 = oDataSetv7.AsEnumerable()
         .Select(dataRow => new getsickdata2
         {

            
             svcid = dataRow.Field<string>("batchid"),




         }).ToList();

                    ViewBag.gtclbatch = joined3;



                    DateTime dd = DateTime.Now.Date;

                    //db.Patient_Detail.Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                    var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where(s.loc == locid&&s.Status==1) orderby (s.created_date) descending select new getclaimdata { IsDHSConfirmed = s.IsDHSConfirmed, regno = s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date,  catagory = c.mc_catdetail,diagnosis=d.dgdetail, relasiont = f.Relationship,loc=s.loc,claimamnt = s.Doc_Amount.ToString(),status=s.Status.ToString() };

                    var pageNumber = page ?? 1;
                    onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);
                }
            }

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }

        // POST: claim_detail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "claim_id,pid,claim_date,mcat_id,dgid,created_date,loc")] claim_detail claim_detail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(claim_detail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(claim_detail);
        }

        // GET: claim_detail/Delete/5
        public ActionResult genminmo()
        {
            int i = 0;
            string  locid = (String)Session["userloc"];
            if (!String.IsNullOrEmpty(locid)) {
            int userid = Convert.ToInt32(Session["UserID"]);
            string opdid = (String)Session["userlocid1"];
            string bid = "";
            claim_batch[] objDrug = new claim_batch[10000];
            var a1 = from s in db.claim_detail  where (s.loc == locid&&s.Status==1&&s.mcat_id!= "CAT011") orderby (s.created_date) descending select new getclaimdata { PID = s.claim_id, claimname = s.ClaimName, crdate = s.created_date, evntdate = s.claim_date,   loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
            foreach (var p in a1)
            {

                //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                //if (oldtestcnt < 1)
                //{
                claim_batch oDrug_Prescription = new claim_batch();
                oDrug_Prescription.claim_id = p.PID;
                oDrug_Prescription.batchid = locid+DateTime.Now.Date.ToString("yyyy/MM/dd")+"/MO";
                oDrug_Prescription.userid = userid;
                oDrug_Prescription.BatchDate = DateTime.Now;
                oDrug_Prescription.UserCaT = 1;
               
                objDrug[i] = oDrug_Prescription;

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
            string    sqlQuery = " update  [dbo].[claim_detail] set Status=2 where claim_id='" + p.PID + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
               // oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlCommand.ExecuteNonQuery();
               oSqlConnection.Close();




                i++;
                //}


            }
            objDrug = objDrug.Where(x => x != null).ToArray();
            db.claim_batch.AddRange(objDrug);
            db.SaveChanges();
            err = "Saved";

            bid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/MO";
            Session["bid"] = bid;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
          

        }
        public ActionResult genminmogvs()
        {
            int i = 0;
            string locid = (String)Session["userloc"];
            if (!String.IsNullOrEmpty(locid))
            {
                int userid = Convert.ToInt32(Session["UserID"]);
                string opdid = (String)Session["userlocid1"];
                string bid = "";
                claim_batch[] objDrug = new claim_batch[10000];
                var a1 = from s in db.claim_detail where (s.loc == locid && s.Status == 1 && s.mcat_id == "CAT011") orderby (s.created_date) descending select new getclaimdata { PID = s.claim_id, claimname = s.ClaimName, crdate = s.created_date, evntdate = s.claim_date, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
                foreach (var p in a1)
                {

                    //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                    //if (oldtestcnt < 1)
                    //{
                    claim_batch oDrug_Prescription = new claim_batch();
                    oDrug_Prescription.claim_id = p.PID;
                    oDrug_Prescription.batchid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/MOGVS";
                    oDrug_Prescription.userid = userid;
                    oDrug_Prescription.BatchDate = DateTime.Now;
                    oDrug_Prescription.UserCaT = 1;

                    objDrug[i] = oDrug_Prescription;

                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    string sqlQuery = " update  [dbo].[claim_detail] set Status=2 where claim_id='" + p.PID + "' ";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    // oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlCommand.ExecuteNonQuery();
                    oSqlConnection.Close();




                    i++;
                    //}


                }
                objDrug = objDrug.Where(x => x != null).ToArray();
                db.claim_batch.AddRange(objDrug);
                db.SaveChanges();
                err = "Saved";

                bid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/MOGVS";
                Session["bid"] = bid;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }


        }
        public ActionResult genmingvsgvs()
        {
            int i = 0;
            string locid = (String)Session["userloc"];
            if (!String.IsNullOrEmpty(locid))
            {
                int userid = Convert.ToInt32(Session["UserID"]);
                string opdid = (String)Session["userlocid1"];
                string bid = "";
                claim_batch[] objDrug = new claim_batch[10000];
                var a1 = from s in db.claim_detail where (s.loc == locid && s.Status == 3 && s.mcat_id == "CAT011"&& s.IsMinit == 0 ) orderby (s.created_date) descending select new getclaimdata { PID = s.claim_id, claimname = s.ClaimName, crdate = s.created_date, evntdate = s.claim_date, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
                foreach (var p in a1)
                {

                    //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                    //if (oldtestcnt < 1)
                    //{
                    claim_batch oDrug_Prescription = new claim_batch();
                    oDrug_Prescription.claim_id = p.PID;
                    oDrug_Prescription.batchid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/MOGVS";
                    oDrug_Prescription.userid = userid;
                    oDrug_Prescription.BatchDate = DateTime.Now;
                    oDrug_Prescription.UserCaT = 1;

                    objDrug[i] = oDrug_Prescription;

                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    string sqlQuery = " update  [dbo].[claim_detail] set IsMinit=1 where claim_id='" + p.PID + "' ";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    // oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlCommand.ExecuteNonQuery();
                    oSqlConnection.Close();




                    i++;
                    //}


                }
                objDrug = objDrug.Where(x => x != null).ToArray();
                db.claim_batch.AddRange(objDrug);
                db.SaveChanges();
                err = "Saved";

                bid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/GVS";
                Session["bid"] = bid;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }


        }
        public ActionResult genminnomo()
        {
            int i = 0;
            string locid = (String)Session["userloc"];
            int userid = Convert.ToInt32(Session["UserID"]);
            string opdid = (String)Session["userlocid1"];
            string bid = "";
            claim_batch[] objDrug = new claim_batch[10000];
            var a1 = from s in db.claim_detail where (s.loc == locid && s.Status == 4) orderby (s.created_date) descending select new getclaimdata { PID = s.claim_id, claimname = s.ClaimName, crdate = s.created_date, evntdate = s.claim_date, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
            foreach (var p in a1)
            {

                //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                //if (oldtestcnt < 1)
                //{
                claim_batch oDrug_Prescription = new claim_batch();
                oDrug_Prescription.claim_id = p.PID;
                oDrug_Prescription.batchid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/NOMOTSI";
                oDrug_Prescription.userid = userid;
                oDrug_Prescription.BatchDate = DateTime.Now;
                oDrug_Prescription.UserCaT = 1;

                objDrug[i] = oDrug_Prescription;

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                string sqlQuery = " update  [dbo].[claim_detail] set IsMinit=1 where claim_id='" + p.PID + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                // oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();




                i++;
                //}


            }
            objDrug = objDrug.Where(x => x != null).ToArray();
            db.claim_batch.AddRange(objDrug);
            db.SaveChanges();
            err = "Saved";

            bid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/NOMO";
            Session["bid"] = bid;
            return View();
        }


        public ActionResult genmindhs()
        {
            int i = 0;
            string locid = (String)Session["userloc"];
            int userid = Convert.ToInt32(Session["UserID"]);
            string opdid = (String)Session["userlocid1"];
            string bid = "";
            claim_batch[] objDrug = new claim_batch[10000];
            var a1 = from s in db.claim_detail where ( s.IsMinit == 0 && s.Status == 5) orderby (s.created_date) descending select new getclaimdata { PID = s.claim_id, claimname = s.ClaimName, crdate = s.created_date, evntdate = s.claim_date, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
            foreach (var p in a1)
            {

                //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                //if (oldtestcnt < 1)
                //{
                claim_batch oDrug_Prescription = new claim_batch();
                oDrug_Prescription.claim_id = p.PID;
                oDrug_Prescription.batchid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/DHS/C";
                oDrug_Prescription.userid = userid;
                oDrug_Prescription.BatchDate = DateTime.Now;
                oDrug_Prescription.UserCaT = 4;

                objDrug[i] = oDrug_Prescription;

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                string sqlQuery = " update  [dbo].[claim_detail] set IsMinit=1 where claim_id='" + p.PID + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                // oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();




                i++;
                //}


            }
            objDrug = objDrug.Where(x => x != null).ToArray();
            db.claim_batch.AddRange(objDrug);
            db.SaveChanges();
            err = "Saved";

            bid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/DHS/C";
            Session["bid"] = bid;
            return View();
        }
        public ActionResult genmintsi()
        {
            int i = 0;
            string locid = (String)Session["userloc"];
            int userid = Convert.ToInt32(Session["UserID"]);
            string opdid = (String)Session["userlocid1"];
            string bid = "";
            claim_batch[] objDrug = new claim_batch[10000];
            var a1 = from s in db.claim_detail where (s.loc == locid && s.IsMinit == 0 && s.Status == 3 && s.mcat_id != "CAT011") orderby (s.created_date) descending select new getclaimdata { PID = s.claim_id, claimname = s.ClaimName, crdate = s.created_date, evntdate = s.claim_date, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
            foreach (var p in a1)
            {

                //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                //if (oldtestcnt < 1)
                //{
                claim_batch oDrug_Prescription = new claim_batch();
                oDrug_Prescription.claim_id = p.PID;
                oDrug_Prescription.batchid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd")+"/TSI/C";
                oDrug_Prescription.userid = userid;
                oDrug_Prescription.BatchDate = DateTime.Now;
                oDrug_Prescription.UserCaT = 2;

                objDrug[i] = oDrug_Prescription;

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                string sqlQuery = " update  [dbo].[claim_detail] set IsMinit=1 where claim_id='" + p.PID + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                // oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();




                i++;
                //}


            }
            objDrug = objDrug.Where(x => x != null).ToArray();
            db.claim_batch.AddRange(objDrug);
            db.SaveChanges();
            err = "Saved";

            bid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/TSI/C";
            Session["bid"] = bid;
            return View();
        }
        public ActionResult genmintsigvs()
        {
            int i = 0;
            string locid = (String)Session["userloc"];
            int userid = Convert.ToInt32(Session["UserID"]);
            string opdid = (String)Session["userlocid1"];
            string bid = "";
            claim_batch[] objDrug = new claim_batch[10000];
            var a1 = from s in db.claim_detail where (s.loc == locid && s.IsMinit == 0 && s.Status == 3&&s.mcat_id== "CAT011") orderby (s.created_date) descending select new getclaimdata { PID = s.claim_id, claimname = s.ClaimName, crdate = s.created_date, evntdate = s.claim_date, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
            foreach (var p in a1)
            {

                //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                //if (oldtestcnt < 1)
                //{
                claim_batch oDrug_Prescription = new claim_batch();
                oDrug_Prescription.claim_id = p.PID;
                oDrug_Prescription.batchid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/TSI/GVS";
                oDrug_Prescription.userid = userid;
                oDrug_Prescription.BatchDate = DateTime.Now;
                oDrug_Prescription.UserCaT = 2;

                objDrug[i] = oDrug_Prescription;

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                string sqlQuery = " update  [dbo].[claim_detail] set IsMinit=1 where claim_id='" + p.PID + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                // oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();




                i++;
                //}


            }
            objDrug = objDrug.Where(x => x != null).ToArray();
            db.claim_batch.AddRange(objDrug);
            db.SaveChanges();
            err = "Saved";

            bid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/TSI/GVS";
            Session["bid"] = bid;
            return View();
        }
        public ActionResult genmingvsr()
        {
            int i = 0;
            string locid = (String)Session["userloc"];
            int userid = Convert.ToInt32(Session["UserID"]);
            string opdid = (String)Session["userlocid1"];
            string bid = "";
            claim_batch[] objDrug = new claim_batch[10000];
            var a1 = from s in db.claim_detail where (s.loc == locid && s.IsMinit == 0 && s.Status == 61) orderby (s.created_date) descending select new getclaimdata { PID = s.claim_id, claimname = s.ClaimName, crdate = s.created_date, evntdate = s.claim_date, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
            foreach (var p in a1)
            {

                //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                //if (oldtestcnt < 1)
                //{
                claim_batch oDrug_Prescription = new claim_batch();
                oDrug_Prescription.claim_id = p.PID;
                oDrug_Prescription.batchid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/TSI/R";
                oDrug_Prescription.userid = userid;
                oDrug_Prescription.BatchDate = DateTime.Now;
                oDrug_Prescription.UserCaT = 2;

                objDrug[i] = oDrug_Prescription;

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                string sqlQuery = " update  [dbo].[claim_detail] set IsMinit=1 where claim_id='" + p.PID + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                // oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();




                i++;
                //}


            }
            objDrug = objDrug.Where(x => x != null).ToArray();
            db.claim_batch.AddRange(objDrug);
            db.SaveChanges();
            err = "Saved";

            bid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/GVS/R";
            Session["bid"] = bid;
            return View();
        }
        public ActionResult genmintsir()
        {
            int i = 0;
            string locid = (String)Session["userloc"];
            int userid = Convert.ToInt32(Session["UserID"]);
            string opdid = (String)Session["userlocid1"];
            string bid = "";
            claim_batch[] objDrug = new claim_batch[10000];
            var a1 = from s in db.claim_detail where (s.loc == locid && s.IsMinit == 0&&s.Status==21) orderby (s.created_date) descending select new getclaimdata { PID = s.claim_id, claimname = s.ClaimName, crdate = s.created_date, evntdate = s.claim_date, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
            foreach (var p in a1)
            {

                //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                //if (oldtestcnt < 1)
                //{
                claim_batch oDrug_Prescription = new claim_batch();
                oDrug_Prescription.claim_id = p.PID;
                oDrug_Prescription.batchid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/TSI/R";
                oDrug_Prescription.userid = userid;
                oDrug_Prescription.BatchDate = DateTime.Now;
                oDrug_Prescription.UserCaT = 2;

                objDrug[i] = oDrug_Prescription;

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                string sqlQuery = " update  [dbo].[claim_detail] set IsMinit=1 where claim_id='" + p.PID + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                // oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();




                i++;
                //}


            }
            objDrug = objDrug.Where(x => x != null).ToArray();
            db.claim_batch.AddRange(objDrug);
            db.SaveChanges();
            err = "Saved";

            bid = locid + DateTime.Now.Date.ToString("yyyy/MM/dd") + "/TSI/R";
            Session["bid"] = bid;
            return View();
        }
        public ActionResult dhsv(int? page, int? page1, string id, string id1)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            var onePageOfProducts = (dynamic)null;
            var sickcate = (dynamic)null;
            char[] MyChar = { '/', '"', ' ' };
            string opdid = "";
            string locid = "";
            var title = new String[100];
            int specid = 0;
            string id2 = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            opdid = (String)Session["userlocid1"];
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}
        locid= (String)Session["userloc"];
            //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };

            //foreach (var item in opd)
            //{

            //    // opdid = item.LOCID;
            //}

            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id1))
            {
                id1 = id1.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id1))
            {
                Session["dtvalu"] = id1;
            }
            if (Session["dtvalu"] != null)
            {
                if (String.IsNullOrEmpty(id))
                {
                    id2 = Session["dtvalu"].ToString();
                }
            }

            if (!String.IsNullOrEmpty(id2) && !String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Parse(id2);
                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and batchid like'%dhs%'  group by batchid order by batchid desc";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {


         svcid = dataRow.Field<string>("batchid"),




     }).ToList();

                ViewBag.gtclbatch = joined3;
                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (b.ServiceNo.Contains(id) && s.created_date.Value.Day == dt1.Day && s.created_date.Value.Month == dt1.Month && s.created_date.Value.Year == dt1.Year) orderby (s.created_date) descending select new getclaimdata { IsDHSConfirmed = s.IsDHSConfirmed, regno = s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
            }
            else if (!String.IsNullOrEmpty(id2))
            {
                DateTime dt1 = DateTime.Parse(id2);
                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and batchid like'%dhs%'  group by batchid order by batchid desc";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {


         svcid = dataRow.Field<string>("batchid"),




     }).ToList();

                ViewBag.gtclbatch = joined3;
                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (s.loc == locid && s.created_date.Value.Day == dt1.Day && s.created_date.Value.Month == dt1.Month && s.created_date.Value.Year == dt1.Year) orderby (s.created_date) descending select new getclaimdata { IsDHSConfirmed = s.IsDHSConfirmed, regno = s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
            }
            else if (!String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Now.Date;
                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and batchid like'%dhs%'  group by batchid order by batchid desc";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {


         svcid = dataRow.Field<string>("batchid"),




     }).ToList();

                ViewBag.gtclbatch = joined3;
                if (opdid.Contains("cl"))
                {
                    var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id == Convert.ToInt32(specid)).Where(p => p.event_start == dt1) select new { s.title };
                }
                DateTime dd = DateTime.Now.Date;
                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join t in db.claim_batch on s.claim_id equals t.claim_id into off join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (b.ServiceNo.Contains(id)) orderby (s.created_date) descending select new getclaimdata { IsDHSConfirmed = s.IsDHSConfirmed, regno = s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString(), batchlist = off.Select(o => o.BatchDate),ulist= off.Select(o => o.UserCaT.ToString()) ,moamount=s.Doc_Amount.ToString(),tsimount=s.TSI_Amount.ToString(),dhsamount=s.DHS_Amount.ToString()};
                //db.Patient_Detail.Include(p => p.Patient).Include(p => p.Status1).Where(p => p.Patient.ServiceNo.Contains(id)).OrderByDescending(p => p.CreatedDate);
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);
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
                   // var patient_Detail = from s in db.Patient_Detail.Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).OrderByDescending(p => p.CreatedDate) select new getpatietdata { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, hyscompllain = s.History_OtherComplain, opddiag = s.OPD_Diagnosis, relasiont = f.Relationship };


                    // db.Patient_Detail.Include(p => p.Patient).OrderByDescending(p => p.CreatedDate);
                    var pageNumber = page ?? 1;
                    //onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);

                }
                else
                {
                    DateTime dd = DateTime.Now.Date;
                    DataTable oDataSetv7 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and batchid like'%dhs%'  group by batchid order by batchid desc";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;
                    //   oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetv7);
                    // oSqlConnection.Close();

                    var joined3 = oDataSetv7.AsEnumerable()
         .Select(dataRow => new getsickdata2
         {


             svcid = dataRow.Field<string>("batchid"),




         }).ToList();

                    ViewBag.gtclbatch = joined3;
                    //db.Patient_Detail.Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                    var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid  join t in db.claim_batch on s.claim_id equals t.claim_id into off join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (s.loc == locid && s.Status == 4) orderby (s.created_date) descending select new getclaimdata {IsDHSConfirmed=s.IsDHSConfirmed,  regno=s.RegisterNo, PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() , batchlist = off.Select(o => o.BatchDate), ulist = off.Select(o => o.UserCaT.ToString()), moamount = s.Doc_Amount.ToString(), tsimount = s.TSI_Amount.ToString(), dhsamount = s.DHS_Amount.ToString() };
                    var pageNumber = page ?? 1;
                    onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);
                }
            }

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }


        public ActionResult Delete(int? page, int? page1, string id, string id1)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            var onePageOfProducts = (dynamic)null;
            var sickcate = (dynamic)null;
            char[] MyChar = { '/', '"', ' ' };
            string opdid = "";
            string locid = "";
            var title = new String[100];
            int specid = 0;
            string id2 = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            opdid = (String)Session["userlocid1"];
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}
             locid=(String)Session["userloc"];
            //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };

            //foreach (var item in opd)
            //{

            //    // opdid = item.LOCID;
            //}

            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id1))
            {
                id1 = id1.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id1))
            {
                Session["dtvalu"] = id1;
            }
            if (Session["dtvalu"] != null)
            {
                if (String.IsNullOrEmpty(id))
                {
                    id2 = Session["dtvalu"].ToString();
                }
            }

            if (!String.IsNullOrEmpty(id2) && !String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Parse(id2);

                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where(b.ServiceNo.Contains(id) && s.created_date.Value.Day == dt1.Day && s.created_date.Value.Month == dt1.Month && s.created_date.Value.Year == dt1.Year) orderby (s.created_date) descending select new getclaimdata { PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
            }
            else if (!String.IsNullOrEmpty(id2))
            {
                DateTime dt1 = DateTime.Parse(id2);
                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (s.loc == locid && s.created_date.Value.Day == dt1.Day && s.created_date.Value.Month == dt1.Month && s.created_date.Value.Year == dt1.Year) orderby (s.created_date) descending select new getclaimdata { PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToPagedList(pageNumber, 10);
            }
            else if (!String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Now.Date;
                if (opdid.Contains("cl"))
                {
                    var shed = from s in db.Clinic_Schedule.Where(p => p.clinic_id == Convert.ToInt32(specid)).Where(p => p.event_start == dt1) select new { s.title };
                }
                DateTime dd = DateTime.Now.Date;
                var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (b.ServiceNo.Contains(id) && s.loc == locid) orderby (s.created_date) descending select new getclaimdata { PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };
                //db.Patient_Detail.Include(p => p.Patient).Include(p => p.Status1).Where(p => p.Patient.ServiceNo.Contains(id)).OrderByDescending(p => p.CreatedDate);
                var pageNumber = page ?? 1;
                onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);
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
                   // var patient_Detail = from s in db.Patient_Detail.Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).OrderByDescending(p => p.CreatedDate) select new getpatietdata { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, hyscompllain = s.History_OtherComplain, opddiag = s.OPD_Diagnosis, relasiont = f.Relationship };


                    // db.Patient_Detail.Include(p => p.Patient).OrderByDescending(p => p.CreatedDate);
                    var pageNumber = page ?? 1;
                   // onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);

                }
                else
                {
                    DateTime dd = DateTime.Now.Date;
                    DataTable oDataSetv7 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "  SELECT [batchid] FROM [MMS].[dbo].[claim_batch] where batchid like'" + locid + "%' and batchid like'%/gvs'  group by batchid order by batchid desc";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;
                    //   oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetv7);
                    // oSqlConnection.Close();

                    var joined3 = oDataSetv7.AsEnumerable()
         .Select(dataRow => new getsickdata2
         {


             svcid = dataRow.Field<string>("batchid"),




         }).ToList();

                    ViewBag.gtclbatch = joined3;



                    //db.Patient_Detail.Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                    var patient_Detail = from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (s.loc == locid && s.Status == 6) orderby (s.created_date) descending select new getclaimdata { PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc, claimamnt = s.ClaimAmount.ToString(), status = s.Status.ToString() };

                    var pageNumber = page ?? 1;
                    onePageOfProducts = patient_Detail.ToArray().ToPagedList(pageNumber, 10);
                }
            }

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }
        public JsonResult Submitclaim(string clitems, string PID)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };

            //   string NewString2 = Present_Complain.Trim(MyChar);
            string NewString3 = PID.Trim(MyChar);

            if (!String.IsNullOrEmpty(clitems))
            {
                clitems = clitems.Trim(MyChar);
            }


            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            //var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //     locid = item.LocationID;
            //}
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
            if (!String.IsNullOrEmpty(locid))
            {


                var objs = (dynamic)null;
                int objcount = 0;
                claim_detail[] objDrug = new claim_detail[1000];

                if (!String.IsNullOrEmpty(clitems))
                {

                    try
                    {
                        objs = JsonConvert.DeserializeObject<List<cLabreader>>(clitems);

                        objcount = objs.Count;


                        int i = 0;
                        IndexGeneration indi = new IndexGeneration();
                        string regno = indi.Createregno(locid);

                        foreach (cLabreader p in objs)
                        {

                            //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                            //if (oldtestcnt < 1)
                            //{
                            claim_detail oDrug_Prescription = new claim_detail();
                            oDrug_Prescription.pid = NewString3;
                            oDrug_Prescription.claim_id = Guid.NewGuid().ToString();
                            oDrug_Prescription.dgid = p.cdgid;
                            oDrug_Prescription.mcat_id = p.cmc_catid;
                            oDrug_Prescription.claim_date = Convert.ToDateTime(p.cevntdt);
                            oDrug_Prescription.loc = locid;
                            oDrug_Prescription.MobileNo = "0"+p.mob;
                            oDrug_Prescription.created_date = DateTime.Now.Date;
                            oDrug_Prescription.ClaimName = p.cnbenef;
                            oDrug_Prescription.IsMinit = 0;
                            oDrug_Prescription.Authority = p.authy;
                            oDrug_Prescription.Nurse = userid.ToString();
                            oDrug_Prescription.Nurse_Date = DateTime.Now;
                            oDrug_Prescription.ClaimAmount = Convert.ToDecimal(p.cclmamn);
                            decimal dmt = 0;
                            try
                            {
                                dmt = Convert.ToDecimal(p.doamnt);
                            }
                            catch (Exception ex)
                            {
                                dmt = 0;
                            }

                            oDrug_Prescription.Doc_Amount = dmt;
                            oDrug_Prescription.Status = 1;
                            oDrug_Prescription.RegisterNo = regno;
                            objDrug[i] = oDrug_Prescription;






                            i++;
                            //}


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




                        objDrug = objDrug.Where(x => x != null).ToArray();
                        db.claim_detail.AddRange(objDrug);
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
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Submitdhs(string cliitems)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
           
            //   string NewString2 = Present_Complain.Trim(MyChar);
            // string NewString3 = PID.Trim(MyChar);

            if (!String.IsNullOrEmpty(cliitems))
            {
                cliitems = cliitems.Trim(MyChar);
            }
            //if (!String.IsNullOrEmpty(rtrsn))
            //{
            //    rtrsn = rtrsn.Trim(MyChar);
            //}
            //if (!String.IsNullOrEmpty(cid))
            //{
            //    cid = cid.Trim(MyChar);
            //}

            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            //var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //     locid = item.LocationID;
            //}
            //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
            //opdid = (String)Session["userlocid1"];
            //foreach (var item in opd)
            //{

            //    //opdid = item.LOCID;
            //}
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}
            //Session["userlocid2"] = locid;
            TranferDetail oTranferDetails = new TranferDetail();
            var objs = (dynamic)null;
            int objcount = 0;
            int i = 0;
            claim_detail[] objDrug = new claim_detail[1000];
            var objs1 = JsonConvert.DeserializeObject<List<clreader>>(cliitems);
            foreach (clreader p in objs1)
            {

                if (!String.IsNullOrEmpty(p.cid1))
                {

                    try
                    {
                        var items = from s in db.claim_detail
                                    where (s.claim_id == p.cid1)
                                    orderby (s.created_date) descending
                                    select new
                                    {
                                        s.claim_id,
                                        s.pid,
                                        s.claim_date,
                                        s.mcat_id,
                                        s.dgid,
                                        s.created_date,
                                        s.loc,
                                        s.ClaimName,
                                        s.ClaimAmount,
                                        s.MobileNo,
                                        s.Status,
                                        s.Doc_Amount,
                                        s.RegisterNo,
                                        s.DHS_Amount,
                                        s.CWF_Amount,
                                        s.TSI_Amount,
                                        s.ClaimReturn,
                                        s.Authority
                                    };
                        //objs = JsonConvert.DeserializeObject<List<cLabreader>>(clitems);

                        //objcount = objs.Count;


                        //int i = 0;
                        //IndexGeneration indi = new IndexGeneration();
                        //string regno = indi.Createregno(locid);

                        int st = 0;
                        int? st1 = 0;

                        //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                        //if (oldtestcnt < 1)
                        //{
                        foreach (var ti in items)
                        {
                            claim_detail oDrug_Prescription = new claim_detail();
                            st1 = ti.Status;
                            oDrug_Prescription.claim_id = p.cid1;
                            oDrug_Prescription.pid = ti.pid;
                            oDrug_Prescription.claim_date = ti.claim_date;
                            oDrug_Prescription.mcat_id = ti.mcat_id;
                            oDrug_Prescription.dgid = ti.dgid;
                            oDrug_Prescription.created_date = ti.created_date;
                            oDrug_Prescription.loc = ti.loc;
                            oDrug_Prescription.ClaimName = ti.ClaimName;
                            oDrug_Prescription.IsMinit = 0;
                            oDrug_Prescription.ClaimAmount = ti.ClaimAmount;
                            oDrug_Prescription.MobileNo = ti.MobileNo;
                            oDrug_Prescription.DHS_Date = DateTime.Now;
                            oDrug_Prescription.DHS = userid.ToString();
                            if (!String.IsNullOrEmpty(p.rtrsn1))
                            {
                                st = 41;
                            }
                            if (String.IsNullOrEmpty(p.rtrsn1) && !String.IsNullOrEmpty(p.clmamnt1) && !p.clmamnt1.ToString().Equals("0.00"))
                            {
                                st = 5;
                            }

                            
                            oDrug_Prescription.Status = st;
                            oDrug_Prescription.Doc_Amount = ti.Doc_Amount;
                            oDrug_Prescription.RegisterNo = ti.RegisterNo;

                            string tsiam = "";
                            if (String.IsNullOrEmpty(p.clmamnt1))
                            {
                                tsiam = "0";
                            }
                            else
                            {
                                tsiam = p.clmamnt1;
                            }
                            oDrug_Prescription.DHS_Amount = Convert.ToDecimal(tsiam);
                            oDrug_Prescription.ClaimReturn = p.rtrsn1;
                            oDrug_Prescription.TSI_Amount = ti.TSI_Amount;
                            oDrug_Prescription.Authority = ti.Authority;

                            objDrug[i] = oDrug_Prescription;
                            i++;
                        }




                        oTranferDetails.PDID = p.cid1;
                        oTranferDetails.ToLoc = userid.ToString();
                        oTranferDetails.FromLoc = opdid;
                        oTranferDetails.TransferDate = DateTime.Now;
                        oTranferDetails.TransID = Guid.NewGuid().ToString();
                        oTranferDetails.TransStatus = st1;



                        //i++;
                        //}



                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
            if (ModelState.IsValid)
            {
                try
                {




                    // objDrug = objDrug.Where(x => x != null).ToArray();
                    //db.claim_detail.AddRange();
                    db.TranferDetails.Add(oTranferDetails);
                    objDrug = objDrug.Where(x => x != null).ToArray();
                    //db.claim_detail.AddRange(objDrug);
                    // db.Entry(objDrug).State = EntityState.Modified;
                    foreach (var p in objDrug)
                    {
                        //db.claim_detail.Attach(p);
                        db.Entry(p).State = EntityState.Modified;
                        db.SaveChanges();
                    }


                    err = "Saved";

                }
                catch (Exception ex)
                {
                    ex.ToString();

                }
            }

            return Json(err, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Submittsi(string cliitems)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
           
            //   string NewString2 = Present_Complain.Trim(MyChar);
            // string NewString3 = PID.Trim(MyChar);

            //if (!String.IsNullOrEmpty(clmamnt))
            //{
            //    clmamnt = clmamnt.Trim(MyChar);
            //}

            //int objcount = objs.Count;
            //Vital[] objVital = new Vital[objcount];
            //int i = 0;

            //foreach (Vitalreader p in objs)
            //{

            //    Vital oVital = new Vital();
            //    oVital.PDID = patient_Detail.PDID;
            //    oVital.VID = indi.CreateVID(i, patient_Detail.PDID);
            //    oVital.CreatedBy = userid.ToString();
            //    oVital.CreatedDate = DateTime.Now;
            //    oVital.LocationID = locid;
            //    oVital.LocID = opdid;
            //    oVital.PDID = patient_Detail.PDID;
            //    oVital.Reading_Time = DateTime.Now;
            //    oVital.ModifiedDate = DateTime.Now.Date;
            //    oVital.VTID = Convert.ToInt32(p.vid);
            //    oVital.VitalValues = p.amount;
            //    oVital.Status = 1;

            //    objVital[i] = oVital;
            //    i++;
            //    // db.Vitals.Add(oVital);

            //}

            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            //var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //     locid = item.LocationID;
            //}
            //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
            //opdid = (String)Session["userlocid1"];
            //foreach (var item in opd)
            //{

            //    //opdid = item.LOCID;
            //}
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}
            //Session["userlocid2"] = locid;
            locid = (String)Session["userloc"];
            TranferDetail oTranferDetails = new TranferDetail();
            var objs = (dynamic)null;
            int objcount = 0;
            int i = 0;
            claim_detail[] objDrug = new claim_detail[1000];
            var objs1 = JsonConvert.DeserializeObject<List<clreader>>(cliitems);
            foreach (clreader p in objs1) { 

                if (!String.IsNullOrEmpty(p.cid1))
            {

                try
                {
                    var items = from s in db.claim_detail where (s.claim_id == p.cid1)
                                orderby (s.created_date) descending select new
                                { s.claim_id, s.pid, s.claim_date, s.mcat_id, s.dgid,
                                    s.created_date, s.loc, s.ClaimName, s.ClaimAmount,
                                    s.MobileNo, s.Status, s.Doc_Amount, s.RegisterNo,
                                    s.DHS_Amount, s.CWF_Amount, s.TSI_Amount, s.ClaimReturn ,s.Authority};
                        //objs = JsonConvert.DeserializeObject<List<cLabreader>>(clitems);

                        //objcount = objs.Count;
                     

                        //int i = 0;
                        //IndexGeneration indi = new IndexGeneration();
                        //string regno = indi.Createregno(locid);

                        int st = 0;
                    int? st1 = 0;

                    //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                    //if (oldtestcnt < 1)
                    //{
                    foreach (var ti in items)
                    {
                            claim_detail oDrug_Prescription = new claim_detail();
                            st1 = ti.Status;
                        oDrug_Prescription.claim_id = p.cid1;
                        oDrug_Prescription.pid = ti.pid;
                        oDrug_Prescription.claim_date = ti.claim_date;
                        oDrug_Prescription.mcat_id = ti.mcat_id;
                        oDrug_Prescription.dgid = ti.dgid;
                        oDrug_Prescription.created_date = ti.created_date;
                        oDrug_Prescription.loc = ti.loc;
                        oDrug_Prescription.ClaimName = ti.ClaimName;
                        oDrug_Prescription.IsMinit = 0;
                        oDrug_Prescription.ClaimAmount = ti.ClaimAmount;
                        oDrug_Prescription.MobileNo = ti.MobileNo;
                            oDrug_Prescription.TSI = userid.ToString();
                            oDrug_Prescription.TSI_Date = DateTime.Now;
                            if (!String.IsNullOrEmpty(p.rtrsn1))
                        {
                            st = 21;
                        }
                        if (String.IsNullOrEmpty(p.rtrsn1) && (!ti.loc.Equals("WLA") || !ti.loc.Equals("KGL")||  !ti.loc.Equals("PLV")||  !ti.loc.Equals("MGR")|| !ti.loc.Equals("SGR")|| !ti.loc.Equals("BCL")|| !ti.loc.Equals("PGL")))
                        {
                            st = 3;
                        }
                        if (String.IsNullOrEmpty(p.rtrsn1) && (ti.loc.Equals("WLA") || ti.loc.Equals("KGL") ||  ti.loc.Equals("PLV") ||  ti.loc.Equals("MGR") || ti.loc.Equals("SGR") || ti.loc.Equals("BCL") || ti.loc.Equals("PGL")))
                        {
                            st = 4;
                        }
                        oDrug_Prescription.Status = st;
                        oDrug_Prescription.Doc_Amount = ti.Doc_Amount;
                        oDrug_Prescription.RegisterNo = ti.RegisterNo;
                            string tsiam = "";
                            if (String.IsNullOrEmpty(p.clmamnt1))
                            {
                                tsiam = "0";
                            }
                            else
                            {
                                tsiam = p.clmamnt1;
                            }
                        oDrug_Prescription.TSI_Amount = Convert.ToDecimal(tsiam);
                        oDrug_Prescription.ClaimReturn = p.rtrsn1;
                            oDrug_Prescription.Authority = ti.Authority;

                            objDrug[i] = oDrug_Prescription;
                            i++;
                        }




                        oTranferDetails.PDID = p.cid1;
                    oTranferDetails.ToLoc = userid.ToString();
                    oTranferDetails.FromLoc = opdid;
                    oTranferDetails.TransferDate = DateTime.Now;
                    oTranferDetails.TransID = Guid.NewGuid().ToString();
                    oTranferDetails.TransStatus = st1;



                    //i++;
                    //}



                }
                catch (Exception ex)
                {

                }
            }
               
            }
            if (ModelState.IsValid)
            {
                try
                {




                    // objDrug = objDrug.Where(x => x != null).ToArray();
                    //db.claim_detail.AddRange();
                    db.TranferDetails.Add(oTranferDetails);
                    objDrug = objDrug.Where(x => x != null).ToArray();
                    //db.claim_detail.AddRange(objDrug);
                    // db.Entry(objDrug).State = EntityState.Modified;
                    foreach (var p in objDrug)
                    {
                        //db.claim_detail.Attach(p);
                        db.Entry(p).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    
                    
                    err = "Saved";

                }
                catch (Exception ex)
                {
                    ex.ToString();

                }
            }

            return Json(err, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Submitgvs(string cliitems)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };

            //   string NewString2 = Present_Complain.Trim(MyChar);
            // string NewString3 = PID.Trim(MyChar);

            //if (!String.IsNullOrEmpty(clmamnt))
            //{
            //    clmamnt = clmamnt.Trim(MyChar);
            //}

            //int objcount = objs.Count;
            //Vital[] objVital = new Vital[objcount];
            //int i = 0;

            //foreach (Vitalreader p in objs)
            //{

            //    Vital oVital = new Vital();
            //    oVital.PDID = patient_Detail.PDID;
            //    oVital.VID = indi.CreateVID(i, patient_Detail.PDID);
            //    oVital.CreatedBy = userid.ToString();
            //    oVital.CreatedDate = DateTime.Now;
            //    oVital.LocationID = locid;
            //    oVital.LocID = opdid;
            //    oVital.PDID = patient_Detail.PDID;
            //    oVital.Reading_Time = DateTime.Now;
            //    oVital.ModifiedDate = DateTime.Now.Date;
            //    oVital.VTID = Convert.ToInt32(p.vid);
            //    oVital.VitalValues = p.amount;
            //    oVital.Status = 1;

            //    objVital[i] = oVital;
            //    i++;
            //    // db.Vitals.Add(oVital);

            //}

            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            //var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //     locid = item.LocationID;
            //}
            //var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
            //opdid = (String)Session["userlocid1"];
            //foreach (var item in opd)
            //{

            //    //opdid = item.LOCID;
            //}
            //var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            //foreach (var item in serW)
            //{

            //    locid = item.LocationID;
            //}
            //Session["userlocid2"] = locid;
            locid = (String)Session["userloc"];
            TranferDetail oTranferDetails = new TranferDetail();
            var objs = (dynamic)null;
            int objcount = 0;
            int i = 0;
            claim_detail[] objDrug = new claim_detail[1000];
            var objs1 = JsonConvert.DeserializeObject<List<clreader>>(cliitems);
            foreach (clreader p in objs1)
            {

                if (!String.IsNullOrEmpty(p.cid1))
                {

                    try
                    {
                        var items = from s in db.claim_detail
                                    where (s.claim_id == p.cid1)
                                    orderby (s.created_date) descending
                                    select new
                                    {
                                        s.claim_id,
                                        s.pid,
                                        s.claim_date,
                                        s.mcat_id,
                                        s.dgid,
                                        s.created_date,
                                        s.loc,
                                        s.ClaimName,
                                        s.ClaimAmount,
                                        s.MobileNo,
                                        s.Status,
                                        s.Doc_Amount,
                                        s.RegisterNo,
                                        s.DHS_Amount,
                                        s.CWF_Amount,
                                        s.TSI_Amount,
                                        s.ClaimReturn,
                                        s.Authority
                                    };
                        //objs = JsonConvert.DeserializeObject<List<cLabreader>>(clitems);

                        //objcount = objs.Count;


                        //int i = 0;
                        //IndexGeneration indi = new IndexGeneration();
                        //string regno = indi.Createregno(locid);

                        int st = 0;
                        int? st1 = 0;

                        //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                        //if (oldtestcnt < 1)
                        //{
                        foreach (var ti in items)
                        {
                            claim_detail oDrug_Prescription = new claim_detail();
                            st1 = ti.Status;
                            oDrug_Prescription.claim_id = p.cid1;
                            oDrug_Prescription.pid = ti.pid;
                            oDrug_Prescription.claim_date = ti.claim_date;
                            oDrug_Prescription.mcat_id = ti.mcat_id;
                            oDrug_Prescription.dgid = ti.dgid;
                            oDrug_Prescription.created_date = ti.created_date;
                            oDrug_Prescription.loc = ti.loc;
                            oDrug_Prescription.ClaimName = ti.ClaimName;
                            oDrug_Prescription.IsMinit = 0;
                            oDrug_Prescription.ClaimAmount = ti.ClaimAmount;
                            oDrug_Prescription.MobileNo = ti.MobileNo;
                            oDrug_Prescription.TSI = userid.ToString();
                            oDrug_Prescription.TSI_Date = DateTime.Now;
                            if (!String.IsNullOrEmpty(p.rtrsn1))
                            {
                                st = 61;
                            }
                            if (String.IsNullOrEmpty(p.rtrsn1) && (!ti.loc.Equals("WLA") || !ti.loc.Equals("KGL") || !ti.loc.Equals("PLV") || !ti.loc.Equals("MGR") || !ti.loc.Equals("SGR") || !ti.loc.Equals("BCL") || !ti.loc.Equals("PGL")))
                            {
                                st = 3;
                            }
                            if (String.IsNullOrEmpty(p.rtrsn1) && (ti.loc.Equals("WLA") || ti.loc.Equals("KGL") || ti.loc.Equals("PLV") || ti.loc.Equals("MGR") || ti.loc.Equals("SGR") || ti.loc.Equals("BCL") || ti.loc.Equals("PGL")))
                            {
                                st = 3;
                            }
                            oDrug_Prescription.Status = st;
                            oDrug_Prescription.Doc_Amount = ti.Doc_Amount;
                            oDrug_Prescription.RegisterNo = ti.RegisterNo;
                            string tsiam = "";
                            if (String.IsNullOrEmpty(p.clmamnt1))
                            {
                                tsiam = "0";
                            }
                            else
                            {
                                tsiam = p.clmamnt1;
                            }
                            oDrug_Prescription.TSI_Amount = Convert.ToDecimal(tsiam);
                            oDrug_Prescription.ClaimReturn = p.rtrsn1;
                            oDrug_Prescription.Authority = ti.Authority;

                            objDrug[i] = oDrug_Prescription;
                            i++;
                        }




                        oTranferDetails.PDID = p.cid1;
                        oTranferDetails.ToLoc = userid.ToString();
                        oTranferDetails.FromLoc = opdid;
                        oTranferDetails.TransferDate = DateTime.Now;
                        oTranferDetails.TransID = Guid.NewGuid().ToString();
                        oTranferDetails.TransStatus = st1;



                        //i++;
                        //}



                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
            if (ModelState.IsValid)
            {
                try
                {




                    // objDrug = objDrug.Where(x => x != null).ToArray();
                    //db.claim_detail.AddRange();
                    db.TranferDetails.Add(oTranferDetails);
                    objDrug = objDrug.Where(x => x != null).ToArray();
                    //db.claim_detail.AddRange(objDrug);
                    // db.Entry(objDrug).State = EntityState.Modified;
                    foreach (var p in objDrug)
                    {
                        //db.claim_detail.Attach(p);
                        db.Entry(p).State = EntityState.Modified;
                        db.SaveChanges();
                    }


                    err = "Saved";

                }
                catch (Exception ex)
                {
                    ex.ToString();

                }
            }

            return Json(err, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Submitclup(string cliitems)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };

         

            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            
            TranferDetail oTranferDetails = new TranferDetail();
            var objs = (dynamic)null;
            int objcount = 0;
            int i = 0;
            oSqlConnection = new SqlConnection(conStr);
            SqlCommand command;
            claim_detail[] objDrug = new claim_detail[1000];
            var objs1 = JsonConvert.DeserializeObject<List<clupreader>>(cliitems);
            foreach (clupreader p in objs1)
            {

                if (!String.IsNullOrEmpty(p.claimcat1))
                {

                    try
                    {
                        command = new SqlCommand("UPDATE claim_detail SET mcat_id = @tracking WHERE claim_id = @order", oSqlConnection);

                        command.Parameters.AddWithValue("@order", p.cid1);
                        command.Parameters.AddWithValue("@tracking", p.claimcat1);
                        if (oSqlConnection.State == ConnectionState.Closed)
                        {
                            oSqlConnection.Open();
                        }
                        int rowsUpdated = command.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {

                    }
                }
                 if (!String.IsNullOrEmpty(p.docamount1))
                {

                    try
                    {
                        command = new SqlCommand("UPDATE claim_detail SET Doc_Amount = @tracking WHERE claim_id = @order", oSqlConnection);

                        command.Parameters.AddWithValue("@order", p.cid1);
                        command.Parameters.AddWithValue("@tracking", p.docamount1);
                        if (oSqlConnection.State == ConnectionState.Closed)
                        {
                            oSqlConnection.Open();
                        }
                        int rowsUpdated = command.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {

                    }
                }
                 if (!String.IsNullOrEmpty(p.clmamnt1))
                {

                    try
                    {
                        command = new SqlCommand("UPDATE claim_detail SET ClaimAmount = @tracking WHERE claim_id = @order", oSqlConnection);

                        command.Parameters.AddWithValue("@order", p.cid1);
                        command.Parameters.AddWithValue("@tracking", p.clmamnt1);
                        if (oSqlConnection.State == ConnectionState.Closed)
                        {
                            oSqlConnection.Open();
                        }
                        int rowsUpdated = command.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
           

            return Json(err, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getclaim(string id, string id1)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }

            if (!String.IsNullOrEmpty(id1))
            {
                id1 = id1.Trim(MyChar);
            }
            DateTime sd = Convert.ToDateTime(id1);
            var items =from s in db.claim_detail join b in db.Patients on s.pid equals b.PID join c in db.claim_catagory on s.mcat_id equals c.mc_catid join d in db.CatDaignosis on s.dgid equals d.dgid join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID where (s.claim_date.Value.Day == sd.Day && s.claim_date.Value.Month == sd.Month && s.claim_date.Value.Year == sd.Year) where (b.ServiceNo == id) orderby (s.created_date) descending select new getclaimdata { PID = s.pid, claimname = s.ClaimName, serviceno = b.ServiceNo, crdate = s.created_date, evntdate = s.claim_date, catagory = c.mc_catdetail, diagnosis = d.dgdetail, relasiont = f.Relationship, loc = s.loc,claimamnt=s.ClaimAmount.ToString() };

            return Json(items, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getclaimmob(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }


            // DateTime sd = Convert.ToDateTime(id1);
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT MobileNo  FROM [MMS].[dbo].[claim_detail] as a inner join [MMS].[dbo].[Patient] as b on a.pid=b.pid where b.ServiceNo='" + id + "' ";

            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();

           string sikcnt =oDataSet.Rows[0]["MobileNO"].ToString();
            return Json(sikcnt, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getgvsclaim(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }

          
           // DateTime sd = Convert.ToDateTime(id1);
            var items = from s in db.VwAllPaidClaimGVS  where (s.ServiceNo==id)  orderby (s.DateSorted) descending select new  { s.BeneficiaryName,s.Amount,s.CategoryName,s.Date,s.Description,s.Name,s.TotalAmount,s.ServiceNo};

            return Json(items, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Getgvsclaim1(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT b.ServiceNo  FROM [MMS].[dbo].[claim_detail] as a left join [dbo].[Patient] as b on a.pid=b.pid where a.RegisterNo='" + id + "' ";

            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();

            string sikcnt = oDataSet.Rows[0]["ServiceNo"].ToString();

            // DateTime sd = Convert.ToDateTime(id1);
            var items = from s in db.VwAllPaidClaimGVS where (s.ServiceNo == sikcnt) orderby (s.DateSorted) descending select new { s.BeneficiaryName, s.Amount, s.CategoryName, s.Date, s.Description, s.Name, s.TotalAmount, s.ServiceNo };

            return Json(items, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Getgvsclaim2(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            string sqlQuery;
            

            // DateTime sd = Convert.ToDateTime(id1);
            var items = from s in db.VwAllPaidClaimGVS where (s.ServiceNo == id) orderby (s.DateSorted) descending select new { s.BeneficiaryName, s.Amount, s.CategoryName, s.Date, s.Description, s.Name, s.TotalAmount, s.ServiceNo };

            return Json(items, JsonRequestBehavior.AllowGet);

        }

        public class clreader
        {

            public string cid1 { get; set; }
            public string clmamnt1 { get; set; }
            public string rtrsn1 { get; set; }

        }
        public class clupreader
        {

            public string cid1 { get; set; }
            public string clmamnt1 { get; set; }
            public string docamount1 { get; set; }
            public string claimcat1 { get; set; }
        }

        public JsonResult Gettsiclaim(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            DataTable odsvoltxndata = new DataTable();
            List<getclaimdata> items = new List<getclaimdata>();
            try
            {

                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT  max(COALESCE(b.surname, (c.surname)) ) sname  ,max( b.Rank ) rnkname, max(c.ServiceNo)  sno    ,max(b.Initials )  inililes, max(c.pid)  pid,max(a.Doc_Amount)  docamount ,max(a.claim_id)  cid ,  max(a.ClaimAmount) "+

 "     appamnt    ,max(a.RegisterNo)  RegisterNo ,max(d.mc_catdetail)  cat ,max(d.mc_catid)  catid ,max(a.ClaimName) as benef FROM[MMS].[dbo].[claim_detail] as a with(nolock)	" +
 "  inner join[MMS]   .[dbo].  [Patient] as c on a.pid=c.pid inner join[MMS].[dbo].[PersonalDetails] as b on    c.ServiceNo= 	" +
 "   b.ServiceNo inner join[MMS].[dbo].[claim_catagory] as d on d.mc_catid=a.mcat_id where a.RegisterNo='" + id + "' 	" +
"	and b.surname!='0'  group by a.claim_id  ";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                 items = odsvoltxndata.AsEnumerable()
          .Select(dataRow => new getclaimdata
          {
              claimname = dataRow.Field<string>("benef"),
              serviceno = dataRow.Field<string>("sno"),
              Surname = dataRow.Field<string>("sname"),
              initials = dataRow.Field<string>("inililes"),
              rank = dataRow.Field<string>("rnkname"),
              cid = dataRow.Field<string>("cid"),
              moamount = dataRow.Field<decimal?>("docamount").ToString(),
              regno = dataRow.Field<string>("RegisterNo"),
              catagory = dataRow.Field<string>("cat"),
              claimamnt = dataRow.Field<decimal?>("appamnt").ToString(),
              diagnosis = dataRow.Field<string>("catid"),
              
          }).ToList();
                // DateTime sd = Convert.ToDateTime(id1);
            }
            catch (Exception ex)
            {

            }
            return Json(items, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getretiredclaim(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            String sr = "";
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            DataTable odsvoltxndata = new DataTable();
            List<getclaimdata> items = new List<getclaimdata>();
            try
            {

                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT  COALESCE(NULLIF((case when  b.Surname != '0'   then b.Surname end), ''), " +
"  (''))  	sname  ,(case when b.Surname != '0' then b.Rank end) rnkname, (a.sNo)sno " +
 "   ,(case when b.Surname != '0' then b.Initials end)  inililes " +


 "      FROM[MMS].[dbo].[View_RetiredMemberStatus] as a with(nolock) " +

 "     left join[MMS].[dbo].[PersonalDetails] as b on    a.sNo=  b.ServiceNo where a.sNo='" + id + "' and b.surname!='0'   ";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                //       items = odsvoltxndata.AsEnumerable()
                //.Select(dataRow => new getclaimdata
                //{
                //    serviceno = dataRow.Field<string>("sno"),
                //    Surname = dataRow.Field<string>("sname"),
                //    initials = dataRow.Field<string>("inililes"),
                //    rank = dataRow.Field<string>("rnkname"),




                //}).ToList();
               
                if (odsvoltxndata.Rows.Count>0)
                {
                    sr = "Member is a active retired member";
                }
                else
                {
                    sr = "Member is inactive and not yet retired member";
                }
                // DateTime sd = Convert.ToDateTime(id1);
            }
            catch (Exception ex)
            {

            }
            return Json(sr, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getremclaim(string id, string id1)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id1))
            {
                id1 = id1.Trim(MyChar);
            }
            DataTable odsvoltxndata = new DataTable();
            List<getclaimdata> items = new List<getclaimdata>();
            try
            {

                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  COALESCE(NULLIF((case when  b.Surname != '0'   then b.Surname end), ''), "+
 " (''))  	sname  ,(case when b.Surname != '0' then b.Rank end) rnkname, (a.claSno)sno " +
  "  ,(case when b.Surname != '0' then b.Initials end)  inililes " +
    " ,a.billAmount,a.catAnnualLimit,c.mc_catdetail,a.Balance,a.claClaimYear " +

 "      FROM[MMS].[dbo].[View_PersonalLimits] as a with(nolock)" +

 "       left join[MMS].[dbo].[claim_catagory] as c on    a.claCatCode=  c.mc_catid " +
" left join[MMS].[dbo].[PersonalDetails] as b on    a.claSno=  b.ServiceNo where a.claSno='"+id+"' and a.claClaimYear='"+id1+"' and b.surname!='0'   ";

oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                items = odsvoltxndata.AsEnumerable()
         .Select(dataRow => new getclaimdata
         {
             serviceno = dataRow.Field<string>("sno"),
             Surname = dataRow.Field<string>("sname"),
             initials = dataRow.Field<string>("inililes"),
             rank = dataRow.Field<string>("rnkname"),
             cid = dataRow.Field<decimal?>("billAmount").ToString(),
             moamount = dataRow.Field<decimal?>("catAnnualLimit").ToString(),
             regno = dataRow.Field<decimal?>("Balance").ToString(),
             catagory = dataRow.Field<string>("mc_catdetail"),
             loc = dataRow.Field<int?>("claClaimYear").ToString(),


         }).ToList();
                // DateTime sd = Convert.ToDateTime(id1);
            }
            catch (Exception ex)
            {

            }
            return Json(items, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getretclaim(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            DataTable odsvoltxndata = new DataTable();
            List<getclaimdata> items = new List<getclaimdata>();
            try
            {

                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT  COALESCE(NULLIF((case when  b.Surname != '0'   then b.Surname end), ''), "+
 " (''))  	sname  ,(case when b.Surname != '0' then b.Rank end) rnkname, (a.sNo)sno" +
 "   ,(case when b.Surname != '0' then b.Initials end)  inililes,  (a.Amount)docamount" +
"	 ,  (a.reason)reason ,(a.RegisterNo)RegisterNo ,a.CreatedDate" +

 "      FROM[MMS].[dbo].[Vw_ReturnClaim] as a with(nolock)" +

 "     left join[MMS].[dbo].[PersonalDetails] as b on    a.sNo=  b.ServiceNo where(a.RegisterNo= '"+id+"' or a.sNo= '"+id+ "') and b.surname!='0'  union " +
" SELECT COALESCE(NULLIF((case when  b.Surname != '0'   then b.Surname end), ''),  ('')) " +
"  	sname  ,(case when b.Surname != '0' then b.Rank end) rnkname, (d.ServiceNo)sno   ,(case when b.Surname " +
 "   != '0' then b.Initials end)  inililes,  (c.Doc_Amount)docamount  ,  (c.ClaimReturn)reason ,(c.RegisterNo)RegisterNo ,(c.created_date)CreatedDate " +

"        FROM " +
  "           [dbo].[claim_detail] as c left join[dbo].[Patient] as d on c.pid =d.PID left join[MMS].[dbo].[PersonalDetails] as b on    d.ServiceNo=  b.ServiceNo " +
" where(c.RegisterNo= '" + id + "' or d.ServiceNo= '" + id + "') and b.surname!='0' and c.Status= 41 ";

oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                items = odsvoltxndata.AsEnumerable()
         .Select(dataRow => new getclaimdata
         {
             serviceno = dataRow.Field<string>("sno"),
             Surname = dataRow.Field<string>("sname"),
             initials = dataRow.Field<string>("inililes"),
             rank = dataRow.Field<string>("rnkname"),
             cid = dataRow.Field<string>("reason"),
             moamount = dataRow.Field<decimal?>("docamount").ToString(),
             regno = dataRow.Field<string>("RegisterNo"),
             crdate = dataRow.Field<DateTime?>("CreatedDate"),
            
            

         }).ToList();
                // DateTime sd = Convert.ToDateTime(id1);
            }
            catch (Exception ex)
            {

            }
            return Json(items, JsonRequestBehavior.AllowGet);

        }
        public JsonResult setdhsrec(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            DataTable odsvoltxndata = new DataTable();
            List<getclaimdata> items = new List<getclaimdata>();
            try
            {

                oSqlCommand = new SqlCommand();
                sqlQuery = " update   claim_detail set IsDHSConfirmed=1  where RegisterNo='" + id + "' ";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
               
            }
            catch (Exception ex)
            {

            }
            return Json("", JsonRequestBehavior.AllowGet);

        }

        public JsonResult Getdhsclaim(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            DataTable odsvoltxndata = new DataTable();
            List<getclaimdata> items = new List<getclaimdata>();
            try
            {

                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT  COALESCE(NULLIF(max(case when c.RelationshipType = 1    and b.Surname != '0'  " +
    " then b.Surname end), ''), max(c.surname))  	sname  ,max(case when c.RelationshipType = " +
    "  1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno    ,max(case when c.RelationshipType = " +
    "   1 then b.Initials  end)  inililes,  max(c.pid)  pid,max(a.Doc_Amount)  docamount ,max(a.claim_id)  cid , " +
     " max(a.ClaimAmount)  appamnt ,max(a.RegisterNo)  RegisterNo ,max(d.mc_catdetail)  cat " +
     "  FROM[MMS].[dbo].[claim_detail] as a with(nolock)   left join[MMS] " +
     "  .[dbo].  [Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] as b on " +
     "   c.ServiceNo=  b.ServiceNo left join[MMS].[dbo].[claim_catagory] as d on d.mc_catid=a. " +
    " mcat_id where a.RegisterNo='" + id + "' and a.status=4 ";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                items = odsvoltxndata.AsEnumerable()
         .Select(dataRow => new getclaimdata
         {
             serviceno = dataRow.Field<string>("sno"),
             Surname = dataRow.Field<string>("sname"),
             initials = dataRow.Field<string>("inililes"),
             rank = dataRow.Field<string>("rnkname"),
             cid = dataRow.Field<string>("cid"),
             moamount = dataRow.Field<decimal?>("docamount").ToString(),
             regno = dataRow.Field<string>("RegisterNo"),
             catagory = dataRow.Field<string>("cat"),
             claimamnt = dataRow.Field<decimal?>("appamnt").ToString(),

         }).ToList();
                // DateTime sd = Convert.ToDateTime(id1);
            }
            catch (Exception ex)
            {

            }
            return Json(items, JsonRequestBehavior.AllowGet);

        }
        // POST: claim_detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            claim_detail claim_detail = db.claim_detail.Find(id);
            db.claim_detail.Remove(claim_detail);
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
    public class cLabreader
    {

        public string cevntdt { get; set; }

        public string cnbenef { get; set; }
        public string cdgid { get; set; }
        public string cmc_catid { get; set; }
        public string cclmamn { get; set; }
        public string doamnt { get; set; }
        public string fwdto { get; set; }
        public string mob { get; set; }
        public string authy { get; set; }
    }
}
