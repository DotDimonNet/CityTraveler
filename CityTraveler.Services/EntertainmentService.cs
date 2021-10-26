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

        /*public bool IsEvent(EntertaimentModel entertaiment)
        {
            if (entertaiment != null || entertaiment.Type == EntertainmentType.Event)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsLandscape(EntertaimentModel entertaiment)
        {
            if (entertaiment != null || entertaiment.Type == EntertainmentType.Landskape)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsInstitution(EntertaimentModel entertaiment)
        {
            if (entertaiment != null || entertaiment.Type == EntertainmentType.Institution)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/

        public IEnumerable<EntertaimentModel> GetAll()
        {
            return (IEnumerable<EntertaimentModel>)_context.Entertaiments;
        }
        
        public IEnumerable<EntertaimentModel> GetEntartainmentByMinRating(int averageRating)
        {
            var ES = new EntertainmentService(_context);
            var result = (List<EntertaimentModel>)_context.Entertaiments.Where(x => x.Type == EntertainmentType.Event);
            foreach (var entertaiment in result)
            {
                if (ES.GetAverageRating(entertaiment) < averageRating)
                {
                    result.Remove(entertaiment);
                }
            }
            return result;
        }
        public IEnumerable<EntertaimentModel> GetEntartainmentByMaxRating(int averageRating)
        {
            var ES = new EntertainmentService(_context);
            var result = (List<EntertaimentModel>)_context.Entertaiments.Where(x => x.Type == EntertainmentType.Event);
            foreach (var entertaiment in result)
            {
                if (ES.GetAverageRating(entertaiment) > averageRating)
                {
                    result.Remove(entertaiment);
                }
            }
            return result;
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

        public async Task<bool> RemoveEntertainment(Guid id)
        {
            try
            {
                _context.Entertaiments.Remove(await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SetEntertaiment(IEnumerable<EntertaimentModel> entertaiments)
        {
            try
            {
                _context.Entertaiments = (DbSet<EntertaimentModel>)entertaiments;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateEntertainment(EntertaimentModel entertaiment)
        {
            try
            {
                _context.Entertaiments.Remove(await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == entertaiment.Id));
                _context.Entertaiments.Add(entertaiment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> AddEntertainment(EntertainmentDTO entertaimentDTO)
        {
            try
            {
                _context.Entertaiments.Add(entertaimentDTO.ToEntertaiment());
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<EntertaimentModel> GetEntertainmentByAddress(AddressModel address)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address == address);
        }
        public async Task<EntertaimentModel> GetEntertainmentByAddress(string houseNumber, string apartmentNumber, string streetTitle)
        {
            return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address.HouseNumber == houseNumber 
            && x.Address.ApartmentNumber == apartmentNumber && x.Address.Street.Title == streetTitle);
        }
        public double GetAverageRating(EntertaimentModel entertaiment)
        {
            if (entertaiment.Rating.Count() > 0)
            {
                int count = 0;
                double raitings = 0;
                foreach (var raiting in entertaiment.Rating)
                {
                    raitings += raiting.Value;
                    count += 1;
                }
                return raitings / count;
            }
            else
            {
                return 0;
            }
        }
        public double GetAveragePrice(EntertaimentModel entertaiment)
        {
            if (entertaiment.Prices.Count() > 0)
            {
                int count = 0;
                double prices = 0;
                foreach (var price in entertaiment.Prices)
                {
                    prices += price.Value;
                    count += 1;
                }
                return prices / count;
            }
            else
            {
                return 0;
            }
        }
    }
}
