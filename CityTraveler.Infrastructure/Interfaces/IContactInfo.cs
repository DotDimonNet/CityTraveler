using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IContactInfo
    {
        public string Phone { get; set; }
        public string WebsiteUrl { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Links { get; set; }
    }
}
