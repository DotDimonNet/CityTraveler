using CityTraveler.Domain.Entities;
using CityTraveler.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IMapService
    {
        public Task<StreetShowDTO> FindStreetDTOByCoordinates(CoordinatesDTO coordinatesDto);
        public Task<IEnumerable<StreetDTO>> FindStreetsDTOByTitle(string streetTitle);
        public IEnumerable<StreetShowDTO> FindStreetsDTO(int skip = 0, int take = 10);
        public IEnumerable<AddressShowDTO> FindAddressesDTOByCoordinates(CoordinatesDTO coordinatesDto);
    }
}
