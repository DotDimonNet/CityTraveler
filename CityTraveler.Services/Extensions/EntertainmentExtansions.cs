using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Extensions
{
    public static class EntertainmentExtansions
    {
        public static EntertaimentModel UpdateEntertainmentWith(this EntertaimentModel model, EntertaimentModel source)
        {
            model.Title = source.Title;
            model.Address = source.Address;
            model.AveragePrice = source.AveragePrice;
            model.AverageRating = source.AverageRating;
            model.Description = source.Description;
            model.Images = source.Images;
            model.Reviews = source.Reviews;
            model.Trips = source.Trips;
            model.Type = source.Type;
            return model;
        }
    }
}
