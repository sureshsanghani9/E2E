using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EViewModals.Invitations
{
    public class InvitationsViewModal
    {
        public List<Invite> Invites { get; set; }
    }

    public class Invite
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public string AdditionalNotes { get; set; }
        public int UserID { get; set; }
    }
}
