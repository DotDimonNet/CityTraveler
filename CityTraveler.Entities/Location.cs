using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTravel.Entities
{
    public abstract class Location
    {
        // Name, address, coordinates, description, feedback, ID
        public string Name { set; get; }
        public Address Address { set; get; }
        public IEnumerable<double> Coordinates { set; get; }
        public string Description { set; get; }
        public IEnumerable<Comment> Feedback { set; get; }
        public Guid ID { set; get; }
    }
}