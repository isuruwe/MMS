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
    public partial class rptdischarge : System.Web.UI.Page
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

                    if (Request.QueryString["pdid"] != null)
                    {
                        searchText = Request.QueryString["pdid"].ToString();
                    }

                    List<DrugStockTransection> customers = null;

                    DataTable dstIntData = new DataTable();
                    DataTable odsvoltxndata = new DataTable();


                    oSqlCommand = new SqlCommand();
                    sqlQuery = "select wd.*,p.ServiceNo  FROM [MMS].[dbo].[ward_discharge] wd INNER JOIN dbo.Patient_Detail pd ON wd.PDID=pd.PDID INNER JOIN dbo.Patient p ON pd.PID = p.PID where wd.pdid='" + searchText + "' ";
                    oSqlCommand.Connection = new SqlConnection(conStr);
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(odsvoltxndata);
                ////////////////////////////////////////////////

                DataTable odsvoltxndata8 = new DataTable();
               

                oSqlCommand = new SqlCommand();
                sqlQuery = "select * from [MMS].[dbo].[Clinic_Master]  where Clinic_ID='" + opdid + "' ";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata8);

                ////////////////////////////////////////////////
                DataTable odsvoltxndata3 = new DataTable();


                    oSqlCommand = new SqlCommand();
                    sqlQuery = "SELECT       (CONCAT(c.itemdescription, b.ItemDescription)) AS Expr1,(d.MethodDetail)as method, " +
                    "    (e.RouteDetail) as route,(a.Dose) as dose,(a.Duration) as duration " +
                     "   FROM[dbo].[Drug_Prescription] as a  LEFT JOIN " +
                     "       DrugItems as b ON a.ItemNo = CAST(b.DrugID AS nvarchar(50)) LEFT JOIN " +
                     "       EPASPharmacyItems as c ON a.ItemNo = c.itemno " +
                     "        left join[dbo].[DrugMethod] as d on a.Method=d.MethodID " +
    " left join[dbo].[DrugRoute] as e on a.Route=e.RouteID where a.pdid='" + searchText + "'  ";
                    oSqlCommand.Connection = new SqlConnection(conStr);
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(odsvoltxndata3);
                   

                    DataTable odsvoltxndata6 = new DataTable();
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "select b.serviceno,b.RelationshipType from [dbo].[Patient_Detail] as a left join [dbo].[Patient] as b on a.pid=b.pid  where a.pdid='" + searchText + "' ";
                    oSqlCommand.Connection = new SqlConnection(conStr);
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(odsvoltxndata6);
                    string svc = odsvoltxndata6.Rows[0]["serviceno"].ToString();
                    string rtyp = odsvoltxndata6.Rows[0]["RelationshipType"].ToString();
                    ////////////////////////////////////////////////

                    DataTable odsvoltxndata5 = new DataTable();


                    oSqlCommand = new SqlCommand();
                    if (rtyp.Equals("1"))
                    {
                        sqlQuery = "IF EXISTS (select * from [dbo].[PersonalDetails] where " +
                          "  ServiceNo='" + svc + "') " +
    " select ServiceNo,COALESCE(Rank, '') +' '+ COALESCE(Initials, '') +' '+ COALESCE(Surname, '') as Surname  , DateOfBirth from[dbo].[PersonalDetails] " +
      "                      where ServiceNo = '" + svc + "' else " +
    " select a.Initials as Initials , COALESCE(a.Initials, '') +' '+ COALESCE(a.Surname, '') as Surname ,a.RANK as Rank ,a.DateOfBirth as " +
     "                       DateOfBirth from[dbo].[Patient] as a left join[dbo].[Patient_Detail] as b on a.pid=b.PID where b.PDID='" + searchText + "' ";
                    }
                    else if (rtyp.Equals("2"))
                    {
                        sqlQuery = "IF EXISTS (SELECT  [SpouseName] as Surname  FROM [MMS].[dbo].[SpouseDetails] as a inner join [dbo].[PersonalDetails] " +
    " as b on a.SNo = b.SNo where b.ServiceNo = '" + svc + "') " +
    " SELECT[SpouseName] as Surname,c.DateOfBirth   FROM[MMS].[dbo].[SpouseDetails] as a inner join[dbo].[PersonalDetails] " +
    " as b on a.SNo=b.SNo left join [dbo].[Patient] as c on c.ServiceNo=b.ServiceNo where b.ServiceNo='" + svc + "' and c.RelationshipType=2 else " +
    " select a.Initials as Initials , a.Surname as Surname , a.RANK as Rank , a.DateOfBirth as DateOfBirth from [dbo].[Patient] as a left join[dbo].[Patient_Detail] as b on a.pid= b.PID  where b.PDID='" + searchText + "' ";
                    }
                    else if (rtyp.Equals("3"))
                    {
                        sqlQuery = "IF EXISTS (SELECT  ParentName as Surname  FROM [dbo].[parents] as a inner join [dbo].[PersonalDetails] " +
    " as b on a.SNo = b.SNo where b.ServiceNo = '" + svc + "' and a.Relationship = 'Father') " +
    " SELECT ParentName as Surname,c.DateOfBirth  FROM[dbo].[parents] as a inner join[dbo].[PersonalDetails] " +
    " as b on a.SNo=b.SNo left join [dbo].[Patient] as c on c.ServiceNo=b.ServiceNo where b.ServiceNo='" + svc + "' and a.Relationship='Father' and c.RelationshipType=3 else " +
    " select a.Initials as Initials , a.Surname as Surname , a.RANK as Rank , a.DateOfBirth as DateOfBirth " +
    " from [dbo].[Patient] as a left join[dbo].[Patient_Detail] as b on a.pid= b.PID  where b.PDID='" + searchText + "' ";
                    }
                    else if (rtyp.Equals("4"))
                    {
                        sqlQuery = "IF EXISTS (SELECT  ParentName as Surname  FROM [dbo].[parents] as a inner join [dbo].[PersonalDetails] " +
    " as b on a.SNo = b.SNo where b.ServiceNo = '" + svc + "' and a.Relationship = 'Mother') " +
    " SELECT ParentName as Surname,c.DateOfBirth  FROM[dbo].[parents] as a inner join[dbo].[PersonalDetails] " +
    " as b on a.SNo=b.SNo left join [dbo].[Patient] as c on c.ServiceNo=b.ServiceNo where b.ServiceNo='" + svc + "' and a.Relationship='Mother' and c.RelationshipType=4 else " +
    " select a.Initials as Initials , a.Surname as Surname , a.RANK as Rank , a.DateOfBirth as DateOfBirth " +
    " from [dbo].[Patient] as a left join[dbo].[Patient_Detail] as b on a.pid= b.PID  where b.PDID='" + searchText + "' ";
                    }
                    else
                    {
                        sqlQuery = "select a.Initials as Initials ,a.Surname as Surname ,a.RANK as Rank ,a.DateOfBirth as DateOfBirth " +
     " from[dbo].[Patient] as a left join[dbo].[Patient_Detail] as b on a.pid=b.PID where b.PDID='" + searchText + "' ";
                    }

                    oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                    oSqlDataAdapter.Fill(odsvoltxndata5);


                    
                    /////////////////////////////////////////////////////////////////

                    //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
                    //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet2", odsvoltxndata);
                    
                    ReportDataSource rdc3 = new ReportDataSource("DataSet3", odsvoltxndata3);
                    
                    ReportDataSource rdc5 = new ReportDataSource("DataSet1", odsvoltxndata5);
                ReportDataSource rdc6 = new ReportDataSource("DataSet4", odsvoltxndata8);
                ReportViewer1.LocalReport.DisplayName = svc;
               ReportViewer1.LocalReport.DataSources.Add(rdc);
                    
                    ReportViewer1.LocalReport.DataSources.Add(rdc3);
            
                    ReportViewer1.LocalReport.DataSources.Add(rdc5);
                ReportViewer1.LocalReport.DataSources.Add(rdc6);
                ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.DataBind();

                }
            }
        
    }
}