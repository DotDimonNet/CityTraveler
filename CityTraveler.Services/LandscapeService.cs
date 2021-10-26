using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Domain.Errors;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;

namespace CityTraveler.Services
{
    public class LandscapeService : IEventService
    {
        ApplicationContext _context;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public LandscapeService(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<EntertaimentModel> GetAll()
        {
            return (IEnumerable<EntertaimentModel>)_context.Entertaiments.Where(x => x.Type == EntertainmentType.Landskape);
        }

        public IEnumerable<EntertaimentModel> GetEntartainmentByMinRating(int averageRating)
        {
            var ES = new EntertainmentService(_context);
            return ES.GetEntartainmentByMinRating(averageRating).Where(x => x.Type == EntertainmentType.Landskape);
        }
        public IEnumerable<EntertaimentModel> GetEntartainmentByMaxRating(int averageRating)
        {
            var ES = new EntertainmentService(_context);
            return ES.GetEntartainmentByMaxRating(averageRating).Where(x => x.Type == EntertainmentType.Landskape);
        }

        public async Task<EntertaimentModel> GetEntertainmentByCoordinates(CoordinatesModel coordinates)
        {
            var ES = new EntertainmentService(_context);
            var result = await ES.GetEntertainmentByCoordinates(coordinates);
            if (result != null || result.Type == EntertainmentType.Landskape)
            {
                return result;
            }
            else
            {
                return default;
            }
        }

        public async Task<EntertaimentModel> GetEntertainmentById(Guid id)
        {
            var ES = new EntertainmentService(_context);
            var result = await ES.GetEntertainmentById(id);
            if (result != null || result.Type == EntertainmentType.Landskape)
            {
                return result;
            }
            else
            {
                return default;
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(StreetModel street)
        {
            var ES = new EntertainmentService(_context);
            var result = (List<EntertaimentModel>)ES.GetEntertainmentsByStreet(street);
            foreach (var entertainment in result)
            {
                if (entertainment.Type != EntertainmentType.Landskape)
                {
                    result.Remove(entertainment);

                }
            }
            return result;
        }
        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(string streetTitle)
        {
            var ES = new EntertainmentService(_context);
            var result = (List<EntertaimentModel>)ES.GetEntertainmentsByStreet(streetTitle);
            foreach (var entertainment in result)
            {
                if (entertainment.Type != EntertainmentType.Landskape)
                {
                    result.Remove(entertainment);

                }
            }
            return result;
        }

        public async Task<EntertaimentModel> GetEntertainmentByTitle(string title)
        {
            var ES = new EntertainmentService(_context);
            var result = await ES.GetEntertainmentByTitle(title);
            if (result != null || result.Type == EntertainmentType.Landskape)
            {
                return result;
            }
            else
            {
                return default;
            }
        }

        public IEnumerable<EntertaimentModel> GetEntertainments(IEnumerable<Guid> ids)
        {
            var ES = new EntertainmentService(_context);
            var result = (List<EntertaimentModel>)ES.GetEntertainments(ids);
            foreach (var entertainment in result)
            {
                if (entertainment.Type != EntertainmentType.Landskape)
                {
                    result.Remove(entertainment);

                }
            }
            return result;
        }

        public async Task<EntertaimentModel> GetEntertainmentByAddress(AddressModel address)
        {
            var ES = new EntertainmentService(_context);
            var result = await ES.GetEntertainmentByAddress(address);
            if (result != null || result.Type == EntertainmentType.Landskape)
            {
                return result;
            }
            else
            {
                return default;
            }
        }
        public async Task<EntertaimentModel> GetEntertainmentByAddress(string houseNumber, string apartmentNumber, string streetTitle)
        {
            var ES = new EntertainmentService(_context);
            var result = await ES.GetEntertainmentByAddress(houseNumber, apartmentNumber, streetTitle);
            if (result != null || result.Type == EntertainmentType.Landskape)
            {
                return result;
            }
            else
            {
                return default;
            }
        }

        public double GetAverageRating(EntertaimentModel entertaiment)
        {
            var ES = new EntertainmentService(_context);
            var result = ES.GetAverageRating(entertaiment);
            return result;
        }

        public double GetAveragePrice(EntertaimentModel entertaiment)
        {
            var ES = new EntertainmentService(_context);
            var result = ES.GetAveragePrice(entertaiment);
            return result;
        }

        //TODO
        public IEnumerable<EntertaimentModel> GetEventByBeginingDay(DateTime begin)
        {
            throw new NotImplementedException();
        }
    }
}
