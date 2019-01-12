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
    public class TaskController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly ITaskRepository _taskRepo;

        public TaskController(IUserRepository userRepo, ITaskRepository taskRepo)
        {
            _userRepo = userRepo;
            _taskRepo = taskRepo;
        }
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ResetTask()
        {
            var loggedInuser = (UserViewModal)Session["User"];
            var tasks = new List<TaskCompletedViewModal>();

            string weekPeriod = string.Empty;
            weekPeriod = TempData["CurrentWeekPeriod"] != null ? TempData["CurrentWeekPeriod"].ToString() : weekPeriod;
            var weekPeriods = _taskRepo.GetWeekTaskCompleted(loggedInuser.EmployerID).ToList();
            
            if (string.IsNullOrEmpty(weekPeriod) && weekPeriods.Any())
            {
                weekPeriod = weekPeriods.FirstOrDefault();
            }

            tasks = _taskRepo.GetTaskCompleted(loggedInuser.EmployerID, weekPeriod).ToList();

            ViewBag.CurrentWeekPeriod = weekPeriod;
            ViewBag.WeekPeriods = weekPeriods;

            return View(tasks);
            
        }

        [HttpPost]
        public JsonResult SetCurrentWeekPeriod(string weekPeriod)
        {
            TempData["CurrentWeekPeriod"] = weekPeriod;
            return Json(new { Code = 1, Message = "Current Week Period has been set successfully." });
        }

        [HttpPost]
        public JsonResult ResetCompletedTask(int taskID)
        {
            var loggedInuser = (UserViewModal)Session["User"];
            int result = _taskRepo.ResetCompletedTask(loggedInuser.RoleID, loggedInuser.EmployerID, loggedInuser.Id, loggedInuser.Id, 0, taskID);

            if (result == -1)
            {
                return Json(new { Code = 1, Message = "Task has been reset successfully!" });
            }
            else
            {
                return Json(new { Code = 0, Message = "Something wrong occured! Please try again!" });
            }
        }
    }
}