using E2ERepositories;
using E2ERepositories.Interface;
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
    }
}