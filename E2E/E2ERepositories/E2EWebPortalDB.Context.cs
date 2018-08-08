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
        public virtual DbSet<EmployerAdmin> EmployerAdmins { get; set; }
        public virtual DbSet<EndClient> EndClients { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<Reviewer> Reviewers { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<WebAppOwner> WebAppOwners { get; set; }
        public virtual DbSet<database_firewall_rules> database_firewall_rules { get; set; }
        public virtual DbSet<UserMI> UserMIS { get; set; }
    
        public virtual int sp_AddErrorLog(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AddErrorLog", userNameParameter);
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
    }
}
