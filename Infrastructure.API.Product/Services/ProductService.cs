using Infrastructure.API.Products.Messages;
using Infrastructure.API.Products.Repositories;
using System.Text.Json;

namespace Infrastructure.API.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResultMessage> SearchByProductIdAsync(ProductSearchMessage message)
        {
            long productId = message.SearchArguments[0];
            var searchByProductIdResult = await _productRepository.SearchByProductIdAsync(productId);
            return new ProductResultMessage()
            {
                Id = message.Id,
                ResultJson = JsonSerializer.Serialize(searchByProductIdResult),
                Type = (ProductResultTypes)(int)message.Type,
                ResultProcess = ResultProcess.Valid
            };
        }

        public async Task<ProductResultMessage> SearchByNameAsync(ProductSearchMessage message)
        {
            string name = message.SearchArguments[0];
            var searchByNameResult = await _productRepository.SearchByNameAsync(name);
            return new ProductResultMessage()
            {
                Id = message.Id,
                ResultJson = JsonSerializer.Serialize(searchByNameResult),
                Type = (ProductResultTypes)(int)message.Type,
                ResultProcess = ResultProcess.Valid
            };
        }

        public async Task<ProductResultMessage> SearchByProductNumberAsync(ProductSearchMessage message)
        {
            string productNumber = message.SearchArguments[0];
            var searchByProductNumberResult = await _productRepository.SearchByProductNumberAsync(productNumber);
            return new ProductResultMessage()
            {
                Id = message.Id,
                ResultJson = JsonSerializer.Serialize(searchByProductNumberResult),
                Type = (ProductResultTypes)(int)message.Type,
                ResultProcess = ResultProcess.Valid
            };
        }

        public async Task<ProductResultMessage> SearchByMinPriceAndMaxPriceAsync(ProductSearchMessage message)
        {
            double minPrice = message.SearchArguments[0];
            double maxPrice = message.SearchArguments[1];
            var searchByMinPriceAndMaxPriceResult = await _productRepository.SearchByMinPriceAndMaxPriceAsync(minPrice, maxPrice);
            return new ProductResultMessage()
            {
                Id = message.Id,
                ResultJson = JsonSerializer.Serialize(searchByMinPriceAndMaxPriceResult),
                Type = (ProductResultTypes)(int)message.Type,
                ResultProcess = ResultProcess.Valid
            };
        }
    }
}
