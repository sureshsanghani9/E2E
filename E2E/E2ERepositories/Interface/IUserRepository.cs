using E2EViewModals.EndClient;
using E2EViewModals.Invitations;
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

        bool AddUserSendInvite(List<Invite> invites, int? EmployerID, string UserName);

        bool IsUserAddedIntoUserAccount(int UserID, int RoleID);
        int UpsertEmpAdminUser(EmployerAdminViewModal user);

        int UpsertReviewer(ReviewerViewModal user);

        int UpsertEmployee(EmployeeViewModal user);

        List<EmployerAdminViewModal> GetEmployerAdminList(int EmployerID, int AdminUserID = -1);

        List<ReviewerViewModal> GetReviewerList(int EmployerID, int ReviewerID = -1);

        List<EmployeeViewModal> GetEmployeeList(int EmployerID, int EmployeeID = -1);

        int ManageUserActivation(int RoleID, int EmployerID, int UserID, string IsActive);

        int DeleteUser(int RoleID, int EmployerID, int UserID);

        int UpsertEndClient(EndClientViewModal client);

        int DeleteEmployer(int EmployerID);

        int GetSubscriptionIDByEmployerID(int EmployerID);

        int UpdateLoginCount();
    }
}