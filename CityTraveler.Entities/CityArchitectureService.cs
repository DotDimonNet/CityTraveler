using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    public class CityArchitectureService : ICityArchitectureService
    {
        public IEnumerable<IEntertaiment> Entertaiments { get; set; } = new List<IEntertaiment>();
        public IEnumerable<IEvent> Events { get; set; } = new List<IEvent>();
        public IEnumerable<ILandskape> Landskapes { get; set; } = new List<ILandskape>();
        public IEnumerable<IStreet> Streets { get; set; } = new List<IStreet>();
        public bool IsActive { get; set; }
        public string Version { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // I will update this service during the day))
        public bool AddObjectToCity(IEntertaiment entertaiment)
        {
            Entertaiments = Entertaiments.Append(entertaiment);
            return true;
        }

        public bool AddObjectToCity(IEvent ev)
        {
            Events = Events.Append(ev);
            return true;
        }

        public bool AddObjectToCity(ILandskape landskape)
        {

            Landskapes = Landskapes.Append(landskape);
            return true;
        }

        public bool AddStreet(IStreet street)
        {
            Streets = Streets.Append(street);
            return true;
        }

        public bool RemoveObjectById(Guid objectId)
        {
            Streets = Streets.Where(x => x.Id != objectId);
            Entertaiments = Entertaiments.Where(x => x.Id != objectId);
            Events = Events.Where(x => x.Id != objectId);
            Landskapes = Landskapes.Where(x => x.Id != objectId);
            return true;
        }

        public bool RemoveStreet(Guid streetId)
        {
            Streets = Streets.Where(x => x.Id != streetId);
            return true;
        }

        public bool UpdateEntertaiment(IEntertaiment entertaiment)
        {
            Entertaiments = Entertaiments.Where(x => x.Id != entertaiment.Id);
            Entertaiments.Append(entertaiment);
            return true;
        }

        public bool UpdateStreet(IStreet street)
        {
            Streets = Streets.Where(x => x.Id != street.Id);
            Streets.Append(street);
            return true;
        }

        public bool ValidateCityMap()
        {
            return Streets.Count() > 10;
        }
    }
}
