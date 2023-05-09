using Infrastructure.API.Products.Messages;

namespace Infrastructure.API.Products.Services
{
    public interface IProductService
    {
        Task<ProductResultMessage> SearchByMinPriceAndMaxPriceAsync(ProductSearchMessage message);
        Task<ProductResultMessage> SearchByNameAsync(ProductSearchMessage message);
        Task<ProductResultMessage> SearchByProductIdAsync(ProductSearchMessage message);
        Task<ProductResultMessage> SearchByProductNumberAsync(ProductSearchMessage message);
    }
}