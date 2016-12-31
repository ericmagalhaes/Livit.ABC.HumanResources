using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using Livit.ABC.Infraestructure.Broker;

namespace Livit.ABC.Application
{
    public static class HumanResourcesApplication
    {
        public static IBus Bus = null;
        public static Mapper Mapper = null;
        public static void SetBus(IBus bus)
        {
            Bus = bus;
        }
        public static void SetMapper(Mapper mapper)
        {
            Mapper = mapper;
        }
    }

    ///// <summary>
    ///// Facade
    ///// </summary>
    //public class HumanResourceServices
    //{
    //    /// <summary>
    //    /// schedule an absence
    //    /// </summary>
    //    /// <param name="userId">request user id</param>
    //    /// <param name="startDate">start date</param>
    //    /// <param name="endDate">end date</param>
    //    public void AddAbsenceRequest(string userId,DateTime startDate, DateTime endDate)
    //    {
    //        var command = new RequestAbsenceCommand(userId, startDate, endDate);
    //        HumanResourcesApplication.Bus.Send(command);
    //    }

    //    /// <summary>
    //    /// reschedule an absence
    //    /// </summary>
    //    /// <param name="userId">request user id</param>
    //    /// <param name="absenceId">absence id</param>
    //    /// <param name="startDate">updated start date</param>
    //    /// <param name="endDate">updated end date</param>
    //    public void RescheduleAbsence(string userId, string absenceId, DateTime startDate, DateTime endDate)
    //    {
    //        var command = new RescheduleAbsenceCommand(userId, absenceId, startDate, endDate);
    //        HumanResourcesApplication.Bus.Send(command);
    //    }
    //}
}
