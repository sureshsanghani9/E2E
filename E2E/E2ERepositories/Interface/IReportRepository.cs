using System;
using System.Collections.Generic;
using System.Linq;
using E2EViewModals.Report;

namespace E2ERepositories.Interface
{
    public interface IReportRepository
    {
        List<ClientSiteActivityReportViewModal> GetClientSiteActivity(int RoleID, int EmployerID, int AdminUserID, int UserID, DateTime StartDate, DateTime EndDate);
        List<BeneficiaryDetailsReportViewModal> GetBeneficiaryDetails(int RoleID, int EmployerID, int UserID, DateTime StartDate, DateTime EndDate);
        List<BeneficiaryListReportViewModal> GetBeneficiaryList(int RoleID, int EmployerID);
        List<WeekPeriodReportViewModal> GetListWeekPeriod(int EmployerID, int UserID, int RoleID, DateTime StartDate, DateTime EndDate);

    }
}
