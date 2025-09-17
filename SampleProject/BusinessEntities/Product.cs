using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private string _name;
        private string _description;
        private decimal _price;
        private int _stockQuantity;
        private readonly List<string> _tags = new List<string>();

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string Description
        {
            get => _description;
            private set => _description = value;
        }

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            private set => _stockQuantity = value;
        }

        public IEnumerable<string> Tags
        {
            get => _tags;
            private set => _tags.Initialize(value);
        }

        // Methods to update properties
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name is required.");
            _name = name;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public void SetPrice(decimal price)
        {
            if (price < 0) throw new ArgumentOutOfRangeException("Price cannot be negative.");
            _price = price;
        }

        public void SetStockQuantity(int quantity)
        {
            if (quantity < 0) throw new ArgumentOutOfRangeException("StockQuantity cannot be negative.");
            _stockQuantity = quantity;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }
}
