﻿using System;

namespace HotelServicesLib
{
    public class ServiceInfo
    {
        public ServiceInfo(string name, decimal cost, string measurement)
        {
            Name = name;
            CostPerUnit = cost;
            Measurement = measurement;
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }
        public string Name { get; }
        public decimal CostPerUnit { get; }
        public string Measurement { get; }
    }
}