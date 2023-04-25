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
    public partial class rpt540 : System.Web.UI.Page
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

                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT        COUNT(b.Category_Type) AS cnt, b.Category_Type "+
" FROM Sick_Category AS a INNER JOIN "+
"                         Sick_CategoryType AS b ON a.CatID = b.CatID "+
" WHERE(a.LocID = 'CBO') AND(MONTH(a.Date) = 1) AND(YEAR(a.Date) = 2021) and a.CatID!=11 and a.CatID!=12 " +
" GROUP BY b.Category_Type";


                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                //DataTable odsvoltxndata = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT        COUNT(b.Category_Type) AS cnt, b.Category_Type ,max(a.LocID)as loc,max(datename(MONTH,dateadd(MONTH,MONTH(a.Date),-1)))as mnt,max(year(a.Date)) as yr " +
" FROM Sick_Category AS a INNER JOIN " +
"                         Sick_CategoryType AS b ON a.CatID = b.CatID " +
" WHERE(a.LocID = 'CBO') AND(MONTH(a.Date) = 1) AND(YEAR(a.Date) = 2021) and (a.CatID=11 or a.CatID=12) " +
" GROUP BY b.Category_Type";


                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(dstIntData);

                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT count(b.RelationshipType),count(case when b.RelationshipType != 1  then a.PDID end )as family " +
" ,count(case when b.RelationshipType = 1 and b.Service_Type = 3 then a.PDID end) as civil" +
" ,count(case when b.RelationshipType = 1 and(c.Service_Type = 1001 or c.Service_Type = 1002 or c.Service_Type = 1003 or c.Service_Type = 1004)" +
" then a.PDID end) as Officers " +
" ,count(case when b.RelationshipType = 1 and c.Service_Type = 1005 then a.PDID end) as Regularam" +
" ,count(case when b.RelationshipType = 1 and c.Service_Type = 1006 then a.PDID end) as Regularaw" +
" ,count(case when b.RelationshipType = 1 and c.Service_Type = 1007 then a.PDID end) as Volunteeram" +
" ,count(case when b.RelationshipType = 1 and c.Service_Type = 1008 then a.PDID end) as Volunteeraw" +
"   FROM[MMS].[dbo].[Patient_Detail] as a inner join Patient as b on a.pid = b.pid left join PersonalDetails as c on c.ServiceNo = b.ServiceNo" +
" left join Clinic_Master as d on a.OPDID = d.Clinic_ID " +
" where Present_Complain!= ''  AND(MONTH(a.createddate) = 1) AND(YEAR(a.createddate) = 2021) and"+
"  d.LocationID = 'cbo'";


                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(dstIntData2);

                //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
                ReportDataSource rdc2 = new ReportDataSource("DataSet2", dstIntData);
                ReportDataSource rdc3 = new ReportDataSource("DataSet3", dstIntData2);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.DataSources.Add(rdc2);
                ReportViewer1.LocalReport.DataSources.Add(rdc3);
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.DataBind();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string opdid = "";
            string locid = DropDownList1.SelectedValue;

            string userid = datest.Value;
            string[] arr = userid.Split('-');
            string yr = arr[0];
            string mt = arr[1];


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

            oSqlCommand = new SqlCommand();
            sqlQuery = " SELECT        COUNT(b.Category_Type) AS cnt, b.Category_Type " +
" FROM Sick_Category AS a INNER JOIN " +
"                         Sick_CategoryType AS b ON a.CatID = b.CatID " +
" WHERE(a.LocID = '"+locid+"') AND(MONTH(a.Date) = "+mt+") AND(YEAR(a.Date) = "+yr+") and a.CatID!=11 and a.CatID!=12 " +
" GROUP BY b.Category_Type";


            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);
            //DataTable odsvoltxndata = new DataTable();


            oSqlCommand = new SqlCommand();
            sqlQuery = " SELECT        COUNT(b.Category_Type) AS cnt, b.Category_Type ,max(a.LocID)as loc,max(datename(MONTH,dateadd(MONTH,MONTH(a.Date),-1)))as mnt,max(year(a.Date)) as yr " +
" FROM Sick_Category AS a INNER JOIN " +
"                         Sick_CategoryType AS b ON a.CatID = b.CatID " +
" WHERE(a.LocID = '" + locid + "') AND(MONTH(a.Date) =" + mt + ") AND(YEAR(a.Date) = " + yr + ") and (a.CatID=11 or a.CatID=12) " +
" GROUP BY b.Category_Type";


            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(dstIntData);

            oSqlCommand = new SqlCommand();
            sqlQuery = " SELECT count(b.RelationshipType),count(case when b.RelationshipType != 1  then a.PDID end )as family " +
" ,count(case when b.RelationshipType = 1 and b.Service_Type = 3 then a.PDID end) as civil" +
" ,count(case when b.RelationshipType = 1 and(c.Service_Type = 1001 or c.Service_Type = 1002 or c.Service_Type = 1003 or c.Service_Type = 1004)" +
" then a.PDID end) as Officers " +
" ,count(case when b.RelationshipType = 1 and c.Service_Type = 1005 then a.PDID end) as Regularam" +
" ,count(case when b.RelationshipType = 1 and c.Service_Type = 1006 then a.PDID end) as Regularaw" +
" ,count(case when b.RelationshipType = 1 and c.Service_Type = 1007 then a.PDID end) as Volunteeram" +
" ,count(case when b.RelationshipType = 1 and c.Service_Type = 1008 then a.PDID end) as Volunteeraw" +
"   FROM[MMS].[dbo].[Patient_Detail] as a inner join Patient as b on a.pid = b.pid left join PersonalDetails as c on c.ServiceNo = b.ServiceNo" +
" left join Clinic_Master as d on a.OPDID = d.Clinic_ID " +
" where Present_Complain!= ''  AND(MONTH(a.createddate) = " + mt + ") AND(YEAR(a.createddate) = " + yr + ") and" +
"  d.LocationID = '" + locid + "'";


            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(dstIntData2);

            //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
            ReportDataSource rdc2 = new ReportDataSource("DataSet2", dstIntData);
            ReportDataSource rdc3 = new ReportDataSource("DataSet3", dstIntData2);
            ReportViewer1.LocalReport.DataSources.Add(rdc);
            ReportViewer1.LocalReport.DataSources.Add(rdc2);
            ReportViewer1.LocalReport.DataSources.Add(rdc3);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }
    }
}