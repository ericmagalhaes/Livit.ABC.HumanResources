namespace Livit.ABC.Domain.Persistence.Models
{
    /// <summary>
    /// Base class for employee Entity
    /// </summary>
    public abstract class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
    }
}