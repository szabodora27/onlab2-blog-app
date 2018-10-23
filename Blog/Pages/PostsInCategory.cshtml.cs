using Blog.Bll.Interfaces;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Blog.Pages
{
    public class PostsInCategoryModel : PageModel
    {
        private readonly IBlogPostService _postService;
        private readonly ICategoryService _categoryService;

        public PostsInCategoryModel(IBlogPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public IEnumerable<BlogPost> BlogPosts { get; set; }

        [BindProperty]
        public Category Category { get; set; }

        public void OnGet(int categoryId)
        {
            BlogPosts = _postService.GetBlogPostsInCategory(categoryId);
            Category = _categoryService.GetCategoryById(categoryId);
        }
    }
}