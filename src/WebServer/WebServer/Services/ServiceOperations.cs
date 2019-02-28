using Castle.Core;
using HotelServicesNetCore;

namespace WebServer.Services
{
    [Interceptor("consoleLogger")]
    public class ServiceOperations: IServicesOperations
    {
        private readonly IServiceInfoContainer _servicesContainer;

        public ServiceOperations(IServiceInfoContainer servicesContainer)
        {
            _servicesContainer = servicesContainer;
        }

        public void ChangeServiceInfo(ServiceInfo oldService, ServiceInfo newService)
        {
            var tmpService = _servicesContainer.GetServiceInfoById(oldService.Id);
            if (tmpService == null)
            {
                return;
            }

            _servicesContainer.UpdateService(oldService, newService);
        }
    }
}