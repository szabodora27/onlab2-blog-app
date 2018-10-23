using AutoMapper;
using Blog.Bll.Dtos;
using Blog.Bll.Interfaces;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Pages
{
    public class ReadPostModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public ReadPostModel(IMapper mapper, IBlogPostService postService, ICommentService commentService, IUserService userService)
        {
            _mapper = mapper;
            _postService = postService;
            _commentService = commentService;
            _userService = userService;
        }

        [BindProperty]
        public PostDetailDto Post { get; set; }

        [BindProperty]
        public IEnumerable<BlogPostIndexDto> RecommendedPosts { get; set; }

        [BindProperty]
        public IEnumerable<CommentDto> Comments { get; set; }

        [BindProperty]
        public string CommentText { get; set; }

        public void OnGet(Guid id)
        {
            Post = _postService.GetBlogPostByIdForRead(id);
            RecommendedPosts = _postService.GetANumberOfBlogPostIndex(2);
            Comments = _commentService.GetCommentsToBlogPost(id);
        }

        public async Task<ActionResult> OnPostCreateCommentAsync(Guid id)
        {
            if(CommentText != null)
            {
                var comment = new Comment();
                comment.BlogPostId = id;
                comment.Text = CommentText;
                await _commentService.CreateCommentAsync(comment);
            }

            return RedirectToPage();
        }

        public async Task<ActionResult> OnGetDeleteAsync(Guid id)
        {
            await _postService.DeleteBlogPostAsync(id);
            return Redirect("/MyBlog/Posts");
        }

        public async Task OnPostLikeAsync(Guid id, bool value)
        {
            if (value)
            {
                await _userService.AddBlogPostToFavoritesAsync(id);
            }
            else
            {
                await _userService.RemoveBlogPostFromFavoritesAsync(id);                
            }          
        }
    }
}