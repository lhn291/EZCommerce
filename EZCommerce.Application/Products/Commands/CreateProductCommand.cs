using EZCommerce.Common;
using MediatR;

namespace EZCommerce.Application.Products.Commands
{
    public record CreateProductCommand(string Name,
                                    string Description,
                                    double Price,
                                    int StockQuantity,
                                    int CategoryId,
                                    int BrandId,
                                    string ShopName) : IRequest<ResultOrError<bool>>;
}
