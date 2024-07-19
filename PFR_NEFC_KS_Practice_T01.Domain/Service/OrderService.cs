using PFR_NEFC_KS_Practice_T01.Domain.Service.IService;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Entity;
using PFR_NEFC_KS_Practice_T01.Infrastructure.UnitOfWork;

namespace PFR_NEFC_KS_Practice_T01.Domain.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddOrderAsync(Order order)
        {
            if (order.TotalAmount != order.OrderItems.Count)
                throw new Exception();

            foreach (var orderItem in order.OrderItems)
            {
                Product product = await _unitOfWork.ProductRepository.GetByIdAsync(orderItem.ProductId);
                product.Stock = product.Stock - orderItem.Quantity;
                await _unitOfWork.ProductRepository.UpdateAsync(product);
                await _unitOfWork.SaveChange();
            }

            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.SaveChange();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return;
            }
            await _unitOfWork.OrderRepository.RemoveAsync(order);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _unitOfWork.OrderRepository.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _unitOfWork.OrderRepository.GetByIdAsync(id);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(orderItem.ProductId);
                var orderDetail = await _unitOfWork.OrderItemRepository.GetByIdAsync(orderItem.Id);
                product.Stock = product.Stock - orderItem.Quantity + orderDetail.Quantity;
                await _unitOfWork.ProductRepository.UpdateAsync(product);
                await _unitOfWork.SaveChange();
            }

            await _unitOfWork.OrderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChange();
        }
    }
}
