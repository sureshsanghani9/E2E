using System;
using AutoMapper;
using E2ERepositories;
using E2EViewModals.Business;
using E2EViewModals.Subscription;
using E2EViewModals.User;

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
        }
    }
}