using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EViewModals.User
{
    public class ReviewerViewModal
    {
        public int ReviewerID {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}
        public int EmployerID {get; set;}
        public int RoleID {get; set;}
        public int Active {get; set;}
        public string ReviewerFirstName {get; set;}
        public string ReviewerMiddleName{get; set;}
        public string ReviewerLastName {get; set;}
        public string ReviewerNickName {get; set;}
        public string ReviewerTitle {get; set;}
        public DateTime DateOfBirth {get; set;}
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
    }
}
