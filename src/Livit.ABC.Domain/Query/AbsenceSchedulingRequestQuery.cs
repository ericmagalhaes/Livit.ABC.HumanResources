using System.Collections.Generic;
using System.Threading.Tasks;
using Livit.ABC.Domain.Shared;
using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.Domain.Query
{
    public class AbsenceSchedulingRequestQuery : IQuery
    {
       public string Id { get; set; }
    }
    public class LeaveSchedulingRequestQuery : IQuery
    {
        public string Id { get; set; }
    }

    public class TaskApprovmentRequestQuery : IQuery
    {
        public string Id { get; set; }
    }
}
