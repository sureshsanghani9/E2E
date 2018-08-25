using System;

namespace E2EViewModals.User
{
    public class EmployerAdminViewModal
    {
        public int AdminUserID{get; set;}
        public string UserName{get; set;}
        public string Password{get; set;}
        public int EmployerID{get; set;}
        public int RoleID{get; set;}
        public int Active{get; set;}
        public string AdminUserFirstName{get; set;}
        public string AdminuserMiddleName{get; set;}
        public string AdminUserLastName {get; set;}
        public string AdminUserNickName {get; set;}
        public string AdminTitle {get; set;}
        public string Address1 {get; set;}
        public string Address2 {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string zip {get; set;}
        public string WorkPhoneNumber {get; set;}
        public string Extn {get; set;}
        public string CellPhoneNumber {get; set;}
        public string PrimaryEmail {get; set;}
        public string SecondaryEmail {get; set;}
        public bool IsPrimary {get; set;}
    }
}
