using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> Get(Guid? userId = null, Guid? productId = null, DateTime? fromDate = null, DateTime? toDate = null);
        IEnumerable<Order> GetAll();
        void DeleteAll();
    }
}
