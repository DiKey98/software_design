﻿using HotelServicesLib;

namespace ConsoleTest.Operations
{
    public class InMemoryServiceOperations: IServicesOperations
    {
        private static InMemoryServiceOperations _serviceOperations;

        private readonly IServiceInfoContainer _servicesContainer;

        private InMemoryServiceOperations(IServiceInfoContainer servicesContainer)
        {
            _servicesContainer = servicesContainer;
        }

        public static InMemoryServiceOperations GetInstance(IServiceInfoContainer servicesContainer)
        {
            return _serviceOperations ?? (_serviceOperations = new InMemoryServiceOperations(servicesContainer));
        }

        public void ChangeServiceInfo(ServiceInfo oldService, ServiceInfo newService)
        {
            var tmpService = _servicesContainer.GetServiceInfoById(oldService.Id);
            if (tmpService == null)
            {
                return;
            }
            _servicesContainer.RemoveServiceInfo(tmpService);
            _servicesContainer.AddServiceInfo(newService);
        }
    }
}