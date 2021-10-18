using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    public class Landskape : ILandskape
    {
        public IEnumerable<IImage> Images { get ; set ; }
        public IEnumerable<IReview> Reviews { get ; set ; }
        public IAddress Address { get ; set ; }
        public Guid Id { get ; set ; }
        public DateTime Created { get ; set ; }
        public DateTime Modified { get ; set ; }
        public string Title { get ; set ; }
        public string Description { get ; set ; }

        public Landskape(IEnumerable<IImage> images, IEnumerable<IReview>reviews, IAddress address, Guid guid, DateTime created, DateTime modified, string title,
            string description)
        {
            Images = images;
            Reviews = reviews;
            Address = address;
            Id = guid;
            Created = created;
            Modified = modified;
            Title = title;
            Description = description;
        }

        public Landskape()
        {

        }
    }
}
