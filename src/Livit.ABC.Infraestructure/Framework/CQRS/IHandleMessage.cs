namespace Livit.ABC.Infraestructure.Framework.CQRS
{
    public interface IHandleMessage<in T>
    {
        void Handle(T message);
    }
}