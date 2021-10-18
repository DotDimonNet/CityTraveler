using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ITripService : IServiceMetadata
    {
        public bool AddNewTrip(ITrip newTrip);

        public bool DeleteTrip(Guid tripId);

        public IEnumerable<ITrip> GetTrips(int skip = 0, int take = 10);

        public ITrip GetTripById(Guid tripId);

        public IEnumerable<ITrip> GetTripByPlace(Guid placeId);

        public Dictionary<Guid, Guid> GetTripByRating(Guid ratingId, Guid tripId);



    }
}
