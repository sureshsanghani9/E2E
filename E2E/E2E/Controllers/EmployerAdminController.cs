using E2EInfrastructure.Helpers;
using E2ERepositories.Interface;
using E2EViewModals.Invitations;
using E2EViewModals.User;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            if (!string.IsNullOrEmpty(code) && EncryptionHelper.Decrypt(code).Split('|').Length >= 2)
            {
                ViewBag.EmployerID = EncryptionHelper.Decrypt(code).Split('|')[0];
                ViewBag.SubscriptionID = EncryptionHelper.Decrypt(code).Split('|')[1];
                ViewBag.RoleID = "2";
                return View();
            }
            else
            {
                TempData["ConfirmationType"] = "InvalidCode";
                return RedirectToAction("Confirmation", "Home");
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
            bool IsPrimary = Convert.ToBoolean(form["Primary"] == "Yes");



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

        public ActionResult ManageEmployee()
        {
            return View();
        }

        public ActionResult ManageAdmin()
        {
            var employerAdmin = _userRepo.GetEmployerAdminList();
            return View(employerAdmin);
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult Subscription()
        {
            return View();
        }

        public JsonResult SendInvitations(InvitationsViewModal Invitations)
        {
            var user = (UserViewModal)Session["User"];
            if (Invitations == null || Invitations.Invites == null)
            {
                return Json(new { Code = 0, Message = "Something wrong is occured. Please try after sometime!" });
            }
            else
            {
                var isSuccess = _userRepo.AddUserSendInvite(Invitations.Invites, user.EmployerID, user.UserName);

                if (!isSuccess)
                {
                    return Json(new { Code = 0, Message = "Something wrong is occured. Please try after sometime!" });
                }

                foreach (Invite invite in Invitations.Invites)
                {
                    string code = EncryptionHelper.Encrypt(invite.UserID + "||" + invite.FirstName + "||" + invite.LastName + "||" + invite.Role + "||" + user.EmployerID + "||" + invite.Email);
                    SendInvitationEmail(invite.Email, invite.AdditionalNotes, user.BusinessName, code);
                }

                return Json(new { Code = 1, Message = "Invitations are sent successfully!" });
            }
            
        }

        private void SendInvitationEmail(string Email, string AdditionalNotes, string BusinessName, string Code)
        {
            //Email = "suresh.sanghani88@gmail.com";

            string baseURL = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            string subject = "Create your new E2EWebPortal Login Profile.";
            String emailBody = "Hello, <br/><br/> Please click on following link/button to create New Login profile and password  for E2EWebPortal. <br/>"
                + "Link : <a href='" + baseURL + "/User/SignUp?code=" + HttpUtility.UrlEncode(Code) + "'>Click here</a> <br/><br/>"
                + (!string.IsNullOrEmpty(AdditionalNotes) ? "Additional Notes : " + AdditionalNotes + "<br/><br/> " : "")
                + "Regards, <br/> " + BusinessName + " ";



            string From = ConfigurationManager.AppSettings["FromEmail"] != null ? ConfigurationManager.AppSettings["FromEmail"].ToString() : "";
            EmailHelper.SendEmail(From, Email, subject, emailBody, null, "", true);
        }

        [AllowAnonymous]
        public ActionResult Signup()
        {
            if (TempData["SignUpCode"] == null)
            {
                TempData["ConfirmationType"] = "AlreadySignUp";
                return RedirectToAction("Confirmation", "Home");
            }

            var userData = EncryptionHelper.Decrypt(TempData["SignUpCode"].ToString()).Split(new string[] { "||" }, StringSplitOptions.None);

            ViewBag.FirstName = userData[1];
            ViewBag.LastName = userData[2];
            ViewBag.AdminUserID = Convert.ToInt16(userData[0]);
            ViewBag.RoleID = Convert.ToInt16(userData[3]);
            ViewBag.EmployerID = Convert.ToInt16(userData[4]);
            ViewBag.UserName = userData[5];

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SignUpEmployerAdmin(FormCollection form)
        {
            EmployerAdminViewModal user = new EmployerAdminViewModal();
            user.UserName = Convert.ToString(form["UserName"].ToString());
            user.Password = Convert.ToString(form["Password"].ToString());
            user.AdminUserID = Convert.ToInt16(form["AdminUserID"] != "" ? form["AdminUserID"].ToString() : "0");
            user.EmployerID = Convert.ToInt16(form["EmployerID"] != "" ? form["EmployerID"].ToString() : "0");
            user.RoleID = Convert.ToInt16(form["RoleID"] != "" ? form["RoleID"].ToString() : "0");
            user.Active = Convert.ToInt16(form["Active"] == "Yes" ? "1" : "0");
            user.AdminUserFirstName = Convert.ToString(form["AdminUserFirstName"].ToString());
            user.AdminuserMiddleName = Convert.ToString(form["AdminuserMiddleName"].ToString());
            user.AdminUserLastName = Convert.ToString(form["AdminUserLastName"].ToString());
            user.AdminUserNickName = Convert.ToString(form["AdminUserNickName"].ToString());
            user.AdminTitle = Convert.ToString(form["AdminTitle"].ToString());
            user.Address1 = Convert.ToString(form["Address1"].ToString());
            user.Address2 = Convert.ToString(form["Address2"].ToString());
            user.City = Convert.ToString(form["City"].ToString());
            user.State = Convert.ToString(form["State"].ToString());
            user.zip = Convert.ToString(form["zip"].ToString());
            user.WorkPhoneNumber = Convert.ToString(form["WorkPhoneNumber"].ToString());
            user.Extn = Convert.ToString(form["Extn"].ToString());
            user.CellPhoneNumber = Convert.ToString(form["CellPhoneNumber"].ToString());
            user.PrimaryEmail = Convert.ToString(form["PrimaryEmail"].ToString());
            user.SecondaryEmail = Convert.ToString(form["SecondaryEmail"].ToString());
            user.IsPrimary = Convert.ToBoolean(form["Primary"] == "Yes");



            var result = _userRepo.UpsertEmpAdminUser(user);

            if (result == -1)
            {
                TempData["ConfirmationType"] = "SignUpSuccessful"; 
                return Json(new { Code = 1, Message = "You are registered successfully with E2EWebPortal." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }


        }

        public ActionResult EditAdmin(int adminUserID)
        {
            var employerAdmin = _userRepo.GetEmployerAdminList(adminUserID).FirstOrDefault();
            if (employerAdmin.AdminUserID > 0)
            {
                employerAdmin.Password = EncryptionHelper.Decrypt(employerAdmin.Password);
            }
            return View(employerAdmin);
        }

    }
}