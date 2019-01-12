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
        List<TaskDetailsByWeekPeriodViewModal> GetTaskDetailsByWeekPeriod(int roleID, int employerID, int reviewerID, int employeeID, string weekPeriod);
        int AddUpdateTaskDetails(TaskDetailsByWeekPeriodViewModal taskDetail);

        List<EndClientInfoViewModal> GetEndClientInfo(int employerID, int employeeID, int endClientID = -1);
        List<TaskReviewCommentViewModal> GetAllReviewComments(int employerID, int commentID = -1);

        int ActiveDeactiveEndClient(int EndClientID, int EmployerID, string Active);

        int DeleteEndClient(int EndClientID, int EmployerID);

        int MakeDefaultTaskReviewComment(int CommentID, int EmployerID, string isDefault);

        int DeleteReviewComments(int CommentID, int EmployerID);

        int UpdateTaskReview(TaskDetailsByWeekPeriodViewModal taskDetails);

        TaskSubStatusSummaryViewModal GetTaskSubStatusSummary(int employerID);

        List<PendSubmissionEEViewModal> GetListPendSubmissionEE(int employerID, string PendPeriod);

        List<PendReviewViewModal> GetListPendReview(int employerID, string PendPeriod);

        int ResetCompletedTask(int roleID, int employerID, int adminUserID, int reviewerID, int employeeID, int taskID);

        List<TaskCompletedViewModal> GetTaskCompleted(int employerID, string weekPeriod);

        List<string> GetWeekTaskCompleted(int employerID);
    }
}
