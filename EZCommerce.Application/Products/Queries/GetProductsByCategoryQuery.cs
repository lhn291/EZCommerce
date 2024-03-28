using EZCommerce.Common;
using EZCommerce.Common.Models.Products;
using MediatR;

namespace EZCommerce.Application.Products.Queries
{
    public record GetProductsByCategoryQuery(string categoryName) : IRequest<ResultOrError<List<ProductResponse>>>;
}
