using System;
using System.Collections.Generic;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using ConsoleTestNetCore.Containers.InMemory;
using ConsoleTestNetCore.Logging;
using ConsoleTestNetCore.Operations;
using ConsoleTestNetCore.UI;
using ConsoleTestNetCore.UI.Commands;
using HotelServicesNetCore;

namespace ConsoleTestNetCore
{
    public class Program
    {
        private static void Main1(string[] args)
        {
            var roles = new[]
            {
                new Role { Name = "Администратор"},
                new Role { Name = "Управляющий"},
                new Role { Name = "Клиент"},
            };

            var users = new List<User>
                {
                    new User { Id = Guid.NewGuid().ToString(), Fio = "Петров П.П", Login = "petr", Password = "11111", Role = roles[2] },
                    new User { Id = Guid.NewGuid().ToString(), Fio = "Смирнов П.П", Login = "smr", Password = "22222", Role = roles[0] },
                    new User { Id = Guid.NewGuid().ToString(), Fio = "Симонов П.П", Login = "simon", Password = "33333", Role = roles[1] },
                    new User { Id = Guid.NewGuid().ToString(), Fio = "Иванов П.П", Login = "iva", Password = "44444", Role = roles[2] },
                    new User { Id = Guid.NewGuid().ToString(), Fio = "Сидоров П.П", Login = "sidor", Password = "55555", Role = roles[2] },
                };

            var availableServices = new List<ServiceInfo>
                {
                    new ServiceInfo { Id = Guid.NewGuid().ToString(), Name = "Спа", CostPerUnit = 1000,
                        Measurement = "час.", IsDeprecated = false},
                    new ServiceInfo { Id = Guid.NewGuid().ToString(), Name = "Бильярд восьмёрка", CostPerUnit = 2000,
                        Measurement = "час", IsDeprecated = false },
                    new ServiceInfo { Id = Guid.NewGuid().ToString(), Name = "Бильярд девятка", CostPerUnit = 2000,
                        Measurement = "час", IsDeprecated = false },
                    new ServiceInfo { Id = Guid.NewGuid().ToString(), Name = "Русский бильярд", CostPerUnit = 2000,
                        Measurement = "час", IsDeprecated = false }
                };

            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Register(Component.For<ConsoleLoggerInterceptor>().LifeStyle.Transient.Named("consoleLogger"));

            container.Register(Component
                .For<IEnumerable<User>>()
                .Instance(users)
                .LifestyleSingleton());

            container.Register(Component
                .For<IEnumerable<Role>>()
                .Instance(roles)
                .LifestyleSingleton());

            container.Register(Component
                .For<IEnumerable<ServiceInfo>>()
                .Instance(availableServices)
                .LifestyleSingleton());

            container.Register(
                Component.For<IUsersContainer>()
                    .ImplementedBy<InMemoryUsersContainer>()
                    .LifestyleSingleton());

            container.Register(
                Component.For<IOrdersContainer>()
                    .ImplementedBy<InMemoryOrdersContainer>()
                    .LifestyleSingleton());

            container.Register(
                Component.For<IServiceInfoContainer>()
                    .ImplementedBy<InMemoryServicesContainer>()
                    .LifestyleSingleton());

            container.Register(
                Component.For<IRolesContainer>()
                    .ImplementedBy<InMemoryRolesContainer>()
                    .LifestyleSingleton());

            var ordersContainer = container.Resolve<IOrdersContainer>();
            var usersContainer = container.Resolve<IUsersContainer>();
            var servicesContainer = container.Resolve<IServiceInfoContainer>();
            var rolesContainer = container.Resolve<IRolesContainer>();

            container.Register(
                Component.For<IUsersOperations>()
                            .ImplementedBy<UserOperations>()
                            .Interceptors(InterceptorReference.ForKey("consoleLogger")).Anywhere
                            .LifestyleSingleton());

            container.Register(
                Component.For<IServicesOperations>()
                    .ImplementedBy<ServiceOperations>()
                    .Interceptors(InterceptorReference.ForKey("consoleLogger")).Anywhere
                    .LifestyleSingleton());

            var userOperations = container.Resolve<IUsersOperations>();
            var servicesOperations = container.Resolve<IServicesOperations>();


            var mainMenu = new Menu();
            var clientMenu = new Menu();
            var managerMenu = new Menu();
            var adminMenu = new Menu();

            mainMenu.AddCommand(new EnterCommand("Вход", usersContainer, new[] { adminMenu, managerMenu, clientMenu }));
            mainMenu.AddCommand(new RegistrationCommand("Регистрация", usersContainer, rolesContainer, mainMenu));

            clientMenu.AddCommand(new OrderServiceCommand("Заказать услугу", servicesContainer, userOperations, clientMenu));
            clientMenu.AddCommand(new CancelOrderCommand("Отменить заказ", userOperations, ordersContainer, clientMenu));
            clientMenu.AddCommand(new PayServiceCommand("Оплатить услугу", userOperations, ordersContainer, clientMenu));
            clientMenu.AddCommand(new ExitCommand("Выход", mainMenu));

            managerMenu.AddCommand(new ViewAllServicesCommand("Посмотреть все услуги", servicesContainer, managerMenu));
            managerMenu.AddCommand(new ViewAllPaidServicesCommand("Посмотреть оплаченные услуги", ordersContainer, managerMenu));
            managerMenu.AddCommand(new ChangeServiceCommand("Изменить услугу", servicesOperations, servicesContainer, managerMenu));
            managerMenu.AddCommand(new ViewAttendanceStatiscticsCommand("Статистика заказов", ordersContainer, managerMenu));
            managerMenu.AddCommand(new ExitCommand("Выход", mainMenu));

            adminMenu.AddCommand(new ViewAllServicesCommand("Посмотреть все услуги", servicesContainer, adminMenu));
            adminMenu.AddCommand(new ViewAllPaidServicesCommand("Посмотреть оплаченные услуги", ordersContainer, adminMenu));
            adminMenu.AddCommand(new ViewAllUnpaidServicesCommand("Посмотреть все неоплаченные услуги", ordersContainer, adminMenu));
            adminMenu.AddCommand(new ViewAllUsersCommand("Посмотреть всех пользователей", usersContainer, adminMenu));
            adminMenu.AddCommand(new ExitCommand("Выход", mainMenu));

            mainMenu.Print();
            mainMenu.SetCommand(mainMenu.ReadCommand());
            mainMenu.Run();
        }
    }
}
