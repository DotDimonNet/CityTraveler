
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ICityArchitectureService : IServiceMetadata
    {
        public Task<bool> AddEntertainmentToCity(EntertaimentModel institution);
        public Task<bool> UpdateCityEntertainment(EntertaimentModel institution);
        public Task<bool> RemoveEntertainmentById(Guid objectId);
        public Task<bool> AddStreet(StreetModel street);
        public Task<bool> UpdateStreet(StreetModel street);
        public Task<bool> RemoveStreet(Guid streetId);
        public IEnumerable<AddressModel> getAddress(int skip=0,int take=10);
        public IEnumerable<StreetModel> getStreet(int skip = 0, int take = 10);
        //Move to Map Servise
        public Task<StreetModel> FindStreetByCoordinates(Guid coordID);
        public Task<StreetModel> FindStreetByCoordinates(double longtitude, double latitude);
        public Task<AddressModel> FindAddressByCoordinates(Guid coordID);
        public Task<AddressModel> FindAddressByCoordinates(double longtitude, double latitude);
        public Task<AddressModel> FindAddressByStreetHouse(Guid streetId, string houseNum);
        public IEnumerable<AddressModel> FindAddressByHouse(string houseNum);
        public Task<bool> ValidateCityMap();
    }   
}

