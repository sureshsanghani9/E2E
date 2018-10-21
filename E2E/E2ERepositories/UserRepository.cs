using E2ERepositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using E2EViewModals.User;
using AutoMapper;
using E2EInfrastructure.Helpers;
using E2EViewModals.Invitations;
using System.Data;
using System.Data.SqlClient;
using static E2EViewModals.CommanEnums;
using E2EViewModals.EndClient;

namespace E2ERepositories
{
    public class UserRepository : IUserRepository
    {
        public UserViewModal GetUser(string userName, string password)
        {
            password = EncryptionHelper.Encrypt(password);
            using (var db = new E2EWebPortalEntities())
            {
                var user = db.sp_Login(userName, password).ToList().FirstOrDefault();
                return Mapper.Map<sp_Login_Result, UserViewModal>(user);
            }
        }

        public UserAccount GetUser(int UserId)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var user = db.UserAccounts.Where(u => u.UserAccountID == UserId).FirstOrDefault();
                return user;
            }
        }

        public UserAccount GetUser(string userName)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var user = db.UserAccounts.Where(u => u.UserName == userName).FirstOrDefault();
                return user;
            }
        }

        public string SetResetCode(string userName)
        {
            string resetCode = string.Empty;
            using (var db = new E2EWebPortalEntities())
            {
                var user = db.UserAccounts.Where(u => u.UserName == userName).FirstOrDefault();
                if (user != null && user.UserAccountID != 0)
                {
                    resetCode = Guid.NewGuid().ToString();
                    var userMIS = db.UserMIS.Where(u => u.UserId == user.UserAccountID).FirstOrDefault();
                    if (userMIS != null && userMIS.Id != 0)
                    {
                        userMIS.ResetCode = resetCode;
                    }
                    else
                    {
                        UserMI userMis = new UserMI { UserId = user.UserAccountID, FailAttempts = 0, ResetCode = resetCode };
                    }
                    db.SaveChanges();

                    return resetCode;
                }
                else
                {
                    return resetCode;
                }

            }
        }

        public string ValidateResetCode(string resetCode)
        {
            string userName = string.Empty;
            using (var db = new E2EWebPortalEntities())
            {
                var user = db.UserMIS.Where(u => u.ResetCode == resetCode).FirstOrDefault();
                if (user != null && user.UserId != 0)
                {
                    userName = db.UserAccounts.Where(u => u.UserAccountID == user.UserId).FirstOrDefault().UserName;
                }

            }
            return userName;
        }

        public bool ValidatePassword(string userName, string password)
        {
            password = EncryptionHelper.Encrypt(password);
            using (var db = new E2EWebPortalEntities())
            {
                return db.UserAccounts.Where(u => u.UserName == userName && u.Password == password).Any();
            }
        }

        public void resetPassword(string userName, string password, string resetCode)
        {

            password = EncryptionHelper.Encrypt(password);
            using (var db = new E2EWebPortalEntities())
            {
                db.UserAccounts.Where(u => u.UserName == userName).FirstOrDefault().Password = password;
                var user = db.UserMIS.Where(u => u.ResetCode == resetCode).FirstOrDefault();
                user.ResetCode = string.Empty;
                user.FailAttempts = 0;
                user.IsBlocked = 0;
                db.SaveChanges();
            }
        }

        public int AddEmpAdminUser(string userName, string password, int employerID, int roleID, int active, string adminUserFirstName, string adminuserMiddleName,
            string adminUserLastName, string adminUserNickName, string adminTitle, string address1, string address2, string city, string state, string zip,
            string workPhoneNumber, string extn, string cellPhoneNumber, string primaryEmail, string secondaryEmail, bool isPrimary)
        {
            password = EncryptionHelper.Encrypt(password);
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_InsertEmpAdminUser(userName, password, employerID, roleID, active, adminUserFirstName, adminuserMiddleName,
                        adminUserLastName, adminUserNickName, adminTitle, address1, address2, city, state, zip,
                        workPhoneNumber, extn, cellPhoneNumber, primaryEmail, secondaryEmail, isPrimary);
            }

        }

        public bool AddUserSendInvite(List<Invite> invites, int? EmployerID, string UserName)
        {
            //using (SqlConnection conn = new SqlConnection("data source=kumudini.database.windows.net;initial catalog=E2EWebPortal;persist security info=True;user id=SRV_E2EWebPortal;password=Admin@e2e;MultipleActiveResultSets=True;"))
            using (var db = new E2EWebPortalEntities())
            {
                var dt = new DataTable();
                dt.Columns.Add("FirstName");
                dt.Columns.Add("LastName");
                dt.Columns.Add("Email");
                dt.Columns.Add("RoleID");
                dt.Columns.Add("Notes");
                dt.Columns.Add("EmployerID");
                dt.Columns.Add("UserName");

                foreach (Invite invite in invites)
                {
                    dt.Rows.Add(invite.FirstName, invite.LastName, invite.Email, invite.Role, invite.AdditionalNotes, EmployerID, UserName);
                }


                var inviteList = new SqlParameter("InviteList", dt);
                inviteList.TypeName = "[dbo].[SendInvitation]";

                var users = db.Database.SqlQuery<int>("exec sp_AddUserSendInvite @InviteList", inviteList).ToList();

                int i = 0;
                foreach (int user in users)
                {
                    invites[i].UserID = user;
                    i++;
                }
                return users.Any();
            }
        }

        public bool IsUserAddedIntoUserAccount(int UserID, int RoleID)
        {
            using (var db = new E2EWebPortalEntities())
            {
                switch (RoleID)
                {
                    case (int)UserRoles.EmployerAdmin:
                        return db.UserAccounts.Any(u => u.AdminUserID == UserID);
                    case (int)UserRoles.Reviewer:
                        return db.UserAccounts.Any(u => u.ReviewerID == UserID);
                    case (int)UserRoles.Employee:
                        return db.UserAccounts.Any(u => u.EmployeeID == UserID);
                    default:
                        return true;
                }
            }
        }

        public int UpsertEmpAdminUser(EmployerAdminViewModal user)
        {
            user.Password = EncryptionHelper.Encrypt(user.Password);
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_UpsertEmpAdminUser(user.AdminUserID
                                                , user.UserName
                                                , user.Password
                                                , user.EmployerID
                                                , user.RoleID
                                                , user.Active
                                                , user.AdminUserFirstName
                                                , user.AdminuserMiddleName
                                                , user.AdminUserLastName
                                                , user.AdminUserNickName
                                                , user.AdminTitle
                                                , user.Address1
                                                , user.Address2
                                                , user.City
                                                , user.State
                                                , user.zip
                                                , user.WorkPhoneNumber
                                                , user.Extn
                                                , user.CellPhoneNumber
                                                , user.PrimaryEmail
                                                , user.SecondaryEmail
                                                , user.IsPrimary);
            }

        }

        public int UpsertReviewer(ReviewerViewModal user)
        {
            user.Password = EncryptionHelper.Encrypt(user.Password);
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_UpsertReviewer(user.ReviewerID
                                                , user.UserName
                                                , user.Password
                                                , user.EmployerID
                                                , user.RoleID
                                                , user.Active
                                                , user.ReviewerFirstName
                                                , user.ReviewerMiddleName
                                                , user.ReviewerLastName
                                                , user.ReviewerNickName
                                                , user.ReviewerTitle
                                                , user.DateOfBirth
                                                , user.Address1
                                                , user.Address2
                                                , user.City
                                                , user.State
                                                , user.zip
                                                , user.WorkPhoneNumber
                                                , user.Extn
                                                , user.CellPhoneNumber
                                                , user.PrimaryEmail
                                                , user.SecondaryEmail);
            }

        }

        public int UpsertEmployee(EmployeeViewModal user)
        {
            user.Password = EncryptionHelper.Encrypt(user.Password);
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_UpsertEmployee(user.EmployeeID
                                                , user.UserName
                                                , user.Password
                                                , user.EmployerID
                                                , user.RoleID
                                                , user.Active
                                                , user.FirstName
                                                , user.MiddleName
                                                , user.LastName
                                                , user.NickName
                                                , user.Title
                                                , user.DateOfBirth
                                                , user.Address1
                                                , user.Address2
                                                , user.City
                                                , user.State
                                                , user.zip
                                                , user.WorkPhoneNumber
                                                , user.Extn
                                                , user.CellPhoneNumber
                                                , user.PrimaryEmail
                                                , user.SecondaryEmail
                                                , user.CurrentVisaStatus
                                                , user.CurrentVisaValidity);
            }

        }

        public List<EmployerAdminViewModal> GetEmployerAdminList(int EmployerID, int AdminUserID = -1)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var users = db.sp_GetEmployerAdminList(EmployerID, AdminUserID).ToList();
                return Mapper.Map<List<sp_GetEmployerAdminList_Result>, List<EmployerAdminViewModal>>(users);
            }
        }

        public List<ReviewerViewModal> GetReviewerList(int EmployerID, int ReviewerID = -1)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var users = db.sp_GetReviewerList(EmployerID, ReviewerID).ToList();
                return Mapper.Map<List<sp_GetReviewerList_Result>, List<ReviewerViewModal>>(users);
            }
        }

        public List<EmployeeViewModal> GetEmployeeList(int EmployerID, int EmployeeID = -1)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var users = db.sp_GetEmployeeList(EmployerID, EmployeeID).ToList();
                return Mapper.Map<List<sp_GetEmployeeList_Result>, List<EmployeeViewModal>>(users);
            }
        }

        public int ManageUserActivation(int RoleID, int EmployerID, int UserID, string IsActive)
        {
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_ManageActivation_EA_Rew_Emp(RoleID, EmployerID, UserID, IsActive);
            }
        }

        public int DeleteUser(int RoleID, int EmployerID, int UserID)
        {
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_Delete_EA_Rew_Emp(RoleID, EmployerID, UserID);
            }
        }

        public int UpsertEndClient(EndClientViewModal client)
        {
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_UpsertEndClient( client.EndClientID
                                            , client.EmployeeID
                                            , client.EmployerID
                                            , client.EndClientBusinessName
                                            , client.EmployeeTitleAtEndClientSite
                                            , client.EndClientAddress1
                                            , client.EndClientAddress2
                                            , client.EndClientCity
                                            , client.EndClientState
                                            , client.EndClientzip
                                            , client.EndClientPhoneNumber
                                            , client.EndClientExtn
                                            , client.EmployeeEmailAtEndClient);
            }
        }

        public int DeleteEmployer(int EmployerID)
        {
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_Delete_Employer(EmployerID);
            }
        }

        public int GetSubscriptionIDByEmployerID(int EmployerID)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var subscription = db.Subscriptions.FirstOrDefault(s=>s.EmployerId == EmployerID);
                return subscription.SubscriptionID;
            }
        }

        public int UpdateLoginCount()
        {
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_UpdateLoginCount();
            }
        }

        public EmployeeViewModal GetEmployeeByID(int EmployeeID)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var employee = db.Employees.FirstOrDefault(e=>e.EmployeeID == EmployeeID);
                return Mapper.Map<Employee, EmployeeViewModal>(employee);
            }
        }

    }
}
