using EZCommerce.API;
using EZCommerce.API.Common.Swagger;
using EZCommerce.Application;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation(builder.Configuration)
        .AddApplication();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.AddSwagger();
    app.Run();
}



