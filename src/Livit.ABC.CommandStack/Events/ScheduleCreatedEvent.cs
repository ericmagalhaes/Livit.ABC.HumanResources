﻿using System;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack.Events
{
    public class ScheduleCreatedEvent : Event
    {
        public ScheduleCreatedEvent(string requestId, string requestedBy, bool needsApproval, string id, DateTime? startDate, DateTime? endDate)
        {
            RequestId = requestId;
            RequestedBy = requestedBy;
            NeedsApproval = needsApproval;
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string RequestId { get; private set; }
        public string Id { get; private set; }
        public string RequestedBy { get; private set; }
        public bool NeedsApproval { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string TokenResponse { get; private set; }
        public static ScheduleCreatedEvent FromAbsenceRequest(AbsenceRequest request)
        {
            var evt = new ScheduleCreatedEvent(
                request.Id,
                request.RequestedBy,
                request.NeedsApproval,
                request.Id,
                request.StartDate,
                request.EndDate);
            return evt;
        }

    }
}