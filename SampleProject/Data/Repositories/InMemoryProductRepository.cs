using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;

namespace Data.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private static readonly ConcurrentDictionary<Guid, Product> _store = new ConcurrentDictionary<Guid, Product>();

        public void Save(Product entity)
        {
            if (entity.Id == Guid.Empty)
            {
                var id = Guid.NewGuid();
                _store[id] = entity;
            }
            else
            {
                _store[entity.Id] = entity;
            }
        }

        public void Delete(Product entity)
        {
            if (entity != null)
                _store.TryRemove(entity.Id, out _);
        }

        public Product Get(Guid id)
        {
            _store.TryGetValue(id, out var entity);
            return entity;
        }

        public IEnumerable<Product> Get(string name = null, decimal? minPrice = null, decimal? maxPrice = null, string tag = null)
        {
            var query = _store.Values.AsEnumerable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name?.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (!string.IsNullOrWhiteSpace(tag))
                query = query.Where(p => p.Tags != null && p.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase));

            return query;
        }

        public IEnumerable<Product> GetAll()
        {
            return _store.Values;
        }

        

        public void DeleteAll()
        {
            _store.Clear();
        }
    }
}
