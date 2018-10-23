using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Bll.Interfaces;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Blog.Areas.MyBlog.Pages
{
    public class EditPostModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostService _postService;
        private readonly ICategoryService _categoryService;

        public EditPostModel(IMapper mapper, IBlogPostService postService, ICategoryService categoryService)
        {
            _mapper = mapper;
            _postService = postService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Cím")]
        public string Title { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        [BindProperty]
        public string FeaturedImageUrl { get; set; }

        [BindProperty]
        public string Content { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public async Task OnGet(Guid id)
        {
            BlogPost post = await _postService.GetBlogPostByIdAsync(id);
            _mapper.Map(post, this);
            Categories = _categoryService.GetCategories();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (FeaturedImage != null)
            {
                var path = Path.Combine(@Directory.GetCurrentDirectory(), $"wwwroot{FeaturedImageUrl}");
                System.IO.File.Delete(path);

                FeaturedImageUrl = $"/uploads/{FeaturedImage.FileName}";
                ResizeAndSaveImage(FeaturedImage, 700);
            }
            
            var post = new BlogPost();
            _mapper.Map(this, post);
            await _postService.UpdateBlogPostAsync(post);
            return RedirectToPage("/Posts");
        }

        [NonHandler]
        private void ResizeAndSaveImage(IFormFile file, int maxWidth)
        {
            using (Image<Rgba32> image = Image.Load(file.OpenReadStream()))
            {
                if (image.Width > maxWidth)
                {
                    image.Mutate(ctx => ctx.Resize(maxWidth, (image.Height / image.Width) * maxWidth));
                }
                image.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName));
            }
        }

        [NonHandler]
        public static string Combine(string path1, string path2)
        {
            // Ensure neither end of path1 or beginning of path2 have slashes
            path1 = path1.Trim().TrimEnd(System.IO.Path.DirectorySeparatorChar);
            path2 = path2.Trim().TrimStart(System.IO.Path.DirectorySeparatorChar);

            // Handle drive letters
            if (path1.Substring(path1.Length - 1, 1) == ":")
                path1 += System.IO.Path.DirectorySeparatorChar;

            return System.IO.Path.Combine(path1, path2);
        }
    }
}