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
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly IBusinessRepository _businessRepo;
        public WebAppAdminController(ISubscriptionRepository SubscriptionRepo, IBusinessRepository businessRepo)
        {
            _subscriptionRepo = SubscriptionRepo;
            _businessRepo = businessRepo;
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
            DateTime PaymentDueDate = Convert.ToDateTime(form["PaymentDueDate"] != "null" ? form["PaymentDueDate"].ToString() : "");

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


        private void SendResetPasswordLink(string toEmail, string Code)
        {
            string baseURL = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            string subject = "Welcome to E2EWebPortal new account setup";
            String emailBody = "Hello, <br/><br/> Welcome to E2EWebPortal web applicaion. <br/><br/> Please follow below instructions to setup new account to start service : <br/><br/>"
                + "1. Click on below link to create new business account and primary Admin user. <br/><br/>"
                + "Link : <a href='" + baseURL + "/EmployerAdmin/AddEmployerAdmin?code=" + Code + "'>Click here</a> <br/><br/>"
                + "2. After completion of information fill out, your account will be created and you will be redirected to Payment page. <br/>"
                + "3. Please make payment through Paypal. <br/>"
                + "4. Upon successful payment process, your account will be activated within 1 business day. Meanwhile, review User manual to learn more about this application service. <br/>"
                + "5. Once account activated successfully, you can start to use this portal service. <br/><br/>"
                + "Thanks again for choosing E2EWebPortal!!! Feel free to call us for any issues or comment.<br/><br/>"
                + "Your Sincerely, <br/> E2EWebPortal Admin";


                
            string From = ConfigurationManager.AppSettings["FromEmail"] != null ? ConfigurationManager.AppSettings["FromEmail"].ToString() : "";
            EmailHelper.SendEmail(From, toEmail, subject, emailBody, null, "", true);
        }

    }
}