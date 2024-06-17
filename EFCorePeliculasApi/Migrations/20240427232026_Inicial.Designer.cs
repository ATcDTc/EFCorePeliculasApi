﻿// <auto-generated />
using System;
using EFCorePeliculasApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240427232026_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Biografia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Actores");
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.Cine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<Point>("Ubicacion")
                        .HasColumnType("geography");

                    b.HasKey("Id");

                    b.ToTable("Cines");
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.CineOferta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CineId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFinal")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<decimal>("PorcentajeDescuento")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.HasIndex("CineId")
                        .IsUnique();

                    b.ToTable("CinesOfertas");
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.Genero", b =>
                {
                    b.Property<int>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Identificador"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Identificador");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("EnCartelera")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaEstreno")
                        .HasColumnType("date");

                    b.Property<string>("PosterURL")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Peliculas");
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.PeliculaActor", b =>
                {
                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<string>("Personaje")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("PeliculaId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("PeliculasActores");
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.SalaDeCine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CineId")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("TipoSalaDeCine")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("CineId");

                    b.ToTable("SalasDeCine");
                });

            modelBuilder.Entity("GeneroPelicula", b =>
                {
                    b.Property<int>("GenerosIdentificador")
                        .HasColumnType("int");

                    b.Property<int>("PeliculasId")
                        .HasColumnType("int");

                    b.HasKey("GenerosIdentificador", "PeliculasId");

                    b.HasIndex("PeliculasId");

                    b.ToTable("GeneroPelicula");
                });

            modelBuilder.Entity("PeliculaSalaDeCine", b =>
                {
                    b.Property<int>("PeliculasId")
                        .HasColumnType("int");

                    b.Property<int>("SalasDeCineId")
                        .HasColumnType("int");

                    b.HasKey("PeliculasId", "SalasDeCineId");

                    b.HasIndex("SalasDeCineId");

                    b.ToTable("PeliculaSalaDeCine");
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.CineOferta", b =>
                {
                    b.HasOne("EFCorePeliculasApi.Entidades.Cine", null)
                        .WithOne("CineOferta")
                        .HasForeignKey("EFCorePeliculasApi.Entidades.CineOferta", "CineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.PeliculaActor", b =>
                {
                    b.HasOne("EFCorePeliculasApi.Entidades.Actor", "Actor")
                        .WithMany("PeliculasActores")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCorePeliculasApi.Entidades.Pelicula", "Pelicula")
                        .WithMany("PeliculasActores")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Pelicula");
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.SalaDeCine", b =>
                {
                    b.HasOne("EFCorePeliculasApi.Entidades.Cine", "Cine")
                        .WithMany("SalasDeCine")
                        .HasForeignKey("CineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cine");
                });

            modelBuilder.Entity("GeneroPelicula", b =>
                {
                    b.HasOne("EFCorePeliculasApi.Entidades.Genero", null)
                        .WithMany()
                        .HasForeignKey("GenerosIdentificador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCorePeliculasApi.Entidades.Pelicula", null)
                        .WithMany()
                        .HasForeignKey("PeliculasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PeliculaSalaDeCine", b =>
                {
                    b.HasOne("EFCorePeliculasApi.Entidades.Pelicula", null)
                        .WithMany()
                        .HasForeignKey("PeliculasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCorePeliculasApi.Entidades.SalaDeCine", null)
                        .WithMany()
                        .HasForeignKey("SalasDeCineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.Actor", b =>
                {
                    b.Navigation("PeliculasActores");
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.Cine", b =>
                {
                    b.Navigation("CineOferta")
                        .IsRequired();

                    b.Navigation("SalasDeCine");
                });

            modelBuilder.Entity("EFCorePeliculasApi.Entidades.Pelicula", b =>
                {
                    b.Navigation("PeliculasActores");
                });
#pragma warning restore 612, 618
        }
    }
}