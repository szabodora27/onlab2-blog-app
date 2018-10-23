using AutoMapper;
using Blog.Areas.MyBlog.Pages;
using Blog.Bll.Dtos;
using Blog.Model.Entities;
using Blog.Pages;

namespace Blog
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<BlogPost, CreatePostModel>().ReverseMap();
            CreateMap<BlogPost, EditPostModel>().ReverseMap();
            CreateMap<AboutDto, AboutModel>().ReverseMap();
        }
    }
}
