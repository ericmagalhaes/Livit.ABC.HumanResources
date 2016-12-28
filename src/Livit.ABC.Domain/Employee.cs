namespace Livit.ABC.Domain
{
    /// <summary>
    /// Employee implementation
    /// </summary>
    public class Employee : Person
    {
        /// <summary>
        /// default employee role
        /// </summary>
        public string Role { get; private set; }
        /// <summary>
        /// manager responsable for employee activities
        /// </summary>
        public Employee Manager { get; private set; }
    }
}