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
    public partial class rptDailyIssue : System.Web.UI.Page
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

                List<DrugStockTransection> customers = null;
                
                    DataTable dstIntData = new DataTable();
                    DataTable odsvoltxndata = new DataTable();
                    

                     oSqlCommand = new SqlCommand();
                    sqlQuery = "SELECT        MAX(DrugStockTransection.TransectionID) AS Expr3, MAX(DrugStockTransection.StockID) AS Expr4, MAX(DrugStockTransection.IssuedTo) AS Expr5, MAX(DrugStockTransection.IssuedDate) AS Expr6, "+
                    "     MAX(DrugStockTransection.BatchID) AS Expr7, SUM(DrugStockTransection.TransectionQty)AS tot, MAX(DrugStockTransection.IssuedUser) AS Expr8, MAX(DrugStockTransection.InLoc) AS Expr9, " +
                  "      max(CONCAT(EPASPharmacyItems.itemdescription,DrugItems.ItemDescription)) AS Expr1, MAX(Clinic_Master.Clinic_Detail) AS Expr11, MAX(Clinic_Master_1.Clinic_Detail) AS Expr2, DrugStockTransection.ItemID " +
" FROM            DrugStockTransection LEFT OUTER JOIN " +
                     "    Clinic_Master ON DrugStockTransection.InLoc = Clinic_Master.Clinic_ID LEFT OUTER JOIN " +
                     "    Clinic_Master AS Clinic_Master_1 ON DrugStockTransection.OutLoc = Clinic_Master_1.Clinic_ID LEFT OUTER JOIN " +
                     "    DrugItems ON DrugStockTransection.ItemID = CAST(DrugItems.DrugID AS nvarchar(50)) LEFT OUTER JOIN " +
                    "     EPASPharmacyItems ON DrugStockTransection.ItemID = EPASPharmacyItems.itemno " +
" where DrugStockTransection.OutLoc='"+opdid+ "' and DrugStockTransection.TransectionQty!=0 GROUP BY DrugStockTransection.ItemID " +
" ORDER BY DrugStockTransection.ItemID";
                    oSqlCommand.Connection =  new SqlConnection(conStr);


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
            sqlQuery = "SELECT        MAX(DrugStockTransection.TransectionID) AS Expr3, MAX(DrugStockTransection.StockID) AS Expr4, MAX(DrugStockTransection.IssuedTo) AS Expr5, MAX(DrugStockTransection.IssuedDate) AS Expr6, " +
            "     MAX(DrugStockTransection.BatchID) AS Expr7, SUM(DrugStockTransection.TransectionQty)AS tot, MAX(DrugStockTransection.IssuedUser) AS Expr8, MAX(DrugStockTransection.InLoc) AS Expr9, " +
          "        max(CONCAT(EPASPharmacyItems.itemdescription,DrugItems.ItemDescription)) AS Expr1, MAX(Clinic_Master.Clinic_Detail) AS Expr11, MAX(Clinic_Master_1.Clinic_Detail) AS Expr2, DrugStockTransection.ItemID " +
" FROM            DrugStockTransection LEFT OUTER JOIN " +
             "    Clinic_Master ON DrugStockTransection.InLoc = Clinic_Master.Clinic_ID LEFT OUTER JOIN " +
             "    Clinic_Master AS Clinic_Master_1 ON DrugStockTransection.OutLoc = Clinic_Master_1.Clinic_ID LEFT OUTER JOIN " +
             "    DrugItems ON DrugStockTransection.ItemID = CAST(DrugItems.DrugID AS nvarchar(50)) LEFT OUTER JOIN " +
            "     EPASPharmacyItems ON DrugStockTransection.ItemID = EPASPharmacyItems.itemno " +
" where DrugStockTransection.OutLoc='" + opdid + "' and DrugStockTransection.TransectionQty!=0 and   CONVERT(DATE,DrugStockTransection.IssuedDate)='" + searchText + "' GROUP BY DrugStockTransection.ItemID " +
" ORDER BY Expr1";
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