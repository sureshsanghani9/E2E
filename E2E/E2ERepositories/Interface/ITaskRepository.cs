using E2EViewModals.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ERepositories.Interface
{
    public interface ITaskRepository
    {
        int UpsertComment(TaskReviewCommentViewModal comment);
    }
}
