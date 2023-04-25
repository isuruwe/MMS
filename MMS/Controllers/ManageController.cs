using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MMS.Models;
using MMS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MMS.Controllers
{
   
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private MMSEntities db = new MMSEntities();
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        public ManageController()
        {
        }
       
        public ActionResult Campreport()
        {
            return View();
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult dchart()
        {
            return View();
        }
        public ActionResult aims()
        {
            return View();
        }
        public ActionResult drugbysvc()
        {
            return View();
        }
        public ActionResult drugbyms()
        {
            return View();
        }
        public JsonResult LineChartData(string id , string id1)
        {
            String[] mnt = id.Split('-');
            string locid = "";
            var serW = from s in db.Clinic_Master.Where(p => p.LocationID == id1).Where(p => p.ClinicTypeID == 19) select new { s.Clinic_ID };

            foreach (var item in serW)
            {

                locid = item.Clinic_ID;
            }

            int days = DateTime.DaysInMonth(Convert.ToInt32( mnt[0]), Convert.ToInt32(mnt[1]));
            string[] darr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
            List<string> datlt = new List<string>();
            List<decimal> cntlt = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
               DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT distinct convert(date, a.[ModifiedDate]) as t ,count(a.ModifiedDate) as r " +
 " FROM[MMS].[dbo].[Patient_Detail] as a   where a.OPDID = '"+ locid + "' and a.PatientCatID = 1 and a.ModifiedDate between " +
" CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar,'" + id + "/" + days.ToString() + " 23:59:59', 111) and " +
 "    (a.PDID in (select distinct pdid from[MMS].[dbo].[Lab_Report] where  PDID = a.PDID  and RequestedTime between " +

 "   CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar, '" + id + "/" + days.ToString() + " 23:59:59', 111)) or " +
"  a.PDID in (select distinct pdid from[MMS].[dbo].[Drug_Prescription] where PDID = a.PDID    and Date_Time between " +
" CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar,'" + id + "/" + days.ToString() + " 23:59:59', 111)) or a.PDID in  " +
 " (select distinct pdid from[MMS].[dbo].[Sick_Category] where PDID = a.PDID    and Date between " +
" CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar,'" + id + "/" + days.ToString() + " 23:59:59', 111))or a.PDID in  " +
 " (select distinct pdid from[MMS].[dbo].[CatDiagList] where PDID = a.PDID))  " +
 "  group by   convert(date, a.ModifiedDate) order by convert(date, a.ModifiedDate)";
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

                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);
                    }
                }
            }
            catch (Exception ex)
            {

            }



            decimal[] carr = cntlt.ToArray();
            Chart _chart = new Chart();
            _chart.labels = darr;
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                label = "Doctors Entered to System "+id1,
                data = carr,
                borderColor = new string[] { "rgba(255,0,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0)" },
                borderWidth = "3"
            });
            List<string> datlt1 = new List<string>();
            List<decimal> cntlt1 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = '" + locid + "' and PatientCatID=1 and  CreatedDate between   CONVERT(varchar,'" + id+"/01',111) and CONVERT(varchar,'"+id+ "/" + days.ToString() + " 23:59:59',111) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt1.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt1.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr1 = datlt1.ToArray();
            decimal[] carr1 = cntlt1.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients Registred to System "+id1,
                data = carr1,
                borderColor = new string[] { "rgba(0,0,255,1)" },
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
                sqlQuery = "  ;WITH CTE as "+
"( "+
" SELECT   convert(date, RequestedTime) as t, count(TestSID) as r "+
"   FROM[MMS].[dbo].[Lab_Report] where RequestedLocID = '"+id1+"' and  RequestedTime between "+
 "   CONVERT(varchar,'" + id + "/01',111)  and CONVERT(varchar,'" + id + "/" + days.ToString() + " 23:59:59',111) " +
"  group by   convert(date,[RequestedTime]), TestSID "+
 " ) "+
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



                    foreach (DataRow row in oDataSet.Tables[0].Rows)
                    {
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt2.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt2.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);
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
                label = "Lab Requests " + id1,
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
                sqlQuery = "  ;WITH CTE as " +
