﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TerminalDashboard.Migrations.Migrations
{
    [DbContext(typeof(TerminalContext))]
    [Migration("20221226121846_FlightId")]
    partial class FlightId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TerminalDashboard.DbModel.Airplane", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirmID")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FirmID");

                    b.ToTable("Airplanes");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Airport", b =>
                {
                    b.Property<string>("Ident")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Continent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Coordinates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Elevation_ft")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gps_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Iata_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Iso_country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Iso_region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Local_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Municipality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Ident");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Firm", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ID");

                    b.ToTable("Firms");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Flight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AirplaneID")
                        .HasMaxLength(200)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime");

                    b.Property<string>("FromIdent")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LandingTime")
                        .HasColumnType("datetime");

                    b.Property<string>("NumberId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ToIdent")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AirplaneID");

                    b.HasIndex("FromIdent");

                    b.HasIndex("ToIdent");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Name", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Name");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Passenger", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("FlightId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ID");

                    b.HasIndex("FlightId");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Suitcase", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("OwnerId")
                        .HasMaxLength(200)
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("OwnerId");

                    b.ToTable("Suitcases");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Airplane", b =>
                {
                    b.HasOne("TerminalDashboard.DbModel.Firm", "Firm")
                        .WithMany("Airplanes")
                        .HasForeignKey("FirmID")
                        .IsRequired()
                        .HasConstraintName("FK_Airplanes_Firms");

                    b.Navigation("Firm");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Flight", b =>
                {
                    b.HasOne("TerminalDashboard.DbModel.Airplane", "Airplane")
                        .WithMany("Flights")
                        .HasForeignKey("AirplaneID")
                        .IsRequired()
                        .HasConstraintName("FK_Flights_Airplanes");

                    b.HasOne("TerminalDashboard.DbModel.Airport", "FromAirport")
                        .WithMany()
                        .HasForeignKey("FromIdent");

                    b.HasOne("TerminalDashboard.DbModel.Airport", "ToAirport")
                        .WithMany()
                        .HasForeignKey("ToIdent");

                    b.Navigation("Airplane");

                    b.Navigation("FromAirport");

                    b.Navigation("ToAirport");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Passenger", b =>
                {
                    b.HasOne("TerminalDashboard.DbModel.Flight", "Flight")
                        .WithMany("Passengers")
                        .HasForeignKey("FlightId")
                        .IsRequired()
                        .HasConstraintName("FK_Passengers_Flights");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Suitcase", b =>
                {
                    b.HasOne("TerminalDashboard.DbModel.Passenger", "Passenger")
                        .WithMany("Suitcases")
                        .HasForeignKey("OwnerId")
                        .IsRequired()
                        .HasConstraintName("FK_Suitcases_Passengers");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Airplane", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Firm", b =>
                {
                    b.Navigation("Airplanes");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Flight", b =>
                {
                    b.Navigation("Passengers");
                });

            modelBuilder.Entity("TerminalDashboard.DbModel.Passenger", b =>
                {
                    b.Navigation("Suitcases");
                });
#pragma warning restore 612, 618
        }
    }
}
