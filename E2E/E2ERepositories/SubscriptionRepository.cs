using AutoMapper;
using E2ERepositories.Interface;
using E2EViewModals.Subscription;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
            //string constr = ConfigurationManager.ConnectionStrings["E2EWebPortalEntities"] != null ? ConfigurationManager.ConnectionStrings["E2EWebPortalEntities"].ToString() : "";
            //string constr = "data source=kumudini.database.windows.net;initial catalog=E2EWebPortal;persist security info=True;user id=SRV_E2EWebPortal;password=Admin@e2e;MultipleActiveResultSets=True;";
            using (var db = new E2EWebPortalEntities())
            //using (SqlConnection con = new SqlConnection(constr))
            {
                
                
                //con.Open();
                //SqlCommand command = new SqlCommand("sp_InsertNewBusiness", con);
                //command.CommandType = CommandType.StoredProcedure;
                //
                //command.Parameters.Add(new SqlParameter("@EmployerName", EmployerName));
                //command.Parameters.Add(new SqlParameter("@BusinessName", BusinessName));
                //command.Parameters.Add(new SqlParameter("@BusinessAddress1", BusinessAddress1));
                //command.Parameters.Add(new SqlParameter("@BusinessAddress2", BusinessAddress2));
                //command.Parameters.Add(new SqlParameter("@City", City));
                //command.Parameters.Add(new SqlParameter("@State", State));
                //command.Parameters.Add(new SqlParameter("@zip", zip));
                //command.Parameters.Add(new SqlParameter("@Phone", Phone));
                //command.Parameters.Add(new SqlParameter("@Fax", Fax));
                //command.Parameters.Add(new SqlParameter("@PrimaryEmail", PrimaryEmail));
                //command.Parameters.Add(new SqlParameter("@SecondaryEmail", SecondaryEmail));
                //command.Parameters.Add(new SqlParameter("@URL", URL));
                //command.Parameters.Add(new SqlParameter("@TotalEmployees", TotalEmployees));
                //command.Parameters.Add(new SqlParameter("@BusinessTaxID", BusinessTaxID));
                //command.Parameters.Add(new SqlParameter("@Active", Active));
                //command.Parameters.Add(new SqlParameter("@UserName", UserName));
                //command.Parameters.Add(new SqlParameter("@SubscriptionDate", SubscriptionDate));
                //command.Parameters.Add(new SqlParameter("@ServiceDetails", ServiceDetails));
                //command.Parameters.Add(new SqlParameter("@SubscriptionType", SubscriptionType));
                //command.Parameters.Add(new SqlParameter("@SubscriptionPlanName", SubscriptionPlanName));
                //command.Parameters.Add(new SqlParameter("@SubscriptionPlanCode", SubscriptionPlanCode));
                //command.Parameters.Add(new SqlParameter("@TotalLogin", TotalLogin));
                //command.Parameters.Add(new SqlParameter("@EffectiveDate", EffectiveDate));
                //command.Parameters.Add(new SqlParameter("@ExpirationDate", ExpirationDate));
                //command.Parameters.Add(new SqlParameter("@AmountCharged", AmountCharged));
                //command.Parameters.Add(new SqlParameter("@RegistrationFeeCharged", RegistrationFeeCharged));
                //command.Parameters.Add(new SqlParameter("@SubscriptionFeeCharged", SubscriptionFeeCharged));
                //command.Parameters.Add(new SqlParameter("@PaymentDueDate", PaymentDueDate));
                //
                //var result = command.ExecuteNonQuery();
                //con.Close();
                //
                //return result;

                //return db.Database.ExecuteSqlCommand("[dbo].[sp_InsertNewBusiness] @EmployerName,@BusinessName,@BusinessAddress1"
                //        +",@BusinessAddress2,@City,@State,@zip,@Phone,@Fax,@PrimaryEmail,@SecondaryEmail,@URL,@TotalEmployees,@BusinessTaxID,@Active,@UserName"
                //        +",@SubscriptionDate, @ServiceDetails, @SubscriptionType, @SubscriptionPlanName, @SubscriptionPlanCode, @TotalLogin, @EffectiveDate, @ExpirationDate"
                //        +",@AmountCharged, @RegistrationFeeCharged, @SubscriptionFeeCharged, @PaymentDueDate"
                //    , new SqlParameter("@EmployerName", EmployerName)
                //    , new SqlParameter("@BusinessName", BusinessName)
                //    , new SqlParameter("@BusinessAddress1", BusinessAddress1)
                //    , new SqlParameter("@BusinessAddress2", BusinessAddress2)
                //    , new SqlParameter("@City", City)
                //    , new SqlParameter("@State", State)
                //    , new SqlParameter("@zip", zip)
                //    , new SqlParameter("@Phone", Phone)
                //    , new SqlParameter("@Fax", Fax)
                //    , new SqlParameter("@PrimaryEmail", PrimaryEmail)
                //    , new SqlParameter("@SecondaryEmail", SecondaryEmail)
                //    , new SqlParameter("@URL", URL)
                //    , new SqlParameter("@TotalEmployees", TotalEmployees)
                //    , new SqlParameter("@BusinessTaxID", BusinessTaxID)
                //    , new SqlParameter("@Active", Active)
                //    , new SqlParameter("@UserName", UserName)
                //    , new SqlParameter("@SubscriptionDate", SubscriptionDate)
                //    , new SqlParameter("@ServiceDetails", ServiceDetails)
                //    , new SqlParameter("@SubscriptionType", SubscriptionType)
                //    , new SqlParameter("@SubscriptionPlanName", SubscriptionPlanName)
                //    , new SqlParameter("@SubscriptionPlanCode", SubscriptionPlanCode)
                //    , new SqlParameter("@TotalLogin", TotalLogin)
                //    , new SqlParameter("@EffectiveDate", EffectiveDate)
                //    , new SqlParameter("@ExpirationDate", ExpirationDate)
                //    , new SqlParameter("@AmountCharged", AmountCharged)
                //    , new SqlParameter("@RegistrationFeeCharged", RegistrationFeeCharged)
                //    , new SqlParameter("@SubscriptionFeeCharged", SubscriptionFeeCharged)
                //    , new SqlParameter("@PaymentDueDate", PaymentDueDate)
                //    );

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
