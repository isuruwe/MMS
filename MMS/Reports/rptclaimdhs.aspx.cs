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
    public partial class rptclaimdhs : System.Web.UI.Page
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
                string bid = "";
                int userid = Convert.ToInt32(Session["UserID"]);

                bid = (String)Session["bid"];
                opdid = (String)Session["userlocid1"];
                string searchText = string.Empty;

                if (Request.QueryString["datest"] != null)
                {
                    searchText = Request.QueryString["datest"].ToString();
                }

                List<DrugStockTransection> customers = null;

                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = " SELECT  b.Surname 	sname  , " +
    "  b.Rank rnkname, (c.ServiceNo)sno    , " +
    " b.Initials  inililes,  (c.pid)pid,sum(a.DHS_Amount)  amount " +
    "  ,(a.RegisterNo)RegisterNo  FROM[MMS].[dbo].[claim_detail] as a with(nolock) " +
    "    inner join[MMS].[dbo].  [Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] " +
    "	 as b on c.ServiceNo=  b.ServiceNo  and c.Service_Type=b.ServiceType left join[MMS].[dbo].[claim_batch] as d on d.claim_id " +
    "	 =a.claim_id where d.batchid='" + bid + "' and b.Surname!='0'  " +
    "	group by c.RelationshipType, b.Surname, c.surname, b.Rank, c.ServiceNo, b.Initials, c.pid, a.RegisterNo,b.RankID order by b.RankID desc";
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
            sqlQuery = " SELECT  COALESCE(NULLIF(max(case when c.RelationshipType = 1    and b.Surname != '0' " +
 " then b.Surname end), ''), max(c.surname))  	sname  ,max(case when c.RelationshipType " +
" = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno    ,max(case when c.RelationshipType = " +
" 1 then b.Initials  end)  inililes,  max(c.pid)  pid,max(a.Doc_Amount)  amount ,max(a.RegisterNo)  RegisterNo" +
" FROM[MMS].[dbo].[claim_detail] as a with(nolock)   left join[MMS].[dbo]. " +
" [Patient] as c on a.pid=c.pid left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo= " +
" b.ServiceNo left join[MMS].[dbo].[claim_batch] as d on d.claim_id=a.claim_id " +
" where d.batchid='" + searchText + "' ";
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