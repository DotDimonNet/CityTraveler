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

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public CityArchitectureService(ApplicationContext context, IMapper mapper, ILogger<CityArchitectureService> logger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddEntertainments(IEnumerable<EntertainmentGetDTO> entertaiments)
        {
            try
            {
                var models = new List<EntertaimentModel>();

                foreach (var entertainment in entertaiments)
                {
                    var model = _mapper.Map<EntertainmentGetDTO, EntertaimentModel>(entertainment);
                    model.Address.Street = await _context.Streets.FirstOrDefaultAsync(x => x.Id == entertainment.StreetId);
                    model.Type = _context.EntertainmentType.FirstOrDefault(x => x.Id == entertainment.Type);
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

        public async Task<bool> AddEntertainment(EntertainmentGetDTO entertainmentDTO)
        {
            try
            {
                var model = _mapper.Map<EntertainmentGetDTO, EntertaimentModel>(entertainmentDTO);
                model.Address.Street = await _context.Streets.FirstOrDefaultAsync(x => x.Id == entertainmentDTO.StreetId);
                model.Type = _context.EntertainmentType.FirstOrDefault(x => x.Id == entertainmentDTO.Type);

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

        public async Task<bool> UpdateEntertainment(EntertainmentUpdateDTO entertaimentDto)
        {
            try
            {
                var id = Guid.Parse(entertaimentDto.Id);
                var model = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id);
                
                if (model == null)
                {
                    var newModel = _mapper.Map<EntertainmentUpdateDTO, EntertaimentModel>(entertaimentDto);
                    _context.Entertaiments.Add(newModel);
                    _logger.LogInformation("Info: Entertainment was not found, but created");
                }
                else
                {
                    var updatedModel = _mapper.Map<EntertainmentUpdateDTO, EntertaimentModel>(entertaimentDto, model);
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

        public async Task<bool> ValidateEntertainments()
        {
            try
            {
                var entertainment = await _context.Entertaiments
                    .FirstOrDefaultAsync(x => x.Address == null
                    || string.IsNullOrEmpty(x.Title)
                    || x.Type == null);

                if (entertainment == null)
                {
                    return true;
                }
                else
                {
                    _logger.LogWarning("Warning: One of entertainments isn't correct");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new CityArchitectureServiceException($"Failed to validate entertainments: {ex.Message}");
            }
        }

        public async Task<bool> RemoveEntertainment(Guid id)
        {
            try
            {
                var entertainment = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == id);

                if (entertainment != null)
                {
                    _context.Entertaiments.Remove(entertainment);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning($"Entertainment was not found by id: {id}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new CityArchitectureServiceException($"Failed to remove entertainment: {ex.Message}");
            }
        }

        public async Task<bool> AddStreet(StreetGetDTO streetDto)
        {
            try
            {
                var street = _mapper.Map<StreetGetDTO, StreetModel>(streetDto);
                _context.Streets.Add(street);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new CityArchitectureServiceException($"Failed to add street: {ex.Message}");
            }
        }

        public async Task<bool> UpdateStreet(StreetDTO streetDto)
        {
            try
            {
                var model = await _context.Streets.FirstOrDefaultAsync(x => x.Id == streetDto.Id);
                var updatedModel = _mapper.Map<StreetDTO, StreetModel>(streetDto, model);
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

        public async Task<bool> ValidateStreets()
        {
            try
            {
                var address = await _context.Streets
                    .FirstOrDefaultAsync(x => x.StreetBeginning == x.StreetEnding
                    || string.IsNullOrEmpty(x.Title));

                if (address == null)
                {
                    return true;
                }
                else
                {
                    _logger.LogWarning("Warning: One of streets isn't correct");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new CityArchitectureServiceException($"Failed to validate streets: {ex.Message}");
            }
        }

        public async Task<bool> RemoveStreet(Guid streetId)
        {
            try
            {
                var street = await _context.Streets.FirstOrDefaultAsync(x => x.Id == streetId); 

                if (street == null)
                {
                    throw new CityArchitectureServiceException("Street not found");
                }

                _context.Streets.Remove(street);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new CityArchitectureServiceException($"Failed to remove street: {ex.Message}");
            }
        }

        public async Task<bool> ValidateAddresses()
        {
            try
            {
                var address = await _context.Addresses
                    .FirstOrDefaultAsync(x => x.Coordinates == null
                    || string.IsNullOrEmpty(x.HouseNumber)
                    || x.StreetId == Guid.Empty);

                if (address == null)
                {
                    return true;
                }
                else
                {
                    _logger.LogWarning("Warning: One of addresses isn't correct");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new CityArchitectureServiceException($"Failed to validate addresses: {ex.Message}");
            }
        }
    }
}