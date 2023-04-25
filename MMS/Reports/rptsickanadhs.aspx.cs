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
    public partial class rptsickanadhs : System.Web.UI.Page
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
                oSqlCommand = new SqlCommand();
                if (searchText == "CBO" || searchText == "AHQ")
                {
                    sqlQuery = " SELECT    c.Category_Type " +
    " ,count(case when b.[isliveout] = 'In' then b.[isliveout]  end) liveIn " +
    " ,count(case when b.[isliveout] = 'Out' then b.[isliveout]  end) livOut " +
    " ,count(b.[isliveout]) as tt " +
 " FROM[MMS].[dbo].[Sick_Category] as a inner join[MMS].[dbo].[SickReport] as b on a.pdid=b.pdid inner join " +
" [MMS].[dbo].[Sick_CategoryType] as c on a.[CatID]=c.CatID " +
" where convert(date, b.[regdate]) between CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText + "',111) and (b.locationid='" + searchText + "'or SickReport.LocationID='AHQ')  group by c.Category_Type";
                }
                else
                {
                    sqlQuery = " SELECT    c.Category_Type " +
   " ,count(case when b.[isliveout] = 'In' then b.[isliveout]  end) liveIn " +
   " ,count(case when b.[isliveout] = 'Out' then b.[isliveout]  end) livOut " +
   " ,count(b.[isliveout]) as tt " +
" FROM[MMS].[dbo].[Sick_Category] as a inner join[MMS].[dbo].[SickReport] as b on a.pdid=b.pdid inner join " +
" [MMS].[dbo].[Sick_CategoryType] as c on a.[CatID]=c.CatID " +
" where convert(date, b.[regdate]) between CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText + "',111) and b.locationid='" + searchText + "'  group by c.Category_Type";
                }
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;

                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                oSqlConnection.Close();
                ////////////////////////////////////////////////
                sqlQuery = " SELECT    d.dgdetail  ,count(case when b.[isliveout] = 'In' then b.[isliveout]  end) liveIn  " +
" ,count(case when b.[isliveout] = 'Out' then b.[isliveout]  end) livOut " +
"  ,count(case when b.[isliveout] IS NULL and e.ServiceNo not like'%c%' and e.RelationshipType = 1 then isnull(b.[isliveout], 1)  end) retired , " +
"  count(case when e.ServiceNo like'%c%' then d.dgdetail  end) Civil  , " +
"   count(case when e.RelationshipType != 1 then  e.RelationshipType  end) Families  , " +
"  count(a.PDID) as tt " +
"   FROM[MMS].[dbo].[Patient_Detail] as a left join[MMS].[dbo].[SickReport] as b on a.PDID=b.PDID inner join " +
" [MMS].[dbo].[CatDiagList] as c on a.PDID=c.PDID " +
" inner join[MMS].[dbo].[CatDaignosis] as d on d.dgid=c.dgid " +
" inner join[MMS].[dbo].Patient as e on e.pid=a.pid " +
" inner join[MMS].[dbo].[Clinic_Master] as f on f.Clinic_ID=a.OPDID " +
" where convert(date, a.[CreatedDate]) between " +
" CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111)" +

