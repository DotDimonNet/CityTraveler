using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.DTO;

namespace CityTraveler.Services.Interfaces
{
    public interface ILandscapeService : IServiceMetadata
    {
        public IEnumerable<EntertaimentModel> GetAll();
        public IEnumerable<EntertaimentModel> GetLandscapes(IEnumerable<Guid> guids);
        public Task<EntertaimentModel> GetLandscapeById(Guid guids);
        public IEnumerable<EntertaimentModel> GetLandscapesByTitle(string title);
        public IEnumerable<EntertaimentModel> GetLandscapesByStreet(string streetTitle);
        public Task<EntertaimentModel> GetLandscapeByCoordinates(CoordinatesDTO coordinates);
        public Task<EntertaimentModel> GetLandscapeByAddress(AddressDTO address);
    }
}
