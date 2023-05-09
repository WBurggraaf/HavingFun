namespace Infrastructure.API.Products.Messages
{
    public class ProductResultMessage
    {
        public Guid Id { get; set; }

        public string ResultJson { get; set; }

        public ResultProcess ResultProcess { get; set; }

        public ProductResultTypes Type { get; set; }
    }

    public enum ProductResultTypes
    {
        SearchByProductId,
        SearchByName,
        SearchByProductNumber,
        SearchByMinPriceAndMaxPrice
    }

    public enum ResultProcess
    {
        Valid,
        Invalid,
        Error
    }

}

