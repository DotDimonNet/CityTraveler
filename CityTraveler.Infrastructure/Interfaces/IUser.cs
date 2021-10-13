using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IUser : IDbData
    {
        public IImage Avatar { get; set; }
        public IEnumerable<IImage> Images { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public IContactInfo ContactInfo { get; set; }
        public IEnumerable<ITrip> HistoryTrips { get; set; }
        public IEnumerable<ITrip> ActiveTrips { get; set; }
    }
}
