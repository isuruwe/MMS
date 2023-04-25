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
    public partial class rptAims2 : System.Web.UI.Page
    {
      
            SqlConnection oSqlConnection;
            SqlCommand oSqlCommand;
            SqlDataAdapter oSqlDataAdapter;
            public string sqlQuery;
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;

                    string opdid = "";
                    string locid = "";

                    int userid = Convert.ToInt32(Session["UserID"]);


                    opdid = (String)Session["userlocid1"];
                    string searchText = string.Empty;
                    string searchText1 = string.Empty;
                    if (Request.QueryString["opdid"] != null)
                    {
                        searchText = Request.QueryString["opdid"].ToString();
                    }
                    if (Request.Params["datest1"] != null)
                    {
                        searchText1 = Request.Params["datest1"].ToString();
                    }
                    List<DrugStockTransection> customers = null;


                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();
                DataTable odsvoltxndata2 = new DataTable();
                DataTable odsvoltxndata3 = new DataTable();
                DataTable odsvoltxndata4 = new DataTable();
                DataTable odsvoltxndata5 = new DataTable();
                DataTable odsvoltxndata6 = new DataTable();
                if (Request.Params["datest"] != null)
                {
                    searchText = Request.Params["datest"].ToString();
                }
                if (Request.Params["datest1"] != null)
                {
                    searchText1 = Request.Params["datest1"].ToString();
                }

                oSqlCommand = new SqlCommand();

                sqlQuery = "select CONVERT(varchar,a.CreatedDate,111) as dt ,count(a.CreatedDate) as t " +
",count(case when b.PDID IS not NULL then  b.PDID end) doc " +
"from[dbo].[Patient_Detail] as a left join[dbo].[CatDiagList] as b on a.pdid=b.pdid " +
" left join [dbo].[Clinic_Master] as c on a.OPDID=c.Clinic_ID " +

"where  c.LocationID='" + DropDownList1.SelectedValue.ToString() + "' and " +
"convert(date, a.[CreatedDate]) between CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111)" +
"group by  CONVERT(varchar, a.CreatedDate,111)";

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                oSqlConnection.Close();
                ////////////////////////////////////////////////
                sqlQuery = "select d.fg as tot2,count(d.tot2) as dt2 from(SELECT  count(pdid) as tot2, (CONVERT(varchar,Date_Time,111))  as fg FROM[MMS].[dbo].[Drug_Prescription] WHERE cast(Date_Time as date) between " +
" cast('" + searchText + "' as date) and cast('" + searchText1 + "' as date) and Issued = '1' and RequestedLocID = '" + DropDownList1.SelectedValue.ToString() + "' group by Date_Time, pdid) as d group by d.fg order by d.fg";

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata2);
                oSqlConnection.Close();
                /////////////////////////////////////////////////////////////////
                sqlQuery = "select d.fg as tot2,count(d.tot2) as dt2 from(SELECT  count(pdid) as tot2, (CONVERT(varchar,Date_Time,111)) as fg FROM[MMS].[dbo].[Drug_Prescription] WHERE cast(Date_Time as date) between " +
    " cast('" + searchText + "' as date) and cast('" + searchText1 + "' as date) and Issued = '1' and RequestedLocID = '" + DropDownList1.SelectedValue.ToString() + "' group by Date_Time, pdid) as d group by d.fg order by d.fg";

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata3);
                oSqlConnection.Close();
                /////////////////////////////////////////////////////////////////
                sqlQuery = " ;WITH CTE as ( " +
     "SELECT   max(CONVERT(varchar,RequestedTime,111)) as t, count(TestSID) as r " +
      " FROM[MMS].[dbo].[Lab_Report]" +
     "  where   convert(date, RequestedTime) between CONVERT(varchar, '" + searchText + "', 111)" +
     "and  CONVERT(varchar, '" + searchText1 + "', 111)  and RequestedLocID = '" + DropDownList1.SelectedValue.ToString() + "'" +
     " group by    TestSID )" +
     "SELECT t, count(*) r" +
    " FROM CTE" +
    " GROUP BY t order by t ";

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata4);
                oSqlConnection.Close();
                /////////////////////////////////////////////////////////////////
                sqlQuery = " ;WITH CTE as ( " +
    "SELECT   max(CONVERT(varchar,RequestedTime,111)) as t, count(TestSID) as r " +
    " FROM[MMS].[dbo].[Lab_Report]" +
    "  where   convert(date, RequestedTime) between CONVERT(varchar, '" + searchText + "', 111)" +
    "and  CONVERT(varchar, '" + searchText1 + "', 111)   and Issued = '1' and RequestedLocID = '" + DropDownList1.SelectedValue.ToString() + "'" +
    " group by    TestSID )" +
    "SELECT t, count(*) r" +
    " FROM CTE" +
    " GROUP BY t order by t ";

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata5);
                oSqlConnection.Close();

                /////////////////////////////////////////////////////////////////
                sqlQuery = "select CONVERT(varchar,a.regdate,111) as dt ,count(a.regdate) as sic " +
    " ,count(case when a.isliveout = 'In' then  a.isliveout end) livin " +
    " ,count(case when a.isliveout = 'Out' then  a.isliveout end) livout " +
     " from[dbo].[SickReport] as a " +

    " where  a.LocationID='" + DropDownList1.SelectedValue.ToString() + "' and " +
    " convert(date, a.[regdate]) between CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111) " +
    " group by  CONVERT(varchar, a.regdate,111) ";

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata6);
                oSqlConnection.Close();
                /////////////////////////////////////////////////////////////////

                //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
                ReportDataSource rdc1 = new ReportDataSource("DataSet2", odsvoltxndata2);
                ReportDataSource rdc2 = new ReportDataSource("DataSet4", odsvoltxndata3);
                ReportDataSource rdc3 = new ReportDataSource("DataSet3", odsvoltxndata4);
                ReportDataSource rdc4 = new ReportDataSource("DataSet5", odsvoltxndata5);
                ReportDataSource rdc5 = new ReportDataSource("DataSet6", odsvoltxndata6);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.DataSources.Add(rdc1);
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
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;

                string searchText = string.Empty;
                string searchText1 = string.Empty;
                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();
                DataTable odsvoltxndata2 = new DataTable();
            DataTable odsvoltxndata3 = new DataTable();
            DataTable odsvoltxndata4 = new DataTable();
            DataTable odsvoltxndata5 = new DataTable();
            DataTable odsvoltxndata6 = new DataTable();
            if (Request.Params["datest"] != null)
                {
                    searchText = Request.Params["datest"].ToString();
                }
                if (Request.Params["datest1"] != null)
                {
                    searchText1 = Request.Params["datest1"].ToString();
                }

                oSqlCommand = new SqlCommand();
              
                    sqlQuery = "select CONVERT(varchar,a.CreatedDate,111) as dt ,count(a.CreatedDate) as t " +
