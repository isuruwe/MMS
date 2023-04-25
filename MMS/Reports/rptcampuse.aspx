<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptcampuse.aspx.cs" Inherits="MMS.Reports.rptcampuse" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="col-md-6">
                                    Date
                                    <input id="datest" type="month" runat="server" class="form-control" style="color:blue"  />
                                  
         
         
          <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
                                </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="629px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1242px">
            <LocalReport ReportPath="Reports\rdlccamprep.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>

