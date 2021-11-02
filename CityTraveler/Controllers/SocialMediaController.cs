using CityTraveler.Services.Interfaces ;
using CityTraveler.Services;
using CityTraveler.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityTraveler.Domain.Errors;
using CityTraveler.Domain.DTO;

namespace CityTraveler.Controllers
{
    [ApiController]
    [Route("api/socialmedia")]
    public class SocialMediaController : Controller
    {
        private readonly ILogger<SocialMediaController> _logger;
        private readonly ISocialMediaService _service;

        public SocialMediaController(ILogger<SocialMediaController> logger, ISocialMediaService socialMediaService)
        {
            _service = socialMediaService;
            _logger = logger;
        }

        [HttpPut("add-review-entertainment")]
        public async Task<IActionResult> AddReview(EntertainmentReviewDTO review, Guid entertainmentId)
        {
            return Json(await _service.AddReviewEntertainment(entertainmentId, review)) ;
        }

        [HttpPut("add-review-trip")]
        public async Task<IActionResult> AddReview(TripReviewDTO review, Guid tripId)
        {
            return Json(await _service.AddReviewTrip(tripId, review));
        }

        [HttpPut("comment")]
        public async Task<IActionResult> AddComment(CommentDTO comment)
        {
            return Json(await _service.AddComment(comment));
        }

        [HttpPut("image")]
        public async Task<IActionResult> AddImage(ReviewImageModel image)
        {
            return Json(await _service.AddImage(image));
        }

        [HttpDelete("delete-review")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            return Json(await _service.RemoveReview(reviewId));
        }

        [HttpDelete("delete-comment")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            return Json(await _service.RemoveComment(commentId));
        }

        [HttpDelete("delete-image")]
        public async Task<IActionResult> DeleteImage(Guid reviewImageId)
        {
            return Json(await _service.RemoveImage(reviewImageId));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetReview(Guid reviewId)
        {
            ReviewModel review = await _service.GetReviewById(reviewId);
            return (Json(review));
        }

        [HttpGet("get-by-title")]
        public async Task<IActionResult> GetReviewByTitle(string title)
        {
            IEnumerable<ReviewModel> review =_service.GetReviewsByTitle(title);
            return (Json(review));

        }

        [HttpGet("get-reviews-by-average-raiting")]
        public async Task<IActionResult> GetReviewsByAverageRaiting(double raiting)
        {
            IEnumerable<ReviewModel> review = _service.GetReviewsByAverageRating(raiting);
            return (Json(review));
        }
        [HttpGet("get-reviews-by-description")]
        public async Task<IActionResult> GetReviewsByDescription([FromQuery] string description)
        {
            IEnumerable<ReviewModel> reviews = _service.GetReviewsByDescription(description);
            return (Json(reviews));
        }
        [HttpGet("get-reviews")]
        public async Task<IActionResult> GetReviewsByDescription([FromQuery] int skip, [FromQuery] int take)
        {
            IEnumerable<ReviewModel> reviews = _service.GetReviews(skip,take);
            return (Json(reviews));
        }

    }
}