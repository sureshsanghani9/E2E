using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EViewModals.Task
{
    public class TaskReviewCommentViewModal
    {
        public int CommentID { get; set; }
        public string CommendDescription { get; set; }
        public int ReviewerID { get; set; }
        public int EmployerID { get; set; }
        public string IsDefault { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
