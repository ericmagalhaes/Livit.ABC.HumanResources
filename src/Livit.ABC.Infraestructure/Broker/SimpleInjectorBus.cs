using System.Collections.Generic;
using System.Threading.Tasks;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;
using SimpleInjector;

namespace Livit.ABC.Infraestructure.Broker
{
    public class SimpleInjectorBus : IBus
    {
        private readonly Container _container = null;
        private readonly IEventStore _store = null;
        public SimpleInjectorBus(IEventStore store, Container container)
        {
            _container = container;
            _store = store;
        }
        public void Send<T>(T command) where T : Command
        {
            SendInternal(command);
        }
        //public async Task SendAsync<T>(T command) where T : Command
        //{
        //    await SendInternalAsync(command);
        //}

        public void RaiseEvent<T>(T @event) where T : Event
        {
            _store?.Save(@event);
            SendInternal(@event);
        }

        //public async Task RaisEventAsync<T>(T @event) where T : Event
        //{
        //    _store?.Save(@event);
        //    await SendInternalAsync(@event);
        //}


        private void SendInternal<T>(T message) where T : Message
        {
            LaunchSagasThatStartWithMessage(message);
            //DeliverMessageToRunningSagas(message);
            DeliverMessageToRegisteredHandlers(message);
        }

        //private async Task SendInternalAsync<T>(T message) where T : Message
        //{
        //    await LaunchSagasThatStartWithMessageAsync(message);
        //    //await DeliverMessageToRunningSagasAsync(message);
        //    await DeliverMessageToRegisteredHandlersAsync(message);
        //}

        private IEnumerable<T> GetContainerInstances<T>() where T : class
        {
            return _container.GetAllInstances<T>();

        }

        #region Private Methods
        private void LaunchSagasThatStartWithMessage<T>(T message) where T : Message
        {
            var sagasToLaunch = GetContainerInstances<IStartWithMessage<T>>();

            foreach (var s in sagasToLaunch)
            {
                s.Handle(message);
            }
        }
        private void DeliverMessageToRunningSagas<T>(T message) where T : Message
        {
            var sagasToNotify = GetContainerInstances<IHandleMessage<T>>();
            foreach (var saga in sagasToNotify)
            {
                saga.Handle(message);
            }
        }
        private void DeliverMessageToRegisteredHandlers<T>(T message) where T : Message
        {
            var handlersToNotify = GetContainerInstances<IHandleMessage<T>>();
            foreach (var h in handlersToNotify)
            {
                h.Handle(message);
            }
        }
        #endregion

        #region Async Methods

        //private async Task LaunchSagasThatStartWithMessageAsync<T>(T message) where T : Message
        //{
        //    var sagasToLaunch = GetContainerInstances<IStartWithMessage<T>>();

        //    foreach (var s in sagasToLaunch)
        //    {
        //        await Task.Run(() =>
        //        {
        //            s.Handle(message);
        //        });
        //    }
        //}


        //private async Task DeliverMessageToRunningSagasAsync<T>(T message) where T : Message
        //{
        //    var sagasToNotify = GetContainerInstances<IHandleMessage<T>>();
        //    foreach (var saga in sagasToNotify)
        //    {
        //        await Task.Run(() =>
        //        {
        //            saga.Handle(message);
        //        });

        //    }
        //}


        //private async Task DeliverMessageToRegisteredHandlersAsync<T>(T message) where T : Message
        //{
        //    var handlersToNotify = GetContainerInstances<IHandleMessage<T>>();
        //    foreach (var h in handlersToNotify)
        //    {

        //        await Task.Run(() =>
        //        {
        //            h.Handle(message);
        //        });
        //    }

        //}
        #endregion
    }
}