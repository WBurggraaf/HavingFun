using Infrastructure.API.Products.Messages;

namespace Infrastructure.API.Products.MessageExchange
{
    public interface IProductSearchExhange
    {
        public Guid UniqueId { get; set; }

        Task Publish(ProductSearchMessage productSearchMessage);
        void Publish(Guid messageId, ProductResultMessage productResultMessage);
        Task ListenToNewSearchMessages(CancellationToken cancellationToken);
        Task<ProductResultMessage> CheckForNewResultMessages(Guid messageId, CancellationToken cancellationToken);
    }
}