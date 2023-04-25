<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptsickreportwo.aspx.cs" Inherits="MMS.Reports.rptsickreportwo" %>



<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
                                    <input id="datest" type="date" runat="server" class="form-control" style="color:blue"  /><asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
    
                                   
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="868px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1480px">
            <LocalReport ReportPath="Reports\rdlcsickreport2.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
    </form>
</body>
</html>


