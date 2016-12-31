using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Livit.ABC.CommandStack.Events;
using Livit.ABC.Domain.Persistence;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;

namespace Livit.ABC.CommandStack.Handlers
{
    public class ApprovalTaskHandler : Handler,
        IHandleMessage<ScheduleCreatedEvent>
    {
        private readonly IApprovalTaskRepository _approvalTaskResRepository = null;
        private readonly IEmployeeRepository _employeeRepository = null;
        private readonly IBus _bus = null;
        public ApprovalTaskHandler(
            IBus bus, 
            IEventStore eventStore,
            IApprovalTaskRepository approvalTaskResRepository,
            IEmployeeRepository employeeRepository) : base(eventStore)
        {
            _bus = bus;
            _approvalTaskResRepository = approvalTaskResRepository;
            _employeeRepository = employeeRepository;
        }

        public void Handle(ScheduleCreatedEvent message)
        {
            if (!message.NeedsApproval)
                return;
            //identify process owner
            var requestedBy = message.RequestedBy;
            // retrieve the manager of the owner 
            var employee = _employeeRepository.ApprovalManagerFromEmployee(requestedBy);
            if (employee.Manager == null)
            {
                var rejected = new ApprovalTaskRejectedEvent(
                    message.RequestId, 
                    "Employee does not have manager");
                _bus.RaiseEvent(rejected);
                return;
            }
                
            var approvalManagerId = employee.Manager.Id;
            var taskId = message.RequestId;
            // create an approval task using 
            var response = _approvalTaskResRepository.CreateApprovalTaskFromProcess(approvalManagerId, taskId);
            if (!response.Success)
            {
                var rejected = new ApprovalTaskRejectedEvent(message.RequestId, response.Description);
                _bus.RaiseEvent(rejected);
                return;
            }
            var created = new ApprovalTaskCreatedEvent(message.RequestId, response.AggregateId);
            _bus.RaiseEvent(created);
        }
    }

    public class SchedulerEvent
    {
        public string id { get; set; }
        public string text { get; set; }
        public string description { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }

        // Additional fields

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
