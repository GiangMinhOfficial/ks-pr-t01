using PFR_NEFC_KS_Practice_T01.Infrastructure.Context;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Repository;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Repository.IRepository;

namespace PFR_NEFC_KS_Practice_T01.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(_context);
            OrderRepository = new OrderRepository(_context);
            OrderItemRepository = new OrderItemRepository(_context);
        }

        public IProductRepository ProductRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IOrderItemRepository OrderItemRepository { get; set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> SaveChange()
        {
            return _context.SaveChangesAsync();
        }
    }
}
