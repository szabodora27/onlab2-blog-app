using Blog.Bll.Dtos;
using Blog.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Blog.Areas.MyBlog.Pages
{
    public class FavoritesModel : PageModel
    {
        private readonly IUserService _userService;

        public FavoritesModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public IEnumerable<BlogPostIndexDto> Posts { get; set; }

        public void OnGet()
        {
            Posts = _userService.GetFavoritePosts();
        }
    }
}