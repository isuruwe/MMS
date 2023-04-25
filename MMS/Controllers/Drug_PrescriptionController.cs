using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


using PagedList;
using Newtonsoft.Json;
using MMS.Models;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MMS.Controllers
{
    public class Drug_PrescriptionController : Controller
    {
        private MMSEntities db = new MMSEntities();
       // private EPASContext db = new EPASContext();
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        string sqlQuery;
        // GET: Drug_Prescription
        public ActionResult Index()
        {
            var drug_Prescription = db.Drug_Prescription.Include(d => d.DrugMethod).Include(d => d.DrugRoute).Include(d => d.Patient_Detail);
            return View(drug_Prescription.ToList());
        }

        public ActionResult getstdet(string id)
        {

            Session["phbatchid"] = id;
            return View();
        }
        public ActionResult getpharma()
        {
          string  opdid = (String)Session["userlocid1"];
            DataTable oDataSetv7 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "   SELECT max(a.[stcheckdate])stcheckdate,max(b.Clinic_Detail)Clinic_Detail,MAX(a.batchid)batchid " +
 " FROM[MMS].[dbo].[DrugStockCheck] as a inner join[dbo].[Clinic_Master] as b on a.storeid=b.Clinic_ID WHERE " +
 " B.LocationID=(SELECT LocationID FROM[dbo].[Clinic_Master] " +
 "       WHERE Clinic_ID = '"+ opdid + "') group by batchid order by stcheckdate desc";
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

      modifieddate= dataRow.Field<DateTime>("stcheckdate"),
     PDID = dataRow.Field<string>("Clinic_Detail"),
     svcid = dataRow.Field<string>("batchid"),




 }).ToList();

            ViewBag.gtck = joined3;
            return View();
        }

        public ActionResult getdhspharma(string DropDownList1)
        {
            string opdid = (String)Session["userlocid1"];
            
            DataTable oDataSetv7 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "   SELECT max(a.[stcheckdate])stcheckdate,max(b.Clinic_Detail)Clinic_Detail,MAX(a.batchid)batchid " +
 " FROM[MMS].[dbo].[DrugStockCheck] as a inner join[dbo].[Clinic_Master] as b on a.storeid=b.Clinic_ID WHERE " +
 " B.LocationID = '" + DropDownList1 + "' group by batchid order by stcheckdate desc";
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

     modifieddate = dataRow.Field<DateTime>("stcheckdate"),
     PDID = dataRow.Field<string>("Clinic_Detail"),
     svcid = dataRow.Field<string>("batchid"),




 }).ToList();

            ViewBag.gtck = joined3;
            return View();
        }

        public JsonResult Savestck(string itemno, string qnty)
        {
            string err = "";
            string nd = "";
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(itemno))
            {
                itemno = itemno.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(qnty))
            {
                qnty = qnty.Trim(MyChar);
            }
            string txtValue = "";
            try{
                txtValue=(String)Session["selectedloc"];

                int userid = Convert.ToInt32(Session["UserID"]);
                string batchid = txtValue + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
                DataTable oDataSetv8 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT * FROM [MMS].[dbo].[DrugStockMaster] where [ItemID]='"+ itemno + "' and StoreID='"+ txtValue + "' ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv8);
                foreach (DataRow row in oDataSetv8.Rows)
                {
                     nd = row["DrugQuantity"].ToString();
                }


                    DataTable oDataSetv7 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " INSERT INTO [dbo].[DrugStockCheck] ([grugindex],[drugid],[storeid],[drugqnty],[batchid],[stcheckdate],CreatedBy,SysQty) " +
 "    VALUES('" + Guid.NewGuid().ToString() + "','"+itemno+"','"+ txtValue + "','"+qnty+"','"+ batchid + "','"+DateTime.Now+"',"+ userid + ",'"+nd+"') ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
            //   oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetv7);
                err = "Saved";
                RedirectToAction("setstock");
            }
            catch (Exception ex)
            {

            }
            
            return Json(err, JsonRequestBehavior.AllowGet);
        }

        public class doreader
        {


            public string LOC1 { get; set; }
            public string isqty1 { get; set; }
            public string rtnqty1 { get; set; }
            public string rmks1 { get; set; }
            public string batchid1 { get; set; }
        }

        public JsonResult Savedo(string id)
        {
            string err = "";
            string nd = "";
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            
            string txtValue = "";
            try
            {


                var objs = JsonConvert.DeserializeObject<List<doreader>>(id);
                //int objcount = objs.Count;
               // Lab_Report[] oLab_Reports = new Lab_Report[objcount];
               // int i = 0;
               // string labtid = "";
                foreach (doreader p in objs)
                {
                    txtValue = (String)Session["userlocid1"];

                    int userid = Convert.ToInt32(Session["UserID"]);

                    DataTable oDataSetv7 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " UPDATE [dbo].[Drugorder]  set status ='1' ,issuedqty='"+p.isqty1+ "',returnqty='"+p.rtnqty1+ "',remarks='"+p.rmks1+ "',modifieddate='"+ DateTime.Now + "',modifiedby="+ userid + " " +
         "      where itemno='"+p.LOC1+ "' and sopdid='"+p.batchid1+"'";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;
                    //   oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetv7);



                    String locid = "";
                 String   opdid = (String)Session["userlocid1"];
                    if (opdid != null)
                    {
                        var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
                        foreach (var item in clincd)
                        {

                            locid = item.LocationID;
                        }
                        var GetDrug = from v in db.DrugStockMasters where (v.ItemID == p.LOC1) && (v.StoreID == opdid) select new { v.ItemIndex, v.ItemID, v.LOC, v.MFD, v.StoreID, v.DrugQuantity };

                        int objcount = GetDrug.Count();
                        DrugStockMaster[] oLab_Reports1 = new DrugStockMaster[objcount];
                        DrugStockMaster[] oLab_Reports2 = new DrugStockMaster[objcount];
                        DrugStockMaster[] oLab_Reports4 = new DrugStockMaster[objcount];
                        DrugStockTransection[] oLab_Reports3 = new DrugStockTransection[objcount];
                        int i = 0;
                        int i1 = 0;
                        string labtid = "";
                        String[] Clinic_ID1 = p.batchid1.Split('/');
                        String Clinic_ID = Clinic_ID1[0];

                        foreach (var item in GetDrug)
                        {
                            var isupdat = from v in db.DrugStockMasters where (v.ItemID == item.ItemID) && (v.StoreID == Clinic_ID) select new { v.ItemIndex, v.ItemID, v.LOC, v.MFD, v.StoreID, v.DrugQuantity };
                            if (isupdat.Count() != 0)
                            {
                                foreach (var item1 in isupdat)
                                {
                                    try
                                    {
                                        DrugStockMaster oLab_Report = new DrugStockMaster();
                                        oLab_Report.ItemID = item1.ItemID;
                                        oLab_Report.ItemIndex = item1.ItemIndex;
                                        oLab_Report.LOC = item1.LOC;
                                        oLab_Report.MFD = item1.MFD;
                                        oLab_Report.StoreID = Clinic_ID;
                                        oLab_Report.DrugQuantity = (Convert.ToDecimal(item1.DrugQuantity) + Convert.ToDecimal(p.isqty1)).ToString();


                                        oLab_Reports1[i1] = oLab_Report;

                                        DrugStockMaster oLab_Report2 = new DrugStockMaster();
                                        oLab_Report2.ItemID = item1.ItemID;
                                        oLab_Report2.ItemIndex = item.ItemIndex;
                                        oLab_Report2.LOC = item1.LOC;
                                        oLab_Report2.MFD = item1.MFD;
                                        oLab_Report2.StoreID = item.StoreID;
                                        oLab_Report2.DrugQuantity = (Convert.ToDecimal(item.DrugQuantity) - Convert.ToDecimal(p.isqty1)).ToString();


                                        oLab_Reports4[i1] = oLab_Report2;


                                        DrugStockTransection oLab_Reportn = new DrugStockTransection();
                                        oLab_Reportn.ItemID = item1.ItemID;
                                        oLab_Reportn.IssuedDate = DateTime.Now;
                                        oLab_Reportn.IssuedTo = locid;
                                        oLab_Reportn.IssuedUser = userid;
                                        oLab_Reportn.OutLoc = opdid;
                                        oLab_Reportn.InLoc = Clinic_ID;
                                        oLab_Reportn.TransectionQty = Convert.ToDecimal(p.isqty1);


                                        oLab_Reports3[i1] = oLab_Reportn;
                                        i1++;
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                            else
                            {
                                IndexGeneration indi = new IndexGeneration();
                                DrugStockMaster oLab_Report = new DrugStockMaster();
                                oLab_Report.ItemID = item.ItemID.ToString();
                                oLab_Report.ItemIndex = Guid.NewGuid().ToString();
                                oLab_Report.LOC = locid;
                                oLab_Report.MFD = DateTime.Now;
                                oLab_Report.StoreID = Clinic_ID;
                                oLab_Report.DrugQuantity = p.isqty1;


                                oLab_Reports2[i] = oLab_Report;

                                DrugStockMaster oLab_Report2 = new DrugStockMaster();
                                oLab_Report2.ItemID = item.ItemID;
                                oLab_Report2.ItemIndex = item.ItemIndex;
                                oLab_Report2.LOC = item.LOC;
                                oLab_Report2.MFD = item.MFD;
                                oLab_Report2.StoreID = item.StoreID;
                                oLab_Report2.DrugQuantity = (Convert.ToDecimal(item.DrugQuantity) - Convert.ToDecimal(p.isqty1)).ToString();


                                oLab_Reports4[i] = oLab_Report2;


                                DrugStockTransection oLab_Reportn = new DrugStockTransection();
                                oLab_Reportn.ItemID = item.ItemID.ToString();
                                oLab_Reportn.IssuedDate = DateTime.Now;
                                oLab_Reportn.IssuedTo = locid;
                                oLab_Reportn.IssuedUser = userid;
                                oLab_Reportn.OutLoc = opdid;
                                oLab_Reportn.InLoc = Clinic_ID;
                                oLab_Reportn.TransectionQty = Convert.ToDecimal(p.isqty1);


                                oLab_Reports3[i] = oLab_Reportn;
                                i++;
                            }




                        }


                        try
                        {
                            oLab_Reports2 = oLab_Reports2.Where(x => x != null).ToArray();
                            if (oLab_Reports2 != null && oLab_Reports2.Count() != 0)
                            {

                                db.DrugStockMasters.AddRange(oLab_Reports2);
                                db.SaveChanges();
                            }

                            oLab_Reports3 = oLab_Reports3.Where(x => x != null).ToArray();
                            if (oLab_Reports3 != null && oLab_Reports3.Count() != 0)
                            {

                                db.DrugStockTransections.AddRange(oLab_Reports3);
                                db.SaveChanges();
                            }
                            oLab_Reports1 = oLab_Reports1.Where(x => x != null).ToArray();
                            if (oLab_Reports1 != null && oLab_Reports1.Count() != 0)
                            {
                                foreach (var lbrpt in oLab_Reports1)
                                {
                                    var actindb = db.DrugStockMasters.Find(lbrpt.ItemIndex);
                                    db.Entry(actindb).CurrentValues.SetValues(lbrpt);
                                    db.Entry(actindb).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                            oLab_Reports4 = oLab_Reports4.Where(x => x != null).ToArray();
                            if (oLab_Reports4 != null && oLab_Reports4.Count() != 0)
                            {
                                foreach (var lbrpt in oLab_Reports4)
                                {
                                    var actindb = db.DrugStockMasters.Find(lbrpt.ItemIndex);
                                    db.Entry(actindb).CurrentValues.SetValues(lbrpt);
                                    db.Entry(actindb).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            return Json("Error!", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("Please Login!", JsonRequestBehavior.AllowGet);
                    }






                }



             
                err = "Saved";
                RedirectToAction("setorder2");
            }
            catch (Exception ex)
            {

            }

            return Json(err, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Savedemand(string itemno, string qnty)
        {
            string err = "";
            string nd = "";
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(itemno))
            {
                itemno = itemno.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(qnty))
            {
                qnty = qnty.Trim(MyChar);
            }
            string txtValue = "";
            try
            {
                txtValue = (String)Session["userlocid1"];

                int userid = Convert.ToInt32(Session["UserID"]);
                string batchid = txtValue + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
                //DataTable oDataSetv8 = new DataTable();
                //oSqlConnection = new SqlConnection(conStr);
                //oSqlCommand = new SqlCommand();
                //sqlQuery = "SELECT * FROM [MMS].[dbo].[DrugStockMaster] where [ItemID]='" + itemno + "' and StoreID='" + txtValue + "' ";
                //// sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                //oSqlCommand.Connection = oSqlConnection;
                //oSqlCommand.CommandText = sqlQuery;
                //oSqlCommand.CommandTimeout = 120;
                ////   oSqlConnection.Open();
                //oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                //oSqlDataAdapter.Fill(oDataSetv8);
                //foreach (DataRow row in oDataSetv8.Rows)
                //{
                //    nd = row["DrugQuantity"].ToString();
                //}


                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " INSERT INTO [dbo].[Drugorder] ([id],[itemno],[ropdid],[demandqty],[sopdid],[createddate],createdby,status) " +
     "    VALUES('" + Guid.NewGuid().ToString() + "','" + itemno + "','" + txtValue + "','" + qnty + "','" + batchid + "','" + DateTime.Now + "'," + userid + ",'0') ";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                err = "Saved";
                RedirectToAction("setorder");
            }
            catch (Exception ex)
            {

            }

            return Json(err, JsonRequestBehavior.AllowGet);
        }
        public ActionResult setorder()
        {
            string txtValue = (String)Session["userlocid1"];
            string batchid = txtValue + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
            DataTable oDataSetv7 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "        SELECT a.demandqty,a.issuedqty,a.returnqty,a.status,a.remarks,COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'')  " +
  "  as itemdescription   FROM[MMS].[dbo].[Drugorder] as a left join[MMS].[dbo].[DrugItems] as d on " +

"    a.itemno=Convert(varchar, d.DrugID)     left join[MMS].[dbo].[EPASPharmacyItems] as e on a.itemno=Convert(varchar, e.[itemno]) where a.ropdid='" + txtValue + "' and a.sopdid='" + batchid + "'    order by a.createddate";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
            //   oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetv7);
            // oSqlConnection.Close();

            var joined3 = oDataSetv7.AsEnumerable()
 .Select(dataRow => new getdrugdata
 {

     ItemID = dataRow.Field<int?>("status").ToString(),
     DrugQuantity = dataRow.Field<string>("demandqty"),
     itemdescription = dataRow.Field<string>("itemdescription"),
     StoreID = dataRow.Field<string>("issuedqty"),
     remarks = dataRow.Field<string>("remarks"),
     returnqty = dataRow.Field<string>("returnqty"),

 }).ToList();

            ViewBag.stck = joined3;

            String locid = (String)Session["userloc"];
            DataTable oDataSetv8 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " SELECT a.[sopdid],max(a.createddate)createddate,max(b.Clinic_Detail)Clinic_Detail,max(a.status)status FROM [MMS].[dbo].[Drugorder] as a left join [dbo].[Clinic_Master] as b on b.Clinic_ID=a.ropdid where b.LocationID='" + locid + "'  group by a.sopdid";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
            //   oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetv8);
            // oSqlConnection.Close();

            var joined4 = oDataSetv8.AsEnumerable()
 .Select(dataRow => new getsickdata2
 {


     svcid = dataRow.Field<string>("sopdid"),
     modifieddate = dataRow.Field<DateTime?>("createddate"),
     lname = dataRow.Field<string>("Clinic_Detail"),
     rt = dataRow.Field<int?>("status"),

 }).ToList();

            ViewBag.gtclbatch = joined4;




            return View();
        }
        [HttpPost]
        public ActionResult setorder(FormCollection formCollection)
        {
            string txtValue = formCollection["gl"];
            Session["selectedloc"] = txtValue;
            string batchid = txtValue + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
            DataTable oDataSetv7 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "        SELECT a.SysQty,a.drugqnty,COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'')  " +
 "  as itemdescription   FROM[MMS].[dbo].[DrugStockCheck] as a left join[MMS].[dbo].[DrugItems] as d on " +

"    a.drugid=Convert(varchar, d.DrugID)     left join[MMS].[dbo].[EPASPharmacyItems] as e on a.drugid=Convert(varchar, e.[itemno]) where a.storeid='" + txtValue + "' and a.batchid='" + batchid + "'    order by a.stcheckdate";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
            //   oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetv7);
            // oSqlConnection.Close();

            var joined3 = oDataSetv7.AsEnumerable()
 .Select(dataRow => new getdrugdata
 {

     ItemID = dataRow.Field<string>("SysQty"),
     DrugQuantity = dataRow.Field<string>("drugqnty"),
     itemdescription = dataRow.Field<string>("itemdescription"),




 }).ToList();

            ViewBag.stck = joined3;
            return View();
        }
        // GET: Drug_Prescription/Details/5
        public ActionResult setorder2()
        {
            String locid = (String)Session["userloc"];
            DataTable oDataSetv8 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = " SELECT a.[sopdid],max(a.createddate)createddate,max(b.Clinic_Detail)Clinic_Detail,max(a.status)status FROM [MMS].[dbo].[Drugorder] as a left join [dbo].[Clinic_Master] as b on b.Clinic_ID=a.ropdid where b.LocationID='"+locid+"'  group by a.sopdid";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
            //   oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetv8);
            // oSqlConnection.Close();

            var joined4 = oDataSetv8.AsEnumerable()
 .Select(dataRow => new getsickdata2
 {


     svcid = dataRow.Field<string>("sopdid"),
     modifieddate= dataRow.Field<DateTime?>("createddate"),
     lname= dataRow.Field<string>("Clinic_Detail"),
     rt = dataRow.Field<int?>("status"),

 }).ToList();

            ViewBag.gtclbatch = joined4;




//            string txtValue = (String)Session["userlocid1"];
//            string batchid = txtValue + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
//            DataTable oDataSetv7 = new DataTable();
//            oSqlConnection = new SqlConnection(conStr);
//            oSqlCommand = new SqlCommand();
//            sqlQuery = "        SELECT a.demandqty,a.issuedqty,a.returnqty,a.status,a.remarks,COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'')  " +
//  "  as itemdescription   FROM[MMS].[dbo].[Drugorder] as a left join[MMS].[dbo].[DrugItems] as d on " +

//"    a.itemno=Convert(varchar, d.DrugID)     left join[MMS].[dbo].[EPASPharmacyItems] as e on a.itemno=Convert(varchar, e.[itemno]) where  a.sopdid='" + batchid + "'    order by a.createddate";
//            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
//            oSqlCommand.Connection = oSqlConnection;
//            oSqlCommand.CommandText = sqlQuery;
//            oSqlCommand.CommandTimeout = 120;
//            //   oSqlConnection.Open();
//            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
//            oSqlDataAdapter.Fill(oDataSetv7);
//            // oSqlConnection.Close();

//            var joined3 = oDataSetv7.AsEnumerable()
// .Select(dataRow => new getdrugdata
// {

//     ItemID = dataRow.Field<string>("status"),
//     DrugQuantity = dataRow.Field<string>("demandqty"),
//     itemdescription = dataRow.Field<string>("itemdescription"),
//     StoreID = dataRow.Field<string>("issuedqty"),
//     remarks = dataRow.Field<string>("remarks"),
//     returnqty = dataRow.Field<string>("returnqty"),

// }).ToList();

           // ViewBag.stck = joined3;
            return View();
        }
        public JsonResult loaddorder(string id)
        {try
            {
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                string batchid = NewString;
                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "        SELECT a.sopdid,a.itemno,a.demandqty,a.issuedqty,a.returnqty,a.status,a.remarks,COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'')  " +
      "  as itemdescription   FROM[MMS].[dbo].[Drugorder] as a left join[MMS].[dbo].[DrugItems] as d on " +

    "    a.itemno=Convert(varchar, d.DrugID)     left join[MMS].[dbo].[EPASPharmacyItems] as e on a.itemno=Convert(varchar, e.[itemno]) where  a.sopdid='" + batchid + "'    order by a.createddate";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getdrugdata
     {

         ItemID = dataRow.Field<int?>("status").ToString(),
         DrugQuantity = dataRow.Field<string>("demandqty"),
         itemdescription = dataRow.Field<string>("itemdescription"),
         StoreID = dataRow.Field<string>("issuedqty"),
         remarks = dataRow.Field<string>("remarks"),
         returnqty = dataRow.Field<string>("returnqty"),
         LOC = dataRow.Field<string>("itemno"),
         batchid = dataRow.Field<string>("sopdid"),
     }).ToList();
                return Json(joined3, JsonRequestBehavior.AllowGet);
                //  ViewBag.stck = joined3;
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            
        }


        [HttpPost]
        public ActionResult setorder2(FormCollection formCollection)
        {
            string txtValue = formCollection["gl"];
            Session["selectedloc"] = txtValue;
            string batchid = txtValue + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
            DataTable oDataSetv7 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "        SELECT a.SysQty,a.drugqnty,COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'')  " +
 "  as itemdescription   FROM[MMS].[dbo].[DrugStockCheck] as a left join[MMS].[dbo].[DrugItems] as d on " +

"    a.drugid=Convert(varchar, d.DrugID)     left join[MMS].[dbo].[EPASPharmacyItems] as e on a.drugid=Convert(varchar, e.[itemno]) where a.storeid='" + txtValue + "' and a.batchid='" + batchid + "'    order by a.stcheckdate";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
            //   oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetv7);
            // oSqlConnection.Close();

            var joined3 = oDataSetv7.AsEnumerable()
 .Select(dataRow => new getdrugdata
 {

     ItemID = dataRow.Field<string>("SysQty"),
     DrugQuantity = dataRow.Field<string>("drugqnty"),
     itemdescription = dataRow.Field<string>("itemdescription"),




 }).ToList();

            ViewBag.stck = joined3;
            return View();
        }
        public ActionResult setstock()
        {
            string txtValue = (String)Session["selectedloc"] ;
            string batchid = txtValue + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
            DataTable oDataSetv7 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "        SELECT a.SysQty,a.drugqnty,COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'')  " + 
  "  as itemdescription   FROM[MMS].[dbo].[DrugStockCheck] as a left join[MMS].[dbo].[DrugItems] as d on "+ 

"    a.drugid=Convert(varchar, d.DrugID)     left join[MMS].[dbo].[EPASPharmacyItems] as e on a.drugid=Convert(varchar, e.[itemno]) where a.storeid='" + txtValue + "' and a.batchid='" + batchid + "'    order by a.stcheckdate";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
            //   oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetv7);
            // oSqlConnection.Close();

            var joined3 = oDataSetv7.AsEnumerable()
 .Select(dataRow => new getdrugdata
 {

     ItemID = dataRow.Field<string>("SysQty"),
     DrugQuantity = dataRow.Field<string>("drugqnty"),
     itemdescription = dataRow.Field<string>("itemdescription"),




 }).ToList();

            ViewBag.stck = joined3;
            return View();
        }

        [HttpPost]
        public ActionResult setstock(FormCollection formCollection)
                {
            string txtValue = formCollection["gl"];
            Session["selectedloc"] = txtValue;
            string batchid = txtValue + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
            DataTable oDataSetv7 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "        SELECT a.SysQty,a.drugqnty,COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'')  " +
 "  as itemdescription   FROM[MMS].[dbo].[DrugStockCheck] as a left join[MMS].[dbo].[DrugItems] as d on " +

"    a.drugid=Convert(varchar, d.DrugID)     left join[MMS].[dbo].[EPASPharmacyItems] as e on a.drugid=Convert(varchar, e.[itemno]) where a.storeid='" + txtValue + "' and a.batchid='" + batchid + "'    order by a.stcheckdate";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
            //   oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetv7);
            // oSqlConnection.Close();

            var joined3 = oDataSetv7.AsEnumerable()
 .Select(dataRow => new getdrugdata
 {

     ItemID = dataRow.Field<string>("SysQty"),
     DrugQuantity = dataRow.Field<string>("drugqnty"),
     itemdescription = dataRow.Field<string>("itemdescription"),
   
    


 }).ToList();

            ViewBag.stck = joined3;
            return View();
        }
        // GET: Drug_Prescription/Details/5
        public ActionResult Details(int? page, string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            //var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //    locid = item.LocationID;
            //}

            opdid = (String)Session["userlocid1"];

            var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

            foreach (var item in serW)
            {

                locid = item.LocationID;
            }
            var onePageOfProducts = (dynamic)null;
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }

            if (!String.IsNullOrEmpty(id))
            {
                //var drugdes =from d in db.Drug_Prescription.Where(p => p.RequestedLocID == locid).Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id))
                //join e in db.Patient_Detail on d.PDID equals e.PDID
                //                                                                   into se
                //                            from e in se.DefaultIfEmpty()
                //              join b in db.Patients on e.PID equals b.PID
                //                       into sc
                //                          from b in sc.DefaultIfEmpty()
                //             join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID
                //             orderby d.Date_Time descending


                //              select new getdocdetail { sv = b.Service_Type, pdids = d.PDID, inililes = b.Initials, sname = b.Surname, sno = b.ServiceNo, rnkname = b.rank1.RNK_NAME, pidp = e.PID, relasiondet =f.Relationship, crdate = d.Date_Time, relasiont = f.RTypeID };


                DataTable oDataSet4 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();

                sqlQuery = "   SELECT  (a.Present_Complain)pcomoplian,b.ServiceStatus, COALESCE(NULLIF((b.Surname ), ''), c.Initials+' '+c.Surname)  	sname  " +
"  ,(b.Rank)rnkname, " +
"  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"   , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate, (h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
"  as b on c.ServiceNo=b.ServiceNo and c.Service_Type= b.ServiceType left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
"  inner join Drug_Prescription as i on i.PDID = a.PDID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where   c.ServiceNo= '" + id + "' and RequestedLocID='" + locid + "' and   Issued= 1 " +

"    order by crdate";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet4);
                oSqlConnection.Close();
                var drugdes = oDataSet4.AsEnumerable()
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

                var filist = drugdes.GroupBy(d => new { d.pdids }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.crdate);
                var pageNumber = page ?? 1;
                onePageOfProducts = filist.ToPagedList(pageNumber, 10);
            }
            else
            {
                //var drugdes =from d in db.Drug_Prescription.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == 1)
                //join e in db.Patient_Detail on d.PDID equals e.PDID
                //                                                    into se
                //             from e in se.DefaultIfEmpty()
                //              join b in db.Patients on e.PID equals b.PID
                //                       into sc
                //                          from b in sc.DefaultIfEmpty()
                //             join f in db.RelationshipTypes on b.RelationshipType equals f.RTypeID
                //             orderby d.Date_Time descending


                //              select new getdocdetail { sv = b.Service_Type, pdids = d.PDID, inililes = b.Initials, sname = b.Surname, sno = b.ServiceNo, rnkname = b.rank1.RNK_NAME, pidp = e.PID, relasiondet = f.Relationship, crdate = d.Date_Time, relasiont = b.RelationshipType };

                DataTable oDataSet4 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();

                sqlQuery = "   SELECT  (a.Present_Complain)pcomoplian,b.ServiceStatus, COALESCE(NULLIF((b.Surname ), ''), c.Initials+' '+c.Surname)  	sname  " +
"  ,(b.Rank)rnkname, " +
"  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"   , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate, (h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
"  as b on c.ServiceNo=b.ServiceNo and c.Service_Type= b.ServiceType left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
"  inner join Drug_Prescription as i on i.PDID = a.PDID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where    RequestedLocID='" + locid + "' and   Issued= 1 " +

"    order by crdate";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet4);
                oSqlConnection.Close();
                var drugdes = oDataSet4.AsEnumerable()
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



                var filist = drugdes.GroupBy(d => new { d.pdids }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.crdate);

                var pageNumber = page ?? 1;
                onePageOfProducts = filist.ToPagedList(pageNumber, 10);
            }



            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();

        }


        public ActionResult view()
        {
            
            return View();
        }

        public ActionResult view1()
        {

            return View();
        }
        public ActionResult view2()
        {

            return View();
        }
        public class Drugreader
        {
           
            public Drugreader(string genid, string isuqn)
            {
                this.psindex1 = genid;
                this.issudqnty1 = isuqn;
            }

            public string issudqnty1 { get; set; }
            
            public string psindex1 { get; set; }

        }
        public class Drugreader1
        {
            
                public string disq1 { get; set; }
            public string dDose { get; set; }
            public int dRoute { get; set; }
            public int dMethod { get; set; }
            public string dDuration { get; set; }
            public string dItemno { get; set; }
            public string dStockTypeID { get; set; }
        }

        public JsonResult Drugissued(string id,string psitems, string ditems)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            string clinicid = "";
            double id1 = 0;
            int userid = Convert.ToInt32(Session["UserID"]);
            opdid = (String)Session["userlocid1"];
            if (opdid != null)
            {
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
                foreach (var item in clincd)
                {

                    locid = item.LocationID;
                }

                char[] MyChar = { '/', '"', ' ' };
                if (!String.IsNullOrEmpty(id))
                {
                    id = id.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(psitems))
                {
                    psitems = psitems.Trim(MyChar);
                }
                if (!String.IsNullOrEmpty(ditems))
                {
                    ditems = ditems.Trim(MyChar);
                }
                var clincd1 = from v in db.Clinic_Master where (v.LocationID == locid && v.Clinic_Detail.Contains("opd") && v.ClinicTypeID == 19) select new { v.Clinic_ID };
                foreach (var item in clincd1)
                {

                    clinicid = item.Clinic_ID;
                }

                string pdid = id;
                string genid = "";
                string isuqn = "";
                var objs = JsonConvert.DeserializeObject<List<Drugreader>>(psitems);
                int objcounts = objs.Count;
                ////////////////////////////////////////////////
                var objs1 = (dynamic)null;
                int objcount = 0;
                try
                {
                    Drug_Prescription[] objDrug = new Drug_Prescription[1000];
                    if (!String.IsNullOrEmpty(ditems))
                    {


                        objs1 = JsonConvert.DeserializeObject<List<Drugreader1>>(ditems);

                        objcount = objs1.Count;


                        int ix = 0;

                        foreach (Drugreader1 p in objs1)
                        {

                            var oldtestcnt = db.Drug_Prescription.Where(d => d.ItemNo == p.dItemno).Where(d => d.PDID == id).Where(d => d.Issued == 0).ToList().Count;
                            if (oldtestcnt < 1)
                            {
                                Drug_Prescription oDrug_Prescription = new Drug_Prescription();
                                oDrug_Prescription.PDID = id;
                                genid = Guid.NewGuid().ToString();
                                oDrug_Prescription.Ps_Index = genid;
                                oDrug_Prescription.Dose = p.dDose;
                                oDrug_Prescription.Method = p.dMethod;
                                oDrug_Prescription.Route = p.dRoute;
                                oDrug_Prescription.ItemNo = p.dItemno;
                                oDrug_Prescription.MethodType = Convert.ToInt32(p.dStockTypeID);
                                oDrug_Prescription.Duration = p.dDuration;
                                oDrug_Prescription.RequestedLocID = locid;
                                oDrug_Prescription.LocID = opdid;
                                oDrug_Prescription.Issued = 0;
                                isuqn = p.disq1;
                                oDrug_Prescription.issuedQuantity = p.disq1;
                                oDrug_Prescription.Date_Time = DateTime.Now.Date;
                                objDrug[ix] = oDrug_Prescription;
                                ix++;
                                objs.Add(new Drugreader(genid, isuqn));
                            }
                            else
                            {
                                Drug_Prescription oDrug_Prescription = new Drug_Prescription();
                                oDrug_Prescription.PDID = id;
                                genid = Guid.NewGuid().ToString();
                                oDrug_Prescription.Ps_Index = genid;
                                oDrug_Prescription.Dose = p.dDose;
                                oDrug_Prescription.Method = p.dMethod;
                                oDrug_Prescription.Route = p.dRoute;
                                oDrug_Prescription.ItemNo = p.dItemno;
                                oDrug_Prescription.MethodType = Convert.ToInt32(p.dStockTypeID);
                                oDrug_Prescription.Duration = p.dDuration;
                                oDrug_Prescription.RequestedLocID = locid;
                                oDrug_Prescription.LocID = opdid;
                                oDrug_Prescription.Issued = 0;
                                isuqn = p.disq1;
                                oDrug_Prescription.issuedQuantity = p.disq1;
                                oDrug_Prescription.Date_Time = DateTime.Now.Date;
                                objDrug[ix] = oDrug_Prescription;
                                ix++;
                                objs.Add(new Drugreader(genid, isuqn));
                            }

                        }
                    }

                    objDrug = objDrug.Where(x => x != null).ToArray();
                    db.Drug_Prescription.AddRange(objDrug);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
                ///////////////////////////////////



                try
                {
                    Drug_Prescription[] cDrug_Prescription = new Drug_Prescription[objs.Count];
                    DrugStockMaster[] oLab_Reports1 = new DrugStockMaster[objs.Count];
                    DrugStockMaster[] oLab_Reports2 = new DrugStockMaster[objs.Count];
                    DrugStockMaster[] oLab_Reports4 = new DrugStockMaster[objs.Count];
                    DrugStockTransection[] oLab_Reports3 = new DrugStockTransection[objs.Count];
                    int i = 0;
                    int i1 = 0;
                    foreach (Drugreader dr in objs)
                    {
                        var druglist = db.Drug_Prescription.Where(p => p.PDID == pdid).Where(p => p.Issued == 0).Where(p => p.Ps_Index == dr.psindex1);
                        // int objcount = druglist.Count();


                        foreach (var imt in druglist)
                        {

                            Drug_Prescription oDrug_Prescription = new Drug_Prescription();
                            oDrug_Prescription.PDID = imt.PDID;
                            oDrug_Prescription.Issued = 1;
                            oDrug_Prescription.Date_Time = imt.Date_Time;
                            oDrug_Prescription.Dose = imt.Dose;
                            oDrug_Prescription.Method = imt.Method;
                            oDrug_Prescription.Route = imt.Route;
                            oDrug_Prescription.Duration = imt.Duration;
                            oDrug_Prescription.IssuedLocID = opdid;
                            oDrug_Prescription.ItemNo = imt.ItemNo;
                            oDrug_Prescription.LocID = imt.LocID;
                            oDrug_Prescription.MethodType = imt.MethodType;
                            oDrug_Prescription.RequestedLocID = imt.RequestedLocID;
                            oDrug_Prescription.GivenBy = userid.ToString();
                            oDrug_Prescription.Ps_Index = imt.Ps_Index;
                            oDrug_Prescription.issuedQuantity = dr.issudqnty1;
                            cDrug_Prescription[i] = oDrug_Prescription;


                            //var GetDrug = from v in db.DrugStockMasters where (v.ItemID == id) && (v.StoreID == opdid) select new { v.ItemIndex, v.ItemID, v.LOC, v.MFD, v.StoreID, v.DrugQuantity };





                            string labtid = "";



                            var isupdat = from v in db.DrugStockMasters where (v.ItemID == imt.ItemNo) && (v.StoreID == opdid) select new { v.ItemIndex, v.ItemID, v.LOC, v.MFD, v.StoreID, v.DrugQuantity };
                            if (isupdat.Count() != 0)
                            {
                                foreach (var item1 in isupdat)
                                {
                                    try
                                    {
                                        DrugStockMaster oLab_Report = new DrugStockMaster();
                                        oLab_Report.ItemID = item1.ItemID;
                                        oLab_Report.ItemIndex = item1.ItemIndex;
                                        oLab_Report.LOC = item1.LOC;
                                        oLab_Report.MFD = item1.MFD;
                                        oLab_Report.StoreID = opdid;
                                        oLab_Report.DrugQuantity = (Convert.ToDecimal(item1.DrugQuantity) - Convert.ToDecimal(dr.issudqnty1)).ToString();


                                        oLab_Reports1[i1] = oLab_Report;



                                        DrugStockTransection oLab_Reportn = new DrugStockTransection();
                                        oLab_Reportn.ItemID = item1.ItemID;
                                        oLab_Reportn.IssuedDate = DateTime.Now;
                                        oLab_Reportn.IssuedTo = locid;
                                        oLab_Reportn.IssuedUser = userid;
                                        oLab_Reportn.OutLoc = opdid;
                                        oLab_Reportn.InLoc = imt.LocID;
                                        oLab_Reportn.TransectionQty = Convert.ToDecimal(dr.issudqnty1);


                                        oLab_Reports3[i1] = oLab_Reportn;
                                        i1++;
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    IndexGeneration indi = new IndexGeneration();
                                    DrugStockMaster oLab_Report = new DrugStockMaster();
                                    oLab_Report.ItemID = imt.ItemNo.ToString();
                                    oLab_Report.ItemIndex = Guid.NewGuid().ToString();
                                    oLab_Report.LOC = locid;
                                    oLab_Report.MFD = DateTime.Now;
                                    oLab_Report.StoreID = opdid;
                                    oLab_Report.DrugQuantity = (0 - Convert.ToInt32(dr.issudqnty1)).ToString();


                                    oLab_Reports2[i1] = oLab_Report;




                                    DrugStockTransection oLab_Reportn = new DrugStockTransection();
                                    oLab_Reportn.ItemID = imt.ItemNo.ToString();
                                    oLab_Reportn.IssuedDate = DateTime.Now;
                                    oLab_Reportn.IssuedTo = imt.LocID;
                                    oLab_Reportn.IssuedUser = userid;
                                    oLab_Reportn.OutLoc = opdid;
                                    oLab_Reportn.InLoc = imt.LocID;
                                    oLab_Reportn.TransectionQty = Convert.ToDecimal(dr.issudqnty1);


                                    oLab_Reports3[i1] = oLab_Reportn;
                                    i1++;
                                }
                                catch (Exception ex)
                                {

                                }
                            }



                            i++;
                        }




                    }





                    cDrug_Prescription = cDrug_Prescription.Where(x => x != null).ToArray();

                    foreach (var lbrpt in cDrug_Prescription)
                    {
                        var actindb = db.Drug_Prescription.Find(lbrpt.Ps_Index);
                        db.Entry(actindb).CurrentValues.SetValues(lbrpt);
                        db.Entry(actindb).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    oLab_Reports2 = oLab_Reports2.Where(x => x != null).ToArray();
                    if (oLab_Reports2 != null && oLab_Reports2.Count() != 0)
                    {

                        db.DrugStockMasters.AddRange(oLab_Reports2);
                        db.SaveChanges();
                    }

                    oLab_Reports3 = oLab_Reports3.Where(x => x != null).ToArray();
                    if (oLab_Reports3 != null && oLab_Reports3.Count() != 0)
                    {

                        db.DrugStockTransections.AddRange(oLab_Reports3);
                        db.SaveChanges();
                    }
                    oLab_Reports1 = oLab_Reports1.Where(x => x != null).ToArray();
                    if (oLab_Reports1 != null && oLab_Reports1.Count() != 0)
                    {
                        foreach (var lbrpt in oLab_Reports1)
                        {
                            var actindb = db.DrugStockMasters.Find(lbrpt.ItemIndex);
                            db.Entry(actindb).CurrentValues.SetValues(lbrpt);
                            db.Entry(actindb).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    oLab_Reports4 = oLab_Reports4.Where(x => x != null).ToArray();
                    if (oLab_Reports4 != null && oLab_Reports4.Count() != 0)
                    {
                        foreach (var lbrpt in oLab_Reports4)
                        {
                            var actindb = db.DrugStockMasters.Find(lbrpt.ItemIndex);
                            db.Entry(actindb).CurrentValues.SetValues(lbrpt);
                            db.Entry(actindb).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                }
                catch (Exception ex)
                {

                }


                return Json(objs, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult loadisudruglist(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);
             string opdid = (String)Session["userlocid1"];

            var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
            var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
            var serc = from s in db.Drug_Prescription.Where(p => p.PDID == NewString && p.Issued==1 && p.IssuedLocID== opdid) orderby s.ItemNo select new { s.Ps_Index, s.ItemNo, s.DrugMethod.MethodDetail,s.DrugMethod.DrugMethodCount, s.DrugRoute.RouteDetail, s.Duration, s.Dose, s.PDID,s.MethodType,s.issuedQuantity };
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
            var joined = from it1 in t1 join it2 in t2 on it1.ItemNo equals it2.itemno   select new  { Ps_Index= it1.Ps_Index, ItemNo= it1.ItemNo, MethodDetail= it1.MethodDetail,mcnt= it1.DrugMethodCount, RouteDetail= it1.RouteDetail, Dose= it1.Dose, Duration= it1.Duration, itemdescription= it2.itemdescription, PDID= it1.PDID,mt= it1.MethodType, isq = it1.issuedQuantity };

            var joined1 = from it1 in t1 join it2 in t3 on it1.ItemNo equals it2.DrugID.ToString() select new  { Ps_Index=it1.Ps_Index, ItemNo= it1.ItemNo, MethodDetail= it1.MethodDetail, mcnt = it1.DrugMethodCount, RouteDetail = it1.RouteDetail, Dose= it1.Dose, Duration= it1.Duration, itemdescription= it2.ItemDescription, PDID= it1.PDID , mt = it1.MethodType,isq=it1.issuedQuantity };
            var u1 = joined.ToList().GroupBy(t=>t.Ps_Index).Select(grp=>grp.First());
            var u2 = joined1.ToList().GroupBy(t => t.Ps_Index).Select(grp => grp.First());
            var joined3 = u1.Concat(u2).DefaultIfEmpty();
           
            return Json(joined3.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult loaddruglist(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);
       string     opdid = (String)Session["userlocid1"];
            if (opdid != null)
            {
                //var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription ,d.itemno};
                //var ser = from s in db.Drug_Prescription.Where(p => p.PDID == NewString)  orderby s.ItemNo  select new { s.Ps_Index, s.ItemNo, s.DrugMethod.MethodDetail, s.DrugRoute.RouteDetail ,s.Duration,s.Dose,s.PDID};
                //int i = 0;

                //foreach (var itm in ser)
                //{


                //  var  items2 = from d in db.EPASPharmacyItems.Where(p => p.itemno == itm.ItemNo) select new { d.itemdescription,d.itemno };
                //    if (i != 0)
                //    {
                //        items = items2.Concat(items);

                //    }
                //    else
                //    {

                //        items = items2;
                //    }

                //    i++;
                //}
                //var t1 = ser.ToList();
                //var t2 = items.ToList();

                //var joined = from it1 in t1 join it2 in t2 on it1.ItemNo equals it2.itemno select new {it1.Ps_Index, it1.ItemNo, it1.MethodDetail, it1.RouteDetail, it1.Dose, it1.Duration, it2.itemdescription,it1.PDID };
                var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
                var serc = from s in db.Drug_Prescription.Where(p => p.PDID == NewString && p.Issued == 0) orderby s.ItemNo select new { s.Ps_Index, s.ItemNo, s.DrugMethod.MethodDetail, s.DrugMethod.DrugMethodCount, s.DrugRoute.RouteDetail, s.Duration, s.Dose, s.PDID, s.MethodType };
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
                var u1 = joined.ToList().GroupBy(t => t.Ps_Index).Select(grp => grp.First());
                var u2 = joined1.ToList().GroupBy(t => t.Ps_Index).Select(grp => grp.First());
                var joined3 = u1.Concat(u2);

                return Json(joined3.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        // GET: Drug_Prescription/Create

        public JsonResult Getspins(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);

           
           
            var serc = from s in db.Patient_Detail.Where(p => p.PDID == NewString)  select new { s.Examination };
            try
            {
                DataTable oDataSetv1 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "   SELECT HypersenceMainCatID,HypersenseDetail,HypersenceMainCategory FROM [MMS].[dbo].[Hypersensivity] as a inner join " +
    " [MMS].[dbo].[HypMainCategory] as b on a.[HyperTypeSubID]=b.HypersenceMainCatID where a.PID=(select pid from [MMS].[dbo].[Patient_Detail] where pdid='"+ NewString + "')  ";
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


                return Json(new { s1 = serc.ToList(), l1 = a2.ToList() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult Create(int? page, string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            //var ser = from s in db.Users.Where(p => p.UserID == userid) select new { s.LocationID };

            //foreach (var item in ser)
            //{

            //    locid = item.LocationID;
            //}

            opdid = (String)Session["userlocid1"];
            if (opdid != null)
            {
                var serW = from s in db.Clinic_Master.Where(p => p.Clinic_ID == opdid) select new { s.LocationID };

                foreach (var item in serW)
                {

                    locid = item.LocationID;
                }
                var onePageOfProducts = (dynamic)null;
                char[] MyChar = { '/', '"', ' ' };
                if (!String.IsNullOrEmpty(id))
                {
                    id = id.Trim(MyChar);
                }

                if (!String.IsNullOrEmpty(id))
                {
                    DataTable oDataSet4 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();

                    sqlQuery = "   SELECT  (a.Present_Complain)pcomoplian,b.ServiceStatus, COALESCE(NULLIF((b.Surname ), ''), c.Initials+' '+c.Surname)  	sname  " +
 "  ,(b.Rank)rnkname, " +
 "  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"   , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate, (h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
 "  as b on c.ServiceNo=b.ServiceNo and c.Service_Type= b.ServiceType left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
 "  inner join Drug_Prescription as i on i.PDID = a.PDID "+
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where   c.ServiceNo= '" + id + "' and RequestedLocID='"+locid+ "' and   Issued= 0 " +

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







                    //var drugdes = from d in db.Drug_Prescription.Where(p => p.RequestedLocID == locid).Where(p => p.Patient_Detail.Patient.ServiceNo.Contains(id))

                    //              join e in db.Patient_Detail on d.PDID equals e.PDID
                    //                                                                   into se
                    //              from e in se.DefaultIfEmpty()
                    //              join b in db.Patients on e.PID equals b.PID
                    //                       into sc
                    //              from b in sc.DefaultIfEmpty()
                    //              orderby d.Date_Time descending


                    //              select new getdocdetail { sv = b.Service_Type, pdids = d.PDID, inililes = b.Initials, sname = b.Surname, sno = b.ServiceNo, rnkname = b.rank1.RNK_NAME, pidp = e.PID, relasiondet = b.RelationshipType1.Relationship, crdate = d.Date_Time, relasiont = b.RelationshipType1.RTypeID };
                    var filist = lid.GroupBy(c => new { c.pdids }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.crdate);
                    var pageNumber = page ?? 1;
                    onePageOfProducts = filist.ToPagedList(pageNumber, 10);
                }
                else
                {
                    DataTable oDataSet4 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();

                    sqlQuery = "   SELECT  (a.Present_Complain)pcomoplian,b.ServiceStatus, COALESCE(NULLIF((b.Surname ), ''), c.Initials+' '+c.Surname)  	sname  " +
 "  ,(b.Rank)rnkname, " +
 "  (c.ServiceNo)sno    ,(b.Initials)inililes, (c.RelationshipType)relasiont " +
"   , (c.pid)pidp, (a.pdid)pdids,(a.status)pstatus,(a.CreatedDate)crdate, (h.Relationship)relasiondet,c.Service_Type FROM[MMS] " +
" .[dbo].[Patient_Detail] as a with(nolock)   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
 "  as b on c.ServiceNo=b.ServiceNo and c.Service_Type= b.ServiceType left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID " +
 "  inner join Drug_Prescription as i on i.PDID = a.PDID " +
" left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType where    RequestedLocID='" + locid + "' and   Issued= 0 " +

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





                    //var drugdes = from d in db.Drug_Prescription.Where(p => p.RequestedLocID == locid).Where(p => p.Issued == 0)
                    //              join e in db.Patient_Detail on d.PDID equals e.PDID
                    //                                                     into se
                    //              from e in se.DefaultIfEmpty()
                    //              join b in db.Patients on e.PID equals b.PID
                    //                       into sc
                    //              from b in sc.DefaultIfEmpty()
                    //              orderby d.Date_Time descending

                    //select new getdocdetail { sv = b.Service_Type, pdids = d.PDID, inililes = b.Initials, sname = b.Surname, sno = b.ServiceNo, rnkname = b.rank1.RNK_NAME, pidp = e.PID, relasiondet = b.RelationshipType1.Relationship, crdate = d.Date_Time, relasiont = b.RelationshipType1.RTypeID };
                    var filist = lid.GroupBy(c => new { c.pdids }).Select(group => group.FirstOrDefault()).OrderByDescending(p => p.crdate);

                    var pageNumber = page ?? 1;
                    onePageOfProducts = filist.ToPagedList(pageNumber, 10);
                }



                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View();
            }
            else
            {
                return RedirectToAction("login", "Users");
            }
        }

        // POST: Drug_Prescription/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ps_Index,PDID,ItemNo,Dose,Route,Method,Duration,LocID,Issued,Date_Time,PrescribeBy,GivenBy,IssuedLocID,RequestedLocID")] Drug_Prescription drug_Prescription)
        {
            if (ModelState.IsValid)
            {
                db.Drug_Prescription.Add(drug_Prescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Method = new SelectList(db.DrugMethods, "MethodID", "MethodDetail", drug_Prescription.Method);
            ViewBag.Route = new SelectList(db.DrugRoutes, "RouteID", "RouteDetail", drug_Prescription.Route);
            ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID", drug_Prescription.PDID);
            return View(drug_Prescription);
        }

        // GET: Drug_Prescription/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drug_Prescription drug_Prescription = db.Drug_Prescription.Find(id);
            if (drug_Prescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.Method = new SelectList(db.DrugMethods, "MethodID", "MethodDetail", drug_Prescription.Method);
            ViewBag.Route = new SelectList(db.DrugRoutes, "RouteID", "RouteDetail", drug_Prescription.Route);
            ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID", drug_Prescription.PDID);
            return View(drug_Prescription);
        }

        // POST: Drug_Prescription/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ps_Index,PDID,ItemNo,Dose,Route,Method,Duration,LocID,Issued,Date_Time,PrescribeBy,GivenBy,IssuedLocID,RequestedLocID")] Drug_Prescription drug_Prescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drug_Prescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Method = new SelectList(db.DrugMethods, "MethodID", "MethodDetail", drug_Prescription.Method);
            ViewBag.Route = new SelectList(db.DrugRoutes, "RouteID", "RouteDetail", drug_Prescription.Route);
            ViewBag.PDID = new SelectList(db.Patient_Detail, "PDID", "PID", drug_Prescription.PDID);
            return View(drug_Prescription);
        }

        // GET: Drug_Prescription/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drug_Prescription drug_Prescription = db.Drug_Prescription.Find(id);
            if (drug_Prescription == null)
            {
                return HttpNotFound();
            }
            return View(drug_Prescription);
        }

        // POST: Drug_Prescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Drug_Prescription drug_Prescription = db.Drug_Prescription.Find(id);
            db.Drug_Prescription.Remove(drug_Prescription);
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
