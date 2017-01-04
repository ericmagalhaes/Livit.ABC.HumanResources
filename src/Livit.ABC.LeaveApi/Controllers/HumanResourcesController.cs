using System.Security.Claims;
using System.Threading.Tasks;
using Livit.ABC.CommandStack.Commands;
using Livit.ABC.Domain.Query;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace Livit.ABC.LeaveApi.Controllers
{
    [Route("api/[controller]")]
    public class HumanResourcesController : Controller
    {
        private readonly IBus _bus = null;
        private readonly IQueryDispatcher _queryDispatcher = null;
        public HumanResourcesController(IBus bus, IQueryDispatcher queryDispatcher)
        {
            _bus = bus;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        [Route("Absence")]
        public void RequestAbsence([FromBody]RequestAbsence request)
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claimName = claimsIdentity.FindFirst(c => c.Type == ClaimTypes.Name);
            var userName = claimName.Value;
            var command = new RequestAbsenceCommand(
                userName,
                request.StartDate,
                request.EndDate);
            _bus.Send(command);
        }
       
        [HttpGet]
        [Route("Absence/{id}")]
        public AbsenceSchedulingRequestQueryResult RequestAbsence(AbsenceSchedulingRequestQuery query)
        {
            return _queryDispatcher.Dispatch<AbsenceSchedulingRequestQuery,AbsenceSchedulingRequestQueryResult>(query);
        }
        [HttpPost]
        [Route("Leave")]
        public void RequestLeave([FromBody]RequestLeave request)
        {
            var userName = SecurityInfo.GetUserIdentity(User);
            var command = new RequestLeaveCommand(
                userName,
                request.LeftDate);
            _bus.Send(command);
        }

        [HttpGet]
        [Route("Leave/{id}")]
        public LeaveSchedulingRequestQueryResult RequestLeave(LeaveSchedulingRequestQuery query)
        {
            return _queryDispatcher.Dispatch<LeaveSchedulingRequestQuery, LeaveSchedulingRequestQueryResult>(query);
        }


    }
}