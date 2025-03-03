using BlazorAppShopping.Data;
using BlazorAppShopping.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppShopping.Service
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }


        public async Task<(List<Category> Categories, int TotalCount)> GetCategoriesAsync(int pageNumber, int pageSize)
        {
            var query = _context.Categories.AsQueryable();
            int totalCount = await query.CountAsync();
            var categories = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (categories, totalCount);
        }


        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var existingEntity = _context.Categories.Local.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached; // Detach the existing entity
            }

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
