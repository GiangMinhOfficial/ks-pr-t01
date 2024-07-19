using PFR_NEFC_KS_Practice_T01.Infrastructure.Entity;

namespace PFR_NEFC_KS_Practice_T01.Domain.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