"    and f.LocationID='" + DropDownList1.SelectedValue.ToString() + "'  group by d.dgdetail order by d.dgdetail asc";

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;

                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata2);
                oSqlConnection.Close();
                /////////////////////////////////////////////////////////////////
                if (searchText == "CBO" || searchText == "AHQ")
                {
                    sqlQuery = " select (select count(svcid)/30 as avgr from  [dbo].[SickReport] where regdate between " +
     " CONVERT(varchar, '" + searchText + "', 111) and CONVERT(varchar,'" + searchText1 + "', 111)and (LocationID = '" + DropDownList1.SelectedValue.ToString() + "' or LocationID='AHQ') )as sta," +
    " (select count(a.CatID) FROM[MMS].[dbo].[Sick_Category] as a inner join[MMS].[dbo].[SickReport] as b on a.pdid=b.pdid inner join" +
    " [MMS].[dbo].[Sick_CategoryType] as c on a.[CatID]=c.CatID" +
    " where b.regdate between" +
    " CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111) and(a.CatID= 4 or a.CatID= 5 or a.CatID= 11 or a.CatID= 12) and (b.LocationID='" + DropDownList1.SelectedValue.ToString() + "' or b.LocationID='AHQ')" +
    " )as stb";

                }
                else
                {
                    sqlQuery = " select (select count(svcid)/30 as avgr from  [dbo].[SickReport] where regdate between " +
    " CONVERT(varchar, '" + searchText + "', 111) and CONVERT(varchar,'" + searchText1 + "', 111)and LocationID = '" + DropDownList1.SelectedValue.ToString() + "' )as sta," +
    " (select count(a.CatID) FROM[MMS].[dbo].[Sick_Category] as a inner join[MMS].[dbo].[SickReport] as b on a.pdid=b.pdid inner join" +
    " [MMS].[dbo].[Sick_CategoryType] as c on a.[CatID]=c.CatID" +
    " where b.regdate between" +
    " CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111) and(a.CatID= 4 or a.CatID= 5 or a.CatID= 11 or a.CatID= 12) and b.LocationID='" + DropDownList1.SelectedValue.ToString() + "'" +
    " )as stb";
                }
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata3);
                oSqlConnection.Close();


                //////////////////////////////////////////////////////////////////
                //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
                ReportDataSource rdc1 = new ReportDataSource("DataSet2", odsvoltxndata2);
                ReportDataSource rdc3 = new ReportDataSource("DataSet3", odsvoltxndata3);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.DataSources.Add(rdc1);
                ReportViewer1.LocalReport.DataSources.Add(rdc3);
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
            if (Request.Params["datest"] != null)
            {
                searchText = Request.Params["datest"].ToString();
            }
            if (Request.Params["datest1"] != null)
            {
                searchText1 = Request.Params["datest1"].ToString();
            }

            oSqlCommand = new SqlCommand();
            if (searchText == "CBO" || searchText == "AHQ")
            {
                sqlQuery = " SELECT    c.Category_Type " +
" ,count(case when b.[isliveout] = 'In' then b.[isliveout]  end) liveIn " +
" ,count(case when b.[isliveout] = 'Out' then b.[isliveout]  end) livOut " +
" ,count(b.[isliveout]) as tt " +
" FROM[MMS].[dbo].[Sick_Category] as a inner join[MMS].[dbo].[SickReport] as b on a.pdid=b.pdid inner join " +
" [MMS].[dbo].[Sick_CategoryType] as c on a.[CatID]=c.CatID " +
" where convert(date, b.[regdate]) between CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111) and (b.locationid='" + DropDownList1.SelectedValue.ToString() + "'or SickReport.LocationID='AHQ')  group by c.Category_Type";
            }
            else
            {
                sqlQuery = " SELECT    c.Category_Type " +
" ,count(case when b.[isliveout] = 'In' then b.[isliveout]  end) liveIn " +
" ,count(case when b.[isliveout] = 'Out' then b.[isliveout]  end) livOut " +
" ,count(b.[isliveout]) as tt " +
" FROM[MMS].[dbo].[Sick_Category] as a inner join[MMS].[dbo].[SickReport] as b on a.pdid=b.pdid inner join " +
" [MMS].[dbo].[Sick_CategoryType] as c on a.[CatID]=c.CatID " +
" where convert(date, b.[regdate]) between CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111) and b.locationid='" + DropDownList1.SelectedValue.ToString() + "'  group by c.Category_Type";
            }
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);
            oSqlConnection.Close();
            ////////////////////////////////////////////////
            sqlQuery = " SELECT    d.dgdetail  ,count(case when b.[isliveout] = 'In' then b.[isliveout]  end) liveIn  " +
 " ,count(case when b.[isliveout] = 'Out' then b.[isliveout]  end) livOut " +
