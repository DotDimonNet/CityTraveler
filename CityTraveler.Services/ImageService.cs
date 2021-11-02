using AutoMapper;
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
    public class ImageService<T> : IImageService<T> where T:ImageModel
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

        public async Task<bool> AddNewImage(T image)
        {
            try
            {
                _context.Add(image);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new ImageServiceException($"Exception on adding new image! {e.Message}");
            }
        }

        public async Task<bool> DeleteImage(Guid imageId)
        {
            try
            {
                var image = await _context.Images.FirstOrDefaultAsync(x=>x.Id==imageId);
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new ImageServiceException($"Exception on deleting image! {e.Message}");
            }
        }

        public async Task<T> GetImageByIdAsync(Guid imageId)
        {
            return (T)await _context.Images.FirstOrDefaultAsync(x=>x.Id == imageId);
        }

        public IEnumerable<T> GetImages(int skip = 0, int take = 7)
        {
            return (IEnumerable<T>)_context.Images.Skip(skip).Take(take);
        }

        public async Task<bool> AddAvatarToUserProfile(string src)
        {
            try
            {
                _context.Add(src);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new ImageServiceException($"Exception on adding avatar to user profile! {e.Message}");
            }
        }

        public async Task<bool> UpdatAvatarForUserProfile(string src, Guid userId)
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
