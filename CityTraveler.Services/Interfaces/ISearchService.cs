using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ISearchService
    {
        public Task<IEnumerable<UserDTO>> FilterUsers(FilterUsers user);
        public Task<IEnumerable<TripDTO>> FilterTrips(FilterTrips trip);
        public Task<IEnumerable<EntertainmentGetDTO>> FilterEntertainments(FilterEntertainment entertainment);

    }
}
