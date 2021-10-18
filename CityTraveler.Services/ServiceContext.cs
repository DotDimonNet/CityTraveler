using CityTraveler.Infrastructure.Interfaces;
using CityTraveler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class ServiceContext : IServiceContext
    {
        public IEnumerable<IServiceMetadata> Services { get; set; } = new List<IServiceMetadata>();

        public IServiceMetadata GetService(string serviceName)
        {
            return Services.FirstOrDefault(x =>x.Title == serviceName);
        }

        public void SetServices(IEnumerable<IServiceMetadata> services)
        {
              Services = services;
        }
    }
}
