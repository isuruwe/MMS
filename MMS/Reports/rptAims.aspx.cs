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
    public partial class rptAims : System.Web.UI.Page
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
                string id = string.Empty;


                  id = datest.Value;
              //  id = "2020-02-05";

                List<DrugStockTransection> customers = null;

                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();
                DataTable odsvoltxndata1 = new DataTable();
                DataTable odsvoltxndata2 = new DataTable();
                DataTable odsvoltxndata3 = new DataTable();
                DataTable odsvoltxndata4 = new DataTable();
                DataTable odsvoltxndata5 = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT distinct  b.LocationID as t ,count(a.ModifiedDate) as r  " +
"  FROM[MMS].[dbo].[Patient_Detail] as a inner join[MMS].[dbo].[Clinic_Master] as b on a.OPDID=b.Clinic_ID where   a.PatientCatID = 1 and convert(date, a.ModifiedDate)= CONVERT(varchar,'" + id + "',111)  and " +
"(a.PDID in (select distinct pdid from[MMS].[dbo].[Lab_Report] where PDID = a.PDID  and convert(date, RequestedTime) =CONVERT(varchar,'" + id + "',111)) or " +
"a.PDID in (select distinct pdid from[MMS].[dbo].[Drug_Prescription] where PDID = a.PDID    and convert(date, Date_Time) =CONVERT(varchar,'" + id + "',111) ) or a.PDID in  " +
" (select distinct pdid from[MMS].[dbo].[Sick_Category] where PDID = a.PDID    and convert(date, Date) =CONVERT(varchar,'" + id + "',111))or a.PDID in  " +
"  (select distinct pdid from[MMS].[dbo].[CatDiagList] where PDID = a.PDID))  " +
"  group by   b.LocationID order by b.LocationID";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                oSqlCommand = new SqlCommand();
                sqlQuery =" SELECT  distinct b.LocationID as t ,count(a.PDID) as r "+
"  FROM[MMS].[dbo].[Patient_Detail] as a inner join[MMS].[dbo].[Clinic_Master] as b on a.OPDID=b.Clinic_ID where a.PatientCatID=1 and " +
" convert(date, a.CreatedDate) =CONVERT(varchar,'" + id + "',111) group by    b.LocationID order by b.LocationID";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata1);
                oSqlCommand = new SqlCommand();
                sqlQuery = "    ;WITH CTE as " +
"(" +
" SELECT   RequestedLocID as t, count(pdid) as r " +
"   FROM[MMS].[dbo].[Drug_Prescription] " +
"   where   convert(date, Date_Time) = CONVERT(varchar, '" + id + "', 111)  " +
"  group by   RequestedLocID, pdid " +
"  ) " +
" SELECT t, count(*) r " +
" FROM CTE " +
" GROUP BY t order by t";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata2);
                oSqlCommand = new SqlCommand();
                sqlQuery = "    ;WITH CTE as " +
"(" +
" SELECT   RequestedLocID as t, count(pdid) as r " +
"   FROM[MMS].[dbo].[Drug_Prescription] " +
"   where   convert(date, Date_Time) = CONVERT(varchar, '" + id + "', 111) and Issued='1' " +
"  group by   RequestedLocID, pdid " +
"  ) " +
" SELECT t, count(*) r " +
" FROM CTE " +
" GROUP BY t order by t";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata3);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  ;WITH CTE as " +
"(" +
" SELECT   RequestedLocID as t, count(TestSID) as r " +
"   FROM[MMS].[dbo].[Lab_Report] " +
 "  where   convert(date,[RequestedTime]) = CONVERT(varchar, '" + id + "', 111)  " +
"  group by   RequestedLocID, TestSID " +
"  ) " +
" SELECT t, count(*) r " +
" FROM CTE " +
" GROUP BY t order by t";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata4);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  ;WITH CTE as " +
"(" +
" SELECT   RequestedLocID as t, count(TestSID) as r " +
"   FROM[MMS].[dbo].[Lab_Report] " +
 "  where   convert(date,[RequestedTime]) = CONVERT(varchar, '" + id + "', 111) and Issued='1'  " +
"  group by   RequestedLocID, TestSID " +
"  ) " +
" SELECT t, count(*) r " +
" FROM CTE " +
" GROUP BY t order by t";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata5);

                //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportDataSource rdc1 = new ReportDataSource("DataSet2", odsvoltxndata1);
                ReportViewer1.LocalReport.DataSources.Add(rdc1);
                ReportDataSource rdc2 = new ReportDataSource("DataSet3", odsvoltxndata2);
                ReportViewer1.LocalReport.DataSources.Add(rdc2);
                ReportDataSource rdc3 = new ReportDataSource("DataSet4", odsvoltxndata3);
                ReportViewer1.LocalReport.DataSources.Add(rdc3);
                ReportDataSource rdc4 = new ReportDataSource("DataSet5", odsvoltxndata4);
                ReportViewer1.LocalReport.DataSources.Add(rdc4);
                ReportDataSource rdc5 = new ReportDataSource("DataSet6", odsvoltxndata5);
                ReportViewer1.LocalReport.DataSources.Add(rdc5);
              
              
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
                string id = string.Empty;


                 id = datest.Value;
                //id = "2020-02-05";

                List<DrugStockTransection> customers = null;

                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();
                DataTable odsvoltxndata1 = new DataTable();
                DataTable odsvoltxndata2 = new DataTable();
                DataTable odsvoltxndata3 = new DataTable();
                DataTable odsvoltxndata4 = new DataTable();
                DataTable odsvoltxndata5 = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT distinct  b.LocationID as t ,count(a.ModifiedDate) as r  " +
