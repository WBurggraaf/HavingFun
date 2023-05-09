using Infrastructure.DB.AdventureWorks.Contexts;
using Infrastructure.DB.AdventureWorks.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.API.Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AdventureWorksDbContext _context;

        public ProductRepository()
        {
            _context = new AdventureWorksDbContext();
        }

        public async Task<List<Product>> SearchByProductIdAsync(long productId)
        {
            return await _context.Products.Where(_ => _.ProductId == productId).ToListAsync();
        }

        public async Task<List<Product>> SearchByNameAsync(string name)
        {
            return await _context.Products.Where(_ => _.Name.Contains(name)).ToListAsync();
        }

        public async Task<List<Product>> SearchByProductNumberAsync(string productNumber)
        {
            return await _context.Products.Where(_ => _.ProductNumber == productNumber).ToListAsync();
        }

        public async Task<List<Product>> SearchByMinPriceAndMaxPriceAsync(double minPrice, double maxPrice)
        {
            var products = await _context.Products.ToListAsync();

            List<Product> filtered = new List<Product>();

            Parallel.ForEach(products, product =>
            {
                string decodedBytes = System.Text.Encoding.UTF8.GetString(product.ListPrice);
                double listPrice = Convert.ToDouble(decodedBytes.Replace(".", ","));

                if (listPrice >= minPrice && listPrice <= maxPrice)
                {
                    filtered.Add(product);
                }
            });

            return filtered;
        }
    }
}
