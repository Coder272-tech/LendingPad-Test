using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        void Update(Order order, Guid productId, Guid userId, int quantity, decimal totalPrice, DateTime orderDate, IEnumerable<string> tags, OrderStatus status);
    }
}
