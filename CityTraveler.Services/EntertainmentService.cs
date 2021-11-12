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

        public EntertainmentService(ApplicationContext context, IMapper mapper, ILogger<EntertainmentService> logger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<EntertainmentShowDTO> GetAllDTO(EntertainmentType typeId = EntertainmentType.All)
        {
            switch (typeId)
            {
                case EntertainmentType.All:
                    return _context.Entertaiments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x));

                case EntertainmentType.Landscape:
                case EntertainmentType.Institution:
                case EntertainmentType.Event:
                    var entertainments = _context.Entertaiments.Where(x => x.Type == typeId);

                    return entertainments.Any()
                        ? entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x))
                        : new List<EntertainmentShowDTO>();

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertainmentShowDTO>();
            }
        }

        public IEnumerable<EntertainmentPreviewDTO> GetEntertainmentsDTOByTitle(string title, EntertainmentType typeId = EntertainmentType.All)
        {
            switch (typeId)
            {
                case EntertainmentType.All:
                    return _context.Entertaiments.Where(x => x.Title.Contains(title))
                        .Select(x => _mapper.Map<EntertaimentModel, EntertainmentPreviewDTO>(x));

                case EntertainmentType.Landscape:
                case EntertainmentType.Institution:
                case EntertainmentType.Event:
                    var entertainments = _context.Entertaiments.Where(x => x.Type == typeId
                        && x.Title.Contains(title));

                    return entertainments.Any()
                        ? entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentPreviewDTO>(x))
                        : new List<EntertainmentPreviewDTO>();

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertainmentPreviewDTO>();
            }
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTO(IEnumerable<Guid> ids, EntertainmentType typeId = EntertainmentType.All)
        {
            switch (typeId)
            {
                case EntertainmentType.All:
                    return _context.Entertaiments.Where(x => ids.Contains(x.Id))
                        .Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x));

                case EntertainmentType.Landscape:
                case EntertainmentType.Institution:
                case EntertainmentType.Event:
                    var entertainments = _context.Entertaiments.Where(x => x.Type == typeId
                        && ids.Contains(x.Id));

                    return entertainments.Any()
                        ? entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x))
                        : new List<EntertainmentShowDTO>();

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertainmentShowDTO>();
            }
        }

        public IEnumerable<EntertainmentPreviewDTO> GetEntertainmentsDTOByStreet(string streetTitle, EntertainmentType typeId = EntertainmentType.All)
        {
            switch (typeId)
            {
                case EntertainmentType.All:
                    return _context.Entertaiments.Where(x => x.Address.Street.Title.Contains(streetTitle))
                        .Select(x => _mapper.Map<EntertaimentModel, EntertainmentPreviewDTO>(x));

                case EntertainmentType.Landscape:
                case EntertainmentType.Institution:
                case EntertainmentType.Event:
                    var entertainments = _context.Entertaiments.Where(x => x.Type == typeId
                        && x.Address.Street.Title.Contains(streetTitle));

                    return entertainments.Any()
                        ? entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentPreviewDTO>(x))
                        : new List<EntertainmentPreviewDTO>();

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return new List<EntertainmentPreviewDTO>();
            }
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByCoordinates(CoordinatesDTO coordinatesDto, EntertainmentType typeId = EntertainmentType.All)
        {
            switch (typeId)
            {
                case EntertainmentType.All:
                    return _context.Entertaiments.Where(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                        && x.Address.Coordinates.Longitude == coordinatesDto.Longitude)
                        .Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x));

                case EntertainmentType.Landscape:
                case EntertainmentType.Institution:
                case EntertainmentType.Event:
                    var entertainments = _context.Entertaiments.Where(x => x.Type == typeId
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

        public async Task<EntertainmentShowDTO> GetEntertainmentDTOByIdAsync(Guid id, EntertainmentType typeId = EntertainmentType.All)
        {
            switch (typeId)
            {
                case EntertainmentType.All:
                    return _mapper.Map<EntertaimentModel, EntertainmentShowDTO>
                        (await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id));

                case EntertainmentType.Landscape:
                case EntertainmentType.Institution:
                case EntertainmentType.Event:
                    var entertainment = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id
                        && x.Type == typeId);

                    return entertainment != null
                        ? _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(entertainment)
                        : null;

                default:
                    _logger.LogWarning("Warning: Type's ID isn't correct. Type's ID is negative or more than 3");
                    return null;
            }
        }

        public async Task<EntertainmentShowDTO> GetEntertainmentDTOByAddressAsync(AddressGetDTO addressDto, EntertainmentType typeId = EntertainmentType.All)
        {
            switch (typeId)
            {
                case EntertainmentType.All:
                    return _mapper.Map<EntertaimentModel, EntertainmentShowDTO>
                        (await _context.Entertaiments
                        .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartmentNumber
                        && x.Address.HouseNumber == addressDto.HouseNumber
                        && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                        && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude));

                case EntertainmentType.Landscape:
                case EntertainmentType.Institution:
                case EntertainmentType.Event:
                    var entertainment = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartmentNumber
                        && x.Address.HouseNumber == addressDto.HouseNumber
                        && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                        && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude
                        && x.Type == typeId);

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