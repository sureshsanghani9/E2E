using E2ERepositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using E2EViewModals.Report;
using AutoMapper;

namespace E2ERepositories
{
    public class ReportRepository : IReportRepository
    {
        public List<ClientSiteActivityReportViewModal> GetClientSiteActivity(int RoleID, int EmployerID, int AdminUserID, int UserID, DateTime StartDate, DateTime EndDate)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var clientSiteActivities = db.rpt_ClientSiteActivity(RoleID, EmployerID, AdminUserID, UserID, StartDate, EndDate).ToList();
                return Mapper.Map<List<rpt_ClientSiteActivity_Result>, List<ClientSiteActivityReportViewModal>>(clientSiteActivities);
            }
        }

        public List<BeneficiaryDetailsReportViewModal> GetBeneficiaryDetails(int RoleID, int EmployerID, int UserID, DateTime StartDate, DateTime EndDate)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var beneficiaryDetails = db.rpt_GetBeneficiaryDetails(RoleID, EmployerID, UserID, StartDate, EndDate).ToList();
                return Mapper.Map<List<rpt_GetBeneficiaryDetails_Result>, List<BeneficiaryDetailsReportViewModal>>(beneficiaryDetails);
            }
        }

        public List<BeneficiaryListReportViewModal> GetBeneficiaryList(int RoleID, int EmployerID)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var beneficiaryList = db.rpt_GetBeneficiaryList(RoleID, EmployerID).ToList();
                return Mapper.Map<List<rpt_GetBeneficiaryList_Result>, List<BeneficiaryListReportViewModal>>(beneficiaryList);
            }
        }

        public List<WeekPeriodReportViewModal> GetListWeekPeriod(int EmployerID, int UserID, int RoleID, DateTime StartDate, DateTime EndDate)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var clientSiteActivities = db.rpt_GetListWeekPeriod(EmployerID, UserID, RoleID, StartDate, EndDate).ToList();
                return Mapper.Map<List<rpt_GetListWeekPeriod_Result>, List<WeekPeriodReportViewModal>>(clientSiteActivities);
            }
        }
    }
}
