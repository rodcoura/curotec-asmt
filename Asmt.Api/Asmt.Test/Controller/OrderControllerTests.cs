using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Asmt.BL.Interfaces;
using Asmt.BL.DTOs;
using Asmt.BL.Exceptions;
using Asmt.Main.Controllers;

namespace Asmt.Test.Controllers;

[TestFixture]
public class OrdersControllerTests
{
    private Mock<IOrderService> _orderServiceMock;
    private Mock<ILogger<OrdersController>> _loggerMock;
    private IMemoryCache _cache;
    private OrdersController _controller;

    [SetUp]
    public void Setup()
    {
        _orderServiceMock = new Mock<IOrderService>();
        _loggerMock = new Mock<ILogger<OrdersController>>();
        _cache = new MemoryCache(new MemoryCacheOptions());
        _controller = new OrdersController(_orderServiceMock.Object, _cache, _loggerMock.Object);
    }

    [Test]
    public async Task GetAllOrdersAsync_ReturnsOkResult_WithOrders()
    {
        // Arrange
        List<OrderDto> expectedOrders = new()
        {
            new OrderDto { Id = 1, /* set other properties */ },
            new OrderDto { Id = 2, /* set other properties */ }
        };
        _orderServiceMock.Setup(x => x.GetAllOrdersAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedOrders);

        // Act
        IActionResult result = await _controller.GetAllOrdersAsync(CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        OkObjectResult okResult = (OkObjectResult)result;
        Assert.That(okResult.Value, Is.EqualTo(expectedOrders));
    }

    [Test]
    public async Task GetOrderByIdAsync_WithValidId_ReturnsOkResult()
    {
        // Arrange
        int orderId = 1;
        OrderDto expectedOrder = new() { Id = orderId, /* set other properties */ };
        _orderServiceMock.Setup(x => x.GetOrderByIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedOrder);

        // Act
        IActionResult result = await _controller.GetOrderByIdAsync(orderId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        OkObjectResult okResult = (OkObjectResult)result;
        Assert.That(okResult.Value, Is.EqualTo(expectedOrder));
    }

    [Test]
    public async Task CreateOrderAsync_WithValidOrder_ReturnsCreatedAtActionResult()
    {
        // Arrange
        OrderDto orderDto = new() { /* set properties */ };
        OrderDto createdOrder = new() { Id = 1, /* set properties */ };
        _orderServiceMock.Setup(x => x.CreateOrderAsync(orderDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdOrder);

        // Act
        IActionResult result = await _controller.CreateOrderAsync(orderDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        CreatedAtActionResult createdResult = (CreatedAtActionResult)result;
        Assert.That(createdResult.Value, Is.EqualTo(createdOrder));
        Assert.That(createdResult.ActionName, Is.EqualTo(nameof(OrdersController.GetOrderByIdAsync)));
    }

    [Test]
    public async Task UpdateOrderAsync_WithValidOrder_ReturnsOkResult()
    {
        // Arrange
        int orderId = 1;
        OrderDto orderDto = new() { Id = orderId, /* set properties */ };
        OrderDto updatedOrder = new() { Id = orderId, /* set properties */ };
        _orderServiceMock.Setup(x => x.UpdateOrderAsync(orderDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedOrder);

        // Act
        IActionResult result = await _controller.UpdateOrderAsync(orderId, orderDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        OkObjectResult okResult = (OkObjectResult)result;
        Assert.That(okResult.Value, Is.EqualTo(updatedOrder));
    }

    [Test]
    public async Task DeleteOrderAsync_WithValidId_ReturnsNoContentResult()
    {
        // Arrange
        int orderId = 1;
        _orderServiceMock.Setup(x => x.DeleteOrderAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        IActionResult result = await _controller.DeleteOrderAsync(orderId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task GetOrderByIdAsync_WithNotFoundException_ReturnsNotFound()
    {
        // Arrange
        int orderId = 1;
        _orderServiceMock.Setup(x => x.GetOrderByIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AsmtException("Order not found", AsmtExceptionType.NotFound));

        // Act
        IActionResult result = await _controller.GetOrderByIdAsync(orderId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }
}