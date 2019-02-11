using System;
using System.Collections.Generic;
using ConsoleTest.Containers;
using ConsoleTest.Containers.ConsoleTest;
using ConsoleTest.MenuBuild;
using ConsoleTest.MenuBuild.Commands;
using ConsoleTest.Operations;
using ConsoleTest.Services;
using ConsoleTest.ServicesInfo;
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

            ServicesOptions.ServicesInputs = new Dictionary<string, Func<IService>>
            {
                {"спаа", ServicesOptions.GetSpaService}
            };

            ServicesOptions.ServicesCosts = new Dictionary<string, decimal>
            {
                {"спаа", 3000},
                {"спаб", 3000},
                {"спав", 3000},
                {"спаг", 3000}
            };

            var availableServices = new List<IServiceInfo>
            {
                new SpaServiceInfo("Спаа", ServicesOptions.ServicesCosts["спаа"]),
                new SpaServiceInfo("Спаб", ServicesOptions.ServicesCosts["спаа"]),
                new SpaServiceInfo("Спав", ServicesOptions.ServicesCosts["спаа"]),
                new SpaServiceInfo("Спаг", ServicesOptions.ServicesCosts["спаа"]),
            };

            var usersContainer = InMemoryUsersContainer.GetInstance(users);
            var servicesContainer = InMemoryServicesContainer.GetInstance(availableServices);
            var userOperations = InMemoryUserOperations.GetInstance(usersContainer, servicesContainer);

            var mainMenu = new Menu();
            var clientMenu = new Menu();
            var managerMenu = new Menu();
            var adminMenu = new Menu();

            mainMenu.AddCommand(new EnterCommand("Вход", usersContainer, new []{adminMenu, managerMenu, clientMenu}));
            mainMenu.AddCommand(new RegistrationCommand("Регистрация", usersContainer, mainMenu));
            mainMenu.AddCommand(new ExitCommand("Выход"));

            clientMenu.AddCommand(new OrderServiceCommand("Заказать услугу", servicesContainer, userOperations, clientMenu));
            clientMenu.AddCommand(new CancelServiceCommand("Отменить заказ", userOperations, servicesContainer, clientMenu));
            clientMenu.AddCommand(new PayServiceCommand("Оплатить услугу", userOperations, servicesContainer, clientMenu));
            clientMenu.AddCommand(new ExitCommand("Выход"));

            managerMenu.AddCommand(new ViewAllServicesCommand("Посмотреть все услуги", servicesContainer));
            managerMenu.AddCommand(new ViewAllPaidServicesCommand("Посмотреть оплаченные услуги", servicesContainer));
            managerMenu.AddCommand(new ExitCommand("Выход"));

            adminMenu.AddCommand(new ViewAllUnpaidServicesCommand("Посмотреть все неоплаченные услуги", servicesContainer));
            adminMenu.AddCommand(new ExitCommand("Выход"));

            mainMenu.Print();
            mainMenu.SetCommand(mainMenu.ReadCommand());
            mainMenu.Run();
        }
    }
}
