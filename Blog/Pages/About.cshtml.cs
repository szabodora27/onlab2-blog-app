using AutoMapper;
using Blog.Bll.Dtos;
using Blog.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Blog.Pages
{
    public class AboutModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AboutModel(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public string Name { get; set; }

        public string About { get; set; }

        public string JobTitle { get; set; }

        public string PictureUrl { get; set; }

        public IEnumerable<BlogPostIndexDto> Posts { get; set; }

        public void OnGet(string id)
        {
            AboutDto post = _userService.GetAboutFromUserById(id);
            _mapper.Map(post, this);
        }
    }
}
