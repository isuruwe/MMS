﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptdhssickreport.aspx.cs" Inherits="MMS.Reports.rptdhssickreport" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
                                    &nbsp;<input id="datest" type="date" runat="server" class="form-control" style="color:blue"  /><asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem>CBO</asp:ListItem>
                                        <asp:ListItem>KAT</asp:ListItem>
                                        <asp:ListItem>RMA</asp:ListItem>
                                        <asp:ListItem>AHP</asp:ListItem>     
            <asp:ListItem>AMP</asp:ListItem>
           <asp:ListItem>BCL</asp:ListItem>
          <asp:ListItem>BIA</asp:ListItem>
           <asp:ListItem>BTD</asp:ListItem>
       
        <asp:ListItem>CBY</asp:ListItem>
         <asp:ListItem>DLA</asp:ListItem>
         <asp:ListItem>EKA</asp:ListItem>
         <asp:ListItem>HIN</asp:ListItem>
          <asp:ListItem>IRM</asp:ListItem>
          
           <asp:ListItem>KGL</asp:ListItem>
         <asp:ListItem>KTK</asp:ListItem>
          <asp:ListItem>MGR</asp:ListItem>
 <asp:ListItem>MOW</asp:ListItem>
           <asp:ListItem>MTV</asp:ListItem>
         <asp:ListItem>PGL</asp:ListItem>
        <asp:ListItem>PKM</asp:ListItem>
          <asp:ListItem>PLV</asp:ListItem>
          <asp:ListItem>KKS</asp:ListItem>
           
          <asp:ListItem>SGR</asp:ListItem>
           <asp:ListItem>VNA</asp:ListItem>
           <asp:ListItem>WAN</asp:ListItem>
          <asp:ListItem>WLA</asp:ListItem>
                                    </asp:DropDownList>
    
                                   
    
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
    
                                   
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="868px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1480px">
            <LocalReport ReportPath="Reports\rdlcsickreport.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
    </form>
</body>
</html>