",count(case when b.PDID IS not NULL then  b.PDID end) doc " +
"from[dbo].[Patient_Detail] as a left join[dbo].[CatDiagList] as b on a.pdid=b.pdid " +
" left join [dbo].[Clinic_Master] as c on a.OPDID=c.Clinic_ID " +

"where  c.LocationID='" + DropDownList1.SelectedValue.ToString() + "' and " +
"convert(date, a.[CreatedDate]) between CONVERT(varchar,'"+searchText+ "',111) and CONVERT(varchar,'" + searchText1 + "',111)" +
"group by  CONVERT(varchar, a.CreatedDate,111)";
                
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                oSqlConnection.Close();
                ////////////////////////////////////////////////
                sqlQuery = "select d.fg as tot1,count(d.tot2) as dt1 from(SELECT  count(pdid) as tot2, (CONVERT(varchar,Date_Time,111))  as fg FROM[MMS].[dbo].[Drug_Prescription] WHERE cast(Date_Time as date) between " +
" cast('" + searchText + "' as date) and cast('" + searchText1 + "' as date)  and RequestedLocID = '"+DropDownList1.SelectedValue.ToString()+ "' group by Date_Time, pdid) as d group by d.fg order by d.fg";

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata2);
                oSqlConnection.Close();
            /////////////////////////////////////////////////////////////////
            sqlQuery = "select d.fg as tot2,count(d.tot2) as dt2 from(SELECT  count(pdid) as tot2, (CONVERT(varchar,Date_Time,111)) as fg FROM[MMS].[dbo].[Drug_Prescription] WHERE cast(Date_Time as date) between " +
