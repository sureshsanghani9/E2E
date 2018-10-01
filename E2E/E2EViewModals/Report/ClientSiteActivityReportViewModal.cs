using System;
using System.Collections.Generic;
using System.Linq;

namespace E2EViewModals.Report
{
    public class ClientSiteActivityReportViewModal
    {
        public string BeneficiaryName { get; set; }
        public string ReportCreatedBy { get; set; }
        public string EndClientBusinessName { get; set; }
        public string EndClientAddress1 { get; set; }
        public string EndClientAddress2 { get; set; }
        public string EndClientCity { get; set; }
        public string EndClientState { get; set; }
        public string EndClientzip { get; set; }
        public string EndClientPhoneNumber { get; set; }
        public string EndClientExtn { get; set; }
        public string EmployeeEmailAtEndClient { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress1 { get; set; }
        public string BusinessAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string zip { get; set; }
        public string Phone { get; set; }
        public string PrimaryEmail { get; set; }
        public string WeekPeriod { get; set; }
        public Nullable<decimal> HoursBilled { get; set; }
        public string TaskDetails { get; set; }
        public string AnyIssues { get; set; }
        public string Solution { get; set; }
        public Nullable<System.DateTime> SubmissionDate { get; set; }
        public string ReviewerName { get; set; }
        public Nullable<System.DateTime> ReviewDate { get; set; }
        public string TaskSubmissionStatus { get; set; }
        public string TaskContinueFromLastWeekPeriod { get; set; }
        public string PercentCompleted { get; set; }
        public string TaskContinueToNextWeekPeriod { get; set; }
        public string ReviewerComments { get; set; }
    }
}
