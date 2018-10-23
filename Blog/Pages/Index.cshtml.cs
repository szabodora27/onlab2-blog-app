using Blog.Bll.Dtos;
using Blog.Bll.Interfaces;
using Blog.Dal.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Blog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogPostService _postService;

        public IndexModel(ILogger<IndexModel> logger, IBlogPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        [BindProperty]
        public IEnumerable<BlogPostIndexDto> RecommendedPosts { get; set; }


        public void OnGet()
        {
            RecommendedPosts = _postService.GetANumberOfBlogPostIndex(6);
            _logger.LogInformation((int)LoggingEvents.GENERATE_ITEMS, "Show student list.");
        }
    }
}