"  ,count(case when b.[isliveout] IS NULL and e.ServiceNo not like'%c%' and e.RelationshipType = 1 then isnull(b.[isliveout], 1)  end) retired , " +
"  count(case when e.ServiceNo like'%c%' then d.dgdetail  end) Civil  , " +
"   count(case when e.RelationshipType != 1 then  e.RelationshipType  end) Families  , " +
"  count(a.PDID) as tt " +
"   FROM[MMS].[dbo].[Patient_Detail] as a left join [MMS].[dbo].[SickReport] as b on a.PDID=b.PDID inner join " +
" [MMS].[dbo].[CatDiagList] as c on a.PDID=c.PDID " +
" inner join[MMS].[dbo].[CatDaignosis] as d on d.dgid=c.dgid " +
" inner join[MMS].[dbo].Patient as e on e.pid=a.pid " +
" inner join[MMS].[dbo].[Clinic_Master] as f on f.Clinic_ID=a.OPDID " +
" where convert(date, a.[CreatedDate]) between " +
" CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111)" +

"    and f.LocationID='" + DropDownList1.SelectedValue.ToString() + "'  group by d.dgdetail order by d.dgdetail asc";

            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata2);
            oSqlConnection.Close();
            /////////////////////////////////////////////////////////////////
            if (searchText == "CBO" || searchText == "AHQ")
            {
                sqlQuery = " select (select count(svcid)/30 as avgr from  [dbo].[SickReport] where regdate between " +
 " CONVERT(varchar, '" + searchText + "', 111) and CONVERT(varchar,'" + searchText1 + "', 111)and (LocationID = '" + DropDownList1.SelectedValue.ToString() + "' or LocationID='AHQ') )as sta," +
" (select count(a.CatID) FROM[MMS].[dbo].[Sick_Category] as a inner join[MMS].[dbo].[SickReport] as b on a.pdid=b.pdid inner join" +
" [MMS].[dbo].[Sick_CategoryType] as c on a.[CatID]=c.CatID" +
" where b.regdate between" +
" CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111) and(a.CatID= 4 or a.CatID= 5 or a.CatID= 11 or a.CatID= 12) and (b.LocationID='" + DropDownList1.SelectedValue.ToString() + "' or b.LocationID='AHQ')" +
" )as stb";

            }
            else
            {
                sqlQuery = " select (select count(svcid)/30 as avgr from  [dbo].[SickReport] where regdate between " +
" CONVERT(varchar, '" + searchText + "', 111) and CONVERT(varchar,'" + searchText1 + "', 111)and LocationID = '" + DropDownList1.SelectedValue.ToString() + "' )as sta," +
" (select count(a.CatID) FROM[MMS].[dbo].[Sick_Category] as a inner join[MMS].[dbo].[SickReport] as b on a.pdid=b.pdid inner join" +
" [MMS].[dbo].[Sick_CategoryType] as c on a.[CatID]=c.CatID" +
" where b.regdate between" +
" CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111) and(a.CatID= 4 or a.CatID= 5 or a.CatID= 11 or a.CatID= 12) and b.LocationID='" + DropDownList1.SelectedValue.ToString() + "'" +
" )as stb";
            }
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata3);
            oSqlConnection.Close();

            //////////////////////////////////////////////////////////////////
            //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
            ReportDataSource rdc1 = new ReportDataSource("DataSet2", odsvoltxndata2);
            ReportDataSource rdc2 = new ReportDataSource("DataSet3", odsvoltxndata3);
            ReportViewer1.LocalReport.DataSources.Add(rdc);
            ReportViewer1.LocalReport.DataSources.Add(rdc1);
            ReportViewer1.LocalReport.DataSources.Add(rdc2);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;

            string searchText = string.Empty;
            string searchText1 = string.Empty;
            DataTable dstIntData = new DataTable();
            DataTable odsvoltxndata = new DataTable();
            DataTable odsvoltxndata2 = new DataTable();
            DataTable odsvoltxndata3 = new DataTable();
            if (Request.Params["datest"] != null)
            {
                searchText = Request.Params["datest"].ToString();
            }
            if (Request.Params["datest1"] != null)
            {
                searchText1 = Request.Params["datest1"].ToString();
            }

            oSqlCommand = new SqlCommand();
           
                sqlQuery = " SELECT    c.Category_Type " +