"  FROM[MMS].[dbo].[Patient_Detail] as a inner join[MMS].[dbo].[Clinic_Master] as b on a.OPDID=b.Clinic_ID where   a.PatientCatID = 1 and convert(date, a.ModifiedDate)= CONVERT(varchar,'" + id + "',111)  and " +
"(a.PDID in (select distinct pdid from[MMS].[dbo].[Lab_Report] where PDID = a.PDID  and convert(date, RequestedTime) =CONVERT(varchar,'" + id + "',111)) or " +
"a.PDID in (select distinct pdid from[MMS].[dbo].[Drug_Prescription] where PDID = a.PDID    and convert(date, Date_Time) =CONVERT(varchar,'" + id + "',111) ) or a.PDID in  " +
" (select distinct pdid from[MMS].[dbo].[Sick_Category] where PDID = a.PDID    and convert(date, Date) =CONVERT(varchar,'" + id + "',111))or a.PDID in  " +
"  (select distinct pdid from[MMS].[dbo].[CatDiagList] where PDID = a.PDID))  " +
"  group by   b.LocationID order by b.LocationID";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT  distinct b.LocationID as t ,count(a.PDID) as r " +
"  FROM[MMS].[dbo].[Patient_Detail] as a inner join[MMS].[dbo].[Clinic_Master] as b on a.OPDID=b.Clinic_ID where a.PatientCatID=1 and " +
" convert(date, a.CreatedDate) =CONVERT(varchar,'" + id + "',111) group by    b.LocationID order by b.LocationID";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata1);
                oSqlCommand = new SqlCommand();
                sqlQuery = "    ;WITH CTE as " +
"(" +
" SELECT   RequestedLocID as t, count(pdid) as r " +
"   FROM[MMS].[dbo].[Drug_Prescription] " +
"   where   convert(date, Date_Time) = CONVERT(varchar, '" + id + "', 111)  " +
"  group by   RequestedLocID, pdid " +
"  ) " +
" SELECT t, count(*) r " +
" FROM CTE " +
" GROUP BY t order by t";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata2);
                oSqlCommand = new SqlCommand();
                sqlQuery = "    ;WITH CTE as " +
"(" +
" SELECT   RequestedLocID as t, count(pdid) as r " +
"   FROM[MMS].[dbo].[Drug_Prescription] " +
"   where   convert(date, Date_Time) = CONVERT(varchar, '" + id + "', 111) and Issued='1' " +
"  group by   RequestedLocID, pdid " +
"  ) " +
" SELECT t, count(*) r " +
" FROM CTE " +
" GROUP BY t order by t";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata3);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  ;WITH CTE as " +
"(" +
" SELECT   RequestedLocID as t, count(TestSID) as r " +
"   FROM[MMS].[dbo].[Lab_Report] " +
 "  where   convert(date,[RequestedTime]) = CONVERT(varchar, '" + id + "', 111)  " +
"  group by   RequestedLocID, TestSID " +
"  ) " +
" SELECT t, count(*) r " +
" FROM CTE " +
" GROUP BY t order by t";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata4);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  ;WITH CTE as " +
"(" +
" SELECT   RequestedLocID as t, count(TestSID) as r " +
"   FROM[MMS].[dbo].[Lab_Report] " +
 "  where   convert(date,[RequestedTime]) = CONVERT(varchar, '" + id + "', 111) and Issued='1'  " +
"  group by   RequestedLocID, TestSID " +
"  ) " +
" SELECT t, count(*) r " +
" FROM CTE " +
" GROUP BY t order by t";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata5);

                //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportDataSource rdc1 = new ReportDataSource("DataSet2", odsvoltxndata1);
                ReportViewer1.LocalReport.DataSources.Add(rdc1);
                ReportDataSource rdc2 = new ReportDataSource("DataSet3", odsvoltxndata2);
                ReportViewer1.LocalReport.DataSources.Add(rdc2);
                ReportDataSource rdc3 = new ReportDataSource("DataSet4", odsvoltxndata3);
                ReportViewer1.LocalReport.DataSources.Add(rdc3);
                ReportDataSource rdc4 = new ReportDataSource("DataSet5", odsvoltxndata4);
                ReportViewer1.LocalReport.DataSources.Add(rdc4);
                ReportDataSource rdc5 = new ReportDataSource("DataSet6", odsvoltxndata5);
                ReportViewer1.LocalReport.DataSources.Add(rdc5);


                ReportViewer1.LocalReport.DataSources.Add(rdc1);
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.DataBind();
            
        }
    }
}