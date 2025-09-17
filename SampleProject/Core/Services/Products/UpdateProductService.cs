using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Products
{
    [AutoRegister]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, string description, decimal price, int stockQuantity, IEnumerable<string> tags)
        {
            product.SetName(name);
            product.SetDescription(description);
            product.SetPrice(price);
            product.SetStockQuantity(stockQuantity);
            product.SetTags(tags);
        }
    }
}
