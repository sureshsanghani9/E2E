using E2ERepositories.Interface;
using E2EViewModals.User;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace E2E.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepo;
        public ReportController(IReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }

        // GET: Report
        public ActionResult EmployeeActivityReport()
        {
            var loggedInuser = (UserViewModal)Session["User"];

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            
            DateTime StartDate = TempData["StartDate"] != null ? (DateTime)TempData["StartDate"] : DateTime.Now.Date.AddMonths(-1).Date;
            DateTime EndDate = TempData["EndDate"] != null ? (DateTime)TempData["EndDate"] : DateTime.Now;

            var activity = _reportRepo.GetClientSiteActivity(loggedInuser.RoleID, loggedInuser.EmployerID, 0, loggedInuser.Id, StartDate, EndDate);
            var beneficiaryDetails = _reportRepo.GetBeneficiaryDetails(loggedInuser.RoleID, loggedInuser.EmployerID, loggedInuser.Id, StartDate, EndDate);
            var beneficiaryList = _reportRepo.GetBeneficiaryList(loggedInuser.RoleID, loggedInuser.EmployerID);

            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"assets\reports\EndClientSiteActivityReport_EE.rdl";
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"assets\reports\EndClientSiteActivityReport_EE_2012.rdl";

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ClientSiteActivity", activity));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetBeneficiaryDetails", beneficiaryDetails));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetBeneficiaryList", beneficiaryList));
            
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
    }
}