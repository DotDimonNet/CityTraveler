using AutoMapper;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Errors;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class ImageService<T> : IImageService<T> where T:ImageDTO
    {

        private readonly ApplicationContext _context;
        private readonly ILogger<ImageService<T>> _logger;
        private readonly IMapper _mapper;

        public ImageService(ApplicationContext context, IMapper mapper, ILogger<ImageService<T>> logger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public bool IsActive { get; set ; }
        public string Version { get; set; }

        public async Task<bool> AddNewImageAsync(T imageDTO)
        {
            try
            {
                if (imageDTO == null)
                {
                    _logger.LogWarning($"Proplem with adding new image with type={imageDTO.GetType()}");
                    return false;
                }


                var model = _mapper.Map<ImageDTO, ImageModel>(imageDTO);

                _context.Images.Add(model);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Image was added {imageDTO}");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception on adding new image! {e.Message}");
                return false;
            }
        }

        public async Task<bool> AddRangeOfImagesAsync(IEnumerable<T> imagesDTO)
        {
            try
            {
                if (!imagesDTO.Any())
                {
                    _logger.LogWarning($"Problem with adding range of images!");
                    return false;
                }

                var models = imagesDTO.Select(x => _mapper.Map<ImageDTO, ImageModel>(x));

                await _context.Images.AddRangeAsync(models);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Range of images was added! {imagesDTO}");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception of adding range of images! {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteImageAsync(Guid imageId)
        {
            try
            {
                if (imageId == Guid.Empty)
                {
                    _logger.LogWarning($"Problem with deleting image! Image with Id={imageId} doesn't exists");
                    return false;
                }

                var image = await _context.Images.FirstOrDefaultAsync(x=>x.Id==imageId);
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception on deleting image! {e.Message}");
                return false;
            }
        }

        public async Task<ImageDTO> GetImageByIdAsync(Guid imageId)
        {
            try
            {
                if (imageId == Guid.Empty)
                {
                    _logger.LogWarning($"Problem with finding image with Id={imageId}");
                }

                var image = await _context.Images.FirstOrDefaultAsync(x => x.Id == imageId);

                return _mapper.Map<ImageModel, ImageDTO>(image);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception on finding image with Id={imageId}! {e.Message}");
                return new List<ImageDTO>().FirstOrDefault();             
            }
        }

        public IEnumerable<ImageDTO> GetImages(int skip = 0, int take = 7)
        {
            try
            {
                var images = _context.Images.Skip(skip).Take(take);

                if (!images.Any())
                {
                    _logger.LogWarning($"Problem with getting images. {images} are null.");
                }

                return images.Select(x => _mapper.Map<ImageModel, ImageDTO>(x));
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception on getting images! {e.Message}");
                return Enumerable.Empty<ImageDTO>();
            }
        }

        public async Task<bool> AddAvatarToUserProfileAsync(Guid userId, string src="")
        {
            try
            {
                if (userId == Guid.Empty && string.IsNullOrEmpty(src))
                {
                    _logger.LogWarning($"Problem on adding avatar for user profile");
                    return false;
                }

                var user = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Id == userId);
                user.AvatarSrc = src;
                
                _context.Update(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Avatar to user profile with UserId={userId} was added. Image source={src}.");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception on adding avatar profile! {e.Message}.");
                return false;
            }
        }

        //?
        public async Task<bool> UpdatAvatarForUserProfileAsync(Guid userId, string src="")
        {
            try
            {
                var user =await  _context.UserProfiles.FirstOrDefaultAsync(x=>x.Id==userId);
                user.AvatarSrc = src;
                _context.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new ImageServiceException($"Exception on updating avatar to user profile! {e.Message}");
            }
        }
    }
}
