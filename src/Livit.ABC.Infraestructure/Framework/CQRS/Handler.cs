using System;
using Livit.ABC.Infraestructure.Framework.EventStore;

namespace Livit.ABC.Infraestructure.Framework.CQRS
{
    public abstract class Handler
    {
        public IEventStore EventStore { get; private set; }


        public Handler(IEventStore eventStore)
        {
            if (eventStore == null)
            {
                throw new ArgumentNullException("eventStore");
            }

            EventStore = eventStore;
        }
    }

}