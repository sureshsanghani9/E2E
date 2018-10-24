using System;
using AutoMapper;
using E2ERepositories;
using E2EViewModals.Business;
using E2EViewModals.Subscription;
using E2EViewModals.User;
using E2EViewModals.Task;
using E2EViewModals.EndClient;
using E2EViewModals.Report;

namespace E2E
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            AutoMapperRepositoriesConfiguration repoConfig = new AutoMapperRepositoriesConfiguration();
            CreateMap<sp_Login_Result, UserViewModal>();
            CreateMap<sp_GetSubscriptionInfo_Result, SubscriptionInfoViewModal>();
            CreateMap<sp_GetBusinessList_Result, BusinessViewModal>();
            CreateMap<sp_InsertNewBusiness_Result, AddBusinessViewModal>();
            CreateMap<sp_GetEmployerAdminList_Result, EmployerAdminViewModal>().ForMember(dest => dest.Active, 
                            opts => opts.MapFrom( 
                            src => string.IsNullOrEmpty(src.Active) ? 0 : Convert.ToInt16(src.Active)));
            CreateMap<sp_GetReviewerList_Result, ReviewerViewModal>().ForMember(dest => dest.Active,
                            opts => opts.MapFrom(
                            src => string.IsNullOrEmpty(src.Active) ? 0 : Convert.ToInt16(src.Active))); 
            CreateMap<sp_GetEmployeeList_Result, EmployeeViewModal>().ForMember(dest => dest.Active,
                            opts => opts.MapFrom(
                            src => string.IsNullOrEmpty(src.Active) ? 0 : Convert.ToInt16(src.Active)));
            CreateMap<sp_GetSubscriptionDetails_AdminUser_Result, SubscriptionViewModal>();
            CreateMap<sp_GetListWeekPeriod_Result, WeekPeriodViewModal>();
            CreateMap<sp_GetTaskDetailsByWeekPeriod_Result, TaskDetailsByWeekPeriodViewModal>();
            CreateMap<sp_GetEndClientInfo_Result, EndClientInfoViewModal>();
            CreateMap<sp_GetAllReviewComments_Result, TaskReviewCommentViewModal>();
            CreateMap<rpt_ClientSiteActivity_Result, ClientSiteActivityReportViewModal>();
            CreateMap<rpt_GetBeneficiaryDetails_Result, BeneficiaryDetailsReportViewModal>();
            CreateMap<rpt_GetBeneficiaryList_Result, BeneficiaryListReportViewModal>();
            CreateMap<rpt_GetListWeekPeriod_Result, WeekPeriodReportViewModal>();
            CreateMap<sp_GetTaskSubStatusSummary_Result, TaskSubStatusSummaryViewModal>();
            CreateMap<sp_GetListPendSubmissionEE_Result, PendSubmissionEEViewModal>();
            CreateMap<sp_GetListPendReview_Result, PendReviewViewModal>();
            CreateMap<Employee, EmployeeViewModal>();
        }
    }
}