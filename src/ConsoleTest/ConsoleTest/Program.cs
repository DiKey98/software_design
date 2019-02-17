using System.Collections.Generic;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using ConsoleTest.Containers;
using ConsoleTest.Logging;
using ConsoleTest.Operations;
using ConsoleTest.UI;
using ConsoleTest.UI.Commands;
using HotelServicesLib;

namespace ConsoleTest
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var users = new List<User>
            {
                new User("Петров П.П", "petr", "11111", Roles.RolesValues.Client),
                new User("Смирнов П.П", "smr", "22222", Roles.RolesValues.Admin),
                new User("Симонов П.П", "simon", "33333", Roles.RolesValues.Manager),
                new User("Иванов П.П", "iva", "44444", Roles.RolesValues.Client),
                new User("Сидоров П.П", "sidor", "55555", Roles.RolesValues.Client),
                new User("Логинов П.П", "loga", "66666", Roles.RolesValues.Client),
            };

            var availableServices = new List<ServiceInfo>
            {
                new ServiceInfo("Спа", 1000, "час"),
                new ServiceInfo("Бильярд восьмёрка", 2000, "час"),
                new ServiceInfo("Бильярд девятка", 2000, "час"),
                new ServiceInfo("Русский бильярд", 2000, "час"),
            };


            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Register(Component.For<ConsoleLoggerInterceptor>().LifeStyle.Singleton.Named("consoleLogger"));

            container.Register(Component
                .For<IEnumerable<User>>()
                .Instance(users)
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

            var ordersContainer = container.Resolve<IOrdersContainer>();
            var usersContainer = container.Resolve<IUsersContainer>();
            var servicesContainer = container.Resolve<IServiceInfoContainer>();

            container.Register(
                Component.For<IUsersOperations>()
                .ImplementedBy<InMemoryUserOperations>()
                .Interceptors(InterceptorReference.ForKey("consoleLogger")).Anywhere
                .LifestyleSingleton());

            container.Register(
                Component.For<IServicesOperations>()
                    .ImplementedBy<InMemoryServiceOperations>()
                    .Interceptors(InterceptorReference.ForKey("consoleLogger")).Anywhere
                    .LifestyleSingleton());

            var userOperations = container.Resolve<IUsersOperations>();
            var servicesOperations = container.Resolve<IServicesOperations>();


            var mainMenu = new Menu();
            var clientMenu = new Menu();
            var managerMenu = new Menu();
            var adminMenu = new Menu();
        
            mainMenu.AddCommand(new EnterCommand("Вход", usersContainer, new[] { adminMenu, managerMenu, clientMenu }));
            mainMenu.AddCommand(new RegistrationCommand("Регистрация", usersContainer, mainMenu));

            clientMenu.AddCommand(new OrderServiceCommand("Заказать услугу", servicesContainer, userOperations, clientMenu));
            clientMenu.AddCommand(new CancelOrderCommand("Отменить заказ", userOperations, ordersContainer, clientMenu));
            clientMenu.AddCommand(new PayServiceCommand("Оплатить услугу", userOperations, ordersContainer, clientMenu));
            clientMenu.AddCommand(new ExitCommand("Выход", mainMenu));

            managerMenu.AddCommand(new ViewAllServicesCommand("Посмотреть все услуги", servicesContainer, managerMenu));
            managerMenu.AddCommand(new ViewAllPaidServicesCommand("Посмотреть оплаченные услуги", ordersContainer, managerMenu));
            managerMenu.AddCommand(new ChangeServiceCommand("Изменить услугу", servicesOperations, servicesContainer, managerMenu));
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
