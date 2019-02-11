using System;
using System.Collections.Generic;
using ConsoleTest.MenuBuild;
using HotelServicesLib;

namespace ConsoleTest.Services
{
    public class ServicesOptions
    {
        public static Dictionary<string, Func<IService>> ServicesInputs;
        public static Dictionary<string, decimal> ServicesCosts;

        public static Func<IService> GetSpaService = () => 
            new SpaService("Спа", ServicesCosts["спа"], false, Menu.CurrentUser, DateTime.Now);

        public static Func<IService> GetBilliards = delegate
        {
            Console.Clear();
            Console.Write("Количество часов: ");
            var hours = int.Parse(Console.ReadLine());
            return new Billiards("Бильярд", ServicesCosts["бильярд"], false, 
                Menu.CurrentUser, DateTime.Now, new TimeSpan(0, hours, 0, 0));
        };

        public static Func<IService> GetAlcohol = delegate
        {
            Console.Clear();
            Console.Write("Количество бутылок: ");
            var count = uint.Parse(Console.ReadLine());
            Console.Write("Литров в бутылке: ");
            var liters = decimal.Parse(Console.ReadLine());
            return new Alcohol("Алкоголь", ServicesCosts["алкоголь"], false,
                Menu.CurrentUser, DateTime.Now, liters, count);
        };
    }
}