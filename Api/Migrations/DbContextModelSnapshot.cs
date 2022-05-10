﻿// <auto-generated />
using System;
using Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(DbContext))]
    partial class DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("Api.Models.Entities.PostEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ContactId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("TagsJson")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContactId = 12345,
                            Content = "Content1",
                            Created = new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1815),
                            TagsJson = "[]",
                            Title = "Title1",
                            Updated = new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1815)
                        },
                        new
                        {
                            Id = 2,
                            ContactId = 12345,
                            Content = "Content2",
                            Created = new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1817),
                            TagsJson = "[]",
                            Title = "Title2",
                            Updated = new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1818)
                        },
                        new
                        {
                            Id = 3,
                            ContactId = 12345,
                            Content = "Content3",
                            Created = new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1818),
                            TagsJson = "[]",
                            Title = "Title3",
                            Updated = new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1819)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
