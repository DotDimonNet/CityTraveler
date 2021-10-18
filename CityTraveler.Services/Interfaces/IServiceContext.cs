using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IServiceContext
    { 
        public void SetServices(IEnumerable<IServiceMetadata> services);       
        public IServiceMetadata GetService(string serviceName);
    }   
}

