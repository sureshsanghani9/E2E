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
            //reportViewer.Height = Unit.Pixel(1000);

            DateTime StartDate = TempData["StartDate"] != null ? Convert.ToDateTime(TempData["StartDate"].ToString()) : DateTime.Now.Date.AddMonths(-1).Date;
            DateTime EndDate = TempData["EndDate"] != null ? Convert.ToDateTime(TempData["EndDate"].ToString()) : DateTime.Now;

            var activity = _reportRepo.GetClientSiteActivity(loggedInuser.RoleID, loggedInuser.EmployerID, 0, loggedInuser.Id, StartDate, EndDate);
            var beneficiaryDetails = _reportRepo.GetBeneficiaryDetails(loggedInuser.RoleID, loggedInuser.EmployerID, loggedInuser.Id, loggedInuser.Id, StartDate, EndDate);
            var beneficiaryList = _reportRepo.GetBeneficiaryList(loggedInuser.RoleID, loggedInuser.EmployerID);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"assets\reports\EndClientSiteActivityReport_2012_EE.rdl";

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ClientSiteActivity", activity));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetBeneficiaryDetails", beneficiaryDetails));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetBeneficiaryList", beneficiaryList));

            ReportParameter p1 = new ReportParameter("RoleID", loggedInuser.RoleID.ToString());
            ReportParameter p2 = new ReportParameter("EmployerID", loggedInuser.EmployerID.ToString());
            ReportParameter p3 = new ReportParameter("AdminUserID", loggedInuser.Id.ToString());
            ReportParameter p4 = new ReportParameter("UserID", loggedInuser.Id.ToString());
            ReportParameter p5 = new ReportParameter("StartDate", StartDate.ToShortDateString());
            ReportParameter p6 = new ReportParameter("EndDate", EndDate.ToShortDateString());
            ReportParameter p7 = new ReportParameter("ReportTitle", "Activity Report");

            reportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7 });

            ViewBag.StartDate = StartDate.ToShortDateString();
            ViewBag.EndDate = EndDate.ToShortDateString();
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        public ActionResult ReviewerActivityReport()
        {
            var loggedInuser = (UserViewModal)Session["User"];

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            DateTime StartDate = TempData["StartDate"] != null ? (DateTime)TempData["StartDate"] : DateTime.Now.Date.AddMonths(-1).Date;
            DateTime EndDate = TempData["EndDate"] != null ? (DateTime)TempData["EndDate"] : DateTime.Now;

            var activity = _reportRepo.GetClientSiteActivity(loggedInuser.RoleID, loggedInuser.EmployerID, loggedInuser.Id, loggedInuser.Id, StartDate, EndDate);
            var beneficiaryDetails = _reportRepo.GetBeneficiaryDetails(loggedInuser.RoleID, loggedInuser.EmployerID, loggedInuser.Id, loggedInuser.Id, StartDate, EndDate);
            var beneficiaryList = _reportRepo.GetBeneficiaryList(loggedInuser.RoleID, loggedInuser.EmployerID);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"assets\reports\EndClientSiteActivityReport_2012_Main.rdl";

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ClientSiteActivity", activity));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetBeneficiaryDetails", beneficiaryDetails));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetBeneficiaryList", beneficiaryList));

            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        public ActionResult EmployerActivityReport()
        {
            var loggedInuser = (UserViewModal)Session["User"];

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            DateTime StartDate = TempData["StartDate"] != null ? (DateTime)TempData["StartDate"] : DateTime.Now.Date.AddMonths(-1).Date;
            DateTime EndDate = TempData["EndDate"] != null ? (DateTime)TempData["EndDate"] : DateTime.Now;

            var activity = _reportRepo.GetClientSiteActivity(loggedInuser.RoleID, loggedInuser.EmployerID, loggedInuser.Id, loggedInuser.Id, StartDate, EndDate);
            var beneficiaryDetails = _reportRepo.GetBeneficiaryDetails(loggedInuser.RoleID, loggedInuser.EmployerID, loggedInuser.Id, loggedInuser.Id, StartDate, EndDate);
            var beneficiaryList = _reportRepo.GetBeneficiaryList(loggedInuser.RoleID, loggedInuser.EmployerID);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"assets\reports\EndClientSiteActivityReport_2012_Main.rdl";

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ClientSiteActivity", activity));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetBeneficiaryDetails", beneficiaryDetails));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GetBeneficiaryList", beneficiaryList));

            ViewBag.ReportViewer = reportViewer;
            return View();
        }


        [HttpPost]
        public JsonResult SetReportDate(FormCollection form)
        {
            string StartDate = Convert.ToString(form["StartDate"].ToString());
            string EndDate = Convert.ToString(form["EndDate"].ToString());

            TempData["StartDate"] = StartDate;
            TempData["EndDate"] = EndDate;

            return Json(new { Code = 1, Message = "Dates are saved successfully." });

        }

    }
}