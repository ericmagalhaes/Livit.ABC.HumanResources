using System;

namespace Livit.ABC.Infraestructure.Framework.CQRS
{
    public class CommandResponse
    {
        public static CommandResponse Ok = new CommandResponse {Success = true};
        public static CommandResponse Failed = new CommandResponse { Success = false };

        public CommandResponse(Guid requestId = default(Guid), Boolean success = false, string aggregateId = "")
        {
            Success = success;
            AggregateId = aggregateId;
            Description = String.Empty;
            RequestId = requestId;
        }

        public Boolean Success { get; private set; }
        public string AggregateId { get; private set; }
        public Guid RequestId { get; set; }
        public string Description { get; set; }
    }
}