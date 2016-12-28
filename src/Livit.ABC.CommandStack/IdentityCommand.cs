using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack
{
    /// <summary>
    /// Responsable to pass sensitive user information for authentication and authorization purpose
    /// </summary>
    public abstract class IdentityCommand : Command
    {
        protected IdentityCommand(string requestedBy)
        {
            RequestedBy = requestedBy;
        }
        /// <summary>
        /// User unique identification who made the request
        /// </summary>
        public string RequestedBy { get; private set; }
    }
}