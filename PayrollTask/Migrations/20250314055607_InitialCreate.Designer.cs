﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PayrollTask.Data;

#nullable disable

namespace PayrollTask.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250314055607_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PayrollTask.Models.Domain.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PayrollTask.Models.Domain.PTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssignedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("PTaskStatusId")
                        .HasColumnType("int");

                    b.Property<string>("ResourceFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubmissionFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SubmittedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PTaskStatusId");

                    b.ToTable("PTasks");
                });

            modelBuilder.Entity("PayrollTask.Models.Domain.PTaskStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PTaskStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Assigned"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Submitted"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Under Review"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Approved"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Rejected"
                        });
                });

            modelBuilder.Entity("PayrollTask.Models.Domain.PTask", b =>
                {
                    b.HasOne("PayrollTask.Models.Domain.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PayrollTask.Models.Domain.PTaskStatus", "PTaskStatus")
                        .WithMany()
                        .HasForeignKey("PTaskStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("PTaskStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
