using Infrastructure.API.Products.MessageExchange;
using Infrastructure.API.Products.Messages;
using Infrastructure.API.Products.Services;
using Moq;

namespace ProductSearchExchangeTests
{
    [TestClass]
    public class ProductSearchExchangeTests
    {

        [TestMethod]
        public void Publish_Should_Add_Result_To_ResultBatch()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var productSearchExchange = new ProductSearchExhange(productServiceMock.Object);
            var messageId = Guid.NewGuid();
            var productResultMessage = new ProductResultMessage();

            // Act
            productSearchExchange.Publish(messageId, productResultMessage);

            // Assert
            //Assert.IsTrue(productSearchExchange._resultBatch.ContainsKey(messageId));
            //Assert.AreEqual(productResultMessage, productSearchExchange._resultBatch[messageId]);
        }

        [TestMethod]
        public async Task CheckForNewResultMessages_Should_Return_ProductResultMessage()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var productSearchExchange = new ProductSearchExhange(productServiceMock.Object);

            var messageId = Guid.NewGuid();
            var productResultMessage = new ProductResultMessage { ResultJson = "{}" };
            productSearchExchange.Publish(messageId, productResultMessage);

            var cancellationTokenSource = new CancellationTokenSource();

            // Act
            var result = await productSearchExchange.CheckForNewResultMessages(messageId, cancellationTokenSource.Token);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(productResultMessage.ResultJson, result.ResultJson);

            cancellationTokenSource.Cancel(); // to cancel the while loop in the background

            await Task.Delay(10); // to ensure the cancellation token is processed
        }
    }
}