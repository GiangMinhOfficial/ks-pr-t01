using PFR_NEFC_KS_Practice_T01.Infrastructure.Context;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Entity;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Repository.IRepository;

namespace PFR_NEFC_KS_Practice_T01.Infrastructure.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
