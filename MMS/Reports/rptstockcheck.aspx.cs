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
    public partial class rptstockcheck : System.Web.UI.Page
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

              
                string searchText = string.Empty;
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;

                string phbatchid = (String)Session["phbatchid"];
                List<DrugStockTransection> customers = null;

                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT a.SysQty,a.drugqnty,COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'')  " +
 "  as itemdescription ,(f.FName + f.LName)as fname,g.LocationID,g.Clinic_Detail,a.stcheckdate " +
  "    FROM[MMS].[dbo].[DrugStockCheck] as a left join[MMS].[dbo].[DrugItems] as d on " +
 "   a.drugid=Convert(varchar, d.DrugID)     left join[MMS].[dbo].[EPASPharmacyItems] as e on a.drugid=Convert(varchar, " +
" e.[itemno])  left join[dbo].[Users] " +
" as f on a.CreatedBy=f.UserID left join[dbo].[Clinic_Master] as g on a.storeid=g.Clinic_ID " +
" where  a.batchid='"+ phbatchid + "'    order by a.stcheckdate ";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);

                //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.DataBind();

            }
        }
    }
}