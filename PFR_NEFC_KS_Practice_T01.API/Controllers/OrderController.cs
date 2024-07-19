using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PFR_NEFC_KS_Practice_T01.Domain.Dto;
using PFR_NEFC_KS_Practice_T01.Domain.Service.IService;
using PFR_NEFC_KS_Practice_T01.Infrastructure.Entity;

namespace PFR_NEFC_KS_Practice_T01.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
                return Ok(ordersDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                var orderDto = _mapper.Map<OrderDto>(order);
                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderDto orderDto)
        {
            try
            {
                if (orderDto == null)
                {
                    return BadRequest("Order is null");
                }
                if (await _orderService.GetOrderByIdAsync(orderDto.Id) != null)
                {
                    return BadRequest("Order already exists");
                }
                var order = _mapper.Map<Order>(orderDto);
                await _orderService.AddOrderAsync(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDto orderDto)
        {
            try
            {
                if (orderDto == null || id != orderDto.Id)
                {
                    return BadRequest("Order is null");
                }
                var order = _mapper.Map<Order>(orderDto);
                await _orderService.UpdateOrderAsync(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                if (await _orderService.GetOrderByIdAsync(id) == null)
                {
                    return NotFound();
                }
                await _orderService.DeleteOrderAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
