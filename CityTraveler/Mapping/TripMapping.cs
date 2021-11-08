using AutoMapper;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CityTraveler.Mapping
{
    public class TripMapping:Profile
    {
        public TripMapping()
        {
            CreateMap<AddNewTripDTO, TripModel>().ReverseMap();

            
            CreateMap<TripModel, DefaultTripDTO>().ReverseMap();

            CreateMap<TripModel, TripDTO>().ReverseMap();
        }
    }
}
