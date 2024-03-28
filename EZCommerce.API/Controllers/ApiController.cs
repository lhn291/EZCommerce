using EZCommerce.Common;
using Microsoft.AspNetCore.Mvc;

namespace EZCommerce.API.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected async Task<ResultOrError<T>> HandleAsync<T>(Func<Task<ResultOrError<T>>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                return ResultOrError<T>.Failure(new Error(ErrorCode.BadRequest, ex.Message));
            }
        }
    }
}