"( " +
" SELECT   convert(date, Date_Time) as t, count(pdid) as r " +
"   FROM[MMS].[dbo].[Drug_Prescription] where RequestedLocID = '" + id1 + "' and  Date_Time between " +
 "  CONVERT(varchar,'" + id + "/01',111)  and CONVERT(varchar,'" + id + "/" + days.ToString() + " 23:59:59',111) " +
"  group by   convert(date,[Date_Time]), pdid " +
 " ) " +
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



                    foreach (DataRow row in oDataSet.Tables[0].Rows)
                    {
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt3.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt3.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);
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
                label = "Pharmacy Requests " + id1,
                data = carr3,
                borderColor = new string[] { "rgba(255,0,255,1)" },
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

        public JsonResult Chart1(string id)
        {
            String[] mnt = id.Split('-');
            int days = DateTime.DaysInMonth(Convert.ToInt32(mnt[0]), Convert.ToInt32(mnt[1]));
            string[] darr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
            List<string> datlt = new List<string>();
            List<decimal> cntlt = new List<decimal>();
            try
            {

                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection("Data Source =135.22.210.105; Initial Catalog = MMS; User ID = mmsuser; Password = password");
                oSqlCommand = new SqlCommand();
                sqlQuery ="  SELECT distinct convert(date, a.[ModifiedDate]) as t ,count(a.ModifiedDate) as r "+
 " FROM[MMS].[dbo].[Patient_Detail] as a   where a.OPDID = 'opd2' and a.PatientCatID = 1 and a.ModifiedDate between " +
" CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar,'" + id + "/" + days.ToString() + " 23:59:59', 111) and " +
 "    (a.PDID in (select distinct pdid from[MMS].[dbo].[Lab_Report] where  PDID = a.PDID  and RequestedTime between " +

 "   CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar, '" + id + "/" + days.ToString() + " 23:59:59', 111)) or " +
"  a.PDID in (select distinct pdid from[MMS].[dbo].[Drug_Prescription] where PDID = a.PDID    and Date_Time between " +
" CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar,'" + id + "/" + days.ToString() + " 23:59:59', 111)) or a.PDID in  " +
 " (select distinct pdid from[MMS].[dbo].[Sick_Category] where PDID = a.PDID    and Date between " +
" CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar,'" + id + "/" + days.ToString() + " 23:59:59', 111))or a.PDID in  " +
 " (select distinct pdid from[MMS].[dbo].[CatDiagList] where PDID = a.PDID))  " +
 "  group by   convert(date, a.ModifiedDate) order by convert(date, a.ModifiedDate)";




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

                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);
                    }
                }
            }
            catch (Exception ex)
            {

            }



            decimal[] carr = cntlt.ToArray();
            Chart _chart = new Chart();
            _chart.labels = darr;
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                label = "Doctors Entered to System RMA",
                data = carr,
                borderColor = new string[] { "rgba(75,192,192,1)" },
                backgroundColor = new string[] { "rgba(255,0,0,0.7)" },
                borderWidth = "1"
            });
            List<string> datlt1 = new List<string>();
            List<decimal> cntlt1 = new List<decimal>();
            try
            {

                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection("Data Source =135.22.210.105; Initial Catalog = MMS; User ID = mmsuser; Password = password");
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd2' and PatientCatID=1 and  CreatedDate between   CONVERT(varchar,'" + id+"/01',111) and CONVERT(varchar,'"+id+ "/" + days.ToString() + " 23:59:59',111) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt1.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt1.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr1 = datlt1.ToArray();
            decimal[] carr1 = cntlt1.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients Registred to System RMA",
                data = carr1,
                borderColor = new string[] { "rgba(0,128,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,255,0.7)" },
                borderWidth = "1"
            });
            _chart.datasets = null;
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);




          
        }
        public JsonResult Chart2(string id)
        {
            String[] mnt = id.Split('-');
            int days = DateTime.DaysInMonth(Convert.ToInt32(mnt[0]), Convert.ToInt32(mnt[1]));
            string[] darr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
            List<string> datlt = new List<string>();
            List<decimal> cntlt = new List<decimal>();
            try
            {

                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection("Data Source =135.22.210.105; Initial Catalog = MMS; User ID = mmsuser; Password = password");
                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT distinct convert(date, a.[ModifiedDate]) as t ,count(a.ModifiedDate) as r " +
 " FROM[MMS].[dbo].[Patient_Detail] as a   where a.OPDID = 'opd3' and a.PatientCatID = 1 and a.ModifiedDate between " +
" CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar,'" + id + "/" + days.ToString() + " 23:59:59', 111) and " +
 "    (a.PDID in (select distinct pdid from[MMS].[dbo].[Lab_Report] where  PDID = a.PDID  and RequestedTime between " +

 "   CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar, '" + id + "/" + days.ToString() + " 23:59:59', 111)) or " +
"  a.PDID in (select distinct pdid from[MMS].[dbo].[Drug_Prescription] where PDID = a.PDID    and Date_Time between " +
" CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar,'" + id + "/" + days.ToString() + " 23:59:59', 111)) or a.PDID in  " +
 " (select distinct pdid from[MMS].[dbo].[Sick_Category] where PDID = a.PDID    and Date between " +
" CONVERT(varchar, '" + id + "/01', 111)   and CONVERT(varchar,'" + id + "/" + days.ToString() + " 23:59:59', 111))or a.PDID in  " +
 " (select distinct pdid from[MMS].[dbo].[CatDiagList] where PDID = a.PDID))  " +
 "  group by   convert(date, a.ModifiedDate) order by convert(date, a.ModifiedDate)";
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

                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);
                    }
                }
            }
            catch (Exception ex)
            {

            }



            decimal[] carr = cntlt.ToArray();
            Chart _chart = new Chart();
            _chart.labels = darr;
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                label = "Doctors Entered to System KAT",
                data = carr,
                borderColor = new string[] { "rgba(75,192,192,1)" },
                backgroundColor = new string[] { "rgba(255,0,0,0.7)" },
                borderWidth = "1"
            });
            List<string> datlt1 = new List<string>();
            List<decimal> cntlt1 = new List<decimal>();
            try
            {

                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection("Data Source =135.22.210.105; Initial Catalog = MMS; User ID = mmsuser; Password = password");
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd3' and PatientCatID=1 and  CreatedDate between   CONVERT(varchar,'" + id+"/01',111) and CONVERT(varchar,'"+id+ "/" + days.ToString() + " 23:59:59',111) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt1.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt1.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr1 = datlt1.ToArray();
            decimal[] carr1 = cntlt1.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients Registred to System KAT",
                data = carr1,
                borderColor = new string[] { "rgba(0,128,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,255,0.7)" },
                borderWidth = "1"
            });
            _chart.datasets = null;
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);




        }
        public JsonResult MultiLineChartData(string id)
        {
            String[] mnt = id.Split('-');
            int days = DateTime.DaysInMonth(Convert.ToInt32(mnt[0]), Convert.ToInt32(mnt[1]));
            string[] darr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" }; 
            List<string> datlt = new List<string>();
            List<decimal> cntlt = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd1' and  CreatedDate between   CONVERT(varchar(10),'"+id+"/01',103) and CONVERT(varchar(10),'"+id+ "/" + days.ToString() + "',103)  group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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

                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);
                    }
                }
            }
            catch (Exception ex)
            {

            }



            decimal[] carr = cntlt.ToArray();
            Chart _chart = new Chart();
            _chart.labels = darr;
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                label = "Patients CBO",
                data = carr,
                borderColor = new string[] { "rgba(255, 77, 0, 1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });
            List<string> datlt1 = new List<string>();
            List<decimal> cntlt1 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd2' and  CreatedDate between   CONVERT(varchar(10),'"+id+"/01',103) and CONVERT(varchar(10),'"+id+ "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt1.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt1.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr1 = datlt1.ToArray();
            decimal[] carr1 = cntlt1.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients RMA",
                data = carr1,
                borderColor = new string[] { "rgba(0, 85, 255, 1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });
            ///////////////////////////////////////

            List<string> datlt2 = new List<string>();
            List<decimal> cntlt2 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd3' and  CreatedDate between   CONVERT(varchar(10),'"+id+"/01',103) and CONVERT(varchar(10),'"+id+ "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();
                        
                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt2.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt2.Add(0);

                            }
                            
                            cnt++;
                        }
                        while (sval != dval);

                           
                      

                        //cnt++;
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
                label = "Patients KAT",
                data = carr2,
                borderColor = new string[] { "rgba(255, 0, 72, 1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });
            ///////////////////////////////////////

            List<string> datlt3 = new List<string>();
            List<decimal> cntlt3 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd4' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt3.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt3.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
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
                label = "Patients EKA",
                data = carr3,
                borderColor = new string[] { "rgba(255, 26, 0, 1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });
            ///////////////////////////////////////

            List<string> datlt4 = new List<string>();
            List<decimal> cntlt4 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd5' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt4.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt4.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
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
                label = "Patients IRM",
                data = carr4,
                borderColor = new string[] { "rgba(255, 153, 0, 1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });
            ///////////////////////////////////////

            List<string> datlt5 = new List<string>();
            List<decimal> cntlt5 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd6' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt5.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt5.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
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
                label = "Patients AHP",
                data = carr5,
                borderColor = new string[] { "rgba(115,108,144,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });
            ///////////////////////////////////////

            List<string> datlt6 = new List<string>();
            List<decimal> cntlt6 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd7' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt6.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt6.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr6 = datlt6.ToArray();
            decimal[] carr6 = cntlt6.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients DLA",
                data = carr6,
                borderColor = new string[] { "rgba(255,65,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt7 = new List<string>();
            List<decimal> cntlt7 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd8' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt7.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt7.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr7 = datlt7.ToArray();
            decimal[] carr7 = cntlt7.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients AMP",
                data = carr7,
                borderColor = new string[] { "rgba(255,158,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt8 = new List<string>();
            List<decimal> cntlt8 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd9' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt8.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt8.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr8 = datlt8.ToArray();
            decimal[] carr8 = cntlt8.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients BCL",
                data = carr8,
                borderColor = new string[] { "rgba(255,242,67,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });



            ///////////////////////////////////////

            List<string> datlt9 = new List<string>();
            List<decimal> cntlt9 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd10' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt9.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt9.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr9 = datlt9.ToArray();
            decimal[] carr9 = cntlt9.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients BIA",
                data = carr9,
                borderColor = new string[] { "rgba(0,79,67,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt10 = new List<string>();
            List<decimal> cntlt10 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd11' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt10.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt10.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr10 = datlt10.ToArray();
            decimal[] carr10 = cntlt10.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients CBY",
                data = carr10,
                borderColor = new string[] { "rgba(93,79,67,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt11 = new List<string>();
            List<decimal> cntlt11 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd12' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt11.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt11.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr11 = datlt11.ToArray();
            decimal[] carr11 = cntlt11.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients HIN",
                data = carr11,
                borderColor = new string[] { "rgba(0,0,255,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt12 = new List<string>();
            List<decimal> cntlt12 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd13' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt12.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt12.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr12 = datlt12.ToArray();
            decimal[] carr12 = cntlt12.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients KGL",
                data = carr12,
                borderColor = new string[] { "rgba(0,0,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });



            ///////////////////////////////////////

            List<string> datlt13 = new List<string>();
            List<decimal> cntlt13 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd14' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt13.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt13.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr13 = datlt13.ToArray();
            decimal[] carr13 = cntlt13.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients PLY",
                data = carr13,
                borderColor = new string[] { "rgba(106,0,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt14 = new List<string>();
            List<decimal> cntlt14 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd15' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt14.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt14.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr14 = datlt14.ToArray();
            decimal[] carr14 = cntlt14.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients KTK",
                data = carr14,
                borderColor = new string[] { "rgba(255,0,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt15 = new List<string>();
            List<decimal> cntlt15 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd16' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt15.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt15.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr15 = datlt15.ToArray();
            decimal[] carr15 = cntlt15.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients MGR",
                data = carr15,
                borderColor = new string[] { "rgba(255,97,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt16 = new List<string>();
            List<decimal> cntlt16 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd17' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt16.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt16.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr16 = datlt16.ToArray();
            decimal[] carr16 = cntlt16.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients MOW",
                data = carr16,
                borderColor = new string[] { "rgba(255,178,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt17 = new List<string>();
            List<decimal> cntlt17 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd18' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt17.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt17.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr17 = datlt17.ToArray();
            decimal[] carr17 = cntlt17.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients MTV",
                data = carr17,
                borderColor = new string[] { "rgba(255,255,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt18 = new List<string>();
            List<decimal> cntlt18 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd19' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt18.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt18.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr18 = datlt18.ToArray();
            decimal[] carr18 = cntlt18.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients PGL",
                data = carr18,
                borderColor = new string[] { "rgba(0,255,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt19 = new List<string>();
            List<decimal> cntlt19 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd20' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt19.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt19.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr19 = datlt19.ToArray();
            decimal[] carr19 = cntlt19.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients PLV",
                data = carr19,
                borderColor = new string[] { "rgba(0,48,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt20 = new List<string>();
            List<decimal> cntlt20 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd21' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt20.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt20.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr20 = datlt20.ToArray();
            decimal[] carr20 = cntlt20.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients SGR",
                data = carr20,
                borderColor = new string[] { "rgba(0,136,0,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt21 = new List<string>();
            List<decimal> cntlt21 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd22' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt21.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt21.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr21 = datlt21.ToArray();
            decimal[] carr21 = cntlt21.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients VNA",
                data = carr21,
                borderColor = new string[] { "rgba(0,136,255,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt22 = new List<string>();
            List<decimal> cntlt22 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd23' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt22.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt22.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr22 = datlt22.ToArray();
            decimal[] carr22 = cntlt22.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients VNI",
                data = carr22,
                borderColor = new string[] { "rgba(167,255,255,1)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });

            ///////////////////////////////////////

            List<string> datlt23 = new List<string>();
            List<decimal> cntlt23 = new List<decimal>();
            try
            {
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                DataSet oDataSet = new DataSet();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT  distinct convert(date,[CreatedDate]) as t ,count(PDID) as r " +
  "FROM[MMS].[dbo].[Patient_Detail] where OPDID = 'opd24' and  CreatedDate between   CONVERT(varchar(10),'" + id + "/01',103) and CONVERT(varchar(10),'" + id + "/" + days.ToString() + "',103) group by   convert(date,[CreatedDate]) order by convert(date,[CreatedDate])";
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
                        DateTime nd = DateTime.Parse(row["t"].ToString());
                        string dval = nd.Day.ToString();

                        string sval = darr[cnt].ToString();


                        do
                        {

                            sval = darr[cnt].ToString();
                            if (dval == sval)
                            {
                                cntlt23.Add(Convert.ToInt32(row["r"].ToString()));
                            }
                            else
                            {
                                cntlt23.Add(0);

                            }

                            cnt++;
                        }
                        while (sval != dval);




                        //cnt++;
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string[] darr23 = datlt23.ToArray();
            decimal[] carr23 = cntlt23.ToArray();




            _dataSet.Add(new Datasets()
            {
                label = "Patients WLA",
                data = carr23,
                borderColor = new string[] { "rgba(167,255,0,0.4)" },
                backgroundColor = new string[] { "rgba(0,0,0,0.0)" },
                borderWidth = "2"
            });



            ////////////////////////////////////////
            _chart.datasets = null;
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult LinkLogin(string provider)
        //{
        //    // Request a redirect to the external login provider to link a login for the current user
        //    return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        //}

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}