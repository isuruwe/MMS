using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using PagedList;
using System.IO;
using System.Globalization;
using SMSapplication;
using System.IO.Ports;
using MMS.Models;
using System.Data.SqlClient;

namespace MMS.Controllers
{
    public class Lab_smsController : Controller
    {
        private MMSEntities db = new MMSEntities();
        private string err;
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        // GET: Lab_sms
        public ActionResult smssend()
        {
            string sFileName = System.IO.Path.GetRandomFileName();
            string sGenName = DateTime.Now.ToString("yyyyMMdd_HHmmss").Trim() + ".txt";

            DateTime dt1 = DateTime.Now.Date;
            string labindex = "";
            string msg = "";
            string tp = "";
            string pdids = "";
            string inililes = "";
            string sname = "";
            string sno = "";
            string rnkname = "";
            string catname = "";
            string tsid = "";
            string catid = "";
            string relasiont = "";
            string rtime = "";
            DateTime? rtimed = new DateTime();
            
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;

            DataTable oDataSetv6 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
        
            SqlCommand command = oSqlConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "getpersonnallab";
            command.Parameters.AddWithValue("@RequestDate", DateTime.Now.Date.ToString());
            command.Parameters.AddWithValue("@location", "");
            command.Parameters.AddWithValue("@ServiceNo", "");
            command.Parameters.AddWithValue("@ismai", "1");
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
        rtime = dataRow.Field<string>("rtim").ToString(),
        rtimed = dataRow.Field<DateTime?>("rtimed"),
        tubecat = dataRow.Field<int?>("TubeCategory"),
        sname = dataRow.Field<string>("sname"),
        rnkname = dataRow.Field<string>("rnkname"),
        sno = dataRow.Field<string>("sno"),
        inililes = dataRow.Field<string>("inililes"),

        relasiont = dataRow.Field<string>("relasiondet"),
    }).ToList();

            try
            {
                 string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "TRUNCATE TABLE [MMS].[dbo].[TempPsnlTbl]; TRUNCATE TABLE [MMS].[dbo].[TempLabTbl]; ";
                 oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlCommand.ExecuteNonQuery();

                oSqlConnection.Close();
            }
            catch (Exception ex)
            {

            }



