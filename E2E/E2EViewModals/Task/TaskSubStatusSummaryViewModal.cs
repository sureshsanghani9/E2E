using System;


namespace E2EViewModals.Task
{
    public class TaskSubStatusSummaryViewModal
    {
        public Nullable<int> EETaskPend7days { get; set; }
        public string LastWeekPeriod { get; set; }
        public Nullable<int> EETaskPendAll { get; set; }
        public string WeekPeriodAll { get; set; }
        public Nullable<int> RewPend7days { get; set; }
        public string RewLastWeekPeriod { get; set; }
        public Nullable<int> RewPendAll { get; set; }
        public string RewWeekPeriodAll { get; set; }
    }
}
