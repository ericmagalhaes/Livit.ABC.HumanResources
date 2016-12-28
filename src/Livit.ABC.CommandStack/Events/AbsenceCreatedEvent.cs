using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack.Events
{
    public class AbsenceCreatedEvent : Event
    {
        public AbsenceCreatedEvent(Guid requestId,string absenceId)
        {
            AbsenceId = absenceId;
        }
        public string AbsenceId { get; set; }
        public Guid RequestId { get; set; }
        
    }
}
