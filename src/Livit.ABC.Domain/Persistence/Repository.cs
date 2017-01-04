using Livit.ABC.Domain.Persistence.Models;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Livit.ABC.Domain.Persistence
{
    public class Repository : DbContext
    {
        public DbSet<ScheduleInfo> ScheduleInfos { get; set; }
        public DbSet<TaskActivity> Tasks { get; set; }
        public DbSet<ApprovalTask> ApprovalTasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EventSourcing> EventsSource { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=HumanResources.db");
            
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }

    public class Program
    {
        public static void Main()
        {
        }
    }

}