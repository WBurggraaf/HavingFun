using System.Threading.Channels;
using System.Collections.Concurrent;
using Infrastructure.API.Products.Messages;
using Infrastructure.API.Products.Services;

namespace Infrastructure.API.Products.MessageExchange
{
    public class ProductSearchExhange : IProductSearchExhange
    {
        private readonly Channel<ProductSearchMessage> _productSearchChannel = Channel.CreateUnbounded<ProductSearchMessage>();
        private readonly ConcurrentDictionary<Guid, ProductResultMessage> _resultBatch = new ConcurrentDictionary<Guid, ProductResultMessage>();

        private readonly IProductService _productService = null;

        public Guid UniqueId { get; set; }

        public ProductSearchExhange(IProductService productService)
        {
            UniqueId = Guid.NewGuid();

            _productService = productService;
        }

        public async Task Publish(ProductSearchMessage productSearchMessage)
        {
            await _productSearchChannel.Writer.WriteAsync(productSearchMessage);
            await Task.Delay(1);
        }

        public void Publish(Guid messageId, ProductResultMessage productResultMessage)
        {
            _resultBatch.TryAdd(messageId, productResultMessage);
        }

        public async Task ListenToNewSearchMessages(CancellationToken cancellationToken)
        {
            var searchMethods = new Dictionary<ProductSearchTypes, Func<ProductSearchMessage, Task<ProductResultMessage>>>
            {
                { ProductSearchTypes.SearchByProductId, _productService.SearchByProductIdAsync },
                { ProductSearchTypes.SearchByName, _productService.SearchByNameAsync },
                { ProductSearchTypes.SearchByProductNumber, _productService.SearchByProductNumberAsync },
                { ProductSearchTypes.SearchByMinPriceAndMaxPrice, _productService.SearchByMinPriceAndMaxPriceAsync }
            };

            while (!cancellationToken.IsCancellationRequested)
            {
                if (await _productSearchChannel.Reader.WaitToReadAsync())
                {
                    while (_productSearchChannel.Reader.TryRead(out var message))
                    {
                        ProductResultMessage productResultMessage = null;

                        if (searchMethods.TryGetValue(message.Type, out var searchMethod))
                        {
                            productResultMessage = await searchMethod(message);
                        }

                        if (productResultMessage != null)
                        {
                            Publish(message.Id, productResultMessage);
                        }
                    }
                    await Task.Delay(1);
                }
            }
        }

        public async Task<ProductResultMessage> CheckForNewResultMessages(Guid messageId, CancellationToken cancellationToken)
        {
            ProductResultMessage? returnValue = null;
            await Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {

                    _resultBatch.TryGetValue(messageId, out ProductResultMessage searchProductResultMessage);
                    if (searchProductResultMessage != null)
                    {
                        _resultBatch.TryRemove(messageId, out ProductResultMessage removedProductResultMessage);
                        if (removedProductResultMessage != null)
                        {
                            returnValue = removedProductResultMessage;
                            break;
                        }
                    }
                }
            });

            return returnValue;
        }
    }
}