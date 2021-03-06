﻿// <auto-generated />
using System;
using Kairos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kairos.Migrations
{
    [DbContext(typeof(HomeContext))]
    [Migration("20200227175312_GioMigrations")]
    partial class GioMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Kairos.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Kairos.Models.Watch", b =>
                {
                    b.Property<int>("WatchId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("Company");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Gender");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Material");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<string>("Size");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("WatchId");

                    b.ToTable("Watches");
                });
#pragma warning restore 612, 618
        }
    }
}
