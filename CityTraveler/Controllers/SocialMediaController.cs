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

        [HttpPost("review-trip")]
        public async Task<IActionResult> AddReview([FromQuery] ReviewDTO review, Guid tripId)
        {
            var result = await _service.AddReviewTrip(tripId, review);
            return Json(result);
        }

        [HttpPost("comment")]
        public async Task<IActionResult> AddComment([FromQuery] CommentDTO comment)
        {
            var result = await _service.AddComment(comment);
            return Json(result);
        }

        [HttpPost("image")]
        public async Task<IActionResult> AddImage([FromQuery] ReviewImageDTO image)
        {
            var result = await _service.AddImage(image);
            return Json(result);
        }
        [HttpPut("comment")]
        public async Task<IActionResult> UpdateComment([FromQuery] Guid Id, CommentDTO comment)
        {
            var result = await _service.UpdateComment(Id, comment);
            return Json(result);
        }

        [HttpPut("image")]
        public async Task<IActionResult> UpdateReview([FromQuery] Guid Id, ReviewDTO review)
        {
            var result = await _service.UpdateReview(Id, review);
            return Json(result);
        }

        [HttpDelete("review")]
        public async Task<IActionResult> DeleteReview([FromQuery] Guid reviewId)
        {
            var result = await _service.RemoveReview(reviewId);
            return Json(result);
        }

        [HttpDelete("comment")]
        public async Task<IActionResult> DeleteComment([FromQuery] Guid commentId)
        {
            var result = await _service.RemoveComment(commentId);
            return Json(result);
        }

        [HttpDelete("image")]
        public async Task<IActionResult> DeleteImage([FromQuery] Guid reviewImageId)
        {
            var result = await _service.RemoveImage(reviewImageId);
            return Json(result);
        }

        [HttpGet("by-id")]
        public async Task<IActionResult> GetReview([FromQuery] Guid reviewId)
        {
            var review = await _service.GetReviewById(reviewId);
            return (Json(review));
        }

        [HttpGet("by-title")]
        public async Task<IActionResult> GetReviewByTitle([FromQuery] string title)
        {
            var review = await _service.GetReviewsByTitle(title);
            return (Json(review));

        }

        [HttpGet("by-average-raiting")]
        public async Task<IActionResult> GetReviewsByAverageRaiting([FromQuery] double raiting)
        {
            var review = await _service.GetReviewsByAverageRating(raiting);
            return (Json(review));
        }

        [HttpGet("by-description")]
        public async Task<IActionResult> GetReviewsByDescription([FromQuery] string description)
        {
            var reviews = await _service.GetReviewsByDescription(description);
            return (Json(reviews));
        }

        [HttpGet("reviews")]
        public async Task<IActionResult> GetReviewsByDescription([FromQuery] int skip, int take)
        {
            var reviews = await _service.GetReviews(skip, take);
            return (Json(reviews));
        }
    }
}