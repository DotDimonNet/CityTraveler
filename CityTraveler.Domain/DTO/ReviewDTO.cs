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
        public ICollection<ImageGetDTO> Images { get; set; } = new List<ImageGetDTO>();
        public ICollection<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid EntertainmentId { get; set; }
        public Guid TripId { get; set; }

    }

    public class ReviewPreviewDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public ImageDTO MainImage { get; set; }
        public DateTime Modified { get; set; }
        public double RatingValue { get; set; }
    }
}
