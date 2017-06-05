using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ChildrensActivityLog2.Repositories;
using ChildrensActivityLog2.Models;

namespace ChildrensActivityLog2.Migrations
{
    [DbContext(typeof(ChildrensActivityLogContext))]
    partial class ChildrensActivityLogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChildrensActivityLog2.Models.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("ChildrensActivityLog2.Models.ChildrensPlayEvents", b =>
                {
                    b.Property<int>("ChildId");

                    b.Property<int>("PlayEventId");

                    b.HasKey("ChildId", "PlayEventId");

                    b.HasIndex("PlayEventId");

                    b.ToTable("ChildrensPlayEvents");
                });

            modelBuilder.Entity("ChildrensActivityLog2.Models.PlayEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("PlayEvents");
                });

            modelBuilder.Entity("ChildrensActivityLog2.Models.SleepingPeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChildId");

                    b.Property<DateTime>("From");

                    b.Property<DateTime>("To");

                    b.Property<int>("TypeOfSleepingPeriod");

                    b.HasKey("Id");

                    b.HasIndex("ChildId");

                    b.ToTable("SleepingPeriods");
                });

            modelBuilder.Entity("ChildrensActivityLog2.Models.ChildrensPlayEvents", b =>
                {
                    b.HasOne("ChildrensActivityLog2.Models.Child", "Child")
                        .WithMany("ChildrensPlayEvents")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChildrensActivityLog2.Models.PlayEvent", "PlayEvent")
                        .WithMany("ChildrensPlayEvents")
                        .HasForeignKey("PlayEventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChildrensActivityLog2.Models.SleepingPeriod", b =>
                {
                    b.HasOne("ChildrensActivityLog2.Models.Child", "Child")
                        .WithMany("SleepingPeriods")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
