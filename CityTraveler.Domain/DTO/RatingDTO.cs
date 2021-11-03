using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class RatingDTO
    {
        public ReviewModel Review { get; set; }
        public ApplicationUserModel User { get; set; }
        public double Value { get; set; }
    }
}