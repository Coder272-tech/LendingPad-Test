using System;
using System.Collections.Generic;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            ProductId = order.ProductId;
            UserId = order.UserId;
            Quantity = order.Quantity;
            TotalPrice = order.TotalPrice;
            Status = order.Status; // new
            OrderDate = order.OrderDate;
            Tags = order.Tags;
        }

        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } // new
        public IEnumerable<string> Tags { get; set; }
    }
}
