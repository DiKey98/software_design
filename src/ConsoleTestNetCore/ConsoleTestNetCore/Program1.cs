using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using ConsoleTestNetCore.Containers.InDB;
using ConsoleTestNetCore.Logging;
using ConsoleTestNetCore.Operations;
using ConsoleTestNetCore.UI;
using ConsoleTestNetCore.UI.Commands;
using HotelServicesNetCore;

namespace ConsoleTestNetCore
{
    public class Program1
    {
        private static void Main(string[] args)
        {
            using (var db = new HotelServicesDbContext())
            {
                var container = new WindsorContainer();
                container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

                container.Register(Component.For<ConsoleLoggerInterceptor>().LifeStyle.Singleton.Named("consoleLogger"));

                container.Register(
                    Component.For<HotelServicesDbContext>()
                        .Instance(db)
                        .LifestyleSingleton());

                container.Register(
                    Component.For<IUsersContainer>()
                        .ImplementedBy<InDbUsersContainer>()
                        .LifestyleSingleton());

                container.Register(
                    Component.For<IOrdersContainer>()
                        .ImplementedBy<InDbOrdersContainer>()
                        .LifestyleSingleton());

                container.Register(
                    Component.For<IServiceInfoContainer>()
                        .ImplementedBy<InDbServicesContainer>()
                        .LifestyleSingleton());

                container.Register(
                    Component.For<IRolesContainer>()
                        .ImplementedBy<InDbRolesContainer>()
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
}
