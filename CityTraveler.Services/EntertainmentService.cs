using CityTraveler.Domain.Entities;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Domain.DTO;
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
            IEnumerable<EntertaimentModel> entertainments;

            if (typeId == 0)
            {
                entertainments = _context.Entertaiments;
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                entertainments = _context.Entertaiments.Where(x => x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }

            if (!entertainments.Any())
            {
                return new List<EntertainmentShowDTO>();
            }
            else
            {
                var result = new List<EntertainmentShowDTO>();
                foreach (var model in entertainments)
                {
                    var resultModel = _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(model);
                    resultModel.Type = model.Type.Name;
                    result.Add(resultModel);
                }
                return result;
            }
        }
        public IEnumerable<EntertaimentModel> GetEntertainmentsByTitle(string title, int typeId = 0)
        {
            if (typeId == 0)
            {
                return _context.Entertaiments.Where(x => x.Title.Contains(title));
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                return _context.Entertaiments.Where(x => x.Title.Contains(title) && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByTitle(string title, int typeId = 0)
        {
            IEnumerable<EntertaimentModel> entertainments;

            if (typeId == 0)
            {
                entertainments = _context.Entertaiments.Where(x => x.Title.Contains(title));
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                entertainments = _context.Entertaiments.Where(x => x.Title.Contains(title) && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }

            if (!entertainments.Any())
            {
                return new List<EntertainmentShowDTO>();
            }
            else
            {
                var result = new List<EntertainmentShowDTO>();
                foreach (var model in entertainments)
                {
                    var resultModel = _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(model);
                    resultModel.Type = model.Type.Name;
                    result.Add(resultModel);
                }
                return result;
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainments(IEnumerable<Guid> ids, int typeId = 0)
        {
            if (typeId == 0)
            {
                return _context.Entertaiments.Where(x => ids.Contains(x.Id));
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                return _context.Entertaiments.Where(x => ids.Contains(x.Id) && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTO(IEnumerable<Guid> ids, int typeId = 0)
        {
            IEnumerable<EntertaimentModel> entertainments;

            if (typeId == 0)
            {
                entertainments = _context.Entertaiments.Where(x => ids.Contains(x.Id));
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                entertainments = _context.Entertaiments.Where(x => ids.Contains(x.Id) && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }

            if (!entertainments.Any())
            {
                return new List<EntertainmentShowDTO>();
            }
            else
            {
                var result = new List<EntertainmentShowDTO>();
                foreach (var model in entertainments)
                {
                    var resultModel = _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(model);
                    resultModel.Type = model.Type.Name;
                    result.Add(resultModel);
                }
                return result;
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(string streetTitle, int typeId = 0)
        {
            if (typeId == 0)
            {
                return _context.Entertaiments
                    .Where(x => x.Address.Street.Title == streetTitle);
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                return _context.Entertaiments
                    .Where(x => x.Address.Street.Title == streetTitle && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByStreet(string streetTitle, int typeId = 0)
        {
            IEnumerable<EntertaimentModel> entertainments;

            if (typeId == 0)
            {
                entertainments = _context.Entertaiments
                    .Where(x => x.Address.Street.Title == streetTitle);
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                entertainments = _context.Entertaiments
                    .Where(x => x.Address.Street.Title == streetTitle
                    && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }

            if (!entertainments.Any())
            {
                return new List<EntertainmentShowDTO>();
            }
            else
            {
                var result = new List<EntertainmentShowDTO>();
                foreach (var model in entertainments)
                {
                    var resultModel = _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(model);
                    resultModel.Type = model.Type.Name;
                    result.Add(resultModel);
                }
                return result;
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainmentsByCoordinates(CoordinatesDTO coordinatesDto, int typeId = 0)
        {
            if (typeId == 0)
            {
                return _context.Entertaiments
                    .Where(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                    && x.Address.Coordinates.Longitude == coordinatesDto.Longitude);
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                return _context.Entertaiments
                    .Where(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                    && x.Address.Coordinates.Longitude == coordinatesDto.Longitude && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }
            
        }

        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByCoordinates(CoordinatesDTO coordinatesDto, int typeId = 0)
        {
            IEnumerable<EntertaimentModel> entertainments;

            if (typeId == 0)
            {
                entertainments = _context.Entertaiments
                    .Where(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                    && x.Address.Coordinates.Longitude == coordinatesDto.Longitude);
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                entertainments = _context.Entertaiments
                    .Where(x => x.Address.Coordinates.Latitude == coordinatesDto.Latitude
                    && x.Address.Coordinates.Longitude == coordinatesDto.Longitude
                    && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }

            if (!entertainments.Any())
            {
                return new List<EntertainmentShowDTO>();
            }
            else
            {
                var result = new List<EntertainmentShowDTO>();
                foreach (var model in entertainments)
                {
                    var resultModel = _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(model);
                    resultModel.Type = model.Type.Name;
                    result.Add(resultModel);
                }
                return result;
            }
        }

        public async Task<EntertaimentModel> GetEntertainmentById(Guid id, int typeId = 0)
        {
            if (typeId == 0)
            {
                return await _context.Entertaiments
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                return await _context.Entertaiments
                    .FirstOrDefaultAsync(x => x.Id == id && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }
        }

        public async Task<EntertainmentShowDTO> GetEntertainmentDTOById(Guid id, int typeId = 0)
        {
            EntertaimentModel entertainment;

            if (typeId == 0)
            {
                entertainment = await _context.Entertaiments
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                entertainment = await _context.Entertaiments
                    .FirstOrDefaultAsync(x => x.Id == id
                    && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }

            if (entertainment == null)
            {
                _logger.LogInformation("Info: Entetainment is not found. ID isn't correct");
                return null;
            }
            else
            {
                var result = _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(entertainment);
                result.Type = entertainment.Type.Name;
                return result;
            }
        }

        public async Task<EntertaimentModel> GetEntertainmentByAddress(AddressGetDTO addressDto, int typeId = 0)
        {
            if (typeId == 0)
            {
                return await _context.Entertaiments
                    .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartmentNumber
                    && x.Address.HouseNumber == addressDto.HouseNumber
                    && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                    && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude);
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                return await _context.Entertaiments
                    .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartmentNumber
                    && x.Address.HouseNumber == addressDto.HouseNumber
                    && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                    && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude 
                    && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }
        }

        public async Task<EntertainmentShowDTO> GetEntertainmentDTOByAddress(AddressGetDTO addressDto, int typeId = 0)
        {
            EntertaimentModel entertainment;

            if (typeId == 0)
            {
                entertainment = await _context.Entertaiments
                    .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartmentNumber
                    && x.Address.HouseNumber == addressDto.HouseNumber
                    && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                    && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude);
            }
            else if (0 < typeId && typeId < 4)
            {
                var type = _context.EntertainmentType.FirstOrDefault(x => x.Id == typeId);
                entertainment = await _context.Entertaiments
                    .FirstOrDefaultAsync(x => x.Address.ApartmentNumber == addressDto.ApartmentNumber
                    && x.Address.HouseNumber == addressDto.HouseNumber
                    && x.Address.Coordinates.Latitude == addressDto.Coordinates.Latitude
                    && x.Address.Coordinates.Longitude == addressDto.Coordinates.Longitude
                    && x.Type == type);
            }
            else
            {
                _logger.LogError("Error: Type ID isn't correct");
                throw new Exception("Type ID isn't correct");
            }

            if (entertainment == null)
            {
                return null;
            }
            else
            {
                var result = _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(entertainment);
                result.Type = entertainment.Type.Name;
                return result;
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