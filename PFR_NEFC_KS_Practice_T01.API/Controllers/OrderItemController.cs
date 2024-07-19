using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PFR_NEFC_KS_Practice_T01.Domain.Dto;
using PFR_NEFC_KS_Practice_T01.Domain.Service.IService;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Entity;

namespace PFR_NEFC_KS_Practice_T01.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IMapper _mapper;

        public OrderItemController(IOrderItemService orderItemService, IMapper mapper)
        {
            _orderItemService = orderItemService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems()
        {
            try
            {
                var orderItems = await _orderItemService.GetAllOrderItemsAsync();
                var orderItemsDto = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
                return Ok(orderItemsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            try
            {
                var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
                if (orderItem == null)
                {
                    return NotFound();
                }

                var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);
                return Ok(orderItemDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderItem(OrderItemDto orderItemDto)
        {
            try
            {
                if (orderItemDto == null)
                {
                    return BadRequest("OrderItem is null");
                }
                if (await _orderItemService.GetOrderItemByIdAsync(orderItemDto.Id) != null)
                {
                    return BadRequest("OrderItem already exists");
                }
                var orderItem = _mapper.Map<OrderItem>(orderItemDto);
                await _orderItemService.AddOrderItemAsync(orderItem);
                return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.Id }, orderItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItemDto orderItemDto)
        {
            try
            {
                if (orderItemDto == null || id != orderItemDto.Id)
                {
                    return BadRequest("OrderItem is null");
                }
                var orderItem = _mapper.Map<OrderItem>(orderItemDto);
                await _orderItemService.UpdateOrderItemAsync(orderItem);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            try
            {
                if (await _orderItemService.GetOrderItemByIdAsync(id) == null)
                {
                    return NotFound();
                }
                await _orderItemService.DeleteOrderItemAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
