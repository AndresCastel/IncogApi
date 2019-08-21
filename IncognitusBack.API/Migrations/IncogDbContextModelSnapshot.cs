﻿// <auto-generated />
using System;
using IncognitusBack.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IncognitusBack.API.Migrations
{
    [DbContext(typeof(IncogDbContext))]
    partial class IncogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IncognitusBack.Core.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Barcode");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Payroll")
                        .IsRequired();

                    b.Property<int>("RolId");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("IncognitusBack.Core.Entities.Employee_Register", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("Break");

                    b.Property<int>("EmployeeId");

                    b.Property<DateTime>("SignIn");

                    b.Property<DateTime>("Signoff");

                    b.Property<int>("Type_RegisterId");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("Type_RegisterId");

                    b.ToTable("Employee_Register");
                });

            modelBuilder.Entity("IncognitusBack.Core.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("IncognitusBack.Core.Entities.RosterC", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Area");

                    b.Property<string>("Break");

                    b.Property<bool>("Confirm");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Day");

                    b.Property<string>("Employee");

                    b.Property<string>("EndTime");

                    b.Property<string>("LabourType");

                    b.Property<string>("LookedIn");

                    b.Property<string>("Payroll");

                    b.Property<string>("Precint");

                    b.Property<int>("ShiftNum");

                    b.Property<string>("StartTime");

                    b.Property<string>("Zone");

                    b.HasKey("Id");

                    b.ToTable("RosterC");
                });

            modelBuilder.Entity("IncognitusBack.Core.Entities.Stuff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Stuff");
                });

            modelBuilder.Entity("IncognitusBack.Core.Entities.StuffType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameStuff")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("StuffType");
                });

            modelBuilder.Entity("IncognitusBack.Core.Entities.Stuff_Assign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("Employee_RegisterId");

                    b.Property<int>("Quantity");

                    b.Property<int>("StuffId");

                    b.HasKey("Id");

                    b.HasIndex("Employee_RegisterId");

                    b.HasIndex("StuffId");

                    b.ToTable("Stuff_Assign");
                });

            modelBuilder.Entity("IncognitusBack.Core.Entities.Type_Register", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Type_Register");
                });

            modelBuilder.Entity("IncognitusBack.Core.Entities.Employee", b =>
                {
                    b.HasOne("IncognitusBack.Core.Entities.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IncognitusBack.Core.Entities.Employee_Register", b =>
                {
                    b.HasOne("IncognitusBack.Core.Entities.Employee", "Employe")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IncognitusBack.Core.Entities.Type_Register", "Type_Register")
                        .WithMany()
                        .HasForeignKey("Type_RegisterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IncognitusBack.Core.Entities.Stuff", b =>
                {
                    b.HasOne("IncognitusBack.Core.Entities.StuffType", "StuffType")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IncognitusBack.Core.Entities.Stuff_Assign", b =>
                {
                    b.HasOne("IncognitusBack.Core.Entities.Employee_Register", "Employee_Register")
                        .WithMany()
                        .HasForeignKey("Employee_RegisterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IncognitusBack.Core.Entities.Stuff", "Stuff")
                        .WithMany()
                        .HasForeignKey("StuffId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
