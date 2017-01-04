using Livit.ABC.Domain.Persistence.Models;

namespace Livit.ABC.Domain.Shared
{
    /// <summary>
    /// Employee implementation
    /// </summary>
    public class Employee : Person
    {

        /// <summary>
        /// default employee role
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// manager responsable for employee activities
        /// </summary>
        public virtual Employee Manager { get; set; }

        public string ManagerId { get;set; }



    }
}