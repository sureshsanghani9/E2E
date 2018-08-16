using E2ERepositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2EViewModals.User;
using AutoMapper;
using E2EInfrastructure.Helpers;

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
                        adminUserLastName,  adminUserNickName, adminTitle, address1, address2, city, state, zip,
                        workPhoneNumber, extn, cellPhoneNumber, primaryEmail, secondaryEmail, isPrimary);
            }

        }

    }
}
