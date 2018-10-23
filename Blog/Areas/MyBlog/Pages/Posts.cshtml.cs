using Blog.Bll.Interfaces;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Areas.MyBlog.Pages
{
    public class BlogPostsModel : PageModel
    {
        private readonly IBlogPostService _postService;

        public BlogPostsModel(IBlogPostService postService)
        {
            _postService = postService;
        }

        [BindProperty]
        public IEnumerable<BlogPost> Blogposts { get; set; }

        public void OnGet()
        {
            Blogposts = _postService.GetBlogPostsOfCurrentUser();
        }
    }
}