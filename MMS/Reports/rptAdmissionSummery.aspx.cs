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
    public partial class rptAdmissionSummery : System.Web.UI.Page
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

                int userid = Convert.ToInt32(Session["UserID"]);

                string searchText = (String)Session["userlocid1"];

                List<DrugStockTransection> customers = null;

                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT wt.Ward_Type,COUNT(WD.Ward_No) AS AddCount,CM.LocationID " +
                           " FROM dbo.Ward_Details WD  " +
                           " INNER JOIN dbo.Ward_Types WT ON WD.Ward_No = WT.ID " +
                           " INNER JOIN dbo.Clinic_Master CM ON WD.OPDID = CM.Clinic_ID " +
                           " WHERE WD.OPDID = '"+ searchText + "' and wd.status=15 " +
                           " GROUP BY wt.Ward_Type,CM.LocationID ";
                oSqlCommand.Connection = new SqlConnection(conStr);
                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(odsvoltxndata);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("Admission", odsvoltxndata);

                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.DataBind();

            }
        }
    }
}