using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MMS;
using System.Threading.Tasks;


using MMS.Models;
using PagedList;
using System.Data.SqlClient;

namespace MMS.Controllers
{
    public class DrugItems1Controller : Controller
    {
        private MMSEntities db = new MMSEntities();
        //private P3Context dbhrms = new P3Context();
        //private EPASContext db = new EPASContext();
        // GET: DrugItems1
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        string sqlQuery;
        public  ActionResult Index(int? page)
        {
            
            var patient_Detail = db.DrugItems;
            return View();
           


        }
        public ActionResult rpclist(int? page)
        {

            var patient_Detail = db.DrugItems;
            return View();



        }
        public JsonResult Getepasdrug(String id)
        {
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            try
            {
                var GetDrug = db.MedicalItemDetailSnMDs.Where(x => x.RequisitionAllocationNo == id).Select(x => new { x.DOQ, x.ItemDescription, x.IssueDate, x.QtyIssueByGroup }).ToList();
                return Json(GetDrug, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult Getepasrpcdrug(String id)
        {
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            try
            {

                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT * FROM [MMS].[dbo].[Vw_epasrpcitems] where [RequisitionAllocationNo]= '" + id + "'";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var GetDrug = oDataSetv7.AsEnumerable()
     .Select(dataRow => new MedicalItemDetailSnMD
     {


         DOQ = dataRow.Field<string>("DOQ").ToString(),
         ItemDescription = dataRow.Field<string>("ItemDescription"),
         IssueDate = dataRow.Field<string>("IssueDate"),
         QtyIssueByGroup =  dataRow.Field<decimal?>("QtyIssueByGroup"),

     }).ToList();




             //   var GetDrug = db.MedicalItemDetailSnMDs.Where(x => x.RequisitionAllocationNo == id).Select(x => new { x.DOQ, x.ItemDescription, x.IssueDate, x.QtyIssueByGroup }).ToList();
                return Json(GetDrug, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }




        public JsonResult savepdrug(String id, String epdr2)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            double id1 = 0;
            int userid = Convert.ToInt32(Session["UserID"]);
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
              
            }
            if (!String.IsNullOrEmpty(epdr2))
            {
                epdr2 = epdr2.Trim(MyChar);
            }
            opdid = (String)Session["userlocid1"];
            if (opdid != null)
            {
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
                foreach (var item in clincd)
                {

                    locid = item.LocationID;
                }
                var GetDrug = db.EPASPharmacyItems.Where(x => x.itemno == id).Select(x => new { x.itemno, x.itemdescription }).ToList();

                int objcount = GetDrug.Count;
                DrugStockMaster[] oLab_Reports1 = new DrugStockMaster[objcount];
                DrugStockMaster[] oLab_Reports2 = new DrugStockMaster[objcount];
                DrugStockTransection[] oLab_Reports3 = new DrugStockTransection[objcount];
                int i = 0;
                int i1 = 0;
                string labtid = "";


                foreach (var item in GetDrug)
                {
                    var isupdat = from v in db.DrugStockMasters where (v.ItemID == item.itemno.ToString()) && (v.StoreID == opdid) select new { v.ItemIndex, v.ItemID, v.LOC, v.MFD, v.StoreID, v.DrugQuantity };
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
                                oLab_Report.StoreID = item1.StoreID;
                                oLab_Report.DrugQuantity = (Convert.ToDecimal(item1.DrugQuantity) + Convert.ToDecimal(epdr2)).ToString();


                                oLab_Reports1[i1] = oLab_Report;
                                DrugStockTransection oLab_Reportn = new DrugStockTransection();
                                oLab_Reportn.ItemID = item1.ItemID;
                                oLab_Reportn.IssuedDate = DateTime.Now;
                                oLab_Reportn.IssuedTo = locid;
                                oLab_Reportn.IssuedUser = userid;
                                oLab_Reportn.OutLoc = "SnMD";
                                oLab_Reportn.InLoc = opdid;
                                oLab_Reportn.TransectionQty = Convert.ToDecimal(epdr2);


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
                        oLab_Report.ItemID = item.itemno.ToString();
                        oLab_Report.ItemIndex = Guid.NewGuid().ToString();
                        oLab_Report.LOC = locid;
                        oLab_Report.MFD = DateTime.Now;
                        oLab_Report.StoreID = opdid;
                        oLab_Report.DrugQuantity = epdr2;


                        oLab_Reports2[i] = oLab_Report;

                        DrugStockTransection oLab_Reportn = new DrugStockTransection();
                        oLab_Reportn.ItemID = item.itemno.ToString();
                        oLab_Reportn.IssuedDate = DateTime.Now;
                        oLab_Reportn.IssuedTo = locid;
                        oLab_Reportn.IssuedUser = userid;
                        oLab_Reportn.OutLoc = "SnMD";
                        oLab_Reportn.InLoc = opdid;
                        oLab_Reportn.TransectionQty = Convert.ToDecimal(epdr2);


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



            return Json("Saved!", JsonRequestBehavior.AllowGet);

        }
        public JsonResult savetrnsdrug(String id, String drugtrqt, String Clinic_ID)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            double id1 = 0;
            int userid = Convert.ToInt32(Session["UserID"]);
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
                //id1 = Convert.ToDouble(id);
            }
            if (!String.IsNullOrEmpty(drugtrqt))
            {
                drugtrqt = drugtrqt.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(Clinic_ID))
            {
                Clinic_ID = Clinic_ID.Trim(MyChar);
            }
            opdid = (String)Session["userlocid1"];
            if (opdid != null)
            {
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
                foreach (var item in clincd)
                {

                    locid = item.LocationID;
                }
                var GetDrug = from v in db.DrugStockMasters where (v.ItemID == id) && (v.StoreID == opdid) select new { v.ItemIndex, v.ItemID, v.LOC, v.MFD, v.StoreID, v.DrugQuantity };

                int objcount = GetDrug.Count();
                DrugStockMaster[] oLab_Reports1 = new DrugStockMaster[objcount];
                DrugStockMaster[] oLab_Reports2 = new DrugStockMaster[objcount];
                DrugStockMaster[] oLab_Reports4 = new DrugStockMaster[objcount];
                DrugStockTransection[] oLab_Reports3 = new DrugStockTransection[objcount];
                int i = 0;
                int i1 = 0;
                string labtid = "";


                foreach (var item in GetDrug)
                {
                    var isupdat = from v in db.DrugStockMasters where (v.ItemID == id) && (v.StoreID == Clinic_ID) select new { v.ItemIndex, v.ItemID, v.LOC, v.MFD, v.StoreID, v.DrugQuantity };
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
                                oLab_Report.DrugQuantity = (Convert.ToDecimal(item1.DrugQuantity) + Convert.ToDecimal(drugtrqt)).ToString();


                                oLab_Reports1[i1] = oLab_Report;

                                DrugStockMaster oLab_Report2 = new DrugStockMaster();
                                oLab_Report2.ItemID = item1.ItemID;
                                oLab_Report2.ItemIndex = item.ItemIndex;
                                oLab_Report2.LOC = item1.LOC;
                                oLab_Report2.MFD = item1.MFD;
                                oLab_Report2.StoreID = item.StoreID;
                                oLab_Report2.DrugQuantity = (Convert.ToDecimal(item.DrugQuantity) - Convert.ToDecimal(drugtrqt)).ToString();


                                oLab_Reports4[i1] = oLab_Report2;


                                DrugStockTransection oLab_Reportn = new DrugStockTransection();
                                oLab_Reportn.ItemID = item1.ItemID;
                                oLab_Reportn.IssuedDate = DateTime.Now;
                                oLab_Reportn.IssuedTo = locid;
                                oLab_Reportn.IssuedUser = userid;
                                oLab_Reportn.OutLoc = opdid;
                                oLab_Reportn.InLoc = Clinic_ID;
                                oLab_Reportn.TransectionQty = Convert.ToDecimal(drugtrqt);


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
                        oLab_Report.DrugQuantity = drugtrqt;


                        oLab_Reports2[i] = oLab_Report;

                        DrugStockMaster oLab_Report2 = new DrugStockMaster();
                        oLab_Report2.ItemID = item.ItemID;
                        oLab_Report2.ItemIndex = item.ItemIndex;
                        oLab_Report2.LOC = item.LOC;
                        oLab_Report2.MFD = item.MFD;
                        oLab_Report2.StoreID = item.StoreID;
                        oLab_Report2.DrugQuantity = (Convert.ToDecimal(item.DrugQuantity) - Convert.ToDecimal(drugtrqt)).ToString();


                        oLab_Reports4[i] = oLab_Report2;


                        DrugStockTransection oLab_Reportn = new DrugStockTransection();
                        oLab_Reportn.ItemID = item.ItemID.ToString();
                        oLab_Reportn.IssuedDate = DateTime.Now;
                        oLab_Reportn.IssuedTo = locid;
                        oLab_Reportn.IssuedUser = userid;
                        oLab_Reportn.OutLoc = opdid;
                        oLab_Reportn.InLoc = Clinic_ID;
                        oLab_Reportn.TransectionQty = Convert.ToDecimal(drugtrqt);


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



            return Json("Saved!", JsonRequestBehavior.AllowGet);

        }
        public JsonResult saverpcdrug(String id, String rpcdr2)
        {

            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            double id1 = 0;
            int userid = Convert.ToInt32(Session["UserID"]);
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
                 id1= Convert.ToDouble(id); 
            }
            if (!String.IsNullOrEmpty(rpcdr2))
            {
                rpcdr2 = rpcdr2.Trim(MyChar);
            }
            opdid = (String)Session["userlocid1"];
            if (opdid != null)
            {
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
                foreach (var item in clincd)
                {

                    locid = item.LocationID;
                }
                var GetDrug = db.DrugItems.Where(x => x.DrugID == id1).Select(x => new { x.DrugID,x.ItemDescription }).ToList();

                int objcount = GetDrug.Count;
                DrugStockMaster[] oLab_Reports1 = new DrugStockMaster[objcount];
                DrugStockMaster[] oLab_Reports2 = new DrugStockMaster[objcount];
                DrugStockTransection[] oLab_Reports3 = new DrugStockTransection[objcount];
                int i = 0;
                int i1 = 0;
                string labtid = "";


                foreach (var item in GetDrug)
                {
                    var isupdat = from v in db.DrugStockMasters where (v.ItemID == item.DrugID.ToString()) && (v.StoreID == opdid) select new { v.ItemIndex, v.ItemID, v.LOC, v.MFD, v.StoreID, v.DrugQuantity };
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
                                oLab_Report.StoreID = item1.StoreID;
                                oLab_Report.DrugQuantity = (Convert.ToDecimal(item1.DrugQuantity) + Convert.ToDecimal(rpcdr2)).ToString() ;


                                oLab_Reports1[i1] = oLab_Report;
                                DrugStockTransection oLab_Reportn = new DrugStockTransection();
                                oLab_Reportn.ItemID = item1.ItemID;
                                oLab_Reportn.IssuedDate = DateTime.Now;
                                oLab_Reportn.IssuedTo = locid;
                                oLab_Reportn.IssuedUser = userid;
                                oLab_Reportn.OutLoc = "RPC";
                                oLab_Reportn.InLoc = opdid;
                                oLab_Reportn.TransectionQty = Convert.ToDecimal(rpcdr2);


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
                        oLab_Report.ItemID = item.DrugID.ToString();
                        oLab_Report.ItemIndex = Guid.NewGuid().ToString();
                        oLab_Report.LOC = locid;
                        oLab_Report.MFD = DateTime.Now;
                        oLab_Report.StoreID = opdid;
                        oLab_Report.DrugQuantity = rpcdr2;


                        oLab_Reports2[i] = oLab_Report;

                        DrugStockTransection oLab_Reportn = new DrugStockTransection();
                        oLab_Reportn.ItemID = item.DrugID.ToString();
                        oLab_Reportn.IssuedDate = DateTime.Now;
                        oLab_Reportn.IssuedTo = locid;
                        oLab_Reportn.IssuedUser = userid;
                        oLab_Reportn.OutLoc = "RPC";
                        oLab_Reportn.InLoc = opdid;
                        oLab_Reportn.TransectionQty = Convert.ToDecimal(rpcdr2);


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



            return Json("Saved!", JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getrpcdrug()
        {
            var GetDrug = db.DrugItems.Select(x => new { x.DrugID, x.ItemDescription }).ToList();
            return Json(GetDrug, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getdrugonr(string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            int di = 0;
            try
            {
              di=   Convert.ToInt32(id);
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
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
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
            var serc = from s in db.DrugStockMasters.Where(p => p.StoreID == opdid).Where(p => p.ItemID == id).Where(p => p.LOC == locid) orderby s.ItemID select new { s.ItemID, s.ItemIndex, s.LOC, s.MFD, s.StoreID, s.DrugQuantity, s.ExpireDate };
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

                ij++;
            }
            var t1 = serc.ToList();
            var t2 = items.ToList();
            var t3 = items5.ToList();
            var joined = from it1 in t1 join it2 in t2 on it1.ItemID equals it2.itemno select new getdrugdata { ItemID = it1.ItemID, LOC = it1.LOC, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.itemdescription };

            var joined1 = from it1 in t1 join it2 in t3 on it1.ItemID equals it2.DrugID.ToString() select new getdrugdata { ItemID = it1.ItemID, LOC = it1.LOC, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.ItemDescription };
            var u1 = joined.ToList();
            var u2 = joined1.ToList();
            var joined3 = u1.Concat(u2);
            return Json(joined3, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getdrugalldep(string id)
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
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
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

        public JsonResult Getdrugalldeploc(string id,string id1)
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
            if (!String.IsNullOrEmpty(id1))
            {
                id1 = id1.Trim(MyChar);
            }
            opdid = (String)Session["userlocid1"];
            if (opdid != null)
            {
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
                foreach (var item in clincd)
                {

                    //locid = item.LocationID;
                }
            }
            locid = id1;
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
        public JsonResult Getdrugtrans()
        {
            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            int di = 0;
           
            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            char[] MyChar = { '/', '"', ' ' };
          

            opdid = (String)Session["userlocid1"];
            if (opdid != null)
            {
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
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
            DataTable oDataSetv7 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "    SELECT a.ItemID,a.ItemIndex,COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'') "+
"   as itemdescription,a.LOC  ,a.MFD,a.StoreID ,a.DrugQuantity ,a.ExpireDate "+
  "  FROM[MMS].[dbo].[DrugStockMaster] as a left join[MMS].[dbo].[DrugItems] as d on "+
 "   a.ItemID=Convert(varchar, d.DrugID)     left join[MMS].[dbo].[EPASPharmacyItems] as e on a.ItemID=Convert(varchar, "+
" e.[itemno]) where a.StoreID='"+opdid+"' and a.LOC='"+locid+"'    order by a.ItemID";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;
            //   oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetv7);
            // oSqlConnection.Close();
           
              var  joined3 = oDataSetv7.AsEnumerable()
   .Select(dataRow => new getdrugdata
   {

       ItemID = dataRow.Field<string>("ItemID"),
       LOC = dataRow.Field<string>("LOC"),
       StoreID = dataRow.Field<string>("StoreID"),
       DrugQuantity = dataRow.Field<string>("DrugQuantity"),
       itemdescription = dataRow.Field<string>("itemdescription"),
      

   }).ToList();
            


            ///////////////////////////////////////////////////////////
            //var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
            //var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
            //var serc = from s in db.DrugStockMasters.Where(p => p.StoreID == opdid).Where(p => p.LOC == locid) orderby s.ItemID select new { s.ItemID, s.ItemIndex, s.LOC, s.MFD, s.StoreID, s.DrugQuantity, s.ExpireDate };
            //int ij = 0;

            //foreach (var itm in serc)
            //{


            //    var items2 = from d in db.EPASPharmacyItems.Where(p => p.itemno == itm.ItemID) select new { d.itemdescription, d.itemno };
            //    if (items2.Count() > 0)
            //    {
            //        if (ij != 0)
            //        {
            //            items = items2.Concat(items);

            //        }
            //        else
            //        {

            //            items = items2;
            //        }
            //    }
            //    else
            //    {
            //        var items3 = from d in db.DrugItems.Where(p => p.DrugID.ToString() == itm.ItemID) select new { d.ItemDescription, d.DrugID };
            //        if (ij != 0)
            //        {
            //            items5 = items3.Concat(items5);

            //        }
            //        else
            //        {

            //            items5 = items3;
            //        }

            //    }

            //    ij++;
            //}
            //var t1 = serc.ToList();
            //var t2 = items?.ToList();
            //var t3 = items5.ToList();
            //var joined = from it1 in t1 join it2 in t2 on it1.ItemID equals it2.itemno select new getdrugdata { ItemID = it1.ItemID, LOC = it1.LOC, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.itemdescription };

            //var joined1 = from it1 in t1 join it2 in t3 on it1.ItemID equals it2.DrugID.ToString() select new getdrugdata { ItemID = it1.ItemID, LOC = it1.LOC, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.ItemDescription };
            //var u1 = joined.ToList();
            //var u2 = joined1.ToList();
            //var joined3 = u1.Concat(u2);
            return Json(joined3, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JsonResult Getepdrug()
        {
            var GetDrug = db.EPASPharmacyItems.Select(x => new { x.itemno, x.itemdescription }).ToList();
            return Json(GetDrug, JsonRequestBehavior.AllowGet);

        }
        public JsonResult loaddrugdept()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            string opdid = "";
            string locid = "";
            int userid = Convert.ToInt32(Session["UserID"]);
            char[] MyChar = { '/', '"', ' ' };
           

            opdid = (String)Session["userlocid1"];
            if (opdid != null)
            {
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
                foreach (var item in clincd)
                {

                    locid = item.LocationID;
                }
            }
            var GetDrug = db.Clinic_Master.Where(x=>x.LocationID == locid).Where(x => x.ClinicTypeID == 22).Select(x => new { x.Clinic_ID, x.Clinic_Detail }).ToList();
            return Json(GetDrug, JsonRequestBehavior.AllowGet);

        }
        public JsonResult savepasdrug(String id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
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
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
                foreach (var item in clincd)
                {

                    locid = item.LocationID;
                }
                var GetDrug = db.MedicalItemDetailSnMDs.Where(x => x.RequisitionAllocationNo == id).Select(x => new { x.DOQ, x.ItemDescription, x.IssueDate, x.QtyIssueByGroup, x.ItemNo, x.LocationID }).ToList();

                int objcount = GetDrug.Count;
                DrugStockMaster[] oLab_Reports1 = new DrugStockMaster[objcount];
                DrugStockMaster[] oLab_Reports2 = new DrugStockMaster[objcount];
                DrugStockTransection[] oLab_Reports3 = new DrugStockTransection[objcount];
                int i = 0;
                int i1 = 0;
                string labtid = "";


                foreach (var item in GetDrug)
                {
                    var isupdat = from v in db.DrugStockMasters where (v.ItemID == item.ItemNo) && (v.StoreID == opdid) select new { v.ItemIndex, v.ItemID, v.LOC, v.MFD, v.StoreID, v.DrugQuantity };
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
                                oLab_Report.StoreID = item1.StoreID;
                                oLab_Report.DrugQuantity = (Convert.ToDecimal(item1.DrugQuantity) + Convert.ToDecimal(item.QtyIssueByGroup)).ToString();


                                oLab_Reports1[i1] = oLab_Report;
                                DrugStockTransection oLab_Reportn = new DrugStockTransection();
                                oLab_Reportn.ItemID = item1.ItemID;
                                oLab_Reportn.IssuedDate = DateTime.Now;
                                oLab_Reportn.IssuedTo = locid;
                                oLab_Reportn.IssuedUser = userid;
                                oLab_Reportn.StockID = opdid;
                                oLab_Reportn.TransectionQty = Convert.ToDecimal(item.QtyIssueByGroup);


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
                        oLab_Report.ItemID = item.ItemNo;
                        oLab_Report.ItemIndex = Guid.NewGuid().ToString();
                        oLab_Report.LOC = locid;
                        oLab_Report.MFD = DateTime.Now;
                        oLab_Report.StoreID = opdid;
                        oLab_Report.DrugQuantity = item.QtyIssueByGroup.ToString();


                        oLab_Reports2[i] = oLab_Report;
                       
                        DrugStockTransection oLab_Reportn = new DrugStockTransection();
                        oLab_Reportn.ItemID = item.ItemNo;
                        oLab_Reportn.IssuedDate = DateTime.Now;
                        oLab_Reportn.IssuedTo = locid;
                        oLab_Reportn.IssuedUser = userid;
                        oLab_Reportn.StockID = opdid;
                        oLab_Reportn.TransectionQty =Convert.ToDecimal( item.QtyIssueByGroup);


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



            return Json("Saved!", JsonRequestBehavior.AllowGet);

        }

        public JsonResult savepasrpcdrug(String id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
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
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
                foreach (var item in clincd)
                {

                    locid = item.LocationID;
                }


                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT * FROM [MMS].[dbo].[Vw_epasrpcitems] where [RequisitionAllocationNo]= '" + id + "'";
                // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var GetDrug = oDataSetv7.AsEnumerable()
     .Select(dataRow => new MedicalItemDetailSnMD
     {


         DOQ = dataRow.Field<string>("DOQ").ToString(),
         ItemDescription = dataRow.Field<string>("ItemDescription"),
         IssueDate = dataRow.Field<string>("IssueDate"),
         QtyIssueByGroup = dataRow.Field<decimal?>("QtyIssueByGroup"),
         ItemNo = dataRow.Field<string>("ItemNo"),
         LocationID = dataRow.Field<string>("LocationID"),

     }).ToList();



               // var GetDrug = db.MedicalItemDetailSnMDs.Where(x => x.RequisitionAllocationNo == id).Select(x => new { x.DOQ, x.ItemDescription, x.IssueDate, x.QtyIssueByGroup, x.ItemNo, x.LocationID }).ToList();

                int objcount = GetDrug.Count;
                DrugStockMaster[] oLab_Reports1 = new DrugStockMaster[objcount];
                DrugStockMaster[] oLab_Reports2 = new DrugStockMaster[objcount];
                DrugStockTransection[] oLab_Reports3 = new DrugStockTransection[objcount];
                int i = 0;
                int i1 = 0;
                string labtid = "";


                foreach (var item in GetDrug)
                {
                    var isupdat = from v in db.DrugStockMasters where (v.ItemID == item.ItemNo) && (v.StoreID == opdid) select new { v.ItemIndex, v.ItemID, v.LOC, v.MFD, v.StoreID, v.DrugQuantity };
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
                                oLab_Report.StoreID = item1.StoreID;
                                oLab_Report.DrugQuantity = (Convert.ToDecimal(item1.DrugQuantity) + Convert.ToDecimal(item.QtyIssueByGroup)).ToString();


                                oLab_Reports1[i1] = oLab_Report;
                                DrugStockTransection oLab_Reportn = new DrugStockTransection();
                                oLab_Reportn.ItemID = item1.ItemID;
                                oLab_Reportn.IssuedDate = DateTime.Now;
                                oLab_Reportn.IssuedTo = locid;
                                oLab_Reportn.IssuedUser = userid;
                                oLab_Reportn.StockID = opdid;
                                oLab_Reportn.TransectionQty = Convert.ToDecimal(item.QtyIssueByGroup);


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
                        oLab_Report.ItemID = item.ItemNo;
                        oLab_Report.ItemIndex = Guid.NewGuid().ToString();
                        oLab_Report.LOC = locid;
                        oLab_Report.MFD = DateTime.Now;
                        oLab_Report.StoreID = opdid;
                        oLab_Report.DrugQuantity = item.QtyIssueByGroup.ToString();


                        oLab_Reports2[i] = oLab_Report;

                        DrugStockTransection oLab_Reportn = new DrugStockTransection();
                        oLab_Reportn.ItemID = item.ItemNo;
                        oLab_Reportn.IssuedDate = DateTime.Now;
                        oLab_Reportn.IssuedTo = locid;
                        oLab_Reportn.IssuedUser = userid;
                        oLab_Reportn.StockID = opdid;
                        oLab_Reportn.TransectionQty = Convert.ToDecimal(item.QtyIssueByGroup);


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



            return Json("Saved!", JsonRequestBehavior.AllowGet);

        }



        // GET: DrugItems1/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugItem drugItem = db.DrugItems.Find(id);
            if (drugItem == null)
            {
                return HttpNotFound();
            }
            return View(drugItem);
        }

        // GET: DrugItems1/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: DrugItems1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,ItemShortDescription,ItemDescription,UOF,TypeOfForm,DrugGroup,StorageCodition,Status,Remarks,StockQuantity,LocationID,DrugID")] DrugItem drugItem)
        {
            if (ModelState.IsValid)
            {
                db.DrugItems.Add(drugItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drugItem);
        }

        // GET: DrugItems1/Edit/5
        public ActionResult Edit(int? page,string id)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
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
                var clincd = from v in db.Clinic_Master where (v.Clinic_ID == opdid) select new { v.LocationID };
                foreach (var item in clincd)
                {

                    locid = item.LocationID;
                }
            }
            var onePageOfProducts = (dynamic)null;
            if (!String.IsNullOrEmpty(id))
            {
                var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
                var serc = from s in db.DrugStockMasters.Where(p => p.StoreID == opdid).Where(p => p.LOC == locid) orderby s.ItemID select new { s.ItemID, s.ItemIndex, s.LOC, s.MFD, s.StoreID, s.DrugQuantity, s.ExpireDate };
                int ij = 0;

                foreach (var itm in serc)
                {


                    var items2 = from d in db.EPASPharmacyItems.Where(p => p.itemno == itm.ItemID).Where(p => p.itemdescription.Contains(id)) select new { d.itemdescription, d.itemno };
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
                        var items3 = from d in db.DrugItems.Where(p => p.DrugID.ToString() == itm.ItemID).Where(p => p.ItemDescription.Contains(id)) select new { d.ItemDescription, d.DrugID };
                        if (ij != 0)
                        {
                            items5 = items3.Concat(items5);

                        }
                        else
                        {

                            items5 = items3;
                        }

                    }

                    ij++;
                }
                var t1 = serc.ToList();
                var t2 = items.ToList();
                var t3 = items5.ToList();
                var joined = from it1 in t1 join it2 in t2 on it1.ItemID equals it2.itemno select new getdrugdata { ItemID = it1.ItemID, LOC = it1.LOC, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.itemdescription };

                var joined1 = from it1 in t1 join it2 in t3 on it1.ItemID equals it2.DrugID.ToString() select new getdrugdata { ItemID = it1.ItemID, LOC = it1.LOC, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.ItemDescription };
                var u1 = joined.ToList();
                var u2 = joined1.ToList();
                var joined3 = u1.Concat(u2);
                //from it1 in u1 join it2 in u2 on it1.ItemID equals it2.ItemID select new { ItemID = it1.ItemID, LOC = it1.LOC, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.itemdescription };
                //var a5 = from s in db.DrugStockMasters.Where(p => p.PID == id)
                //         join b in db.Clinic_Master on s.OPDID equals b.Clinic_ID into cs
                //         from b in cs.DefaultIfEmpty()
                //         join c in db.CatDaignosis on s.DaignosisID equals c.dgid into com
                //         from c in com.DefaultIfEmpty()

                //         select new { s.PDID, s.CreatedDate, b.Clinic_Detail, c.dgdetail };



                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToArray().ToPagedList(pageNumber, 10);

            }
            else
            {
                var items = from d in db.EPASPharmacyItems.Where(p => p.itemno == "SLAF6501003010") select new { d.itemdescription, d.itemno };
                var items5 = from d in db.DrugItems.Where(p => p.DrugID == 603) select new { d.ItemDescription, d.DrugID };
                var serc = from s in db.DrugStockMasters.Where(p => p.StoreID == opdid).Where(p => p.LOC == locid) orderby s.ItemID select new { s.ItemID, s.ItemIndex, s.LOC, s.MFD, s.StoreID, s.DrugQuantity, s.ExpireDate };
                int ij = 0;

                foreach (var itm in serc)
                {


                    var items2 = from d in db.EPASPharmacyItems.Where(p => p.itemno == itm.ItemID) select new { d.itemdescription, d.itemno };
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
                        var items3 = from d in db.DrugItems.Where(p => p.DrugID.ToString() == itm.ItemID) select new { d.ItemDescription, d.DrugID };
                        if (ij != 0)
                        {
                            items5 = items3.Concat(items5);

                        }
                        else
                        {

                            items5 = items3;
                        }

                    }

                    ij++;
                }
                var t1 = serc.ToList();
                var t2 = items.ToList();
                var t3 = items5.ToList();
                var joined = from it1 in t1 join it2 in t2 on it1.ItemID equals it2.itemno select new getdrugdata { ItemID = it1.ItemID, LOC = it1.LOC, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.itemdescription };

                var joined1 = from it1 in t1 join it2 in t3 on it1.ItemID equals it2.DrugID.ToString() select new getdrugdata { ItemID = it1.ItemID, LOC = it1.LOC, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.ItemDescription };
                var u1 = joined.ToList();
                var u2 = joined1.ToList();
                var joined3 = u1.Concat(u2);
                //from it1 in u1 join it2 in u2 on it1.ItemID equals it2.ItemID select new { ItemID = it1.ItemID, LOC = it1.LOC, StoreID = it1.StoreID, DrugQuantity = it1.DrugQuantity, itemdescription = it2.itemdescription };
                //var a5 = from s in db.DrugStockMasters.Where(p => p.PID == id)
                //         join b in db.Clinic_Master on s.OPDID equals b.Clinic_ID into cs
                //         from b in cs.DefaultIfEmpty()
                //         join c in db.CatDaignosis on s.DaignosisID equals c.dgid into com
                //         from c in com.DefaultIfEmpty()

                //         select new { s.PDID, s.CreatedDate, b.Clinic_Detail, c.dgdetail };



                var pageNumber = page ?? 1;
                onePageOfProducts = joined3.ToArray().ToPagedList(pageNumber, 10);
            }

           
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }

        // POST: DrugItems1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,ItemShortDescription,ItemDescription,UOF,TypeOfForm,DrugGroup,StorageCodition,Status,Remarks,StockQuantity,LocationID,DrugID")] DrugItem drugItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drugItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drugItem);
        }

        // GET: DrugItems1/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugItem drugItem = db.DrugItems.Find(id);
            if (drugItem == null)
            {
                return HttpNotFound();
            }
            return View(drugItem);
        }

        // POST: DrugItems1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DrugItem drugItem = db.DrugItems.Find(id);
            db.DrugItems.Remove(drugItem);
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
