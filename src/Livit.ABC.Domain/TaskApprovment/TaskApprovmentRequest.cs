using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Infraestructure.Common;

namespace Livit.ABC.Domain.TaskApprovment
{
    public class TaskApprovmentRequest:Aggregate
    {
        public TaskApprovmentRequest()
        {
            Id = Guid.NewGuid();
        }
        public string HumanResourcesRequestId { get; private set; }
        public bool IsApproved { get; private set; }
        public string ManagerId { get; private set; }

        public void Apply(TaskApprovmentRequestCreatedEvent evt)
        {
            HumanResourcesRequestId = evt.HumanResourcesRequestId;
            IsApproved = evt.IsApproved;
            ManagerId = evt.ManagerId;
        }

        public static class Factory
        {
            public static TaskApprovmentRequest Create(string requestId, string managerId, bool isApproved = false)
            {
                var created = new TaskApprovmentRequestCreatedEvent(requestId,isApproved,managerId);
                var request = new TaskApprovmentRequest();
                request.RaiseEvent(created);
                return request;
            }
        }



    }
}
