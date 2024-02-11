﻿// <auto-generated />
using System;
using Memidlo.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Memidlo.Web.Migrations
{
    [DbContext(typeof(MemidloDbContext))]
    [Migration("20230613174436_Remove URLHandle property from Mem")]
    partial class RemoveURLHandlepropertyfromMem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Memidlo.Web.Models.Domain.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Memidlo.Web.Models.Domain.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Memidlo.Web.Models.Domain.Mem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeaturedImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Main")
                        .HasColumnType("bit");

                    b.Property<string>("PageTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Mems");
                });

            modelBuilder.Entity("Memidlo.Web.Models.Domain.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MemId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Memidlo.Web.Models.Domain.Comment", b =>
                {
                    b.HasOne("Memidlo.Web.Models.Domain.Mem", null)
                        .WithMany("Comments")
                        .HasForeignKey("MemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Memidlo.Web.Models.Domain.Like", b =>
                {
                    b.HasOne("Memidlo.Web.Models.Domain.Mem", null)
                        .WithMany("Likes")
                        .HasForeignKey("MemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Memidlo.Web.Models.Domain.Tag", b =>
                {
                    b.HasOne("Memidlo.Web.Models.Domain.Mem", null)
                        .WithMany("Tags")
                        .HasForeignKey("MemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Memidlo.Web.Models.Domain.Mem", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
