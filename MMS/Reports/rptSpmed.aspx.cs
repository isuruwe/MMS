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
    public partial class rptSpmed : System.Web.UI.Page
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

               
                string searchText = string.Empty;

                if (Request.QueryString["datest"] != null)
                {
                    searchText = Request.QueryString["datest"].ToString();
                }

                List<DrugStockTransection> customers = null;

                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = "select c.ServiceNo ,a.Present_Complain,c.Rank,c.Initials,c.Surname,a.CreatedDate, "+
                 "   d.Clinic_Detail from [dbo].[Patient_Detail]as a inner join [dbo].[Patient] as b on a.PID=b.PID  "+
        "    inner join  [dbo].[PersonalDetails] as c on b.Serviceno=c.ServiceNo inner join  [dbo].[Clinic_Master] "+
         "   as d on a.OPDID=d.Clinic_ID where b.Serviceno in(select svcid from[dbo].[Sp_List])  and convert "+
          "  (varchar, a.CreatedDate, 111)=convert(varchar,  GETDATE(), 111)order by a.CreatedDate desc";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.DataBind();
               
                




            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string searchText = string.Empty;

            if (Request.Params["datest"] != null)
            {
                searchText = Request.Params["datest"].ToString();
            }


            DataTable dstIntData = new DataTable();
            DataTable odsvoltxndata = new DataTable();


            oSqlCommand = new SqlCommand();
            sqlQuery = "select c.ServiceNo ,a.Present_Complain,c.Rank,c.Initials,c.Surname,a.CreatedDate, " +
                   "   d.Clinic_Detail from [dbo].[Patient_Detail]as a inner join [dbo].[Patient] as b on a.PID=b.PID  " +
          "    inner join  [dbo].[PersonalDetails] as c on b.Serviceno=c.ServiceNo inner join  [dbo].[Clinic_Master] " +
           "   as d on a.OPDID=d.Clinic_ID where b.Serviceno in(select svcid from[dbo].[Sp_List])  and  " +
            "  cast(a.CreatedDate as date)=cast('"+searchText+"' as date) order by a.CreatedDate desc";
            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
            ReportViewer1.LocalReport.DataSources.Add(rdc);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();

        }
    }
}