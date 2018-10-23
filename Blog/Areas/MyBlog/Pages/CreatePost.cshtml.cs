using AutoMapper;
using Blog.Bll.Interfaces;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace Blog.Areas.MyBlog.Pages
{
    public class CreatePostModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostService _postService;
        private readonly ICategoryService _categoryService;

        public CreatePostModel(IMapper mapper, IBlogPostService postService, ICategoryService categoryService)
        {
            _mapper = mapper;
            _postService = postService;
            _categoryService = categoryService;
        }

        [BindProperty]
        [Required]
        [Display(Name = "Cím")]
        public string Title { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }
        public string FeaturedImageUrl { get; set; }

        [BindProperty]
        [Required]
        public string Content { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public void OnGet()
        {
            Categories = _categoryService.GetCategories();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = _categoryService.GetCategories();
                return Page();
            }      
           
            FeaturedImageUrl = $"/uploads/{FeaturedImage.FileName}";

            ResizeAndSaveImage(FeaturedImage, 900);

            var post = new BlogPost();
            _mapper.Map(this, post);
            await _postService.CreateBlogPostAsync(post);
            return RedirectToPage("/Posts");
        }

        public IActionResult OnPostTinyMceImage()
        {
            IFormFile file;
            try
            {
                file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    ResizeAndSaveImage(file, 900);               
                }
                return new JsonResult(new { location = $"/uploads/{file.FileName}" });
            }
            catch (System.Exception ex)
            {
                return new JsonResult($"Upload Failed: {ex.Message}");
            }
        }

        [NonHandler]
        private void ResizeAndSaveImage(IFormFile file, int maxWidth)
        {
            using (Image<Rgba32> image = Image.Load(file.OpenReadStream()))
            {
                if(image.Width > maxWidth)
                {
                    image.Mutate(ctx => ctx.Resize(maxWidth, (image.Height/image.Width)*maxWidth));
                }
                image.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName));
            }
        }
    }
}