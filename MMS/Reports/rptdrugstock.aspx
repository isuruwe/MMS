<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptdrugstock.aspx.cs" Inherits="MMS.Reports.rptdrugstock" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-right: 318px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="auto-style1" Font-Names="Verdana" Font-Size="8pt" Height="707px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1191px">
            <LocalReport ReportPath="Reports\rdlcdrugqnty.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
    </form>
</body>
</html>
