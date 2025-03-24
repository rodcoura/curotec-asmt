using Moq;
using Asmt.BL.Services;
using Asmt.BL.DTOs;
using Asmt.DAL.Models;
using Asmt.DAL.Interfaces;
using Asmt.BL.Exceptions;
using System.Linq.Expressions;

namespace Asmt.Test.BL;

[TestFixture]
public class OrderServiceTests
{
    private Mock<IOrderRepository> _mockOrderRepository;
    private OrderService _orderService;

    [SetUp]
    public void Setup()
    {
        _mockOrderRepository = new Mock<IOrderRepository>();
        _orderService = new OrderService(_mockOrderRepository.Object);
    }

    [Test]
    public async Task CreateOrderAsync_ValidOrder_ReturnsCreatedOrder()
    {
        // Arrange
        OrderDto orderDto = new()
        {
            CustomerId = 1,
            PricePreTax = 100,
            Tax = 10,
            Status = OrderStatusType.Pending,
            OrderItems = new List<OrderItemDto>
            {
                new() { Price = 50, OrderId = 1 }
            }
        };

        Order createdOrder = new()
        {
            Id = 1,
            CustomerId = orderDto.CustomerId,
            PricePreTax = orderDto.PricePreTax,
            Tax = orderDto.Tax,
            Status = orderDto.Status,
            OrderItems = new List<OrderItem>
            {
                new() { Id = 1, Price = 50, OrderId = 1 }
            }
        };

        _mockOrderRepository.Setup(r => r.AddAsync(It.IsAny<Order>(), CancellationToken.None))
            .ReturnsAsync(createdOrder);

        // Act
        OrderDto result = await _orderService.CreateOrderAsync(orderDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(createdOrder.Id));
        Assert.That(result.CustomerId, Is.EqualTo(createdOrder.CustomerId));
        Assert.That(result.PricePreTax, Is.EqualTo(createdOrder.PricePreTax));
        Assert.That(result.Tax, Is.EqualTo(createdOrder.Tax));
        Assert.That(result.OrderItems.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task CreateOrderAsync_InvalidOrder_ThrowsValidationException()
    {
        // Arrange
        OrderDto orderDto = new()
        {
            CustomerId = 0, // Invalid CustomerId
            PricePreTax = -100, // Invalid PricePreTax
            Tax = -10, // Invalid Tax
        };

        // Act & Assert
        AsmtException? ex = Assert.ThrowsAsync<AsmtException>(async () => 
            await _orderService.CreateOrderAsync(orderDto));
        Assert.That(ex.ExceptionType, Is.EqualTo(AsmtExceptionType.ValidationError));
    }

    [Test]
    public async Task GetOrderByIdAsync_ExistingOrder_ReturnsOrder()
    {
        // Arrange
        int orderId = 1;
        Order order = new()
        {
            Id = orderId,
            CustomerId = 1,
            PricePreTax = 100,
            Tax = 10,
            Status = OrderStatusType.Pending,
            OrderItems = new List<OrderItem>()
        };

        _mockOrderRepository.Setup(r => r.GetByIdAsync(orderId, CancellationToken.None))
            .ReturnsAsync(order);

        // Act
        OrderDto result = await _orderService.GetOrderByIdAsync(orderId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(orderId));
    }

    [Test]
    public async Task GetOrderByIdAsync_NonExistingOrder_ThrowsNotFoundException()
    {
        // Arrange
        int orderId = 999;
        _mockOrderRepository.Setup(r => r.GetByIdAsync(orderId, CancellationToken.None))
            .ReturnsAsync((Order?)null);

        // Act & Assert
        AsmtException? ex = Assert.ThrowsAsync<AsmtException>(async () => 
            await _orderService.GetOrderByIdAsync(orderId));
        Assert.That(ex.ExceptionType, Is.EqualTo(AsmtExceptionType.NotFound));
    }

    [Test]
    public async Task UpdateOrderAsync_ValidOrder_ReturnsUpdatedOrder()
    {
        // Arrange
        CancellationToken cancellationToken = default;
        int orderId = 1;

        Order existingOrder = new()
        {
            Id = orderId,
            CustomerId = 1,
            PricePreTax = 100,
            Tax = 10,
            Status = OrderStatusType.Completed,
            OrderItems = new List<OrderItem>
            {
                new() { Id = 1, Price = 75, OrderId = orderId }
            }
        };

        OrderDto updatedOrderDto = new()
        {
            Id = orderId,
            CustomerId = 1,
            PricePreTax = 150,
            Tax = 15,
            Status = OrderStatusType.Completed,
            OrderItems = new List<OrderItemDto>
            {
                new() { Id = 1, Price = 75, OrderId = orderId }
            }
        };

        Order updatedOrder = new()
        {
            Id = orderId,
            CustomerId = 1,
            PricePreTax = 150,
            Tax = 15,
            Status = OrderStatusType.Completed,
            OrderItems = new List<OrderItem>
            {
                new() { Id = 1, Price = 75, OrderId = orderId }
            }
        };

        _mockOrderRepository.Setup(r => r.GetByIdAsync(orderId, cancellationToken))
            .ReturnsAsync(existingOrder);
        _mockOrderRepository.Setup(r => r.UpdateAsync(It.IsAny<Order>(), cancellationToken))
            .ReturnsAsync(updatedOrder);

        // Act
        OrderDto result = await _orderService.UpdateOrderAsync(updatedOrderDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.PricePreTax, Is.EqualTo(updatedOrder.PricePreTax));
        Assert.That(result.Tax, Is.EqualTo(updatedOrder.Tax));
        Assert.That(result.Status, Is.EqualTo(updatedOrder.Status));
    }

    [Test]
    public async Task DeleteOrderAsync_ExistingOrder_ReturnsTrue()
    {
        // Arrange
        int orderId = 1;
        _mockOrderRepository.Setup(r => r.DeleteAsync(orderId, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        bool result = await _orderService.DeleteOrderAsync(orderId);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task GetAllOrdersAsync_ReturnsAllOrders()
    {
        // Arrange
        List<Order> orders = new()
        {
            new() { Id = 1, CustomerId = 1, PricePreTax = 100, Tax = 10 },
            new() { Id = 2, CustomerId = 2, PricePreTax = 200, Tax = 20 }
        };

        _mockOrderRepository.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<Order, Order>>>(), null, null, null, CancellationToken.None))
            .ReturnsAsync(orders);

        // Act
        IEnumerable<OrderDto> result = await _orderService.GetAllOrdersAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }
}


