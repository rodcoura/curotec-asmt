using Microsoft.AspNetCore.Mvc;
using Asmt.BL.Interfaces;
using Asmt.BL.DTOs;
using Asmt.BL.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using Asmt.Main.Common;
using Microsoft.AspNetCore.Authorization;

namespace Asmt.Main.Controllers;

/// <summary>
/// Controller for managing orders.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : AsmtControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMemoryCache _cache;
    private const string AllOrdersCacheKey = "AllOrders";
    private const string OrderByIdCacheKey = "Order_{0}"; // {0} will be replaced with orderId

    /// <summary>
    /// Constructor for the OrdersController.
    /// </summary>
    /// <param name="orderService">The order service.</param>
    /// <param name="cache">The cache.</param>
    /// <param name="logger">The logger.</param>
    public OrdersController(IOrderService orderService, IMemoryCache cache, ILogger<OrdersController> logger) : base(logger)
    {
        _orderService = orderService;
        _cache = cache;
    }

    /// <summary>
    /// Get all orders.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The orders.</returns>  
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllOrdersAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (_cache.TryGetValue(AllOrdersCacheKey, out IEnumerable<OrderDto> cachedOrders))
            {
                return Ok(cachedOrders);
            }

            IEnumerable<OrderDto> orders = await _orderService.GetAllOrdersAsync(cancellationToken);

            MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(AsmtOpts.Cache.SlidingDuration)
                .SetAbsoluteExpiration(AsmtOpts.Cache.AbsoluteDuration);
            
            _cache.Set(AllOrdersCacheKey, orders, cacheOptions);
            
            return Ok(orders);
        }
        catch (AsmtException ex)
        {
            return HandleAsmtException(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all orders");
            return StatusCode(500, "An unexpected error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Get an order by ID.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The order.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            string cacheKey = string.Format(OrderByIdCacheKey, id);
            if (_cache.TryGetValue(cacheKey, out OrderDto cachedOrder))
            {
                return Ok(cachedOrder);
            }

            OrderDto order = await _orderService.GetOrderByIdAsync(id, cancellationToken);

            MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(AsmtOpts.Cache.SlidingDuration)
                .SetAbsoluteExpiration(AsmtOpts.Cache.AbsoluteDuration);
            
            _cache.Set(cacheKey, order, cacheOptions);
            
            return Ok(order);
        }
        catch (AsmtException ex)
        {
            return HandleAsmtException(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting order with ID {OrderId}", id);
            return StatusCode(500, "An unexpected error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Create an order.
    /// </summary>
    /// <param name="orderDto">The order to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created order.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOrderAsync([FromBody] OrderDto orderDto, CancellationToken cancellationToken)
    {
        try
        {
            OrderDto createdOrder = await _orderService.CreateOrderAsync(orderDto, cancellationToken);
            _cache.Remove(AllOrdersCacheKey); // Invalidate the all orders cache
            return Ok(createdOrder);
        }
        catch (AsmtException ex)
        {
            return HandleAsmtException(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating order");
            return StatusCode(500, "An unexpected error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Update an order.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    /// <param name="orderDto">The order to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated order.</returns>   
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOrderAsync(int id, [FromBody] OrderDto orderDto, CancellationToken cancellationToken)
    {
        try
        {
            if (id != orderDto.Id)
            {
                return BadRequest("The ID in the URL must match the ID in the request body.");
            }

            OrderDto updatedOrder = await _orderService.UpdateOrderAsync(orderDto, cancellationToken);
            
            // Invalidate both caches
            _cache.Remove(AllOrdersCacheKey);
            _cache.Remove(string.Format(OrderByIdCacheKey, id));
            
            return Ok(updatedOrder);
        }
        catch (AsmtException ex)
        {
            return HandleAsmtException(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating order with ID {OrderId}", id);
            return StatusCode(500, "An unexpected error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Delete an order.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deleted order.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteOrderAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            bool result = await _orderService.DeleteOrderAsync(id, cancellationToken);
            if (!result)
            {
                return NotFound($"Order with ID {id} not found");
            }

            // Invalidate both caches
            _cache.Remove(AllOrdersCacheKey);
            _cache.Remove(string.Format(OrderByIdCacheKey, id));
            
            return NoContent();
        }
        catch (AsmtException ex)
        {
            return HandleAsmtException(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting order with ID {OrderId}", id);
            return StatusCode(500, "An unexpected error occurred while processing your request.");
        }
    }
}
