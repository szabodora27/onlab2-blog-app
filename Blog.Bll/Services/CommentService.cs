using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Bll.Dtos;
using Blog.Bll.Interfaces;
using Blog.Dal;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Blog.Bll.Services
{
    public class CommentService : ServiceBase, ICommentService
    {
        private readonly ApplicationDbContext _ctx;

        public CommentService(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _ctx = ctx;
        }
        public async Task CreateCommentAsync(Comment comment)
        {
            comment.CreatedById = GetCurrentUserId();
            comment.CreatedDate = DateTime.UtcNow;
            await _ctx.Comments.AddAsync(comment);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var comment = await _ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);
            comment.IsDeleted = true;
            await _ctx.SaveChangesAsync();
        }

        public IEnumerable<CommentDto> GetCommentsToBlogPost(Guid postId)
        {
            return _ctx.Comments.Include(c => c.CreatedBy)
                .Where(c => c.BlogPostId == postId && c.IsDeleted == false)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Text = c.Text,
                    CreatorName = c.CreatedBy.FirstName+" "+c.CreatedBy.FirstName,
                    CreatedDate = c.CreatedDate
                })
                .ToList();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            _ctx.Attach(comment);
            comment.LastModifiedById = GetCurrentUserId();
            comment.LastModifiedDate = DateTime.UtcNow;
            _ctx.Entry(comment).Property("Text").IsModified = true;
            await _ctx.SaveChangesAsync();
        }
    }
}
