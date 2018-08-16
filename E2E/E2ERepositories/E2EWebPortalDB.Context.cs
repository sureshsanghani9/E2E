﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class E2EWebPortalEntities : DbContext
    {
        public E2EWebPortalEntities()
            : base("name=E2EWebPortalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<E2E_UserRole> E2E_UserRole { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EndClient> EndClients { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<Reviewer> Reviewers { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<WebAppOwner> WebAppOwners { get; set; }
        public virtual DbSet<database_firewall_rules> database_firewall_rules { get; set; }
        public virtual DbSet<UserMI> UserMIS { get; set; }
        public virtual DbSet<EmployerAdmin> EmployerAdmins { get; set; }
    
        public virtual int sp_AddErrorLog(string userName, string errorMessage)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var errorMessageParameter = errorMessage != null ?
                new ObjectParameter("ErrorMessage", errorMessage) :
                new ObjectParameter("ErrorMessage", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AddErrorLog", userNameParameter, errorMessageParameter);
        }
    
        public virtual ObjectResult<sp_E2EWebPortalLogin_Result> sp_E2EWebPortalLogin(string userName, string password, ObjectParameter op_RoleID)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_E2EWebPortalLogin_Result>("sp_E2EWebPortalLogin", userNameParameter, passwordParameter, op_RoleID);
        }
    
        public virtual ObjectResult<sp_Login_Result> sp_Login(string userName, string password)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Login_Result>("sp_Login", userNameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<Nullable<System.DateTime>> test()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<System.DateTime>>("test");
        }
    
        public virtual ObjectResult<sp_GetSubscriptionInfo_Result> sp_GetSubscriptionInfo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetSubscriptionInfo_Result>("sp_GetSubscriptionInfo");
        }
    
        public virtual int sp_ManageBusinessActivation(string isActive, Nullable<int> employerID)
        {
            var isActiveParameter = isActive != null ?
                new ObjectParameter("IsActive", isActive) :
                new ObjectParameter("IsActive", typeof(string));
    
            var employerIDParameter = employerID.HasValue ?
                new ObjectParameter("EmployerID", employerID) :
                new ObjectParameter("EmployerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ManageBusinessActivation", isActiveParameter, employerIDParameter);
        }
    
        public virtual ObjectResult<sp_InsertNewBusiness_Result> sp_InsertNewBusiness(string employerName, string businessName, string businessAddress1, string businessAddress2, string city, string state, string zip, string phone, string fax, string primaryEmail, string secondaryEmail, string uRL, Nullable<int> totalEmployees, string businessTaxID, string active, string userName, Nullable<System.DateTime> subscriptionDate, string serviceDetails, string subscriptionType, string subscriptionPlanName, string subscriptionPlanCode, Nullable<int> totalLogin, Nullable<System.DateTime> effectiveDate, Nullable<System.DateTime> expirationDate, Nullable<decimal> amountCharged, Nullable<decimal> registrationFeeCharged, Nullable<decimal> subscriptionFeeCharged, Nullable<System.DateTime> paymentDueDate)
        {
            var employerNameParameter = employerName != null ?
                new ObjectParameter("EmployerName", employerName) :
                new ObjectParameter("EmployerName", typeof(string));
    
            var businessNameParameter = businessName != null ?
                new ObjectParameter("BusinessName", businessName) :
                new ObjectParameter("BusinessName", typeof(string));
    
            var businessAddress1Parameter = businessAddress1 != null ?
                new ObjectParameter("BusinessAddress1", businessAddress1) :
                new ObjectParameter("BusinessAddress1", typeof(string));
    
            var businessAddress2Parameter = businessAddress2 != null ?
                new ObjectParameter("BusinessAddress2", businessAddress2) :
                new ObjectParameter("BusinessAddress2", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("City", city) :
                new ObjectParameter("City", typeof(string));
    
            var stateParameter = state != null ?
                new ObjectParameter("State", state) :
                new ObjectParameter("State", typeof(string));
    
            var zipParameter = zip != null ?
                new ObjectParameter("zip", zip) :
                new ObjectParameter("zip", typeof(string));
    
            var phoneParameter = phone != null ?
                new ObjectParameter("Phone", phone) :
                new ObjectParameter("Phone", typeof(string));
    
            var faxParameter = fax != null ?
                new ObjectParameter("Fax", fax) :
                new ObjectParameter("Fax", typeof(string));
    
            var primaryEmailParameter = primaryEmail != null ?
                new ObjectParameter("PrimaryEmail", primaryEmail) :
                new ObjectParameter("PrimaryEmail", typeof(string));
    
            var secondaryEmailParameter = secondaryEmail != null ?
                new ObjectParameter("SecondaryEmail", secondaryEmail) :
                new ObjectParameter("SecondaryEmail", typeof(string));
    
            var uRLParameter = uRL != null ?
                new ObjectParameter("URL", uRL) :
                new ObjectParameter("URL", typeof(string));
    
            var totalEmployeesParameter = totalEmployees.HasValue ?
                new ObjectParameter("TotalEmployees", totalEmployees) :
                new ObjectParameter("TotalEmployees", typeof(int));
    
            var businessTaxIDParameter = businessTaxID != null ?
                new ObjectParameter("BusinessTaxID", businessTaxID) :
                new ObjectParameter("BusinessTaxID", typeof(string));
    
            var activeParameter = active != null ?
                new ObjectParameter("Active", active) :
                new ObjectParameter("Active", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var subscriptionDateParameter = subscriptionDate.HasValue ?
                new ObjectParameter("SubscriptionDate", subscriptionDate) :
                new ObjectParameter("SubscriptionDate", typeof(System.DateTime));
    
            var serviceDetailsParameter = serviceDetails != null ?
                new ObjectParameter("ServiceDetails", serviceDetails) :
                new ObjectParameter("ServiceDetails", typeof(string));
    
            var subscriptionTypeParameter = subscriptionType != null ?
                new ObjectParameter("SubscriptionType", subscriptionType) :
                new ObjectParameter("SubscriptionType", typeof(string));
    
            var subscriptionPlanNameParameter = subscriptionPlanName != null ?
                new ObjectParameter("SubscriptionPlanName", subscriptionPlanName) :
                new ObjectParameter("SubscriptionPlanName", typeof(string));
    
            var subscriptionPlanCodeParameter = subscriptionPlanCode != null ?
                new ObjectParameter("SubscriptionPlanCode", subscriptionPlanCode) :
                new ObjectParameter("SubscriptionPlanCode", typeof(string));
    
            var totalLoginParameter = totalLogin.HasValue ?
                new ObjectParameter("TotalLogin", totalLogin) :
                new ObjectParameter("TotalLogin", typeof(int));
    
            var effectiveDateParameter = effectiveDate.HasValue ?
                new ObjectParameter("EffectiveDate", effectiveDate) :
                new ObjectParameter("EffectiveDate", typeof(System.DateTime));
    
            var expirationDateParameter = expirationDate.HasValue ?
                new ObjectParameter("ExpirationDate", expirationDate) :
                new ObjectParameter("ExpirationDate", typeof(System.DateTime));
    
            var amountChargedParameter = amountCharged.HasValue ?
                new ObjectParameter("AmountCharged", amountCharged) :
                new ObjectParameter("AmountCharged", typeof(decimal));
    
            var registrationFeeChargedParameter = registrationFeeCharged.HasValue ?
                new ObjectParameter("RegistrationFeeCharged", registrationFeeCharged) :
                new ObjectParameter("RegistrationFeeCharged", typeof(decimal));
    
            var subscriptionFeeChargedParameter = subscriptionFeeCharged.HasValue ?
                new ObjectParameter("SubscriptionFeeCharged", subscriptionFeeCharged) :
                new ObjectParameter("SubscriptionFeeCharged", typeof(decimal));
    
            var paymentDueDateParameter = paymentDueDate.HasValue ?
                new ObjectParameter("PaymentDueDate", paymentDueDate) :
                new ObjectParameter("PaymentDueDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_InsertNewBusiness_Result>("sp_InsertNewBusiness", employerNameParameter, businessNameParameter, businessAddress1Parameter, businessAddress2Parameter, cityParameter, stateParameter, zipParameter, phoneParameter, faxParameter, primaryEmailParameter, secondaryEmailParameter, uRLParameter, totalEmployeesParameter, businessTaxIDParameter, activeParameter, userNameParameter, subscriptionDateParameter, serviceDetailsParameter, subscriptionTypeParameter, subscriptionPlanNameParameter, subscriptionPlanCodeParameter, totalLoginParameter, effectiveDateParameter, expirationDateParameter, amountChargedParameter, registrationFeeChargedParameter, subscriptionFeeChargedParameter, paymentDueDateParameter);
        }
    
        public virtual int sp_InsertEmpAdminUser(string userName, string password, Nullable<int> employerID, Nullable<int> roleID, Nullable<int> active, string adminUserFirstName, string adminuserMiddleName, string adminUserLastName, string adminUserNickName, string adminTitle, string address1, string address2, string city, string state, string zip, string workPhoneNumber, string extn, string cellPhoneNumber, string primaryEmail, string secondaryEmail, Nullable<bool> isPrimary)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var employerIDParameter = employerID.HasValue ?
                new ObjectParameter("EmployerID", employerID) :
                new ObjectParameter("EmployerID", typeof(int));
    
            var roleIDParameter = roleID.HasValue ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(int));
    
            var activeParameter = active.HasValue ?
                new ObjectParameter("Active", active) :
                new ObjectParameter("Active", typeof(int));
    
            var adminUserFirstNameParameter = adminUserFirstName != null ?
                new ObjectParameter("AdminUserFirstName", adminUserFirstName) :
                new ObjectParameter("AdminUserFirstName", typeof(string));
    
            var adminuserMiddleNameParameter = adminuserMiddleName != null ?
                new ObjectParameter("AdminuserMiddleName", adminuserMiddleName) :
                new ObjectParameter("AdminuserMiddleName", typeof(string));
    
            var adminUserLastNameParameter = adminUserLastName != null ?
                new ObjectParameter("AdminUserLastName", adminUserLastName) :
                new ObjectParameter("AdminUserLastName", typeof(string));
    
            var adminUserNickNameParameter = adminUserNickName != null ?
                new ObjectParameter("AdminUserNickName", adminUserNickName) :
                new ObjectParameter("AdminUserNickName", typeof(string));
    
            var adminTitleParameter = adminTitle != null ?
                new ObjectParameter("AdminTitle", adminTitle) :
                new ObjectParameter("AdminTitle", typeof(string));
    
            var address1Parameter = address1 != null ?
                new ObjectParameter("Address1", address1) :
                new ObjectParameter("Address1", typeof(string));
    
            var address2Parameter = address2 != null ?
                new ObjectParameter("Address2", address2) :
                new ObjectParameter("Address2", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("City", city) :
                new ObjectParameter("City", typeof(string));
    
            var stateParameter = state != null ?
                new ObjectParameter("State", state) :
                new ObjectParameter("State", typeof(string));
    
            var zipParameter = zip != null ?
                new ObjectParameter("zip", zip) :
                new ObjectParameter("zip", typeof(string));
    
            var workPhoneNumberParameter = workPhoneNumber != null ?
                new ObjectParameter("WorkPhoneNumber", workPhoneNumber) :
                new ObjectParameter("WorkPhoneNumber", typeof(string));
    
            var extnParameter = extn != null ?
                new ObjectParameter("Extn", extn) :
                new ObjectParameter("Extn", typeof(string));
    
            var cellPhoneNumberParameter = cellPhoneNumber != null ?
                new ObjectParameter("CellPhoneNumber", cellPhoneNumber) :
                new ObjectParameter("CellPhoneNumber", typeof(string));
    
            var primaryEmailParameter = primaryEmail != null ?
                new ObjectParameter("PrimaryEmail", primaryEmail) :
                new ObjectParameter("PrimaryEmail", typeof(string));
    
            var secondaryEmailParameter = secondaryEmail != null ?
                new ObjectParameter("SecondaryEmail", secondaryEmail) :
                new ObjectParameter("SecondaryEmail", typeof(string));
    
            var isPrimaryParameter = isPrimary.HasValue ?
                new ObjectParameter("IsPrimary", isPrimary) :
                new ObjectParameter("IsPrimary", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertEmpAdminUser", userNameParameter, passwordParameter, employerIDParameter, roleIDParameter, activeParameter, adminUserFirstNameParameter, adminuserMiddleNameParameter, adminUserLastNameParameter, adminUserNickNameParameter, adminTitleParameter, address1Parameter, address2Parameter, cityParameter, stateParameter, zipParameter, workPhoneNumberParameter, extnParameter, cellPhoneNumberParameter, primaryEmailParameter, secondaryEmailParameter, isPrimaryParameter);
        }
    
        public virtual ObjectResult<sp_GetBusinessList_Result> sp_GetBusinessList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetBusinessList_Result>("sp_GetBusinessList");
        }
    }
}
