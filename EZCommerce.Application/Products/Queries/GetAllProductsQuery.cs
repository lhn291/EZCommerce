using EZCommerce.Common;
using EZCommerce.Common.Models.Products;
using MediatR;

namespace EZCommerce.Application.Products.Queries
{
    public record GetAllProductsQuery() : IRequest<ResultOrError<List<ProductResponse>>>;
}
