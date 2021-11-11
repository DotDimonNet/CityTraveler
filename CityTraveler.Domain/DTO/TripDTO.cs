using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class TripDTO
    {
        public DateTime TripStart { get; set; } = new DateTime();
        public DateTime TripEnd { get; set; } = new DateTime();
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string TagSting { get; set; } = "";
        public TimeSpan OptimalSpent { get; set; } = new TimeSpan();
        public TimeSpan RealSpent { get; set; } = new TimeSpan();
        public double Price { get; set; } = 0.0;
        public double AverageRating { get; set; } = 0.0;
        public IEnumerable<ImageDTO> Images { get; set; } = new List<ImageDTO>();
        public IEnumerable<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
        public IEnumerable<EntertainmentGetDTO> Entertaiments { get; set; } = new List<EntertainmentGetDTO>();
    }

    public class DefaultTripDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string TagString { get; set; } = "";
        public double Price { get; set; } = 0.0;
        public double AverageRating { get; set; } = 0.0;
        public TimeSpan OptimalSpent { get; set; } = new TimeSpan();
        public IEnumerable<UserDTO> Users { get; set; } = new List<UserDTO>();
        public IEnumerable<ImageDTO> Images { get; set; } = new List<ImageDTO>();
        public IEnumerable<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
        public IEnumerable<EntertainmentGetDTO> Entertaiments { get; set; } = new List<EntertainmentGetDTO>();
        public bool DefaultTrip { get; set; } = true;
    }


    public class TripPrewievDTO 
    {
        public Guid Id { get; set; }
        public double AverageRating { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TagSting { get; set; }
        public TimeSpan OptimalSpent { get; set; }
        public ImageDTO MainImage { get; set; }
    }

    public class AddNewTripDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TripStart { get; set; }
        public IEnumerable<EntertainmentGetDTO> Entertaiments { get; set; } = new List<EntertainmentGetDTO>();
    }

    public class InfoTripDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TagSting { get; set; }
        public PriceDTO Price { get; set; }
        public double AverageRating { get; set; }
        public TimeSpan OptimalSpent { get; set; }
        public TimeSpan RealSpent { get; set; }
        public DateTime TripStart { get; set; }
        public DateTime TripEnd { get; set; }
        public IEnumerable<ImageDTO> Images { get; set; } = new List<ImageDTO>();
        public IEnumerable<ReviewPreviewDTO> Reviews { get; set; } = new List<ReviewPreviewDTO>();
        public IEnumerable<EntertainmentShowDTO> Entertaiments { get; set; } = new List<EntertainmentShowDTO>();
        public IEnumerable<UserDTO> Users { get; set; } = new List<UserDTO>();
    }
}
