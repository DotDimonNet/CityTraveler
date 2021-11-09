using AutoMapper;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Mapping
{
    public class ReviewMapping : Profile
    {
        public ReviewMapping()
        {
            CreateMap<RatingDTO, RatingModel>().
                ForMember(x=>x.ReviewId, o=>o.Ignore());
            CreateMap<CommentDTO, CommentModel>().
                ForMember(x=>x.Status, o=>o.Ignore());
            CreateMap<CommentModel, CommentDTO>().
               ForMember(x => x.Status, o => o.Ignore());
            CreateMap<ReviewDTO, ReviewModel>().
                ForMember(x=>x.RatingId, o=>o.Ignore()).
                ForMember(x=>x.UserId, o=>o.Ignore()).
                ReverseMap();
            CreateMap<ReviewImageDTO, ReviewImageModel>();
            CreateMap<ReviewDTO, ReviewModel>().
                ForMember(x => x.Images, o => o.Ignore()).
                ForMember(x => x.Comments, o => o.Ignore());
            CreateMap<ReviewModel, ReviewDTO>().
                ForMember(x => x.Images, o => o.Ignore()).
                ForMember(x => x.Comments, o => o.Ignore());
            CreateMap<ReviewDTO, ReviewModel>().
                ReverseMap();
            CreateMap<ReviewDTO, ReviewModel>().
                ReverseMap();
            CreateMap<IEnumerable<EntertainmentGetDTO>, IEnumerable<EntertaimentModel>>();
            CreateMap<IEnumerable<TripDTO>, IEnumerable<TripModel>>();
            CreateMap< IEnumerable < UserDTO > , IEnumerable <ApplicationUserModel>>();
        }
    }
}
