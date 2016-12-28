using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;

namespace Livit.ABC.Infraestructure.Broker
{
    public class InMemoryBus : IBus
    {
        public IEventStore EventStore { get; private set; }


        private static readonly IDictionary<Type, Type> RegisteredSagas = new Dictionary<Type, Type>();
        private static readonly IList<Type> RegisteredHandlers = new List<Type>();

        public InMemoryBus(IEventStore eventStore)
        {
            //if (eventStore == null)
            //{
            //    throw new ArgumentNullException("eventStore");
            //}
            EventStore = eventStore;
        }


        #region IBus
       

        void IBus.Send<T>(T message)
        {
            SendInternal(message);
        }

        //async Task IBus.SendAsync<T>(T message)
        //{
        //    await SendInternalAsync(message);
        //}
        void IBus.RaiseEvent<T>(T theEvent)  
        {
            if (EventStore != null)
                EventStore.Save(theEvent);
            SendInternal(theEvent);
        }
        #endregion


        #region Private Members
        private void SendInternal<T>(T message) where T : Message
        {
            LaunchSagasThatStartWithMessage(message);
            DeliverMessageToRunningSagas(message);
            DeliverMessageToRegisteredHandlers(message);

            // Saga and handlers are similar things. Handlers are  one-off event handlers
            // whereas saga may be persisted and survive sessions, wait for more messages and so forth.
            // Saga are mostly complex workflows; handlers are plain one-off event handlers.
        }
        //private async Task SendInternalAsync<T>(T message) where T : Message
        //{
        //    await LaunchSagasThatStartWithMessageAsync(message);
        //    await DeliverMessageToRunningSagasAsync(message);
        //    await DeliverMessageToRegisteredHandlersAsync(message);

        //    // Saga and handlers are similar things. Handlers are  one-off event handlers
        //    // whereas saga may be persisted and survive sessions, wait for more messages and so forth.
        //    // Saga are mostly complex workflows; handlers are plain one-off event handlers.
        //}

        private void LaunchSagasThatStartWithMessage<T>(T message) where T : Message
        {
            var messageType = message.GetType();
            var openInterface = typeof(IStartWithMessage<>);
            var closedInterface = openInterface.MakeGenericType(messageType);
            var sagasToLaunch = from s in RegisteredSagas.Values
                                 where closedInterface.IsAssignableFrom(s)
                                 select s;
            foreach (var s in sagasToLaunch)
            {
               dynamic sagaInstance = Activator.CreateInstance(s, this, EventStore);
                sagaInstance.Handle(message);
            }
        }
        //private async Task LaunchSagasThatStartWithMessageAsync<T>(T message) where T : Message
        //{
        //    var messageType = message.GetType();
        //    var openInterface = typeof(IStartWithMessage<>);
        //    var closedInterface = openInterface.MakeGenericType(messageType);
        //    var sagasToLaunch = from s in RegisteredSagas.Values
        //                        where closedInterface.IsAssignableFrom(s)
        //                        select s;
        //    foreach (var s in sagasToLaunch)
        //    {
        //        dynamic sagaInstance = Activator.CreateInstance(s, this, EventStore);
        //        await Task.Run(() =>
        //        {
        //            sagaInstance.Handle(message);
        //        });
        //    }
            
        //}

        private void DeliverMessageToRunningSagas<T>(T message) where T : Message
        {
            var messageType = message.GetType();
            var openInterface = typeof(IHandleMessage<>);
            var closedInterface = openInterface.MakeGenericType(messageType);
            var sagasToNotify = from s in RegisteredSagas.Values
                                where closedInterface.IsAssignableFrom(s)
                                select s;
            foreach (var s in sagasToNotify)
            {
                dynamic sagaInstance = Activator.CreateInstance(s, this, EventStore);
                sagaInstance.Handle(message);
            }
        }
        //private async Task DeliverMessageToRunningSagasAsync<T>(T message) where T : Message
        //{
        //    var messageType = message.GetType();
        //    var openInterface = typeof(IHandleMessage<>);
        //    var closedInterface = openInterface.MakeGenericType(messageType);
        //    var sagasToNotify = from s in RegisteredSagas.Values
        //                        where closedInterface.IsAssignableFrom(s)
        //                        select s;
        //    foreach (var s in sagasToNotify)
        //    {
        //        dynamic sagaInstance = Activator.CreateInstance(s, this, EventStore);
        //        await Task.Run(() =>
        //        {
        //            sagaInstance.Handle(message);
        //        });
                
        //    }
        //}

        private void DeliverMessageToRegisteredHandlers<T>(T message) where T : Message
        {
            var messageType = message.GetType();
            var openInterface = typeof(IHandleMessage<>);
            var closedInterface = openInterface.MakeGenericType(messageType);
            var handlersToNotify = from h in RegisteredHandlers
                                   where closedInterface.IsAssignableFrom(h)
                                   select h;
            foreach (var h in handlersToNotify)
            {
                dynamic sagaInstance = Activator.CreateInstance(h, EventStore);     // default ctor is enough
                sagaInstance.Handle(message);
            }
        }
        //private async Task DeliverMessageToRegisteredHandlersAsync<T>(T message) where T : Message
        //{
        //    var messageType = message.GetType();
        //    var openInterface = typeof(IHandleMessage<>);
        //    var closedInterface = openInterface.MakeGenericType(messageType);
        //    var handlersToNotify = from h in RegisteredHandlers
        //                           where closedInterface.IsAssignableFrom(h)
        //                           select h;
        //    foreach (var h in handlersToNotify)
        //    {
        //        dynamic sagaInstance = Activator.CreateInstance(h, EventStore);     // default ctor is enough
        //        await Task.Run(() => { 
        //            sagaInstance.Handle(message);
        //        });
        //    }
        //}
        #endregion
    }
}