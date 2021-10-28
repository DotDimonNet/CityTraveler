using CityTraveler.Domain.Entities;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Domain.DTO;
using CityTraveler.Services.Extensions;
using CityTraveler.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CityTraveler.Services
{
    public class EntertainmentService : IEntertainmentService
    {
        ApplicationContext _context;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public EntertainmentService(ApplicationContext context) 
        {
            _context = context;
        }

        public IEnumerable<EntertaimentModel> GetAll()
        {
            return _context.Entertaiments;
        }

        public async Task<EntertaimentModel> GetEntertainmentByCoordinates(CoordinatesModel coordinates)
        {
            return await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Address.Coordinates == coordinates);
        }

        public async Task<EntertaimentModel> GetEntertainmentById(Guid id)
        {
            return await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(StreetModel street)
        {
            return _context.Entertaiments
                .Where(x => x.Address.Street == street);
        }

        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(string streetTitle)
        {
            return _context.Entertaiments
                .Where(x => x.Address.Street.Title == streetTitle);
        }

        public async Task<EntertaimentModel> GetEntertainmentByTitle(string title)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x=>x.Title == title);
        }

        public IEnumerable<EntertaimentModel> GetEntertainments(IEnumerable<Guid> ids)
        {
            return _context.Entertaiments.Where(x => ids.Contains(x.Id));
        }

        public async Task<EntertaimentModel> GetEntertainmentByAddress(AddressModel address)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address == address);
        }

        //Need to change
        public async Task<EntertaimentModel> GetEntertainmentByAddress(string houseNumber, string apartmentNumber, string streetTitle)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address.HouseNumber == houseNumber 
            && x.Address.ApartmentNumber == apartmentNumber && x.Address.Street.Title == streetTitle);
        }

        public double GetAverageRating(EntertaimentModel entertaiment)
        {
            if (entertaiment.Reviews.Count() > 0)
            {
                int count = 0;
                double raitings = 0;
                foreach (var review in entertaiment.Reviews)
                {
                    raitings += review.Rating.Value;
                    count += 1;
                }
                return raitings / count;
            }
            else
            {
                return 0;
            }
        }
    }
}
