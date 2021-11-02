using AutoMapper;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CoordinatesDTO, CoordinatesModel>();
            CreateMap<EntertainmentAddressDTO, AddressModel>()
                .ForMember(x => x.ApartmentNumber, o => o.MapFrom(z => z.ApartmentNumber))
                .ForMember(x => x.StreetId, o => o.Ignore());
            CreateMap<EntertainmentDTO, EntertaimentModel>()
                .ForMember(x => x.Address, o => o.MapFrom(z => z.Address))
                .ForMember(x => x.AveragePrice, o => o.Ignore())
                .ForMember(x => x.Type, o => o.Ignore());
        }
    }
}
