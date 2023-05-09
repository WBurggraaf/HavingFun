using Infrastructure.API.Products.EndPoints;
using Infrastructure.API.Products.MessageExchange;
using Infrastructure.API.Products.Repositories;
using Infrastructure.API.Products.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Infrastructure.API.Products
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.Services.AddSingleton<IProductSearchExhange, ProductSearchExhange>();
            builder.Services.AddSingleton<IProductSearch, ProductSearch>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();

            ServiceProvider? serviceProvider = builder.Services.BuildServiceProvider();
            IProductSearchExhange? exchange = serviceProvider.GetService<IProductSearchExhange>();
            IProductSearch? productSearch = serviceProvider.GetService<IProductSearch>();
            productSearch.SetupListners(exchange);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            productSearch.SetupRoute(app, exchange);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }

    }
}