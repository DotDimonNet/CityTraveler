using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class EntertainmentReviewDTO: ReviewDTO
    {
        public EntertaimentModel Entertaiment { get; set; }
    }
    public class TripReviewDTO : ReviewDTO
    {
        public TripModel Trip { get; set; }
    }
}
