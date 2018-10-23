using Blog.Bll.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Bll.Interfaces
{
    public interface IUserService
    {
        Task AddBlogPostToFavoritesAsync(Guid id);

        Task RemoveBlogPostFromFavoritesAsync(Guid id);

        IEnumerable<BlogPostIndexDto> GetFavoritePosts();

        AboutDto GetAboutFromUserById(string id);

        Task<string> GetAboutAsync();

        Task<IdentityResult> SetAboutAsync(string about);

        Task<string> GetPictureUrlAsync();

        Task<IdentityResult> SetPictureUrlAsync(string pictureUrl);

        Task<string> GetJobTitleAsync();

        Task<IdentityResult> SetJobTitleAsync(string jobtitle);
    }
}
