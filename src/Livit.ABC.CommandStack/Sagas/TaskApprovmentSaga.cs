using Livit.ABC.CommandStack.Commands;
using Livit.ABC.CommandStack.Events;
using Livit.ABC.Domain.Persistence;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Domain.TaskApprovment;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;

namespace Livit.ABC.CommandStack.Sagas
{
    public class TaskApprovmentSaga : Saga,
        IStartWithMessage<ScheduleCreatedEvent>,
        IHandleMessage<RequestApprovmentCommand>
    {
        private readonly IBus _bus = null;
        private readonly IEventStore _eventStore = null;
        private readonly IApprovalTaskRepository _approvalTaskRepository = null;
        private readonly IEmployeeRepository _employeeRepository = null;
        public TaskApprovmentSaga(
            IBus bus, 
            IEventStore eventStore, 
            IApprovalTaskRepository approvalTaskRepository,
            IEmployeeRepository employeeRepository) : base(bus, eventStore)
        {
            _bus = bus;
            _eventStore = eventStore;
            _approvalTaskRepository = approvalTaskRepository;
            _employeeRepository = employeeRepository;
        }

        public void Handle(RequestApprovmentCommand message)
        {
            var requestId = message.HumanResourcesRequestId;
            var isApproved = message.IsApproved;
            var managerId = message.ManagerId;
            var request = TaskApprovmentRequest.Factory.Create(requestId, managerId, isApproved);
            
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
            var request = TaskApprovmentRequest.Factory.Create(taskId, approvalManagerId);
            var response = _approvalTaskRepository.CreateApprovalTaskFromProcess(request);
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
}