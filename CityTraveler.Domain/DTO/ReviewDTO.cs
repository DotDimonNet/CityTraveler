using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class ReviewDTO
    {
        public Guid RatingId { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Guid> Images { get; set; } = new List<Guid>();
        public ICollection<Guid> Comments { get; set; } = new List<Guid>();
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class EntertainmentReviewDTO : ReviewDTO
    {
        public Guid EntertainmentId { get; set; }
    }
    public class TripReviewDTO : ReviewDTO
    {
        public Guid TripId { get; set; }
    }
}
