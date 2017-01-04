using System;
using System.Net.Http.Headers;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Livit.ABC.CommandStack.Events;
using Livit.ABC.Domain.Persistence;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Infraestructure;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;
using Livit.ABC.Infraestructure.Mapper;
using Newtonsoft.Json.Linq;

namespace Livit.ABC.CommandStack.Sagas
{
    public class ExternalProviderSaga : Saga,
        IStartWithMessage<SetApprovalStatusCreatedEvent>,
        IHandleMessage<ExternalProviderScheduleCreatedEvent>
    {
        private readonly IBus _bus = null;
        private readonly IEventStore _eventStore = null;
        private readonly ISchedulingRepository _schedulingRepository = null;
        private readonly AccessTokenService _accessTokenService = null;
        public ExternalProviderSaga(IBus bus, IEventStore eventStore, ISchedulingRepository schedulingRepository, AccessTokenService accessTokenService) : base(bus, eventStore)
        {
            _bus = bus;
            _eventStore = eventStore;
            _schedulingRepository = schedulingRepository;
            _accessTokenService = accessTokenService;

        }

        public void Handle(ExternalProviderScheduleCreatedEvent message)
        {
            var schedulingRequest = new SchedulingRequest();
            var evt = MapUtil.Map
                <ExternalProviderScheduleCreatedEvent,
                SchedulingRequestExternalScheduleProviderCreatedEvent>(message);
            schedulingRequest.Apply(evt);
            var response = _schedulingRepository.SetScheduleExternalProviderInformation(schedulingRequest);
            if(response.Success)
                _eventStore.Save(message);

        }

        public void Handle(SetApprovalStatusCreatedEvent message)
        {
            var requestId = message.RequestId;
            var service = CalendarServiceByToken(_accessTokenService);

            var  scheduleInfo = _schedulingRepository.ScheduleInfoByRequest(requestId);

            var evt = new Google.Apis.Calendar.v3.Data.Event
            {
                Summary = scheduleInfo.Description,
                Start = new EventDateTime()
                {
                    DateTime = scheduleInfo.StartDate,
                    TimeZone = "America/Sao_Paulo"
                }
            };
            if(scheduleInfo.EndDate != DateTime.MinValue)
                evt.End = new EventDateTime()
                {
                    DateTime = scheduleInfo.EndDate,
                    TimeZone = evt.Start.TimeZone
                };
            var request = service.Events.Insert(evt, "primary");
            var createdEvent = request.Execute();
            var created = new ExternalProviderScheduleCreatedEvent(requestId,"Google",createdEvent.ICalUID);
            _bus.RaiseEvent(created);
        }

        private CalendarService CalendarServiceByToken(AccessTokenService accessTokenService)
        {
            var tokenObject = JObject.Parse(_accessTokenService.GetValue());
            var tokenResponse = new TokenResponse();
            tokenResponse.AccessToken = tokenObject.GetValue("AccessToken").Value<string>();

            var service = new CalendarService();
            service.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                tokenResponse.AccessToken);
            return service;
        }
    }
}