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
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace CityTraveler.Services
{
    public class MapService : IMapService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<MapService> _logger;
        private readonly IMapper _mapper;
        public MapService(ApplicationContext context, IMapper mapper, ILogger<MapService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public async Task<StreetModel> FindStreetByCoordinates(CoordinatesDTO coordinatesDto)
        {
            return await _context.Streets.FirstOrDefaultAsync(x => x.Addresses
               .Any(x => x.Coordinates.Latitude == coordinatesDto.Latitude
               && x.Coordinates.Longitude == coordinatesDto.Longitude));
        }

        public async Task<StreetShowDTO> FindStreetDTOByCoordinates(CoordinatesDTO coordinatesDto)
        {
            var street = await _context.Streets.FirstOrDefaultAsync(x => x.Addresses
               .Any(x => x.Coordinates.Latitude == coordinatesDto.Latitude
               && x.Coordinates.Longitude == coordinatesDto.Longitude));

            if (street == null)
            {
                _logger.LogInformation("Info: Street is not found");
                return null;
            }
            else
            {
                return _mapper.Map<StreetModel, StreetShowDTO>(street);
            }
        }

        public IEnumerable<StreetShowDTO> GetStreetsDTO(int skip = 0, int take = 10)
        {
            try
            {
                var streers = _context.Streets.Skip(skip).Take(take);
                return streers.Select(x => _mapper.Map<StreetModel, StreetShowDTO>(x));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new CityArchitectureServiceException($"Failed to get streets: {ex.Message}");
            }
        }

        public IEnumerable<AddressModel> FindAddressesByCoordinates(CoordinatesDTO coordinatesDto)
        {
            return _context.Addresses.Where(x => x.Coordinates.Latitude == coordinatesDto.Latitude
                && x.Coordinates.Longitude == coordinatesDto.Longitude);
        }

        public IEnumerable<AddressShowDTO> FindAddressesDTOByCoordinates(CoordinatesDTO coordinatesDto)
        {
            var addresses = _context.Addresses.Where(x => x.Coordinates.Latitude == coordinatesDto.Latitude
                && x.Coordinates.Longitude == coordinatesDto.Longitude);

            if (addresses.Count() == 0)
            {
                return new List<AddressShowDTO>();
            }
            else
            {
                return addresses.Select(x => _mapper.Map<AddressModel, AddressShowDTO>(x));
            }
        }
    }
}
