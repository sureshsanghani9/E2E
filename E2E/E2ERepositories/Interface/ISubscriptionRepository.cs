using E2EViewModals.Subscription;
using System;
using System.Collections.Generic;

namespace E2ERepositories.Interface
{
    public interface ISubscriptionRepository
    {
        List<SubscriptionInfoViewModal> GetSubscriptionInfo();

        int InsertNewBusiness(string EmployerName
                                    , string BusinessName
                                    , string BusinessAddress1
                                    , string BusinessAddress2
                                    , string City
                                    , string State
                                    , string zip
                                    , string Phone
                                    , string Fax
                                    , string PrimaryEmail
                                    , string SecondaryEmail
                                    , string URL
                                    , int TotalEmployees
                                    , string BusinessTaxID
                                    , string Active
                                    , string UserName
                                    , DateTime SubscriptionDate
                                    , string ServiceDetails
                                    , string SubscriptionType
                                    , string SubscriptionPlanName
                                    , string SubscriptionPlanCode
                                    , int TotalLogin
                                    , DateTime EffectiveDate
                                    , DateTime ExpirationDate
                                    , decimal AmountCharged
                                    , decimal RegistrationFeeCharged
                                    , decimal SubscriptionFeeCharged
                                    , DateTime PaymentDueDate);
    }
}
