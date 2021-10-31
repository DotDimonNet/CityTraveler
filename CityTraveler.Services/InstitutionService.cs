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
            return (IEnumerable<EntertaimentModel>)_context.Entertaiments;
        }

        public async Task<EntertaimentModel> GetInstitutionByCoordinates(CoordinatesModel coordinates)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address.Coordinates == coordinates
                && x.Type == EntertainmentType.Institution);
        }

        public async Task<EntertaimentModel> GetInstitutionById(Guid id)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id
                && x.Type == EntertainmentType.Institution);
        }

        public IEnumerable<EntertaimentModel> GetInstitutionsByStreet(StreetModel street)
        {
            return _context.Entertaiments.Where(x => x.Address.Street == street
                && x.Type == EntertainmentType.Institution);
        }

        public IEnumerable<EntertaimentModel> GetInstitutionsByStreet(string streetTitle)
        {
            return _context.Entertaiments.Where(x => x.Address.Street.Title == streetTitle
                && x.Type == EntertainmentType.Institution);
        }

        public async Task<EntertaimentModel> GetInstitutionByTitle(string title)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Title == title
                && x.Type == EntertainmentType.Institution);
        }

        public IEnumerable<EntertaimentModel> GetInstitutions(IEnumerable<Guid> ids)
        {
            return _context.Entertaiments.Where(x => ids.Contains(x.Id)
                && x.Type == EntertainmentType.Institution);
        }

        public async Task<EntertaimentModel> GetInstitutionByAddress(AddressModel address)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address == address
                && x.Type == EntertainmentType.Institution);
        }

        public async Task<EntertaimentModel> GetInstitutionByAddress(string houseNumber, string apartmentNumber, string streetTitle)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address.HouseNumber == houseNumber
            && x.Address.ApartmentNumber == apartmentNumber
            && x.Address.Street.Title == streetTitle
            && x.Type == EntertainmentType.Institution);
        }

    }
}
