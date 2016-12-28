namespace Livit.ABC.Domain
{
    /// <summary>
    /// Base class for employee Entity
    /// </summary>
    public abstract class Person
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
    }
}