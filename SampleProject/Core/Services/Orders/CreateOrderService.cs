using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using static Core.Exceptions.OrderExceptions;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IUpdateOrderService _updateService;
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IOrderRepository _repository;
        private readonly IGetOrderService _getService;

        public CreateOrderService(IIdObjectFactory<Order> orderFactory, IOrderRepository repository, IUpdateOrderService updateService, IGetOrderService getService)
        {
            _orderFactory = orderFactory;
            _repository = repository;
            _updateService = updateService;
            _getService = getService;
        }

        public Order Create(Guid id, Guid productId, Guid userId, int quantity, decimal totalPrice, DateTime orderDate, IEnumerable<string> tags, OrderStatus status = OrderStatus.Pending)
        {
            var existing = _getService.GetOrder(id);
            if (existing != null)
                throw new OrderAlreadyExistsException(id);

            var order = _orderFactory.Create(id);
            _updateService.Update(order, productId, userId, quantity, totalPrice, orderDate, tags, status);
            _repository.Save(order);
            return order;
        }
    }
}
