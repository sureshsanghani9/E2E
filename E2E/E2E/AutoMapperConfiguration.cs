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
        }
    }
}