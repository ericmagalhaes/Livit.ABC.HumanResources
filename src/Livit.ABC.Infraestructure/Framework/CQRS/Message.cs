
namespace Livit.ABC.Infraestructure.Framework.CQRS
{
    public class Message
    {
        public string SagaId { get; protected set; }
        public string Name { get; protected set; }
    }
}