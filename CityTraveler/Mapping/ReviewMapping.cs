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
                ForMember(x => x.ReviewId, o => o.Ignore());
            CreateMap<CommentDTO, CommentModel>().
                ForMember(x => x.Status, o => o.Ignore());
            CreateMap<CommentModel, CommentDTO>().
               ForMember(x => x.Status, o => o.Ignore());
            CreateMap<ReviewDTO, ReviewModel>().
                ForMember(x => x.RatingId, o => o.Ignore()).
                ForMember(x => x.UserId, o => o.Ignore()).
                ReverseMap();
            CreateMap<ReviewImageDTO, ReviewImageModel>();
            CreateMap<EntertainmentReviewDTO, EntertainmentReviewModel>().
                ReverseMap();
            CreateMap<TripReviewDTO, TripReviewModel>().
                ReverseMap();
            CreateMap<EntertainmentGetDTO, EntertaimentModel>().ReverseMap();
            CreateMap<IEnumerable<TripDTO>, IEnumerable<TripModel>>();
            CreateMap<IEnumerable<UserDTO>, IEnumerable<ApplicationUserModel>>();
        }
    }
}
