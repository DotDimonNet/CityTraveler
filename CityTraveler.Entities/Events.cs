using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    public class Event :IEvent
    {
        public DateTime Start { get; set ; }
        public DateTime End { get; set; }
        public IEnumerable<IImage> Images { get; set; }
        public IEnumerable<IReview> Reviews { get; set; }
        public IAddress Address { get; set; }
        public Dictionary<DayOfWeek, DateTime> Open { get; set; }
        public Dictionary<DayOfWeek, DateTime> Closed { get; set; }
        public IEnumerable<IPrice> Prices { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Title { get ; set; }
        public string Description { get; set; }
        public Event(DateTime start,DateTime end, IEnumerable<IImage> images, IEnumerable<IReview> reviews, IAddress address, 
            Dictionary<DayOfWeek, DateTime> open, Dictionary<DayOfWeek, DateTime> closed, IEnumerable<IPrice> prices, Guid ID,
            DateTime created, string title, string description)
        {
            Start = start;
            End = end;
            Images = images;
            Reviews = reviews;
            Address = address;
            Open = open;
            Closed = closed;
            Prices = prices;
            Id = ID;
            Created = created;
            Modified = created;
            Title = title;
            Description = description;
        }

        public Event()
        {

        }
    }
}
