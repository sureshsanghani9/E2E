﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EViewModals.Business
{
    public partial class BusinessViewModal
    {
        public int EmployerID { get; set; }
        public string EmployerName { get; set; }
        public string BusinessName { get; set; }
        public string Active { get; set; }
        public string Phone { get; set; }
        public string PrimaryEmail { get; set; }
        public string BusinessAddress1 { get; set; }
        public string BusinessAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string zip { get; set; }
        public Nullable<int> EmployerAccountNumber { get; set; }
        public string SecondaryEmail { get; set; }
        public string URL { get; set; }
        public Nullable<int> TotalEmployees { get; set; }
        public string BusinessTaxID { get; set; }
        public Nullable<int> AdminUserID { get; set; }
        public string AdminUserFirstName { get; set; }
        public string AdminUserLastName { get; set; }
        public string AdminTitle { get; set; }
        public Nullable<bool> IsPrimary { get; set; }
        public string UserName { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<int> UserAccountID { get; set; }
    }
}
