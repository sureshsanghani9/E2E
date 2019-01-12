using System;

namespace E2EViewModals.Task
{
    public class TaskCompletedViewModal
    {
        public string BeneficiaryName { get; set; }
        public string EndClientBusinessName { get; set; }
        public string EndClientCity { get; set; }
        public string EndClientState { get; set; }
        public Nullable<System.DateTime> SubmissionDate { get; set; }
        public string TaskDetails { get; set; }
        public string TaskSubmissionStatus { get; set; }
        public Nullable<System.DateTime> ReviewDate { get; set; }
        public string ReviewerComments { get; set; }
        public int TaskID { get; set; }
    }
}
