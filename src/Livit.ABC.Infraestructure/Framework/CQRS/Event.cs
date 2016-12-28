using System;

namespace Livit.ABC.Infraestructure.Framework.CQRS
{
    public class Event : Message
    {
        public DateTime TimeStamp { get; private set; }

        public Event()
        {
            TimeStamp = DateTime.Now;
            Name = this.GetType().Name;
        }

    }
}