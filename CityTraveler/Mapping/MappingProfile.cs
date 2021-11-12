using AutoMapper;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
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
            CreateMap<PriceModel, PriceDTO>();
            CreateMap<StreetGetDTO, StreetModel>();
            CreateMap<StreetModel, StreetShowDTO>();
            CreateMap<StreetModel, StreetDTO>().ReverseMap();
            CreateMap<AddressModel, AddressShowDTO>();
            CreateMap<PriceDTO, EntertaimentPriceModel>();
            CreateMap<CoordinatesDTO, CoordinatesModel>();
            CreateMap<ReviewImageModel, ImageDTO>();
            CreateMap<ImageGetDTO, EntertaimentImageModel>();
            CreateMap<ImageGetDTO, ImageModel>().ReverseMap();
            CreateMap<ImageDTO, ImageModel>().ReverseMap();
            CreateMap<AddressGetDTO, AddressModel>()
                .ForMember(x => x.StreetId, o => o.Ignore());
            CreateMap<ReviewModel, ReviewPreviewDTO>()
                .ForMember(x => x.MainImage, o => o.MapFrom(z => z.Images.FirstOrDefault(p => p.IsMain == true)))
                .ForMember(x => x.RatingValue, o => o.MapFrom(z => z.Rating.Value));
            CreateMap<TripModel, TripPrewievDTO>()
                .ForMember(x => x.MainImage, o => o.MapFrom(z => z.Images.FirstOrDefault(p => p.IsMain == true)));
            CreateMap<EntertaimentModel, EntertainmentShowDTO>()
                .ForMember(x => x.Type, o => o.MapFrom(z => z.Type.ToString()));
            CreateMap<EntertaimentModel, EntertainmentPreviewDTO>()
                .ForMember(x => x.Type, o => o.MapFrom(z => z.Type.ToString()))
                .ForMember(x => x.ReviewsCount, o => o.MapFrom(z => z.Reviews.Count()))
                .ForMember(x => x.TripsCount, o => o.MapFrom(z => z.Trips.Count()));
            CreateMap<EntertainmentGetDTO, EntertaimentModel>()
                .ForMember(x => x.Address, o => o.MapFrom(z => z.Address))
                .ForMember(x => x.Type, o => o.MapFrom(z => (EntertainmentType)z.Type));
            CreateMap<EntertainmentUpdateDTO, EntertaimentModel>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Type, o => o.MapFrom(z => (EntertainmentType)z.Type));
        }
    }
}
