using AutoMapper;
using E2ERepositories.Interface;
using E2EViewModals.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ERepositories
{
    public class BusinessRepository : IBusinessRepository
    {
        public List<BusinessViewModal> GetBusinessList()
        {
            using (var db = new E2EWebPortalEntities())
            {
                var businessList = db.sp_GetBusinessList().ToList();
                return Mapper.Map<List<sp_GetBusinessList_Result>, List<BusinessViewModal>>(businessList);
            }
        }

        public int ManageBusinessActivation(int EmployerID, string IsActive)
        {
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_ManageBusinessActivation(IsActive, EmployerID);
            }
        }
    }
}
