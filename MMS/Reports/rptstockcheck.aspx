﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptstockcheck.aspx.cs" Inherits="MMS.Reports.rptstockcheck" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="723px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1016px">
            <LocalReport ReportPath="Reports\rdlcstockcheck.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    <div>
    
    </div>
    </form>
</body>
</html>
