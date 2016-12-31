using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Livit.ABC.CommandStack.Events;
using Livit.ABC.Infraestructure;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;
using Newtonsoft.Json;
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
            string[] scopes = { CalendarService.Scope.CalendarReadonly };
            string ApplicationName = "HR Web";
            var tokenObject = JObject.Parse(_accessTokenService.GetValue());
            var tokenResponse = new TokenResponse();
            tokenResponse.AccessToken = tokenObject.GetValue("AccessToken").Value<string>();
            tokenResponse.TokenType = tokenObject.GetValue("TokenType").Value<string>();
            


            var service = new CalendarService();
            service.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                tokenResponse.AccessToken);
            
            var evt = new Google.Apis.Calendar.v3.Data.Event();
            evt.Summary = "asdasdasdasd";
            evt.Start = new EventDateTime()
            {
                DateTime = DateTime.Now.AddDays(2),
                TimeZone = "America/Sao_Paulo"
            };
            evt.End = new EventDateTime()
            {
                DateTime = evt.Start.DateTime.Value.AddDays(4),
                TimeZone = evt.Start.TimeZone
            };
            var request = service.Events.Insert(evt,"primary");
            var createdEvent = request.Execute();

        }
    }
}