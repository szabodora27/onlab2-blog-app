using Blog.Bll.Dtos;
using Blog.Bll.Interfaces;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Pages
{
    public class SearchResultModel : PageModel
    {
        private readonly IBlogPostService _postService;

        public SearchResultModel(IBlogPostService postService)
        {
            _postService = postService;
        }


        [BindProperty]
        public IEnumerable<BlogPostIndexDto> RecommendedPosts { get; set; }

        [BindProperty]
        public PaginatedList<BlogPost> BlogPosts { get; set; }

        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex)
       {
            RecommendedPosts = _postService.GetANumberOfBlogPostIndex(2);

            var posts = _postService.GetBlogPosts();

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Content.ToUpper().Contains(searchString.ToUpper()) || s.Title.ToUpper().Contains(searchString.ToUpper()));
            }

            int pageSize = 2;
            BlogPosts = await PaginatedList<BlogPost>.CreateAsync(posts, pageIndex ?? 1, pageSize);
        }
    }
}