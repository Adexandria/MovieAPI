﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesAPI.Services;

namespace MoviesAPI.Migrations
{
    [DbContext(typeof(MovieDb))]
    partial class MovieDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("MoviesAPI.Model.Movies", b =>
                {
                    b.Property<Guid>("MoviesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.HasKey("MoviesId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MoviesAPI.Model.Rentals", b =>
                {
                    b.Property<Guid>("RentalsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("OnRent")
                        .HasColumnType("bit");

                    b.HasKey("RentalsId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("MoviesAPI.Model.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MoviesAPI.Model.Rentals", b =>
                {
                    b.HasOne("MoviesAPI.Model.Movies", "Movies")
                        .WithMany()
                        .HasForeignKey("RentalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
