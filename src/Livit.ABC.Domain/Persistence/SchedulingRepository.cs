using System.Linq;
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

        public CommandResponse SetScheduleExternalProviderInformation(SchedulingRequest request)
        {

            var taskId = request.Id.ToString();
            var providerId = request.Provider;
            var providerScheduleId = request.ProviderScheduleId;

            var scheduleInfo = (from sInfo in _repository.ScheduleInfos
                                where sInfo.TaskActivity.Id == taskId
                                select sInfo).FirstOrDefault();
            if (scheduleInfo == null)
            {
                var scheduleNotFoundResponse = CommandResponse.Failed;
                scheduleNotFoundResponse.Description = $"ScheduleInfo Not Found: Task Id:{taskId}";
                return scheduleNotFoundResponse;
            }

            scheduleInfo.ProviderId = providerId;
            scheduleInfo.ProviderScheduleId = providerScheduleId;
            _repository.ScheduleInfos.Update(scheduleInfo);
            var count = _repository.SaveChanges();

            if (count > 0)
                return CommandResponse.Ok;
            
            var cannotUpdateDatabaseResponse = CommandResponse.Failed;
            cannotUpdateDatabaseResponse.Description =
                $"Cannot update ScheduleInfo: ScheduleInfo Id:{scheduleInfo.Id} Task Id:{taskId}";
            return cannotUpdateDatabaseResponse;
        }
    }
}