using Blog.Bll.Dtos;
using Blog.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Bll.Interfaces
{
    public interface IBlogPostService
    {
        IQueryable<BlogPost> GetBlogPosts();

        Task<IEnumerable<BlogPost>> GetBlogPostsByUserIdAsync(string id);

        IEnumerable<BlogPost> GetBlogPostsOfCurrentUser();

        Task<BlogPost> GetBlogPostByIdAsync(Guid id);

        PostDetailDto GetBlogPostByIdForRead(Guid id);

        IEnumerable<BlogPost> GetBlogPostsInCategory(int id);

        Task<IEnumerable<BlogPost>> GetBlogPostsInCategoryAsync(int id);

        IEnumerable<BlogPostIndexDto> GetANumberOfBlogPostIndex(int number);

        Task CreateBlogPostAsync(BlogPost blogpost);

        Task UpdateBlogPostAsync(BlogPost updatedPost);

        Task DeleteBlogPostAsync(Guid id);
    }
}
