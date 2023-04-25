using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using PagedList;
using MMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMS.Models;

using System.Web.Security;
using System.Data.SqlClient;
using System.Data;

namespace MMS.Controllers
{
    public class Lab_ReportController : Controller
    {
        private MMSEntities db = new MMSEntities();
       
     
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
     
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        string sqlQuery;
        // GET: Lab_Report
        //public ActionResult Index()
        //{
        //    var lab_Report = db.Lab_Report.Include(l => l.Lab_SubCategory).Include(l => l.Patient_Detail);
        //    return View(lab_Report.ToList());
        //}

        // GET: Lab_Report/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Lab_Report lab_Report = db.Lab_Report.Find(id);
        //    if (lab_Report == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lab_Report);
        //}
        public JsonResult loadlabtest(string id,string catid)
        {
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);
            string NewString1 = catid.Trim(MyChar);
            int vbn = Convert.ToInt32(NewString1);
            var ser = from s in db.Lab_Report
                      join h in db.Lab_SubCategory on s.LabTestID equals h.LabTestID
                      join v in db.Lab_MainCategory on h.CategoryID equals v.CategoryID
  where(s.TestSID == NewString)where(v.TubeCategory == vbn)  orderby v.CategoryName, h.SubCategoryID select new {s.Lab_Index, s.LabTestID,h.ReferenceRange,h.ReferenceRangeUnit ,h.SubCategoryName,s.TestSID};
            
            return Json(ser, JsonRequestBehavior.AllowGet);
        }

        public JsonResult viewlabtest(string id, string catid)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");

            char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                string NewString1 = catid.Trim(MyChar);
                string Lab_Index = "";
                string LabTestID = "";
                string ReferenceRange = "";
                string ReferenceRangeUnit = "";
                string SubCategoryName = "";
                string testResult = "";
                string teststatus = "";
                string CategoryName = "";
                string ServiceNo = "";
                string Surname = "";
                string Initials = "";
                string RNK_NAME = "";
                string Relationship = "";
                string RequestedTime = "";
            string appvby1 = "";
            int? stp =0;
            int? tubcat = 0;
            string testsid = "";
            int tb = Convert.ToInt32(NewString1);

