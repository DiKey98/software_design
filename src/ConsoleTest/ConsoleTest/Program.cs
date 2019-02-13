using System.Collections.Generic;
using ConsoleTest.Containers;
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

            var ordersContainer = InMemoryOrdersContainer.GetInstance();
            var usersContainer = InMemoryUsersContainer.GetInstance(users);
            var servicesContainer = InMemoryServicesContainer.GetInstance(availableServices);

            var userOperations = InMemoryUserOperations.GetInstance(usersContainer, ordersContainer, servicesContainer);
            var servicesOperations = InMemoryServiceOperations.GetInstance(servicesContainer);

            var mainMenu = new Menu();
            var clientMenu = new Menu();
            var managerMenu = new Menu();
            var adminMenu = new Menu();

            mainMenu.AddCommand(new EnterCommand("Вход", usersContainer, new []{adminMenu, managerMenu, clientMenu}));
            mainMenu.AddCommand(new RegistrationCommand("Регистрация", usersContainer, mainMenu));

            clientMenu.AddCommand(new OrderServiceCommand("Заказать услугу", servicesContainer, userOperations, clientMenu));
            clientMenu.AddCommand(new CancelOrderCommand("Отменить заказ", userOperations, ordersContainer, clientMenu));
            clientMenu.AddCommand(new PayServiceCommand("Оплатить услугу", userOperations, ordersContainer, clientMenu));
            clientMenu.AddCommand(new ExitCommand("Выход", mainMenu));

            managerMenu.AddCommand(new ViewAllServicesCommand("Посмотреть все услуги", ordersContainer, managerMenu));
            managerMenu.AddCommand(new ViewAllPaidServicesCommand("Посмотреть оплаченные услуги", ordersContainer, managerMenu));
            managerMenu.AddCommand(new ChangeServiceCommand("Изменить услугу", servicesOperations, servicesContainer, managerMenu));
            managerMenu.AddCommand(new ExitCommand("Выход", mainMenu));

            adminMenu.AddCommand(new ViewAllServicesCommand("Посмотреть все услуги", ordersContainer, adminMenu));
            adminMenu.AddCommand(new ViewAllPaidServicesCommand("Посмотреть оплаченные услуги", ordersContainer, adminMenu));
            adminMenu.AddCommand(new ViewAllUnpaidServicesCommand("Посмотреть все неоплаченные услуги", ordersContainer, adminMenu));
            adminMenu.AddCommand(new ExitCommand("Выход", mainMenu));

            mainMenu.Print();
            mainMenu.SetCommand(mainMenu.ReadCommand());
            mainMenu.Run();
        }
    }
}
