using EZCommerce.Application.Products.Queries;
using EZCommerce.Common;
using EZCommerce.Common.Models.Products;
using EZCommerce.Infrastructure.Data.Interfaces;
using MediatR;
using Serilog;

namespace EZCommerce.Application.Products.Handlers.QueryHandlers
{
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, ResultOrError<List<ProductResponse>>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByCategoryQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ResultOrError<List<ProductResponse>>> Handle(GetProductsByCategoryQuery categoryQuery, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                Log.Information("Handling GetProductsByCategoryQuery for category '{CategoryName}'", categoryQuery.categoryName);

                var products = await _productRepository.GetByCategory(categoryQuery.categoryName);

                if (products == null || !products.Any())
                {
                    errors.Add(new Error(ErrorCode.NotFound, $"No products found in category '{categoryQuery.categoryName}'."));
                    Log.Warning("No products found in category '{CategoryName}'\n", categoryQuery.categoryName);
                    return ResultOrError<List<ProductResponse>>.Failure(errors);
                }

                Log.Information("Successfully fetched products in category '{CategoryName}'\n", categoryQuery.categoryName);

                return ResultOrError<List<ProductResponse>>.Success(products);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while handling GetProductsByCategoryQuery for category '{CategoryName}'\n", categoryQuery.categoryName);

                errors.Add(new Error(ErrorCode.InternalServerError, $"An error occurred while fetching products in category '{categoryQuery.categoryName}'."));

                return ResultOrError<List<ProductResponse>>.Failure(errors);
            }
        }
    }
}
