using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Bll.Interfaces;
using Blog.Dal;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Blog.Bll.Services
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        private readonly ApplicationDbContext _ctx;

        public CategoryService(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _ctx = ctx;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _ctx.Categories.ToList();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _ctx.Categories.ToListAsync();
        }

        public Category GetCategoryById(int id)
        {
            return _ctx.Categories.SingleOrDefault(c => c.CategoryId == id);
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _ctx.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);
        }
    }
}
