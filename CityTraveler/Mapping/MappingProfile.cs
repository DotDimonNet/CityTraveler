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
            CreateMap<ImageGetDTO, EntertaimentImageModel>();
            CreateMap<PriceDTO, PriceModel>();
            CreateMap<PriceModel, PriceDTO>();
            CreateMap<CoordinatesDTO, CoordinatesModel>();
            CreateMap<StreetGetDTO, StreetModel>();
            CreateMap<StreetModel, StreetShowDTO>();
            CreateMap<AddressModel, AddressShowDTO>();
            CreateMap<AddressGetDTO, AddressModel>()
                .ForMember(x => x.StreetId, o => o.Ignore());
            CreateMap<EntertaimentModel, EntertainmentShowDTO>()
                .ForMember(x => x.Type, o => o.Ignore());
            CreateMap<EntertainmentGetDTO, EntertaimentModel>()
                .ForMember(x => x.Address, o => o.MapFrom(z => z.Address))
                .ForMember(x => x.AveragePrice, o => o.Ignore())
                .ForMember(x => x.Type, o => o.Ignore());
        }
    }
}
