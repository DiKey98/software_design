using System;
using System.Collections.Generic;
using ConsoleTest.UI;
using HotelServicesLib;

namespace ConsoleTestNetCore.UI.Commands
{
    public class CancelOrderCommand: ICommand
    {
        private readonly IOrdersContainer _ordersContainer;
        private readonly IUsersOperations _userOperations;
        private readonly Menu _clientMenu;

        public string Name { get; }
        
        public CancelOrderCommand(string name, IUsersOperations userOperations, IOrdersContainer ordersContainer, Menu clientMenu)
        {
            Name = name;
            _ordersContainer = ordersContainer;
            _clientMenu = clientMenu;
            _userOperations = userOperations;
        }

        public void Execute()
        {
            Console.WriteLine("Заказанные услуги:");
            Console.WriteLine();
            PrintServices(_ordersContainer.GetOrders(Menu.CurrentUser, false));
            Console.WriteLine();
            Console.Write("Введите идентификатор заказа: ");
            var idOrder = Console.ReadLine();
            var order = _ordersContainer.GetOrderById(idOrder);
            if (order == null)
            {
                Refresh("Неверный id заказа");
                return;
            }
            if (order.IsPaid)
            {
                Refresh("Заказ уже оплачен");
                return;
            }
            _userOperations.CancelService(Menu.CurrentUser, order.Id);
            Console.Clear();
            Console.WriteLine("Услуга успешно отменена");
            Console.WriteLine();
            Console.WriteLine();
            _clientMenu.Print();
            _clientMenu.SetCommand(_clientMenu.ReadCommand());
            _clientMenu.Run();
        }

        private void Refresh(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine();
            Execute();
        }

        private void PrintServices(ICollection<Order> orders)
        {
            if (orders.Count == 0)
            {
                Console.WriteLine("Нет заказанных услуг. Нажмите любую клавишу...");
                Console.ReadKey(false);
                Console.Clear();
                _clientMenu.Print();
                _clientMenu.SetCommand(_clientMenu.ReadCommand());
                _clientMenu.Run();
                return;
            }
            foreach (var order in orders)
            {
                Console.WriteLine($"Заказ {order.Id} с услуги {order.Service.Name}");
            }
        }
    }
}