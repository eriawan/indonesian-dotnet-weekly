using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;

namespace SampleAspNetAspire90.Web
{
    public class ProductlistApiClient(HttpClient httpClient)
    {
        public async Task<List<Product>> GetProductAsync(int maxItems = 10, CancellationToken cancellationToken = default)
        {
            List<Product> products = new();

            await foreach (var product in httpClient.GetFromJsonAsAsyncEnumerable<Product>("/productlist", cancellationToken))
            {
                if (products.Count >= maxItems)
                {
                    break;
                }
                if (product is not null)
                {
                    products.Add(product);
                }
            }

            return products;
        }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public decimal ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
    }
}
