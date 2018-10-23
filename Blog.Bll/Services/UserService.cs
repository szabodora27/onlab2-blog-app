using Blog.Bll.Dtos;
using Blog.Bll.Interfaces;
using Blog.Dal;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Bll.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly ApplicationDbContext _ctx;

        public UserService(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _ctx = ctx;
        }

        public async Task AddBlogPostToFavoritesAsync(Guid id)
        {
            _ctx.Favorites.Add(new Favorite {BlogPostId = id, UserId = GetCurrentUserId()});
            await _ctx.SaveChangesAsync();
        }

        public async Task<string> GetAboutAsync()
        {
            return await _ctx.Users.OfType<ApplicationUser>()
                .Where(u => u.Id == GetCurrentUserId())
                .Select(u => u.About)
                .FirstOrDefaultAsync();
        }

        public AboutDto GetAboutFromUserById(string id)
        {
            return _ctx.Users.OfType<ApplicationUser>()
                .Where(u => u.Id == id)
                .Select(u => new AboutDto
                {
                    Name = u.LastName+" "+u.FirstName,
                    About = u.About,
                    JobTitle = u.JobTitle,
                    PictureUrl = u.PictureUrl,
                    Posts = u.BlogPosts
                                .Where(b => b.IsDeleted == false && b.CreatedBy.Id == id)
                                .OrderBy(r => Guid.NewGuid())
                                .Take(3)
                                .Select(b => new BlogPostIndexDto
                                {
                                    Id = b.Id,
                                    FeaturedImageUrl = b.FeaturedImageUrl,
                                    Title = b.Title
                                })
                                .ToList()
                })
                .FirstOrDefault();
        }

        public IEnumerable<BlogPostIndexDto> GetFavoritePosts()
        {
            return _ctx.Favorites.Where(b => b.UserId == GetCurrentUserId() && b.BlogPost.IsDeleted == false)
                .Include(f => f.BlogPost)
                .ThenInclude(b => b.CreatedBy)
                .Select(f => new BlogPostIndexDto
                {
                    Id = f.BlogPost.Id,
                    FeaturedImageUrl = f.BlogPost.FeaturedImageUrl,
                    Title = f.BlogPost.Title,
                    CreatedBy = f.BlogPost.CreatedBy.LastName+" "+f.BlogPost.CreatedBy.FirstName
                })
                .ToList();
        }

        public async Task<string> GetJobTitleAsync()
        {
            return await _ctx.Users.OfType<ApplicationUser>()
                .Where(u => u.Id == GetCurrentUserId())
                .Select(u => u.JobTitle)
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetPictureUrlAsync()
        {
            return await _ctx.Users.OfType<ApplicationUser>()
                .Where(u => u.Id == GetCurrentUserId())
                .Select(u => u.PictureUrl)
                .FirstOrDefaultAsync();
        }

        public async Task RemoveBlogPostFromFavoritesAsync(Guid id)
        {
            Favorite favorite = _ctx.Favorites
                .Where(f => f.BlogPostId == id && f.UserId == GetCurrentUserId())
                .SingleOrDefault();
            _ctx.Favorites.Remove(favorite);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IdentityResult> SetAboutAsync(string about)
        {
            var user =_ctx.Users.OfType<ApplicationUser>()
                .Where(u => u.Id == GetCurrentUserId())
                .FirstOrDefault();

            if(user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Nincs ilyen user" });
            }

            user.About = about;
            await _ctx.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> SetJobTitleAsync(string jobtitle)
        {
            var user = _ctx.Users.OfType<ApplicationUser>()
                .Where(u => u.Id == GetCurrentUserId())
                .FirstOrDefault();

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Nincs ilyen user" });
            }

            user.JobTitle = jobtitle;
            await _ctx.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> SetPictureUrlAsync(string pictureUrl)
        {
            var user = _ctx.Users.OfType<ApplicationUser>()
                .Where(u => u.Id == GetCurrentUserId())
                .FirstOrDefault();

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Nincs ilyen user" });
            }

            user.PictureUrl = pictureUrl;
            await _ctx.SaveChangesAsync();

            return IdentityResult.Success;
        }
    }
}
