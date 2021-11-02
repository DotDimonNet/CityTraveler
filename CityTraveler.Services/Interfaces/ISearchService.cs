﻿using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ISearchService : IServiceMetadata
    {
        public Task<IEnumerable<ApplicationUserModel>> FilterUsers(FilterUsers user);
        public IEnumerable<TripModel> FilterTrips(FilterTrips trip);
        public IEnumerable<EntertaimentModel> FilterEntertainments(FilterEntertainment entertainment);
    }
}
