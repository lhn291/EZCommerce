using EZCommerce.Application.Products.Commands;
using EZCommerce.Application.Products.Queries;
using EZCommerce.Common;
using EZCommerce.Common.Models.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EZCommerce.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ApiController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResultOrError<List<ProductResponse>>> Get()
        {
            return await HandleAsync(async () =>
            {
                var query = new GetAllProductsQuery();
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [HttpGet("{categoryName}")]
        public async Task<ResultOrError<List<ProductResponse>>> Get(string categoryName)
        {
            return await HandleAsync(async () =>
            {
                var query = new GetProductsByCategoryQuery(categoryName);
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [HttpPost]
        public async Task<ResultOrError<bool>> Post(CreateProductCommand command)
        {
            return await HandleAsync(async () =>
            {
                var result = await _mediator.Send(command);
                return result;
            });
        }

        [HttpPut]
        public async Task<ResultOrError<bool>> Put(UpdateProductCommand command)
        {
            return await HandleAsync(async () =>
            {
                var result = await _mediator.Send(command);
                return result;
            });

        }
    }
}
