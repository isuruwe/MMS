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
    public partial class rptlabtest : System.Web.UI.Page
    {
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        public string sqlQuery;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string opdid = "";
            string locid = "";

            int userid = Convert.ToInt32(Session["UserID"]);


            opdid = (String)Session["userlocid1"];
            string searchText = string.Empty;


            searchText = datest.Value;


            List<DrugStockTransection> customers = null;

            DataTable dstIntData = new DataTable();
            DataTable odsvoltxndata = new DataTable();
            DataTable odsvoltxndata1 = new DataTable();

            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT distinct MAX(c.ServiceNo) as serviceno, MAX(a.RequestedTime) as reqdate,e.CategoryName FROM  [Lab_Report] as a inner join Patient_Detail as b on a.PDID=b.PDID  " +
" inner join Patient as c on c.PID = b.PID " +
 " inner join Lab_SubCategory as d on a.LabTestID = d.LabTestID " +
 " inner join Lab_MainCategory as e on d.CategoryID = e.CategoryID " +
 " WHERE cast(RequestedTime as date) = cast('2019/09/03' as date)  and c.ServiceNo = '0' and a.Issued = 0 group by e.CategoryName";
            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);
            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT distinct MAX(c.ServiceNo) as serviceno, MAX(a.RequestedTime) as reqdate,e.CategoryName FROM  [Lab_Report] as a inner join Patient_Detail as b on a.PDID=b.PDID  " +
" inner join Patient as c on c.PID = b.PID " +
 " inner join Lab_SubCategory as d on a.LabTestID = d.LabTestID " +
 " inner join Lab_MainCategory as e on d.CategoryID = e.CategoryID " +
 " WHERE cast(RequestedTime as date) = cast('2019/09/03' as date)  and c.ServiceNo = '0' and a.Issued = 1 group by e.CategoryName";
            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata1);
            //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
            ReportDataSource rdc1 = new ReportDataSource("DataSet2", odsvoltxndata1);
            ReportViewer1.LocalReport.DataSources.Add(rdc);
            ReportViewer1.LocalReport.DataSources.Add(rdc1);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }
    }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string opdid = "";
            string locid = "";

            int userid = Convert.ToInt32(Session["UserID"]);


            opdid = (String)Session["userlocid1"];
            string searchText = string.Empty;


            searchText = datest.Value;


            List<DrugStockTransection> customers = null;

            DataTable dstIntData = new DataTable();
            DataTable odsvoltxndata = new DataTable();
            DataTable odsvoltxndata1 = new DataTable();

            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT distinct MAX(c.ServiceNo) as serviceno, MAX(a.RequestedTime) as reqdate,e.CategoryName FROM  [Lab_Report] as a inner join Patient_Detail as b on a.PDID=b.PDID  " +
" inner join Patient as c on c.PID = b.PID " +
 " inner join Lab_SubCategory as d on a.LabTestID = d.LabTestID " +
 " inner join Lab_MainCategory as e on d.CategoryID = e.CategoryID " +
 " WHERE cast(RequestedTime as date) = cast('"+searchText+"' as date)  and c.ServiceNo = '"+drname.Value+"' and a.Issued = 0 group by e.CategoryName";
            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);
            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT distinct MAX(c.ServiceNo) as serviceno, MAX(a.RequestedTime) as reqdate,e.CategoryName FROM  [Lab_Report] as a inner join Patient_Detail as b on a.PDID=b.PDID  " +
" inner join Patient as c on c.PID = b.PID " +
 " inner join Lab_SubCategory as d on a.LabTestID = d.LabTestID " +
 " inner join Lab_MainCategory as e on d.CategoryID = e.CategoryID " +
 " WHERE cast(RequestedTime as date) = cast('" + searchText + "' as date)  and c.ServiceNo = '" + drname.Value + "' and a.Issued = 1 group by e.CategoryName";
            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata1);
            //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
            ReportDataSource rdc1 = new ReportDataSource("DataSet2", odsvoltxndata1);
            ReportViewer1.LocalReport.DataSources.Add(rdc);
            ReportViewer1.LocalReport.DataSources.Add(rdc1);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }
    }
}