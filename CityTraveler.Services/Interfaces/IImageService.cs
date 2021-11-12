using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IImageService<T> where T:ImageDTO
    {
        public Task<bool> AddNewImageAsync(T imageDTO);
        public Task<bool> AddRangeOfImagesAsync (IEnumerable<T> images);
        public Task<bool> DeleteImageAsync(Guid imageId);
        public IEnumerable<ImageDTO> GetImages(int skip = 0, int take = 7);
        public Task<ImageDTO> GetImageByIdAsync(Guid imageId);
        public Task<bool> AddAvatarToUserProfileAsync(Guid userId, string src);
        public Task<bool> UpdatAvatarForUserProfileAsync(Guid userId, string src);
    }
}
