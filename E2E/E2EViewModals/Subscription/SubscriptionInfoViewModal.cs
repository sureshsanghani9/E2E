using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EViewModals.Subscription
{
    public class SubscriptionInfoViewModal
    {
        public Nullable<System.DateTime> PaymentDueDate { get; set; }
        public string EmployerName { get; set; }
        public Nullable<int> EmployerAccountNumber { get; set; }
        public string SubscriptionPlanCode { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public Nullable<int> TotalLogin { get; set; }
        public Nullable<int> TotalLoginUsed { get; set; }
        public Nullable<int> TotalLoginAvailable { get; set; }
        public Nullable<int> TotalEmployeeLogin { get; set; }
        public Nullable<int> TotalReviewerLogin { get; set; }
        public Nullable<int> TotalEmployerAdmin { get; set; }
        public int EmployerID { get; set; }
    }
}
