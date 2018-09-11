using E2ERepositories.Interface;
using E2EViewModals.Task;
using System;


namespace E2ERepositories
{
    public class TaskRepository : ITaskRepository
    {
        public int UpsertComment(TaskReviewCommentViewModal comment)
        {
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_UpsertComments(  comment.CommentID
                                            , comment.ReviewerID
                                            , comment.EmployerID
                                            , comment.CommendDescription);
            }
        }
    }
}
