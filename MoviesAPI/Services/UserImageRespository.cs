using MoviesAPI.UserModel;
using System;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class UserImageRespository : IUserImage
    {
        readonly MovieDb db;
        public UserImageRespository(MovieDb db)
        {
            this.db = db;
        }
        public async Task<UserImage> AddUserImage(string userId, string url)
        {
            UserImage userImage = new UserImage
            {
                ImageId = Guid.NewGuid(),
                UserImageURL = url,
                UserId = userId
            };
            await db.UserImage.AddAsync(userImage);
            await db.SaveChangesAsync();
            return userImage;
        }
    }
}
