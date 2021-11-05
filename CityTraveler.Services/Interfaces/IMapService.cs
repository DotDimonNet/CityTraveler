using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IMapService : IServiceMetadata 
    {
        public Task<StreetShowDTO> FindStreetDTOByCoordinates(CoordinatesDTO coordinatesDto);
        public Task<StreetModel> FindStreetByCoordinates(CoordinatesDTO coordinatesDto);
        public Task<IEnumerable<StreetDTO>> FindStreetsDTOByTitle(string streetTitle);
        public IEnumerable<StreetShowDTO> FindStreetsDTO(int skip = 0, int take = 10);
        public IEnumerable<AddressModel> FindAddressesByCoordinates(CoordinatesDTO coordinatesDto);
        public IEnumerable<AddressShowDTO> FindAddressesDTOByCoordinates(CoordinatesDTO coordinatesDto);
    }
}
