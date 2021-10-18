using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface ITrip : IDbData, IDescribable
    {
        public DateTime TripStart { get; set; }
        public DateTime TripEnd { get; set; }
        public IEnumerable<IInstitution> Institutions { get; set; }
        public IEnumerable<IEvent> Events { get; set; }
        public IEnumerable<ILandskape> Landskapes { get; set; }
        public IEnumerable<IRating> Ratings { get; set; }
        public IUser Owner { get; set; }
        public IEnumerable<IReview> Reviews { get; set; }
        public Guid TemplateId { get; set; }
        public ConcurrentDictionary<Guid, IRoute> RoutesDictionary { get; set; }
        public IPrice Price { get; set; }
        public double AverageRating { get; set; }
    }
}
