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

        [HttpPost("review-entertainment")]
        public async Task<IActionResult> AddReview(EntertainmentReviewDTO review, Guid entertainmentId)
        {
            return Json(await _service.AddReviewEntertainment(entertainmentId, review)) ;
        }

        [HttpPost("review-trip")]
        public async Task<IActionResult> AddReview(TripReviewDTO review, Guid tripId)
        {
            return Json(await _service.AddReviewTrip(tripId, review));
        }

        [HttpPost("comment")]
        public async Task<IActionResult> AddComment(CommentDTO comment)
        {
            return Json(await _service.AddComment(comment));
        }

        [HttpPost("image")]
        public async Task<IActionResult> AddImage(ReviewImageDTO image)
        {
            return Json(await _service.AddImage(image));
        }
        [HttpPut("comment")]
        public async Task<IActionResult> UpdateComment(CommentModel comment)
        {
            return Json(await _service.UpdateComment(comment));
        }

        [HttpPut("image")]
        public async Task<IActionResult> UpdateReview(ReviewModel review)
        {
            return Json(await _service.UpdateReview(review));
        }

        [HttpDelete("review")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            return Json(await _service.RemoveReview(reviewId));
        }

        [HttpDelete("comment")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            return Json(await _service.RemoveComment(commentId));
        }

        [HttpDelete("image")]
        public async Task<IActionResult> DeleteImage(Guid reviewImageId)
        {
            return Json(await _service.RemoveImage(reviewImageId));
        }

        [HttpGet("by-id")]
        public async Task<IActionResult> GetReview(Guid reviewId)
        {
            ReviewDTO review = await _service.GetReviewById(reviewId);
            return (Json(review));
        }

        [HttpGet("by-title")]
        public IActionResult GetReviewByTitle(string title)
        {
            IEnumerable<ReviewDTO> review =_service.GetReviewsByTitle(title);
            return (Json(review));

        }

        [HttpGet("by-average-raiting")]
        public IActionResult GetReviewsByAverageRaiting(double raiting)
        {
            IEnumerable<ReviewDTO> review = _service.GetReviewsByAverageRating(raiting);
            return (Json(review));
        }
        [HttpGet("by-description")]
        public IActionResult GetReviewsByDescription([FromQuery] string description)
        {
            IEnumerable<ReviewDTO> reviews = _service.GetReviewsByDescription(description);
            return (Json(reviews));
        }
        [HttpGet("reviews")]
        public IActionResult GetReviewsByDescription([FromQuery] int skip, int take)
        {
            IEnumerable<ReviewDTO> reviews = _service.GetReviews(skip,take);
            return (Json(reviews));
        }

    }
}