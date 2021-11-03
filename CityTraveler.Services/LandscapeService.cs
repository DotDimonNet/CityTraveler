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
    public class LandscapeService : ILandscapeService
    {
        ApplicationContext _context;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public LandscapeService(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<EntertaimentModel> GetAll()
        {
            return (IEnumerable<EntertaimentModel>)_context.Entertaiments
                .Where(x => x.Type == EntertainmentType.Landskape);
        }

        public async Task<EntertaimentModel> GetLandscapeByCoordinates(CoordinatesDTO coordinatesDto)
        {
            return await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                && x.Address.Coordinates.Longitude == coordinatesDto.Longitude
                && x.Type == EntertainmentType.Landskape);
        }

        public async Task<EntertaimentModel> GetLandscapeById(Guid id)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id
                && x.Type == EntertainmentType.Landskape);
        }

        public IEnumerable<EntertaimentModel> GetLandscapesByStreet(string streetTitle)
        {
            return _context.Entertaiments.Where(x => x.Address.Street.Title.Contains(streetTitle)
                && x.Type == EntertainmentType.Landskape);
        }

        public IEnumerable<EntertaimentModel> GetLandscapesByTitle(string title)
        {
            return _context.Entertaiments.Where(x => x.Title.Contains(title)
                && x.Type == EntertainmentType.Landskape);
        }

        public IEnumerable<EntertaimentModel> GetLandscapes(IEnumerable<Guid> ids)
        {
            return _context.Entertaiments.Where(x => ids.Contains(x.Id)
                && x.Type == EntertainmentType.Landskape);
        }

        public Task<EntertaimentModel> GetLandscapeByAddress(AddressGetDTO address)
        {
            throw new NotImplementedException();
        }

        /*public async Task<EntertaimentModel> GetLandscapeByAddress(AddressDTO addressDto)
        {
            return await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartsmentNumber
                && x.Address.HouseNumber == addressDto.HouseNumber
                && x.Address.Street.Title.Contains(addressDto.StreetTitle)
                && x.Type == EntertainmentType.Landskape);
        }*/
    }
}
