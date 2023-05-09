using Infrastructure.API.Products.MessageExchange;
using Infrastructure.API.Products.Messages;

namespace Infrastructure.API.Products.EndPoints
{
    public class ProductSearch : IProductSearch
    {
        public void SetupRoute(IEndpointRouteBuilder app, IProductSearchExhange exchange)
        {
            app.MapGet("/product/search", async (long? productId, string? name, string? productNumber, double? minPrice, double? maxPrice) =>
            {
                return await SearchProduct(exchange, productId, name, productNumber, minPrice, maxPrice);
            })
            .WithOpenApi();
        }

        public async Task<IResult> SearchProduct(IProductSearchExhange exchange, long? productId, string? name, string? productNumber, double? minPrice, double? maxPrice)
        {
            if (exchange != null)
            {
                ProductSearchMessage productSearchMessage = new ProductSearchMessage
                {
                    Id = Guid.NewGuid()
                };

                productSearchMessage.Create(productId, name, productNumber, minPrice, maxPrice);

                if (productSearchMessage.Type == ProductSearchTypes.Invalid)
                {
                    return Results.Ok("Search By productId or name or productNumber or (minPrice and maxPrice).");
                }

                await exchange.Publish(productSearchMessage);
                var cts = new CancellationTokenSource();
                var result = await exchange.CheckForNewResultMessages(productSearchMessage.Id, cts.Token);

                if (result == null)
                {
                    return Results.BadRequest();
                }

                return Results.Ok(result.ResultJson);
            }
            else
            {
                return Results.StatusCode(500);
            }
        }

        public void SetupListners(IProductSearchExhange exchange)
        {
            Task.Run(() =>
            {
                var cts = new CancellationTokenSource();
                if (exchange != null)
                {
                    exchange.ListenToNewSearchMessages(cts.Token);
                }
            });
        }
    }
}
