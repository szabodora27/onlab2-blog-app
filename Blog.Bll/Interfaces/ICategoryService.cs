using Blog.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Bll.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Category GetCategoryById(int id);

        Task<Category> GetCategoryByIdAsync(int id);
    }
}
