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
    //[Authorize]
    public class HomeController : Controller
    {
        private MMSEntities db = new MMSEntities();
       
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        public ActionResult Index()
        {
            try
            {
                String loc = (String)Session["userlocid1"];
                String locid="";
                string spid = "";
                string cldet = "";
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                int userid = Convert.ToInt32(Session["UserID"]);
                DataTable oDataSetsp1 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
              string  sqlQuery = "    select * FROM [MMS].[dbo].[Staff_Master] where UserID='" + userid + "' and LOCID='"+ loc + "'   ";
             
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
                    spid = row["SpecialityID"].ToString();
                   
                }
                ////////////////////////////////////////
                 sqlQuery = "    select Clinic_Detail FROM [MMS].[dbo].[Clinic_Master] where Clinic_ID='" + loc + "'   ";

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
                    cldet = row["Clinic_Detail"].ToString();
                    locid = row["LocationID"].ToString();
                }
                Session["userlocid2"] = locid;
                ////////////////////////////////////////
                oSqlConnection.Close();
                Session["cldet"] = cldet;
                User us = GetCurrentUser();

                #region Load MenuItems

                List<MenuItem> all = new List<MenuItem>();
               
               int mid = 0;
                //int pmid = 0;
                if (userid == 10000008 || userid == 10000071)
                {

                    var obj = from sm in db.UserPermissions
                              join me in db.MenuItems on sm.MenuID equals me.MenuID

                              where sm.UserID == us.UserID
                              select new
                              {
                                  sm.MenuID,
                                  me.MenuName,
                                  me.MethodName,
                                  me.NavURL,
                                  me.ParentMenuID,
                                  me.ControllerName
                              };
                    foreach (var k in obj)
                    {
                        MenuItem sm = new MenuItem();
                        sm.MenuID = k.MenuID;
                        sm.MenuName = k.MenuName;
                        sm.MethodName = k.MethodName;
                        sm.NavURL = k.NavURL;
                        sm.ParentMenuID = k.ParentMenuID;
                        sm.ControllerName = k.ControllerName;
                        all.Add(sm);
                    }
                }
                else if (loc.Trim().ToLower().Contains("opd"))
                {
                    if (spid.Equals("27"))
                    {


                        mid = 2;
                        var obj = from sm in db.UserPermissions
                                  join me in db.MenuItems on sm.MenuID equals me.MenuID

                                  where sm.UserID == us.UserID && (me.ParentMenuID == mid || me.MenuID == mid || me.ParentMenuID == 7 || me.MenuID == 7 || me.ParentMenuID == 45 || me.MenuID == 45)
                                  select new
                                  {
                                      sm.MenuID,
                                      me.MenuName,
                                      me.MethodName,
                                      me.NavURL,
                                      me.ParentMenuID,
                                      me.ControllerName
                                  };
                        foreach (var k in obj)
                        {
                            MenuItem sm = new MenuItem();
                            sm.MenuID = k.MenuID;
                            sm.MenuName = k.MenuName;
                            sm.MethodName = k.MethodName;
                            sm.NavURL = k.NavURL;
                            sm.ParentMenuID = k.ParentMenuID;
                            sm.ControllerName = k.ControllerName;
                            all.Add(sm);
                        }

                    }
                    else if (spid.Equals("26"))
                    {


                        mid = 37;
                        var obj = from sm in db.UserPermissions
                                  join me in db.MenuItems on sm.MenuID equals me.MenuID

                                  where sm.UserID == us.UserID && (me.ParentMenuID == mid || me.MenuID == mid || me.ParentMenuID == 7 || me.MenuID == 7 || me.ParentMenuID == 45 || me.MenuID == 45)
                                  select new
                                  {
                                      sm.MenuID,
                                      me.MenuName,
                                      me.MethodName,
                                      me.NavURL,
                                      me.ParentMenuID,
                                      me.ControllerName
                                  };
                        foreach (var k in obj)
                        {
                            MenuItem sm = new MenuItem();
                            sm.MenuID = k.MenuID;
                            sm.MenuName = k.MenuName;
                            sm.MethodName = k.MethodName;
                            sm.NavURL = k.NavURL;
                            sm.ParentMenuID = k.ParentMenuID;
                            sm.ControllerName = k.ControllerName;
                            all.Add(sm);
                        }

                    }
                    else
                    {


                        mid = 45;
                        var obj = from sm in db.UserPermissions
                                  join me in db.MenuItems on sm.MenuID equals me.MenuID

                                  where sm.UserID == us.UserID && (me.ParentMenuID == mid || me.MenuID == mid || me.ParentMenuID == 7 || me.MenuID == 7)
                                  select new
                                  {
                                      sm.MenuID,
                                      me.MenuName,
                                      me.MethodName,
                                      me.NavURL,
                                      me.ParentMenuID,
                                      me.ControllerName
                                  };
                        foreach (var k in obj)
                        {
                            MenuItem sm = new MenuItem();
                            sm.MenuID = k.MenuID;
                            sm.MenuName = k.MenuName;
                            sm.MethodName = k.MethodName;
                            sm.NavURL = k.NavURL;
                            sm.ParentMenuID = k.ParentMenuID;
                            sm.ControllerName = k.ControllerName;
                            all.Add(sm);
                        }

                    }
                }
                else if (loc.Trim().ToLower().Contains("mcm"))
                {
                   


                        mid = 90;
                        var obj = from sm in db.UserPermissions
                                  join me in db.MenuItems on sm.MenuID equals me.MenuID

                                  where sm.UserID == us.UserID && (me.ParentMenuID == mid || me.MenuID == mid || me.ParentMenuID == 7 || me.MenuID == 7 )
                                  select new
                                  {
                                      sm.MenuID,
                                      me.MenuName,
                                      me.MethodName,
                                      me.NavURL,
                                      me.ParentMenuID,
                                      me.ControllerName
                                  };
                        foreach (var k in obj)
                        {
                            MenuItem sm = new MenuItem();
                            sm.MenuID = k.MenuID;
                            sm.MenuName = k.MenuName;
                            sm.MethodName = k.MethodName;
                            sm.NavURL = k.NavURL;
                            sm.ParentMenuID = k.ParentMenuID;
                            sm.ControllerName = k.ControllerName;
                            all.Add(sm);
                        }

                   
                }
                else if (loc.Trim().ToLower().Contains("ward"))
                {
                    if (spid.Equals("26"))
                    {


                        mid = 37;
                        var obj = from sm in db.UserPermissions
                                  join me in db.MenuItems on sm.MenuID equals me.MenuID

                                  where sm.UserID == us.UserID && (me.ParentMenuID == mid || me.MenuID == mid || me.ParentMenuID == 7 || me.MenuID == 7 || me.ParentMenuID == 88 || me.MenuID == 88)
                                  select new
                                  {
                                      sm.MenuID,
                                      me.MenuName,
                                      me.MethodName,
                                      me.NavURL,
                                      me.ParentMenuID,
                                      me.ControllerName
                                  };
                        foreach (var k in obj)
                        {
                            MenuItem sm = new MenuItem();
                            sm.MenuID = k.MenuID;
                            sm.MenuName = k.MenuName;
                            sm.MethodName = k.MethodName;
                            sm.NavURL = k.NavURL;
                            sm.ParentMenuID = k.ParentMenuID;
                            sm.ControllerName = k.ControllerName;
                            all.Add(sm);
                        }

                    }
                    else
                    {
                        mid = 88;
                        var obj = from sm in db.UserPermissions
                                  join me in db.MenuItems on sm.MenuID equals me.MenuID

                                  where sm.UserID == us.UserID && (me.ParentMenuID == mid || me.MenuID == mid || me.ParentMenuID == 7 || me.MenuID == 7)
                                  select new
                                  {
                                      sm.MenuID,
                                      me.MenuName,
                                      me.MethodName,
                                      me.NavURL,
                                      me.ParentMenuID,
                                      me.ControllerName
                                  };
                        foreach (var k in obj)
                        {
                            MenuItem sm = new MenuItem();
                            sm.MenuID = k.MenuID;
                            sm.MenuName = k.MenuName;
                            sm.MethodName = k.MethodName;
                            sm.NavURL = k.NavURL;
                            sm.ParentMenuID = k.ParentMenuID;
                            sm.ControllerName = k.ControllerName;
                            all.Add(sm);
                        }
                    }
                }
                else if (loc.Trim().ToLower().Contains("phy"))
                {
                    if (spid.Equals("22"))
                    {


                        mid = 40;
                        var obj = from sm in db.UserPermissions
                                  join me in db.MenuItems on sm.MenuID equals me.MenuID

                                  where sm.UserID == us.UserID && (me.ParentMenuID == mid || me.MenuID == mid || me.ParentMenuID == 7 || me.MenuID == 7 )
                                  select new
                                  {
                                      sm.MenuID,
                                      me.MenuName,
                                      me.MethodName,
                                      me.NavURL,
                                      me.ParentMenuID,
                                      me.ControllerName
                                  };
                        foreach (var k in obj)
                        {
                            MenuItem sm = new MenuItem();
                            sm.MenuID = k.MenuID;
                            sm.MenuName = k.MenuName;
                            sm.MethodName = k.MethodName;
                            sm.NavURL = k.NavURL;
                            sm.ParentMenuID = k.ParentMenuID;
                            sm.ControllerName = k.ControllerName;
                            all.Add(sm);
                        }

                    }
                    else
                    {
                        mid = 40;
                        var obj = from sm in db.UserPermissions
                                  join me in db.MenuItems on sm.MenuID equals me.MenuID

                                  where sm.UserID == us.UserID && (me.ParentMenuID == mid || me.MenuID == mid || me.ParentMenuID == 7 || me.MenuID == 7)
                                  select new
                                  {
                                      sm.MenuID,
                                      me.MenuName,
                                      me.MethodName,
                                      me.NavURL,
                                      me.ParentMenuID,
                                      me.ControllerName
                                  };
                        foreach (var k in obj)
                        {
                            MenuItem sm = new MenuItem();
                            sm.MenuID = k.MenuID;
                            sm.MenuName = k.MenuName;
                            sm.MethodName = k.MethodName;
                            sm.NavURL = k.NavURL;
                            sm.ParentMenuID = k.ParentMenuID;
                            sm.ControllerName = k.ControllerName;
                            all.Add(sm);
                        }
                    }
                }
                else
                {
                    var obj = from sm in db.UserPermissions
                              join me in db.MenuItems on sm.MenuID equals me.MenuID

                              where sm.UserID == us.UserID
                              select new
                              {
                                  sm.MenuID,
                                  me.MenuName,
                                  me.MethodName,
                                  me.NavURL,
                                  me.ParentMenuID,
                                  me.ControllerName
                              };
                    foreach (var k in obj)
                    {
                        MenuItem sm = new MenuItem();
                        sm.MenuID = k.MenuID;
                        sm.MenuName = k.MenuName;
                        sm.MethodName = k.MethodName;
                        sm.NavURL = k.NavURL;
                        sm.ParentMenuID = k.ParentMenuID;
                        sm.ControllerName = k.ControllerName;
                        all.Add(sm);
                    }
                }

               

                #endregion

                #region Display User Image

                byte[] imageByteData;
                //var ProfileImage = from s in dbhrms.ServicePersonnelProfiles
                //                   where s.ServiceNo == us.ServiceNo
                //                   select new { s.ProfilePicture, s.ServiceNo };

                ////foreach (var item in ProfileImage)
                //{
                //    imageByteData = item.ProfilePicture;
                //    string imageBase64Data = Convert.ToBase64String(imageByteData);
                //    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                //    ViewBag.ImageData = imageDataURL;

                //}
                ViewBag.Rank = us.Rank;
                #endregion

                #region User Details

                ViewBag.ProfileData = us.LName;
                ViewBag.Serviceno = us.ServiceNo;

                #endregion

             //   #region Display Dashboard Data
            //var locid=(dynamic)null;
            //    var loccount=(dynamic)null;
            //   String opdid = (String)Session["userlocid1"];
            //    var odailypatient1 = from s in db.Patient_Detail.Where(p => p.CreatedDate.Year==DateTime.Now.Year && p.CreatedDate.Month == DateTime.Now.Month && p.CreatedDate.Day == DateTime.Now.Day).Where(p=>p.PatientCatID==1)
            //                         join b in db.Clinic_Master on s.OPDID equals b.Clinic_ID   into cs 
            //              from b in cs.DefaultIfEmpty().Where(p => p.ClinicTypeID== 19)
            //                         group s by b.LocationID into g
            //              select new {locid=g.Key.ToString(),loccount=g.Count()};
            //     List<DashBoadModel> oDashBoadModel = new List<DashBoadModel>();

            //    DashBoadModel oDash = new DashBoadModel();

            //    foreach (var item in odailypatient1)
            //    {
            //        oDash = new DashBoadModel();
            //        oDash.locid = item.locid.ToString();
            //        oDash.loccount = item.loccount;
            //        oDashBoadModel.Add(oDash);

            //    }

            //    #endregion
            //    ViewBag.odailypatient = oDashBoadModel;
                return View(all);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Users");
                //throw;
            }

        }


        public JsonResult LineChartData(string id)
        {
            String[] mnt = id.Split('-');
            string locid = "";
            string opdid = "";
            opdid = (String)Session["userlocid1"];
            //var serW = from s in db.Clinic_Master.Where(p => p.LocationID == id1).Where(p => p.ClinicTypeID == 19) select new { s.Clinic_ID };

            //foreach (var item in serW)
            //{

            //    locid = item.Clinic_ID;
            //}
            if (opdid.ToLower().Trim().Contains("ward"))
            {
                List<Datasets> _dataSet = new List<Datasets>();
                int days = DateTime.DaysInMonth(Convert.ToInt32(mnt[0]), Convert.ToInt32(mnt[1]));
                string[] darr = new string[] { "AHP", "AMP", "BCL", "BIA", "CBO", "CBY", "DLA", "EKA", "HIN", "IRM", "KAT", "KGL", "KTK", "MGR", "MOW", "MTV", "PGL", "PLV", "KKS", "RMA", "SGR", "VNA", "WAN", "WLA" };
                string[] cdarr = new string[] { "AHP", "AMP", "BCL", "BIA", "CBO", "CBY", "DLA", "EKA", "HIN", "IRM", "KAT", "KGL", "KTK", "MGR", "MOW", "MTV", "PGL", "PLV", "PLY", "RMA", "SGR", "VNA", "VNI", "WLA" };
                List<string> datlt1 = new List<string>();
                List<decimal> cntlt1 = new List<decimal>();
               


                List<string> datlt = new List<string>();
                List<decimal> cntlt = new List<decimal>();
                try
                {
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DataSet oDataSet = new DataSet();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " ; WITH CTE as " +
    " ( " +
     " SELECT   CM.LocationID as t, count(wd.pdid) as r " +
    "   FROM dbo.Ward_Details WD  INNER JOIN dbo.Ward_Types WT ON WD.Ward_No = WT.ID " +
    " INNER JOIN dbo.Clinic_Master CM ON WD.OPDID = CM.Clinic_ID inner join [dbo].[Ward_Mgt_Plan] wm on wd.WDID=wm.PDID" +
    "   where     convert(date,   wm.[Date]) = CONVERT(varchar,'" + DateTime.Now.Date + "',111)" +
     " group by   CM.LocationID, wd.pdid " +
    "  )" +
    " SELECT t, count(*) r " +
    " FROM CTE " +
    " GROUP BY t order by t";
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
                        foreach (var row1 in darr)
                        {
                            foreach (DataRow row in oDataSet.Tables[0].Rows)
                            {
                                string dval = row["t"].ToString();

                                string sval = row1;
                                //darr[cnt].ToString();



                                if (sval == dval)
                                {
                                    cntlt.Add(Convert.ToInt32(row["r"].ToString()));
                                    cnt = 1;
                                    break;
                                }
                                else
                                {
                                    cnt = 0;
                                    // cntlt1.Add(0);

                                }

                            }

                            if (cnt != 1)
                            {
                                cntlt.Add(0);
                                cnt = 0;
                            }

                           
                        }
                    }

                }
                catch (Exception ex)
                {

                }



                decimal[] carr = cntlt.ToArray();
                Chart _chart = new Chart();
                _chart.labels = cdarr;
                _chart.datasets = new List<Datasets>();

                _dataSet.Add(new Datasets()
                {
                    label = "Ward Today Management Plan",
                    data = carr,
                    borderColor = new string[] { "rgba(255,0,0,1)" },
                    backgroundColor = new string[] { "rgba(0,0,0,0)" },
                    borderWidth = "3"
                });


                //////////////////////////////////////////////
                List<string> datlt2 = new List<string>();
                List<decimal> cntlt2 = new List<decimal>();
                try
                {
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DataSet oDataSet = new DataSet();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " ; WITH CTE as " +
    " ( " +
     " SELECT   CM.LocationID as t, count(wd.pdid) as r " +
    "   FROM dbo.Ward_Details WD  INNER JOIN dbo.Ward_Types WT ON WD.Ward_No = WT.ID " +
    " INNER JOIN dbo.Clinic_Master CM ON WD.OPDID = CM.Clinic_ID " +
    "   where     convert(date,  wd.[Created_Date]) = CONVERT(varchar,'" + DateTime.Now.Date + "',111)" +
     " group by   CM.LocationID, pdid " +
    "  )" +
    " SELECT t, count(*) r " +
    " FROM CTE " +
    " GROUP BY t order by t";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet);
                    oSqlConnection.Close();

                    if (oDataSet.Tables[0].Rows.Count > 0)
                    {
                        // int cnt = 0;
                        int cnt = 0;
                        foreach (var row1 in darr)
                        {
                            foreach (DataRow row in oDataSet.Tables[0].Rows)
                            {
                                string dval = row["t"].ToString();

                                string sval = row1;
                                //darr[cnt].ToString();



                                if (sval == dval)
                                {
                                    cntlt2.Add(Convert.ToInt32(row["r"].ToString()));
                                    cnt = 1;
                                    break;
                                }
                                else
                                {
                                    cnt = 0;
                                    // cntlt1.Add(0);

                                }

                            }

                            if (cnt != 1)
                            {
                                cntlt2.Add(0);
                                cnt = 0;
                            }


                            
                        }
                    }
                }
                catch (Exception ex)
                {

                }


                string[] darr2 = datlt2.ToArray();
                decimal[] carr2 = cntlt2.ToArray();




                _dataSet.Add(new Datasets()
                {
                    label = "Ward Today Admissions",
                    data = carr2,
                    borderColor = new string[] { "rgba(0,255,0,1)" },
                    backgroundColor = new string[] { "rgba(0,0,0,0)" },
                    borderWidth = "3"
                });
                //////////////////////////////////////////////
                List<string> datlt3 = new List<string>();
                List<decimal> cntlt3 = new List<decimal>();
                try
                {
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DataSet oDataSet = new DataSet();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " ; WITH CTE as " +
    " ( " +
     " SELECT   CM.LocationID as t, count(wd.pdid) as r " +
    "   FROM dbo.Ward_Details WD  INNER JOIN dbo.Ward_Types WT ON WD.Ward_No = WT.ID " +
    " INNER JOIN dbo.Clinic_Master CM ON WD.OPDID = CM.Clinic_ID " +
    "   where    wd.Status = 16 and  convert(date,  wd.[Created_Date]) = CONVERT(varchar,'" + DateTime.Now.Date + "',111)" +
     " group by   CM.LocationID, pdid " +
    "  )" +
    " SELECT t, count(*) r " +
    " FROM CTE " +
    " GROUP BY t order by t";
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
                        foreach (var row1 in darr)
                        {
                            foreach (DataRow row in oDataSet.Tables[0].Rows)
                            {
                                string dval = row["t"].ToString();

                                string sval = row1;
                                //darr[cnt].ToString();



                                if (sval == dval)
                                {
                                    cntlt3.Add(Convert.ToInt32(row["r"].ToString()));
                                    cnt = 1;
                                    break;
                                }
                                else
                                {
                                    cnt = 0;
                                    // cntlt1.Add(0);

                                }

                            }

                            if (cnt != 1)
                            {
                                cntlt3.Add(0);
                                cnt = 0;
                            }
                           
                        }
                    }
                }
                catch (Exception ex)
                {

                }


                string[] darr3 = datlt3.ToArray();
                decimal[] carr3 = cntlt3.ToArray();




                _dataSet.Add(new Datasets()
                {
                    label = "Ward Today Discharges",
                    data = carr3,
                    borderColor = new string[] { "rgba(255,0,255,1)" },
                    backgroundColor = new string[] { "rgba(0,0,0,0)" },
                    borderWidth = "3"
                });

                /////////////////////////////////////////////////
                ///
                //////////////////////////////////////////////
                List<string> datlt4 = new List<string>();
                List<decimal> cntlt4 = new List<decimal>();
                try
                {
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DataSet oDataSet = new DataSet();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "     ;WITH CTE as " +
    " ( " +
     " SELECT   CM.LocationID as t, count(wd.pdid) as r " +
    "   FROM dbo.Ward_Details WD  INNER JOIN dbo.Ward_Types WT ON WD.Ward_No = WT.ID " +
    " INNER JOIN dbo.Clinic_Master CM ON WD.OPDID = CM.Clinic_ID and cm.ClinicTypeID=20" +
    "   where    wd.Status = 15 " +
     " group by   CM.LocationID, pdid " +
    "  )" +
    " SELECT t, count(*) r " +
    " FROM CTE " +
    " GROUP BY t order by t";
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
                        foreach (var row1 in darr)
                        {
                            foreach (DataRow row in oDataSet.Tables[0].Rows)
                            {
                                string dval = row["t"].ToString();

                                string sval = row1;
                                //darr[cnt].ToString();



                                if (sval == dval)
                                {
                                    cntlt4.Add(Convert.ToInt32(row["r"].ToString()));
                                    cnt = 1;
                                    break;
                                }
                                else
                                {
                                    cnt = 0;
                                    // cntlt1.Add(0);

                                }

                            }

                            if (cnt != 1)
                            {
                                cntlt4.Add(0);
                                cnt = 0;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                }


                string[] darr4 = datlt4.ToArray();
                decimal[] carr4 = cntlt4.ToArray();




                _dataSet.Add(new Datasets()
                {
                    label = "WARD Total Admited Patients",
                    data = carr4,
                    borderColor = new string[] { "rgba(255,134,14,0.7)" },
                    backgroundColor = new string[] { "rgba(0,0,0,0)" },
                    borderWidth = "3"
                });

                /////////////////////////////////////////////////
            
                //////////////////////////////////////////////

                _chart.datasets = null;
                _chart.datasets = _dataSet;
                return Json(_chart, JsonRequestBehavior.AllowGet);





            }
            else
            {
                int days = DateTime.DaysInMonth(Convert.ToInt32(mnt[0]), Convert.ToInt32(mnt[1]));
                string[] darr = new string[] { "AHP", "AMP", "BCL", "BIA", "CBO", "CBY", "DLA", "EKA", "HIN", "IRM", "KAT", "KGL", "KTK", "MGR", "MOW", "MTV", "PGL", "PLV", "KKS", "RMA", "SGR", "VNA", "WAN", "WLA" };
                string[] cdarr = new string[] { "AHP", "AMP", "BCL", "BIA", "CBO", "CBY", "DLA", "EKA", "HIN", "IRM", "KAT", "KGL", "KTK", "MGR", "MOW", "MTV", "PGL", "PLV", "PLY", "RMA", "SGR", "VNA", "VNI", "WLA" };
                List<string> datlt1 = new List<string>();
                List<decimal> cntlt1 = new List<decimal>();
                try
                {
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DataSet oDataSet = new DataSet();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "   SELECT  distinct  b.LocationID as t ,count(a.PDID) as r " +
    "  FROM[MMS].[dbo].[Patient_Detail] as a inner join[MMS].[dbo].[Clinic_Master] as b on a.OPDID=b.Clinic_ID where a.PatientCatID=1 and B.ClinicTypeID=19 and " +
    " convert(date, a.CreatedDate) =CONVERT(varchar,'" + id + "',111) group by    b.LocationID order by b.LocationID";
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

                        foreach (var row1 in darr)
                        {

                            foreach (DataRow row in oDataSet.Tables[0].Rows)
                            {

                                string dval = row["t"].ToString();

                                string sval = row1;
                                //darr[cnt].ToString();



                                if (sval == dval)
                                {
                                    cntlt1.Add(Convert.ToInt32(row["r"].ToString()));
                                    cnt = 1;
                                    break;
                                }
                                else
                                {
                                    cnt = 0;
                                    // cntlt1.Add(0);

                                }

                            }

                            if (cnt != 1)
                            {
                                cntlt1.Add(0);
                                cnt = 0;
                            }
                            //do
                            //{



                            //    cnt++;
                            //}
                            //while (sval != dval);
                        }
                    }
                }
                catch (Exception ex)
                {

                }


                string[] darr1 = datlt1.ToArray();
                decimal[] carr1 = cntlt1.ToArray();
                List<Datasets> _dataSet = new List<Datasets>();



                _dataSet.Add(new Datasets()
                {
                    label = "Patients Registred to System ",
                    data = carr1,
                    borderColor = new string[] { "rgba(0,0,255,1)" },
                    backgroundColor = new string[] { "rgba(0,0,0,0)" },
                    borderWidth = "3"
                });



                List<string> datlt = new List<string>();
                List<decimal> cntlt = new List<decimal>();
                try
                {
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DataSet oDataSet = new DataSet();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " SELECT distinct  b.LocationID as t ,count(a.ModifiedDate) as r  " +
    "  FROM[MMS].[dbo].[Patient_Detail] as a inner join[MMS].[dbo].[Clinic_Master] as b on a.OPDID=b.Clinic_ID where   a.PatientCatID = 1 and convert(date, a.ModifiedDate)= CONVERT(varchar,'" + id + "',111)  and " +
    "(a.PDID in (select distinct pdid from[MMS].[dbo].[Lab_Report] where PDID = a.PDID  and convert(date, RequestedTime) =CONVERT(varchar,'" + id + "',111)) or " +
    "a.PDID in (select distinct pdid from[MMS].[dbo].[Drug_Prescription] where PDID = a.PDID    and convert(date, Date_Time) =CONVERT(varchar,'" + id + "',111) ) or a.PDID in  " +
     " (select distinct pdid from[MMS].[dbo].[Sick_Category] where PDID = a.PDID    and convert(date, Date) =CONVERT(varchar,'" + id + "',111))or a.PDID in  " +
    "  (select distinct pdid from[MMS].[dbo].[CatDiagList] where PDID = a.PDID))  " +
     "  group by   b.LocationID order by b.LocationID";
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
                        foreach (var row1 in darr)
                        {
                            foreach (DataRow row in oDataSet.Tables[0].Rows)
                            {
                                string dval = row["t"].ToString();

                                string sval = row1;
                                //darr[cnt].ToString();



                                if (sval == dval)
                                {
                                    cntlt.Add(Convert.ToInt32(row["r"].ToString()));
                                    cnt = 1;
                                    break;
                                }
                                else
                                {
                                    cnt = 0;
                                    // cntlt1.Add(0);

                                }

                            }

                            if (cnt != 1)
                            {
                                cntlt.Add(0);
                                cnt = 0;
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

                }
                catch (Exception ex)
                {

                }



                decimal[] carr = cntlt.ToArray();
                Chart _chart = new Chart();
                _chart.labels = cdarr;
                _chart.datasets = new List<Datasets>();

                _dataSet.Add(new Datasets()
                {
                    label = "Doctors Entered to System ",
                    data = carr,
                    borderColor = new string[] { "rgba(255,0,0,1)" },
                    backgroundColor = new string[] { "rgba(0,0,0,0)" },
                    borderWidth = "3"
                });


                //////////////////////////////////////////////
                List<string> datlt2 = new List<string>();
                List<decimal> cntlt2 = new List<decimal>();
                try
                {
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DataSet oDataSet = new DataSet();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "  ;WITH CTE as " +
    "(" +
    " SELECT   RequestedLocID as t, count(TestSID) as r " +
    "   FROM[MMS].[dbo].[Lab_Report] " +
     "  where   convert(date,[RequestedTime]) = CONVERT(varchar, '" + id + "', 111) and Issued='1' and IsApproved=1 " +
    "  group by   RequestedLocID, TestSID " +
    "  ) " +
    " SELECT t, count(*) r " +
    " FROM CTE " +
    " GROUP BY t order by t";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet);
                    oSqlConnection.Close();

                    if (oDataSet.Tables[0].Rows.Count > 0)
                    {
                        // int cnt = 0;
                        int cnt = 0;
                        foreach (var row1 in darr)
                        {
                            foreach (DataRow row in oDataSet.Tables[0].Rows)
                            {
                                string dval = row["t"].ToString();

                                string sval = row1;
                                //darr[cnt].ToString();



                                if (sval == dval)
                                {
                                    cntlt2.Add(Convert.ToInt32(row["r"].ToString()));
                                    cnt = 1;
                                    break;
                                }
                                else
                                {
                                    cnt = 0;
                                    // cntlt1.Add(0);

                                }

                            }

                            if (cnt != 1)
                            {
                                cntlt2.Add(0);
                                cnt = 0;
                            }


                            //foreach (DataRow row in oDataSet.Tables[0].Rows)
                            //{
                            //    //DateTime nd = DateTime.Parse(row["t"].ToString());
                            //    //string dval = row["t"].ToString();

                            //    //string sval = darr[cnt].ToString();


                            //    //do
                            //    //{

                            //    //    sval = darr[cnt].ToString();
                            //    //    if (dval == sval)
                            //    //    {
                            //    //        cntlt2.Add(Convert.ToInt32(row["r"].ToString()));
                            //    //    }
                            //    //    else
                            //    //    {
                            //    //        cntlt2.Add(0);

                            //    //    }

                            //    //    cnt++;
                            //    //}
                            //    //while (sval != dval);
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {

                }


                string[] darr2 = datlt2.ToArray();
                decimal[] carr2 = cntlt2.ToArray();




                _dataSet.Add(new Datasets()
                {
                    label = "Lab Reports Issued",
                    data = carr2,
                    borderColor = new string[] { "rgba(0,255,0,1)" },
                    backgroundColor = new string[] { "rgba(0,0,0,0)" },
                    borderWidth = "3"
                });
                //////////////////////////////////////////////
                List<string> datlt3 = new List<string>();
                List<decimal> cntlt3 = new List<decimal>();
                try
                {
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DataSet oDataSet = new DataSet();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "    ;WITH CTE as " +
    "(" +
    " SELECT   RequestedLocID as t, count(pdid) as r " +
    "   FROM[MMS].[dbo].[Drug_Prescription] " +
    "   where   convert(date, Date_Time) = CONVERT(varchar, '" + id + "', 111) and Issued='1' " +
    "  group by   RequestedLocID, pdid " +
    "  ) " +
    " SELECT t, count(*) r " +
    " FROM CTE " +
    " GROUP BY t order by t";
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
                        foreach (var row1 in darr)
                        {
                            foreach (DataRow row in oDataSet.Tables[0].Rows)
                            {
                                string dval = row["t"].ToString();

                                string sval = row1;
                                //darr[cnt].ToString();



                                if (sval == dval)
                                {
                                    cntlt3.Add(Convert.ToInt32(row["r"].ToString()));
                                    cnt = 1;
                                    break;
                                }
                                else
                                {
                                    cnt = 0;
                                    // cntlt1.Add(0);

                                }

                            }

                            if (cnt != 1)
                            {
                                cntlt3.Add(0);
                                cnt = 0;
                            }
                            //int cnt = 0;



                            //foreach (DataRow row in oDataSet.Tables[0].Rows)
                            //{
                            //    //DateTime nd = DateTime.Parse(row["t"].ToString());
                            //    string dval = row["t"].ToString();

                            //    string sval = darr[cnt].ToString();


                            //    do
                            //    {

                            //        sval = darr[cnt].ToString();
                            //        if (dval == sval)
                            //        {
                            //            cntlt3.Add(Convert.ToInt32(row["r"].ToString()));
                            //        }
                            //        else
                            //        {
                            //            cntlt3.Add(0);

                            //        }

                            //        cnt++;
                            //    }
                            //    while (sval != dval);
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {

                }


                string[] darr3 = datlt3.ToArray();
                decimal[] carr3 = cntlt3.ToArray();




                _dataSet.Add(new Datasets()
                {
                    label = "Pharmacy Drugs Issued",
                    data = carr3,
                    borderColor = new string[] { "rgba(255,0,255,1)" },
                    backgroundColor = new string[] { "rgba(0,0,0,0)" },
                    borderWidth = "3"
                });

                /////////////////////////////////////////////////
                ///
                //////////////////////////////////////////////
                List<string> datlt4 = new List<string>();
                List<decimal> cntlt4 = new List<decimal>();
                try
                {
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DataSet oDataSet = new DataSet();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "     ;WITH CTE as " +
    " ( " +
     " SELECT   CM.LocationID as t, count(wd.pdid) as r " +
    "   FROM dbo.Ward_Details WD  INNER JOIN dbo.Ward_Types WT ON WD.Ward_No = WT.ID " +
    " INNER JOIN dbo.Clinic_Master CM ON WD.OPDID = CM.Clinic_ID " +
    "   where    wd.Status = 15 " +
     " group by   CM.LocationID, pdid " +
    "  )" +
    " SELECT t, count(*) r " +
    " FROM CTE " +
    " GROUP BY t order by t";
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
                        foreach (var row1 in darr)
                        {
                            foreach (DataRow row in oDataSet.Tables[0].Rows)
                            {
                                string dval = row["t"].ToString();

                                string sval = row1;
                                //darr[cnt].ToString();



                                if (sval == dval)
                                {
                                    cntlt4.Add(Convert.ToInt32(row["r"].ToString()));
                                    cnt = 1;
                                    break;
                                }
                                else
                                {
                                    cnt = 0;
                                    // cntlt1.Add(0);

                                }

                            }

                            if (cnt != 1)
                            {
                                cntlt4.Add(0);
                                cnt = 0;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                }


                string[] darr4 = datlt4.ToArray();
                decimal[] carr4 = cntlt4.ToArray();




                _dataSet.Add(new Datasets()
                {
                    label = "WARD Admited Patients",
                    data = carr4,
                    borderColor = new string[] { "rgba(255,134,14,0.7)" },
                    backgroundColor = new string[] { "rgba(0,0,0,0)" },
                    borderWidth = "3"
                });

                /////////////////////////////////////////////////
                //////////////////////////////////////////////
                List<string> datlt5 = new List<string>();
                List<decimal> cntlt5 = new List<decimal>();
                try
                {
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                    string sqlQuery;
                    DateTime dd = DateTime.Now.Date;
                    DataSet oDataSet = new DataSet();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "  ;WITH CTE as " +
    "(" +
    " SELECT   RequestedLocID as t, count(TestSID) as r " +
    "   FROM[MMS].[dbo].[Lab_Report] " +
     "  where   convert(date,[RequestedTime]) = CONVERT(varchar, '" + id + "', 111) and Issued='1' and ( IsApproved is null or IsApproved=0) " +
    "  group by   RequestedLocID, TestSID " +
    "  ) " +
    " SELECT t, count(*) r " +
    " FROM CTE " +
    " GROUP BY t order by t";
                    // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet);
                    oSqlConnection.Close();

                    if (oDataSet.Tables[0].Rows.Count > 0)
                    {
                        // int cnt = 0;
                        int cnt = 0;
                        foreach (var row1 in darr)
                        {
                            foreach (DataRow row in oDataSet.Tables[0].Rows)
                            {
                                string dval = row["t"].ToString();

                                string sval = row1;
                                //darr[cnt].ToString();



                                if (sval == dval)
                                {
                                    cntlt5.Add(Convert.ToInt32(row["r"].ToString()));
                                    cnt = 1;
                                    break;
                                }
                                else
                                {
                                    cnt = 0;
                                    // cntlt1.Add(0);

                                }

                            }

                            if (cnt != 1)
                            {
                                cntlt5.Add(0);
                                cnt = 0;
                            }


                            //foreach (DataRow row in oDataSet.Tables[0].Rows)
                            //{
                            //    //DateTime nd = DateTime.Parse(row["t"].ToString());
                            //    //string dval = row["t"].ToString();

                            //    //string sval = darr[cnt].ToString();


                            //    //do
                            //    //{

                            //    //    sval = darr[cnt].ToString();
                            //    //    if (dval == sval)
                            //    //    {
                            //    //        cntlt2.Add(Convert.ToInt32(row["r"].ToString()));
                            //    //    }
                            //    //    else
                            //    //    {
                            //    //        cntlt2.Add(0);

                            //    //    }

                            //    //    cnt++;
                            //    //}
                            //    //while (sval != dval);
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {

                }


                string[] darr5 = datlt5.ToArray();
                decimal[] carr5 = cntlt5.ToArray();




                _dataSet.Add(new Datasets()
                {
                    label = "Lab Reports Pending MLT Approval",
                    data = carr5,
                    borderColor = new string[] { "rgba(255,85,0,1)" },
                    backgroundColor = new string[] { "rgba(0,0,0,0)" },
                    borderWidth = "3"
                });
                //////////////////////////////////////////////

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
        }

        public class DashBoadModel
        {

            public string locid { get; set; }
            public int loccount { get; set; }

        }
        private User GetCurrentUser()
        {
            User us = new User();
            int userID = 0;
            try
            {
                //if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                //{
                //    userID = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
                //}
                if (Session["UserID"] != null)
                {
                    userID = (int)Session["UserID"];
                }

                //int userID = Int32.Parse(HttpContext.User.Identity.Name);

                us = (from u in db.Users
                      where u.UserID == userID
                      select u).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return us;
        }

    }
}
