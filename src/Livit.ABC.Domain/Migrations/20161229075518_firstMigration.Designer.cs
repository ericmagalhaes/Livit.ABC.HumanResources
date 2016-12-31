using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Livit.ABC.Domain.Persistence;

namespace Livit.ABC.Domain.Migrations
{
    [DbContext(typeof(Repository))]
    [Migration("20161229075518_firstMigration")]
    partial class firstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Livit.ABC.Domain.Scheduling.ScheduleInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<Guid?>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("ScheduleInfos");
                });

            modelBuilder.Entity("Livit.ABC.Domain.Scheduling.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<bool>("NeedsApproval");

                    b.Property<string>("RequestedBy");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Livit.ABC.Domain.Scheduling.ScheduleInfo", b =>
                {
                    b.HasOne("Livit.ABC.Domain.Scheduling.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId");
                });
        }
    }
}
