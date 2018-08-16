//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E2ERepositories
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subscription
    {
        public int SubscriptionID { get; set; }
        public string SubscriptionStatus { get; set; }
        public Nullable<System.DateTime> SubscriptionDate { get; set; }
        public string ServiceDetails { get; set; }
        public string SubscriptionPlanName { get; set; }
        public string SubscriptionPlanCode { get; set; }
        public Nullable<int> TotalLogin { get; set; }
        public Nullable<int> LoginUsed { get; set; }
        public Nullable<int> LoginAvailable { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public string PaypalPaymentConfirmationNum { get; set; }
        public Nullable<System.DateTime> PaypalPaymentDate { get; set; }
        public Nullable<decimal> AmountCharged { get; set; }
        public Nullable<decimal> TotalAmtPaid { get; set; }
        public Nullable<decimal> RecurringAmountPaid { get; set; }
        public Nullable<System.DateTime> RecurringAmountPaidDate { get; set; }
        public Nullable<decimal> RegistrationFeeCharged { get; set; }
        public Nullable<decimal> RegistrationFeePaid { get; set; }
        public Nullable<System.DateTime> RegistrationFeePaidDate { get; set; }
        public Nullable<decimal> LateFeeCharged { get; set; }
        public Nullable<decimal> LateFeeAmountPaid { get; set; }
        public Nullable<System.DateTime> LateFeePaidDate { get; set; }
        public Nullable<decimal> CancellationFeeCharged { get; set; }
        public Nullable<System.DateTime> CancellationDate { get; set; }
        public Nullable<System.DateTime> PaymentDueDate { get; set; }
        public Nullable<int> EmployerId { get; set; }
        public string BusinessName { get; set; }
        public Nullable<int> AdminUserID { get; set; }
        public string EmployerAdminName { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string SubscriptionType { get; set; }
        public Nullable<decimal> SubscriptionFeeCharged { get; set; }
        public Nullable<decimal> SubscriptionFeePaid { get; set; }
        public Nullable<System.DateTime> SubscriptionFeePaidDate { get; set; }
        public Nullable<decimal> RecurringAmountCharged { get; set; }
        public Nullable<decimal> CancellationFeePaid { get; set; }
    
        public virtual Business Business { get; set; }
    }
}
