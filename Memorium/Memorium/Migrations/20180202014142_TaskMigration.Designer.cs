using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Memorium.Models;

namespace Memorium.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180202014142_TaskMigration")]
    partial class TaskMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.4");

            modelBuilder.Entity("Memorium.Models.TaskModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("content");

                    b.Property<bool>("isFinished");

                    b.Property<string>("status");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.ToTable("Tasks");
                });
        }
    }
}
