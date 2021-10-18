using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class CityArchitectureService //: ICityArchitectureService
    {
        /*private readonly IServiceContext _serviceContext;
        private readonly DbContext _dbContext;
        public bool IsActive { get; set; }
        public string Version { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public CityArchitectureService(IServiceContext serviceContext, DbContext dbContext)
        {
            _dbContext = dbContext;
            _serviceContext = serviceContext;
        }

        public async Task<bool> AddObjectToCity(IInstitution institution)
        {
            _dbContext.Institutions.Collection.Add(institution);

            var query = $"";

            var affectedObjects = await _dbContext.Institutions.RequestManager.SendRequestAsync(query, null, false);
            return affectedObjects > 0;
        }

        public async Task<bool> AddObjectToCity(IEvent ev)
        {
            _dbContext.Events.Collection.Add(ev);

            var query = $"";

            var affectedObjects = await _dbContext.Events.RequestManager.SendRequestAsync(query, null, false);
            return affectedObjects > 0;
        }

        public async Task<bool> AddObjectToCity(ILandskape landskape)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddStreet(IStreet street)
        {
            _dbContext.Streets.Collection.Add(street);

            var query = $"";

            var affectedObjects = await _dbContext.Streets.RequestManager.SendRequestAsync(query, null, false);
            return affectedObjects > 0;
        }

        public async Task<bool> BuildCityMap()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveObjectById(Guid objectId, PlaceType type)
        {
            switch (type)
            {
                case PlaceType.Event:
                    _dbContext.Events.Collection.RemoveAll(x => x.Id == objectId);
                    break;
                case PlaceType.Institution:
                    _dbContext.Institutions.Collection.RemoveAll(x => x.Id == objectId);
                    break;
                case PlaceType.Landscape:
                    _dbContext.Landskapes.Collection.RemoveAll(x => x.Id == objectId);
                    break;
                default:
                    break;
            }

            var query = $"";

            var affectedObjects = await _dbContext.Events.RequestManager.SendRequestAsync(query, null, false);
            return affectedObjects > 0;
        }

        public async Task<bool> RemoveStreet(Guid streetId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCityObject(IInstitution institute)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCityObject(IEvent ev)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCityObject(ILandskape landskape)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateStreet(IStreet street)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateCityMap()
        {
            throw new NotImplementedException();
        }*/
    }
}
