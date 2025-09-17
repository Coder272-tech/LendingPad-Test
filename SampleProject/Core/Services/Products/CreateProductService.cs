using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using static Core.Exceptions.ProductExceptions;

namespace Core.Services.Products
{
    [AutoRegister]
    public class CreateProductService : ICreateProductService
    {
        private readonly IProductRepository _repository;
        private readonly IIdObjectFactory<Product> _productFactory;
        private readonly IUpdateProductService _updateService;

        public CreateProductService(IIdObjectFactory<Product> productFactory, IProductRepository repository, IUpdateProductService updateService)
        {
            _repository = repository;
            _updateService = updateService;
            _productFactory = productFactory;
        }

        public Product Create(Guid id, string name, string description, decimal price, int stockQuantity, IEnumerable<string> tags)
        {
            if (_repository.Get(id) != null)
                throw new ProductAlreadyExistsException(id);

            // ✅ Create product via factory
            var product = _productFactory.Create(id);

            _updateService.Update(product, name, description, price, stockQuantity, tags);
            _repository.Save(product);
            return product;
        }
    }
}
