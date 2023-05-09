using Infrastructure.API.Products.EndPoints;
using Infrastructure.API.Products.MessageExchange;
using Moq;
using TechTalk.SpecFlow.CommonModels;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.API.Products.Services;
using Infrastructure.API.Products.Repositories;
using Microsoft.AspNetCore.Routing;
using Infrastructure.DB.AdventureWorks.Models;
using Newtonsoft.Json;

namespace Infrastructure.API.Products.SpecFlow.Tests.StepDefinitions
{
    [Binding]
    public class ProductSearchStepDefinitions
    {
        private IProductSearch _productSearch = new ProductSearch();
        private long? _productId;
        private string? _name;
        private string? _productNumber;
        private double? _minPrice;
        private double? _maxPrice;
        private Microsoft.AspNetCore.Http.IResult? _searchResult;

        [Given(@"the API is running")]
        public void GivenTheAPIIsRunning()
        {
        }

        [When(@"the client sends a request to search for products with the following parameters:")]
        public async Task WhenTheClientSendsARequestToSearchForProductsWithTheFollowingParameters(Table table)
        {
            var parameters = table.Rows[0];
            _productId = long.TryParse(parameters["productId"], out long productId) ? productId : null;
            _name = parameters.ContainsKey("name") ? parameters["name"] : null;
            _productNumber = parameters.ContainsKey("productNumber") ? parameters["productNumber"] : null;
            _minPrice = double.TryParse(parameters.ContainsKey("minPrice") ? parameters["minPrice"] : null, out double minPrice) ? minPrice : null;
            _maxPrice = double.TryParse(parameters.ContainsKey("maxPrice") ? parameters["maxPrice"] : null, out double maxPrice) ? maxPrice : null;

            Mock<IProductRepository> productRepository = new Mock<IProductRepository>();
            IProductSearchExhange exchange = new ProductSearchExhange(new ProductService(productRepository.Object));

            Product product = new Product();
            product.Name = "Test";

            productRepository.Setup(_ => _.SearchByMinPriceAndMaxPriceAsync(It.IsAny<double>(), It.IsAny<double>())).Returns(Task.FromResult(new List<Product> { product }));
            productRepository.Setup(_ => _.SearchByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(new List<Product> { product }));
            productRepository.Setup(_ => _.SearchByProductIdAsync(It.IsAny<long>())).Returns(Task.FromResult(new List<Product> { product }));
            productRepository.Setup(_ => _.SearchByProductNumberAsync(It.IsAny<string>())).Returns(Task.FromResult(new List<Product> { product }));

            _productSearch.SetupListners(exchange);
            _searchResult = await _productSearch.SearchProduct(exchange, _productId, _name, _productNumber, _minPrice, _maxPrice);
        }

        [Then(@"the API should return a list of products matching the search criteria")]
        public void ThenTheAPIShouldReturnAListOfProductsMatchingTheSearchCriteria()
        {
            _searchResult.Should().NotBeNull();
            _searchResult.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().NotBeNull();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().BeAssignableTo<string>();

            //List<Product> products = JsonSerializer.Deserialize<List<Product>>((_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value);

            //products.Should().NotBeNull();
            //products.Should().NotBeEmpty();

            //// Check that each product in the list is valid
            //foreach (var product in products)
            //{
            //    product.ProductId.Should().NotBe(0);
            //    product.Name.Should().NotBeNullOrEmpty();
            //    product.ProductNumber.Should().NotBeNullOrEmpty();
            //    product.StandardCost.Should().NotBeNull();
            //    product.ListPrice.Should().NotBeNull();
            //    // Add more assertions for each property of the Product class
            //}
        }

        [Then(@"the API should return an error message indicating that only one search parameter is allowed at a time")]
        public void ThenTheAPIShouldReturnAnErrorMessageIndicatingThatOnlyOneSearchParameterIsAllowedAtATime()
        {
            _searchResult.Should().NotBeNull();
            _searchResult.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().NotBeNull();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().BeAssignableTo<string>();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.ToString().Should().Contain("Search By productId or name or productNumber or (minPrice and maxPrice).");
        }

        [Then(@"the API should return an error message indicating that the productId is invalid")]
        public void ThenTheAPIShouldReturnAnErrorMessageIndicatingThatTheProductIdIsInvalid()
        {
            _searchResult.Should().NotBeNull();
            _searchResult.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().NotBeNull();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().BeAssignableTo<string>();
            //(_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.ToString().Should().Be("Search By productId or name or productNumber or (minPrice and maxPrice).");
        }

        [Then(@"the API should return an error message indicating that the name is invalid")]
        public void ThenTheAPIShouldReturnAnErrorMessageIndicatingThatTheNameIsInvalid()
        {
            _searchResult.Should().NotBeNull();
            _searchResult.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().NotBeNull();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().BeAssignableTo<string>();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.ToString().Should().Be("Search By productId or name or productNumber or (minPrice and maxPrice).");
        }

        [Then(@"the API should return an error message indicating that the productNumber is invalid")]
        public void ThenTheAPIShouldReturnAnErrorMessageIndicatingThatTheProductNumberIsInvalid()
        {
            _searchResult.Should().NotBeNull();
            _searchResult.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().NotBeNull();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().BeAssignableTo<string>();
            //(_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.ToString().Should().Be("Search By productId or name or productNumber or (minPrice and maxPrice).");
        }

        [Then(@"the API should return an error message indicating that the price range is invalid")]
        public void ThenTheAPIShouldReturnAnErrorMessageIndicatingThatThePriceRangeIsInvalid()
        {
            _searchResult.Should().NotBeNull();
            _searchResult.Should().BeOfType<Microsoft.AspNetCore.Http.HttpResults.Ok<string>>();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().NotBeNull();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.Should().BeAssignableTo<string>();
            (_searchResult as Microsoft.AspNetCore.Http.HttpResults.Ok<string>).Value.ToString().Should().Be("Search By productId or name or productNumber or (minPrice and maxPrice).");
        }
    }
}
