using Blog.Bll.Dtos;
using Blog.Dal;
using Blog.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Bll.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<CommentDto> GetCommentsToBlogPost(Guid postId);

        Task CreateCommentAsync(Comment comment);

        Task UpdateCommentAsync(Comment comment);

        Task DeleteCommentAsync(Guid id);
    }
}
