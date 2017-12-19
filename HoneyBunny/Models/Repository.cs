using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace HoneyBunny.Models
{
    public class Repository : IDisposable, IRepository
    {
        private ApplicationDbContext _db;

        public Repository(ApplicationDbContext context)
        {
            _db = context;
        }

        #region Categories
        public async Task<List<Category>> CategoryListAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task InsertCategoryAsync(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _db.Entry(category).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryAsync(id);
            _db.Categories.Remove(category);

            await _db.SaveChangesAsync();
        }
        #endregion

        #region Products
        public async Task<List<Product>> ProductListAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task InsertProductAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductAsync(id);
            _db.Products.Remove(product);

            await _db.SaveChangesAsync();
        }
        #endregion

        #region IDisposable Support
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}