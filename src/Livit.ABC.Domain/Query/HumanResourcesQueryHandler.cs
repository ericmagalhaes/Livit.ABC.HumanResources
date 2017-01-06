using System.Linq;
using Livit.ABC.Domain.Persistence;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Mapper;

namespace Livit.ABC.Domain.Query
{
    public class HumanResourcesQueryHandler:
        IQueryHandler<AbsenceSchedulingRequestQuery,AbsenceSchedulingRequestQueryResult>,
        IQueryHandler<LeaveSchedulingRequestQuery,LeaveSchedulingRequestQueryResult>

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
            var taskId = query.Id;
            var absenceResult = from tasks in taskActivites
                join sched in schedules on tasks.Id equals sched.TaskActivity.Id
                join apprv in approvals on tasks.Id equals apprv.TaskActivity.Id
                where tasks.Id == taskId
                select new
                {
                    tasks.Id,
                    tasks.Created,
                    tasks.Modified,
                    tasks.RequestedBy,
                    sched.StartDate,
                    sched.EndDate,
                    IsApproved = apprv.Approved,
                    sched.Description,
                    sched.ProviderId,
                    sched.ProviderScheduleId,
                    apprv.Approver
                };
            
            var result = MapUtil.Map<AbsenceSchedulingRequestQueryResult>(absenceResult.Single());
            return result;

        }

        public LeaveSchedulingRequestQueryResult Retrieve(LeaveSchedulingRequestQuery query)
        {
            var taskActivites = _repository.Tasks;
            var schedules = _repository.ScheduleInfos;
            var approvals = _repository.ApprovalTasks;
            var taskId = query.Id;
            var leaveResult = from tasks in taskActivites
                                join sched in schedules on tasks.Id equals sched.TaskActivity.Id
                                join apprv in approvals on tasks.Id equals apprv.TaskActivity.Id
                                where tasks.Id == taskId
                                select new
                                {
                                    tasks.Id,
                                    tasks.Created,
                                    tasks.Modified,
                                    tasks.RequestedBy,
                                    sched.StartDate,
                                    sched.EndDate,
                                    IsApproved = apprv.Approved,
                                    sched.Description,
                                    sched.ProviderId,
                                    sched.ProviderScheduleId,
                                    apprv.Approver
                                };

            var result = MapUtil.Map<LeaveSchedulingRequestQueryResult>(leaveResult.Single());
            return result;
        }
    }
}