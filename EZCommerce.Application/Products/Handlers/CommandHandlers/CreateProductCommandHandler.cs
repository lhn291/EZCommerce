using EZCommerce.Common.Extensions;
using EZCommerce.Common;
using EZCommerce.Common.Models.Products;
using EZCommerce.Infrastructure.Data.Interfaces;
using MediatR;
using Serilog;
using EZCommerce.Application.Products.Commands;

namespace EZCommerce.Application.Products.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResultOrError<bool>>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ResultOrError<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                Log.Information($"Handling CreateProductCommand for product '{request.Name}'");

                var productExists = await _productRepository.IsProductNameExists(request.Name);
                if (productExists)
                {
                    errors.Add(new Error(ErrorCode.Conflict, $"Product with name '{request.Name}' already exists."));
                }

                if (request.Price <= 0)
                {
                    errors.Add(new Error(ErrorCode.BadRequest, "Price must be greater than 0."));
                }

                if (request.StockQuantity <= 0)
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
                    var errorMessage = errors.ToErrorMessage();
                    Log.Warning("Errors occurred while handling CreateProductCommand for product '{ProductName}':\nList<Errors>\n{Errors}\n", request.Name, errorMessage);
                    return ResultOrError<bool>.Failure(errors);
                }


                var product = new ProductRequest
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    StockQuantity = request.StockQuantity,
                    CategoryId = request.CategoryId,
                    BrandId = request.BrandId,
                    ShopName = request.ShopName
                };

                var productResponse = await _productRepository.CreateAsync(product, existingCategory!, existingBrand!);

                Log.Information("Product '{ProductName}' created successfully\n", request.Name);

                return ResultOrError<bool>.Success(productResponse);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while handling CreateProductCommand for product '{ProductName}'\n", request.Name);

                errors.Add(new Error(ErrorCode.InternalServerError, "An error occurred while creating the product."));

                return ResultOrError<bool>.Failure(errors);
            }
        }
    }
}