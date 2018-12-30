using E2EInfrastructure.Helpers;
using E2ERepositories;
using E2ERepositories.Interface;
using E2EViewModals.Business;
using E2EViewModals.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E2EViewModals.Invitations;

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
                return Json(new { Code = 1, Message = "Employer has been " + (isActive ? "activated" : "deactivated") + " successfully." });
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
            var user = (UserViewModal)Session["User"];

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
            DateTime EffectiveDate = Convert.ToDateTime(form["EffectiveDate"] != "null" ? form["EffectiveDate"].ToString() : "1970/1/1");
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
            string subject = "Create New E2EWebPortal Login Profile as Primary Employer Admin";
            String emailBody = "Hello, <br/><br/> Welcome to E2EWebPortal!!! <br/><br/> Please follow below instructions to setup new account as an Employer to start E2EWebPortal services. <br/><br/>"
                + "<ol><li>Please <a href='" + baseURL + "/EmployerAdmin/AddEmployerAdmin?code=" + HttpUtility.UrlEncode(Code) + "'>click here</a> to create new E2EWebport login profile as a new Employer and create primary Employer Admin profile. After clicking on link, you will be redirected to create new login profile form page. Please fill out required information to setup new profile.</li>"
                + "<li>Upon successful account setup, simply give us a call or send an email to verify and activate your account. Please mention ‘Account Activation+ &lt; your Business Name &gt;’ in email subject or when you call. </li>"
                + "<li>If you are <b>Free Trial</b> subscriber then your account will be activated within 4 business hours after receiving account activation request. Upon successful activation, simply go to E2EWebportal login page and enter your primary employer admin username and password and you will be login as Employer Admin.</li>"
                + "<li>If you are <b>paid subscriber</b> then we will send you an invoice, with amount due, on your primary email and will call you to notify. Once we receive full payment, your account will be activated within 4 business hours. Upon successful activation, simply go to E2EWebportal login page and enter your primary employer admin username and password and you will be login as Employer Admin.</li>"
                + "<li>Upon successful account activation, you will receive User Guide on following email. Please review this user guide to learn how to use this portal as a Employer Admin, Reviewer and Employee.</li></ol><br/><br/>"
                + "Feel free to call us for any issues or comment. <br/><br/>Thanks again for choosing E2EWEBPORTAL!!!! <br/><br/>"
                + "Thank you!!<br/>E2EWEBPORTAL"
                + "<br/><br/>IMPORTANT NOTICE:  The information contained in this electronic e-mail and any accompanying attachment(s) is intended only for the use of the intended recipient and may be confidential and/or legally protected.  If any reader of this communication is not the intended recipient, unauthorized use, disclosure, or copying is strictly prohibited, and may be unlawful.  If you have received this communication in error, please immediately notify the sender by replying this e-mail or forwarding this email to support@e2ewebportal.com with subject 'OPT-OUT'. Also,delete the original message and all copies from your system.";



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

        public ActionResult BulkInvitation()
        {
            var businessList = _businessRepo.GetBusinessList();
            ViewBag.Employers = businessList;
            return View();
        }

        [HttpPost]
        public JsonResult UploadBulkInvitation()
        {

            if (Request.Files != null && Request.Files.Count > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files[0].FileName).ToLower();
                string fileName = Guid.NewGuid() + extension;
                //AzureHelper.UploadFile("emailinvitecontainer", fileName, Request.Files[0]);

                string[] validFileTypes = { ".csv" };
                string path = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), fileName);

                if (validFileTypes.Contains(extension))
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                    }

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    Request.Files[0].SaveAs(path);

                    return Json(new { Code = 1, Message = "File is uploaded successfully.", FileName = fileName });
                }
                else
                {
                    return Json(new { Code = 0, Message = "File extension is not supported!", FileName = "" });
                }
            }
            else
            {
                return Json(new { Code = 0, Message = "File upload fails!", FileName = "" });
            }

        }

        [HttpPost]
        public JsonResult ProcessBulkInvitation(FormCollection form)
        {
            var user = (UserViewModal)Session["User"];
            string fileName = Convert.ToString(form["fileName"].ToString());
            int EmployerId = Convert.ToInt16(form["EmployerId"] != "" ? form["EmployerId"].ToString() : "0"); ;

            string connString = "";

            string path = string.Format("{0}\\{1}", Server.MapPath("~/Content/Uploads"), fileName);

            DataTable dt = new DataTable();
            if (fileName.Contains(".csv"))
            {
                dt = ExcelCsvHelper.ConvertCsvToDataTable(path);
            }
            //Connection String to Excel Workbook 
            else if (fileName.Contains(".xlsx"))
            {
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                dt = ExcelCsvHelper.ConvertXslxToDataTable(path, connString);
            }
            else if (fileName.Contains(".xls"))
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                dt = ExcelCsvHelper.ConvertXslxToDataTable(path, connString);
            }

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            if (dt.Columns.Count < 5)
            {
                return Json(new { Code = 1, Message = "Invalid CSV File. Please you correct CSV file." });
            }

            List<Invite> invites = new List<Invite>();

            foreach (DataRow dr in dt.Rows)
            {
                Invite invite = new Invite
                {
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    Email = dr["Email"].ToString(),
                    Role = Convert.ToInt16(dr["RoleID"].ToString()),
                    AdditionalNotes = dr["Notes"].ToString()
                };

                invites.Add(invite);
            }

            if (!invites.Any())
            {
                return Json(new { Code = 0, Message = "Something wrong is occured. Please try after sometime!" });
            }

            var isSuccess = _userRepo.AddUserSendInvite(invites, EmployerId, user.UserName);

            if (!isSuccess)
            {
                return Json(new { Code = 0, Message = "Something wrong is occured. Please try after sometime!" });
            }

            foreach (Invite invite in invites)
            {
                string code = EncryptionHelper.Encrypt(invite.UserID + "||" + invite.FirstName + "||" + invite.LastName + "||" + invite.Role + "||" + user.EmployerID + "||" + invite.Email);
                SendInvitationEmail(invite.Email, invite.AdditionalNotes, user.BusinessName, code);
            }

            return Json(new { Code = 1, Message = "Invitations are sent successfully!" });
        }

        private void SendInvitationEmail(string Email, string AdditionalNotes, string BusinessName, string Code)
        {
            //Email = "suresh.sanghani88@gmail.com";

            string baseURL = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            string subject = "Create New E2EWebPortal Login Profile";
            String emailBody = "Hello, <br/><br/> Welcome to E2EWebPortal!!! <br/><br/>"
                               + "Please <a href='" + baseURL + "/User/SignUp?code=" + HttpUtility.UrlEncode(Code) + "'>click here</a> to create new E2EWebport login profile. After clicking on link, you will be redirected to create new login profile form page. Please fill out required information to setup new login. <br/><br/>"
                               + (!string.IsNullOrEmpty(AdditionalNotes) ? "Additional Notes : " + AdditionalNotes + "<br/><br/> " : "")
                               + "Thank you!! <br/>" + BusinessName
                               + "<br/><br/>IMPORTANT NOTICE:  The information contained in this electronic e-mail and any accompanying attachment(s) is intended only for the use of the intended recipient and may be confidential and/or legally protected.  If any reader of this communication is not the intended recipient, unauthorized use, disclosure, or copying is strictly prohibited, and may be unlawful.  If you have received this communication in error, please immediately notify the sender by replying this e-mail or forwarding this email to support@e2ewebportal.com with subject 'OPT-OUT'. Also,delete the original message and all copies from your system.";

            string From = ConfigurationManager.AppSettings["FromEmail"] != null ? ConfigurationManager.AppSettings["FromEmail"].ToString() : "";
            EmailHelper.SendEmail(From, Email, subject, emailBody, null, "", true);
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