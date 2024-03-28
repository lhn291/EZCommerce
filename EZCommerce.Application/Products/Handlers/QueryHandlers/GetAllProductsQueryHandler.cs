using EZCommerce.Application.Products.Queries;
using EZCommerce.Common;
using EZCommerce.Common.Models.Products;
using EZCommerce.Infrastructure.Data.Interfaces;
using MediatR;
using Serilog;

namespace EZCommerce.Application.Products.Handlers.QueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ResultOrError<List<ProductResponse>>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ResultOrError<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                Log.Information("Handling GetAllProductsQuery");

                var products = await _productRepository.GetAll();

                if (products == null || !products.Any())
                {
                    errors.Add(new Error(ErrorCode.NotFound, "No products found."));
                    Log.Warning("No products found\n");
                    return ResultOrError<List<ProductResponse>>.Failure(errors);
                }

                Log.Information("Successfully fetched all products\n");

                return ResultOrError<List<ProductResponse>>.Success(products);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while handling GetAllProductsQuery\n");

                errors.Add(new Error(ErrorCode.InternalServerError, "An error occurred while fetching all products."));

                return ResultOrError<List<ProductResponse>>.Failure(errors);
            }
        }
    }
}