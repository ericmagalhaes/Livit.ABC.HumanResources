using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.Infraestructure.Framework.EventStore
{
    public interface IEventStore
    {
        //IEnumerable<T> Find<T>(Func<T, bool> filter);
        //IEnumerable<MatchEvent> All(string matchId);

        void Save<T>(T theEvent) where T : Event;
    }
}