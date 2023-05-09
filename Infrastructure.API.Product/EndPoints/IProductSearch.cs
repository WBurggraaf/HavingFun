using Infrastructure.API.Products.MessageExchange;

namespace Infrastructure.API.Products.EndPoints
{
    public interface IProductSearch
    {
        Task<IResult> SearchProduct(IProductSearchExhange exchange, long? productId, string? name, string? productNumber, double? minPrice, double? maxPrice);
        void SetupRoute(IEndpointRouteBuilder app, IProductSearchExhange exchange);
        void SetupListners(IProductSearchExhange exchange);
    }
}