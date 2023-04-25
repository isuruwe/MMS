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
    public partial class rptpharmexp : System.Web.UI.Page
    {
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        public string sqlQuery;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["EPASContext"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string opdid = "";
                string locid = "";

                int userid = Convert.ToInt32(Session["UserID"]);


                opdid = (String)Session["userloc"];
                string searchText = string.Empty;

                //if (Request.QueryString["pdid"] != null)
                //{
                //    searchText = Request.QueryString["pdid"].ToString();
                //}

              

                DataTable odsvoltxndata = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = "select *    FROM [EPAS].[dbo].[MedicalItemDetailSnMD] where [LocationID]='"+ opdid + "'and ExpiryDate is not null and ExpiryDate >=(GETDATE()) and ExpiryDate < (GETDATE()+30) "+
"  order by ExpiryDate ";
                oSqlCommand.Connection = new SqlConnection(conStr);
                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
              



                /////////////////////////////////////////////////////////////////

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


            opdid = (String)Session["userloc"];
            string searchText = string.Empty;

            //if (Request.QueryString["pdid"] != null)
            //{
            //    searchText = Request.QueryString["pdid"].ToString();
            //}



            DataTable odsvoltxndata = new DataTable();


            oSqlCommand = new SqlCommand();
            sqlQuery = "select *    FROM [EPAS].[dbo].[MedicalItemDetailSnMD] where [LocationID]='" + opdid + "'and ExpiryDate is not null and ExpiryDate >=(GETDATE()) and ExpiryDate < (GETDATE()+30) " +
"  order by ExpiryDate ";
            oSqlCommand.Connection = new SqlConnection(conStr);
            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);




            /////////////////////////////////////////////////////////////////

            //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);




            ReportViewer1.LocalReport.DataSources.Add(rdc);


            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string opdid = "";
            string locid = "";

            int userid = Convert.ToInt32(Session["UserID"]);


            opdid = (String)Session["userloc"];
            string searchText = string.Empty;

            //if (Request.QueryString["pdid"] != null)
            //{
            //    searchText = Request.QueryString["pdid"].ToString();
            //}



            DataTable odsvoltxndata = new DataTable();


            oSqlCommand = new SqlCommand();
            sqlQuery = "select *    FROM [EPAS].[dbo].[MedicalItemDetailSnMD] where [LocationID]='" + opdid + "'and ExpiryDate is not null and ExpiryDate >=(GETDATE()-30) and ExpiryDate < (GETDATE()) " +
"  order by ExpiryDate desc ";
            oSqlCommand.Connection = new SqlConnection(conStr);
            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);




            /////////////////////////////////////////////////////////////////

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