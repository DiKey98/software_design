using System;
using HotelServicesNetCore;
using Microsoft.EntityFrameworkCore;

namespace WebServer.Models
{
    public class HotelServicesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ServiceInfo> ServiceInfos { get; set; }
        public DbSet<Authorization> Authorizations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hotel_test;Username=postgres;Password=12345");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roles = new[]
            {
                new Role { Id = Guid.NewGuid().ToString(), Name = "Клиент"},
                new Role { Id = Guid.NewGuid().ToString(), Name = "Администратор"},
                new Role { Id = Guid.NewGuid().ToString(), Name = "Управляющий"},
            };

            modelBuilder.Entity<Role>().HasData(roles);

            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.NewGuid().ToString(), Fio = "Петров П.П", Login = "petr", Password = "11111", RoleId = roles[0].Id},
                new User { Id = Guid.NewGuid().ToString(), Fio = "Смирнов П.П", Login = "smr", Password = "22222", RoleId = roles[1].Id },
                new User { Id = Guid.NewGuid().ToString(), Fio = "Симонов П.П", Login = "simon", Password = "33333", RoleId = roles[2].Id },
                new User { Id = Guid.NewGuid().ToString(), Fio = "Иванов П.П", Login = "iva", Password = "44444", RoleId = roles[0].Id },
                new User { Id = Guid.NewGuid().ToString(), Fio = "Сидоров П.П", Login = "sidor", Password = "55555", RoleId = roles[0].Id }
                );

            modelBuilder.Entity<ServiceInfo>().HasData(
                new ServiceInfo { Id = Guid.NewGuid().ToString(), Name = "Спа", CostPerUnit = 1000,
                    Measurement = "час.", IsDeprecated = false,
                    ImgSrc = "~/images/services/spa.png" }, 

                new ServiceInfo { Id = Guid.NewGuid().ToString(), Name = "Бильярд восьмёрка", CostPerUnit = 2000,

                    Measurement = "час", IsDeprecated = false,
                    ImgSrc = "~/images/services/eight.png"}, 

                new ServiceInfo { Id = Guid.NewGuid().ToString(), Name = "Бильярд девятка", CostPerUnit = 2000,
                    Measurement = "час", IsDeprecated = false,
                    ImgSrc = "~/images/services/nine.png"
                }, 

                new ServiceInfo { Id = Guid.NewGuid().ToString(), Name = "Русский бильярд", CostPerUnit = 2000,
                    Measurement = "час", IsDeprecated = false,
                    ImgSrc = "~/images/services/russian_billiards.png"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}