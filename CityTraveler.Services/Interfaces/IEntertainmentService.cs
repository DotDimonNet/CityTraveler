using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IEntertainmentService : IServiceMetadata
    {
        public Task<bool> AddEntertainment(EntertaimentModel entertaiment);
        public Task<bool> SetEntertaiment(IEnumerable<EntertaimentModel> entertaiments);
        public Task<bool> UpdateEntertainment(EntertaimentModel entertaiment);
        public Task<bool> RemoveEntertainment(Guid id);
        public IEnumerable<EntertaimentModel> GetAll();
        public IEnumerable<EntertaimentModel> GetEntertainments(IEnumerable<Guid> guids);
        public Task<EntertaimentModel> GetEntertainmentById(Guid guids);
        public Task<EntertaimentModel> GetEntertainmentByTitle(string title);
        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(StreetModel street);
        public Task<EntertaimentModel> GetEntertainmentByCoordinates(CoordinatesModel coordinates);
        public Task<EntertaimentModel> GetEntertainmentByAddress(AddressModel address);
        public IEnumerable<EntertaimentModel> GetEntartainmentByMinRating(int averageRating);
        public IEnumerable<EntertaimentModel> GetEntartainmentByMaxRating(int averageRating);

    }
}
