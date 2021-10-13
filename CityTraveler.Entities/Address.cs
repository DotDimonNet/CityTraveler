using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTravel.Entities
{
    public class Address : IAddress
    {
        public ICoordinates Coordinates { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string HouseNumber { get; set; }
        public IStreet Street { get; set; }
        public Address(ICoordinates coordinates, string title, string description, Guid ID,
            DateTime created, string houseNum, IStreet street)
        {
            Coordinates = coordinates;
            Title = title;
            Description = description;
            Id = ID;
            Created = created;
            Modified = created;
            HouseNumber = houseNum;
            Street = street;
        }

    }
}