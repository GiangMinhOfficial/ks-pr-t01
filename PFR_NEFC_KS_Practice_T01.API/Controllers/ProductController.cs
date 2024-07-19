using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PFR_NEFC_KS_Practice_T01.Domain.Dto;
using PFR_NEFC_KS_Practice_T01.Domain.Service.IService;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Entity;

namespace PFR_NEFC_KS_Practice_T01.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                var productDto = _mapper.Map<ProductDto>(product);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return BadRequest("Product is null");
                }
                if (await _productService.GetProductByIdAsync(productDto.Id) != null)
                {
                    return BadRequest("Product already exists");
                }
                var product = _mapper.Map<Product>(productDto);
                await _productService.AddProductAsync(product);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            try
            {
                if (productDto == null || id != productDto.Id)
                {
                    return BadRequest("Product is null");
                }
                var product = _mapper.Map<Product>(productDto);
                await _productService.UpdateProductAsync(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (await _productService.GetProductByIdAsync(id) == null)
                {
                    return NotFound();
                }
                await _productService.DeleteProductAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
