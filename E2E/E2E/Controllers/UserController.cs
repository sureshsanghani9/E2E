using E2EInfrastructure.Helpers;
using E2ERepositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static E2EViewModals.CommanEnums;

namespace E2E.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // GET: User
        public ActionResult SignUp(string code)
        {
            var userData = EncryptionHelper.Decrypt(code).Split(new string[] { "||" }, StringSplitOptions.None);
            if (userData.Length != 6)
            {
                TempData["ConfirmationType"] = "InvalidCode";
                return RedirectToAction("Confirmation", "Home");
            }

            var FirstName = userData[1];
            var LastName = userData[2];
            var UserID = Convert.ToInt16(userData[0]);
            var RoleID = Convert.ToInt16(userData[3]);

            if (_userRepo.IsUserAddedIntoUserAccount(UserID, RoleID))
            {
                TempData["ConfirmationType"] = "AlreadySignUp";
                return RedirectToAction("Confirmation", "Home");
            }

            TempData["SignUpCode"] = code;

            switch (RoleID)
            {
                case (int)UserRoles.EmployerAdmin:
                    return RedirectToAction("SignUp", "EmployerAdmin");
                case (int)UserRoles.Reviewer:
                    return RedirectToAction("SignUp", "Reviewer");
                case (int)UserRoles.Employee:
                    return RedirectToAction("SignUp", "Employee");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}