using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Orders;
using WebApi.Models.Orders;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _create;
        private readonly IUpdateOrderService _update;
        private readonly IGetOrderService _get;
        private readonly IDeleteOrderService _delete;

        public OrderController(
            ICreateOrderService create,
            IUpdateOrderService update,
            IGetOrderService get,
            IDeleteOrderService delete)
        {
            _create = create;
            _update = update;
            _get = get;
            _delete = delete;
        }

        // GET api/order/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var order = _get.GetOrder(id);
            if (order == null)
                return DoesNotExist();

            return Found(new OrderData(order));
        }

        // GET api/order/list?userId=...&productId=...&fromDate=...&toDate=...
        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetList(Guid? userId = null, Guid? productId = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var orders = _get.GetOrders(userId, productId, fromDate, toDate);
            var orderData = new List<OrderData>();
            foreach (var order in orders)
                orderData.Add(new OrderData(order));

            return Found(orderData);
        }

        // GET api/order/debug/all
        [HttpGet]
        [Route("debug/all")]
        public HttpResponseMessage GetAll()
        {
            var orders = _get.GetAllOrders();
            var orderData = new List<OrderData>();
            foreach (var order in orders)
                orderData.Add(new OrderData(order));

            return Found(orderData);
        }

        // POST api/order/create
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] CreateOrderRequest request)
        {
            var order = _create.Create(
                request.Id,
                request.ProductId,
                request.UserId,
                request.Quantity,
                request.TotalPrice,
                request.OrderDate,
                request.Tags,
                request.OrderStatus
            );

            return Found(new OrderData(order));
        }

        // PUT api/order/{id}/update
        [HttpPut]
        [Route("{id:guid}/update")]
        public HttpResponseMessage Update(Guid id, [FromBody] UpdateOrderRequest request)
        {
            var order = _get.GetOrder(id);
            if (order == null)
                return DoesNotExist();

            _update.Update(order,
                request.ProductId,
                request.UserId,
                request.Quantity,
                request.TotalPrice,
                request.OrderDate,
                request.Tags,
                request.OrderStatus
            );

            return Found(new OrderData(order));
        }

        // DELETE api/order/{id}/delete
        [HttpDelete]
        [Route("{id:guid}/delete")]
        public HttpResponseMessage Delete(Guid id)
        {
            var order = _get.GetOrder(id);
            if (order == null)
                return DoesNotExist();

            _delete.Delete(order);
            return Found();
        }

        // DELETE api/order/clear
        [HttpDelete]
        [Route("clear")]
        public HttpResponseMessage DeleteAll()
        {
            _delete.DeleteAll();
            return Found();
        }
    }

    // Request DTOs
    public class CreateOrderRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    }

    public class UpdateOrderRequest
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    }
}
