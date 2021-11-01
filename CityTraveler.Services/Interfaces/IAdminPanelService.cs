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
    public interface IAdminPanelService
    {
        public Task<IEnumerable<ApplicationUserModel>> AdminFilterUsers(FilterAdminUser filter);
        public Task<IEnumerable<EntertaimentModel>> AdminFilterEntertaiments(FilterAdminEntertaiment filter);
        public Task<IEnumerable<TripModel>> AdminFilterTrips(FilterAdminTrip filter);
        public Task<IEnumerable<AddressModel>> AdminFilterStreets(FilterAdminStreet filter);
        public Task<IEnumerable<ReviewModel>> AdminFilterReview(FilterAdminReview filter);

    }
}
