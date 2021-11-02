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
        public async Task<IActionResult> AddReview(EntertainmentReviewModel review, Guid entertainmentId)
        {
            return Json(await _service.AddReviewEntertainment(entertainmentId, review)) ;
        }

        [HttpPut("add-review-trip")]
        public async Task<IActionResult> AddReview(TripReviewModel review, Guid tripId)
        {
            return Json(await _service.AddReviewTrip(tripId, review));
        }

        [HttpPut("comment")]
        public async Task<IActionResult> AddComment(CommentModel comment, Guid reviewId)
        {
            return Json(await _service.AddComment(comment, reviewId));
        }

        [HttpPut("image")]
        public async Task<IActionResult> AddComment(ReviewImageModel image, Guid reviewId)
        {
            _logger.LogInformation($"");
            return Json(await _service.AddImage(image, reviewId));
        }

        [HttpDelete("delete-review")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            return Json(await _service.RemoveReview(reviewId));
        }

        [HttpDelete("delete-comment")]
        public async Task<IActionResult> DeleteComment(Guid commentId, Guid reviewId)
        {
            return Json(await _service.RemoveComment(commentId, reviewId));
        }

        [HttpDelete("delete-image")]
        public async Task<IActionResult> DeleteImage(Guid reviewImageId, Guid reviewId)
        {
            return Json(await _service.RemoveImage(reviewImageId, reviewId));
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

    }
}