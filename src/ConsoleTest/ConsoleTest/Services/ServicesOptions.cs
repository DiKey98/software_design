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
            new SpaService("Спаа", ServicesCosts["спаа"], false, Menu.CurrentUser, DateTime.Now);
    }
}