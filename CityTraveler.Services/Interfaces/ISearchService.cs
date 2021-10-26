using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ISearchService : IServiceMetadata
    {
        public IEnumerable<ApplicationUserModel> FilterUsers(FilterUsers user);
        public IEnumerable<TripModel> FilterTrips(FilterTrips trip);
        public IEnumerable<EntertaimentModel> FilterEntertainments(FilterEntertainment entertainment);
        //public IEnumerable<ApplicationUserModel> FilterUsersAlike(UserProfileModel u);
        public IEnumerable<TripModel> FilterTripsAlike(TripModel t);
        public IEnumerable<EntertaimentModel> FilterEntertainmentsAlike(EntertaimentModel e);
    }
}
