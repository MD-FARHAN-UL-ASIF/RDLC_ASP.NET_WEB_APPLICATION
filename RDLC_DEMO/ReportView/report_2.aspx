<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report_2.aspx.cs" Inherits="RDLC_DEMO.ReportView.report_2" %>

<!DOCTYPE html>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:Button ID="btnLoadReport" Text="Load Report" runat="server" OnClick="btnLoadReport_Click" />
        </div>
        <div style="height: 100vh; width: 100%; display: flex; justify-content: center; align-items: center;">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%"></rsweb:ReportViewer>
</div>

    </form>
</body>
</html>

