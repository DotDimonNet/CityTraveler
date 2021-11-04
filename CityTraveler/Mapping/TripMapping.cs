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
<<<<<<< HEAD
            
            CreateMap<TripModel, DefaultTripDTO>().ReverseMap();
           
=======

            CreateMap<TripModel, DefaultTripDTO>();
            CreateMap<DefaultTripDTO, TripDTO>();

>>>>>>> 6694f9dfc711096bf70aee411350a3f1b232939e
            CreateMap<TripModel, TripDTO>().ReverseMap();
        }
    }
}
