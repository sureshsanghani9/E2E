using AutoMapper;
using E2EViewModals.Business;
using E2EViewModals.Subscription;
using E2EViewModals.User;

namespace E2ERepositories
{
    public class AutoMapperRepositoriesConfiguration : Profile
    {
        public AutoMapperRepositoriesConfiguration()
        {
            //CreateMap<sp_Login_Result, UserViewModal>();
            //CreateMap<sp_GetSubscriptionInfo_Result, SubscriptionInfoViewModal>();
            //CreateMap<sp_GetBusinessList_Result, BusinessViewModal>();
        }
    }
}
