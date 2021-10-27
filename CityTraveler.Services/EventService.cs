using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Domain.Errors;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace CityTraveler.Services
{
    public class EventService : IEventService
    {
        ApplicationContext _context;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public EventService(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<EntertaimentModel> GetAll()
        {
            return (IEnumerable<EntertaimentModel>)_context.Entertaiments;
        }

        public async Task<EntertaimentModel> GetEventByCoordinates(CoordinatesModel coordinates)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address.Coordinates == coordinates 
                && x.Type == EntertainmentType.Event);
        }

        public async Task<EntertaimentModel> GetEventById(Guid id)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id
                && x.Type == EntertainmentType.Event);
        }

        public IEnumerable<EntertaimentModel> GetEventsByStreet(StreetModel street)
        {
            return _context.Entertaiments.Where(x => x.Address.Street == street
                && x.Type == EntertainmentType.Event);
        }

        public IEnumerable<EntertaimentModel> GetEventsByStreet(string streetTitle)
        {
            return _context.Entertaiments.Where(x => x.Address.Street.Title == streetTitle
                && x.Type == EntertainmentType.Event);
        }

        public async Task<EntertaimentModel> GetEventByTitle(string title)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Title == title
                && x.Type == EntertainmentType.Event);
        }

        public IEnumerable<EntertaimentModel> GetEvents(IEnumerable<Guid> ids)
        {
            return _context.Entertaiments.Where(x => ids.Contains(x.Id)
                && x.Type == EntertainmentType.Event);
        }

        public async Task<EntertaimentModel> GetEventByAddress(AddressModel address)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address == address
                && x.Type == EntertainmentType.Event);
        }

        public async Task<EntertaimentModel> GetEventByAddress(string houseNumber, string apartmentNumber, string streetTitle)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address.HouseNumber == houseNumber
            && x.Address.ApartmentNumber == apartmentNumber 
            && x.Address.Street.Title == streetTitle
            && x.Type == EntertainmentType.Event);
        }

        //TODO
        public IEnumerable<EntertaimentModel> GetEventByBeginingDay(DateTime begin)
    {
        throw new NotImplementedException();
    }
    }
}
