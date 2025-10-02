using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;

namespace Data.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private static readonly ConcurrentDictionary<Guid, Order> _store = new ConcurrentDictionary<Guid, Order>();

        public void Save(Order entity)
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

        public void Delete(Order entity)
        {
            if (entity != null)
                _store.TryRemove(entity.Id, out _);
        }

        public Order Get(Guid id)
        {
            _store.TryGetValue(id, out var entity);
            return entity;
        }

        public IEnumerable<Order> Get(Guid? userId = null, Guid? productId = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = _store.Values.AsEnumerable();

            if (userId.HasValue)
                query = query.Where(o => o.UserId == userId.Value);

            if (productId.HasValue)
                query = query.Where(o => o.ProductId == productId.Value);

            if (fromDate.HasValue)
                query = query.Where(o => o.OrderDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(o => o.OrderDate <= toDate.Value);

            return query;
        }

        public IEnumerable<Order> GetAll()
        {
            return _store.Values;
        }

        public void DeleteAll()
        {
            _store.Clear();
        }
    }
}
