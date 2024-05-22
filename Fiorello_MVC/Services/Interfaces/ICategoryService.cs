using Fiorello_MVC.Models;
using Fiorello_MVC.ViewModels.Categories;

namespace Fiorello_MVC.Services.Interfaces
{
    public interface ICategoryService 
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<CategoryProductVM>> GetAllWithProductCountAsync();
        Task<Category> GetById(int id);
        Task<bool> ExistAsync(string name);
        Task CreateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<CategoryDetailVM> GetByIdWithProducts(int id);
        Task<IEnumerable<CategoryArchiveVM>> GetAllArchiveAsync();
    }
}