" ,count(case when b.[isliveout] = 'In' then b.[isliveout]  end) liveIn " +
" ,count(case when b.[isliveout] = 'Out' then b.[isliveout]  end) livOut " +
" ,count(b.[isliveout]) as tt " +
" FROM[MMS].[dbo].[Sick_Category] as a inner join[MMS].[dbo].[SickReport] as b on a.pdid=b.pdid inner join " +
" [MMS].[dbo].[Sick_CategoryType] as c on a.[CatID]=c.CatID " +
" where convert(date, b.[regdate]) between CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111)  group by c.Category_Type";
            
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);
            oSqlConnection.Close();
            ////////////////////////////////////////////////
            sqlQuery = " SELECT    d.dgdetail  ,count(case when b.[isliveout] = 'In' then b.[isliveout]  end) liveIn  " +
 " ,count(case when b.[isliveout] = 'Out' then b.[isliveout]  end) livOut " +
"  ,count(case when b.[isliveout] IS NULL and e.ServiceNo not like'%c%' and e.RelationshipType = 1 then isnull(b.[isliveout], 1)  end) retired , " +
"  count(case when e.ServiceNo like'%c%' then d.dgdetail  end) Civil  , " +
"   count(case when e.RelationshipType != 1 then  e.RelationshipType  end) Families  , " +
"  count(a.PDID) as tt " +
"   FROM[MMS].[dbo].[Patient_Detail] as a left join [MMS].[dbo].[SickReport] as b on a.PDID=b.PDID inner join " +
" [MMS].[dbo].[CatDiagList] as c on a.PDID=c.PDID " +
" inner join[MMS].[dbo].[CatDaignosis] as d on d.dgid=c.dgid " +
" inner join[MMS].[dbo].Patient as e on e.pid=a.pid " +
" inner join[MMS].[dbo].[Clinic_Master] as f on f.Clinic_ID=a.OPDID " +
" where convert(date, a.[CreatedDate]) between " +
" CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111)" +

"     group by d.dgdetail order by d.dgdetail asc";

            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata2);
            oSqlConnection.Close();
            /////////////////////////////////////////////////////////////////
         
                sqlQuery = " select (select count(svcid)/30 as avgr from  [dbo].[SickReport] where regdate between " +
" CONVERT(varchar, '" + searchText + "', 111) and CONVERT(varchar,'" + searchText1 + "', 111) )as sta," +
" (select count(a.CatID) FROM[MMS].[dbo].[Sick_Category] as a inner join[MMS].[dbo].[SickReport] as b on a.pdid=b.pdid inner join" +
" [MMS].[dbo].[Sick_CategoryType] as c on a.[CatID]=c.CatID" +
" where b.regdate between" +
" CONVERT(varchar,'" + searchText + "',111) and CONVERT(varchar,'" + searchText1 + "',111) and(a.CatID= 4 or a.CatID= 5 or a.CatID= 11 or a.CatID= 12) " +
" )as stb";
            
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata3);
            oSqlConnection.Close();

            //////////////////////////////////////////////////////////////////
            //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
            ReportDataSource rdc1 = new ReportDataSource("DataSet2", odsvoltxndata2);
            ReportDataSource rdc2 = new ReportDataSource("DataSet3", odsvoltxndata3);
            ReportViewer1.LocalReport.DataSources.Add(rdc);
            ReportViewer1.LocalReport.DataSources.Add(rdc1);
            ReportViewer1.LocalReport.DataSources.Add(rdc2);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();

        }
    }
}