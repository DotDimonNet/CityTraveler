using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.DTO;

namespace CityTraveler.Services.Interfaces
{
    public interface ICityArchitectureService : IServiceMetadata
    {
        public Task<bool> AddEntertainments(IEnumerable<EntertainmentGetDTO> entertaiments);
        public Task<bool> AddEntertainment(EntertainmentGetDTO entertaimentDTO);
        public Task<bool> UpdateEntertainment(EntertaimentModel entertaiment);
        public Task<bool> ValidateEntertainments();
        public Task<bool> RemoveEntertainment(Guid id);
        public Task<bool> AddStreet(StreetGetDTO street);
        public Task<bool> UpdateStreet(StreetModel street);
        public Task<bool> ValidateStreets();
        public Task<bool> RemoveStreet(Guid streetId);
        public Task<bool> ValidateAddresses();
    }   
}

