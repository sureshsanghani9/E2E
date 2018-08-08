using E2EInfrastructure.Helpers;
using E2ERepositories;
using E2ERepositories.Interface;
using E2EViewModals.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E2E.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string returnUrl)
        {
            HttpCookie cookie = Request.Cookies.Get("E2EWebPortalCookies");
            if (cookie != null)
            {
                IUserRepository repo = new UserRepository();
                
                string value = EncryptionHelper.Decrypt(cookie.Value);
                ViewBag.UserName = value.Split('-')[1];
                ViewBag.Password = value.Split('-')[2];
                //UserAccount user = repo.GetUser(Convert.ToInt16(value.Split('-')[0]));
                //if (user != null && user.UserAccountID != 0)
                //{
                //    FormsAuthentication.SetAuthCookie(value, true);
                //    if (string.IsNullOrEmpty(returnUrl))
                //        return RedirectToAction("Dashboard", "Home");
                //    return RedirectToLocal(returnUrl);
                //}
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public JsonResult Login(string username, string password, bool rememberMe)
        {
            IUserRepository repo = new UserRepository();
            UserViewModal user = repo.GetUser(username, password);

            if (user != null && user.UserId != 0 && user.UserId != -1)
            {
                //FormsAuthentication.SignOut();
                FormsAuthentication.SetAuthCookie(EncryptionHelper.Encrypt(user.UserId.ToString()+ "-"+ user.UserName.ToString() + "-" + password), rememberMe);

                RemoveCookie("E2EWebPortalCookies", Request, Response);
                if (rememberMe)
                {
                    var cookie = new HttpCookie("E2EWebPortalCookies", EncryptionHelper.Encrypt(user.UserId.ToString() + "-" + user.UserName.ToString() + "-" + password));
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(cookie);
                }

                Session["User"] = user;
                return Json(new { Code = 1, Message = "You are logged in successfully!" });
            }
            else
            {
                if (user != null && user.UserId == -1)
                {
                    if (user.Id == 5)
                    {
                        return Json(new { Code = -1, Message = "You account has been locked. Please reset your password." });
                    }
                    else
                    {
                        return Json(new { Code = -2, Message = "Login Failed! " + (5 - user.Id).ToString() + " Attemps left!" });
                    }

                }
                else
                {
                    return Json(new { Code = 0, Message = "Login Failed! No User found!" });
                }
            }
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        public ActionResult ResetPassword(string ResetCode)
        {
            ViewBag.ResetCode = ResetCode;
            return View();
        }

        [HttpPost]
        public JsonResult ForgetPassword(string userName)
        {
            IUserRepository repo = new UserRepository();
            string resetCode = repo.SetResetCode(userName);

            if (!string.IsNullOrEmpty(resetCode))
            {
                TempData["ConfirmationType"] = "EmailSent";
                SendResetPasswordLink(userName, resetCode);
                return Json(new { Code = 1, Message = "Reset password link has been sent on your email." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Invalid User Name!" });
            }
        }

        [HttpPost]
        public JsonResult ResetPassword(string password, string resetCode)
        {
            IUserRepository repo = new UserRepository();
            string userName = repo.ValidateResetCode(resetCode);
            if (string.IsNullOrEmpty(userName))
            {
                return Json(new { Code = -1, Message = "Invalid Reset Link!" });
            }
            repo.resetPassword(userName, password, resetCode);
            TempData["ConfirmationType"] = "PasswordReset";
            return Json(new { Code = 1, Message = "Password has been reset successfully!" });
        }

        public ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Dashboard", "Home");
            }
        }

        public ActionResult LogOff()
        {
            //RemoveCookie("E2EWebPortalCookies", Request, Response);
            Session.Remove("User");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Confirmation()
        {
            return View();
        }

        public ActionResult ForgetUserName()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            var user = (UserViewModal)Session["User"];
            if(user.RoleID == 1)
            {
                return RedirectToAction("Index", "WebAppAdmin");
            }
            else if(user.RoleID == 2)
            {
                return RedirectToAction("Index", "EmployerAdmin");
            }
            else if(user.RoleID == 3)
            {
                return RedirectToAction("Index", "Reviewer");
            }
            else if(user.RoleID == 4)
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private void RemoveCookie(string p, HttpRequestBase Request, HttpResponseBase Response)
        {
            HttpCookie currentUserCookie = Request.Cookies[p];
            if (currentUserCookie != null)
            {
                Response.Cookies.Remove(p);
                currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                currentUserCookie.Value = null;
                Response.SetCookie(currentUserCookie);
            }
        }

        private void SendResetPasswordLink(string userName, string resetCode)
        {
            string baseURL = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            string subject = "Complete your E2EWebPortal password reset request";
            string emailBody = "hey, <br/><br/> Please use following link/button to reset password. <br/><br/> Reset Link : <a href='" + baseURL + "/Home/ResetPassword?resetCode=" + resetCode + "'>Click here</a>"
                               + "<br/><br/> Regards,<br/> E2EWebPortal";
            string From = ConfigurationManager.AppSettings["FromEmail"] != null ? ConfigurationManager.AppSettings["FromEmail"].ToString() : "";
            EmailHelper.SendEmail(From, userName, subject, emailBody, null, "", true);
        }
    }
}