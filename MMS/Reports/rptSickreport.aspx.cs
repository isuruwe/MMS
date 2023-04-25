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
    public partial class rptSickreport : System.Web.UI.Page
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

                if (Request.QueryString["opdid"] != null)
                {
                    searchText = Request.QueryString["opdid"].ToString();
                }

                List<DrugStockTransection> customers = null;

                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();


                oSqlCommand = new SqlCommand();
                if (searchText=="CBO"|| searchText == "AHQ")
                {
                    sqlQuery = "SELECT        SickReport.LocationID, SickReport.regdate, SickReport.islow, SickReport.isduty, SickReport.service, SickReport.age, SickReport.isliveout, SickReport.OPDID, Patient_Detail.Present_Complain, Vw_Formation.DivisionName, " +
                     "   Sick_Category.CatPeriod, PersonalDetails.Rank, PersonalDetails.Initials, PersonalDetails.Surname, CatDaignosis.dgdetail, Sick_CategoryType.Category_Type, PersonalDetails.ServiceNo " +
                      "   FROM            SickReport LEFT JOIN " +
                     "   Patient_Detail ON SickReport.PDID = Patient_Detail.PDID LEFT JOIN " +
                     "   Sick_Category ON Patient_Detail.PDID = Sick_Category.PDID LEFT JOIN " +
                    "    Sick_CategoryType ON Sick_Category.CatID = Sick_CategoryType.CatID LEFT JOIN " +
                    "    CatDiagList ON SickReport.PDID = CatDiagList.PDID LEFT JOIN " +
                    "    CatDaignosis ON CatDiagList.dgid = CatDaignosis.dgid LEFT JOIN " +
                    "    Patient ON Patient_Detail.PID = Patient.PID LEFT  JOIN " +
                     "   PersonalDetails ON Patient.ServiceNo = PersonalDetails.ServiceNo LEFT  JOIN " +
                     "   Vw_Formation ON SickReport.OPDID = Vw_Formation.DivisionID and   SickReport.LocationID = Vw_Formation.LocationID where CONVERT(DATE,SickReport.regdate)='" + DateTime.Now.Date.ToShortDateString() + "' and (SickReport.LocationID='" + searchText + "'or SickReport.LocationID='AHQ')  and PersonalDetails.Surname!='0' order by Vw_Formation.DivisionName ";
                }
                else
                {
                    sqlQuery = "SELECT        SickReport.LocationID, SickReport.regdate, SickReport.islow, SickReport.isduty, SickReport.service, SickReport.age, SickReport.isliveout, SickReport.OPDID, Patient_Detail.Present_Complain, Vw_Formation.DivisionName, " +
                     "   Sick_Category.CatPeriod, PersonalDetails.Rank, PersonalDetails.Initials, PersonalDetails.Surname, CatDaignosis.dgdetail, Sick_CategoryType.Category_Type, PersonalDetails.ServiceNo " +
                      "   FROM            SickReport LEFT JOIN " +
                     "   Patient_Detail ON SickReport.PDID = Patient_Detail.PDID LEFT JOIN " +
                     "   Sick_Category ON Patient_Detail.PDID = Sick_Category.PDID LEFT JOIN " +
                    "    Sick_CategoryType ON Sick_Category.CatID = Sick_CategoryType.CatID LEFT JOIN " +
                    "    CatDiagList ON SickReport.PDID = CatDiagList.PDID LEFT JOIN " +
                    "    CatDaignosis ON CatDiagList.dgid = CatDaignosis.dgid LEFT JOIN " +
                    "    Patient ON Patient_Detail.PID = Patient.PID LEFT  JOIN " +
                     "   PersonalDetails ON Patient.ServiceNo = PersonalDetails.ServiceNo LEFT  JOIN " +
                     "   Vw_Formation ON SickReport.OPDID = Vw_Formation.DivisionID and   SickReport.LocationID = Vw_Formation.LocationID where CONVERT(DATE,SickReport.regdate)='" + DateTime.Now.Date.ToShortDateString() + "' and SickReport.LocationID='" + searchText + "' and PersonalDetails.Surname!='0' order by Vw_Formation.DivisionName  ";
                }
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
            string opdid = "";
            string locid = "";

            int userid = Convert.ToInt32(Session["UserID"]);
            string searchText2 = string.Empty;
            string searchText1 = string.Empty;
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
            if (Request.Params["datest"] != null)
            {
                searchText2 = Request.Params["datest"].ToString();
            }
            //if (Request.Params["datest1"] != null)
            //{
            //    searchText1 = Request.Params["datest1"].ToString();
            //}
            opdid = (String)Session["userlocid1"];
            string searchText = string.Empty;

            if (Request.QueryString["opdid"] != null)
            {
                
            }
            searchText = (String)Session["userloc"];
            List<DrugStockTransection> customers = null;

            DataTable dstIntData = new DataTable();
            DataTable odsvoltxndata = new DataTable();


            oSqlCommand = new SqlCommand();
            if (searchText == "CBO" || searchText == "AHQ")
            {
                sqlQuery = "SELECT        SickReport.LocationID, SickReport.regdate, SickReport.islow, SickReport.isduty, SickReport.service, SickReport.age, SickReport.isliveout, SickReport.OPDID, Patient_Detail.Present_Complain, Vw_Formation.DivisionName, " +
                 "   Sick_Category.CatPeriod, PersonalDetails.Rank, PersonalDetails.Initials, PersonalDetails.Surname, CatDaignosis.dgdetail, Sick_CategoryType.Category_Type, PersonalDetails.ServiceNo " +
                  "   FROM            [MMS].[dbo].SickReport LEFT JOIN " +
                 "   [MMS].[dbo].Patient_Detail ON SickReport.PDID = Patient_Detail.PDID LEFT JOIN " +
                 "   [MMS].[dbo].Sick_Category ON Patient_Detail.PDID = Sick_Category.PDID LEFT JOIN " +
                "    [MMS].[dbo].Sick_CategoryType ON Sick_Category.CatID = Sick_CategoryType.CatID LEFT JOIN " +
                "    [MMS].[dbo].CatDiagList ON SickReport.PDID = CatDiagList.PDID LEFT JOIN " +
                "    [MMS].[dbo].CatDaignosis ON CatDiagList.dgid = CatDaignosis.dgid LEFT JOIN " +
                "    [MMS].[dbo].Patient ON Patient_Detail.PID = Patient.PID LEFT  JOIN " +
                 "   [MMS].[dbo].PersonalDetails ON Patient.ServiceNo = PersonalDetails.ServiceNo LEFT  JOIN " +
                 "   [MMS].[dbo].Vw_Formation ON SickReport.OPDID = Vw_Formation.DivisionID and   SickReport.LocationID = Vw_Formation.LocationID where CONVERT(DATE,SickReport.regdate)='" + searchText2 + "'  and (SickReport.LocationID='" + searchText + "'or SickReport.LocationID='AHQ')  and PersonalDetails.Surname!='0' order by Vw_Formation.DivisionName ";
            }
            else
            {
                sqlQuery = "SELECT        SickReport.LocationID, SickReport.regdate, SickReport.islow, SickReport.isduty, SickReport.service, SickReport.age, SickReport.isliveout, SickReport.OPDID, Patient_Detail.Present_Complain, Vw_Formation.DivisionName, " +
                 "   Sick_Category.CatPeriod, PersonalDetails.Rank, PersonalDetails.Initials, PersonalDetails.Surname, CatDaignosis.dgdetail, Sick_CategoryType.Category_Type, PersonalDetails.ServiceNo " +
                  "   FROM            [MMS].[dbo].SickReport LEFT JOIN " +
                 "   [MMS].[dbo].Patient_Detail ON SickReport.PDID = Patient_Detail.PDID LEFT JOIN " +
                 "   [MMS].[dbo].Sick_Category ON Patient_Detail.PDID = Sick_Category.PDID LEFT JOIN " +
                "    [MMS].[dbo].Sick_CategoryType ON Sick_Category.CatID = Sick_CategoryType.CatID LEFT JOIN " +
                "    [MMS].[dbo].CatDiagList ON SickReport.PDID = CatDiagList.PDID LEFT JOIN " +
                "    [MMS].[dbo].CatDaignosis ON CatDiagList.dgid = CatDaignosis.dgid LEFT JOIN " +
                "    [MMS].[dbo].Patient ON Patient_Detail.PID = Patient.PID LEFT  JOIN " +
                 "   [MMS].[dbo].PersonalDetails ON Patient.ServiceNo = PersonalDetails.ServiceNo LEFT  JOIN " +
                 "   [MMS].[dbo].Vw_Formation ON SickReport.OPDID = Vw_Formation.DivisionID and   SickReport.LocationID = Vw_Formation.LocationID where CONVERT(DATE,SickReport.regdate)='" + searchText2 + "'  and SickReport.LocationID='" + searchText + "' and PersonalDetails.Surname!='0' order by Vw_Formation.DivisionName  ";
            }
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
}