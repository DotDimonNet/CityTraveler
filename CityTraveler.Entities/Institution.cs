using CityTraveler.Infrastructure.Enums;
using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CityTraveler.Entities
{
    public class Institution : IInstitution
    {
        public InstitutionType Type { get ; set ; }
        public IEnumerable<IImage> Images { get ; set ; }
        public IEnumerable<IReview> Reviews { get ; set ; }
        public IAddress Address { get ; set ; }
        public Dictionary<DayOfWeek, DateTime> Open { get ; set ; }
        public Dictionary<DayOfWeek, DateTime> Closed { get ; set ; }
        public IEnumerable<IPrice> Prices { get ; set ; }
        public Guid Id { get ; set ; }
        public DateTime Created { get ; set ; }
        public DateTime Modified { get ; set ; }
        public string Title { get ; set ; }
        public string Description { get ; set ; }

        public Institution(InstitutionType type, IEnumerable<IImage> images, IEnumerable<IReview> reviews, IAddress address, IEnumerable<Price> prices, Guid guid,
            Dictionary<DayOfWeek, DateTime> open, Dictionary<DayOfWeek, DateTime> close, string title, string description)
        {
            Type = type;
            Images = images;
            Reviews = reviews;
            Address = address;
            Open = open;
            Closed = close;
            Prices = prices;
            Id = guid;
            Created = DateTime.Now;
            Modified = DateTime.Now;
            Title = title;
            Description = description;
        }

        public Institution()
        {

        }
    }
}