" cast('" + searchText + "' as date) and cast('" + searchText1 + "' as date) and Issued = '1' and RequestedLocID = '" + DropDownList1.SelectedValue.ToString() + "' group by Date_Time, pdid) as d group by d.fg order by d.fg";

            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata3);
            oSqlConnection.Close();
            /////////////////////////////////////////////////////////////////
            sqlQuery = " ;WITH CTE as ( "+
 "SELECT   max(CONVERT(varchar,RequestedTime,111)) as t, count(TestSID) as r " +
  " FROM[MMS].[dbo].[Lab_Report]"+
 "  where   convert(date, RequestedTime) between CONVERT(varchar, '" + searchText + "', 111)" +
 "and  CONVERT(varchar, '" + searchText1 + "', 111)  and RequestedLocID = '"+DropDownList1.SelectedValue.ToString()+"'" +
 " group by    TestSID )"+
 "SELECT t, count(*) r"+
" FROM CTE"+
" GROUP BY t order by t ";

            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata4);
            oSqlConnection.Close();
            /////////////////////////////////////////////////////////////////
            sqlQuery = " ;WITH CTE as ( " +
"SELECT   max(CONVERT(varchar,RequestedTime,111)) as t, count(TestSID) as r " +
" FROM[MMS].[dbo].[Lab_Report]" +
"  where   convert(date, RequestedTime) between CONVERT(varchar, '" + searchText + "', 111)" +
"and  CONVERT(varchar, '" + searchText1 + "', 111)   and Issued = '1' and RequestedLocID = '" + DropDownList1.SelectedValue.ToString() + "'" +
" group by    TestSID )" +
"SELECT t, count(*) r" +
" FROM CTE" +
" GROUP BY t order by t ";

            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata5);
            oSqlConnection.Close();
            /////////////////////////////////////////////////////////////////
            sqlQuery = "select CONVERT(varchar,a.regdate,111) as dt ,count(a.regdate) as sic " +
     " ,count(case when a.isliveout = 'In' then  a.isliveout end) livin " +
     " ,count(case when a.isliveout = 'Out' then  a.isliveout end) livout " +
      " from[dbo].[SickReport] as a " +

     " where  a.LocationID='" + DropDownList1.SelectedValue.ToString() + "' and " +
     " convert(date, a.[regdate]) between CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111) " +
     " group by  CONVERT(varchar, a.regdate,111) ";

            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata6);
            oSqlConnection.Close();
            /////////////////////////////////////////////////////////////////
            //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
                ReportDataSource rdc1 = new ReportDataSource("DataSet2", odsvoltxndata2);
            ReportDataSource rdc2 = new ReportDataSource("DataSet4", odsvoltxndata3);
            ReportDataSource rdc3 = new ReportDataSource("DataSet3", odsvoltxndata4);
            ReportDataSource rdc4 = new ReportDataSource("DataSet5", odsvoltxndata5);
            ReportDataSource rdc5 = new ReportDataSource("DataSet6", odsvoltxndata6);
            ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.DataSources.Add(rdc1);
            ReportViewer1.LocalReport.DataSources.Add(rdc2);
            ReportViewer1.LocalReport.DataSources.Add(rdc3);
            ReportViewer1.LocalReport.DataSources.Add(rdc4);
            ReportViewer1.LocalReport.DataSources.Add(rdc5);
            ReportViewer1.LocalReport.Refresh();
                ReportViewer1.DataBind();

            }
        
    }
}