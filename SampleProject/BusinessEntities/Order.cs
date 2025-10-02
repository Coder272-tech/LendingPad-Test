using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private readonly List<string> _tags = new List<string>();
        private Guid _productId;
        private Guid _userId;
        private int _quantity;
        private decimal _totalPrice;
        private DateTime _orderDate;
        private OrderStatus _status = OrderStatus.Pending; // backing field

        public Guid ProductId
        {
            get => _productId;
            private set => _productId = value;
        }

        public Guid UserId
        {
            get => _userId;
            private set => _userId = value;
        }

        public int Quantity
        {
            get => _quantity;
            private set => _quantity = value;
        }

        public decimal TotalPrice
        {
            get => _totalPrice;
            private set => _totalPrice = value;
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            private set => _orderDate = value;
        }

        public OrderStatus Status
        {
            get => _status;
            private set => _status = value;
        }

        public IEnumerable<string> Tags
        {
            get => _tags;
            private set => _tags.Initialize(value);
        }

        public void SetProductId(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId), "ProductId must be provided.");
            _productId = productId;
        }

        public void SetUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId), "UserId must be provided.");
            _userId = userId;
        }

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
            _quantity = quantity;
        }

        public void SetStatus(OrderStatus status)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), status))
                throw new ArgumentOutOfRangeException(nameof(status), "Invalid order status.");
            _status = status;
        }

        public void SetTotalPrice(decimal totalPrice)
        {
            if (totalPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(totalPrice), "TotalPrice cannot be negative.");
            _totalPrice = totalPrice;
        }

        public void SetOrderDate(DateTime orderDate)
        {
            if (orderDate == default)
                throw new ArgumentNullException(nameof(orderDate), "OrderDate must be provided.");
            _orderDate = orderDate;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }

    public enum OrderStatus
    {
        Pending = 0,
        Processing = 1,
        Completed = 2,
        Cancelled = 3
    }
}
