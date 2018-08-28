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

        [HttpPost]
        public JsonResult ManageUserActivation(int roleID, int employerID, int userID, bool isActive)
        {
            int result = _userRepo.ManageUserActivation(roleID, employerID, userID, isActive ? "1" : "0");
            if (result == -1)
            {
                return Json(new { Code = 1, Message = "User has been " + (isActive ? "activated" : "deactivated") + " successfully." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }

        }

        [HttpPost]
        public JsonResult DeleteUser(int roleID, int employerID, int userID)
        {
            int result = _userRepo.DeleteUser(roleID, employerID, userID);
            if (result == -1)
            {
                return Json(new { Code = 1, Message = "User has been deleted successfully." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }

        }

    }
}