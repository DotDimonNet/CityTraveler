using CityTraveler.Infrastructure.Enums;
using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ICityArchitectureService : IServiceMetadata
    {
        public Task<bool> AddObjectToCity(IInstitution institution);
        public Task<bool> AddObjectToCity(IEvent ev);
        public Task<bool> AddObjectToCity(ILandskape landskape);
        public Task<bool> UpdateCityObject(IInstitution institution);
        public Task<bool> UpdateCityObject(IEvent ev);
        public Task<bool> UpdateCityObject(ILandskape landskape);
        public Task<bool> RemoveObjectById(Guid objectId, PlaceType type);
        public Task<bool> AddStreet(IStreet street);
        public Task<bool> UpdateStreet(IStreet street);
        public Task<bool> RemoveStreet(Guid streetId);
        public Task<bool> BuildCityMap();
        public Task<bool> ValidateCityMap();
    }   
}

