using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;

namespace CityTraveler.Domain.DTO
{
    public class EntertainmentGetDTO
    {
        public AddressGetDTO Address { get; set; }
        public int Type { get; set; }
        public PriceDTO AveragePrice { get; set; }
        IEnumerable<ImageGetDTO> Images { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }

    public class EntertainmentShowDTO
    {
        public AddressShowDTO Address { get; set; }
        public string Type { get; set; }
        public PriceDTO AveragePrice { get; set; }
        public double AverageRating { get; set; }
        public IEnumerable<TripPrewievDTO> Trips { get; set; }
        public IEnumerable<ReviewPreviewDTO> Reviews { get; set; }
        IEnumerable<ImageShowDTO> Images { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid Id { set; get; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }

    public class EntertainmentPreviewDTO
    {
        public AddressShowDTO Address { get; set; }
        public string Type { get; set; }
        public PriceDTO AveragePrice { get; set; }
        public double AverageRating { get; set; }
        public int ReviewsCount { get; set; }
        public int TripsCount { get; set; }
        ImageShowDTO MainImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid Id { set; get; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }

    public class EntertainmentUpdateDTO
    {
        public string Id { get; set; }
        public AddressGetDTO Address { get; set; }
        public int Type { get; set; }
        public PriceDTO AveragePrice { get; set; }
        public  IEnumerable<ImageGetDTO> Images { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}
