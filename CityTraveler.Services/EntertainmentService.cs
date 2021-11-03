using CityTraveler.Domain.Entities;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Domain.DTO;
using CityTraveler.Services.Extensions;
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

        public IEnumerable<EntertainmentShowDTO> GetAllDTO()
        {
            var entertainments = _context.Entertaiments;

            if (entertainments.Count() == 0)
            {
                return new List<EntertainmentShowDTO>();
            }
            else
            {
                return entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x));
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainmentsByTitle(string title)
        {
            return _context.Entertaiments.Where(x => x.Title.Contains(title));
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByTitle(string title)
        {
            var entertainments = _context.Entertaiments.Where(x => x.Title.Contains(title));

            if (entertainments.Count() == 0)
            {
                return new List<EntertainmentShowDTO>();
            }
            else
            {
                return entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x));
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainments(IEnumerable<Guid> ids)
        {
            return _context.Entertaiments.Where(x => ids.Contains(x.Id));
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTO(IEnumerable<Guid> ids)
        {
            var entertainments = _context.Entertaiments.Where(x => ids.Contains(x.Id));

            if (entertainments.Count() == 0)
            {
                return new List<EntertainmentShowDTO>();
            }
            else
            {
                return entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x));
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(string streetTitle)
        {
            return _context.Entertaiments
                .Where(x => x.Address.Street.Title == streetTitle);
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByStreet(string streetTitle)
        {
            var entertainments = _context.Entertaiments
                .Where(x => x.Address.Street.Title == streetTitle);

            if (entertainments.Count() == 0)
            {
                return new List<EntertainmentShowDTO>();
            }
            else
            {
                return entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x));
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainmentsByCoordinates(CoordinatesDTO coordinatesDto)
        {
            return _context.Entertaiments
                .Where(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                && x.Address.Coordinates.Longitude == coordinatesDto.Longitude);
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByCoordinates(CoordinatesDTO coordinatesDto)
        {
            var entertainments = _context.Entertaiments
                .Where(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                && x.Address.Coordinates.Longitude == coordinatesDto.Longitude);

            if (entertainments.Count() == 0)
            {
                return new List<EntertainmentShowDTO>();
            }
            else
            {
                return entertainments.Select(x => _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(x));
            }
        }

        public async Task<EntertaimentModel> GetEntertainmentById(Guid id)
        {
            return await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EntertainmentShowDTO> GetEntertainmentDTOById(Guid id)
        {
            var entertainment = await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entertainment == null)
            {
                _logger.LogInformation("Info: Entetainment is not found. ID isn't correct");
                return null;
            }
            else
            {
                return _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(entertainment);
            }
        }

        public async Task<EntertaimentModel> GetEntertainmentByAddress(AddressGetDTO addressDto)
        {
            return await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartmentNumber
                && x.Address.HouseNumber == addressDto.HouseNumber
                && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude);
        }

        public async Task<EntertainmentShowDTO> GetEntertainmentDTOByAddress(AddressGetDTO addressDto)
        {
            var entertainment = await _context.Entertaiments
                .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartmentNumber
                && x.Address.HouseNumber == addressDto.HouseNumber
                && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude);
            return _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(entertainment);
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