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
    public partial class rptSurgery : System.Web.UI.Page
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
                sqlQuery = "SELECT TOP 1000 [SGID],[PID],[PDID],[DateofSurgery],[DateodDischarge],b.TDescription,c.nprDescription,d.TADescription "+
     " ,[Indication],[AntibioticP],[Surgeon],[AssistedBy],[Anesthetist],[SurgeryStart],[SurgeryEnd],[Catheter] " +
    "   ,[CentralIVline] ,[Epidural],[Findings],[PrcedureDetail],[DrainsInserted],[Specimens],[SpecimensHistology] " +
    "  ,[SpecimensMicrobiology],[MonitoringInstruct],e.NutDesc,[Nutrition] ,[SpecialIntruct], a.DateofAdmit " +
   "     FROM[MMS].[dbo].[SurgeryMaster] as a left join[dbo].[SurgeryTable]  as b on a.TheaterTable=b.TID " +
" left join[dbo].[SurgeryNProcedure] as c on a.ProcedureName=c.nprid left join[dbo].[SurgeryType] as d " +
" on a.AnesthesiaType=d.TAID left join[dbo].[SurgeryNutrition] as e on a.Nutritionid=e.NID where a.pdid='"+searchText+"' ";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                DataTable odsvoltxndata2 = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = "select a.pcatid,c.pomduration,d.pomfdesc,a.pdid from  [dbo].[SurgeryPomDetail] as a  " +
" left join[dbo].[SurgeryPDuration] as c on a.pduid=c.pomdid left join[dbo].[SurgeryFrequency] as d on a.pfeqid=d.pomfid where a.pdid='" + searchText + "' ";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata2);
                DataTable odsvoltxndata3 = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT       (CONCAT(c.itemdescription, b.ItemDescription)) AS Expr1,(d.MethodDetail)as method, " +
                "    (e.RouteDetail) as route,(a.Dose) as dose,(a.Duration) as duration " +
                 "   FROM[dbo].[SurgeryAP] as a  LEFT JOIN " +
                 "       DrugItems as b ON a.ItemNo = CAST(b.DrugID AS nvarchar(50)) LEFT JOIN " +
                 "       EPASPharmacyItems as c ON a.ItemNo = c.itemno " +
                 "        left join[dbo].[DrugMethod] as d on a.Method=d.MethodID " +
" left join[dbo].[DrugRoute] as e on a.Route=e.RouteID where a.pdid='" + searchText + "' and a.GivenBy='2' ";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata3);
                DataTable odsvoltxndata4 = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT      (CONCAT(c.itemdescription, b.ItemDescription)) AS Expr1,(d.MethodDetail)as method, " +
                "    (e.RouteDetail) as route,(a.Dose) as dose,(a.Duration) as duration " +
                 "   FROM[dbo].[SurgeryAP] as a  LEFT JOIN " +
                 "       DrugItems as b ON a.ItemNo = CAST(b.DrugID AS nvarchar(50)) LEFT JOIN " +
                 "       EPASPharmacyItems as c ON a.ItemNo = c.itemno " +
                 "        left join[dbo].[DrugMethod] as d on a.Method=d.MethodID " +
" left join[dbo].[DrugRoute] as e on a.Route=e.RouteID where a.pdid='" + searchText + "' and a.GivenBy='3' ";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata4);

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
                    sqlQuery = "IF EXISTS (select * from [dbo].[PersonalDetails] where "+
                      "  ServiceNo='"+svc+"') "+
" select ServiceNo,COALESCE(Rank, '') +' '+ COALESCE(Initials, '') +' '+ COALESCE(Surname, '') as Surname  , DateOfBirth from[dbo].[PersonalDetails] " +
  "                      where ServiceNo = '"+svc+"' else "+
" select a.Initials as Initials , COALESCE(a.Initials, '') +' '+ COALESCE(a.Surname, '') as Surname ,a.RANK as Rank ,a.DateOfBirth as " +
 "                       DateOfBirth from[dbo].[Patient] as a left join[dbo].[Patient_Detail] as b on a.pid=b.PID where b.PDID='" + searchText + "' ";
                }
                else if (rtyp.Equals("2"))
                {
                    sqlQuery = "IF EXISTS (SELECT  [SpouseName] as Surname  FROM [MMS].[dbo].[SpouseDetails] as a inner join [dbo].[PersonalDetails] "+
" as b on a.SNo = b.SNo where b.ServiceNo = '"+svc+"') " +
" SELECT[SpouseName] as Surname,c.DateOfBirth   FROM[MMS].[dbo].[SpouseDetails] as a inner join[dbo].[PersonalDetails] " +
" as b on a.SNo=b.SNo left join [dbo].[Patient] as c on c.ServiceNo=b.ServiceNo where b.ServiceNo='" + svc + "' and c.RelationshipType=2 else " +
" select a.Initials as Initials , a.Surname as Surname , a.RANK as Rank , a.DateOfBirth as DateOfBirth from [dbo].[Patient] as a left join[dbo].[Patient_Detail] as b on a.pid= b.PID  where b.PDID='" + searchText + "' ";
                }
                else if (rtyp.Equals("3"))
                {
                    sqlQuery = "IF EXISTS (SELECT  ParentName as Surname  FROM [dbo].[parents] as a inner join [dbo].[PersonalDetails] "+
" as b on a.SNo = b.SNo where b.ServiceNo = '" + svc + "' and a.Relationship = 'Father') " +
" SELECT ParentName as Surname,c.DateOfBirth  FROM[dbo].[parents] as a inner join[dbo].[PersonalDetails] " +
" as b on a.SNo=b.SNo left join [dbo].[Patient] as c on c.ServiceNo=b.ServiceNo where b.ServiceNo='" + svc + "' and a.Relationship='Father' and c.RelationshipType=3 else " +
" select a.Initials as Initials , a.Surname as Surname , a.RANK as Rank , a.DateOfBirth as DateOfBirth "+
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
 " from[dbo].[Patient] as a left join[dbo].[Patient_Detail] as b on a.pid=b.PID where b.PDID='"+searchText+"' ";
                }

                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata5);


                DataTable odsvoltxndata7 = new DataTable();


                oSqlCommand = new SqlCommand();
                sqlQuery = "select a.Noofpkts,b.Suturematerials,c.tdesc from [dbo].[SurgeryClosure] as a left join [dbo].[SurgerySutureM] as b "+
" on a.SuturematerialsID = b.SMID left join[dbo].[SurgeryTechniq] as c on a.Technique = c.tid where a.pdid='" + searchText + "' ";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata7);
                /////////////////////////////////////////////////////////////////

                //Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", odsvoltxndata);
                ReportDataSource rdc2 = new ReportDataSource("DataSet2", odsvoltxndata2);
                ReportDataSource rdc3 = new ReportDataSource("DataSet3", odsvoltxndata3);
                ReportDataSource rdc4 = new ReportDataSource("DataSet4", odsvoltxndata4);
                ReportDataSource rdc5 = new ReportDataSource("DataSet5", odsvoltxndata5);
                ReportDataSource rdc6 = new ReportDataSource("DataSet6", odsvoltxndata7);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.DataSources.Add(rdc2);
                ReportViewer1.LocalReport.DataSources.Add(rdc3);
                ReportViewer1.LocalReport.DataSources.Add(rdc4);
                ReportViewer1.LocalReport.DataSources.Add(rdc5);
                ReportViewer1.LocalReport.DataSources.Add(rdc6);
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.DataBind();

            }
        }
    }
}