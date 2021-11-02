using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IImageService<T> where T:ImageModel
    {
        public Task<bool> AddNewImage(T image);
        public Task<bool> DeleteImage(Guid imageId);
        public IEnumerable<T> GetImages(int skip = 0, int take = 7);
        public Task<T> GetImageByIdAsync(Guid imageId);
        public Task<bool> AddAvatarToUserProfile(string src);
        public Task<bool> UpdatAvatarForUserProfile(string src,  Guid userId);
    }
}
