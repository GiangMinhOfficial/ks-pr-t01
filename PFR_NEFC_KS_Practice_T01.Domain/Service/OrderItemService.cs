using PFR_NEFC_KS_Practice_T01.Domain.Service.IService;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Entity;
using PFR_NEFC_KS_Practice_T01.Infrastructure.UnitOfWork;

namespace PFR_NEFC_KS_Practice_T01.Domain.Service
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            Product product = await _unitOfWork.ProductRepository.GetByIdAsync(orderItem.ProductId);
            product.Stock = product.Stock - orderItem.Quantity;
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChange();

            await _unitOfWork.OrderItemRepository.AddAsync(orderItem);
            await _unitOfWork.SaveChange();
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
            {
                return;
            }
            await _unitOfWork.OrderItemRepository.RemoveAsync(orderItem);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _unitOfWork.OrderItemRepository.GetAllAsync();
        }

        public async Task<OrderItem?> GetOrderItemByIdAsync(int id)
        {
            return await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(orderItem.ProductId);
            var orderDetail = await _unitOfWork.OrderItemRepository.GetByIdAsync(orderItem.Id);
            product.Stock = product.Stock - orderItem.Quantity + orderDetail.Quantity;
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChange();

            await _unitOfWork.OrderItemRepository.UpdateAsync(orderItem);
            await _unitOfWork.SaveChange();
        }
    }
}
