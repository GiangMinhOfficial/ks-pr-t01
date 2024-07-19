using PFR_NEFC_KS_Practice_T01.Infrastructure.Context;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Entity;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Repository.IRepository;

namespace PFR_NEFC_KS_Practice_T01.Infrastructure.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
