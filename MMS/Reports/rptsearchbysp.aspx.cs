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
    public partial class rptsearchbysp : System.Web.UI.Page
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
                sqlQuery = "SELECT       Drug_Prescription.issuedQuantity AS qnty,  Drug_Prescription.Date_Time, " +


      "               CONCAT(EPASPharmacyItems.itemdescription, DrugItems.ItemDescription) AS Expr1, Clinic_Master.Clinic_Detail AS Expr11, Drug_Prescription.ItemNo " +
" FROM            Drug_Prescription LEFT OUTER JOIN " +
     "                   Clinic_Master ON Drug_Prescription.IssuedLocID = Clinic_Master.Clinic_ID " +

       "                 LEFT OUTER JOIN " +
             "           Patient_Detail ON Drug_Prescription.PDID = Patient_Detail.PDID " +

            "            LEFT OUTER JOIN " +
           "             Patient ON Patient.PID = Patient_Detail.PID " +

           "              LEFT OUTER JOIN " +

              "          DrugItems ON Drug_Prescription.ItemNo = CAST(DrugItems.DrugID AS nvarchar(50)) LEFT OUTER JOIN " +
           "             EPASPharmacyItems ON Drug_Prescription.ItemNo = EPASPharmacyItems.itemno " +

          "               where Drug_Prescription.IssuedLocID = 'PHY2' and Patient.ServiceNo = 'AW/3266' GROUP BY Drug_Prescription.ItemNo ,Drug_Prescription.issuedQuantity,EPASPharmacyItems.itemdescription,Drug_Prescription.Date_Time " +

        "				 ,DrugItems.ItemDescription,Clinic_Master.Clinic_Detail " +
        "               ORDER BY Expr1";
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
            sqlQuery = "SELECT       Drug_Prescription.issuedQuantity AS qnty,  Drug_Prescription.Date_Time,Patient.ServiceNo, " +


                  "   CONCAT(EPASPharmacyItems.itemdescription, DrugItems.ItemDescription) AS Expr1, Clinic_Master.Clinic_Detail AS Expr11, Drug_Prescription.ItemNo  " +
" FROM            Drug_Prescription LEFT OUTER JOIN  " +
                "        Clinic_Master ON Drug_Prescription.IssuedLocID = Clinic_Master.Clinic_ID  " +
               " LEFT OUTER JOIN " +
                "        Patient_Detail ON Drug_Prescription.PDID = Patient_Detail.PDID " +

                  "      LEFT OUTER JOIN " +
                 "       Patient ON Patient.PID = Patient_Detail.PID " +

                   "      LEFT OUTER JOIN  " +

                   "     DrugItems ON Drug_Prescription.ItemNo = CAST(DrugItems.DrugID AS nvarchar(50)) LEFT OUTER JOIN  " +
                     "   EPASPharmacyItems ON Drug_Prescription.ItemNo = EPASPharmacyItems.itemno  " +

                     "    where Drug_Prescription.IssuedLocID = '"+ opdid + "' and (EPASPharmacyItems.itemdescription like'%"+ drname.Value+ "%' or DrugItems.ItemDescription like'" + drname.Value + "') and CONVERT(DATE,Drug_Prescription.Date_Time) ='"+searchText+"' GROUP BY Drug_Prescription.ItemNo ,Drug_Prescription.issuedQuantity,EPASPharmacyItems.itemdescription,Drug_Prescription.Date_Time  " +


                    "	 ,DrugItems.ItemDescription,Clinic_Master.Clinic_Detail ,Patient.ServiceNo " +
                     "  ORDER BY Expr1";
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