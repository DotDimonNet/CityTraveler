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
        public IEnumerable<EntertaimentModel> GetEntertainmentByTitle(string title);
        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(string streetTitle);
        public Task<EntertaimentModel> GetEntertainmentByCoordinates(CoordinatesDTO coordinates);
        public Task<EntertaimentModel> GetEntertainmentByAddress(AddressDTO address);
        public double GetAverageRating(EntertaimentModel entertaiment);
    }
}
