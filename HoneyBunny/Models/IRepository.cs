using System.Collections.Generic;
using System.Threading.Tasks;
using HoneyBunny.Models;

namespace HoneyBunny.Models
{
    public interface IRepository
    {
        ApplicationDbContext _db { get; set; }
        Task<List<Category>> CategoryListAsync();
        Task<Category> GetCategoryAsync(int id);
        Task InsertCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);

        Task<List<Product>> ProductListAsync();
        Task<Product> GetProductAsync(int id);
        Task InsertProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
