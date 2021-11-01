using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Extensions
{
    public static class ReviewExtensions
    {
        public static ReviewModel ToReview(this ReviewDTO reviewDTO)
        {
            try
            {
                return new ReviewModel()
                {
                    Description = reviewDTO.Description,
                    Comments = reviewDTO.Comments,
                    Title = reviewDTO.Title,
                    Rating = reviewDTO.Rating,
                    RatingId = reviewDTO.Rating.Id,
                    Images = reviewDTO.Images,
                    User = reviewDTO.User,
                    UserId = reviewDTO.User.UserId
                };
            }
            catch (Exception)
            {
                throw new Exception("ReviewDTO isn't correct");
            }

        }
        public static EntertainmentReviewModel ToEntertainmentReview(this EntertainmentReviewDTO reviewDTO)
        {
            try
            {
                return new EntertainmentReviewModel()
                {
                    Description = reviewDTO.Description,
                    Comments = reviewDTO.Comments,
                    Title = reviewDTO.Title,
                    Rating = reviewDTO.Rating,
                    RatingId = reviewDTO.Rating.Id,
                    Images = reviewDTO.Images,
                    User = reviewDTO.User,
                    UserId = reviewDTO.User.UserId,
                    Entertaiment= reviewDTO.Entertaiment
                    
                };
            }
            catch (Exception)
            {
                throw new Exception("EntertainmentReviewDTO isn't correct");
            }

        }
        public static TripReviewModel ToTripReview(this TripReviewDTO reviewDTO)
        {
            try
            {
                return new TripReviewModel()
                {
                    Description = reviewDTO.Description,
                    Comments = reviewDTO.Comments,
                    Title = reviewDTO.Title,
                    Rating = reviewDTO.Rating,
                    RatingId = reviewDTO.Rating.Id,
                    Images = reviewDTO.Images,
                    User = reviewDTO.User,
                    UserId = reviewDTO.User.UserId,
                    Trip = reviewDTO.Trip
                };
            }
            catch (Exception)
            {
                throw new Exception("TripReviewDTO isn't correct");
            }

        }
    }
}
