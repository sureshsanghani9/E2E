using E2EInfrastructure.Helpers;
using E2ERepositories.Interface;
using E2EViewModals.EndClient;
using E2EViewModals.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E2E.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUserRepository _userRepo;
        public EmployeeController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        // GET: Employee
        public ActionResult Index()
        {
            return View();
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
            ViewBag.EmployeeID = Convert.ToInt16(userData[0]);
            ViewBag.RoleID = Convert.ToInt16(userData[3]);
            ViewBag.EmployerID = Convert.ToInt16(userData[4]);
            ViewBag.UserName = userData[5];

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SignUpEmployerAdmin(FormCollection form)
        {
            EmployeeViewModal user = new EmployeeViewModal();
            user.UserName = Convert.ToString(form["UserName"].ToString());
            user.Password = Convert.ToString(form["Password"].ToString());
            user.EmployeeID = Convert.ToInt16(form["EmployeeID"] != "" ? form["EmployeeID"].ToString() : "0");
            user.EmployerID = Convert.ToInt16(form["EmployerID"] != "" ? form["EmployerID"].ToString() : "0");
            user.RoleID = Convert.ToInt16(form["RoleID"] != "" ? form["RoleID"].ToString() : "0");
            user.Active = Convert.ToInt16(form["Active"] == "Yes" ? "1" : "0");
            user.FirstName = Convert.ToString(form["FirstName"].ToString());
            user.MiddleName = Convert.ToString(form["MiddleName"].ToString());
            user.LastName = Convert.ToString(form["LastName"].ToString());
            user.NickName = Convert.ToString(form["NickName"].ToString());
            user.Title = Convert.ToString(form["Title"].ToString());
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
            user.CurrentVisaStatus = Convert.ToString(form["CurrentVisaStatus"].ToString());
            user.CurrentVisaValidity = Convert.ToString(form["CurrentVisaValidity"].ToString());
            user.DateOfBirth = Convert.ToDateTime(form["DateOfBirth"] != "null" ? form["DateOfBirth"].ToString() : "1970/1/1");



            var result = _userRepo.UpsertEmployee(user);

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

        public ActionResult AddEndClient()
        {
            return View();
        }

        public ActionResult ManageEndClients()
        {
            return View();
        }

        public JsonResult SaveEndClientData(FormCollection form)
        {
            var user = (UserViewModal)Session["User"];
            EndClientViewModal client = new EndClientViewModal();
            client.EndClientID = Convert.ToInt16(form["EndClientID"] != null ? form["EndClientID"].ToString() : "0");
            client.EmployeeID = Convert.ToInt16(user.Id);
            client.EmployerID  = user.EmployerID;
            client.EndClientBusinessName = Convert.ToString(form["EndClientBusinessName"].ToString());
            client.EmployeeTitleAtEndClientSite= Convert.ToString(form["EmployeeTitleAtEndClientSite"].ToString());
            client.EndClientAddress1 = Convert.ToString(form["EndClientAddress1"].ToString());
            client.EndClientAddress2 = Convert.ToString(form["EndClientAddress2"].ToString());
            client.EndClientCity = Convert.ToString(form["EndClientCity"].ToString());
            client.EndClientState = Convert.ToString(form["EndClientState"].ToString());
            client.EndClientzip = Convert.ToString(form["EndClientzip"].ToString());
            client.EndClientPhoneNumber = Convert.ToString(form["EndClientPhoneNumber"].ToString());
            client.EndClientExtn = Convert.ToString(form["EndClientExtn"].ToString());
            client.EmployeeEmailAtEndClient = Convert.ToString(form["EmployeeEmailAtEndClient"].ToString());


            var result = _userRepo.UpsertEndClient(client);

            if (result == -1)
            {
                return Json(new { Code = 1, Message = "End Client data has been saved successfully!" });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }


        }

        public ActionResult MyProfile()
        {
            var loggedInuser = (UserViewModal)Session["User"];
            var user = _userRepo.GetEmployeeList(loggedInuser.EmployerID, loggedInuser.Id).FirstOrDefault();
            if (user.EmployeeID > 0 && !string.IsNullOrEmpty(user.Password))
            {
                user.Password = EncryptionHelper.Decrypt(user.Password);
            }
            return View(user);
        }

    }
}