﻿using CityTraveler.Domain.Entities;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class CityArchitectureService : ICityArchitectureService
    {
        private readonly ApplicationContext _dbContext;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public CityArchitectureService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddEntertainmentToCity(EntertaimentModel entertaiment)
        {
            try
            {
                _dbContext.Entertaiments.Add(entertaiment);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new DbInitializationException("Failed to add entertainment");
                //return false;
            }
            return true;
        }

        public async Task<bool> UpdateCityEntertainment(EntertaimentModel entertaiment)
        {
            try
            {
                _dbContext.Entertaiments.Update(entertaiment);
                DbSet<EntertaimentModel> en = (DbSet<EntertaimentModel>)_dbContext.Entertaiments.Where(x => x.Id != entertaiment.Id);
                _dbContext.Entertaiments.Add(entertaiment);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveEntertainmentById(Guid objectId)
        {
            try
            {
                EntertaimentModel mo= await _dbContext.Entertaiments.FirstOrDefaultAsync(x => x.Id == objectId);
                _dbContext.Entertaiments.Remove(mo);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> AddStreet(StreetModel street)
        {
            try
            {
                _dbContext.Streets.Add(street);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateStreet(StreetModel street)
        {
            try
            {
                DbSet<StreetModel> en = (DbSet<StreetModel>)_dbContext.Entertaiments.Where(x => x.Id != street.Id);
                _dbContext.Streets.Add(street);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveStreet(Guid streetId)
        {
            try
            {
                _dbContext.Streets = (DbSet<StreetModel>)_dbContext.Entertaiments.Where(x => x.Id != streetId);
               await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateCityMap()
        {
            return validateAddresses().Result && validateEntertainments().Result;
        }

        public async Task<bool> validateEntertainments() 
        {
            try
            {
                EntertaimentModel en = await _dbContext.Entertaiments.FirstOrDefaultAsync(x => x.Address == null);
                EntertaimentModel en1 = await _dbContext.Entertaiments.FirstOrDefaultAsync(x => x.Type == null);
                if (en == null && en1 == null)
                    return true;
                else
                    return false;

            }
            catch (Exception e) 
            {
                return false;
            }
        }
        public async Task<bool> validateAddresses()
        {
            try
            {
                AddressModel ad = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.Coordinates == null);
                AddressModel ad1 = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.Street == null);
                if (ad == null && ad1 == null)
                    return true;
                else
                    return false;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<StreetModel> FindStreetByCoordinates(Guid coordID)
        {
            AddressModel ad = await _dbContext.Addresses.FirstOrDefaultAsync(x=>x.CoordinatesId == coordID);
            return ad.Street;
        }
        public async Task<StreetModel> FindStreetByCoordinates(double longtitude, double latitude) 
        {
            AddressModel ad = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.Coordinates.Longitude == longtitude && x.Coordinates.Latitude == latitude);
            return ad.Street;
        }
        public async Task<AddressModel> FindAddressByCoordinates(Guid coordID)
        {
            return await _dbContext.Addresses.FirstOrDefaultAsync(x => x.CoordinatesId == coordID);
        }

        public async Task<List<EntertaimentModel>> FindEntertainmentByStreet(Guid streetId)
        {
            return await _dbContext.Entertaiments.Where(x=>x.Address.StreetId == streetId).ToListAsync();
        }

        public async Task<IEnumerable<EntertaimentModel>> FindEntertainmentByCoordinate(Guid coorId)
        {
            return _dbContext.Entertaiments.Where(x => x.Address.CoordinatesId == coorId);
        }

        public async Task<IEnumerable<EntertaimentModel>> FindEntertainmentByAddress(Guid addressId)
        {
            return _dbContext.Entertaiments.Where(x => x.Address.StreetId == addressId);
        }

        public async Task<AddressModel> FindAddressByStreetHouse(Guid streetId, string houseNum)
        {
            return await _dbContext.Addresses.FirstOrDefaultAsync(x=>x.StreetId == streetId && x.HouseNumber == houseNum);
        }

        public async Task<IEnumerable<AddressModel>> FindAddressByHouse(string houseNum)
        {
            return _dbContext.Addresses.Where(x => x.HouseNumber == houseNum);
        }

        public async Task<IEnumerable<EntertaimentModel>> FindEntertainmentByStreetTitle(string streetTitle)
        {
            return _dbContext.Entertaiments.Where(x => x.Address.Street.Title == streetTitle);
        }

        public async Task<AddressModel> FindAddressByCoordinates(double longtitude, double latitude)
        {
            return await _dbContext.Addresses.FirstOrDefaultAsync(x => x.Coordinates.Longitude == longtitude&&x.Coordinates.Latitude==latitude);
        }
    }
}
