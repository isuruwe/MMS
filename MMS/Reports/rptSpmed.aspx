<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptSpmed.aspx.cs" Inherits="MMS.Reports.rptSpmed" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-bottom: 238px;
            margin-right: 196px;
        }
    </style>
    <script>
                            $(document).ready(function () {

                                document.getElementById('datest').valueAsDate = new Date();
                            });

                        </script>
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
                                </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1346px" CssClass="auto-style1" Height="728px">
            <LocalReport ReportPath="Reports\rdlcSplist.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
