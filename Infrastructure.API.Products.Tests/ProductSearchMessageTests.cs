using Infrastructure.API.Products.Messages;

namespace Infrastructure.API.Products.UnitTests.Messages
{
    [TestClass]
    public class ProductSearchMessageTests
    {
        [TestMethod]
        public void Create_Should_Set_SearchArguments_And_Type_With_Null_Values()
        {
            // Arrange
            var productSearchMessage = new ProductSearchMessage();
            long? productId = null;
            string name = null;
            string productNumber = null;
            double? minPrice = null;
            double? maxPrice = null;

            // Act
            productSearchMessage.Create(productId, name, productNumber, minPrice, maxPrice);

            // Assert
            Assert.IsNull(productSearchMessage.SearchArguments);
            Assert.AreEqual(ProductSearchTypes.Invalid, productSearchMessage.Type);
        }

        [TestMethod]
        public void Validate_Should_Return_Invalid_When_Name_Length_Is_Greater_Than_10()
        {
            // Arrange
            var productSearchMessage = new ProductSearchMessage();
            var productId = (long?)null;
            var name = "testname1234567";
            var productNumber = (string)null;
            var minPrice = (double?)null;
            var maxPrice = (double?)null;

            // Act
            var result = productSearchMessage.Validate(ProductSearchTypes.SearchByName, productId, name, productNumber, minPrice, maxPrice);

            // Assert
            Assert.AreEqual(ProductSearchTypes.Invalid, result);
        }

        [TestMethod]
        public void Validate_Should_Return_Invalid_When_MinPrice_Is_Greater_Than_MaxPrice()
        {
            // Arrange
            var productSearchMessage = new ProductSearchMessage();
            var productId = (long?)null;
            var name = (string)null;
            var productNumber = (string)null;
            var minPrice = 2.34;
            var maxPrice = 1.23;

            // Act
            var result = productSearchMessage.Validate(ProductSearchTypes.SearchByMinPriceAndMaxPrice, productId, name, productNumber, minPrice, maxPrice);

            // Assert
            Assert.AreEqual(ProductSearchTypes.Invalid, result);
        }
    }
}