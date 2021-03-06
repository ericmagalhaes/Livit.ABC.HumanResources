﻿using System.Security.Claims;
using Livit.ABC.CommandStack.Commands;
using Livit.ABC.Domain.Query;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.LeaveApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livit.ABC.LeaveApi.Controllers
{
    [Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    public class TaskApprovmentController : Controller
    {
        private readonly IBus _bus = null;
        private readonly IQueryDispatcher _queryDispatcher = null;
        public TaskApprovmentController(IBus bus, IQueryDispatcher queryDispatcher)
        {
            _bus = bus;
            _queryDispatcher = queryDispatcher;
        }
        [HttpPost]
        public void RequestAbsence([FromBody]RequestApprovment request)
        {
            var userName = SecurityInfo.GetUserIdentity(User);
            var requestId = request.HumanResourcesRequestId;
            var isApproved = request.IsApproved;
            var command = new RequestApprovmentCommand(
                userName,
                requestId,
                isApproved,
                userName);
            _bus.Send(command);
        }

        [HttpGet]
        public TaskApprovmentRequestQueryResult RequestAbsence([FromQuery]TaskApprovmentRequestQuery query)
        {
            return _queryDispatcher.Dispatch<TaskApprovmentRequestQuery, TaskApprovmentRequestQueryResult>(query);
        }
    }

    public static class SecurityInfo
    {
        public static string GetUserIdentity(ClaimsPrincipal identity)
        {
            var claimsIdentity = identity;
            var claimName = claimsIdentity.FindFirst(c => c.Type == ClaimTypes.Name);
            var userName = claimName.Value;
            return userName;
        }
    }
}