using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    public class User : IUser
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
        public bool Blocked { get; set; }
        public UserRole Role { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsOnline { get; set; }
    }
}
