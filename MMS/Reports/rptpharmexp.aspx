<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptpharmexp.aspx.cs" Inherits="MMS.Reports.rptpharmexp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-right: 78px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" CssClass="form-control"  Text="View This Month Expire" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" CssClass="form-control" Text="View Last Month Expired" OnClick="Button2_Click" />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="auto-style1" Font-Names="Verdana" Font-Size="8pt" Height="547px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1233px">
            <LocalReport ReportPath="Reports\rdlcpharmexp.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
    </form>
</body>
</html>
