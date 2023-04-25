using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MMS.Reports
{
    public partial class rptcampuse : System.Web.UI.Page
    {
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        public string sqlQuery;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string opdid = "";
                string locid = "";

                int userid = Convert.ToInt32(Session["UserID"]);

                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;

                opdid = (String)Session["userlocid1"];
                string searchText = string.Empty;

                if (Request.QueryString["datest"] != null)
                {
                    searchText = Request.QueryString["datest"].ToString();
                }




                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();
                DataTable dstIntData2 = new DataTable();
                DataTable dstIntData3 = new DataTable();
                DataTable dstIntData4 = new DataTable();

                oSqlCommand = new SqlCommand();
                sqlQuery = "  ;WITH CTE as "+
"( " +
" SELECT   b.LocationID as t, count(a.PDID) as r " +
"   FROM[dbo].[Clinic_Master] as b left join " +
 "  [MMS].[dbo].[Drug_Prescription] as a on  a.RequestedLocID = b.LocationID " +
"    and(MONTH(a.Date_Time) = 10)AND(YEAR(a.Date_Time) = 0) and Issued = '1' " +
 "  group by   b.LocationID, a.PDID " +
"  ) " +
" SELECT t,(case when  count(r) = 1 then 0 else count(r) end) v  " +
 " FROM CTE GROUP BY t order by t";


                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                //DataTable odsvoltxndata = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = "  ;WITH CTE as "+
" ( " +
 " SELECT   b.LocationID as t, count(a.TestSID) as r " +
 "  FROM[dbo].[Clinic_Master] as b left join " +
 "  [MMS].[dbo].[Lab_Report] as a on  a.RequestedLocID = b.LocationID " +
"   and(MONTH(a.RequestedTime) = 10)AND(YEAR(a.RequestedTime) = 0) " +
 "   and Issued = '1' " +
 "  group by   b.LocationID, a.TestSID " +
 " ) " +
 " SELECT t,(case when  count(r) = 1 then 0 else count(r) end) r " +
"  FROM CTE GROUP BY t order by t ";


                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(dstIntData);

                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT  count(*) as cnt ,b.LocationID from  [dbo].[Clinic_Master] as b  "+
" left join[dbo].[Patient_Detail] as v on v.OPDID=b.Clinic_ID "+
" and(MONTH(v.CreatedDate) = 10)AND(YEAR(v.CreatedDate) = 0) and v.modifiedby is not null "+
" group by b.LocationID";



oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(dstIntData2);
                /////////////////////////////////////////
                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT  count(*) as cnt ,b.LocationID,max(datename(MONTH,dateadd(MONTH,MONTH(v.CreatedDate),-1)))as n,max(year(v.CreatedDate))as y from  [dbo].[Clinic_Master] as b "+ 
 " left join[dbo].[Patient_Detail] as v on v.OPDID=b.Clinic_ID and(MONTH(v.CreatedDate) = 10)AND(YEAR(v.CreatedDate) = 0) "+
" and v.Present_Complain is not null "+
" group by b.LocationID";



                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(dstIntData3);

                /////////////////////////////////////////
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT  count(PDID) as cnt ,b.LocationID from  [dbo].[Clinic_Master] as b  "+
" left join[dbo].[Ward_Details] as v on v.OPDID=b.Clinic_ID and(MONTH(v.Created_Date) = 10)AND(YEAR(v.Created_Date) = 0) and v.Status=15 "+
" group by b.LocationID";




oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(dstIntData4);
                //////////////////////////////////////////////////

                //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
                ReportDataSource rdc2 = new ReportDataSource("DataSet2", dstIntData);
                ReportDataSource rdc3 = new ReportDataSource("DataSet3", dstIntData2);
                ReportDataSource rdc4 = new ReportDataSource("DataSet4", dstIntData3);
                ReportDataSource rdc5 = new ReportDataSource("DataSet5", dstIntData4);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.DataSources.Add(rdc2);
                ReportViewer1.LocalReport.DataSources.Add(rdc3);
                ReportViewer1.LocalReport.DataSources.Add(rdc4);
                ReportViewer1.LocalReport.DataSources.Add(rdc5);
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.DataBind();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string opdid = "";
            string locid ="";

            string userid = datest.Value;
            string[] arr = userid.Split('-');
            string yr = arr[0];
            string mt = arr[1];


            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;

          

            //opdid = (String)Session["userlocid1"];
            //string searchText = string.Empty;

            //if (Request.QueryString["datest"] != null)
            //{
            //    searchText = Request.QueryString["datest"].ToString();
            //}




            DataTable dstIntData = new DataTable();
            DataTable odsvoltxndata = new DataTable();
            DataTable dstIntData2 = new DataTable();
            DataTable dstIntData3 = new DataTable();
            DataTable dstIntData4 = new DataTable();

            oSqlCommand = new SqlCommand();
            sqlQuery = "  ;WITH CTE as " +
"( " +
" SELECT   b.LocationID as t, count(a.PDID) as r " +
"   FROM[dbo].[Clinic_Master] as b left join " +
"  [MMS].[dbo].[Drug_Prescription] as a on  a.RequestedLocID = b.LocationID " +
"    and(MONTH(a.Date_Time) = "+mt+")AND(YEAR(a.Date_Time) = "+yr+") and Issued = '1' " +
"  group by   b.LocationID, a.PDID " +
"  ) " +
" SELECT t,(case when  count(r) = 1 then 0 else count(r) end) v  " +
" FROM CTE GROUP BY t order by t";


            oSqlCommand.Connection = new SqlConnection(conStr);
            oSqlCommand.CommandTimeout = 1500;

            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);
            //DataTable odsvoltxndata = new DataTable();


            oSqlCommand = new SqlCommand();
            sqlQuery = "  ;WITH CTE as " +
" ( " +
" SELECT   b.LocationID as t, count(a.TestSID) as r " +
"  FROM[dbo].[Clinic_Master] as b left join " +
"  [MMS].[dbo].[Lab_Report] as a on  a.RequestedLocID = b.LocationID " +
"   and(MONTH(a.RequestedTime) = " + mt + ")AND(YEAR(a.RequestedTime) = " + yr + ") " +
"   and Issued = '1' " +
"  group by   b.LocationID, a.TestSID " +
" ) " +
" SELECT t,(case when  count(r) = 1 then 0 else count(r) end) r " +
"  FROM CTE GROUP BY t order by t ";


            oSqlCommand.Connection = new SqlConnection(conStr);

            oSqlCommand.CommandTimeout = 1500;
            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(dstIntData);

            oSqlCommand = new SqlCommand();
            sqlQuery = "  SELECT  count(*) as cnt ,b.LocationID from  [dbo].[Clinic_Master] as b  " +
" left join[dbo].[Patient_Detail] as v on v.OPDID=b.Clinic_ID " +
" and(MONTH(v.CreatedDate) = " + mt + ")AND(YEAR(v.CreatedDate) = " + yr + ") and v.modifiedby is not null " +
" group by b.LocationID order by b.LocationID";



            oSqlCommand.Connection = new SqlConnection(conStr);

            oSqlCommand.CommandTimeout = 1500;
            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(dstIntData2);
            /////////////////////////////////////////
            oSqlCommand = new SqlCommand();
            sqlQuery = " SELECT  count(*) as cnt ,b.LocationID,max(datename(MONTH,dateadd(MONTH,MONTH(v.CreatedDate),-1)))as n,max(year(v.CreatedDate))as y from  [dbo].[Clinic_Master] as b " +
" left join[dbo].[Patient_Detail] as v on v.OPDID=b.Clinic_ID and(MONTH(v.CreatedDate) = " + mt + ")AND(YEAR(v.CreatedDate) = " + yr + ") " +
" and v.Present_Complain is not null " +
" group by b.LocationID order by b.LocationID";



            oSqlCommand.Connection = new SqlConnection(conStr);

            oSqlCommand.CommandTimeout = 1500;
            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(dstIntData3);

            /////////////////////////////////////////
            oSqlCommand = new SqlCommand();
            sqlQuery = "  SELECT  count(PDID) as cnt ,b.LocationID from  [dbo].[Clinic_Master] as b  " +
" left join[dbo].[Ward_Details] as v on v.OPDID=b.Clinic_ID and(MONTH(v.Created_Date) = " + mt + ")AND(YEAR(v.Created_Date) = " + yr + ") and v.Status=15 " +
" group by b.LocationID order by b.LocationID";




            oSqlCommand.Connection = new SqlConnection(conStr);

            oSqlCommand.CommandTimeout = 1500;
            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(dstIntData4);
            //////////////////////////////////////////////////

            //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
            ReportDataSource rdc2 = new ReportDataSource("DataSet2", dstIntData);
            ReportDataSource rdc3 = new ReportDataSource("DataSet3", dstIntData2);
            ReportDataSource rdc4 = new ReportDataSource("DataSet4", dstIntData3);
            ReportDataSource rdc5 = new ReportDataSource("DataSet5", dstIntData4);
            ReportViewer1.LocalReport.DataSources.Add(rdc);
            ReportViewer1.LocalReport.DataSources.Add(rdc2);
            ReportViewer1.LocalReport.DataSources.Add(rdc3);
            ReportViewer1.LocalReport.DataSources.Add(rdc4);
            ReportViewer1.LocalReport.DataSources.Add(rdc5);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }
    }
}