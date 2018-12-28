using E2EInfrastructure.Helpers;
using E2ERepositories;
using E2ERepositories.Interface;
using E2EViewModals.Task;
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
    public class ReviewerController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly ITaskRepository _taskRepo;
        public ReviewerController(IUserRepository userRepo, ITaskRepository taskRepo)
        {
            _userRepo = userRepo;
            _taskRepo = taskRepo;
        }

        // GET: Reviewer
        public ActionResult Index()
        {
            var loggedInuser = (UserViewModal)Session["User"];
            string weekPeriod = string.Empty;
            var weekPeriods = _taskRepo.GetListWeekPeriod(loggedInuser.EmployerID, loggedInuser.Id, loggedInuser.RoleID, out weekPeriod);
            var comments = _taskRepo.GetAllReviewComments(loggedInuser.EmployerID).OrderByDescending(c => c.IsDefault).ToList();

            weekPeriod = TempData["CurrentWeekPeriod"] != null ? TempData["CurrentWeekPeriod"].ToString() : weekPeriod;
            var tasks = _taskRepo.GetTaskDetailsByWeekPeriod(loggedInuser.RoleID, loggedInuser.EmployerID, 0, loggedInuser.Id, weekPeriod);

            ViewBag.Comments = comments;
            ViewBag.CurrentWeekPeriod = weekPeriod;
            ViewBag.Tasks = tasks;
            ViewBag.TaskIds = string.Join<string>(",", tasks.Select(t => t.TaskID.ToString()));
            ViewBag.NumberOfTask = tasks.Count.ToString();
            ViewBag.ReviewerName = loggedInuser.FirstName;

            return View(weekPeriods);
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
            var loggedInuser = (UserViewModal)Session["User"];
            var comments = _taskRepo.GetAllReviewComments(loggedInuser.EmployerID);
            return View(comments);
        }

        public JsonResult SaveCommentData(FormCollection form)
        {
            var user = (UserViewModal)Session["User"];
            TaskReviewCommentViewModal comment = new TaskReviewCommentViewModal();
            comment.CommentID = Convert.ToInt16(form["CommentID"] != null ? form["CommentID"].ToString() : "0");
            comment.ReviewerID = Convert.ToInt16(user.Id);
            comment.EmployerID = user.EmployerID;
            comment.CommendDescription = Convert.ToString(form["CommendDescription"].ToString());
            comment.IsDefault = Convert.ToString(form["IsDefault"].ToString());


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

        [HttpPost]
        public JsonResult EditComment(int commentID)
        {
            var loggedInuser = (UserViewModal)Session["User"];
            var comments = _taskRepo.GetAllReviewComments(loggedInuser.EmployerID, commentID);

            if (comments.Any())
            {
                return Json(new { Code = 1, Message = "Data retrived successfully!", Data = comments.FirstOrDefault() });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!", Data = new TaskReviewCommentViewModal() });
            }
        }

        [HttpPost]
        public JsonResult MakeDefaultTaskReviewComment(int commentID, int employerId, bool isDefault)
        {
            int result = _taskRepo.MakeDefaultTaskReviewComment(commentID, employerId, isDefault ? "Yes" : "No");
            if (result == -1)
            {
                return Json(new { Code = 1, Message = "Comment has been marked " + (isDefault ? "default" : "not a default") + " successfully." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }

        }

        [HttpPost]
        public JsonResult DeleteReviewComments(int commentID, int employerId)
        {
            int result = _taskRepo.DeleteReviewComments(commentID, employerId);
            if (result == -1)
            {
                return Json(new { Code = 1, Message = "Comment has been deleted successfully." });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }

        }

        [HttpPost]
        public JsonResult SetCurrentWeekPeriod(string weekPeriod)
        {
            TempData["CurrentWeekPeriod"] = weekPeriod;
            return Json(new { Code = 1, Message = "Current Week Period has been set successfully." });
        }

        [HttpPost]
        public JsonResult SaveTaskDetails(FormCollection form)
        {
            var loggedInuser = (UserViewModal)Session["User"];
            var NumberOfTask = Convert.ToString(form["NumberOfTask"].ToString());
            var TaskIds = Convert.ToString(form["TaskIds"].ToString()).Split(',');
            var CurrentWeekPeriod = Convert.ToString(form["CurrentWeekPeriod"].ToString());

            TaskDetailsByWeekPeriodViewModal taskDetail = new TaskDetailsByWeekPeriodViewModal();
            taskDetail.EmployerID = loggedInuser.EmployerID;
            taskDetail.ReviewerID = loggedInuser.Id;
            taskDetail.WeekPeriod = CurrentWeekPeriod;


            foreach (string taskId in TaskIds)
            {
                var IsChecked = Convert.ToString(form["SelectTask_" + taskId].ToString());
                if (IsChecked != "1")
                {
                    continue;
                }
                taskDetail.EmployeeID = Convert.ToInt16(form["EmployeeID_" + taskId] != null ? form["EmployeeID_" + taskId].ToString() : "0");
                taskDetail.TaskID = Convert.ToInt16(form["TaskID_" + taskId] != null ? form["TaskID_" + taskId].ToString() : "0");
                taskDetail.TaskSubmissionStatus = Convert.ToString(form["TaskSubmissionStatus_" + taskId].ToString());
                taskDetail.ReviewDate = Convert.ToDateTime(form["ReviewDate_" + taskId] != "null" ? form["ReviewDate_" + taskId].ToString() : "1970/1/1");
                taskDetail.ReviewerComments = Convert.ToString(form["ReviewerComments_" + taskId].ToString());
                taskDetail.EmployeeName = Convert.ToString(form["EmployeeName_" + taskId].ToString());
                taskDetail.ReviewerName = Convert.ToString(form["ReviewerName_" + taskId].ToString());

                taskDetail.EmployerName = Convert.ToString(form["EmployerName_" + taskId].ToString());
                taskDetail.EndClientBusinessName = Convert.ToString(form["EndClientBusinessName_" + taskId].ToString());

                var result = _taskRepo.UpdateTaskReview(taskDetail);
                var employee = _userRepo.GetEmployeeByID(taskDetail.EmployeeID);
                SendTaskReviewedEmail(employee.PrimaryEmail, taskDetail);
                SendTaskReviewedEmailToReviewer(loggedInuser.PrimaryEmail, taskDetail);
            }

            return Json(new { Code = 1, Message = "Task Details are saved successfully." });
        }

        private void SendTaskReviewedEmail(string email, TaskDetailsByWeekPeriodViewModal task)
        {
            string emailBody = "Hi, <br/><br/>This is to confirm that task details review is performed by employer’s designated reviewer for following week period. <br/><br/>"
                               + "End-Client Name: " + task.EndClientBusinessName + "<br/>"
                               + "Beneficiary Name: " + task.EmployeeName + "<br/>"
                               + "Week Period: " + task.WeekPeriod + "<br/>"
                               + "Task Status: " + task.TaskSubmissionStatus + "<br/>"
                               + "Reviewer: " + task.ReviewerName + "<br/>"
                               + "Review Date: " + (task.ReviewDate.HasValue ? task.ReviewDate.Value.ToShortDateString() : "") + "<br/>"
                               + "<br/><br/>If Task status is ‘Task Completed’, no additional action needed. "
                               + "<br/>If Task status is ‘Task Re-assigned’, please take an action as mentioned and resubmit task details with required information. "
                               + "<br/><br/> If you have any additional questions regarding this week task submission and review, please contact to your Employer Administrator or Reviewer."
                               + "<br/><br/> Thank you!!<br/>" + task.EmployerName
                               + "<br/><br/>IMPORTANT NOTICE:  The information contained in this electronic e-mail and any accompanying attachment(s) is intended only for the use of the intended recipient and may be confidential and/or legally protected.  If any reader of this communication is not the intended recipient, unauthorized use, disclosure, or copying is strictly prohibited, and may be unlawful.  If you have received this communication in error, please immediately notify the sender by replying this e-mail or forwarding this email to support@e2ewebportal.com with subject 'OPT-OUT'. Also,delete the original message and all copies from your system.";
            string From = ConfigurationManager.AppSettings["FromEmail"] != null ? ConfigurationManager.AppSettings["FromEmail"].ToString() : "";
            EmailHelper.SendEmail(From, email, "Task Reviewed – Week period : " + task.WeekPeriod, emailBody, null, "", true);
        }


        private void SendTaskReviewedEmailToReviewer(string email, TaskDetailsByWeekPeriodViewModal task)
        {
            string emailBody = "Hi, <br/><br/You have been reviewed task successfully. Below are details for task. <br/><br/>"
                               + "End-Client Name: " + task.EndClientBusinessName + "<br/>"
                               + "Beneficiary Name: " + task.EmployeeName + "<br/>"
                               + "Week Period: " + task.WeekPeriod + "<br/>"
                               + "Task Status: " + task.TaskSubmissionStatus + "<br/>"
                               + "Reviewer: " + task.ReviewerName + "<br/>"
                               + "Review Date: " + (task.ReviewDate.HasValue ? task.ReviewDate.Value.ToShortDateString() : "") + "<br/>"
                               + "<br/><br/>If Task status is ‘Task Completed’, no additional action needed. "
                               + "<br/>If Task status is ‘Task Re-assigned’, please take an action as mentioned and resubmit task details with required information. "
                               + "<br/><br/> If you have any additional questions regarding this week task submission and review, please contact to your Employer Administrator or Reviewer."
                               + "<br/><br/> Thank you!!<br/>" + task.EmployerName
                               + "<br/><br/>IMPORTANT NOTICE:  The information contained in this electronic e-mail and any accompanying attachment(s) is intended only for the use of the intended recipient and may be confidential and/or legally protected.  If any reader of this communication is not the intended recipient, unauthorized use, disclosure, or copying is strictly prohibited, and may be unlawful.  If you have received this communication in error, please immediately notify the sender by replying this e-mail or forwarding this email to support@e2ewebportal.com with subject 'OPT-OUT'. Also,delete the original message and all copies from your system.";
            string From = ConfigurationManager.AppSettings["FromEmail"] != null ? ConfigurationManager.AppSettings["FromEmail"].ToString() : "";
            EmailHelper.SendEmail(From, email, "Task Reviewed – Week period : " + task.WeekPeriod, emailBody, null, "", true);
        }

    }
}