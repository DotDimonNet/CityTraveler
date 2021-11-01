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

        [HttpPut]
        [Route("add-review-entertainment")]
        public async Task<IActionResult> AddReview(EntertainmentReviewModel review, Guid entertainmentId)
        {
            return Json(await _service.AddReviewEntertainment(entertainmentId, review)) ;
        }
        [HttpPut]
        [Route("add-review-trip")]
        public async Task<IActionResult> AddReview(TripReviewModel review, Guid tripId)
        {
            return Json(await _service.AddReviewTrip(tripId, review));
        }
        [HttpPut]
        [Route("add-comment")]
        public async Task<IActionResult> AddComment(CommentModel comment, Guid reviewId)
        {
            return Json(await _service.AddComment(comment, reviewId));
        }
        [HttpPut]
        [Route("add-image")]
        public async Task<IActionResult> AddComment(ReviewImageModel image, Guid reviewId)
        {
            return Json(await _service.AddImage(image, reviewId));
        }
        [HttpDelete]
        [Route("delete-review")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            return Json(await _service.RemoveReview(reviewId));
        }
        [HttpDelete]
        [Route("delete-comment")]
        public async Task<IActionResult> DeleteComment(Guid commentId, Guid reviewId)
        {
            return Json(await _service.RemoveComment(commentId, reviewId));
        }
        [HttpDelete]
        [Route("delete-image")]
        public async Task<IActionResult> DeleteImage(Guid reviewImageId, Guid reviewId)
        {
            return Json(await _service.RemoveImage(reviewImageId, reviewId));
        }

        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetReview(Guid reviewId)
        {
            ReviewModel review;
            try
            {
                review = await _service.GetReviewById(reviewId);
            }
            catch (SocialMediaServiceException e) when (reviewId == null)
            {

                throw new SocialMediaServiceException($"Review with Id={reviewId} not found", e);
            }
            catch (SocialMediaServiceException e)
            {
                throw new SocialMediaServiceException("Exception on finding review by Id", e);
            }
            return (Json(review));

        }
        [HttpGet]
        [Route("get-by-title")]
        public async Task<IActionResult> GetReviewByTitle(string title)
        {
            IEnumerable<ReviewModel> review;
            try
            {
                review = _service.GetReviewsByTitle(title);
            }
            catch (SocialMediaServiceException e) when (title == null)
            {

                throw new SocialMediaServiceException($"Review with title={title} not found", e);
            }
            catch (SocialMediaServiceException e)
            {
                throw new SocialMediaServiceException("Exception on finding review by title", e);
            }
            return (Json(review));

        }

        [HttpGet]
        [Route("get-reviews-by-average-raiting")]
        public async Task<IActionResult> GetReviewsByAverageRaiting(double raiting)
        {
            IEnumerable<ReviewModel> review;
            try
            {
                review = _service.GetReviewsByAverageRaiting(raiting);
            }
            catch (SocialMediaServiceException e) when (raiting == -1)
            {

                throw new SocialMediaServiceException($"Review with raiting={raiting} not found", e);
            }
            catch (SocialMediaServiceException e)
            {
                throw new SocialMediaServiceException("Exception on finding review by raiting", e);
            }
            return (Json(review));

        }
        [HttpGet]
        [Route("get-reviews-by-description")]
        public async Task<IActionResult> GetReviewsByDescription([FromQuery] string description)
        {
            IEnumerable<ReviewModel> reviews = _service.GetReviewsByDescription(description);
            return (Json(reviews));
        }

    }
}