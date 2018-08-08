using E2EViewModals.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ERepositories.Interface
{
    public interface IUserRepository
    {
        UserViewModal GetUser(string userName, string password);

        UserAccount GetUser(int UserId);

        UserAccount GetUser(string userName);

        string SetResetCode(string userName);

        string ValidateResetCode(string resetCode);

        bool ValidatePassword(string userName, string password);

        void resetPassword(string userName, string password, string resetCode);

    }
}
