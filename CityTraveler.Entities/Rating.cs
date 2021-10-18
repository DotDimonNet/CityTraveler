using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    public class Rating : IRating
    {
        public double Value { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set ; }
        public DateTime Modified { get; set; }
        public Guid OwnerId { get; set; }

        public Rating(double value) 
        {
            Id = Guid.NewGuid();
            Value = value;
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        public Rating()
        {

        }
    }
}
