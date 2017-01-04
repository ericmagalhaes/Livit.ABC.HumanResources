using System;
using System.Net.Http.Headers;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Livit.ABC.CommandStack.Events;
using Livit.ABC.Infraestructure;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;
using Newtonsoft.Json.Linq;

namespace Livit.ABC.CommandStack.Handlers
{
    public class GoogleCalendarHandler : Handler,
        IHandleMessage<ScheduleCreatedEvent>
    {
        private readonly AccessTokenService _accessTokenService = null;
        public GoogleCalendarHandler(IEventStore eventStore,AccessTokenService accessTokenService) : base(eventStore)
        {
            _accessTokenService = accessTokenService;
        }

       
        public void Handle(ScheduleCreatedEvent message)
        {
           

        }
    }
}