            DataTable oDataSet39c = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "   select top 1 ServiceNo from [dbo].[Lab_Report] as a left join [dbo].[Users] as b on a.ApprovedUser=b.userid where TestSID='" + NewString + "' and IsApproved=1";
            
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
          
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet39c);

            foreach (DataRow row in oDataSet39c.Rows)
            {

                appvby1 = row["ServiceNo"].ToString();
            }


                var ser = from s in db.Lab_Report
                          where (s.TestSID == NewString)
                         
                          where (s.Issued == "1")
                          join h in db.Lab_SubCategory on s.LabTestID equals h.LabTestID
                          join v in db.Lab_MainCategory on h.CategoryID equals v.CategoryID
                          join x in db.Patient_Detail on s.PDID equals x.PDID
                          join y in db.Patients on x.PID equals y.PID
                        
                          join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID
                          where (v.TubeCategory == tb)
                          orderby v.CategoryName, h.SubCategoryID
                          select new getreportdata {tubcat=v.TubeCategory, Testsid = s.TestSID, Lab_Index = s.Lab_Index, LabTestID = s.LabTestID, ReferenceRange = h.ReferenceRange, ReferenceRangeUnit = h.ReferenceRangeUnit, SubCategoryName = h.SubCategoryName, testResult = s.testResult, teststatus = s.teststatus, CategoryName = h.Lab_MainCategory.CategoryName, ServiceNo = y.ServiceNo, Surname = y.Surname, Initials = y.Initials, RNK_NAME =y.rank1.RNK_NAME, Relationship = z.Relationship, RequestedTime = s.IssuedTime.ToString(),stp=y.Service_Type,appvby=appvby1 };
               // var ser1 = from s in db.Lab_Report.Where(p => p.TestSID == "slaf") orderby s.Lab_SubCategory.SubCategoryID select new getreportdata { Lab_Index = s.Lab_Index, LabTestID = s.LabTestID, ReferenceRange = s.Lab_SubCategory.ReferenceRange, ReferenceRangeUnit = s.Lab_SubCategory.ReferenceRangeUnit, SubCategoryName = s.Lab_SubCategory.SubCategoryName, testResult = s.testResult, teststatus = s.teststatus, CategoryName = s.Lab_SubCategory.Lab_MainCategory.CategoryName, ServiceNo = s.Patient_Detail.Patient.ServiceNo, Surname = s.Patient_Detail.Patient.Surname, Initials = s.Patient_Detail.Patient.Initials, RNK_NAME = s.Patient_Detail.Patient.rank1.RNK_NAME, Relationship = s.Patient_Detail.Patient.RelationshipType1.Relationship, RequestedTime = s.RequestedTime.ToString() };
            List<getreportdata> df = new List<getreportdata>();
            try
            {
                foreach (var item in ser)
                {
                    tubcat = item.tubcat;
                    testsid = item.Testsid;
                    Lab_Index = item.Lab_Index;
                    LabTestID = item.LabTestID;
                    ReferenceRange = item.ReferenceRange;
                    ReferenceRangeUnit = item.ReferenceRangeUnit;
                    SubCategoryName = item.SubCategoryName;
                    testResult = item.testResult;
                    teststatus = item.teststatus;
                    CategoryName = item.CategoryName;
                    ServiceNo = item.ServiceNo;
                    Surname = item.Surname;
                    Initials = item.Initials;
                    RNK_NAME = item.RNK_NAME;
                    Relationship = item.Relationship;
                    RequestedTime = item.RequestedTime.ToString();
                    stp= item.stp;
                    appvby1 = item.appvby;

                    if (Relationship == "SELF")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.MedicalCategories on s.SNo equals b.SNo
                                                into sc
                                                from b in sc.DefaultIfEmpty()
                                                where s.ServiceNo == ServiceNo && s.ServiceType==stp
                                                select new getreportdata {tubcat=tubcat,Testsid=testsid,  Lab_Index = Lab_Index, LabTestID = LabTestID, ReferenceRange = ReferenceRange, ReferenceRangeUnit = ReferenceRangeUnit, SubCategoryName = SubCategoryName, testResult = testResult, teststatus = teststatus, CategoryName = CategoryName, ServiceNo = ServiceNo, Surname = s.Surname, Initials = s.Initials, RNK_NAME = s.Rank, Relationship = Relationship, RequestedTime = RequestedTime.ToString(),appvby= appvby1 };

                        if (PersonResultList1.Count() > 0)
                        {

                            df.Add(PersonResultList1.First());
                        }
                        else
                        {
                           // df.Add(ser.First());
                            // iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            //  stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    if (Relationship.ToLower() == "spouse")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.SpouseDetails on s.SNo equals b.SNo
                                                where s.ServiceNo == item.ServiceNo && s.ServiceType == stp
                                                select new getreportdata { tubcat = tubcat, Testsid = testsid, Lab_Index = Lab_Index, LabTestID = LabTestID, ReferenceRange = ReferenceRange, ReferenceRangeUnit = ReferenceRangeUnit, SubCategoryName = SubCategoryName, testResult = testResult, teststatus = teststatus, CategoryName = CategoryName, ServiceNo = ServiceNo, Surname = b.SpouseName, Initials = " ", RNK_NAME = s.Rank, Relationship = Relationship, RequestedTime = RequestedTime.ToString(), appvby = appvby1 };


                        if (PersonResultList1.Count() > 0)
                        {
                            // ser.AsEnumerable().Concat(PersonResultList1);
                            //  ser1 = ser1.a(PersonResultList1);
                            df.Add(PersonResultList1.First());
                        }
                        else
                        {
                           // df.Add(ser.First());
                            // iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            //stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    if (Relationship.ToLower() == "father")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo
                                                where s.ServiceNo == item.ServiceNo && b.Relationship == "Father" && s.ServiceType == stp
                                                select new getreportdata { tubcat = tubcat, Testsid = testsid, Lab_Index = Lab_Index, LabTestID = LabTestID, ReferenceRange = ReferenceRange, ReferenceRangeUnit = ReferenceRangeUnit, SubCategoryName = SubCategoryName, testResult = testResult, teststatus = teststatus, CategoryName = CategoryName, ServiceNo = ServiceNo, Surname = b.ParentName, Initials = " ", RNK_NAME = s.Rank, Relationship = Relationship, RequestedTime = RequestedTime.ToString(), appvby = appvby1 };


                        if (PersonResultList1.Count() > 0)
                        {
                            //  ser.AsEnumerable().Concat(PersonResultList1);
                            // ser1 = ser1.Concat(PersonResultList1);
                            df.Add(PersonResultList1.First());
                        }
                        else
                        {
                           // df.Add(ser.First());
                            // iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            //  stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    if (Relationship.ToLower() == "mother")
                    {
                        var PersonResultList1 = from s in db.PersonalDetails
                                                join b in db.parents on s.SNo equals b.SNo

                                                where s.ServiceNo == item.ServiceNo && b.Relationship == "Mother" && s.ServiceType == stp
                                                select new getreportdata { tubcat = tubcat, Testsid = testsid, Lab_Index = Lab_Index, LabTestID = LabTestID, ReferenceRange = ReferenceRange, ReferenceRangeUnit = ReferenceRangeUnit, SubCategoryName = SubCategoryName, testResult = testResult, teststatus = teststatus, CategoryName = CategoryName, ServiceNo = ServiceNo, Surname = b.ParentName, Initials = " ", RNK_NAME = s.Rank, Relationship = Relationship, RequestedTime = RequestedTime.ToString(), appvby = appvby1 };


                        if (PersonResultList1.Count() > 0)
                        {
                            //ser1 = ser1.Concat(PersonResultList1);
                            // ser.AsEnumerable().Concat(PersonResultList1);
                            df.Add(PersonResultList1.First());
                        }
                        else
                        {
                            //df.Add(ser.First());
                            //  iserror = "3";
                        }
                        foreach (var itemw in PersonResultList1)
                        {
                            // stype = Convert.ToInt32(itemw.Service_Type);
                        }




                    }
                    if (Relationship == "Child")
                    {
                      var  PersonResultList1 = from s in db.Lab_Report
                                               join x in db.Lab_SubCategory on s.LabTestID equals x.LabTestID
                                               join y in db.Lab_MainCategory on x.CategoryID equals y.CategoryID
                                               join z in db.Patient_Detail on s.PDID equals z.PDID
                                               join w in db.Patients on z.PID equals w.PID
                                               join n in db.RelationshipTypes on w.RelationshipType equals n.RTypeID
                                               where (s.TestSID == NewString)where(x.CategoryID == NewString1)where(s.Issued == "1") orderby x.SubCategoryID select new getreportdata { tubcat = tubcat, Testsid = testsid, Lab_Index = s.Lab_Index, LabTestID = s.LabTestID, ReferenceRange = x.ReferenceRange, ReferenceRangeUnit = x.ReferenceRangeUnit, SubCategoryName = x.SubCategoryName, testResult = s.testResult, teststatus = s.teststatus, CategoryName = y.CategoryName, ServiceNo = w.ServiceNo, Surname = w.Surname, Initials = "", RNK_NAME = w.rank1.RNK_NAME, Relationship =n.Relationship, RequestedTime = s.RequestedTime.ToString(), appvby = appvby1 };
                        if (PersonResultList1.Count() > 0)
                        {
                            //ser1 = ser1.Concat(PersonResultList1);
                            // ser.AsEnumerable().Concat(PersonResultList1);
                            df = PersonResultList1.ToList();
                        }
                        else
                        {
                            //df.Add(ser.First());
                            //  iserror = "3";
                        }

                    }
                   

                }
                if (df.Count() < 1)
                {

                        df = ser.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(df.AsQueryable(),  JsonRequestBehavior.AllowGet);
        }
        public JsonResult viewcamp()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            int userid = Convert.ToInt32(Session["UserID"]);
            var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID, s.Location.Description };
            String stndes = "";
            foreach (var item in opd)
            {
                stndes = item.Description;

            }
           
            return Json(opd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult acctest(string id, string catid)
        {
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);
            string NewString1 = catid.Trim(MyChar);
            string pdidx = "";
            int userid = 0;
            if (Session["UserID"] != null)
            {
                 userid = Convert.ToInt32(Session["UserID"]);
            }
            DataTable oDataSet37x = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " update a set a.IsApproved=1,ApprovedUser='"+ userid + "',ApprovedDate=GETDATE()  FROM [MMS].[dbo].[Lab_Report] a inner join [dbo].[Lab_SubCategory] b on a.LabTestID=b.LabTestID " +
"  inner join[dbo].Lab_MainCategory c on b.CategoryID = c.CategoryID "+
"  where a.TestSID = '"+ NewString + "' and a.issued = 1 and (a.IsApproved is null or a.IsApproved=0) and c.TubeCategory = '" + NewString1+"' ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
            //     oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet37x);
            //////////////////////////////////
            DataTable oDataSetsp2 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            if (oSqlConnection.State == ConnectionState.Closed)
            {
                oSqlConnection.Open();
            }
            oSqlCommand = new SqlCommand();
            sqlQuery = "    select top 1 pdid FROM [MMS].[dbo].[Lab_Report] where TestSID='" + NewString + "'   ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            //  oSqlConnection.Open();
            oSqlCommand.CommandTimeout = 120;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetsp2);
            foreach (DataRow row in oDataSetsp2.Rows)
            {
                pdidx = (String)row["pdid"];

            }


            ////////////////////////////////////

            DataTable oDataSet37y = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " update  [MMS].[dbo].[Patient_Detail] set status=2 where pdid='"+pdidx+"' and status=1 ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
            //     oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet37y);



            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Searchlabsno(string id, string id1)
        {
            
         
            Create(1, id,id1);


            return View("Create");
        }
        public JsonResult viewsick(string id)
        {
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);


            var ser = from s in db.Sick_Category.Where(p => p.CatIndex == NewString) join  b in db.Sick_CategoryType on s.CatID equals b.CatID select new { s.Patient_Detail.Patient.Initials, s.Patient_Detail.Patient.ServiceNo, s.Patient_Detail.Patient.Surname, b.Category_Type, s.CatPeriod };

            return Json(ser, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Savereport(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            foreach (var item in ser)
            {

                locid = item.LocationID;
            }
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }

           

            var objs = JsonConvert.DeserializeObject<List<labtestreader>>(id);
            int objcount = objs.Count;
            Lab_Report[] oLab_Reports = new Lab_Report[objcount];
            int i = 0;
            string labtid = "";
            foreach (labtestreader p in objs)
            {
               
                var olddetail = db.Lab_Report.Where(d => d.Lab_Index == p.Lab_Index1);
                //var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == PDID).Where(d => d.Issued == 0).ToList().Count;
                foreach (var item in olddetail)
                {
                    
                        Lab_Report oLab_Report = new Lab_Report();
                        oLab_Report.PDID = item.PDID;
                    oLab_Report.TestSID = item.TestSID;
                        oLab_Report.RequestedLocID = item.RequestedLocID;
                        oLab_Report.RequestedTime = item.RequestedTime;
                        oLab_Report.Lab_Index = p.Lab_Index1;
                    oLab_Report.teststatus = p.Labst1;
                        oLab_Report.LabTestID = item.LabTestID;
                        labtid= item.LabTestID;
                        oLab_Report.testResult = p.SubCategoryName1;
                        oLab_Report.Issued = "1";
                    oLab_Report.IssuedTime = DateTime.Now;
                        oLab_Reports[i] = oLab_Report;
                        i++;
                    
                }

            }
            try {
                oLab_Reports = oLab_Reports.Where(x => x != null).ToArray();

                foreach (var lbrpt in oLab_Reports) {
                    var actindb = db.Lab_Report.Find(lbrpt.Lab_Index);
                    db.Entry(actindb).CurrentValues.SetValues(lbrpt);
                    db.Entry(actindb).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
           
            return Json(ser, JsonRequestBehavior.AllowGet);

        }
        public class labtestreader
        {

           
            public string SubCategoryName1 { get; set; }
            public string Lab_Index1 { get; set; }
            public string Labst1 { get; set; }
        }
        // GET: Lab_Report/Create
        public ActionResult Create(int? page,string id, string id1)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            string id2 = "";
            int userid = Convert.ToInt32(Session["UserID"]);
          
            opdid = (String)Session["userlocid1"];

            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " select * from [dbo].[Clinic_Master] with (nolock) where Clinic_ID='" + opdid + "' ";
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.Connection = oSqlConnection;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var ser = oDataSet.AsEnumerable()
    .Select(dataRow => new Clinic_Master
    {
        LocationID = dataRow.Field<string>("LocationID")
       
    }).ToList();

            foreach (var item in ser)
            {
                locid = item.LocationID;

            }

          
            var onePageOfProducts=(dynamic)null;
            char[] MyChar = { '/', '"', ' ' };
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

            if (!String.IsNullOrEmpty(id2)&& !String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Parse(id2);

                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                // oSqlCommand = new SqlCommand();

               // oSqlCommand.Connection = oSqlConnection;
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnal";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", id);
                command.CommandTimeout = 20000;


                // oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);

                // oSqlConnection.Close();
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<DateTime?>("rtim").ToString(),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();




                // var  lablist = (from f in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year).Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id)) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime, tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory }).OrderByDescending(p => p.rtimed).ToList();
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year).Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id));
                //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();

                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d => d.rtimed);
                var pageNumber = page ?? 1;
                 onePageOfProducts = joined3.ToList();
            }
            else if (!String.IsNullOrEmpty(id2))
            {
                DateTime dt1 = DateTime.Parse(id2);
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                // oSqlCommand = new SqlCommand();

               // oSqlCommand.Connection = oSqlConnection;
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnal";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", "");
                command.CommandTimeout = 20000;


                // oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);

                // oSqlConnection.Close();
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();



                //var lablist = (from f in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime, tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory }).OrderByDescending(p => p.rtimed).ToList();
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year);
                //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();

                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d => d.rtimed);
                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToList();
            }
            else if ( !String.IsNullOrEmpty(id))
            {
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                // oSqlCommand = new SqlCommand();

               // oSqlCommand.Connection = oSqlConnection;
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnal";
               command.Parameters.AddWithValue("@RequestDate","");
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", id);
                command.CommandTimeout = 20000;


                // oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);

                // oSqlConnection.Close();
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();

                // var lablist = (from f in db.Lab_Report.Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id)) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime ,tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory }).OrderByDescending(p => p.rtimed).ToList();
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id));
                //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID,c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();

                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d => d.rtimed);

                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToList();
            }
            else
            {
                DateTime dt1 = DateTime.Now;
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
               // oSqlCommand = new SqlCommand();
                
               // oSqlCommand.Connection = oSqlConnection;
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnal";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", "");
                command.CommandTimeout = 20000;
                

               // oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);
               
                //   oSqlConnection.Open();
             
                // oSqlConnection.Close();
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();






                // var lablist = (from f in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime,tubecat=s.Lab_SubCategory.Lab_MainCategory.TubeCategory }).OrderByDescending(p => p.rtimed).ToList();
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year);
                //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();

                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d => d.rtimed);


                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToList();
            }
           
            

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
            
            //ViewBag.LabTestID = new SelectList(db.Lab_SubCategory, "LabTestID", "CategoryID");
            //ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID");
            //return View();
        }
        //public ActionResult Delete(int? page, string id, string id1, string currentFilter)
        //{
        //    db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
        //    string opdid = "";
        //    string locid = "";
        //    string id2 = "";
        //    int userid = Convert.ToInt32(Session["UserID"]);
        //    var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

        //    //foreach (var item in ser)
        //    //{

        //    //    locid = item.LocationID;
        //    //}
        //    opdid = (String)Session["userlocid1"];
        //    var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
        //    foreach (var item in clincd)
        //    {

        //        locid = item.LocationID;
        //    }
        //    var onePageOfProducts = (dynamic)null;
        //    char[] MyChar = { '/', '"', ' ' };
        //    if (!String.IsNullOrEmpty(id))
        //    {
        //        id = id.Trim(MyChar);
        //    }
        //    if (!String.IsNullOrEmpty(id1))
        //    {
        //        id1 = id1.Trim(MyChar);
        //    }

        //    if (id != null)
        //    {
        //        page = 1;
        //        ViewBag.currentFilter = id;
        //    }
        //    else
        //    {
        //        id = currentFilter;
        //        ViewBag.currentFilter = id;
        //    }

        //    if (!String.IsNullOrEmpty(id2) && !String.IsNullOrEmpty(id))
        //    {
        //        DateTime dt1 = DateTime.Parse(id2);
        //        var lablist = (from f in db.Lab_Report
        //                       join y in db.Patient_Detail on f.PDID equals y.PDID
        //                       join z in db.Patients on y.PID equals z.PID
        //                       where (f.RequestedLocID == locid)where(f.Issued == "0") where(f.RequestedTime.Value.Day == dt1.Day && f.RequestedTime.Value.Month == dt1.Month && f.RequestedTime.Value.Year == dt1.Year)where(z.ServiceNo.Contains(id)) select f).GroupBy(s => new { w.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).Select(s => new getlabdata { pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime }).OrderByDescending(p => p.rtimed).ToList();
        //        //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year).Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id));
        //        //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
        //        var pageNumber = page ?? 1;
        //        onePageOfProducts = lablist.ToPagedList(pageNumber, 10);
        //    }
        //    else if (!String.IsNullOrEmpty(id2))
        //    {
        //        DateTime dt1 = DateTime.Parse(id2);
        //        var lablist = (from f in db.Lab_Report
        //                       join x in db.Lab_SubCategory on f.LabTestID equals x.SubCategoryID.ToString()
        //                       join y in db.Lab_MainCategory on x.CategoryID equals y.CategoryID
        //                       join z in db.Patient_Detail on f.PDID equals z.PDID
        //                       join w in db.Patients on z.PID equals w.PID
        //                       join n in db.RelationshipTypes on w.RelationshipType equals n.RTypeID
        //       where(f.RequestedLocID == locid)where(f.Issued == "0")where(f.RequestedTime.Value.Day == dt1.Day && f.RequestedTime.Value.Month == dt1.Month && f.RequestedTime.Value.Year == dt1.Year) select f).GroupBy(s => new { s.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).Select(s => new getlabdata { pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime }).OrderByDescending(p => p.rtimed).ToList();
        //        //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year);
        //        //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
        //        var pageNumber = page ?? 1;
        //        onePageOfProducts = lablist.ToPagedList(pageNumber, 10);
        //    }
        //    else if (!String.IsNullOrEmpty(id))
        //    {

        //        var lablist = (from f in db.Lab_Report
        //                       join x in db.Lab_SubCategory on f.LabTestID equals x.SubCategoryID.ToString()
        //                       join y in db.Patient_Detail on f.PDID equals y.PDID
        //                       join z in db.Patients on y.PID equals z.PID
        //                       join w in db.Lab_MainCategory on x.CategoryID equals w.CategoryID
        //                       where (f.Issued == "0") where(z.ServiceNo.Contains(id)) select f,w).GroupBy(s => new { w.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).Select(s => new getlabdata { pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime }).OrderByDescending(p => p.rtimed).ToList();
        //        //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id));
        //        //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID,c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
        //        var pageNumber = page ?? 1;
        //        onePageOfProducts = lablist.ToPagedList(pageNumber, 10);
        //    }
        //    else
        //    {
        //        DateTime dt1 = DateTime.Now.Date;
        //        var lablist = (from f in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).Select(s => new getlabdata { pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime }).OrderByDescending(p => p.rtimed).ToList();
        //        //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "0").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year);
        //        //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
        //        var pageNumber = page ?? 1;
        //        onePageOfProducts = lablist.ToPagedList(pageNumber, 10);
        //    }



        //    ViewBag.OnePageOfProducts = onePageOfProducts;
        //    return View();

        //    //ViewBag.LabTestID = new SelectList(db.Lab_SubCategory, "LabTestID", "CategoryID");
        //    //ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID");
        //    //return View();
        //}
        public ActionResult labsms(int? page, string id)
        {
           
            return View();

           
        }
        public ActionResult ViewReport(int? page, string id, string id1)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            string id2 = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //    locid = item.LocationID;
            //}

            opdid = (String)Session["userlocid1"];
           
            var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
            foreach (var item in clincd) 
            {

                locid = item.LocationID;
            }

            var onePageOfProducts = (dynamic)null;
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id1))
            {
                id1 = id1.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty( id1))
            {
                Session["dtvalu"] = id1;
            }
            if (Session["dtvalu"]!=null)
            {
                if (page != null)
                {
                    id1 = Session["dtvalu"].ToString();
                }
                else if (String.IsNullOrEmpty(id1))
                {
                    Session["dtvalu"] = null;
                }
            }

            if (!String.IsNullOrEmpty(id))
            {
                Session["snvalu"] = id;
            }
            if (Session["snvalu"] != null)
            {
              if( page!=null)
                {
                    id = Session["snvalu"].ToString();
                }
                else if(String.IsNullOrEmpty(id))
                {
                    Session["snvalu"] = null;
                }
                  
                
            }


            if (!String.IsNullOrEmpty(id1) && !String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Parse(id1);

                //                DataTable oDataSetv6 = new DataTable();
                //                oSqlConnection = new SqlConnection(conStr);
                //                oSqlCommand = new SqlCommand();
                //                sqlQuery = " SELECT e.TubeCategory,a.TestSID,e.CategoryID,a.pdid,e.CategoryName,max(a.Issued) Issued,CAST(a.[RequestedTime] " +
                //" as DATE) as RequestedTime, COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1 and b.Surname != '0' " +
                //" then b.Surname end), max(case when c.RelationshipType = 2 then k.SpouseName end), " +
                //" max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB then f.ChildName end), " +
                //" max(case when c.RelationshipType = 3 and g.Relationship = 'Father' then g.ParentName end), " +
                //" max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName end)), ''), max(c.surname)) " +
                //" sname ,max(case when c.RelationshipType = 1 then b.Rank end) rnkname, max(c.ServiceNo) sno " +
                //" ,max(case when c.RelationshipType = 1 then b.Initials end) inililes, max(c.RelationshipType) relasiont, max(h.Relationship) " +
                //                    " relasiondet FROM[MMS].[dbo] .[Lab_Report] as a with(nolock) " +
                //" inner join[MMS].[dbo].[Lab_SubCategory] as d on d.[LabTestID]=a.[LabTestID] inner join[MMS].[dbo]. " +
                //" [Lab_MainCategory] as e on e.CategoryID=d.CategoryID " +
                //" inner join[MMS].[dbo].[Patient_Detail] as l on l.pdid=a.PDID inner join[MMS].[dbo].[Patient] as c on l.pid=c.pid " +
                //" left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
                //" left join[MMS].[dbo].[SpouseDetails] as k on b.SNo=k.SNo " +
                //" left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                //" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                //" where c.serviceno='" + id + "' and a.issued='1' and convert(date, a.RequestedTime)=convert(varchar,'" + dt1.Date.ToString() + "',111) group by " +
                //" e.CategoryName, a.TestSID, CAST(a.[RequestedTime] as DATE),e.CategoryID,e.TubeCategory,a.pdid order by e.TubeCategory, a.TestSID desc";
                //                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                //                oSqlCommand.Connection = oSqlConnection;
                //                oSqlCommand.CommandText = sqlQuery;
                //                oSqlCommand.CommandTimeout = 120;
                //                //   oSqlConnection.Open();
                //                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                //                oSqlDataAdapter.Fill(oDataSetv6);
                //                // oSqlConnection.Close();
                //                var lablist = oDataSetv6.AsEnumerable()
                //        .Select(dataRow => new getlabdata
                //        {

                //            tsid = dataRow.Field<string>("TestSID"),
                //            catid = dataRow.Field<string>("CategoryID"),
                //            pdids = dataRow.Field<string>("pdid"),
                //            catname = dataRow.Field<string>("CategoryName"),
                //            rtime = dataRow.Field<DateTime?>("RequestedTime").ToString(),
                //            rtimed = dataRow.Field<DateTime?>("RequestedTime"),
                //            tubecat = dataRow.Field<int?>("TubeCategory"),
                //            sname = dataRow.Field<string>("sname"),
                //            rnkname = dataRow.Field<string>("rnkname"),
                //            sno = dataRow.Field<string>("sno"),
                //            inililes = dataRow.Field<string>("inililes"),

                //            relasiont = dataRow.Field<string>("relasiondet"),
                //        }).ToList();

                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                // oSqlCommand = new SqlCommand();

                // oSqlCommand.Connection = oSqlConnection;
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallab";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", id);
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;


                // oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);

                //   oSqlConnection.Open();

                // oSqlConnection.Close();
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();

                // var lablist = (from f in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year).Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id)) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory, pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime }).OrderByDescending(p => p.rtimed).ToList();
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year).Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id));
                //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID,c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();

                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid)  && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d => d.rtimed);



                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToList();
            }
            else if (!String.IsNullOrEmpty(id1))
            {

                               DateTime dt1 = DateTime.Parse(id1);
                //                DataTable oDataSetv6 = new DataTable();
                //                oSqlConnection = new SqlConnection(conStr);
                //                oSqlCommand = new SqlCommand();
                //                sqlQuery = " SELECT e.TubeCategory,a.TestSID,e.CategoryID,a.pdid,e.CategoryName,max(a.Issued) Issued,CAST(a.[RequestedTime] " +
                //" as DATE) as RequestedTime, COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1 and b.Surname != '0' " +
                //" then b.Surname end), max(case when c.RelationshipType = 2 then k.SpouseName end), " +
                //" max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB then f.ChildName end), " +
                //" max(case when c.RelationshipType = 3 and g.Relationship = 'Father' then g.ParentName end), " +
                //" max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName end)), ''), max(c.surname)) " +
                //" sname ,max(case when c.RelationshipType = 1 then b.Rank end) rnkname, max(c.ServiceNo) sno " +
                //" ,max(case when c.RelationshipType = 1 then b.Initials end) inililes, max(c.RelationshipType) relasiont, max(h.Relationship) " +
                //                    " relasiondet FROM[MMS].[dbo] .[Lab_Report] as a with(nolock) " +
                //" inner join[MMS].[dbo].[Lab_SubCategory] as d on d.[LabTestID]=a.[LabTestID] inner join[MMS].[dbo]. " +
                //" [Lab_MainCategory] as e on e.CategoryID=d.CategoryID " +
                //" inner join[MMS].[dbo].[Patient_Detail] as l on l.pdid=a.PDID inner join[MMS].[dbo].[Patient] as c on l.pid=c.pid " +
                //" left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
                //" left join[MMS].[dbo].[SpouseDetails] as k on b.SNo=k.SNo " +
                //" left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                //" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                //" where a.issued='1' and convert(date, a.RequestedTime)=convert(varchar,'" + dt1.Date.ToString() + "',111) group by " +
                //" e.CategoryName, a.TestSID, CAST(a.[RequestedTime] as DATE),e.CategoryID,e.TubeCategory,a.pdid order by e.TubeCategory, a.TestSID desc";
                //                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                //                oSqlCommand.Connection = oSqlConnection;
                //                oSqlCommand.CommandText = sqlQuery;
                //                oSqlCommand.CommandTimeout = 120;
                //                //   oSqlConnection.Open();
                //                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                //                oSqlDataAdapter.Fill(oDataSetv6);
                //                // oSqlConnection.Close();
                //                var lablist = oDataSetv6.AsEnumerable()
                //        .Select(dataRow => new getlabdata
                //        {

                //            tsid = dataRow.Field<string>("TestSID"),
                //            catid = dataRow.Field<string>("CategoryID"),
                //            pdids = dataRow.Field<string>("pdid"),
                //            catname = dataRow.Field<string>("CategoryName"),
                //            rtime = dataRow.Field<DateTime?>("RequestedTime").ToString(),
                //            rtimed = dataRow.Field<DateTime?>("RequestedTime"),
                //            tubecat = dataRow.Field<int?>("TubeCategory"),
                //            sname = dataRow.Field<string>("sname"),
                //            rnkname = dataRow.Field<string>("rnkname"),
                //            sno = dataRow.Field<string>("sno"),
                //            inililes = dataRow.Field<string>("inililes"),

                //            relasiont = dataRow.Field<string>("relasiondet"),
                //        }).ToList();

                //DateTime dt1 = DateTime.Now;
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                // oSqlCommand = new SqlCommand();

                // oSqlCommand.Connection = oSqlConnection;
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallab";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString() );
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", "");
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;


                // oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);

                //   oSqlConnection.Open();

                // oSqlConnection.Close();
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();

                // var lablist = (from f in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory, pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime }).OrderByDescending(p => p.rtimed).ToList();
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year);
                //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();

                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid)  && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d => d.rtimed);

                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToList();
            }
            else if (!String.IsNullOrEmpty(id))
            {

                // var lablist = (from f in db.Lab_Report.Where(p => p.Issued == "1").Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id)) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory, pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString() ,rtimed= s.RequestedTime}).OrderByDescending(p => p.rtimed).ToList();
                //                DataTable oDataSetv6 = new DataTable();
                //                oSqlConnection = new SqlConnection(conStr);
                //                oSqlCommand = new SqlCommand();
                //                sqlQuery = " SELECT e.TubeCategory,a.TestSID,e.CategoryID,a.pdid,e.CategoryName,max(a.Issued) Issued,CAST(a.[RequestedTime] " +
                //" as DATE) as RequestedTime, COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1 and b.Surname != '0' " +
                //" then b.Surname end), max(case when c.RelationshipType = 2 then k.SpouseName end), " +
                //" max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB then f.ChildName end), " +
                //" max(case when c.RelationshipType = 3 and g.Relationship = 'Father' then g.ParentName end), " +
                //" max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName end)), ''), max(c.surname)) " +
                //" sname ,max(case when c.RelationshipType = 1 then b.Rank end) rnkname, max(c.ServiceNo) sno " +
                //" ,max(case when c.RelationshipType = 1 then b.Initials end) inililes, max(c.RelationshipType) relasiont, max(h.Relationship) " +
                //                    " relasiondet FROM[MMS].[dbo] .[Lab_Report] as a with(nolock) " +
                //" inner join[MMS].[dbo].[Lab_SubCategory] as d on d.[LabTestID]=a.[LabTestID] inner join[MMS].[dbo]. " +
                //" [Lab_MainCategory] as e on e.CategoryID=d.CategoryID " +
                //" inner join[MMS].[dbo].[Patient_Detail] as l on l.pdid=a.PDID inner join[MMS].[dbo].[Patient] as c on l.pid=c.pid " +
                //" left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
                //" left join[MMS].[dbo].[SpouseDetails] as k on b.SNo=k.SNo " +
                //" left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
                //" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
                //" where  a.issued='1' and c.serviceno='"+id+"' group by " +
                //" e.CategoryName, a.TestSID, CAST(a.[RequestedTime] as DATE),e.CategoryID,e.TubeCategory,a.pdid order by e.TubeCategory, a.TestSID desc";
                //                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                //                oSqlCommand.Connection = oSqlConnection;
                //                oSqlCommand.CommandText = sqlQuery;
                //                oSqlCommand.CommandTimeout = 120;
                //                //   oSqlConnection.Open();
                //                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                //                oSqlDataAdapter.Fill(oDataSetv6);
                //                // oSqlConnection.Close();
                //                var lablist = oDataSetv6.AsEnumerable()
                //        .Select(dataRow => new getlabdata
                //        {

                //            tsid = dataRow.Field<string>("TestSID"),
                //            catid = dataRow.Field<string>("CategoryID"),
                //            pdids = dataRow.Field<string>("pdid"),
                //            catname = dataRow.Field<string>("CategoryName"),
                //            rtime = dataRow.Field<DateTime?>("RequestedTime").ToString(),
                //            rtimed = dataRow.Field<DateTime?>("RequestedTime"),
                //            tubecat = dataRow.Field<int?>("TubeCategory"),
                //            sname = dataRow.Field<string>("sname"),
                //            rnkname = dataRow.Field<string>("rnkname"),
                //            sno = dataRow.Field<string>("sno"),
                //            inililes = dataRow.Field<string>("inililes"),

                //            relasiont = dataRow.Field<string>("relasiondet"),
                //        }).ToList();

                DateTime dt1 = DateTime.Now;
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                // oSqlCommand = new SqlCommand();

                // oSqlCommand.Connection = oSqlConnection;
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallab";
                command.Parameters.AddWithValue("@RequestDate","");
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", id);
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;


                // oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);

                //   oSqlConnection.Open();

                // oSqlConnection.Close();
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();

                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.Issued == "1").Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id));
                // IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();
               
                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid)  && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d => d.rtimed);




                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToList();
            }
            else
            {
               
                DateTime dt1 = DateTime.Now;
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallab";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", "");
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();



                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();
                List<getlabdata> temp3 = new List<getlabdata>();
                string lnm ="";
                int cvb = 0;
                foreach (var item in lablist)
                {
                    
                    if (i1.Equals(item.tsid)  && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm+"/"+item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);
                        
                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;
                    
                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d=>d.rtimed);


                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year);
                // IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToList();
            }



            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();


           

            //ViewBag.LabTestID = new SelectList(db.Lab_SubCategory, "LabTestID", "CategoryID");
            //ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID");
            //return View();
        }

        public ActionResult AcceptReport(int? page, string id, string id1)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            string id2 = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //    locid = item.LocationID;
            //}

            opdid = (String)Session["userlocid1"];

            var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
            foreach (var item in clincd)
            {

                locid = item.LocationID;
            }

            var onePageOfProducts = (dynamic)null;
            char[] MyChar = { '/', '"', ' ' };
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
                if (page != null)
                {
                    id1 = Session["dtvalu"].ToString();
                }
                else if (String.IsNullOrEmpty(id1))
                {
                    Session["dtvalu"] = null;
                }
            }

            if (!String.IsNullOrEmpty(id))
            {
                Session["snvalu"] = id;
            }
            if (Session["snvalu"] != null)
            {
                if (page != null)
                {
                    id = Session["snvalu"].ToString();
                }
                else if (String.IsNullOrEmpty(id))
                {
                    Session["snvalu"] = null;
                }


            }


            if (!String.IsNullOrEmpty(id1) && !String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Parse(id1);

              

                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                // oSqlCommand = new SqlCommand();

                // oSqlCommand.Connection = oSqlConnection;
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallabP";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", id);
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;


                // oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);

                //   oSqlConnection.Open();

                // oSqlConnection.Close();
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();

                // var lablist = (from f in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year).Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id)) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory, pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime }).OrderByDescending(p => p.rtimed).ToList();
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year).Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id));
                //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID,c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();

                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d => d.rtimed);



                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToPagedList(pageNumber, 10);
            }
            else if (!String.IsNullOrEmpty(id1))
            {

                DateTime dt1 = DateTime.Parse(id1);
                
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                // oSqlCommand = new SqlCommand();

                // oSqlCommand.Connection = oSqlConnection;
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallabP";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", "");
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;


                // oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);

                //   oSqlConnection.Open();

                // oSqlConnection.Close();
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();

                // var lablist = (from f in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory, pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime }).OrderByDescending(p => p.rtimed).ToList();
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year);
                //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();

                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d => d.rtimed);

                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToPagedList(pageNumber, 10);
            }
            else if (!String.IsNullOrEmpty(id))
            {

               

                DateTime dt1 = DateTime.Now;
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                // oSqlCommand = new SqlCommand();

                // oSqlCommand.Connection = oSqlConnection;
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallabP";
                command.Parameters.AddWithValue("@RequestDate", "");
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", id);
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;


                // oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);

                //   oSqlConnection.Open();

                // oSqlConnection.Close();
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();

                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.Issued == "1").Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id));
                // IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();

                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d => d.rtimed);




                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToPagedList(pageNumber, 10);
            }
            else
            {

                DateTime dt1 = DateTime.Now;
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallabP";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", "");
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();



                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();
                List<getlabdata> temp3 = new List<getlabdata>();
                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2).OrderByDescending(d => d.rtimed);


                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year);
                // IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToPagedList(pageNumber, 10);
            }



            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();




            //ViewBag.LabTestID = new SelectList(db.Lab_SubCategory, "LabTestID", "CategoryID");
            //ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID");
            //return View();
        }


        public ActionResult ViewReportbyid(int? page, string id, string id1)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            string id2 = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };
            string ServiceNo = "";
            //foreach (var item in ser)
            //{

            //    locid = item.LocationID;
            //}

            opdid = (String)Session["userlocid1"];
            Session["svcrpt"] ="'"+ id.ToString()+"'";
            var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
            foreach (var item in clincd)
            {

                locid = item.LocationID;
            }

            var onePageOfProducts = (dynamic)null;
            char[] MyChar = { '/', '"', ' ' };
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
                if (page != null)
                {
                    id1 = Session["dtvalu"].ToString();
                }
                else if (String.IsNullOrEmpty(id1))
                {
                    Session["dtvalu"] = null;
                }
            }

            if (!String.IsNullOrEmpty(id))
            {
                Session["snvalu"] = id;
            }
            if (Session["snvalu"] != null)
            {
                if (page != null)
                {
                    id = Session["snvalu"].ToString();
                }
                else if (String.IsNullOrEmpty(id))
                {
                    Session["snvalu"] = null;
                }


            }


            if (!String.IsNullOrEmpty(id1) && !String.IsNullOrEmpty(id))
            {
                DateTime dt1 = DateTime.Parse(id1);
               // DateTime dt1 = DateTime.Parse(id1);
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallab";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", "");
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();





                // var lablist = (from f in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year).Where(p => y.ServiceNo.Contains(id)) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory, pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime  }).OrderByDescending(p => p.rtimed).ToList();
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year).Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id));
                //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID,c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();
                List<getlabdata> temp3 = new List<getlabdata>();
                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2);



                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToPagedList(pageNumber, 10);
            }
            else if (!String.IsNullOrEmpty(id1))
            {



                DateTime dt1 = DateTime.Parse(id1);
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallab";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", "");
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();





                //var lablist = (from f in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory, pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime }).OrderByDescending(p => p.rtimed).ToList();
               
                
                
                
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year);
                //IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();
                List<getlabdata> temp3 = new List<getlabdata>();
                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2);
                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToPagedList(pageNumber, 10);
            }
            else if (!String.IsNullOrEmpty(id))
            {
                DataTable oDataSet1 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select ServiceNo from [dbo].[Patient] with (nolock) where pid='" + id + "'";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                //oDataSet = null;
                oSqlDataAdapter.Fill(oDataSet1);
                oSqlConnection.Close();
                var opd = oDataSet1.AsEnumerable()
        .Select(dataRow => new Patient
        {
            
            ServiceNo = dataRow.Field<string>("ServiceNo")

        }).ToList();



                foreach (var item in opd)
                {
                    ServiceNo = item.ServiceNo;

                }



                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallab";
                command.Parameters.AddWithValue("@RequestDate", "");
                command.Parameters.AddWithValue("@location", "");
                command.Parameters.AddWithValue("@ServiceNo", ServiceNo);
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();


                // var lablist = (from f in db.Lab_Report
                //                join x in db.Lab_SubCategory on f.LabTestID equals x.SubCategoryID.ToString()
                //                join y in db.Lab_MainCategory on x.CategoryID equals y.CategoryID
                //                join z in db.Patient_Detail on f.PDID equals z.PDID
                //                join w in db.Patients on z.PID equals w.PID
                //                join n in db.RelationshipTypes on w.RelationshipType equals n.RTypeID
                //where(f.Issued == "1")where(w.PID==id) select f).GroupBy(s => new { y.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory, pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime }).OrderByDescending(p => p.rtimed).ToList();
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.Issued == "1").Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id));
                // IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();
                List<getlabdata> temp3 = new List<getlabdata>();
                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2);
                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToPagedList(pageNumber, 10);
            }
            else
            {
                DateTime dt1 = DateTime.Now.Date;
                DataTable oDataSetv6 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                SqlCommand command = oSqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getpersonnallab";
                command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
                command.Parameters.AddWithValue("@location", locid);
                command.Parameters.AddWithValue("@ServiceNo", "");
                command.Parameters.AddWithValue("@ismai", "");
                command.CommandTimeout = 20000;
                oSqlDataAdapter = new SqlDataAdapter(command);
                oSqlDataAdapter.Fill(oDataSetv6);
                var lablist = oDataSetv6.AsEnumerable()
        .Select(dataRow => new getlabdata
        {

            tsid = dataRow.Field<string>("TestSID"),
            catid = dataRow.Field<int?>("CategoryID").ToString(),
            pdids = dataRow.Field<string>("pdid"),
            catname = dataRow.Field<string>("CategoryName"),
            rtime = dataRow.Field<string>("rtim"),
            rtimed = dataRow.Field<DateTime?>("rtimed"),
            tubecat = dataRow.Field<int?>("TubeCategory"),
            sname = dataRow.Field<string>("sname"),
            rnkname = dataRow.Field<string>("rnkname"),
            sno = dataRow.Field<string>("sno"),
            inililes = dataRow.Field<string>("inililes"),

            relasiont = dataRow.Field<string>("relasiondet"),
        }).ToList();


                //  var lablist = (from f in db.Lab_Report.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year) select f).GroupBy(s => new { s.Lab_SubCategory.Lab_MainCategory.CategoryName, s.PDID, s.TestSID }).Select(g => g.FirstOrDefault()).OrderBy(g => g.TestSID).Select(s => new getlabdata { tubecat = s.Lab_SubCategory.Lab_MainCategory.TubeCategory, pdids = s.PDID, inililes = s.Patient_Detail.Patient.Initials, sname = s.Patient_Detail.Patient.Surname, sno = s.Patient_Detail.Patient.ServiceNo, rnkname = s.Patient_Detail.Patient.rank1.RNK_NAME, catname = s.Lab_SubCategory.Lab_MainCategory.CategoryName, tsid = s.TestSID, catid = s.Lab_SubCategory.Lab_MainCategory.CategoryID, relasiont = s.Patient_Detail.Patient.RelationshipType1.Relationship, rtime = s.RequestedTime.ToString(), rtimed = s.RequestedTime }).OrderByDescending(p => p.rtimed).ToList();
                //db.Lab_Report.Include(l => l.Patient_Detail).Where(p => p.RequestedLocID == locid).Where(p => p.Issued == "1").Where(p => p.RequestedTime.Value.Day == dt1.Day && p.RequestedTime.Value.Month == dt1.Month && p.RequestedTime.Value.Year == dt1.Year);
                // IEnumerable<Lab_Report> filist = lablist.GroupBy(c => new { c.Lab_SubCategory.Lab_MainCategory.CategoryName, c.Lab_SubCategory.Lab_MainCategory.CategoryID, c.PDID, c.TestSID }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.RequestedTime);
                var i1 = ""; var i2 = ""; int? i3 = 0;
                List<getlabdata> temp = new List<getlabdata>();
                List<getlabdata> temp2 = new List<getlabdata>();
                List<getlabdata> temp3 = new List<getlabdata>();
                string lnm = "";
                int cvb = 0;
                foreach (var item in lablist)
                {

                    if (i1.Equals(item.tsid) && i3.Equals(item.tubecat))
                    {
                        if (temp.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp.RemoveAt(temp.Count - 1);
                        }
                        else if (temp2.Any(x => x.tsid == item.tsid && x.tubecat == item.tubecat))
                        {
                            temp2.RemoveAt(temp2.Count - 1);
                        }
                        lnm = lnm + "/" + item.catname;
                        item.catname = lnm;
                        temp2.Add(item);
                    }
                    else
                    {
                        lnm = "";
                        cvb++;
                        temp.Add(item);

                        lnm = lnm + "/" + item.catname;
                    }
                    i1 = item.tsid;
                    i2 = item.sno;
                    i3 = item.tubecat;

                }

                var joined3 = temp.Concat(temp2);

                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToPagedList(pageNumber, 10);
            }



            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();




            //ViewBag.LabTestID = new SelectList(db.Lab_SubCategory, "LabTestID", "CategoryID");
            //ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID");
            //return View();
        }

        public JsonResult LineChartData(string id, string id2)
        {
            String[] mnt = id.Split('-');
            string locid = "";
            id = id.Replace("'","");
            id = id.Replace(@"""", "");
            //var serW = from s in db.Clinic_Master.Where(p => p.LocationID == id1).Where(p => p.ClinicTypeID == 19) select new { s.Clinic_ID };

            //foreach (var item in serW)
            //{

            //    locid = item.Clinic_ID;
            //}

            //    int days = DateTime.DaysInMonth(Convert.ToInt32(mnt[0]), Convert.ToInt32(mnt[1]));
            //  string[] darr = new string[] { "AHP", "AMP", "BCL", "BIA", "CBO", "CBY", "DLA", "EKA", "HIN", "IRM", "KAT", "KGL", "KTK", "MGR", "MOW", "MTV", "PGL", "PLV", "KKS", "RMA", "SGR", "VNA", "WAN", "WLA" };
            // string[] cdarr = new string[] { "AHP", "AMP", "BCL", "BIA", "CBO", "CBY", "DLA", "EKA", "HIN", "IRM", "KAT", "KGL", "KTK", "MGR", "MOW", "MTV", "PGL", "PLV", "PLY", "RMA", "SGR", "VNA", "VNI", "WLA" };
            string[] datlt = new string[1000];
            List<decimal> cntlt = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT  distinct convert(date, a.RequestedTime) as t ,MAX(a.testResult) as r  "+
 " FROM[MMS].[dbo].[Lab_Report] as a inner join[MMS].[dbo].[Lab_SubCategory] as b on a.LabTestID=b.LabTestID inner join[MMS].[dbo].[Patient_Detail] as c on a.PDID=c.PDID inner join[MMS].[dbo].[Patient] as d on c.PID=d.PID where "+
" d.pid='"+id+"' and a.Issued=1 and (b.SubCategoryName like '%"+id2+"%' )group by   convert(date, a.RequestedTime) order by convert(date, a.RequestedTime) ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet);
                oSqlConnection.Close();

                if (oDataSet.Tables[0].Rows.Count > 0)
                {
                    int cnt = 0;
                   
                        foreach (DataRow row in oDataSet.Tables[0].Rows)
                        {
                            //string dval = row["t"].ToString();

                           // string sval = row1;
                            //darr[cnt].ToString();



                          
                                cntlt.Add(Convert.ToDecimal(row["r"].ToString()));
                                datlt[cnt]=(row["t"].ToString());
                                cnt++;
                              
                           

                        }

                       

                        //string dval = row["t"].ToString();

                        //string sval = darr[cnt].ToString();


                        //do
                        //{

                        //    sval = darr[cnt].ToString();
                        //    if (dval == sval)
                        //    {
                        //        cntlt.Add(Convert.ToInt32(row["r"].ToString()));
                        //    }
                        //    else
                        //    {
                        //        cntlt.Add(0);

                        //    }

                        //    cnt++;
                        //}
                        //while (sval != dval);
                    }
               

            }
            catch (Exception ex)
            {

            }

            datlt = datlt.Where(x => x != null).ToArray();

            decimal[] carr = cntlt.ToArray();
            Chart _chart = new Chart();
            _chart.labels = datlt;
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                label =id2,
                data = carr,
                borderColor = new string[] { "rgba(255,0,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0)" },
                borderWidth = "3"
            });
           
            /////////////////////////////////////////////////
            _chart.datasets = null;
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);




            //          List<string> datlt = new List<string>();
            //          List<int> cntlt = new List<int>();
            //          try
            //          {
            //              string sqlQuery;
            //              DataSet oDataSet = new DataSet();
            //              oSqlConnection = new SqlConnection("Data Source =135.22.210.105; Initial Catalog = MMS; User ID = mmsuser; Password = password");
            //              oSqlCommand = new SqlCommand();
            //              sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r "+
            //"FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd1'  group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
            //              // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            //              oSqlCommand.Connection = oSqlConnection;
            //              oSqlCommand.CommandText = sqlQuery;
            //              oSqlConnection.Open();
            //              oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            //              oSqlDataAdapter.Fill(oDataSet);
            //              oSqlConnection.Close();

            //              if (oDataSet.Tables[0].Rows.Count > 0)
            //              {
            //                  int cnt = 0;
            //                  foreach (DataRow row in oDataSet.Tables[0].Rows)
            //                  {

            //                      datlt.Add(row["t"].ToString());
            //                      cntlt.Add( Convert.ToInt32(row["r"].ToString()));

            //                      cnt++;
            //                  }
            //              }
            //          }
            //          catch (Exception ex)
            //          {

            //          }

            //          string[] darr = datlt.ToArray();
            //          int[] carr = cntlt.ToArray();
            //          Chart _chart = new Chart();
            //          _chart.labels = darr;
            //              //new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "Novemeber", "December" };
            //          _chart.datasets = new List<Datasets>();
            //          List<Datasets> _dataSet = new List<Datasets>();
            //          _dataSet.Add(new Datasets()
            //          {
            //              label = "Current Year",
            //              data = carr,
            //              //new int[] { 28, 48, 40, 19, 86, 27, 90, 20, 45, 65, 34, 22 },
            //              borderColor = new string[] { "#800080" },
            //              borderWidth = "1"
            //          });

        }


        // POST: Lab_Report/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lab_Index,PDID,LabTestID,Result,RequestedLocID,Issued,IssuedTime,RequestedTime,IssuedLocID,LabImg")] Lab_Report lab_Report)
        {
            if (ModelState.IsValid)
            {
                db.Lab_Report.Add(lab_Report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LabTestID = new SelectList(db.Lab_SubCategory, "LabTestID", "CategoryID", lab_Report.LabTestID);
            ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID", lab_Report.PDID);
            return View(lab_Report);
        }

        // GET: Lab_Report/Edit/5
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
            int userid = Convert.ToInt32(Session["UserID"]);
            var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            foreach (var item in ser)
            {

                locid = item.LocationID;
            }
            var opd = from s in db.Staff_Master.Where(p => p.UserID == userid) select new { s.LOCID };
            opdid = (String)Session["userlocid1"];
            foreach (var item in opd)
            {

                // opdid = item.LOCID;
            }

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
                // var patient_Detail = from s in db.Patient_Detail.Where(p => p.Patient.ServiceNo.Contains(id)).OrderByDescending(p => p.CreatedDate) select new getpatietdata { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, hyscompllain = s.History_OtherComplain, opddiag = s.OPD_Diagnosis, relasiont = s.Patient.RelationshipType1.Relationship };
                DataTable oDataSet4 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();

                sqlQuery = "   SELECT  (a.Present_Complain)pcomoplian,b.ServiceStatus, COALESCE(NULLIF((b.Surname ), ''), c.Initials+' '+c.Surname)  	sname  " +
"  ,(b.Rank)rnkname, " +
"  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"   , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate, (h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
"  as b on c.ServiceNo=b.ServiceNo and c.Service_Type= b.ServiceType left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where   a.PatientCatID!=5 and c.ServiceNo= '" + id + "'   " +

"    order by crdate";
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
            stype = dataRow.Field<int>("Service_Type"),
        }).ToList();
                //db.Patient_Detail.Include(p => p.Patient).Include(p => p.Status1).Where(p => p.Patient.ServiceNo.Contains(id)).OrderByDescending(p => p.CreatedDate);
                var pageNumber = page ?? 1;
                onePageOfProducts = lid.ToArray().ToPagedList(pageNumber, 10);
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
                    // var patient_Detail = from s in db.Patient_Detail.Where(p => title.Contains(p.Patient.ServiceNo)).Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).OrderByDescending(p => p.CreatedDate) select new getpatietdata { pdids = s.PDID, inililes = s.Patient.Initials, sname = s.Patient.Surname, sno = s.Patient.ServiceNo, rnkname = s.Patient.rank1.RNK_NAME, pcomoplian = s.Present_Complain, hyscompllain = s.History_OtherComplain, opddiag = s.OPD_Diagnosis, relasiont = s.Patient.RelationshipType1.Relationship };
                    DataTable oDataSet4 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();

                    sqlQuery = "   SELECT  (a.Present_Complain)pcomoplian,b.ServiceStatus, COALESCE(NULLIF((b.Surname ), ''), c.Initials+' '+c.Surname)  	sname  " +
 "  ,(b.Rank)rnkname, " +
 "  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"   , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate, (h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
 "  as b on c.ServiceNo=b.ServiceNo and c.Service_Type= b.ServiceType left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where   a.PatientCatID!=5 and c.ServiceNo= '" + id + "'   " +

 "    order by crdate";
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
                stype = dataRow.Field<int>("Service_Type"),
            }).ToList();

                    // db.Patient_Detail.Include(p => p.Patient).OrderByDescending(p => p.CreatedDate);
                    var pageNumber = page ?? 1;
                    onePageOfProducts = lid.ToArray().ToPagedList(pageNumber, 10);

                }
                else
                {
                    DateTime dd = DateTime.Now.Date;

                    //db.Patient_Detail.Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate);
                    // var patient_Detail = from s in db.Patient_Detail.Where(p => p.Status == 1 || p.Status == 7).Where(p => p.OPDID == opdid).Where(p => p.CreatedDate.Day == dd.Day && p.CreatedDate.Month == dd.Month && p.CreatedDate.Year == dd.Year).OrderByDescending(p => p.CreatedDate) select new getpatietdata { pdids= s.PDID,inililes= s.Patient.Initials,sname= s.Patient.Surname,sno= s.Patient.ServiceNo,rnkname= s.Patient.rank1.RNK_NAME,pcomoplian= s.Present_Complain,hyscompllain=  s.History_OtherComplain,opddiag= s.OPD_Diagnosis,relasiont= s.Patient.RelationshipType1.Relationship };


                   // DateTime dd = DateTime.Now.Date;

                    DataTable oDataSet3 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "       SELECT  (a.Present_Complain)pcomoplian,b.ServiceStatus, COALESCE(NULLIF((b.Surname ), ''), c.Initials+' '+c.Surname )  	sname  " +
"  ,(b.Rank)rnkname, " +
"  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"  , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate, (h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
"   as b on c.ServiceNo=b.ServiceNo and c.Service_Type= b.ServiceType left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
" where convert(date, a.[CreatedDate]) =CONVERT(varchar,'" + dd.ToShortDateString() + "',111)  and (a.status= 2 or a.status= 5) and a.PatientCatID!=2and a.PatientCatID!=5  " +
" and a.opdid='" + opdid + "'  order by crdate ";
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
                stype = dataRow.Field<int>("Service_Type"),
            }).ToList();


                    var pageNumber = page ?? 1;
                    onePageOfProducts = lid.ToArray().ToPagedList(pageNumber, 10);
                }
            }
           
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }


      

        // POST: Lab_Report/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lab_Index,PDID,LabTestID,Result,RequestedLocID,Issued,IssuedTime,RequestedTime,IssuedLocID,LabImg")] Lab_Report lab_Report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lab_Report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LabTestID = new SelectList(db.Lab_SubCategory, "LabTestID", "CategoryID", lab_Report.LabTestID);
            ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID", lab_Report.PDID);
            return View(lab_Report);
        }

        // GET: Lab_Report/Delete/5
       

        // POST: Lab_Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Lab_Report lab_Report = db.Lab_Report.Find(id);
            db.Lab_Report.Remove(lab_Report);
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
