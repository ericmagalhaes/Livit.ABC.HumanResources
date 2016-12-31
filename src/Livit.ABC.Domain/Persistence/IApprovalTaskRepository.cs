using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.Domain.Persistence
{
    public interface IApprovalTaskRepository 
    {
        CommandResponse CreateApprovalTaskFromProcess(string approvalManager,string taskId);
    }
}