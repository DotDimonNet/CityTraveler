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
    public class CityArchitectureService : ICityArchitectureService
    {
        private readonly ILogger<CityArchitectureService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper; 

        public CityArchitectureService(ApplicationContext context, IMapper mapper, ILogger<CityArchitectureService> logger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddEntertainmentsAsync(IEnumerable<EntertainmentGetDTO> entertaiments)
        {
            try
            {
                var models = new List<EntertaimentModel>();

                foreach (var entertainment in entertaiments)
                {
                    var streetId = Guid.Parse(entertainment.Address.StreetId);
                    var model = _mapper.Map<EntertainmentGetDTO, EntertaimentModel>(entertainment);
                    model.Address.Street = await _context.Streets.FirstOrDefaultAsync(x => x.Id == streetId);
                    models.Add(model);
                }

                _context.Entertaiments.AddRange(models);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new CityArchitectureServiceException($"Failed to set entertainments: {ex.Message}");
            }
        }

        public async Task<bool> AddEntertainmentAsync(EntertainmentGetDTO entertainmentDTO)
        {
            try
            {
                var streetId = Guid.Parse(entertainmentDTO.Address.StreetId);
                var model = _mapper.Map<EntertainmentGetDTO, EntertaimentModel>(entertainmentDTO);
                model.Address.Street = await _context.Streets.FirstOrDefaultAsync(x => x.Id == streetId);

                _context.Entertaiments.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new CityArchitectureServiceException($"Failed to add entertainment: {ex.Message}");
            }
        }

        public async Task<bool> UpdateEntertainmentAsync(EntertainmentUpdateDTO entertaimentDto)
        {
            try
            {
                var id = Guid.Parse(entertaimentDto.Id);
                var model = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id);
                
                if (model == null)
                {
                    var newModel = _mapper.Map<EntertainmentUpdateDTO, EntertaimentModel>(entertaimentDto);
                    newModel.Address.Street = _context.Streets
                        .FirstOrDefault(x => x.Id == Guid.Parse(entertaimentDto.Address.StreetId));
                    _context.Entertaiments.Add(newModel);
                    _logger.LogInformation("Info: Entertainment was not found, but created");
                }
                else
                {
                    var updatedModel = _mapper.Map<EntertainmentUpdateDTO, EntertaimentModel>(entertaimentDto, model);
                    updatedModel.Address.Street = _context.Streets
                        .FirstOrDefault(x => x.Id == Guid.Parse(entertaimentDto.Address.StreetId));
                    _context.Entertaiments.Update(updatedModel);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new CityArchitectureServiceException($"Failed to update entertainment: {ex.Message}");
            }
        }

        public bool ValidateEntertainments()
        {
            try
            {
                var entertainments = _context.Entertaiments
                    .Where(x => x.Address == null
                    || string.IsNullOrEmpty(x.Title));

                if (entertainments.Any())
                {
                    var result = entertainments.Join(entertainments,
                        x => x.Id.ToString(),
                        y => y.Title,
                        (x, y) => new { Title = y, ID = x });
                    _logger.LogWarning($"Warning: One or more entertainments aren't correct:\n{result}");
                    return false;
                }

                _logger.LogInformation("Info: Validation was finished. All entertainments are correct.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveEntertainmentAsync(Guid id)
        {
            try
            {
                var entertainment = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id);

                if (entertainment != null)
                {
                    _context.Entertaiments.Remove(entertainment);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Info: The entertainment with ID - {id} was removed");
                    return true;
                }

                _logger.LogWarning($"Entertainment was not founded by id - {id}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddStreetAsync(StreetGetDTO streetDto)
        {
            try
            {
                var street = _mapper.Map<StreetGetDTO, StreetModel>(streetDto);

                try
                {
                    _context.Streets.Add(street);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Info: {street.Title} street was created");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error: {ex.Message}");
                    throw new CityArchitectureServiceException($"Failed to add street: {ex.Message}");
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("Warning: Passed not correct SrteetGetDTO into adding method");
                return false;
            }
        }

        public async Task<bool> UpdateStreetAsync(StreetDTO streetDto)
        {
            try
            {
                var model = await _context.Streets.FirstOrDefaultAsync(x => x.Id == streetDto.Id);
                var updatedModel = _mapper.Map<StreetDTO, StreetModel>(streetDto, model);

                try
                {
                    _context.Streets.Update(updatedModel);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error: {ex.Message}");
                    throw new CityArchitectureServiceException($"Failed to update street: {ex.Message}");
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("Warning: Passed not correct SrteetDTO into updating method");
                return false;
            }
        }

        public bool ValidateStreets()
        {
            try
            {
                var streets = _context.Streets
                    .Where(x => string.IsNullOrEmpty(x.Title));

                if (streets.Any())
                {
                    var result = streets.Join(streets,
                        x => x.Id.ToString(),
                        y => y.Title,
                        (x, y) => new { Title = y, ID = x });
                    _logger.LogWarning($"Warning: One or more streets aren't correct:\n{result}");
                    return false;
                }

                _logger.LogInformation("Info: Validation was finished. All streets are correct.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveStreetAsync(Guid streetId)
        {
            try
            {
                var street = await _context.Streets.FirstOrDefaultAsync(x => x.Id == streetId); 

                if (street != null)
                {
                    _context.Streets.Remove(street);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Info: The street with ID - {streetId} was removed");
                    return true;
                }

                _logger.LogWarning($"Street was not founded by id - {streetId}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return false;
            }
        }

        public bool ValidateAddresses()
        {
            try
            {
                var addresses = _context.Addresses
                    .Where(x => x.Coordinates == null
                    || string.IsNullOrEmpty(x.HouseNumber)
                    || x.StreetId == Guid.Empty);

                if (addresses.Any())
                {
                    var result = addresses.Join(addresses,
                        x => $"{x.Street.Title} - {x.HouseNumber} - {x.ApartmentNumber}",
                        y => y.Id.ToString(),
                        (x, y) => new { Title = x, ID = y });
                    _logger.LogWarning($"Warning: One or more streets aren't correct:\n{result}");
                    return false;
                }

                _logger.LogInformation("Info: Validation was finished. All addresses are correct.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return false;
            }
        }
    }
}