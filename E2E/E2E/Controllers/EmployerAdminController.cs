using E2EInfrastructure.Helpers;
using E2ERepositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E2E.Controllers
{
    [Authorize]
    public class EmployerAdminController : Controller
    {
        private readonly IUserRepository _userRepo;
        public EmployerAdminController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // GET: EmployerAdmin
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AddEmployerAdmin(string code)
        {
            if (!string.IsNullOrEmpty(code) && EncryptionHelper.Decrypt(code).Split('|').Length > 2)
            {
                ViewBag.EmployerID = EncryptionHelper.Decrypt(code).Split('|')[0];
                ViewBag.SubscriptionID = EncryptionHelper.Decrypt(code).Split('|')[1];
                ViewBag.RoleID = "2";
                return View();
            }
            else
            {
                TempData["ConfirmationType"] = "InvalidCode";
                return RedirectToAction("Home", "Confirmation");
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult AddEmployerAdminData(FormCollection form)
        {
            string UserName = Convert.ToString(form["UserName"].ToString());
            string Password = Convert.ToString(form["Password"].ToString());
            int EmployerID = Convert.ToInt16(form["EmployerID"] != "" ? form["EmployerID"].ToString() : "0");
            int RoleID = Convert.ToInt16(form["RoleID"] != "" ? form["RoleID"].ToString() : "0");
            int Active = Convert.ToInt16(form["Active"] == "Yes" ? "1" : "0");
            string AdminUserFirstName = Convert.ToString(form["AdminUserFirstName"].ToString());
            string AdminuserMiddleName = Convert.ToString(form["AdminuserMiddleName"].ToString());
            string AdminUserLastName = Convert.ToString(form["AdminUserLastName"].ToString());
            string AdminUserNickName = Convert.ToString(form["AdminUserNickName"].ToString());
            string AdminTitle = Convert.ToString(form["AdminTitle"].ToString());
            string Address1 = Convert.ToString(form["Address1"].ToString());
            string Address2 = Convert.ToString(form["Address2"].ToString());
            string City = Convert.ToString(form["City"].ToString());
            string State = Convert.ToString(form["State"].ToString());
            string zip = Convert.ToString(form["zip"].ToString());
            string WorkPhoneNumber = Convert.ToString(form["WorkPhoneNumber"].ToString());
            string Extn = Convert.ToString(form["Extn"].ToString());
            string CellPhoneNumber = Convert.ToString(form["CellPhoneNumber"].ToString());
            string PrimaryEmail = Convert.ToString(form["PrimaryEmail"].ToString());
            string SecondaryEmail = Convert.ToString(form["SecondaryEmail"].ToString());
            bool IsPrimary = Convert.ToBoolean(form["IsPrimary"] == "Yes");



            var result = _userRepo.AddEmpAdminUser(UserName, Password, EmployerID, RoleID, Active, AdminUserFirstName, AdminuserMiddleName, AdminUserLastName
                        , AdminUserNickName, AdminTitle, Address1, Address2, City, State, zip, WorkPhoneNumber
                        , Extn, CellPhoneNumber, PrimaryEmail, SecondaryEmail, IsPrimary);

            if (result > 0)
            {
                return Json(new { Code = 1, Message = "Employer Admin has been added successfully." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }


        }

    }
}