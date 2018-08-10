using E2EViewModals.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ERepositories.Interface
{
    public interface IBusinessRepository
    {
        List<BusinessViewModal> GetBusinessList();
        int ManageBusinessActivation(int EmployerID, string IsActive);
    }
}
