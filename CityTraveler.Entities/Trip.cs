using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    public class Trip : ITrip
    {
        public DateTime TripStart { get ; set; }
        public DateTime TripEnd { get; set; }
        public IEnumerable<IInstitution> Institutions { get; set; }
        public IEnumerable<IEvent> Events { get; set; }
        public IEnumerable<ILandskape> Landskapes { get ; set ; }
        public IEnumerable<IRating> Ratings { get; set ; }
        public IUser Owner { get; set; }
        public IEnumerable<IReview> Reviews { get; set; }
        public ITrip Template { get; set; }
        public ConcurrentDictionary<Guid, IRoute> RoutesDictionary { get; set; }
        public IPrice Price { get; set; }
        public Guid Id { get; set ; }
        public DateTime Created { get; set; }
        public DateTime Modified { get ; set ; }
        public string Title { get ; set ; }
        public string Description { get; set; }
        public Guid TemplateId { get; set; }
        public double AverageRating { get; set; }
    }
}
