using Blog.Bll.Dtos;
using Blog.Bll.Interfaces;
using Blog.Dal;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Bll.Services
{
    public class BlogPostService : ServiceBase, IBlogPostService
    {
        private readonly ApplicationDbContext _ctx;

        public BlogPostService(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _ctx = ctx;
        }

        public async Task CreateBlogPostAsync(BlogPost blogpost)
        {
            blogpost.CreatedById = GetCurrentUserId();
            blogpost.CreatedDate = DateTime.UtcNow;
            await _ctx.BlogPosts.AddAsync(blogpost);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteBlogPostAsync(Guid id)
        {
            var post = await _ctx.BlogPosts.SingleOrDefaultAsync(c => c.Id == id);
            post.IsDeleted = true;
            await _ctx.SaveChangesAsync();
        }

        public IEnumerable<BlogPostIndexDto> GetANumberOfBlogPostIndex(int number)
        {
            return _ctx.BlogPosts.Where(b => b.IsDeleted == false)
                .Include(b => b.CreatedBy)
                .OrderBy(r => Guid.NewGuid())
                .Take(number)
                .Select(b => new BlogPostIndexDto
                {
                    Id = b.Id,
                    FeaturedImageUrl = b.FeaturedImageUrl,
                    Title = b.Title,
                    CreatedBy = b.CreatedBy.LastName + " " + b.CreatedBy.FirstName
                })
                .ToList();
        }

        public async Task<BlogPost> GetBlogPostByIdAsync(Guid id)
        {
            return await _ctx.BlogPosts.SingleOrDefaultAsync(b => b.Id == id);
        }

        public PostDetailDto GetBlogPostByIdForRead(Guid id)
        {
            return _ctx.BlogPosts.Include(b => b.CreatedBy).Include(b => b.Favorites)
                .Select(b => new PostDetailDto {
                    Id = b.Id,
                    Title = b.Title,
                    Content = b.Content,
                    FeaturedImageUrl = b.FeaturedImageUrl,
                    CreatorId = b.CreatedById,
                    CreatorName = b.CreatedBy.LastName + " " + b.CreatedBy.FirstName,
                    IsFavorite = b.Favorites.Where(f => f.BlogPostId == id && f.UserId == GetCurrentUserId()).SingleOrDefault() == null ? false : true
                })
                .SingleOrDefault(b => b.Id == id);
        }

        public IQueryable<BlogPost> GetBlogPosts()
        {
            return _ctx.BlogPosts.Include(b => b.CreatedBy).Where(b => b.IsDeleted == false);
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPostsByUserIdAsync(string id)
        {
            return await _ctx.BlogPosts.Include(b => b.Category)
                .Where(b => b.CreatedById == id && b.IsDeleted == false)
                .ToListAsync();
        }

        public IEnumerable<BlogPost> GetBlogPostsInCategory(int id)
        {
            return _ctx.BlogPosts
                .Include(b => b.CreatedBy)
                .Where(b => b.CategoryId == id && b.IsDeleted == false)
                .ToList();
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPostsInCategoryAsync(int id)
        {
            return await _ctx.BlogPosts
                .Where(b => b.CategoryId == id && b.IsDeleted == false)
                .ToListAsync();
        }

        public IEnumerable<BlogPost> GetBlogPostsOfCurrentUser()
        {
            return _ctx.BlogPosts.Include(b => b.Category)
                        .Where(b => b.CreatedById == GetCurrentUserId() && b.IsDeleted == false)
                        .ToList();
        }

        public async Task UpdateBlogPostAsync(BlogPost updatedPost)
        {
            _ctx.Attach(updatedPost);

            _ctx.Entry(updatedPost).Property("FeaturedImageUrl").IsModified = true;
            _ctx.Entry(updatedPost).Property("Title").IsModified = true;
            _ctx.Entry(updatedPost).Property("Content").IsModified = true;
            _ctx.Entry(updatedPost).Property("CategoryId").IsModified = true;

            await _ctx.SaveChangesAsync();
        }
    }
}
