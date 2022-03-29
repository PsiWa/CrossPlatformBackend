﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Seregin_Backend.Data;

namespace Seregin_Backend.Migrations
{
    [DbContext(typeof(BuildingContext))]
    [Migration("20220329211922_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Seregin_Backend.Models.Apartment", b =>
                {
                    b.Property<int>("ApartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("GeneralArea")
                        .HasColumnType("float");

                    b.Property<int>("InBldngID")
                        .HasColumnType("int");

                    b.Property<double>("KitchenArea")
                        .HasColumnType("float");

                    b.Property<double>("LivingArea")
                        .HasColumnType("float");

                    b.Property<int>("RoomsN")
                        .HasColumnType("int");

                    b.HasKey("ApartmentID");

                    b.HasIndex("InBldngID");

                    b.ToTable("Apartment");
                });

            modelBuilder.Entity("Seregin_Backend.Models.Building", b =>
                {
                    b.Property<int>("BuildingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CeilingH")
                        .HasColumnType("float");

                    b.Property<string>("CodeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DemolitionPerspective")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FloorsN")
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Review")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BuildingID");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("Seregin_Backend.Models.DesignProject", b =>
                {
                    b.Property<int>("DesignProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AptID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("DesignProjectID");

                    b.HasIndex("AptID");

                    b.HasIndex("UserID");

                    b.ToTable("DesignProject");
                });

            modelBuilder.Entity("Seregin_Backend.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("USurame")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Seregin_Backend.Models.Apartment", b =>
                {
                    b.HasOne("Seregin_Backend.Models.Building", "Bldng")
                        .WithMany("Apts")
                        .HasForeignKey("InBldngID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Seregin_Backend.Models.DesignProject", b =>
                {
                    b.HasOne("Seregin_Backend.Models.Apartment", "Apt")
                        .WithMany("Projects")
                        .HasForeignKey("AptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Seregin_Backend.Models.User", "Usr")
                        .WithMany("Projects")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
