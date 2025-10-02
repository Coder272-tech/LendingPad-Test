using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _repository;

        public GetOrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        // Single order by ID
        public Order GetOrder(Guid id)
        {
            // Make sure you call Get(id) which returns a single Order
            return ((IRepository<Order>)_repository).Get(id);
        }

        // Filtered list of orders
        public IEnumerable<Order> GetOrders(Guid? userId = null, Guid? productId = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            return _repository.Get(userId, productId, fromDate, toDate);
        }

        // Debug endpoint: all orders
        public IEnumerable<Order> GetAllOrders()
        {
            return _repository.GetAll();
        }
    }
}
