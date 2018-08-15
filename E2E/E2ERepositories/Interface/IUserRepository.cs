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

        int AddEmpAdminUser(string userName, string password, int employerID, int roleID, int active, string adminUserFirstName, string adminuserMiddleName,
            string adminUserLastName, string adminUserNickName, string adminTitle, string address1, string address2, string city, string state, string zip,
            string workPhoneNumber, string extn, string cellPhoneNumber, string primaryEmail, string secondaryEmail, bool isPrimary);

    }
}