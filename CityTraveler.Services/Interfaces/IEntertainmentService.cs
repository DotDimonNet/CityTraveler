using CityTraveler.Domain.Entities;
using CityTraveler.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IEntertainmentService : IServiceMetadata
    {
        public IEnumerable<EntertaimentModel> GetAll();
        public IEnumerable<EntertaimentModel> GetEntertainments(IEnumerable<Guid> guids);
        public Task<EntertaimentModel> GetEntertainmentById(Guid guids);
        public Task<EntertaimentModel> GetEntertainmentByTitle(string title);
        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(StreetModel street);
        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(string streetTitle);
        public Task<EntertaimentModel> GetEntertainmentByCoordinates(CoordinatesModel coordinates);
        public Task<EntertaimentModel> GetEntertainmentByAddress(AddressModel address);
        public Task<EntertaimentModel> GetEntertainmentByAddress(string houseNumber, string apartmentNumber, string streetTitle);
        public IEnumerable<EntertaimentModel> GetEntartainmentByMinRating(int averageRating);
        public IEnumerable<EntertaimentModel> GetEntartainmentByMaxRating(int averageRating);
        public double GetAverageRating(EntertaimentModel entertaiment);
        public double GetAveragePrice(EntertaimentModel entertaiment);
    }
}
