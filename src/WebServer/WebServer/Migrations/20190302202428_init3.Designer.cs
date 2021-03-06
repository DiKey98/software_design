﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebServer.Models;

namespace WebServer.Migrations
{
    [DbContext(typeof(HotelServicesDbContext))]
    [Migration("20190302202428_init3")]
    partial class init3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("HotelServicesNetCore.Authorization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SessionId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Authorizations");
                });

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
                        new { Id = "83679d65-1376-47a2-849a-2067a1827e1f", Name = "Клиент" },
                        new { Id = "071a6fae-5c71-41e4-838e-fe4dd5caa74e", Name = "Администратор" },
                        new { Id = "0a32f7e4-6c54-4224-b01b-87c3766a2f04", Name = "Управляющий" }
                    );
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
                        new { Id = "482cf7e2-9173-4b82-8256-198d0d94a15a", CostPerUnit = 1000m, ImgSrc = "~/images/services/spa.png", IsDeprecated = false, Measurement = "час.", Name = "Спа" },
                        new { Id = "bea244f6-846b-43da-bb15-3bdd8bbc551c", CostPerUnit = 2000m, ImgSrc = "~/images/services/eight.png", IsDeprecated = false, Measurement = "час", Name = "Бильярд восьмёрка" },
                        new { Id = "24eba2e4-b3d1-44e1-8ec4-6d3f702bea43", CostPerUnit = 2000m, ImgSrc = "~/images/services/nine.png", IsDeprecated = false, Measurement = "час", Name = "Бильярд девятка" },
                        new { Id = "8da23433-2dc0-4f99-9a05-c76dc01bad4b", CostPerUnit = 2000m, ImgSrc = "~/images/services/russian_billiards.png", IsDeprecated = false, Measurement = "час", Name = "Русский бильярд" }
                    );
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
                        new { Id = "89441ea2-af34-4ce7-a962-794cd72691c7", Fio = "Петров П.П", Login = "petr", Password = "11111", RoleId = "83679d65-1376-47a2-849a-2067a1827e1f" },
                        new { Id = "966afb7d-56c6-427d-9dd1-bb8f21cb1b59", Fio = "Смирнов П.П", Login = "smr", Password = "22222", RoleId = "071a6fae-5c71-41e4-838e-fe4dd5caa74e" },
                        new { Id = "d7bbc2e5-c93f-4940-a1c4-6d799a4cad1c", Fio = "Симонов П.П", Login = "simon", Password = "33333", RoleId = "0a32f7e4-6c54-4224-b01b-87c3766a2f04" },
                        new { Id = "668e9aa2-e708-45fd-b349-6afcbaf3845c", Fio = "Иванов П.П", Login = "iva", Password = "44444", RoleId = "83679d65-1376-47a2-849a-2067a1827e1f" },
                        new { Id = "d7564ecd-0b66-45ac-9b4c-baa6582fe110", Fio = "Сидоров П.П", Login = "sidor", Password = "55555", RoleId = "83679d65-1376-47a2-849a-2067a1827e1f" }
                    );
                });

            modelBuilder.Entity("HotelServicesNetCore.Authorization", b =>
                {
                    b.HasOne("HotelServicesNetCore.User", "User")
                        .WithOne("Authorization")
                        .HasForeignKey("HotelServicesNetCore.Authorization", "UserId");
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
