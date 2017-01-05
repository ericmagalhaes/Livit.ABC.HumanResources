using System.Security.Claims;
using Livit.ABC.CommandStack.Commands;
using Livit.ABC.Domain.Query;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.LeaveApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Livit.ABC.LeaveApi.Controllers
{   
    
    /// <summary>
    /// HumanResources api controller provides methods for absence and leave requests
    /// </summary>
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
        /// <summary>
        /// responsable for absence requests
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Absence")]
        [Produces(typeof(AbsenceSchedulingRequestQueryResult))]
        
        public IActionResult RequestAbsence([FromBody]RequestAbsence request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claimName = claimsIdentity.FindFirst(c => c.Type == ClaimTypes.Name);
            var userName = claimName.Value;
            var command = new RequestAbsenceCommand(
                userName,
                request.StartDate,
                request.EndDate);

            _bus.Send(command);
            var id = command.RequestId;
            var query = new AbsenceSchedulingRequestQuery();
            query.Id = id;
            return CreatedAtRoute("GetRequestAbsence", new {id}, id);
        }

       /// <summary>
       /// return an existing absence request
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>  
        [HttpGet]
        [Route("Absence/{id}",Name="GetRequestAbsence")]
        public AbsenceSchedulingRequestQueryResult RequestAbsence(string id)
        {
            var query = new AbsenceSchedulingRequestQuery();
            query.Id = id;
            return _queryDispatcher.Dispatch<AbsenceSchedulingRequestQuery,AbsenceSchedulingRequestQueryResult>(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Route("Leave")]
        public IActionResult RequestLeave([FromBody]RequestLeave request)
        {
            var userName = SecurityInfo.GetUserIdentity(User);
            var command = new RequestLeaveCommand(
                userName,
                request.LeftDate);
            _bus.Send(command);
            var id = command.RequestId;
            var query = new AbsenceSchedulingRequestQuery();
            query.Id = id;
            return CreatedAtRoute("GetRequestLeave", new { id }, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Leave/{id}",Name="GetRequestLeave")]
        public LeaveSchedulingRequestQueryResult RequestLeave(LeaveSchedulingRequestQuery query)
        {
            return _queryDispatcher.Dispatch<LeaveSchedulingRequestQuery, LeaveSchedulingRequestQueryResult>(query);
        }


    }
}