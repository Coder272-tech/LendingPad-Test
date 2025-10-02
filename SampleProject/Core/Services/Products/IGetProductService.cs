using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    public interface IGetProductService
    {
        Product GetProduct(Guid id);

        // Get all products (for debug)
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProducts(string name = null, decimal? minPrice = null, decimal? maxPrice = null, string tag = null);
    }
}
