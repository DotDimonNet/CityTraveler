using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Errors
{
    public static class ErrorMessages
    {
        //social media exceptions
        public static string imageNotFound = "Image not found";
        public static string commentNotFound = "Comment not found";
        public static string reviewNotFound = "Review not found";
        public static string failedToRemoveReview = "Failed to remove review";
        public static string failedToUpdateReview = "Failed to update review";
        public static string failedToAddReview = "Failed to add review";
        public static string failedToRemoveComment = "Failed to remove comment";
        public static string failedToPostRating = "Failed to post rating";
        public static string failedToRemoveRating = "Failed to remove rating";
        public static string failedToUpdateRating = "Failed to update rating";
        public static string failedToAddComment = "Failed to add comment";
        public static string failedToUpdateComment = "Failed to update comment";
        public static string failedToGetReviews = "Failed to gate reviews";
        public static string failedToGetComments = "Failed to gate comments";
        public static string failedToGetImages = "Failed to gate images";
        //search service exceptions
        public static string failedToFilterUsers = "Failed to filter users";
        public static string failedToFilterTrips = "Failed to filter trips";
        public static string failedToFilterEntertainments = "Failed to filter entertainments";
        public static string priceMoreMorePriceLess = "PriceMore can`t be more than priceLess";
        public static string ratingMoreMoreratingLess = "RatingMore can`t be more than ratingLess";
        public static string ratingMoreLessZero = "RatingMore can`t be less that 0";
        public static string ratingLessLessZero = "RatingLess can`t be less that 0";
        public static string priceMoreLessZero = "PriceMore can`t be less that 0";
        public static string priceLessLessZero = "PriceLess can`t be less that 0";
    }
}
