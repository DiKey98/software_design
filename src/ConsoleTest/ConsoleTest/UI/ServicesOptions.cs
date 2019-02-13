using System;
using System.Collections.Generic;
using HotelServicesLib;

namespace ConsoleTest.UI
{
    public class ServicesOptions
    {
        public static Dictionary<string, Func<ServiceInfo>> ServicesInputs;
        public static Dictionary<string, decimal> ServicesCosts;

        public static Func<ServiceInfo> GetSpaService = () =>
            new ServiceInfo("Спа", ServicesCosts["спа"], "час.");

        public static Func<ServiceInfo> GetBilliards = delegate
        {
            Console.Clear();
            Console.Write("Количество часов: ");
            var hours = int.Parse(Console.ReadLine());
            return new Billiards("Бильярд", ServicesCosts["бильярд"], false, 
                Menu.CurrentUser, DateTime.Now, new TimeSpan(0, hours, 0, 0));
        };

        public static Func<ServiceInfo> GetAlcohol = delegate
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