﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E2E.Controllers
{
    [Authorize]
    public class EmployerAdminController : Controller
    {
        // GET: EmployerAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}