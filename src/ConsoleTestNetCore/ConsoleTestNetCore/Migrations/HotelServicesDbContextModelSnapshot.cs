﻿// <auto-generated />
using System;
using ConsoleTestNetCore.Containers.InDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ConsoleTestNetCore.Migrations
{
    [DbContext(typeof(HotelServicesDbContext))]
    partial class HotelServicesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("HotelServicesNetCore.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Cost");

                    b.Property<bool>("IsPaid");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("ServiceId");

                    b.Property<long>("Units");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("HotelServicesNetCore.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "7b5aead8-6888-4a95-a7bb-dc64a350292e",
                            Name = "Клиент"
                        },
                        new
                        {
                            Id = "37b8175c-6f09-4fbe-b5b7-afcd3d3285b9",
                            Name = "Администратор"
                        },
                        new
                        {
                            Id = "0e59f4f5-0c6a-4a8c-a2e9-77545924ce61",
                            Name = "Управляющий"
                        });
                });

            modelBuilder.Entity("HotelServicesNetCore.ServiceInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("CostPerUnit");

                    b.Property<string>("ImgSrc");

                    b.Property<bool>("IsDeprecated");

                    b.Property<string>("Measurement");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ServiceInfos");

                    b.HasData(
                        new
                        {
                            Id = "bdc0f864-3cf2-4ee9-8ef2-ad8bc7790b36",
                            CostPerUnit = 1000m,
                            IsDeprecated = false,
                            Measurement = "час.",
                            Name = "Спа"
                        },
                        new
                        {
                            Id = "33b364c3-f54e-49e1-bac2-f7775e65c978",
                            CostPerUnit = 2000m,
                            IsDeprecated = false,
                            Measurement = "час",
                            Name = "Бильярд восьмёрка"
                        },
                        new
                        {
                            Id = "5a36076e-6e72-4199-a504-7324e79deb01",
                            CostPerUnit = 2000m,
                            IsDeprecated = false,
                            Measurement = "час",
                            Name = "Бильярд девятка"
                        },
                        new
                        {
                            Id = "f7f1b91e-f932-4c79-b95c-a86fd90ed5ed",
                            CostPerUnit = 2000m,
                            IsDeprecated = false,
                            Measurement = "час",
                            Name = "Русский бильярд"
                        });
                });

            modelBuilder.Entity("HotelServicesNetCore.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Fio");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.Property<string>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "275925f4-4def-40ee-ad6a-35fd1c94810c",
                            Fio = "Петров П.П",
                            Login = "petr",
                            Password = "11111",
                            RoleId = "7b5aead8-6888-4a95-a7bb-dc64a350292e"
                        },
                        new
                        {
                            Id = "083593ae-b82b-494c-a215-f02b7af5781a",
                            Fio = "Смирнов П.П",
                            Login = "smr",
                            Password = "22222",
                            RoleId = "37b8175c-6f09-4fbe-b5b7-afcd3d3285b9"
                        },
                        new
                        {
                            Id = "add73c8d-801a-4242-bae0-31dd9351ed34",
                            Fio = "Симонов П.П",
                            Login = "simon",
                            Password = "33333",
                            RoleId = "0e59f4f5-0c6a-4a8c-a2e9-77545924ce61"
                        },
                        new
                        {
                            Id = "7f8b2a30-8a78-4b81-99fc-677c105fd68b",
                            Fio = "Иванов П.П",
                            Login = "iva",
                            Password = "44444",
                            RoleId = "7b5aead8-6888-4a95-a7bb-dc64a350292e"
                        },
                        new
                        {
                            Id = "25c9c630-1b06-4951-aeb5-3c1d9dca3e62",
                            Fio = "Сидоров П.П",
                            Login = "sidor",
                            Password = "55555",
                            RoleId = "7b5aead8-6888-4a95-a7bb-dc64a350292e"
                        });
                });

            modelBuilder.Entity("HotelServicesNetCore.Order", b =>
                {
                    b.HasOne("HotelServicesNetCore.ServiceInfo", "Service")
                        .WithMany("Orders")
                        .HasForeignKey("ServiceId");

                    b.HasOne("HotelServicesNetCore.User", "User")
                        .WithMany("Order")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HotelServicesNetCore.User", b =>
                {
                    b.HasOne("HotelServicesNetCore.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}
