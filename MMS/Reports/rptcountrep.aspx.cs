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
    public partial class rptcountrep : System.Web.UI.Page
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
                DataSet odsvoltxndata3 = new DataSet();


                oSqlCommand = new SqlCommand();
                sqlQuery = "select LocationID from Clinic_Master where Clinic_ID='" + opdid + "'";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata3);
                if (odsvoltxndata3.Tables[0].Rows.Count > 0)
                {
                    locid = odsvoltxndata3.Tables[0].Rows[0][0].ToString();
                }
                string searchText = string.Empty;

                if (Request.QueryString["datest"] != null)
                {
                    searchText = Request.QueryString["datest"].ToString();
                }

                List<DrugStockTransection> customers = null;

                DataTable dstIntData = new DataTable();
                DataSet odsvoltxndata = new DataSet();


                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT count(*) over () as tot FROM[MMS].[dbo].[Lab_Report] WHERE cast(RequestedTime as date) = cast(getdate() as date)  and RequestedLocID ='" + locid + "' group by TestSID";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata);
                if (odsvoltxndata.Tables[0].Rows.Count > 0)
                {
                    string sa = odsvoltxndata.Tables[0].Rows[0][0].ToString();
                    Session["lr"] = sa;
                }
                else
                {
                    Session["lr"] = "0";
                }




                DataTable dstIntData1 = new DataTable();
                DataSet odsvoltxndata1 = new DataSet();


                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT count(*) over () as tot FROM[MMS].[dbo].[Lab_Report] WHERE cast(RequestedTime as date) = cast(getdate() as date) and Issued = '1' and RequestedLocID ='" + locid + "' group by TestSID";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata1);

                if (odsvoltxndata1.Tables[0].Rows.Count > 0)
                {
                    string sa1 = odsvoltxndata1.Tables[0].Rows[0][0].ToString();
                    Session["li"] = sa1;
                }
                else
                {
                    Session["li"] = "0";
                }
                DataSet odsvoltxndata2 = new DataSet();


                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT count(*) over () as tot FROM[MMS].[dbo].[Drug_Prescription] WHERE cast(Date_Time as date) = cast(getdate() as date) and RequestedLocID ='" + locid + "' group by pdid";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata2);

                if (odsvoltxndata2.Tables[0].Rows.Count > 0)
                {
                    string sa1 = odsvoltxndata2.Tables[0].Rows[0][0].ToString();
                    Session["dr"] = sa1;
                }
                else
                {
                    Session["dr"] = "0";
                }
                DataSet odsvoltxndata4 = new DataSet();


                oSqlCommand = new SqlCommand();
                sqlQuery = "SELECT count(*) over () as tot FROM[MMS].[dbo].[Drug_Prescription] WHERE cast(Date_Time as date) = cast(getdate() as date) and Issued = '1' and RequestedLocID ='" + locid + "' group by pdid";
                oSqlCommand.Connection = new SqlConnection(conStr);


                oSqlCommand.CommandText = sqlQuery;
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

                oSqlDataAdapter.Fill(odsvoltxndata4);

                if (odsvoltxndata4.Tables[0].Rows.Count > 0)
                {
                    string sa1 = odsvoltxndata4.Tables[0].Rows[0][0].ToString();
                    Session["di"] = sa1;
                }
                else
                {
                    Session["di"] = "0";
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string opdid = "";
            string locid = "";

            int userid = Convert.ToInt32(Session["UserID"]);


            opdid = (String)Session["userlocid1"];
            string searchText = string.Empty;

            if (Request.Params["datest"] != null)
            {
                searchText = Request.Params["datest"].ToString();
            }
            DataSet odsvoltxndata3 = new DataSet();


            oSqlCommand = new SqlCommand();
            sqlQuery = "select LocationID from Clinic_Master where Clinic_ID='" + opdid + "'";
            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata3);
            if (odsvoltxndata3.Tables[0].Rows.Count > 0)
            {
                locid = odsvoltxndata3.Tables[0].Rows[0][0].ToString();
            }
            List<DrugStockTransection> customers = null;

            DataTable dstIntData = new DataTable();
            DataSet odsvoltxndata = new DataSet();
            

            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT count(*) over () as tot FROM[MMS].[dbo].[Lab_Report] WHERE cast(RequestedTime as date) = cast('" + searchText + "' as date)  and RequestedLocID ='" + DropDownList1.SelectedValue.ToString() + "' group by TestSID";
            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata);
            if (odsvoltxndata.Tables[0].Rows.Count > 0)
            {
                string sa = odsvoltxndata.Tables[0].Rows[0][0].ToString();
                Session["lr"] = sa;
            }
            else
            {
                Session["lr"] = "0";
            }




            DataTable dstIntData1 = new DataTable();
            DataSet odsvoltxndata1 = new DataSet();


            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT count(*) over () as tot FROM[MMS].[dbo].[Lab_Report] WHERE cast(RequestedTime as date) = cast('" + searchText + "' as date) and Issued = '1' and RequestedLocID ='" + DropDownList1.SelectedValue.ToString() + "' group by TestSID";
            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata1);

            if (odsvoltxndata1.Tables[0].Rows.Count > 0)
            {
                string sa1 = odsvoltxndata1.Tables[0].Rows[0][0].ToString();
                Session["li"] = sa1;
            }
            else
            {
                Session["li"] = "0";
            }
            DataSet odsvoltxndata2 = new DataSet();


            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT count(*) over () as tot FROM[MMS].[dbo].[Drug_Prescription] WHERE cast(Date_Time as date) = cast('" + searchText + "' as date) and RequestedLocID ='" + DropDownList1.SelectedValue.ToString() + "' group by pdid";
            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata2);

            if (odsvoltxndata2.Tables[0].Rows.Count > 0)
            {
                string sa1 = odsvoltxndata2.Tables[0].Rows[0][0].ToString();
                Session["dr"] = sa1;
            }
            else
            {
                Session["dr"] = "0";
            }
            DataSet odsvoltxndata4 = new DataSet();


            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT count(*) over () as tot FROM[MMS].[dbo].[Drug_Prescription] WHERE cast(Date_Time as date) = cast('" + searchText + "' as date) and Issued = '1' and RequestedLocID ='" + DropDownList1.SelectedValue.ToString() + "' group by pdid";
            oSqlCommand.Connection = new SqlConnection(conStr);


            oSqlCommand.CommandText = sqlQuery;
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlDataAdapter.Fill(odsvoltxndata4);

            if (odsvoltxndata4.Tables[0].Rows.Count > 0)
            {
                string sa1 = odsvoltxndata4.Tables[0].Rows[0][0].ToString();
                Session["di"] = sa1;
            }
            else
            {
                Session["di"] = "0";
            }
        }
    }
    
}