using System;

namespace Livit.ABC.Infraestructure.Common
{
    public class DomainEvent 
    {
        public DateTime TimeStamp { get; private set; }

        public DomainEvent()
        {
            TimeStamp = DateTime.Now;
        }
    }
}