using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface ICityArchitectureService : IServiceMetadata
    {
        public IEnumerable<IEntertaiment> Entertaiments { get; set; }
        public IEnumerable<IEvent> Events { get; set; }
        public IEnumerable<ILandskape> Landskapes { get; set; }
        public bool AddObjectToCity(IEntertaiment entertaiment);
        public bool AddObjectToCity(IEvent ev);
        public bool AddObjectToCity(ILandskape landskape);
        public bool RemoveObjectById(Guid objectId);
        public bool AddStreet(IStreet street);
        public bool UpdateStreet(IStreet street);
        public bool RemoveStreet(Guid streetId);
        public bool UpdateEntertaiment(IEntertaiment entertaiment);
        public bool ValidateCityMap();
    }   
}

