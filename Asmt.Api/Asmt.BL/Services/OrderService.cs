using System;
using Asmt.BL.Interfaces;
using Asmt.DAL.Interfaces;
using Asmt.BL.DTOs;
using Asmt.DAL.Models;
using FluentValidation;
using FluentValidation.Results;
using Asmt.BL.Exceptions;

namespace Asmt.BL.Services;

/// <inheritdoc/>
public class OrderService : IOrderService
{
    /// <summary>
    /// The order repository.
    /// </summary>
    private readonly IOrderRepository _orderRepository;

    /// <summary>
    /// Constructor for the order service.
    /// </summary>
    /// <param name="orderRepository">The order repository.</param>
    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    /// <inheritdoc/>
    public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto, CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await orderDto.ValidateAsync(cancellationToken);
        if (!validationResult.IsValid)
            throw new AsmtException("Validation failed", AsmtExceptionType.ValidationError, new ValidationException(validationResult.Errors));

        Order order = new()
        {
            CustomerId = orderDto.CustomerId,
            PricePreTax = orderDto.PricePreTax,
            Tax = orderDto.Tax,
            Status = orderDto.Status,
            OrderItems = orderDto.OrderItems.Select(item => new OrderItem
            {
                Price = item.Price,
                OrderId = item.OrderId
            }).ToList()
        };

        Order createdOrder = await _orderRepository.AddAsync(order);
        return MapToDto(createdOrder);
    }

    /// <inheritdoc/>
    public async Task<OrderDto> UpdateOrderAsync(OrderDto orderDto, CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await orderDto.ValidateAsync(cancellationToken);
        if (!validationResult.IsValid)
            throw new AsmtException("Validation failed", AsmtExceptionType.ValidationError, new ValidationException(validationResult.Errors));

        Order existingOrder = await _orderRepository.GetByIdAsync(orderDto.Id) ?? throw new AsmtException($"Order with ID {orderDto.Id} not found", AsmtExceptionType.NotFound);

        existingOrder.PricePreTax = orderDto.PricePreTax;
        existingOrder.Tax = orderDto.Tax;
        existingOrder.Status = orderDto.Status;
        existingOrder.CustomerId = orderDto.CustomerId;

        // Update existing order items and add new ones
        var existingItemIds = existingOrder.OrderItems.Select(i => i.Id).ToList();
        var updatedItemIds = orderDto.OrderItems.Where(i => i.Id != 0).Select(i => i.Id).ToList();

        // Remove items that are no longer in the updated order
        existingOrder.OrderItems.RemoveAll(item => !updatedItemIds.Contains(item.Id));

        // Update existing items and add new ones
        foreach (var itemDto in orderDto.OrderItems)
        {
            if (itemDto.Id != 0 && existingItemIds.Contains(itemDto.Id))
            {
                // Update existing item
                var existingItem = existingOrder.OrderItems.First(i => i.Id == itemDto.Id);
                existingItem.Price = itemDto.Price;
            }
            else
            {
                // Add new item
                existingOrder.OrderItems.Add(new OrderItem
                {
                    Price = itemDto.Price,
                    OrderId = existingOrder.Id
                });
            }
        }

        Order updatedOrder = await _orderRepository.UpdateAsync(existingOrder);
        return MapToDto(updatedOrder);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        return await _orderRepository.DeleteAsync(orderId);
    }

    /// <inheritdoc/>
    public async Task<OrderDto> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken = default)
    {
        Order order = await _orderRepository.GetByIdAsync(orderId) ?? throw new AsmtException($"Order with ID {orderId} not found", AsmtExceptionType.NotFound);
        return MapToDto(order);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Order> orders = await _orderRepository.GetByAsync(o => o);
        return orders.Select(MapToDto);
    }

    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            PricePreTax = order.PricePreTax,
            Tax = order.Tax,
            Status = order.Status,
            CustomerId = order.CustomerId,
            OrderItems = order.OrderItems.Select(item => new OrderItemDto
            {
                Id = item.Id,
                Price = item.Price,
                OrderId = item.OrderId
            }).ToList()
        };
    }
}
