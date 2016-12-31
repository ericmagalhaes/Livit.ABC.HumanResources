using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Mapper;

namespace Livit.ABC.Domain.Persistence
{
  

    public interface ISchedulingRepository 
    {
        CommandResponse CreateScheduledingFromRequest(SchedulingRequest request);
    }
}

