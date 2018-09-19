using E2EViewModals.EndClient;
using E2EViewModals.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ERepositories.Interface
{
    public interface ITaskRepository
    {
        int UpsertComment(TaskReviewCommentViewModal comment);
        List<WeekPeriodViewModal> GetListWeekPeriod(int employerID, int userID, int roleID, out string weekperiod);
        List<TaskDetailsByWeekPeriodViewModal> GetTaskDetailsByWeekPeriod(int roleID, int employerID, int reviewerID, int employeeID, string weekPeriod, int taskID = -1);
        int AddUpdateTaskDetails(TaskDetailsByWeekPeriodViewModal taskDetail);

        List<EndClientInfoViewModal> GetEndClientInfo(int employerID, int employeeID, int endClientID = -1);
        List<TaskReviewCommentViewModal> GetAllReviewComments(int employerID, int commentID = -1);

    }
}
