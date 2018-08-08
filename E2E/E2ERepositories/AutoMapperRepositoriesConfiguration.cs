using AutoMapper;
using E2EViewModals.User;

namespace E2ERepositories
{
    public class AutoMapperRepositoriesConfiguration : Profile
    {
        public AutoMapperRepositoriesConfiguration()
        {
            CreateMap<sp_Login_Result, UserViewModal>();
        }
    }
}
