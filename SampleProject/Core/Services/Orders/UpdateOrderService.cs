using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateOrderService : IUpdateOrderService
    {
        public void Update(Order order, Guid productId, Guid userId, int quantity, decimal totalPrice, DateTime orderDate, IEnumerable<string> tags, OrderStatus status)
        {
            order.SetProductId(productId);
            order.SetUserId(userId);
            order.SetQuantity(quantity);
            order.SetTotalPrice(totalPrice);
            order.SetOrderDate(orderDate);
            order.SetTags(tags);
            order.SetStatus(status); // new
        }
    }
}
