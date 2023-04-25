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
    public partial class rptdhspftsick : System.Web.UI.Page
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

                if (Request.QueryString["svcno"] != null)
                {
                    searchText = Request.QueryString["svcno"].ToString();
                }




                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = " select a.serviceno,d.Category_Type,c.CatPeriod,c.Date  from [dbo].[Patient] as a inner join [dbo].[Patient_Detail] as b on a.pid=b.pid inner join "+
" [dbo].[Sick_Category] as c on c.PDID = b.PDID inner join[dbo].[Sick_CategoryType] as d on d.CatID=c.CatID where a.ServiceNo='"+svcno+"' and c.date > DATEADD(year,-1,GETDATE()) order by c.date";


                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);


                DataTable odsvoltxndata1 = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = "  select a.serviceno,d.Category_Type,c.CatPeriod,c.Date,f.datedischarge,f.dateadmit  from [dbo].[Patient] as a inner join [dbo].[Patient_Detail] as b "+
 " on a.pid = b.pid inner join[dbo].[Ward_Details] as e left join[dbo].[ward_discharge] as f on e.PDID=f.PDID "+
" on b.pdid=e.pdid inner join[dbo].[Ward_Sick_Category] as c on c.PDID = e.wdid inner join[dbo].[Sick_CategoryType] as d on "+
 "  d.CatID=c.CatID where a.ServiceNo='"+svcno+"' and c.date > DATEADD(year,-1, GETDATE()) "+
 "   order by c.date";


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


            searchText = svcno.Value;

            String[] mnt = searchText.Split('-');
           // int days = DateTime.DaysInMonth(Convert.ToInt32(mnt[0]), Convert.ToInt32(mnt[1]));

            List<DrugStockTransection> customers = null;

            DataTable dstIntData = new DataTable();
            DataTable odsvoltxndata = new DataTable();


            oSqlCommand = new SqlCommand();

            sqlQuery = " select a.serviceno,d.Category_Type,c.CatPeriod,c.Date  from [dbo].[Patient] as a inner join [dbo].[Patient_Detail] as b on a.pid=b.pid inner join " +
     " [dbo].[Sick_Category] as c on c.PDID = b.PDID inner join[dbo].[Sick_CategoryType] as d on d.CatID=c.CatID where a.ServiceNo='" + searchText + "' and c.date > DATEADD(year,-1,GETDATE()) order by c.date";


            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);

            DataTable odsvoltxndata1 = new DataTable();


            oSqlCommand = new SqlCommand();
            sqlQuery = "  select a.serviceno,d.Category_Type,c.CatPeriod,c.Date,f.datedischarge,f.dateadmit  from [dbo].[Patient] as a inner join [dbo].[Patient_Detail] as b " +
" on a.pid = b.pid inner join[dbo].[Ward_Details] as e left join[dbo].[ward_discharge] as f on e.PDID=f.PDID " +
" on b.pdid=e.pdid inner join[dbo].[Ward_Sick_Category] as c on c.PDID = e.wdid inner join[dbo].[Sick_CategoryType] as d on " +
"  d.CatID=c.CatID where a.ServiceNo='" + searchText + "' and c.date > DATEADD(year,-1, GETDATE()) " +
"   order by c.date";


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