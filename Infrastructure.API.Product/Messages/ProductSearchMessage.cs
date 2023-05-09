namespace Infrastructure.API.Products.Messages
{
    public class ProductSearchMessage
    {
        public Guid Id { get; set; }
        public List<dynamic> SearchArguments { get; set; } = new List<dynamic>();

        public ProductSearchTypes Type { get; set; }

        public void Create(long? productId, string name, string productNumber, double? minPrice, double? maxPrice)
        {
            var searchTypeMapping = new Dictionary<(bool, bool, bool, bool, bool), ProductSearchTypes>
            {
                { (true,  false, false, false, false), ProductSearchTypes.SearchByProductId },
                { (false, true,  false, false, false), ProductSearchTypes.SearchByName },
                { (false, false, true,  false, false), ProductSearchTypes.SearchByProductNumber },
                { (false, false, false, true,  true),  ProductSearchTypes.SearchByMinPriceAndMaxPrice }
            };

            Type = searchTypeMapping.TryGetValue((
                productId.HasValue,
                !string.IsNullOrEmpty(name),
                !string.IsNullOrEmpty(productNumber),
                minPrice.HasValue,
                maxPrice.HasValue), out var searchType) ? searchType : ProductSearchTypes.Invalid;

            Type = Validate(Type, productId, name, productNumber, minPrice, maxPrice);

            if (Type != ProductSearchTypes.Invalid)
            {
                SearchArguments = new List<dynamic>();
                if (productId.HasValue) SearchArguments.Add(productId.Value);
                if (!string.IsNullOrEmpty(name)) SearchArguments.Add(name);
                if (!string.IsNullOrEmpty(productNumber)) SearchArguments.Add(productNumber);
                if (minPrice.HasValue) SearchArguments.Add(minPrice.Value);
                if (maxPrice.HasValue) SearchArguments.Add(maxPrice.Value);
            }
            else
            {
                SearchArguments = null;
            }
        }

        public ProductSearchTypes Validate(ProductSearchTypes Type, long? productId, string name, string productNumber, double? minPrice, double? maxPrice)
        {
            if (Type == ProductSearchTypes.SearchByName)
            {
                if (!string.IsNullOrWhiteSpace(name) && name.Length > 10)
                {
                    Type = ProductSearchTypes.Invalid;
                }
            }

            if (Type == ProductSearchTypes.SearchByMinPriceAndMaxPrice)
            {
                if (minPrice > maxPrice)
                {
                    Type = ProductSearchTypes.Invalid;
                }
            }

            return Type;
        }
    }



    public enum ProductSearchTypes
    {
        Invalid,
        SearchByProductId,
        SearchByName,
        SearchByProductNumber,
        SearchByMinPriceAndMaxPrice
    }

}

