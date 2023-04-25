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
    public partial class rptmedsr : System.Web.UI.Page
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
                   
                    List<DrugStockTransection> customers = null;


                    DataTable dstIntData = new DataTable();
                    DataTable odsvoltxndata = new DataTable();
                   
                    if (Request.Params["datest"] != null)
                    {
                        searchText = Request.Params["datest"].ToString();
                    }
                    

                    oSqlCommand = new SqlCommand();

                sqlQuery = "SELECT a.createddate,a.msstation,d.ServiceNo, d.Surname,d.Rank,d.Initials,case when a.msfitness = 1 then 'Fit' else  'Unfit' end as med FROM  " +
" [dbo].[MedicalScreen] as a inner join[dbo].[Patient_Detail] as b on a.pdid=b.PDID " +
" inner join[dbo].[Patient] as c on b.PID=c.PID inner join[dbo].[PersonalDetails] as d on c.ServiceNo=d.ServiceNo " +
" where  a.status=2 and a.msstation='" + DropDownList1.SelectedValue.ToString() + "' and convert(date, a.createddate)=convert(varchar,'" + DateTime.Now.Date + "',111)";


                oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(odsvoltxndata);
                    oSqlConnection.Close();
                    ////////////////////////////////////////////////
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
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;

                string searchText = string.Empty;

            DataTable odsvoltxndata = new DataTable();

            if (Request.Params["datest"] != null)
                {
                    searchText = Request.Params["datest"].ToString();
                }
               

                oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT a.createddate,a.msstation,d.ServiceNo, d.Surname,d.Rank,d.Initials,case when a.msfitness = 1 then 'Fit' else  'Unfit' end as med FROM  " +
" [dbo].[MedicalScreen] as a inner join[dbo].[Patient_Detail] as b on a.pdid=b.PDID " +
" inner join[dbo].[Patient] as c on b.PID=c.PID inner join[dbo].[PersonalDetails] as d on c.ServiceNo=d.ServiceNo " +
" where  a.status=2 and a.msstation='" + DropDownList1.SelectedValue.ToString() + "' and convert(date, a.createddate)=convert(varchar,'"+ searchText + "',111)";

            

                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                oSqlConnection.Close();
             
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