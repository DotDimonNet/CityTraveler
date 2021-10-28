using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;

namespace CityTraveler.Services.Interfaces
{
    public interface IEventService : IServiceMetadata
    {
        public IEnumerable<EntertaimentModel> GetAll();
        public IEnumerable<EntertaimentModel> GetEvents(IEnumerable<Guid> guids);
        public Task<EntertaimentModel> GetEventById(Guid guids);
        public Task<EntertaimentModel> GetEventByTitle(string title);
        public IEnumerable<EntertaimentModel> GetEventsByStreet(StreetModel street);
        public IEnumerable<EntertaimentModel> GetEventsByStreet(string streetTitle);
        public Task<EntertaimentModel> GetEventByCoordinates(CoordinatesModel coordinates);
        public Task<EntertaimentModel> GetEventByAddress(AddressModel address);
        public Task<EntertaimentModel> GetEventByAddress(string houseNumber, string apartmentNumber, string streetTitle);
        public IEnumerable<EntertaimentModel> GetEventByBeginingDay(DateTime begin);
    }
}
