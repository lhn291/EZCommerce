using AutoMapper;
using EZCommerce.Common.Models.Products;
using EZCommerce.Core.Entities;
using EZCommerce.Infrastructure.Data.DbContext;
using EZCommerce.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EZCommerce.Infrastructure.Data.Repositories
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task<List<ProductResponse>> GetByCategory(string name)
        {
            try
            {
                var products = await _db.Products
                    .Where(c => c.Category.Name.Contains(name))
                    .Select(p => new ProductResponse
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        StockQuantity = p.StockQuantity,
                        CategoryName = p.Category.Name,
                        BrandName = p.Brand.Name,
                        ShopName = p.ShopName
                    })
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while fetching products by name.";
                throw new Exception(errorMessage, ex);
            }
        }

        public new async Task<List<ProductResponse>> GetAll()
        {
            try
            {
                var products = await _db.Products
                    .Select(p => new ProductResponse
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        StockQuantity = p.StockQuantity,
                        CategoryName = p.Category.Name,
                        BrandName = p.Brand.Name,
                        ShopName = p.ShopName
                    })
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while fetching all products.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<bool> CreateAsync(ProductRequest productRequest, Category category, Brand brand)
        {
            try
            {
                var product = _mapper.Map<Product>(productRequest);

                product.Category = category;
                product.Brand = brand;

                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while creating the product.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<bool> IsProductNameExists(string productName)
        {
            return await _db.Products.AnyAsync(p => p.Name == productName);
        }

        public async Task<Product?> GetByIdAsync(int productId)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return product;
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await _db.Categories.FindAsync(categoryId);
        }

        public async Task<Brand?> GetBrandByIdAsync(int brandId)
        {
            return await _db.Brands.FindAsync(brandId);
        }

        public async Task<bool> UpdateAsync(Product productRequest, Category category, Brand brand)
        {
            try
            {
                var productToUpdate = await _db.Products.FirstOrDefaultAsync(p => p.Id == productRequest.Id);

                if (productToUpdate == null)
                    return false;

                productToUpdate.Name = productRequest.Name;
                productToUpdate.Description = productRequest.Description;
                productToUpdate.Price = productRequest.Price;
                productToUpdate.StockQuantity = productRequest.StockQuantity;
                productToUpdate.Category = category;
                productToUpdate.Brand = brand;
                productToUpdate.ShopName = productRequest.ShopName;

                _db.Products.Update(productToUpdate);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while updating the product.");
            }
        }

    }
}
