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
    public class InstitutionService : IInstitutionService
    {
        ApplicationContext _context;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public InstitutionService(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<EntertaimentModel> GetAll()
        {
            return (IEnumerable<EntertaimentModel>)_context.Entertaiments
                .Where(x => x.Type == EntertainmentType.Institution);
        }

        public async Task<EntertaimentModel> GetInstitutionByCoordinates(CoordinatesDTO coordinatesDto)
        {
            return await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                && x.Address.Coordinates.Longitude == coordinatesDto.Longitude
                && x.Type == EntertainmentType.Institution);
        }

        public async Task<EntertaimentModel> GetInstitutionById(Guid id)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id
                && x.Type == EntertainmentType.Institution);
        }

        public IEnumerable<EntertaimentModel> GetInstitutionsByStreet(string streetTitle)
        {
            return _context.Entertaiments.Where(x => x.Address.Street.Title.Contains(streetTitle)
                && x.Type == EntertainmentType.Institution);
        }

        public IEnumerable<EntertaimentModel> GetInstitutionsByTitle(string title)
        {
            return _context.Entertaiments.Where(x => x.Title.Contains(title)
                && x.Type == EntertainmentType.Institution);
        }

        public IEnumerable<EntertaimentModel> GetInstitutions(IEnumerable<Guid> ids)
        {
            return _context.Entertaiments.Where(x => ids.Contains(x.Id)
                && x.Type == EntertainmentType.Institution);
        }

        public async Task<EntertaimentModel> GetInstitutionByAddress(AddressDTO addressDto)
        {
            return await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartsmentNumber
                && x.Address.HouseNumber == addressDto.HouseNumber
                && x.Address.Street.Title.Contains(addressDto.StreetTitle)
                && x.Type == EntertainmentType.Institution);
        }
    }
}
