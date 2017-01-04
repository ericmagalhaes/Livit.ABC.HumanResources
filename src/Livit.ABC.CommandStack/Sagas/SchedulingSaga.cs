using System;
using Livit.ABC.CommandStack.Commands;
using Livit.ABC.CommandStack.Events;
using Livit.ABC.Domain.Persistence;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;
using Livit.ABC.Infraestructure.Mapper;

namespace Livit.ABC.CommandStack.Sagas
{
    /// <summary>
    /// Saga works like a process manager
    /// It`ll route the command to their specific handle
    /// </summary>
    public class SchedulingSaga : Saga,
        IStartWithMessage<RequestAbsenceCommand>,
        IStartWithMessage<RequestLeaveCommand>
    {
        private readonly ISchedulingRepository _schedulingRepository = null;
        #region constructors
        public SchedulingSaga(IBus bus, IEventStore eventStore, ISchedulingRepository schedulingRepository) : base(bus, eventStore)
        {
            _schedulingRepository = schedulingRepository;
        }
        
        #endregion
        
        #region StartWithMessage

        public void Handle(RequestAbsenceCommand message)
        {
            var request = MapUtil.Map<RequestAbsenceCommand, SchedulingRequest>(message);
            
            var response = _schedulingRepository.CreateScheduledingFromRequest(request);

            if (!response.Success)
            {
                var rejected = new ScheduleRequestRejectedEvent(request.Id.ToString(), response.Description);
                Bus.RaiseEvent(rejected);
                return;
            }
            message.RequestId = response.RequestId.ToString();
            var absenceRequest = AbsenceRequest.Factory.Create(
                response.RequestId.ToString(), 
                request.RequestedBy,
                request.StartDate, 
                request.EndDate);

            var created = MapUtil.Map<AbsenceRequest, ScheduleCreatedEvent>(absenceRequest);
            
            Bus.RaiseEvent(created);
        }

        public void Handle(RequestLeaveCommand message)
        {
            var request = MapUtil.Map<RequestLeaveCommand, SchedulingRequest>(message);
            var response = _schedulingRepository.CreateScheduledingFromRequest(request);

            if (!response.Success)
            {
                var rejected = new ScheduleRequestRejectedEvent(request.Id.ToString(), response.Description);
                Bus.RaiseEvent(rejected);
                return;
            }

            var leaveRequest = LeaveRequest.Factory.Create(
                response.AggregateId,
                request.RequestedBy,
                request.EndDate);

            var created = MapUtil.Map<LeaveRequest, ScheduleCreatedEvent>(leaveRequest);
            Bus.RaiseEvent(created);
        }
        
        #endregion
        
    }
    
}
