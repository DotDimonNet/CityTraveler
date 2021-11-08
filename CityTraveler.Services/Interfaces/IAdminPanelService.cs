using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Filters;
using CityTraveler.Domain.Filters.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IAdminPanelService : IServiceMetadata
    {
        public Task<IEnumerable<UserChangeAdminDTO>> FilterUsers(FilterAdminUser filter);
        public Task<IEnumerable<EntertaimentModel>> FilterEntertaiments(FilterAdminEntertaiment filter);
        public Task<IEnumerable<TripDTO>> FilterTrips(FilterAdminTrip filter);
        public Task<IEnumerable<AddressShowDTO>> FindAdressStreets(FilterAdminStreet filter);
        public Task<IEnumerable<ReviewDTO>> FilterReview(FilterAdminReview filter);

    }
}
