using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Livit.ABC.Domain.Persistence;

namespace Livit.ABC.Domain.Migrations
{
    [DbContext(typeof(Repository))]
    partial class RepositoryModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Livit.ABC.Domain.Scheduling.ApprovalTask", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ApprovalDate");

                    b.Property<bool>("Approved");

                    b.Property<string>("Approver");

                    b.Property<DateTime>("Created");

                    b.Property<string>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("ApprovalTasks");
                });

            modelBuilder.Entity("Livit.ABC.Domain.Scheduling.EventSourcing", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<string>("AggregateId");

                    b.Property<string>("Body");

                    b.Property<string>("SagaId");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("Id");

                    b.ToTable("EventsSource");
                });

            modelBuilder.Entity("Livit.ABC.Domain.Scheduling.ScheduleInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("TaskId");

                    b.Property<string>("ProviderId");

                    b.Property<string>("ProviderScheduleId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("ScheduleInfos");
                });

            modelBuilder.Entity("Livit.ABC.Domain.Scheduling.Task", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<bool>("NeedsApproval");

                    b.Property<string>("RequestedBy");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Livit.ABC.Domain.Shared.Employee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ManagerId");

                    b.Property<string>("Name");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Livit.ABC.Domain.Scheduling.ApprovalTask", b =>
                {
                    b.HasOne("Livit.ABC.Domain.Scheduling.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("Livit.ABC.Domain.Scheduling.ScheduleInfo", b =>
                {
                    b.HasOne("Livit.ABC.Domain.Scheduling.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("Livit.ABC.Domain.Shared.Employee", b =>
                {
                    b.HasOne("Livit.ABC.Domain.Shared.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");
                });
        }
    }
}
