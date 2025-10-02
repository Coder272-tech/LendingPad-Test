using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IGetOrderService
    {
        Order GetOrder(Guid id);
        IEnumerable<Order> GetOrders(Guid? userId = null, Guid? productId = null, DateTime? fromDate = null, DateTime? toDate = null);
        IEnumerable<Order> GetAllOrders();
    }
}
