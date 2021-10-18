using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    public class Review : IReview
    {
        public IRating Rating { get; set; }
        public IEnumerable<IImage> Images { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid OwnerID { get; set; }
        public Guid ObjectId { get; set; }
        public IEnumerable<Guid> Likes { get; set; }

        public Review(IRating rating, IEnumerable<IImage> images, string title, string description)
        {
            Rating = rating;
            Images = images;
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        public Review()
        {
                
        }
    }
}
