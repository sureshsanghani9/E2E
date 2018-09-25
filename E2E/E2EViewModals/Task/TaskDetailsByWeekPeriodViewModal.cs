using System;

namespace E2EViewModals.Task
{
    public class TaskDetailsByWeekPeriodViewModal
    {
        public int TaskID { get; set; }
        public System.DateTime WeekStartDate { get; set; }
        public System.DateTime WeekEndDate { get; set; }
        public string WeekPeriod { get; set; }
        public string EmployerName { get; set; }
        public string EmployeeName { get; set; }
        public string EndClientBusinessName { get; set; }
        public string EndClientWorkLocation { get; set; }
        public System.DateTime TaskCreationDate { get; set; }
        public Nullable<decimal> HoursBilled { get; set; }
        public string TaskDetails { get; set; }
        public string AnyIssues { get; set; }
        public string Solution { get; set; }
        public string PercentCompleted { get; set; }
        public Nullable<System.DateTime> SubmissionDate { get; set; }
        public string TaskContinueFromLastWeekPeriod { get; set; }
        public string TaskContinueToNextWeekPeriod { get; set; }
        public string TaskSubmissionStatus { get; set; }
        public Nullable<System.DateTime> ReviewDate { get; set; }
        public string ReviewerComments { get; set; }
        public string ReviewerName { get; set; }
        public int EmployeeID { get; set; }
        public Nullable<int> ReviewerID { get; set; }
        public int EmployerID { get; set; }
        public int EndClientID { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
