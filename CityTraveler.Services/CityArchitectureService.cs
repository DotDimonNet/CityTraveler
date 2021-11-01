using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Errors;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityTraveler.Domain.DTO;
using CityTraveler.Services.Extensions;

namespace CityTraveler.Services
{
    public class CityArchitectureService : ICityArchitectureService
    {
        private readonly ApplicationContext _context;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public CityArchitectureService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> RemoveEntertainment(Guid id)
        {
            try
            {
                var entertainment = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id);
                _context.Entertaiments.Remove(entertainment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to remove entertainment");
            }
        }

        public async Task<bool> AddEntertainments(IEnumerable<EntertainmentDTO> entertaiments)
        {
            try
            {
                var models = new List<EntertaimentModel>();
                var types = _context.EntertainmentType;

                foreach (var ent in entertaiments)
                {
                    var model = ent.ToEntertaiment();
                    model.Type = types.FirstOrDefault(x => x.Id == ent.Type);
                    models.Add(model);
                }

                _context.Entertaiments.AddRange(models);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to set entertainments: {e}");
            }
        }

        public async Task<bool> UpdateEntertainment(EntertaimentModel entertaiment)
        {
            try
            {
                var model = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == entertaiment.Id);
                if (model == null)
                {
                    _context.Entertaiments.Add(entertaiment);
                }
                else
                {
                    _context.Entertaiments.Update(model.UpdateEntertainmentWith(entertaiment));
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddEntertainment(EntertainmentDTO entertaimentDTO)
        {
            try
            {
                var model = entertaimentDTO.ToEntertaiment();
                model.Type = _context.EntertainmentType.FirstOrDefault(x => x.Id == entertaimentDTO.Type);
                _context.Entertaiments.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddStreet(StreetModel street)
        {
            try
            {
                _context.Streets.Add(street);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new CityArchitectureServiceException("Failed to add street");
            }
            return true;
        }

        public async Task<bool> UpdateStreet(StreetModel street)
        {
            try
            {
                /*DbSet<StreetModel> en = (DbSet<StreetModel>)_dbContext.Entertaiments.Where(x => x.Id != street.Id);
                _dbContext.Streets.Add(street);*/
                _context.Streets.Update(street);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new CityArchitectureServiceException("Failed to update street");
                //return false;
            }
            return true;
        }

        public async Task<bool> RemoveStreet(Guid streetId)
        {
            try
            {
                StreetModel st = await _context.Streets.FirstOrDefaultAsync(x => x.Id == streetId);
                if (st == null)
                    throw new CityArchitectureServiceException("Street not found");
                _context.Streets.Remove(st);
               await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new CityArchitectureServiceException("Failed to remove street");
                //return false;
            }
            return true;
        }

        public async Task<bool> validateEntertainments() 
        {
            try
            {
                EntertaimentModel en = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address == null);
                EntertaimentModel en1 = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Type == null);
                if (en == null && en1 == null)
                    return true;
                else
                    return false;

            }
            catch (Exception e) 
            {
                throw new CityArchitectureServiceException("Failed to validate city map");
                //return false;
            }
        }

        public bool ValidateAddresses()
        {
            return _context.Addresses.Any(x => x.Street == null || x.Coordinates == null);
        }

        public IEnumerable<StreetModel> FindStreetByCoordinates(double longtitude, double latitude) 
        {
            return _context.Streets.Where(x => x.Addresses
                .Any(x => x.Coordinates.Latitude == longtitude && x.Coordinates.Latitude == latitude));
        }

        public async Task<AddressModel> FindAddressByCoordinates(Guid coordID)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x => x.CoordinatesId == coordID);
        }

        public async Task<AddressModel> FindAddressByStreetHouse(Guid streetId, string houseNum)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x=>x.StreetId == streetId && x.HouseNumber == houseNum);
        }

        public IEnumerable<AddressModel> FindAddressByHouse(string houseNum)
        {
            return _context.Addresses.Where(x => x.HouseNumber == houseNum);
        }

        public async Task<AddressModel> FindAddressByCoordinates(double longtitude, double latitude)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x => x.Coordinates.Longitude == longtitude&&x.Coordinates.Latitude==latitude);
        }

        public IEnumerable<AddressModel> getAddress(int skip = 0, int take = 10)
        {
            try
            {
                return _context.Addresses.Skip(skip).Take(take);
            }
            catch (Exception e) 
            {
                throw new CityArchitectureServiceException("Failed to get addresses");
                //return null;
            }
        }

        public IEnumerable<StreetModel> getStreet(int skip = 0, int take = 10)
        {
            try
            {
                return _context.Streets.Skip(skip).Take(take);
            }
            catch (Exception e)
            {
                throw new CityArchitectureServiceException("Failed to get streets");
                //return null;
            }
        }
    }
}
