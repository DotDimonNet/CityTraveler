using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.DTO;

namespace CityTraveler.Services.Interfaces
{
    public interface ICityArchitectureService
    {
        public Task<bool> AddEntertainmentsAsync(IEnumerable<EntertainmentGetDTO> entertaiments);
        public Task<bool> AddEntertainmentAsync(EntertainmentGetDTO entertaimentDTO);
        public Task<bool> UpdateEntertainmentAsync(EntertainmentUpdateDTO entertaimentDto);
        public bool ValidateEntertainments();
        public Task<bool> RemoveEntertainmentAsync(Guid id);
        public Task<bool> AddStreetAsync(StreetGetDTO street);
        public Task<bool> UpdateStreetAsync(StreetDTO streetDto);
        public Task<bool> AddCoordinatesToStreet(CoordinatesDTO coordinatesDTO, string streetId);
        public bool ValidateStreets();
        public Task<bool> RemoveStreetAsync(Guid streetId);
        public bool ValidateAddresses();
    }   
}

