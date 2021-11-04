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
            CreateMap<PriceModel, PriceDTO>();
            CreateMap<StreetGetDTO, StreetModel>();
            CreateMap<StreetModel, StreetShowDTO>();
            CreateMap<StreetModel, StreetDTO>();
            CreateMap<AddressModel, AddressShowDTO>();
            CreateMap<PriceDTO, EntertaimentPriceModel>();
            CreateMap<CoordinatesDTO, CoordinatesModel>();
            CreateMap<ImageGetDTO, EntertaimentImageModel>();
            CreateMap<AddressGetDTO, AddressModel>()
                .ForMember(x => x.StreetId, o => o.Ignore());
            CreateMap<EntertaimentModel, EntertainmentShowDTO>()
                .ForMember(x => x.Type, o => o.Ignore());
            CreateMap<TripModel, TripPrewievDTO>()
                .ForMember(x => x.MainImage, o => o.MapFrom(z => z.Images.Where(p => p.IsMain == true)));
            CreateMap<ReviewModel, ReviewPreviewDTO>()
                .ForMember(x => x.MainImage, o => o.MapFrom(z => z.Images.Where(p => p.IsMain == true)))
                .ForMember(x => x.RatingValue, o => o.MapFrom(z => z.Rating.Value));
            CreateMap<EntertaimentModel, EntertainmentPreviewDTO>()
                .ForMember(x => x.Type, o => o.MapFrom(z => z.Type.Name))
                .ForMember(x => x.ReviewsCount, o => o.MapFrom(z => z.Reviews.Count()))
                .ForMember(x => x.TripsCount, o => o.MapFrom(z => z.Trips.Count()));
            CreateMap<EntertainmentGetDTO, EntertaimentModel>()
                .ForMember(x => x.Address, o => o.MapFrom(z => z.Address))
                .ForMember(x => x.Type, o => o.Ignore());
            CreateMap<EntertainmentUpdateDTO, EntertaimentModel>()
                .ForMember(x => x.Address, o => o.Ignore())
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Type, o => o.Ignore());
        }
    }
}
