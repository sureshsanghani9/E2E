<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReportViewerWebForm.aspx.cs" Inherits="ReportViewerForMvc.ReportViewerWebForm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script runat="server">
//void Page_Load(object sender, EventArgs e)
//{
//    E2ERepositories.Interface.IReportRepository _reportRepo = new E2ERepositories.ReportRepository();
//    if (!IsPostBack)
//    {
//        ReportViewer reportViewer = new ReportViewer();
//        reportViewer.ProcessingMode = ProcessingMode.Local;

//        reportViewer.SizeToReportContent = true;
//        reportViewer.Width = Unit.Percentage(100);

//        DateTime StartDate = Convert.ToDateTime("09/01/2018");
//        DateTime EndDate = Convert.ToDateTime("09/30/2018");

//        var activity = _reportRepo.GetClientSiteActivity(4, 41, 0, 19, StartDate, EndDate);
//        var beneficiaryDetails = _reportRepo.GetBeneficiaryDetails(4, 41, 19, 19, StartDate, EndDate);
//        var beneficiaryList = _reportRepo.GetBeneficiaryList(4, 41);

//        //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"assets\reports\EndClientSiteActivityReport_2012_EE_3.rdl";
//        reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"assets\reports\EndClientSiteActivityReport_EE_2012.rdl";

//        reportViewer.LocalReport.DataSources.Clear();

//        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ClientSiteActivity", activity));
//        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetBeneficiaryDetails", beneficiaryDetails));
//        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetBeneficiaryList", beneficiaryList));

//        ReportParameter p1 = new ReportParameter("RoleID", "4");
//        ReportParameter p2 = new ReportParameter("EmployerID", "2");
//        ReportParameter p3 = new ReportParameter("AdminUserID", "19");
//        ReportParameter p4 = new ReportParameter("UserID", "19");
//        ReportParameter p5 = new ReportParameter("StartDate", StartDate.ToShortDateString());
//        ReportParameter p6 = new ReportParameter("EndDate", EndDate.ToShortDateString());
//        ReportParameter p7 = new ReportParameter("ReportTitle", "RTITL");
//        ReportParameter p8 = new ReportParameter("Petitioner", "Petitioner");
//        ReportParameter p9 = new ReportParameter("Beneficiary", "Beneficiary");
//        ReportParameter p10 = new ReportParameter("EndClientName", "EndClientName");
//        ReportParameter p11 = new ReportParameter("Address1", "Address1");
//        ReportParameter p12 = new ReportParameter("Address2", "Address2");
//        ReportParameter p13 = new ReportParameter("City", "City");
//        ReportParameter p14 = new ReportParameter("State", "State");
//        ReportParameter p15 = new ReportParameter("Zip", "Zip");
//        ReportParameter p16  = new ReportParameter("ReportCreatedBy", "ReportCreatedBy");

//        reportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7 });
//        //reportViewer.LocalReport.Refresh();
//    }
//}
    </script>

</head>
<body style="margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <Scripts>
                    <asp:ScriptReference Assembly="ReportViewerForMvc" Name="ReportViewerForMvc.Scripts.PostMessage.js" />
                </Scripts>
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="True" AsyncRendering="true" ></rsweb:ReportViewer>
        </div>
    </form>


    
</body>
</html>
