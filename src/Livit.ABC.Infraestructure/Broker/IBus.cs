using System.Threading.Tasks;
using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.Infraestructure.Broker
{
    public interface IBus
    {
        void Send<T>(T command) where T : Command;
        //Task SendAsync<T>(T command) where T : Command;
        void RaiseEvent<T>(T theEvent) where T : Event;
        

    }
}