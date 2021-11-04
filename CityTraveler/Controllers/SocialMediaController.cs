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
        public async Task<IActionResult> AddReview([FromQuery] EntertainmentReviewDTO review, Guid entertainmentId)
        {
            return Json(await _service.AddReviewEntertainment(entertainmentId, review)) ;
        }

        [HttpPost("review-trip")]
        public async Task<IActionResult> AddReview([FromQuery] TripReviewDTO review, Guid tripId)
        {
            return Json(await _service.AddReviewTrip(tripId, review));
        }

        [HttpPost("comment")]
        public async Task<IActionResult> AddComment([FromQuery] CommentDTO comment)
        {
            return Json(await _service.AddComment(comment));
        }

        [HttpPost("image")]
        public async Task<IActionResult> AddImage([FromQuery] ReviewImageDTO image)
        {
            return Json(await _service.AddImage(image));
        }
        [HttpPut("comment")]
        public async Task<IActionResult> UpdateComment([FromQuery] Guid Id, CommentDTO comment)
        {
            return Json(await _service.UpdateComment(Id, comment));
        }

        [HttpPut("image")]
        public async Task<IActionResult> UpdateReview([FromQuery] Guid Id, ReviewDTO review)
        {
            return Json(await _service.UpdateReview(Id, review));
        }

        [HttpDelete("review")]
        public async Task<IActionResult> DeleteReview([FromQuery] Guid reviewId)
        {
            return Json(await _service.RemoveReview(reviewId));
        }

        [HttpDelete("comment")]
        public async Task<IActionResult> DeleteComment([FromQuery] Guid commentId)
        {
            return Json(await _service.RemoveComment(commentId));
        }

        [HttpDelete("image")]
        public async Task<IActionResult> DeleteImage([FromQuery] Guid reviewImageId)
        {
            return Json(await _service.RemoveImage(reviewImageId));
        }

        [HttpGet("by-id")]
        public async Task<IActionResult> GetReview([FromQuery] Guid reviewId)
        {
            ReviewDTO review = await _service.GetReviewById(reviewId);
            return (Json(review));
        }

        [HttpGet("by-title")]
        public async Task<IActionResult> GetReviewByTitle([FromQuery] string title)
        {
            IEnumerable<ReviewDTO> review = await _service.GetReviewsByTitle(title);
            return (Json(review));

        }

        [HttpGet("by-average-raiting")]
        public async Task<IActionResult> GetReviewsByAverageRaiting([FromQuery] double raiting)
        {
            IEnumerable<ReviewDTO> review = await _service.GetReviewsByAverageRating(raiting);
            return (Json(review));
        }
        [HttpGet("by-description")]
        public async Task<IActionResult> GetReviewsByDescription([FromQuery] string description)
        {
            IEnumerable<ReviewDTO> reviews = await _service.GetReviewsByDescription(description);
            return (Json(reviews));
        }
        [HttpGet("reviews")]
        public async Task<IActionResult> GetReviewsByDescription([FromQuery] int skip, int take)
        {
            IEnumerable<ReviewDTO> reviews = await _service.GetReviews(skip, take);
            return (Json(reviews));
        }

    }
}