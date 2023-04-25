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
    public partial class rptDrugbysvc : System.Web.UI.Page
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
                sqlQuery = "   SELECT a.[issuedQuantity],a.[Date_Time],COALESCE(d.[ItemDescription],'') +COALESCE(e.itemdescription,'') as itmdes,c.ServiceNo " +
"  FROM[MMS].[dbo].[Drug_Prescription] as a inner join[MMS].[dbo].[Patient_Detail] as b on a.PDID=b.PDID "+
" inner join[MMS].[dbo].[Patient] as c on c.PID=b.PID "+
" left join[MMS].[dbo].[DrugItems] as d on a.ItemNo=Convert(varchar, d.DrugID)  " +
"   left join[MMS].[dbo].[EPASPharmacyItems] as e on a.ItemNo=Convert(varchar, e.[itemno]) where Convert(Date, a.Date_Time)='"+ DateTime.Now.ToShortDateString() + "' and a.[IssuedLocID]='" + opdid + "' and a.[Issued]=1 and  a.[issuedQuantity]!='0'  " +
" order by c.ServiceNo ";

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


            List<DrugStockTransection> customers = null;

            DataTable dstIntData = new DataTable();
            DataTable odsvoltxndata = new DataTable();


            oSqlCommand = new SqlCommand();
            sqlQuery = " SELECT a.[issuedQuantity],a.[Date_Time],COALESCE(d.[ItemDescription], '') + COALESCE(e.itemdescription, '') as itmdes,c.ServiceNo " +
"  FROM[MMS].[dbo].[Drug_Prescription] as a inner join[MMS].[dbo].[Patient_Detail] as b on a.PDID=b.PDID " +
" inner join[MMS].[dbo].[Patient] as c on c.PID=b.PID " +
" left join[MMS].[dbo].[DrugItems] as d on a.ItemNo=Convert(varchar, d.DrugID)  " +
"   left join[MMS].[dbo].[EPASPharmacyItems] as e on a.ItemNo=Convert(varchar, e.[itemno]) where Convert(Date, a.Date_Time)='"+ searchText + "' and a.[IssuedLocID]='" + opdid+"' and a.[Issued]=1 and  a.[issuedQuantity]!='0'  " +
" order by c.ServiceNo ";
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