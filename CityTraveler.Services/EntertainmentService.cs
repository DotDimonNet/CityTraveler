using CityTraveler.Domain.Entities;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Errors;
using CityTraveler.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace CityTraveler.Services
{
    public class EntertainmentService : IEntertainmentService
    {
        private readonly ILogger<EntertainmentService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public EntertainmentService(ApplicationContext context, IMapper mapper, ILogger<EntertainmentService> logger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<EntertainmentShowDTO> GetAllDTO(int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return _context.Entertaiments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x));

                case var n when n > 0 && n < 4:
                    var entertainments = _context.Entertaiments.Where(x => x.Type == (EntertainmentType)typeId);

                    return entertainments.Any()
                        ? entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x))
                        : new List<EntertainmentShowDTO>();

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertainmentShowDTO>();
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainmentsByTitle(string title, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return _context.Entertaiments.Where(x => x.Title.Contains(title));

                case var n when n > 0 && n < 4:
                    return _context.Entertaiments.Where(x => x.Type == (EntertainmentType)typeId
                        && x.Title.Contains(title));

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertaimentModel>();
            }
        }

        public IEnumerable<EntertainmentPreviewDTO> GetEntertainmentsDTOByTitle(string title, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return _context.Entertaiments.Where(x => x.Title.Contains(title))
                        .Select(x => _mapper.Map<EntertaimentModel, EntertainmentPreviewDTO>(x));

                case var n when n > 0 && n < 4:
                    var entertainments = _context.Entertaiments.Where(x => x.Type == (EntertainmentType)typeId
                        && x.Title.Contains(title));

                    return entertainments.Any()
                        ? entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentPreviewDTO>(x))
                        : new List<EntertainmentPreviewDTO>();

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertainmentPreviewDTO>();
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainments(IEnumerable<Guid> ids, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return _context.Entertaiments.Where(x => ids.Contains(x.Id));

                case var n when n > 0 && n < 4:
                    return _context.Entertaiments.Where(x => x.Type == (EntertainmentType)typeId
                        && ids.Contains(x.Id));

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertaimentModel>();
            }
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTO(IEnumerable<Guid> ids, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return _context.Entertaiments.Where(x => ids.Contains(x.Id))
                        .Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x));

                case var n when n > 0 && n < 4:
                    var entertainments = _context.Entertaiments.Where(x => x.Type == (EntertainmentType)typeId
                        && ids.Contains(x.Id));

                    return entertainments.Any()
                        ? entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x))
                        : new List<EntertainmentShowDTO>();

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertainmentShowDTO>();
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(string streetTitle, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return _context.Entertaiments
                    .Where(x => x.Address.Street.Title == streetTitle);

                case var n when n > 0 && n < 4:
                    return _context.Entertaiments.Where(x => x.Type == (EntertainmentType)typeId
                        && x.Address.Street.Title == streetTitle);

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertaimentModel>();
            }
        }

        public IEnumerable<EntertainmentPreviewDTO> GetEntertainmentsDTOByStreet(string streetTitle, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return _context.Entertaiments.Where(x => x.Address.Street.Title.Contains(streetTitle))
                        .Select(x => _mapper.Map<EntertaimentModel, EntertainmentPreviewDTO>(x));

                case var n when n > 0 && n < 4:
                    var entertainments = _context.Entertaiments.Where(x => x.Type == (EntertainmentType)typeId
                        && x.Address.Street.Title.Contains(streetTitle));

                    return entertainments.Any()
                        ? entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentPreviewDTO>(x))
                        : new List<EntertainmentPreviewDTO>();

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertainmentPreviewDTO>();
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainmentsByCoordinates(CoordinatesDTO coordinatesDto, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return _context.Entertaiments
                       .Where(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                       && x.Address.Coordinates.Longitude == coordinatesDto.Longitude);

                case var n when n > 0 && n < 4:
                    return _context.Entertaiments.Where(x => x.Type == (EntertainmentType)typeId
                       && x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                       && x.Address.Coordinates.Longitude == coordinatesDto.Longitude);

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertaimentModel>();
            }
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByCoordinates(CoordinatesDTO coordinatesDto, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return _context.Entertaiments.Where(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                        && x.Address.Coordinates.Longitude == coordinatesDto.Longitude)
                        .Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x));

                case var n when n > 0 && n < 4:
                    var entertainments = _context.Entertaiments.Where(x => x.Type == (EntertainmentType)typeId
                    && x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                    && x.Address.Coordinates.Longitude == coordinatesDto.Longitude);

                    return entertainments.Any()
                        ? entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x))
                        : new List<EntertainmentShowDTO>();

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertainmentShowDTO>();
            }
        }

        public async Task<EntertaimentModel> GetEntertainmentByIdAsync(Guid id, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return await _context.Entertaiments
                    .FirstOrDefaultAsync(x => x.Id == id);

                case var n when n > 0 && n < 4:
                    return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Type == (EntertainmentType)typeId
                       && x.Id == id);

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return null;
            }
        }

        public async Task<EntertainmentShowDTO> GetEntertainmentDTOByIdAsync(Guid id, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return _mapper.Map<EntertaimentModel, EntertainmentShowDTO>
                        (await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id));

                case var n when n > 0 && n < 4:
                    var entertainment = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id
                        && x.Type == (EntertainmentType)typeId);

                    return entertainment != null
                        ? _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(entertainment)
                        : null;

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return null;
            }
        }

        public async Task<EntertaimentModel> GetEntertainmentByAddressAsync(AddressGetDTO addressDto, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return await _context.Entertaiments
                       .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartmentNumber
                       && x.Address.HouseNumber == addressDto.HouseNumber
                       && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                       && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude);

                case var n when n > 0 && n < 4:
                    return await _context.Entertaiments.FirstOrDefaultAsync(x => x.Type == (EntertainmentType)typeId
                       && x.Address.HouseNumber == addressDto.HouseNumber
                       && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                       && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude);

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return null;
            }
        }

        public async Task<EntertainmentShowDTO> GetEntertainmentDTOByAddressAsync(AddressGetDTO addressDto, int typeId = 0)
        {
            switch (typeId)
            {
                case 0:
                    return _mapper.Map<EntertaimentModel, EntertainmentShowDTO>
                        (await _context.Entertaiments
                        .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartmentNumber
                        && x.Address.HouseNumber == addressDto.HouseNumber
                        && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                        && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude));

                case var n when n > 0 && n < 4:
                    var entertainment = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartmentNumber
                        && x.Address.HouseNumber == addressDto.HouseNumber
                        && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                        && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude
                        && x.Type == (EntertainmentType)typeId);

                    return entertainment != null
                        ? _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(entertainment)
                        : null;

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return null;
            }
        }

        public double GetAverageRating(EntertaimentModel entertaiment)
        {
            if (entertaiment.Reviews.Count() > 0)
            {
                int count = 0;
                double raitings = 0;
                foreach (var review in entertaiment.Reviews)
                {
                    raitings += review.Rating.Value;
                    count += 1;
                }
                return raitings / count;
            }
            else
            {
                return 0;
            }
        }
    }
}