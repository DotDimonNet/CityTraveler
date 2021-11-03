using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.DTO;

namespace CityTraveler.Services.Interfaces
{
    public interface IEventService : IServiceMetadata
    {
        public IEnumerable<EntertaimentModel> GetAll();
        public IEnumerable<EntertaimentModel> GetEvents(IEnumerable<Guid> guids);
        public Task<EntertaimentModel> GetEventById(Guid guids);
        public IEnumerable<EntertaimentModel> GetEventByTitle(string title);
        public IEnumerable<EntertaimentModel> GetEventsByStreet(string streetTitle);
        public Task<EntertaimentModel> GetEventByCoordinates(CoordinatesDTO coordinates);
        public Task<EntertaimentModel> GetEventByAddress(AddressGetDTO address);
        public IEnumerable<EntertaimentModel> GetEventByBeginingDay(DateTime begin);
    }
}
