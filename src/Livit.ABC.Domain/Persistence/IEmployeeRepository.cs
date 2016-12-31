using Livit.ABC.Domain.Shared;

namespace Livit.ABC.Domain.Persistence
{
    public interface IEmployeeRepository 
    {
        Employee ApprovalManagerFromEmployee(string eomployeeId);
        Employee RegisterEmployee(string email, string managerId=null);
    }
}