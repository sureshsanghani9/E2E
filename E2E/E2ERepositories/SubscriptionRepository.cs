using AutoMapper;
using E2ERepositories.Interface;
using E2EViewModals.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ERepositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        public List<SubscriptionInfoViewModal> GetSubscriptionInfo()
        {
            using (var db = new E2EWebPortalEntities())
            {
                var subscriptionInfo = db.sp_GetSubscriptionInfo().ToList();
                return Mapper.Map<List<sp_GetSubscriptionInfo_Result>, List<SubscriptionInfoViewModal>>(subscriptionInfo);
            }
        }

        public int InsertNewBusiness(string EmployerName
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
                                    , DateTime PaymentDueDate)
        {

            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_InsertNewBusiness(EmployerName
                                    , BusinessName
                                    , BusinessAddress1
                                    , BusinessAddress2
                                    , City
                                    , State
                                    , zip
                                    , Phone
                                    , Fax
                                    , PrimaryEmail
                                    , SecondaryEmail
                                    , URL
                                    , TotalEmployees
                                    , BusinessTaxID
                                    , Active
                                    , UserName
                                    , SubscriptionDate
                                    , ServiceDetails
                                    , SubscriptionType
                                    , SubscriptionPlanName
                                    , SubscriptionPlanCode
                                    , TotalLogin
                                    , EffectiveDate
                                    , ExpirationDate
                                    , AmountCharged
                                    , RegistrationFeeCharged
                                    , SubscriptionFeeCharged
                                    , PaymentDueDate);
            }
        }
    }
}
