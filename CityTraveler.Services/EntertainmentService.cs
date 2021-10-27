using CityTraveler.Domain.Entities;
using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return (IEnumerable<EntertaimentModel>)_context.Entertaiments;
        }
        
        public IEnumerable<EntertaimentModel> GetEntartainmentByMinRating(int averageRating)
        {
            return (IEnumerable<EntertaimentModel>)_context.Entertaiments
                .SelectMany(x => x.Trips.Where(y => y.AverageRating > averageRating)).Select(x => x.Entertaiment);
        }
        public IEnumerable<EntertaimentModel> GetEntartainmentByMaxRating(int averageRating)
        {
            return (IEnumerable<EntertaimentModel>)_context.Entertaiments
                .SelectMany(x => x.Trips.Where(y => y.AverageRating < averageRating)).Select(x => x.Entertaiment);
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
        public async Task<bool> AddEntertainment(EntertaimentModel entertaiment)
        {
            try
            {
                _context.Entertaiments.Add(entertaiment);
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
    }
}
