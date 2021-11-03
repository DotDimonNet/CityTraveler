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
            return (IEnumerable<EntertaimentModel>)_context.Entertaiments.
                Where(x => x.Type == EntertainmentType.Event);
        }

        public async Task<EntertaimentModel> GetEventByCoordinates(CoordinatesDTO coordinatesDto)
        {
            return await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                && x.Address.Coordinates.Longitude == coordinatesDto.Longitude
                && x.Type == EntertainmentType.Event);
        }

        public async Task<EntertaimentModel> GetEventById(Guid id)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id
                && x.Type == EntertainmentType.Event);
        }

        public IEnumerable<EntertaimentModel> GetEventsByStreet(string streetTitle)
        {
            return _context.Entertaiments.Where(x => x.Address.Street.Title.Contains(streetTitle)
                && x.Type == EntertainmentType.Event);
        }

        public IEnumerable<EntertaimentModel> GetEventByTitle(string title)
        {
            return _context.Entertaiments.Where(x => x.Title.Contains(title)
                && x.Type == EntertainmentType.Event);
        }

        public IEnumerable<EntertaimentModel> GetEvents(IEnumerable<Guid> ids)
        {
            return _context.Entertaiments.Where(x => ids.Contains(x.Id)
                && x.Type == EntertainmentType.Event);
        }

        /*public async Task<EntertaimentModel> GetEventByAddress(AddressDTO addressDto)
        {
            return await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartsmentNumber
                && x.Address.HouseNumber == addressDto.HouseNumber
                && x.Address.Street.Title.Contains(addressDto.StreetTitle)
                && x.Type == EntertainmentType.Event);
        }*/

        public IEnumerable<EntertaimentModel> GetEventByBeginingDay(DateTime begin)
        {
            return _context.Entertaiments.Where(x => x.Begin >= begin
            && x.Type == EntertainmentType.Event); 
        }

        public Task<EntertaimentModel> GetEventByAddress(AddressGetDTO address)
        {
            throw new NotImplementedException();
        }
    }
}
