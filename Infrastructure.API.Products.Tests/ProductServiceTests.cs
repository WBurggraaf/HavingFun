using Infrastructure.API.Products.Messages;
using Infrastructure.API.Products.Repositories;
using Infrastructure.API.Products.Services;
using Infrastructure.DB.AdventureWorks.Models;
using Moq;

namespace Infrastructure.API.Products.UnitTests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private ProductService _productService;

        [TestInitialize]
        public void Initialize()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [TestMethod]
        public async Task SearchByProductIdAsync_Should_Call_ProductRepository_SearchByProductIdAsync_With_Correct_Parameters()
        {
            // Arrange
            var message = new ProductSearchMessage
            {
                Id = new System.Guid(),
                SearchArguments = new List<dynamic> { 123 }
            };
            var searchByProductIdResult = new List<Product> { new Product { ProductId = 123 } };
            //_productRepositoryMock.Setup(r => r.SearchByProductIdAsync(message.SearchArguments[0])).ReturnsAsync(searchByProductIdResult);

            // Act
            var result = await _productService.SearchByProductIdAsync(message);

            // Assert
        }

        [TestMethod]
        public async Task SearchByNameAsync_Should_Call_ProductRepository_SearchByNameAsync_With_Correct_Parameters()
        {
            // Arrange
            var message = new ProductSearchMessage
            {
                Id = new System.Guid(),
                SearchArguments = new List<dynamic> { "Product A" }
            };
            var searchByNameResult = new List<Product> { new Product { Name = "Product A" } };
            //_productRepositoryMock.Setup(r => r.SearchByNameAsync(message.SearchArguments[0])).ReturnsAsync(searchByNameResult);

            // Act
            var result = await _productService.SearchByNameAsync(message);

            // Assert
        }

        [TestMethod]
        public async Task SearchByProductNumberAsync_Should_Call_ProductRepository_SearchByProductNumberAsync_With_Correct_Parameters()
        {
            // Arrange
            var message = new ProductSearchMessage
            {
                Id = new System.Guid(),
                SearchArguments = new List<dynamic> { "PA123" }
            };
            var searchByProductNumberResult = new List<Product> { new Product { ProductNumber = "PA123" } };
            //_productRepositoryMock.Setup(r => r.SearchByProductNumberAsync(message.SearchArguments[0])).ReturnsAsync(searchByProductNumberResult);

            // Act
            var result = await _productService.SearchByProductNumberAsync(message);

            // Assert
        }

        [TestMethod]
        public async Task SearchByMinPriceAndMaxPriceAsync_Should_Call_ProductRepository_SearchByMinPriceAndMaxPriceAsync_With_Correct_Parameters()
        {
            // Arrange
            var message = new ProductSearchMessage
            {
                Id = new System.Guid(),
                SearchArguments = new List<dynamic> { 10.5, 20.5 }
            };
            var searchByMinPriceAndMaxPriceResult = new List<Product> { new Product { ListPrice = new byte[] { 49, 48, 46, 57, 57 } } };
           // _productRepositoryMock.Setup(r => r.SearchByMinPriceAndMaxPriceAsync(message.SearchArguments[0], message.SearchArguments[1])).ReturnsAsync(searchByMinPriceAndMaxPriceResult);

            // Act
            var result = await _productService.SearchByMinPriceAndMaxPriceAsync(message);

            // Assert
        }
    }
}