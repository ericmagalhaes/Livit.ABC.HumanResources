using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Infraestructure.Common;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Mapper;

namespace Livit.ABC.Domain.Persistence
{
    public class SchedulingRepository : ISchedulingRepository
    {
        private readonly Repository _repository = null;
        public SchedulingRepository(Repository db)
        {
            _repository = db;
        }

        public virtual CommandResponse CreateScheduledingFromRequest(SchedulingRequest request)
        {
            var scheduleInfo = MapUtil.Map<SchedulingRequest, ScheduleInfo>(request);
            _repository.ScheduleInfos.Add(scheduleInfo);
            var count = _repository.SaveChanges();
            
            var response = new CommandResponse(request.Id, count > 0, request.Id.ToString());
            return response;
        }
    }
}