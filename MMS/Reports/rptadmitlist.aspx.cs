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
    public partial class rptadmitlist : System.Web.UI.Page
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

                int userid = Convert.ToInt32(Session["UserID"]);

                string searchText = (String)Session["userlocid1"];

                List<DrugStockTransection> customers = null;

                DataTable dstIntData = new DataTable();
                DataTable odsvoltxndata = new DataTable();
           string     opdid = (String)Session["userlocid1"];

                oSqlCommand = new SqlCommand();
                sqlQuery = "   SELECT max(a.Present_Complain)pcomoplian, COALESCE(NULLIF(concat(max(case when c.RelationshipType = 1  " +
"  and b.Surname != '0' " +
 " then b.Surname end), max(case when c.RelationshipType = 2 then e.SpouseName  end),    " +
"  max(case when c.RelationshipType = 5 and c.DateOfBirth = f.DOB  then f.ChildName  end), " +
"   max(case when c.RelationshipType = 3 and g.Relationship = 'Father'   then g.ParentName  end), " +
 "   max(case when c.RelationshipType = 4 and g.Relationship = 'Mother' then g.ParentName  end)), ''), max(c.surname))  " +
"	sname  ,max(case when c.RelationshipType = 1 then b.Rank  end) rnkname, max(c.ServiceNo)  sno " +
"	 ,max(case when c.RelationshipType = 1 then b.Initials  end)  inililes, max(c.RelationshipType) relasiont " +
"	  , max(c.pid)  pidp, max(a.pdid)  pdids,max(sc.CatId)  pstatus,max(a.CreatedDate) crdate, max(h.Relationship) " +

 "     relasiondet,max(d.LocationID) loc FROM[MMS].[dbo].[Patient_Detail] as a with(nolock) inner join [dbo].Ward_Details wd ON wd.PDID = a.PDID   left join[MMS].[dbo].[Patient] as c on a.pid=c.pid " +
"  left join[MMS].[dbo].[PersonalDetails] as b on c.ServiceNo=b.ServiceNo " +
"  left join[MMS].[dbo].[Clinic_Master] as d on a.OPDID=d.Clinic_ID left join[MMS].[dbo].[SpouseDetails] as e on b.SNo=e.SNo " +
"   left join[MMS].[dbo].[Children] as f on b.SNo=f.SNo left join[MMS].[dbo].[parents] as g on b.SNo=g.SNo " +
 "   left join[MMS].[dbo].[RelationshipType] as h on h.RTypeID=c.RelationshipType " +
  " LEFT JOIN Sick_Category sc ON a.pdid = sc.PDID  " +
  "left join CatDiagList cdl ON a.PDID = cdl.PDID " +
 "left join CatDaignosis cds ON cdl.dgid = cds.dgid " +
 "left join ward_details WDD ON a.PDID = WDD.PDID  " +
 "left join Ward_Types WT ON  WD.Ward_No=WT.Id " +
" where   wd.status= 15 and a.PatientCatID!=2" +
" and wd.opdid='" + opdid + "' group by a.PDID, a.CreatedDate order by crdate desc ";
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
}