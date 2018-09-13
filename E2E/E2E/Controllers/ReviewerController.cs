using E2EInfrastructure.Helpers;
using E2ERepositories;
using E2ERepositories.Interface;
using E2EViewModals.Task;
using E2EViewModals.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E2E.Controllers
{
    [Authorize]
    public class ReviewerController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly TaskRepository _taskRepo;
        public ReviewerController(IUserRepository userRepo, TaskRepository taskRepo)
        {
            _userRepo = userRepo;
            _taskRepo = taskRepo;
        }

        // GET: Reviewer
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
            ViewBag.ReviewerID = Convert.ToInt16(userData[0]);
            ViewBag.RoleID = Convert.ToInt16(userData[3]);
            ViewBag.EmployerID = Convert.ToInt16(userData[4]);
            ViewBag.UserName = userData[5];

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SignUpReviewer(FormCollection form)
        {
            ReviewerViewModal user = new ReviewerViewModal();
            user.UserName = Convert.ToString(form["UserName"].ToString());
            user.Password = Convert.ToString(form["Password"].ToString());
            user.ReviewerID = Convert.ToInt16(form["ReviewerID"] != "" ? form["ReviewerID"].ToString() : "0");
            user.EmployerID = Convert.ToInt16(form["EmployerID"] != "" ? form["EmployerID"].ToString() : "0");
            user.RoleID = Convert.ToInt16(form["RoleID"] != "" ? form["RoleID"].ToString() : "0");
            user.Active = Convert.ToInt16(form["Active"] == "Yes" ? "1" : "0");
            user.ReviewerFirstName = Convert.ToString(form["ReviewerFirstName"].ToString());
            user.ReviewerMiddleName = Convert.ToString(form["ReviewerMiddleName"].ToString());
            user.ReviewerLastName = Convert.ToString(form["ReviewerLastName"].ToString());
            user.ReviewerNickName = Convert.ToString(form["ReviewerNickName"].ToString());
            user.ReviewerTitle = Convert.ToString(form["ReviewerTitle"].ToString());
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
            user.DateOfBirth = Convert.ToDateTime(form["DateOfBirth"] != "null" ? form["DateOfBirth"].ToString() : "1970/1/1");

            var result = _userRepo.UpsertReviewer(user);

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

        public ActionResult ManageComments()
        {
            return View();
        }

        public JsonResult SaveCommentData(FormCollection form)
        {
            var user = (UserViewModal)Session["User"];
            TaskReviewCommentViewModal comment = new TaskReviewCommentViewModal();
            comment.CommentID = Convert.ToInt16(form["CommentID"] != null ? form["CommentID"].ToString() : "0");
            comment.ReviewerID = Convert.ToInt16(user.Id);
            comment.EmployerID = user.EmployerID;
            comment.CommendDescription = Convert.ToString(form["CommendDescription"].ToString());


            var result = _taskRepo.UpsertComment(comment);

            if (result == -1)
            {
                return Json(new { Code = 1, Message = "Comment data has been saved successfully!" });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }


        }


        public ActionResult MyProfile()
        {
            var loggedInuser = (UserViewModal)Session["User"];
            var user = _userRepo.GetReviewerList(loggedInuser.EmployerID, loggedInuser.Id).FirstOrDefault();
            if (user.ReviewerID > 0 && !string.IsNullOrEmpty(user.Password))
            {
                user.Password = EncryptionHelper.Decrypt(user.Password);
            }
            return View(user);
        }

    }
}