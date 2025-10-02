using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class DeleteOrderService : IDeleteOrderService
    {
        private readonly IOrderRepository _repository;

        public DeleteOrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Order order)
        {
            _repository.Delete(order);
        }

        public void DeleteAll()
        {
            _repository.DeleteAll();
        }
    }
}
