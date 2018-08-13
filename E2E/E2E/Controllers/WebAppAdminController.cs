using E2ERepositories;
using E2ERepositories.Interface;
using E2EViewModals.Business;
using E2EViewModals.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E2E.Controllers
{
    [Authorize]
    public class WebAppAdminController : Controller
    {
        // GET: WebAppAdmin
        public ActionResult Index()
        {
            ISubscriptionRepository repo = new SubscriptionRepository();
            var subscriptionInfo = repo.GetSubscriptionInfo();
            return View(subscriptionInfo);
        }

        public ActionResult ManageEmployer()
        {
            IBusinessRepository repo = new BusinessRepository();
            var businessList = repo.GetBusinessList();
            return View(businessList);
        }

        [HttpPost]
        public JsonResult ActiveDeactiveEmployer(int employerId, bool isActive)
        {
            IBusinessRepository repo = new BusinessRepository();
            int result = repo.ManageBusinessActivation(employerId, isActive ? "Activate" : "Deactivate");
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
            int TotalEmployees = Convert.ToInt16(form["TotalEmployees"].ToString());
            string BusinessTaxID = Convert.ToString(form["BusinessTaxID"].ToString());
            string Active = Convert.ToString(form["Active"].ToString());
            string UserName = user.UserName;
            DateTime SubscriptionDate = Convert.ToDateTime(form["SubscriptionDate"].ToString());
            string ServiceDetails = Convert.ToString(form["ServiceDetails"].ToString());
            string SubscriptionType = Convert.ToString(form["SubscriptionType"].ToString());
            string SubscriptionPlanName = Convert.ToString(form["SubscriptionPlanName"].ToString());
            string SubscriptionPlanCode = Convert.ToString(form["SubscriptionPlanCode"].ToString());
            int TotalLogin = Convert.ToInt16(form["TotalLogin"].ToString());
            DateTime EffectiveDate = Convert.ToDateTime(form["EffectiveDate"].ToString());
            DateTime ExpirationDate = Convert.ToDateTime(form["ExpirationDate"].ToString());
            decimal AmountCharged = Convert.ToDecimal(form["AmountCharged"].ToString());
            decimal RegistrationFeeCharged = Convert.ToDecimal(form["RegistrationFeeCharged"].ToString());
            decimal SubscriptionFeeCharged = Convert.ToDecimal(form["SubscriptionFeeCharged"].ToString());
            DateTime PaymentDueDate = Convert.ToDateTime(form["PaymentDueDate"].ToString());

            ISubscriptionRepository repo = new SubscriptionRepository();
            var result = repo.InsertNewBusiness(EmployerName
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

            if (result == -1)
            {
                return Json(new { Code = 1, Message = "Employer has been added successfully." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }
        }
    }
}