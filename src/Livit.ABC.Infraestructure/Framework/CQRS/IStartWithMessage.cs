namespace Livit.ABC.Infraestructure.Framework.CQRS
{
    public interface IStartWithMessage<in T>
    {
        void Handle(T message);
    }
}