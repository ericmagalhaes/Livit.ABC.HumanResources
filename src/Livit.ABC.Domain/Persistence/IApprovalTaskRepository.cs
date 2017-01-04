using Livit.ABC.Domain.TaskApprovment;
using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.Domain.Persistence
{
    public interface IApprovalTaskRepository 
    {
        CommandResponse CreateApprovalTaskFromProcess(TaskApprovmentRequest request);
        CommandResponse SetApprovalTaskStatus(TaskApprovmentRequest request);
    }
}