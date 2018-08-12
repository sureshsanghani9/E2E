using E2ERepositories;
using E2ERepositories.Interface;
using E2EViewModals.Business;
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
    }
}