﻿using AutoMapper;
using E2ERepositories.Interface;
using E2EViewModals.Task;
using System;
using System.Linq;
using System.Collections.Generic;
using E2EViewModals.EndClient;

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

        public List<WeekPeriodViewModal> GetListWeekPeriod(int employerID, int userID, int roleID,  out string weekperiod)
        {
            using (var db = new E2EWebPortalEntities())
            {
                System.Data.Entity.Core.Objects.ObjectParameter output = new System.Data.Entity.Core.Objects.ObjectParameter("Weekperiod", typeof(int));
                var weekPeriods = db.sp_GetListWeekPeriod(employerID, userID, roleID, output).ToList();
                weekperiod = output.Value.ToString();
                return Mapper.Map<List<sp_GetListWeekPeriod_Result>, List<WeekPeriodViewModal>>(weekPeriods);
            }
        }

        public List<TaskDetailsByWeekPeriodViewModal> GetTaskDetailsByWeekPeriod(int roleID, int employerID, int reviewerID, int employeeID, string weekPeriod, int taskID = -1)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var tasks = db.sp_GetTaskDetailsByWeekPeriod(roleID, employerID, reviewerID, employeeID, weekPeriod, taskID).ToList();
                return Mapper.Map<List<sp_GetTaskDetailsByWeekPeriod_Result>, List<TaskDetailsByWeekPeriodViewModal>>(tasks);
            }
        }

        public int AddUpdateTaskDetails(TaskDetailsByWeekPeriodViewModal taskDetails)
        {
            using (var db = new E2EWebPortalEntities())
            {
                return db.sp_AddUpdateTaskDetails(taskDetails.EmployerID
                                                        , taskDetails.EmployeeID
                                                        , taskDetails.WeekPeriod
                                                        , taskDetails.HoursBilled
                                                        , taskDetails.TaskDetails
                                                        , taskDetails.AnyIssues
                                                        , taskDetails.Solution
                                                        , taskDetails.PercentCompleted
                                                        , taskDetails.SubmissionDate
                                                        , taskDetails.TaskContinueFromLastWeekPeriod
                                                        , taskDetails.TaskContinueToNextWeekPeriod);
            }
        }

        public List<EndClientInfoViewModal> GetEndClientInfo(int employerID, int employeeID, int endClientID = -1)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var endClients = db.sp_GetEndClientInfo(employerID, employeeID, endClientID).ToList();
                return Mapper.Map<List<sp_GetEndClientInfo_Result>, List<EndClientInfoViewModal>>(endClients);
            }
        }

        public List<TaskReviewCommentViewModal> GetAllReviewComments(int employerID, int commentID = -1)
        {
            using (var db = new E2EWebPortalEntities())
            {
                var endClients = db.sp_GetAllReviewComments(employerID, commentID).ToList();
                return Mapper.Map<List<sp_GetAllReviewComments_Result>, List<TaskReviewCommentViewModal>>(endClients);
            }
        }

    }
}