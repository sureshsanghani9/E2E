using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EViewModals.Subscription
{
    public class SubscriptionViewModal
    {
        public string EmployerName { get; set; }
        public Nullable<int> EmployerAccountNumber { get; set; }
        public int TotalLogin { get; set; }
        public int LoginAvailable { get; set; }
        public string SubscriptionPlanName { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public string BusinessActive { get; set; }
        public int SubscriptionID { get; set; }
        public string SubscriptionStatus { get; set; }
        public Nullable<System.DateTime> SubscriptionDate { get; set; }
        public string ServiceDetails { get; set; }
        public string SubscriptionType { get; set; }
        public string SubscriptionPlanCode { get; set; }
        public Nullable<int> LoginUsed { get; set; }
        public string PaypalPaymentConfirmationNum { get; set; }
        public Nullable<System.DateTime> PaypalPaymentDate { get; set; }
        public Nullable<decimal> AmountCharged { get; set; }
        public Nullable<decimal> TotalAmtPaid { get; set; }
        public Nullable<decimal> RegistrationFeeCharged { get; set; }
        public Nullable<decimal> RegistrationFeePaid { get; set; }
        public Nullable<System.DateTime> RegistrationFeePaidDate { get; set; }
        public Nullable<decimal> SubscriptionFeeCharged { get; set; }
        public Nullable<decimal> SubscriptionFeePaid { get; set; }
        public Nullable<System.DateTime> SubscriptionFeePaidDate { get; set; }
        public Nullable<decimal> RecurringAmountCharged { get; set; }
        public Nullable<decimal> RecurringAmountPaid { get; set; }
        public Nullable<System.DateTime> RecurringAmountPaidDate { get; set; }
        public Nullable<decimal> LateFeeCharged { get; set; }
        public Nullable<decimal> LateFeeAmountPaid { get; set; }
        public Nullable<System.DateTime> LateFeePaidDate { get; set; }
        public Nullable<decimal> CancellationFeeCharged { get; set; }
        public Nullable<decimal> CancellationFeePaid { get; set; }
        public Nullable<System.DateTime> CancellationDate { get; set; }
        public Nullable<System.DateTime> PaymentDueDate { get; set; }
        public Nullable<int> EmployerId { get; set; }
        public Nullable<int> AdminUserID { get; set; }
        public string EmployerAdminName { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string BusinessAddress1 { get; set; }
        public string BusinessAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string zip { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string URL { get; set; }
        public Nullable<int> TotalEmployees { get; set; }
        public string BusinessTaxID { get; set; }
        public string Active { get; set; }
    }
}
