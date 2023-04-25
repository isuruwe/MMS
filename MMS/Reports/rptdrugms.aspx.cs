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
    public partial class rptdrugms : System.Web.UI.Page
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

                if (Request.QueryString["datest"] != null)
                {
                    searchText = Request.QueryString["datest"].ToString();
                }




                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT* from( " +
" SELECT a.TransectionQty,DAY(a.IssuedDate) as t " +
"	  ,COALESCE(d.[ItemDescription], '') + COALESCE(e.itemdescription, '') as itmdes " +
 " FROM[MMS].[dbo].[DrugStockTransection] as a " +
"  left join[MMS].[dbo].[DrugItems] as d on a.ItemID=Convert(varchar, d.DrugID) " +
 "  left join[MMS].[dbo].[EPASPharmacyItems] as e on a.ItemID=Convert(varchar, e.[itemno]) where a.IssuedDate between " +
"CONVERT(varchar,'2020-01-01',111)  and CONVERT(varchar,'2020-01-28 23:59:59',111)  and a.OutLoc=' '  and  a.TransectionQty!='0' " +
"GROUP BY a.TransectionQty, a.IssuedDate, d.[ItemDescription], e.itemdescription " +
" ) src";


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

        protected void Button1_Click(object sender, EventArgs e)
        {
            string opdid = "";
            string locid = "";

            int userid = Convert.ToInt32(Session["UserID"]);


            opdid = (String)Session["userlocid1"];
            string searchText = string.Empty;


            searchText = datest.Value;

            String[] mnt = searchText.Split('-');
            int days = DateTime.DaysInMonth(Convert.ToInt32(mnt[0]), Convert.ToInt32(mnt[1]));

            List<DrugStockTransection> customers = null;

            DataTable dstIntData = new DataTable();
            DataTable odsvoltxndata = new DataTable();


            oSqlCommand = new SqlCommand();
          
            sqlQuery = " SELECT* from( " +
" SELECT a.TransectionQty,DAY(a.IssuedDate) as t " +
"	  ,COALESCE(d.[ItemDescription], '') + COALESCE(e.itemdescription, '') as itmdes " +
" FROM[MMS].[dbo].[DrugStockTransection] as a " +
"  left join[MMS].[dbo].[DrugItems] as d on a.ItemID=Convert(varchar, d.DrugID) " +
"  left join[MMS].[dbo].[EPASPharmacyItems] as e on a.ItemID=Convert(varchar, e.[itemno]) where a.IssuedDate between " +
"CONVERT(varchar,'" + searchText + "/01',111)  and CONVERT(varchar,'" + searchText + "/" + days.ToString() + " 23:59:59',111)  and a.OutLoc='"+ opdid + "'  and  a.TransectionQty!='0' " +
"GROUP BY a.TransectionQty, a.IssuedDate, d.[ItemDescription], e.itemdescription " +
" ) src";
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