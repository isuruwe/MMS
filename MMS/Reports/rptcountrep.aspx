<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptcountrep.aspx.cs" Inherits="MMS.Reports.rptcountrep" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/js/jquery.min.js"></script>
    <script>
                            $(document).ready(function () {

                                document.getElementById('datest').valueAsDate = new Date();
                            });

                        </script>
    <style type="text/css">
        .auto-style1 {
            width: 446px;
        }
        .auto-style2 {
            width: 445px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
     <div class="col-md-6">
                                    Date
                                    <input id="datest" type="date" runat="server" class="form-control" style="color:blue"  />
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem>CBO</asp:ListItem>
                                        <asp:ListItem>KAT</asp:ListItem>
                                        <asp:ListItem>RMA</asp:ListItem>
                                        <asp:ListItem> AHP ></asp:ListItem>     
            <asp:ListItem>AMP</asp:ListItem>
           <asp:ListItem>BCL</asp:ListItem>
          <asp:ListItem>   BIA</asp:ListItem>
           <asp:ListItem>  BTD</asp:ListItem>
       
        <asp:ListItem>  CBY</asp:ListItem>
         <asp:ListItem>  DLA</asp:ListItem>
         <asp:ListItem> EKA</asp:ListItem>
         <asp:ListItem> HIN</asp:ListItem>
          <asp:ListItem> IRM</asp:ListItem>
          
           <asp:ListItem>   KGL</asp:ListItem>
         <asp:ListItem>   KTK</asp:ListItem>
          <asp:ListItem>   MGR</asp:ListItem>
 <asp:ListItem>  MOW</asp:ListItem>
           <asp:ListItem>  MTV</asp:ListItem>
         <asp:ListItem>   PGL</asp:ListItem>
        <asp:ListItem>  PKM</asp:ListItem>
          <asp:ListItem>  PLV</asp:ListItem>
          <asp:ListItem>  PLY</asp:ListItem>
           
          <asp:ListItem> SGR</asp:ListItem>
           <asp:ListItem>   VNA</asp:ListItem>
           <asp:ListItem>    VNI</asp:ListItem>
          <asp:ListItem>   WLA</asp:ListItem>
                                    </asp:DropDownList>
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="auto-style2">&nbsp;</td>
                                            <td>Phamacy Daily Reports</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2">Drug Requests</td>
                                            <td><%:Session["dr"]%></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2">Drug Issues</td>
                                            <td><%:Session["di"]%></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table style="width:100%;">
                                            <tr>
                                                <td class="auto-style1">&nbsp;</td>
                                                <td>Lab Daily Reports</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style1">Lab Requests</td>
                                                <td><%:Session["lr"]%></td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style1">Issued Lab Reports</td>
                                                <td><%:Session["li"]%></td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
    
    </div>
    </form>
</body>
</html>

