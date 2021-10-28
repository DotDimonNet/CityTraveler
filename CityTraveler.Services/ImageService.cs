using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Errors;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class ImageService<T> : IImageService<T> where T:ImageModel, IServiceMetadata
    {

        private ApplicationContext _context;
        public ImageService(ApplicationContext context)
        {
            _context = context;      
        }

        public bool IsActive { get; set ; }
        public string Version { get; set; }

        public async Task<bool> AddNewImage(T image)
        {
            try
            {
                _context.Add(image);
                await _context.SaveChangesAsync();
            }
            catch (ImageServiceException e)
            {
                throw new ImageServiceException("Exception on adding new image!", e);
            }
            return true;
        }
        public async Task<bool> DeleteImage(Guid imageId)
        {
            try
            {
                var image = await _context.Images.FirstOrDefaultAsync(x=>x.Id==imageId);
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }
            catch (ImageServiceException e)
            {
                throw new ImageServiceException("Exception on deleting image!", e);
            }
            return true;
        }

        public async Task<T> GetImageByIdAsync(Guid imageId)
        {
            return (T)await _context.Images.FirstOrDefaultAsync(x=>x.Id==imageId);
        }

        public IEnumerable<T> GetImages(int skip = 0, int take=7)
        {
            return (IEnumerable<T>)_context.Images.Skip(skip).Take(take);
        }
        public async Task<bool> AddAvatarToUserProfile(string src)
        {
            try
            {
                _context.Add(src);
                await _context.SaveChangesAsync();
            }
            catch (ImageServiceException e)
            {
                throw new ImageServiceException("Exception on adding avatar to user profile!", e);
            }
            return true;
        }
        public async Task<bool> UpdatAvatarForUserProfile(string src, Guid userId)
        {
            try
            {
                var user =await  _context.UserProfiles.FirstOrDefaultAsync(x=>x.Id==userId);
                user.AvatarSrc = src;
                _context.Update(user);
                _context.SaveChanges();
            }
            catch (ImageServiceException e)
            {
                throw new ImageServiceException("Exception on updating avatar to user profile!", e);
            }
            return true;
        }
    }
}
