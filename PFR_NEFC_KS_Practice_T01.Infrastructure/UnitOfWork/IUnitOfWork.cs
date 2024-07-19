using PFR_NEFC_KS_Practice_T01.Infrastructure.Repository.IRepository;

namespace PFR_NEFC_KS_Practice_T01.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        Task<int> SaveChange();
    }
}
