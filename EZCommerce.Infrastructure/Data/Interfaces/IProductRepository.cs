using EZCommerce.Common.Models.Products;
using EZCommerce.Core.Entities;

namespace EZCommerce.Infrastructure.Data.Interfaces
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<List<ProductResponse>> GetByCategory(string name);
        new Task<List<ProductResponse>> GetAll();
        Task<bool> CreateAsync(ProductRequest productRequest, Category category, Brand brand);
        Task<bool> UpdateAsync(Product productRequest, Category category, Brand brand);
        Task<Category?> GetCategoryByIdAsync(int categoryId);
        Task<Brand?> GetBrandByIdAsync(int brandId);
        Task<bool> IsProductNameExists(string productName);
        Task<Product?> GetByIdAsync(int productId);
    }
}
