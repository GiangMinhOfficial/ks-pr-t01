using PFR_NEFC_KS_Practice_T01.Domain.Service.IService;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Entity;
using PFR_NEFC_KS_Practice_T01.Infrastructure.UnitOfWork;

namespace PFR_NEFC_KS_Practice_T01.Domain.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveChange();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return;
            }
            await _unitOfWork.ProductRepository.RemoveAsync(product);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChange();
        }
    }
}
