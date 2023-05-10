﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ReservaContext))]
    partial class ReservaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Factura", b =>
                {
                    b.Property<int>("FacturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacturaId"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("Monto")
                        .HasColumnType("int");

                    b.Property<int>("PagoId")
                        .HasColumnType("int");

                    b.HasKey("FacturaId");

                    b.HasIndex("PagoId")
                        .IsUnique();

                    b.ToTable("Factura", (string)null);

                    b.HasData(
                        new
                        {
                            FacturaId = 1,
                            Estado = "Paga",
                            Fecha = new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Monto = 2000,
                            PagoId = 1
                        });
                });

            modelBuilder.Entity("Domain.Entities.MetodoPago", b =>
                {
                    b.Property<int>("MetodoPagoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MetodoPagoId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetodoPagoId");

                    b.ToTable("MetodoPago", (string)null);

                    b.HasData(
                        new
                        {
                            MetodoPagoId = 1,
                            Descripcion = "Tarjeta"
                        },
                        new
                        {
                            MetodoPagoId = 2,
                            Descripcion = "Mercado Pago"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Pago", b =>
                {
                    b.Property<int>("PagoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PagoId"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("MetodoPagoId")
                        .HasColumnType("int");

                    b.Property<int>("Monto")
                        .HasColumnType("int");

                    b.Property<int>("ReservaId")
                        .HasColumnType("int");

                    b.HasKey("PagoId");

                    b.HasIndex("MetodoPagoId");

                    b.HasIndex("ReservaId")
                        .IsUnique();

                    b.ToTable("Pago", (string)null);

                    b.HasData(
                        new
                        {
                            PagoId = 1,
                            Fecha = new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            MetodoPagoId = 1,
                            Monto = 2000,
                            ReservaId = 1
                        });
                });

            modelBuilder.Entity("Domain.Entities.Pasaje", b =>
                {
                    b.Property<int>("PasajeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PasajeId"));

                    b.Property<string>("Nota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReservaId")
                        .HasColumnType("int");

                    b.HasKey("PasajeId");

                    b.HasIndex("ReservaId");

                    b.ToTable("Pasaje", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Reserva", b =>
                {
                    b.Property<int>("ReservaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservaId"));

                    b.Property<string>("Clase")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumeroAsiento")
                        .HasColumnType("int");

                    b.Property<int>("PasajeroId")
                        .HasColumnType("int");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ViajeId")
                        .HasColumnType("int");

                    b.HasKey("ReservaId");

                    b.ToTable("Reserva", (string)null);

                    b.HasData(
                        new
                        {
                            ReservaId = 1,
                            Clase = "Alta",
                            Fecha = new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            NumeroAsiento = 4,
                            PasajeroId = 0,
                            Precio = 2000,
                            UsuarioId = new Guid("00000000-0000-0000-0000-000000000000"),
                            ViajeId = 0
                        });
                });

            modelBuilder.Entity("Domain.Entities.Factura", b =>
                {
                    b.HasOne("Domain.Entities.Pago", "Pago")
                        .WithOne("Factura")
                        .HasForeignKey("Domain.Entities.Factura", "PagoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pago");
                });

            modelBuilder.Entity("Domain.Entities.Pago", b =>
                {
                    b.HasOne("Domain.Entities.MetodoPago", "MetodoPago")
                        .WithMany("Pagos")
                        .HasForeignKey("MetodoPagoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Reserva", "Reserva")
                        .WithOne("Pago")
                        .HasForeignKey("Domain.Entities.Pago", "ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MetodoPago");

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("Domain.Entities.Pasaje", b =>
                {
                    b.HasOne("Domain.Entities.Reserva", "Reserva")
                        .WithMany("Pasajes")
                        .HasForeignKey("ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("Domain.Entities.MetodoPago", b =>
                {
                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("Domain.Entities.Pago", b =>
                {
                    b.Navigation("Factura")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Reserva", b =>
                {
                    b.Navigation("Pago")
                        .IsRequired();

                    b.Navigation("Pasajes");
                });
#pragma warning restore 612, 618
        }
    }
}
