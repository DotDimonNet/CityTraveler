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
            return (IEnumerable<EntertaimentModel>)_context.Entertaiments;
        }

        public async Task<EntertaimentModel> GetLandscapeByCoordinates(CoordinatesModel coordinates)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address.Coordinates == coordinates
                && x.Type == EntertainmentType.Landskape);
        }

        public async Task<EntertaimentModel> GetLandscapeById(Guid id)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id
                && x.Type == EntertainmentType.Landskape);
        }

        public IEnumerable<EntertaimentModel> GetLandscapesByStreet(StreetModel street)
        {
            return _context.Entertaiments.Where(x => x.Address.Street == street
                && x.Type == EntertainmentType.Landskape);
        }

        public IEnumerable<EntertaimentModel> GetLandscapesByStreet(string streetTitle)
        {
            return _context.Entertaiments.Where(x => x.Address.Street.Title == streetTitle
                && x.Type == EntertainmentType.Landskape);
        }

        public IEnumerable<EntertaimentModel> GetLandscapeByTitle(string title)
        {
            return  _context.Entertaiments.Where(x => x.Title.Contains(title)
                && x.Type == EntertainmentType.Landskape);
        }

        public IEnumerable<EntertaimentModel> GetLandscapes(IEnumerable<Guid> ids)
        {
            return _context.Entertaiments.Where(x => ids.Contains(x.Id)
                && x.Type == EntertainmentType.Landskape);
        }

        public async Task<EntertaimentModel> GetLandscapeByAddress(AddressModel address)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address == address
                && x.Type == EntertainmentType.Landskape);
        }

        public async Task<EntertaimentModel> GetLandscapeByAddress(string houseNumber, string apartmentNumber, string streetTitle)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address.HouseNumber == houseNumber
            && x.Address.ApartmentNumber == apartmentNumber
            && x.Address.Street.Title == streetTitle
            && x.Type == EntertainmentType.Landskape);
        }
    }
}
