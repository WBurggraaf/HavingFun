using Infrastructure.DB.AdventureWorks.Models;

namespace Infrastructure.API.Products.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> SearchByProductIdAsync(long productId);
        Task<List<Product>> SearchByNameAsync(string name);
        Task<List<Product>> SearchByProductNumberAsync(string productNumber);
        Task<List<Product>> SearchByMinPriceAndMaxPriceAsync(double minPrice, double maxPrice);
    }
}
