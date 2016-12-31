using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livit.ABC.Domain.Persistence;
using Livit.ABC.Domain.Shared;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Mapper;

namespace Livit.ABC.Domain.Query
{
    public class HumanResourcesQueryHandler:
        IQueryHandler<AbsenceSchedulingRequestQuery,AbsenceSchedulingRequestQueryResult>

    {
        private readonly Repository _repository = null;
        public HumanResourcesQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public AbsenceSchedulingRequestQueryResult Retrieve(AbsenceSchedulingRequestQuery query)
        {
            var taskActivites = _repository.Tasks;
            var schedules = _repository.ScheduleInfos;
            var approvals = _repository.ApprovalTasks;

            var absenceResult = from tasks in taskActivites
                join sched in schedules on tasks.Id equals sched.TaskActivity.Id
                join apprv in approvals on tasks.Id equals apprv.TaskActivity.Id
                select new
                {
                    tasks.Id,
                    tasks.Created,
                    tasks.Modified,
                    tasks.RequestedBy,
                    sched.StartDate,
                    sched.EndDate,
                    IsApproved = apprv.Approved,
                    Approver = apprv.Approver
                };
            
            var result = MapUtil.Map<AbsenceSchedulingRequestQueryResult>(absenceResult.Single());
            return result;

        }
    }

    public class AbsenceSchedulingRequestQuery : IQuery
    {
       public string Id { get; set; }
    }
    public class AbsenceSchedulingRequestQueryResult : IQueryResult
    {
        public string Id { get; set; }
        public string RequestedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }
        public string Approver { get; set; }
    }
}
