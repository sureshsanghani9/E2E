using System;
using AutoMapper;
using E2ERepositories;
using E2EViewModals.User;

namespace E2E
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            AutoMapperRepositoriesConfiguration repoConfig = new AutoMapperRepositoriesConfiguration();
            CreateMap<sp_Login_Result, UserViewModal>();
        }
    }
}