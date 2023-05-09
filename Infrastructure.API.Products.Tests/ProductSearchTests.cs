using Infrastructure.API.Products.EndPoints;
using Infrastructure.API.Products.MessageExchange;
using Infrastructure.API.Products.Messages;
using Moq;

namespace ProductSearchTests
{
    [TestClass]
    public class ProductSearchTests
    {
        [TestMethod]
        public async Task SearchProduct_Should_Return_BadRequest_When_Exchange_Returns_Null()
        {
            // Arrange
            var exchangeMock = new Mock<IProductSearchExhange>();
            exchangeMock.Setup(x => x.Publish(It.IsAny<ProductSearchMessage>()))
                        .Returns(Task.CompletedTask);
            exchangeMock.Setup(x => x.CheckForNewResultMessages(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((ProductResultMessage)null);

            var productSearch = new ProductSearch();

            // Act
            var result = await productSearch.SearchProduct(exchangeMock.Object, null, "Road", null, null, null);
            var statusCodeResult = result as Microsoft.AspNetCore.Http.HttpResults.BadRequest;

            // Assert
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task SearchProduct_Should_Return_Ok_When_Exchange_Returns_Valid_Result()
        {
            // Arrange
            var exchangeMock = new Mock<IProductSearchExhange>();
            exchangeMock.Setup(x => x.Publish(It.IsAny<ProductSearchMessage>()))
                        .Returns(Task.CompletedTask);
            exchangeMock.Setup(x => x.CheckForNewResultMessages(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new ProductResultMessage { ResultJson = "{}" });

            var productSearch = new ProductSearch();

            // Act
            var result = await productSearch.SearchProduct(exchangeMock.Object, null, "Road", null, null, null);
            var okResult = result as Microsoft.AspNetCore.Http.HttpResults.Ok<string>;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("{}", okResult.Value);
        }

        [TestMethod]
        public void SetupListeners_Should_Start_New_Search_Message_Listening()
        {
            // Arrange
            var exchangeMock = new Mock<IProductSearchExhange>();
            var productSearch = new ProductSearch();

            // Act
            productSearch.SetupListners(exchangeMock.Object);

            // Assert
            exchangeMock.Verify(x => x.ListenToNewSearchMessages(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}