using EZCommerce.Application.Products.Commands;
using EZCommerce.Common;
using EZCommerce.Infrastructure.Data.Interfaces;
using MediatR;

namespace EZCommerce.Application.Products.Handlers.CommandHandlers
{
    public class UpdateProductCommandHandlers : IRequestHandler<UpdateProductCommand, ResultOrError<bool>>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandlers(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ResultOrError<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(request.Id);
                if (existingProduct == null)
                {
                    errors.Add(new Error(ErrorCode.NotFound, $"Product with ID {request.Id} not found."));
                }

                var productWithSameName = await _productRepository.IsProductNameExists(request.Name);
                if (productWithSameName)
                {
                    errors.Add(new Error(ErrorCode.Conflict, $"Product with name '{request.Name}' already exists."));
                }

                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    errors.Add(new Error(ErrorCode.BadRequest, "Product name must not be empty."));
                }

                if (request.Price <= 0)
                {
                    errors.Add(new Error(ErrorCode.BadRequest, "Price must be greater than 0."));
                }

                if (request.StockQuantity < 0)
                {
                    errors.Add(new Error(ErrorCode.BadRequest, "Stock quantity must be greater than 0."));
                }

                var existingCategory = await _productRepository.GetCategoryByIdAsync(request.CategoryId);
                if (existingCategory == null)
                {
                    errors.Add(new Error(ErrorCode.NotFound, $"Category with ID {request.CategoryId} not found."));
                }

                var existingBrand = await _productRepository.GetBrandByIdAsync(request.BrandId);
                if (existingBrand == null)
                {
                    errors.Add(new Error(ErrorCode.NotFound, $"Brand with ID {request.BrandId} not found."));
                }

                if (errors.Any())
                {
                    return ResultOrError<bool>.Failure(errors);
                }

                existingProduct!.Name = request.Name;
                existingProduct.Description = request.Description;
                existingProduct.Price = request.Price;
                existingProduct.StockQuantity = request.StockQuantity;
                existingProduct.CategoryId = request.CategoryId;
                existingProduct.BrandId = request.BrandId;
                existingProduct.ShopName = request.ShopName;

                await _productRepository.UpdateAsync(existingProduct, existingCategory!, existingBrand!);

                return ResultOrError<bool>.Success(true);
            }
            catch (Exception)
            {
                errors.Add(new Error(ErrorCode.InternalServerError, "An error occurred while updating the product."));
                return ResultOrError<bool>.Failure(errors);
            }
        }
    }
}