            using (System.IO.StreamWriter SW = new System.IO.StreamWriter(
                   Server.MapPath("~/" + sGenName + ".txt")))
            {
                try
                {
                    SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                    msg = "SC \n Service No:01877 \n T/AVM L R Jayaweera \n Creatinine     1.0 test          0.4-1.4      mg/dl \n";
                    SW.WriteLine(msg);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                    msg = "SC \n Service No:01876 \n Gp Capt N D B Abeysekera \n Creatinine     1.0 test          0.4-1.4      mg/dl \n";
                    SW.WriteLine(msg);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                    msg = "SC \n Service No:01992 \n Gp Capt P A V Padmaperuma \n Creatinine     1.0 test          0.4-1.4      mg/dl \n";
                    SW.WriteLine(msg);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                    msg = "SC \n Service No:02155 \n Capt G S S Perera \n Creatinine     1.0 test          0.4-1.4      mg/dl \n";
                    SW.WriteLine(msg);
                }
                catch (Exception ex)
                {

                }
                foreach (var item1 in lablist)
                {
                    try
                    {
                        SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                        SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                        msg = item1.catname + "\n Service No:" + item1.sno + "\n " + item1.rnkname + " " + item1.inililes + " " + item1.sname + "\n" + item1.rtime + "\n";
                        SW.WriteLine(msg);
                        SW.WriteLine("Test Name\tResult\tReference Range\tUnit\n");

                        var lablist1 = from s in db.Lab_Report
                                       join h in db.Lab_SubCategory on s.LabTestID equals h.LabTestID
                                       join v in db.Lab_MainCategory on h.CategoryID equals v.CategoryID
                                       join x in db.Patient_Detail on s.PDID equals x.PDID
                                       join y in db.Patients on x.PID equals y.PID
                                       join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID
                                       where (s.TestSID == item1.tsid)where(v.CategoryID == item1.catid)where(s.Issued == "1")where(s.Isemail != 1) orderby h.SubCategoryID select new { s.Lab_Index, s.LabTestID, h.ReferenceRange, h.ReferenceRangeUnit, h.SubCategoryName, s.testResult, s.teststatus, v.CategoryName, y.ServiceNo, y.Surname, y.Initials, y.rank1.RNK_NAME, z.Relationship, s.RequestedTime };

                        foreach (var item in lablist1)
                        {
                            try
                            {
                               // string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                                string sqlQuery;
                                DataSet oDataSet = new DataSet();
                                oSqlConnection = new SqlConnection(conStr);
                                oSqlCommand = new SqlCommand();
                                sqlQuery = "UPDATE [dbo].[Lab_Report] SET  [Isemail] = 1  WHERE [Lab_Index] ='" + item.Lab_Index + "' ";
                                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                                oSqlCommand.Connection = oSqlConnection;
                                oSqlCommand.CommandText = sqlQuery;
                                oSqlConnection.Open();
                                oSqlCommand.ExecuteNonQuery();

                                oSqlConnection.Close();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                string catnm = "", tesre = "", tesst = "", tesrang = "", tesrangun = "";
                                if (!string.IsNullOrEmpty(item.SubCategoryName))
                                {
                                    catnm = item.SubCategoryName.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.testResult))
                                {
                                    tesre = item.testResult.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.teststatus))
                                {
                                    tesst = item.teststatus.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.ReferenceRange))
                                {
                                    tesrang = item.ReferenceRange.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.ReferenceRangeUnit))
                                {
                                    tesrangun = item.ReferenceRangeUnit.Trim();
                                }

                                tp = catnm + "\t\t" + tesre + "\t" + tesst + "\t" + tesrang + "\t" + tesrangun;
                                SW.WriteLine(tp);
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        // tp = item.phoneno;
                    }
                    catch (Exception ex)
                    {

                    }

                }
                SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                SW.Close();
            }

            System.IO.FileStream fs = null;
            fs = System.IO.File.Open(Server.MapPath("~/" +
                     sGenName + ".txt"), System.IO.FileMode.Open);
            byte[] btFile = new byte[fs.Length];
            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=" +
                               sGenName);
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(btFile);
            Response.End();
            return View();
        }
        public ActionResult smsdaysend(string id)
        {
            string sFileName = System.IO.Path.GetRandomFileName();

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            DateTime dt1 = DateTime.Now;
            if (String.IsNullOrEmpty(id))
            {
               
            }
            else
            {
                 dt1 = DateTime.Parse(id);
            }
           
            string sGenName = dt1.ToString("yyyyMMdd_HHmmss").Trim() + ".txt";
            //  DateTime dt1 = DateTime.Now.Date;
            string labindex = "";
            string msg = "";
            string tp = "";
            string pdids = "";
            string inililes = "";
            string sname = "";
            string sno = "";
            string rnkname = "";
            string catname = "";
            string tsid = "";
            string catid = "";
            string relasiont = "";
            string rtime = "";
            DateTime? rtimed = new DateTime();

            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;

            DataTable oDataSetv6 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            // oSqlCommand = new SqlCommand();

            // oSqlCommand.Connection = oSqlConnection;
            SqlCommand command = oSqlConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "getpersonnallab";
            command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
            command.Parameters.AddWithValue("@location", "");
            command.Parameters.AddWithValue("@ServiceNo", "");
            command.Parameters.AddWithValue("@ismai", "");
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
        rtime = dataRow.Field<string>("rtim").ToString(),
        rtimed = dataRow.Field<DateTime?>("rtimed"),
        tubecat = dataRow.Field<int?>("TubeCategory"),
        sname = dataRow.Field<string>("sname"),
        rnkname = dataRow.Field<string>("rnkname"),
        sno = dataRow.Field<string>("sno"),
        inililes = dataRow.Field<string>("inililes"),

        relasiont = dataRow.Field<string>("relasiondet"),
    }).ToList();

            try
            {
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "TRUNCATE TABLE [MMS].[dbo].[TempPsnlTbl]; TRUNCATE TABLE [MMS].[dbo].[TempLabTbl]; ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlCommand.ExecuteNonQuery();

                oSqlConnection.Close();
            }
            catch (Exception ex)
            {

            }

            using (System.IO.StreamWriter SW = new System.IO.StreamWriter(
                   Server.MapPath("~/" + sGenName + ".txt")))
            {
                try
                {
                    SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                    msg = "SC \n Service No:01877 \n T/AVM L R Jayaweera \n Creatinine     1.0 test          0.4-1.4      mg/dl \n";
                    SW.WriteLine(msg);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                    msg = "SC \n Service No:01876 \n Gp Capt N D B Abeysekera \n Creatinine     1.0 test          0.4-1.4      mg/dl \n";
                    SW.WriteLine(msg);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                    msg = "SC \n Service No:01992 \n Gp Capt P A V Padmaperuma \n Creatinine     1.0 test          0.4-1.4      mg/dl \n";
                    SW.WriteLine(msg);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                    msg = "SC \n Service No:02155 \n Capt G S S Perera \n Creatinine     1.0 test          0.4-1.4      mg/dl \n";
                    SW.WriteLine(msg);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                    msg = "SC \n Service No:03613 \n Flt Lt IC Weerasinghe \n Creatinine     1.0 test          0.4-1.4      mg/dl \n";
                    SW.WriteLine(msg);
                }
                catch (Exception ex)
                {

                }
                foreach (var item1 in lablist)
                {
                    try
                    {
                        SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                        SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                        msg = item1.catname + "\n Service No:" + item1.sno + "\n " + item1.rnkname + " " + item1.inililes + " " + item1.sname + "\n" + item1.rtime + "\n";
                        SW.WriteLine(msg);
                        SW.WriteLine("Test Name\tResult\tReference Range\tUnit\n");

                        var lablist1 = from s in db.Lab_Report
                                       join h in db.Lab_SubCategory on s.LabTestID equals h.LabTestID
                                       join v in db.Lab_MainCategory on h.CategoryID equals v.CategoryID
                                       join x in db.Patient_Detail on s.PDID equals x.PDID
                                       join y in db.Patients on x.PID equals y.PID
                                       join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID
                                                    where(s.TestSID == item1.tsid)where(h.CategoryID == item1.catid)where(s.Issued == "1") orderby h.SubCategoryID select new { s.Lab_Index, s.LabTestID, h.ReferenceRange, h.ReferenceRangeUnit, h.SubCategoryName, s.testResult, s.teststatus, h.Lab_MainCategory.CategoryName, y.ServiceNo, y.Surname, y.Initials, y.rank1.RNK_NAME, z.Relationship, s.RequestedTime };

                        foreach (var item in lablist1)
                        {
                            //try
                            //{
                              
                            //    string sqlQuery;
                            //    DataSet oDataSet = new DataSet();
                            //    oSqlConnection = new SqlConnection(conStr);
                            //    oSqlCommand = new SqlCommand();
                            //    sqlQuery = "UPDATE [dbo].[Lab_Report] SET  [Isemail] = 1  WHERE [Lab_Index] ='" + item.Lab_Index + "' ";
                            //    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            //    oSqlCommand.Connection = oSqlConnection;
                            //    oSqlCommand.CommandText = sqlQuery;
                            //    oSqlConnection.Open();
                            //    oSqlCommand.ExecuteNonQuery();

                            //    oSqlConnection.Close();
                            //}
                            //catch (Exception ex)
                            //{

                            //}
                            try
                            {
                                string catnm="",tesre = "", tesst = "", tesrang = "", tesrangun = "";
                                if (!string.IsNullOrEmpty(item.SubCategoryName))
                                    {
                                    catnm = item.SubCategoryName.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.testResult))
                                {
                                    tesre = item.testResult.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.teststatus))
                                {
                                    tesst = item.teststatus.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.ReferenceRange))
                                {
                                    tesrang = item.ReferenceRange.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.ReferenceRangeUnit))
                                {
                                    tesrangun = item.ReferenceRangeUnit.Trim();
                                }

                                tp = catnm + "\t\t" + tesre + "\t" + tesst + "\t" + tesrang + "\t" + tesrangun;
                                SW.WriteLine(tp);
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        // tp = item.phoneno;

                    }
                    catch (Exception ex)
                    {
                        SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    }
                }
                SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                SW.Close();
            }

            System.IO.FileStream fs = null;
            fs = System.IO.File.Open(Server.MapPath("~/" +
                     sGenName + ".txt"), System.IO.FileMode.Open);
            byte[] btFile = new byte[fs.Length];
            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=" +
                               sGenName);
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(btFile);
            Response.End();
            return View();
        }
        public ActionResult smspsend(string id,string id2)
        {
            string sFileName = System.IO.Path.GetRandomFileName();

            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id2))
            {
                id2 = id2.Trim(MyChar);
            }
            DateTime dt1 = DateTime.Now;
            if (String.IsNullOrEmpty(id))
            {
               
            }
            else
            {
                 dt1 = DateTime.Parse(id);
            }
           
            string sGenName = dt1.ToString("yyyyMMdd_HHmmss").Trim() + ".txt";
            //  DateTime dt1 = DateTime.Now.Date;
            string labindex = "";
            string msg = "";
            string tp = "";
            string pdids = "";
            string inililes = "";
            string sname = "";
            string sno = "";
            string rnkname = "";
            string catname = "";
            string tsid = "";
            string catid = "";
            string relasiont = "";
            string rtime = "";
            DateTime? rtimed = new DateTime();

            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;

            DataTable oDataSetv6 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            // oSqlCommand = new SqlCommand();

            // oSqlCommand.Connection = oSqlConnection;
            SqlCommand command = oSqlConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "getpersonnallab";
            command.Parameters.AddWithValue("@RequestDate", dt1.Date.ToString());
            command.Parameters.AddWithValue("@location", "");
            command.Parameters.AddWithValue("@ServiceNo", id2);
            command.Parameters.AddWithValue("@ismai", "");
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
        rtime = dataRow.Field<string>("rtim").ToString(),
        rtimed = dataRow.Field<DateTime?>("rtimed"),
        tubecat = dataRow.Field<int?>("TubeCategory"),
        sname = dataRow.Field<string>("sname"),
        rnkname = dataRow.Field<string>("rnkname"),
        sno = dataRow.Field<string>("sno"),
        inililes = dataRow.Field<string>("inililes"),

        relasiont = dataRow.Field<string>("relasiondet"),
    }).ToList();
           


            using (System.IO.StreamWriter SW = new System.IO.StreamWriter(
                   Server.MapPath("~/" + sGenName + ".txt")))
            {
                
                
                
              
               
                foreach (var item1 in lablist)
                {
                    try
                    {
                        SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                        SW.WriteLine("\t \t \t SRI LANKA AIR FORCE HOSPITAL LABORATORY SERVICES\n");

                        msg = item1.catname + "\n Service No:" + item1.sno + "\n " + item1.rnkname + " " + item1.inililes + " " + item1.sname + "\n" + item1.rtime + "\n";
                        SW.WriteLine(msg);
                        SW.WriteLine("Test Name\tResult\tReference Range\tUnit\n");

                        var lablist1 = from s in db.Lab_Report
                                       join h in db.Lab_SubCategory on s.LabTestID equals h.LabTestID
                                       join v in db.Lab_MainCategory on h.CategoryID equals v.CategoryID
                                       join x in db.Patient_Detail on s.PDID equals x.PDID
                                       join y in db.Patients on x.PID equals y.PID
                                       join z in db.RelationshipTypes on y.RelationshipType equals z.RTypeID
                                       where(s.TestSID == item1.tsid)where(h.CategoryID == item1.catid)where(s.Issued == "1") orderby h.SubCategoryID select new { s.Lab_Index, s.LabTestID, h.ReferenceRange, h.ReferenceRangeUnit, h.SubCategoryName, s.testResult, s.teststatus, v.CategoryName, y.ServiceNo, y.Surname, y.Initials, y.rank1.RNK_NAME, z.Relationship, s.RequestedTime };

                        foreach (var item in lablist1)
                        {
                            //try
                            //{
                              
                            //    string sqlQuery;
                            //    DataSet oDataSet = new DataSet();
                            //    oSqlConnection = new SqlConnection(conStr);
                            //    oSqlCommand = new SqlCommand();
                            //    sqlQuery = "UPDATE [dbo].[Lab_Report] SET  [Isemail] = 1  WHERE [Lab_Index] ='" + item.Lab_Index + "' ";
                            //    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                            //    oSqlCommand.Connection = oSqlConnection;
                            //    oSqlCommand.CommandText = sqlQuery;
                            //    oSqlConnection.Open();
                            //    oSqlCommand.ExecuteNonQuery();

                            //    oSqlConnection.Close();
                            //}
                            //catch (Exception ex)
                            //{

                            //}
                            try
                            {
                                string catnm="",tesre = "", tesst = "", tesrang = "", tesrangun = "";
                                if (!string.IsNullOrEmpty(item.SubCategoryName))
                                    {
                                    catnm = item.SubCategoryName.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.testResult))
                                {
                                    tesre = item.testResult.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.teststatus))
                                {
                                    tesst = item.teststatus.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.ReferenceRange))
                                {
                                    tesrang = item.ReferenceRange.Trim();
                                }
                                if (!string.IsNullOrEmpty(item.ReferenceRangeUnit))
                                {
                                    tesrangun = item.ReferenceRangeUnit.Trim();
                                }

                                tp = catnm + "\t\t" + tesre + "\t" + tesst + "\t" + tesrang + "\t" + tesrangun;
                                SW.WriteLine(tp);
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        // tp = item.phoneno;
                       
                    }
                    catch (Exception ex)
                    {
                        SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    }
                }
                SW.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                SW.Close();
            }

            System.IO.FileStream fs = null;
            fs = System.IO.File.Open(Server.MapPath("~/" +
                     sGenName + ".txt"), System.IO.FileMode.Open);
            byte[] btFile = new byte[fs.Length];
            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=" +
                               sGenName);
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(btFile);
            Response.End();
            return View();
        }
        public ActionResult Index()
        {

            //string sFileName = System.IO.Path.GetRandomFileName();
            //string sGenName = DateTime.Now.ToString("yyyyMMdd_HHmmss").Trim()+".txt";


            //string msg = "";
            //string tp = "";
            //var lablist = db.Lab_sms.Where(p => p.status == "0").OrderByDescending(p => p.createdtime);
            //using (System.IO.StreamWriter SW = new System.IO.StreamWriter(
            //       Server.MapPath("~/" + sGenName + ".txt")))
            //{
            //    foreach (var item in lablist)
            //    {

            //        msg= item.massegetext;

            //        tp = item.phoneno;
            //        SW.WriteLine(tp+","+msg);
            //        if (ModelState.IsValid)
            //        {
            //            Lab_sms oLab_sms = new Lab_sms();
            //            oLab_sms.smsid = item.smsid;
            //            oLab_sms.createdtime = item.createdtime;
            //            oLab_sms.massegetext = item.massegetext;
            //            oLab_sms.phoneno = item.phoneno;
            //            oLab_sms.pid = item.pid;
            //            oLab_sms.senttime = DateTime.Now;

            //            oLab_sms.status = "1";
            //            using (var context = new MMSEntities())
            //            {
            //                context.Entry(oLab_sms).State = EntityState.Modified;
            //                context.SaveChanges();
            //            }
            //        }
            //    }
            //    SW.Close();
            //}

            //System.IO.FileStream fs = null;
            //fs = System.IO.File.Open(Server.MapPath("~/" +
            //         sGenName + ".txt"), System.IO.FileMode.Open);
            //byte[] btFile = new byte[fs.Length];
            //fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            //fs.Close();
            //Response.AddHeader("Content-disposition", "attachment; filename=" +
            //                   sGenName);
            //Response.ContentType = "application/octet-stream";
            //Response.BinaryWrite(btFile);
            //Response.End();

            return View(db.Lab_sms.ToList());
        }

        // GET: Lab_sms/Details/5
        public ActionResult Details(int? page, string id)
        {
            if (id != null)
            {
                Session["searchid"] = id;
            }
            if (page > 1)
            {
                id = Session["searchid"].ToString();
            }
            if (!String.IsNullOrEmpty(id))
            {

                // string er = lab_sms.createdtime.ToString();
                var onePageOfProducts = (dynamic)null;
                char[] MyChar = { '/', '"', ' ', 'T', 'Z' };
                //if (!String.IsNullOrEmpty(id))
                //{
                //    id = id.Trim(MyChar);
                //    id = id.Replace('T', ' ');
                //    id = id.Replace('-', '/');
                //    id = id.Substring(0, 10);
                //}
                string dt1 = DateTime.Parse(id.Trim()).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                DateTime dt = DateTime.ParseExact(dt1, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                var lablist = db.Lab_sms.Where(p => p.createdtime.Value.Day == dt.Day && p.createdtime.Value.Month == dt.Month && p.createdtime.Value.Year == dt.Year).OrderByDescending(p => p.createdtime);
                var pageNumber = page ?? 1;
                onePageOfProducts = lablist.ToPagedList(pageNumber, 10);
                ViewBag.OnePageOfProducts = onePageOfProducts;
            }
            else
            {
                var onePageOfProducts = (dynamic)null;
                DateTime dt = DateTime.Now;
                var lablist = db.Lab_sms.Where(p => p.createdtime.Value.Day == dt.Day && p.createdtime.Value.Month == dt.Month && p.createdtime.Value.Year == dt.Year).OrderByDescending(p => p.createdtime);

                var pageNumber = page ?? 1;
                onePageOfProducts = lablist.ToPagedList(pageNumber, 10);
                ViewBag.OnePageOfProducts = onePageOfProducts;
            }
            return View();

        }

        // GET: Lab_sms/Create
        public ActionResult Create(int? page, string id)
        {
            var onePageOfProducts = (dynamic)null;
            var lablist = db.Lab_sms.Where(p => p.status == "0").OrderByDescending(p => p.createdtime);

            var pageNumber = page ?? 1;
            onePageOfProducts = lablist.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfProducts = onePageOfProducts;

            return View();
        }

        // POST: Lab_sms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "smsid,massegetext,phoneno,status,createdtime,senttime")] Lab_sms lab_sms)
        {
            if (ModelState.IsValid)
            {
                //var patient_Detail = db.Patient_Detail.Include(p => p.Patient).Where(p => p.Patient.ServiceNo.Contains("")).OrderByDescending(p => p.CreatedDate);
                lab_sms.createdtime = DateTime.Now;
                lab_sms.status = "0";
                db.Lab_sms.Add(lab_sms);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View();
        }

        public JsonResult Savesms(string pdd, string smstext, string pntext)
        {
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(pdd))
            {
                pdd = pdd.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(smstext))
            {
                smstext = smstext.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(pntext))
            {
                pntext = pntext.Trim(MyChar);
            }

            Lab_sms lab_sms = new Lab_sms();
            lab_sms.pid = pdd;
            lab_sms.massegetext = smstext;
            lab_sms.phoneno = pntext;
            lab_sms.createdtime = DateTime.Now;
            lab_sms.status = "0";
            db.Lab_sms.Add(lab_sms);
            db.SaveChanges();
            err = "Saved";
            return Json(err, JsonRequestBehavior.AllowGet);
        }

        // GET: Lab_sms/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab_sms lab_sms = db.Lab_sms.Find(id);
            if (lab_sms == null)
            {
                return HttpNotFound();
            }
            return View(lab_sms);
        }

        // POST: Lab_sms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "smsid,massegetext,phoneno,status,createdtime,senttime")] Lab_sms lab_sms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lab_sms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lab_sms);
        }

        // GET: Lab_sms/Delete/5
        public ActionResult Delete(long? id)
        {

            return View();
        }
        public ActionResult Delete2(long? id)
        {

            return View();
        }
        public ActionResult email(long? id)
        {

            return View();
        }
        // POST: Lab_sms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Lab_sms lab_sms = db.Lab_sms.Find(id);
            db.Lab_sms.Remove(lab_sms);
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
