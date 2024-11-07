﻿// <auto-generated />
using System;
using BeautyNest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeautyNest.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241107100239_Dodavanje gradova")]
    partial class Dodavanjegradova
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BeautyNest.Models.Domain.Grad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gradovi");
                });

            modelBuilder.Entity("BeautyNest.Models.Domain.Kategorija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slika")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("BeautyNest.Models.Domain.KategorijaUsluge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("KategorijeUsluga");
                });

            modelBuilder.Entity("BeautyNest.Models.Domain.Salon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GradId")
                        .HasColumnType("int");

                    b.Property<string>("KontaktTelefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NaslovnaFotografija")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProsjecnaOcjena")
                        .HasColumnType("float");

                    b.Property<TimeSpan>("RadnoVrijemeDo")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("RadnoVrijemeOd")
                        .HasColumnType("time");

                    b.Property<bool>("SubotaRadna")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GradId");

                    b.ToTable("Saloni");
                });

            modelBuilder.Entity("BeautyNest.Models.Domain.Usluga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cijena")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("KategorijaUslugeId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Trajanje")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("KategorijaUslugeId");

                    b.ToTable("Usluge");
                });

            modelBuilder.Entity("KategorijaSalon", b =>
                {
                    b.Property<int>("KategorijeId")
                        .HasColumnType("int");

                    b.Property<int>("SaloniId")
                        .HasColumnType("int");

                    b.HasKey("KategorijeId", "SaloniId");

                    b.HasIndex("SaloniId");

                    b.ToTable("KategorijaSalon");
                });

            modelBuilder.Entity("BeautyNest.Models.Domain.KategorijaUsluge", b =>
                {
                    b.HasOne("BeautyNest.Models.Domain.Salon", "Salon")
                        .WithMany("KategorijeUsluga")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("BeautyNest.Models.Domain.Salon", b =>
                {
                    b.HasOne("BeautyNest.Models.Domain.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId");

                    b.Navigation("Grad");
                });

            modelBuilder.Entity("BeautyNest.Models.Domain.Usluga", b =>
                {
                    b.HasOne("BeautyNest.Models.Domain.KategorijaUsluge", "KategorijaUsluge")
                        .WithMany("Usluge")
                        .HasForeignKey("KategorijaUslugeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KategorijaUsluge");
                });

            modelBuilder.Entity("KategorijaSalon", b =>
                {
                    b.HasOne("BeautyNest.Models.Domain.Kategorija", null)
                        .WithMany()
                        .HasForeignKey("KategorijeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeautyNest.Models.Domain.Salon", null)
                        .WithMany()
                        .HasForeignKey("SaloniId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeautyNest.Models.Domain.KategorijaUsluge", b =>
                {
                    b.Navigation("Usluge");
                });

            modelBuilder.Entity("BeautyNest.Models.Domain.Salon", b =>
                {
                    b.Navigation("KategorijeUsluga");
                });
#pragma warning restore 612, 618
        }
    }
}