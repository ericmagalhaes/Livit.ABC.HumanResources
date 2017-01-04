using System;
using System.Linq;
using Livit.ABC.Domain.Persistence.Models;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Domain.TaskApprovment;
using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.Domain.Persistence
{
    public class ApprovalTaskRepository : IApprovalTaskRepository
    {
        private readonly Repository _repository = null;
        public ApprovalTaskRepository(Repository db)
        {
            _repository = db;
        }

        private TaskActivity GetById(string id)
        {
            return _repository.Tasks.FirstOrDefault(i => i.Id == id);
        }

        private ApprovalTask GetApprovalByTaskId(string id)
        {
            return _repository.ApprovalTasks.FirstOrDefault(i => i.TaskActivity.Id == id);
        }

        public CommandResponse CreateApprovalTaskFromProcess(TaskApprovmentRequest request)
        {
            var approvalManagerId = request.ManagerId;
            var taskActivity = GetById(request.HumanResourcesRequestId);

            var approvalTask = new ApprovalTask
            {
                TaskActivity = taskActivity,
                Approver = approvalManagerId,
                Created = DateTime.Now
            };

            _repository.ApprovalTasks.Add(approvalTask);
            var count = _repository.SaveChanges();
            var response = new CommandResponse(Guid.Empty, count > 0, approvalTask.Id);
            return response;

        }

        public CommandResponse SetApprovalTaskStatus(TaskApprovmentRequest request)
        {
            var taskId = request.HumanResourcesRequestId;
            var isApproved = request.IsApproved;
            var managerId = request.ManagerId;

            var approvalTask = GetApprovalByTaskId(taskId);
            approvalTask.ApprovalDate = DateTime.Now;
            approvalTask.Approved = isApproved;
            approvalTask.Approver = managerId;
            _repository.ApprovalTasks.Update(approvalTask);
            var count = _repository.SaveChanges();
            var response = new CommandResponse(Guid.Empty, count > 0, approvalTask.Id);
            return response;

        }
    }
}