using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;

namespace CityTraveler.Services.Interfaces
{
    public interface ILandscapeService : IServiceMetadata
    {
        public IEnumerable<EntertaimentModel> GetAll();
        public IEnumerable<EntertaimentModel> GetLandscapes(IEnumerable<Guid> guids);
        public Task<EntertaimentModel> GetLandscapeById(Guid guids);
        public IEnumerable<EntertaimentModel> GetLandscapeByTitle(string title);
        public IEnumerable<EntertaimentModel> GetLandscapesByStreet(StreetModel street);
        public IEnumerable<EntertaimentModel> GetLandscapesByStreet(string streetTitle);
        public Task<EntertaimentModel> GetLandscapeByCoordinates(CoordinatesModel coordinates);
        public Task<EntertaimentModel> GetLandscapeByAddress(AddressModel address);
        public Task<EntertaimentModel> GetLandscapeByAddress(string houseNumber, string apartmentNumber, string streetTitle);
    }
}
