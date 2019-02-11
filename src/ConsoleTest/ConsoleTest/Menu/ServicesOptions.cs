using System;
using System.Collections.Generic;
using ConsoleTest.Services;
using HotelServicesLib;

namespace ConsoleTest.Menu
{
    public class ServicesOptions
    {
        public static readonly Dictionary<string, IService> ServicesInputs =
            new Dictionary<string, IService>
            {
                {"спа", GetSpaService()}
            };

        public static readonly Dictionary<string, decimal> ServicesCosts =
            new Dictionary<string, decimal>
            {
                {"спа", 3000}
            };

        public static IService GetSpaService()
        {
            return new SpaService("Спа", ServicesCosts["спа"], false, Menu.CurrentUser, DateTime.Now);
        }
    }
}