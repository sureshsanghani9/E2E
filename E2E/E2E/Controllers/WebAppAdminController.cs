using E2EInfrastructure.Helpers;
using E2ERepositories;
using E2ERepositories.Interface;
using E2EViewModals.Business;
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
    public class WebAppAdminController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly IBusinessRepository _businessRepo;
        
        public WebAppAdminController(ISubscriptionRepository SubscriptionRepo, IBusinessRepository businessRepo, IUserRepository userRepo, ITaskRepository taskRepo)
        {
            _subscriptionRepo = SubscriptionRepo;
            _businessRepo = businessRepo;
            _userRepo = userRepo;
        }

        // GET: WebAppAdmin
        public ActionResult Index()
        {
            var subscriptionInfo = _subscriptionRepo.GetSubscriptionInfo();
            return View(subscriptionInfo);
        }

        public ActionResult ManageEmployer()
        {
            var businessList = _businessRepo.GetBusinessList();
            return View(businessList);
        }

        [HttpPost]
        public JsonResult ActiveDeactiveEmployer(int employerId, bool isActive)
        {
            int result = _businessRepo.ManageBusinessActivation(employerId, isActive ? "Activate" : "Deactivate");
            if (result == -1)
            {
                return Json(new { Code = 1, Message = "Employer has been "+ (isActive ? "activated" : "deactivated") + " successfully." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }
            
        }

        public ActionResult AddEmployer()
        {
            var modal = new BusinessViewModal();
            return View(modal);
        }

        [HttpPost]
        public JsonResult AddEmployerData(FormCollection form)
        {
            var user = (UserViewModal) Session["User"];

            string EmployerName = Convert.ToString(form["EmployerName"].ToString());
            string BusinessName = Convert.ToString(form["BusinessName"].ToString());
            string BusinessAddress1 = Convert.ToString(form["BusinessAddress1"].ToString());
            string BusinessAddress2 = Convert.ToString(form["BusinessAddress2"].ToString());
            string City = Convert.ToString(form["City"].ToString());
            string State = Convert.ToString(form["State"].ToString());
            string zip = Convert.ToString(form["zip"].ToString());
            string Phone = Convert.ToString(form["Phone"].ToString());
            string Fax = Convert.ToString(form["Fax"].ToString());
            string PrimaryEmail = Convert.ToString(form["PrimaryEmail"].ToString());
            string SecondaryEmail = Convert.ToString(form["SecondaryEmail"].ToString());
            string URL = Convert.ToString(form["URL"].ToString());
            int TotalEmployees = Convert.ToInt16(form["TotalEmployees"] != "" ? form["TotalEmployees"].ToString() : "0");
            string BusinessTaxID = Convert.ToString(form["BusinessTaxID"].ToString());
            string Active = Convert.ToString(form["Active"].ToString()) == "Yes" ? "1" : "0";
            string UserName = user.UserName;
            DateTime SubscriptionDate = Convert.ToDateTime(form["SubscriptionDate"] != "null" ? form["SubscriptionDate"].ToString() : "1970/1/1");
            string ServiceDetails = Convert.ToString(form["ServiceDetails"].ToString());
            string SubscriptionType = Convert.ToString(form["SubscriptionType"].ToString());
            string SubscriptionPlanName = Convert.ToString(form["SubscriptionPlanName"].ToString());
            string SubscriptionPlanCode = Convert.ToString(form["SubscriptionPlanCode"].ToString());
            int TotalLogin = Convert.ToInt16(form["TotalLogin"] != "" ? form["TotalLogin"].ToString() : "0");
            DateTime EffectiveDate = Convert.ToDateTime(form["EffectiveDate"] != "null" ? form["EffectiveDate"].ToString(): "1970/1/1");
            DateTime ExpirationDate = Convert.ToDateTime(form["ExpirationDate"] != "null" ? form["ExpirationDate"].ToString() : "1970/1/1");
            decimal AmountCharged = Convert.ToDecimal(form["AmountCharged"] != "" ? form["AmountCharged"].ToString() : "0");
            decimal RegistrationFeeCharged = Convert.ToDecimal(form["RegistrationFeeCharged"] != "" ? form["RegistrationFeeCharged"].ToString() : "0");
            decimal SubscriptionFeeCharged = Convert.ToDecimal(form["SubscriptionFeeCharged"] != "" ? form["SubscriptionFeeCharged"].ToString() : "0");
            DateTime PaymentDueDate = Convert.ToDateTime(form["PaymentDueDate"] != "null" ? form["PaymentDueDate"].ToString() : "1970/1/1");

            var result = _businessRepo.InsertNewBusiness(EmployerName
                                    , BusinessName
                                    , BusinessAddress1
                                    , BusinessAddress2
                                    , City
                                    , State
                                    , zip
                                    , Phone
                                    , Fax
                                    , PrimaryEmail
                                    , SecondaryEmail
                                    , URL
                                    , TotalEmployees
                                    , BusinessTaxID
                                    , Active
                                    , UserName
                                    , SubscriptionDate
                                    , ServiceDetails
                                    , SubscriptionType
                                    , SubscriptionPlanName
                                    , SubscriptionPlanCode
                                    , TotalLogin
                                    , EffectiveDate
                                    , ExpirationDate
                                    , AmountCharged
                                    , RegistrationFeeCharged
                                    , SubscriptionFeeCharged
                                    , PaymentDueDate);

            if (result.EmployerID > 0)
            {
                string code = EncryptionHelper.Encrypt(result.EmployerID.ToString() + "|" + result.SubscriptionID.ToString());
                SendResetPasswordLink(PrimaryEmail, code);

                return Json(new { Code = 1, Message = "Employer has been added successfully." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }
        }


        public ActionResult EditAdmin(int adminUserID, int employerId)
        {
            var employerAdmin = _userRepo.GetEmployerAdminList(employerId, adminUserID).FirstOrDefault();
            if (employerAdmin.AdminUserID > 0 && !string.IsNullOrEmpty(employerAdmin.Password))
            {
                employerAdmin.Password = EncryptionHelper.Decrypt(employerAdmin.Password);
            }
            return View(employerAdmin);
        }

        [HttpPost]
        public JsonResult SaveEmployerAdmin(FormCollection form)
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

        [HttpPost]
        public JsonResult DeleteEmployer(int roleID, int employerID, int userID)
        {
            int result = _userRepo.DeleteEmployer(employerID);
            if (result == -1)
            {
                return Json(new { Code = 1, Message = "User has been deleted successfully." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }

        }

        private void SendResetPasswordLink(string toEmail, string Code)
        {
            //toEmail = "suresh.sanghani88@gmail.com";
            string baseURL = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            string subject = "Welcome to E2EWebPortal new account setup";
            String emailBody = "Hello, <br/><br/> Welcome to E2EWebPortal web applicaion. <br/><br/> Please follow below instructions to setup new account to start service : <br/><br/>"
                + "1. Click on below link to create new business account and primary Admin user. <br/><br/>"
                + "Link : <a href='" + baseURL + "/EmployerAdmin/AddEmployerAdmin?code=" + HttpUtility.UrlEncode(Code) + "'>Click here</a> <br/><br/>"
                + "2. After completion of information fill out, your account will be created and you will be redirected to Payment page. <br/>"
                + "3. Please make payment through Paypal. <br/>"
                + "4. Upon successful payment process, your account will be activated within 1 business day. Meanwhile, review User manual to learn more about this application service. <br/>"
                + "5. Once account activated successfully, you can start to use this portal service. <br/><br/>"
                + "Thanks again for choosing E2EWebPortal!!! Feel free to call us for any issues or comment.<br/><br/>"
                + "Your Sincerely, <br/> E2EWebPortal Admin";


                
            string From = ConfigurationManager.AppSettings["FromEmail"] != null ? ConfigurationManager.AppSettings["FromEmail"].ToString() : "";
            EmailHelper.SendEmail(From, toEmail, subject, emailBody, null, "", true);
        }

        [HttpPost]
        public JsonResult SendAccountSetupLink(int employerID, string primaryEmail)
        {
            int subscriptionID = _userRepo.GetSubscriptionIDByEmployerID(employerID);
            if (subscriptionID > 0)
            {
                string code = EncryptionHelper.Encrypt(employerID.ToString() + "|" + subscriptionID.ToString());
                SendResetPasswordLink(primaryEmail, code);
                return Json(new { Code = 1, Message = "Email has been sent successfully." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }

        }

        [HttpPost]
        public JsonResult UpdateLoginCount()
        {
            int subscriptionID = _userRepo.UpdateLoginCount();
            return Json(new { Code = 1, Message = "Login Count has been updated successfully." });

        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult ManageAlerts()
        {
            return View();
        }

        public ActionResult Subscription()
        {
            return View();
        }

    }
}