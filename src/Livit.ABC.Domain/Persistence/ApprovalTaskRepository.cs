using System;
using System.Linq;
using Livit.ABC.Domain.Scheduling;
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
        public CommandResponse CreateApprovalTaskFromProcess(string approvalManager, string taskId)
        {
            var taskActivity = _repository.Tasks.FirstOrDefault(i => i.Id == taskId);

            var approvalTask = new ApprovalTask
            {
                TaskActivity = taskActivity,
                Approver = approvalManager,
                Created = DateTime.Now
            };

            _repository.ApprovalTasks.Add(approvalTask);
            var count = _repository.SaveChanges();
            var response = new CommandResponse(Guid.Empty,count > 0,approvalTask.Id);
            return response;

        }
    }
}