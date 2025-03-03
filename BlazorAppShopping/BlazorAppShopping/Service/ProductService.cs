using BlazorAppShopping.Data;
using BlazorAppShopping.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppShopping.Service
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Product> Products, int TotalCount)> GetProductsAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            int totalCount = await query.CountAsync();

            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task AddProductAsync(Product Product)
        {
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
        }

        //public async Task UpdateProductAsync(Product Product)
        //{
        //    var existingEntity = _context.Products.Local.FirstOrDefault(c => c.ProductId == Product.ProductId);
        //    if (existingEntity != null)
        //    {
        //        _context.Entry(existingEntity).State = EntityState.Detached; // Detach the existing entity
        //    }

        //    _context.Products.Update(Product);
        //    await _context.SaveChangesAsync();
        //}

        public async Task UpdateProductAsync(Product updatedProduct)
        {
            var existingProduct = await _context.Products.FindAsync(updatedProduct.ProductId);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
                await _context.SaveChangesAsync();
            }
            else
            {
                await AddProductAsync(updatedProduct);
            }
        }

        public async Task DeleteProductAsync(int ProductId)
        {
            var Product = await _context.Products.FindAsync(ProductId);
            if (Product != null)
            {
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products
                                 .Include(p => p.Category) 
                                 .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

    }
}
