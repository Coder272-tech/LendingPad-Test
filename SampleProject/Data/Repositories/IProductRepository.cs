using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> Get(string name = null, decimal? minPrice = null, decimal? maxPrice = null, string tag = null);

        // Method to get all products (for debug endpoint)
        IEnumerable<Product> GetAll();
        void DeleteAll();
    }
}
