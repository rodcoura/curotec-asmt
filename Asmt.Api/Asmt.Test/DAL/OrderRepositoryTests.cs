using Asmt.DAL.Interfaces;
using Asmt.DAL.Repositories;
using Asmt.Test.DAL.Fixtures;
using Asmt.DAL.Models;

namespace Asmt.Test.DAL;

[TestFixture]
public class OrderRepositoryTests : DBContextFixture
{
    private IOrderRepository _repository;

    [SetUp]
    public void Setup()
    {
        _repository = new OrderRepository(_context);

        _context.Customers.Add(new Customer { Name = "John Doe" });
        _context.Customers.Add(new Customer { Name = "Jane Doe" });
        _context.SaveChanges();
    }

    [Test]
    public async Task CreateOrder_WithValidData_ShouldSucceed()
    {
        // Arrange
        Order order = new()
        {
            CustomerId = 1,
            PricePreTax = 100.00m,
            Tax = 10.00m,
            Status = OrderStatusType.Pending
        };

        // Act
        Order result = await _repository.AddAsync(order);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.GreaterThan(0));
        Assert.That(result.CreateDT, Is.Not.EqualTo(default(DateTime)));
        Assert.That(result.UpdateDT, Is.Null);
    }

    [Test]
    public async Task GetOrder_WithValidId_ShouldReturnOrder()
    {
        // Arrange
        Order order = new()
        {
            CustomerId = 1,
            PricePreTax = 100.00m,
            Tax = 10.00m,
            Status = OrderStatusType.Pending
        };
        Order created = await _repository.AddAsync(order);

        // Act
        Order result = await _repository.GetByIdAsync(created.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(created.Id));
        Assert.That(result.PricePreTax, Is.EqualTo(100.00m));
        Assert.That(result.Tax, Is.EqualTo(10.00m));
        Assert.That(result.Price, Is.EqualTo(110.00m));
    }

    [Test]
    public async Task UpdateOrder_WithValidData_ShouldSucceed()
    {
        // Arrange
        Order order = new()
        {
            CustomerId = 1,
            PricePreTax = 100.00m,
            Tax = 10.00m,
            Status = OrderStatusType.Pending
        };

        Order created = await _repository.AddAsync(order);

        created.PricePreTax = 150.00m;
        created.Tax = 15.00m;
        created.Status = OrderStatusType.Completed;

        // Act
        Order result = await _repository.UpdateAsync(created);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.PricePreTax, Is.EqualTo(150.00m));
        Assert.That(result.Tax, Is.EqualTo(15.00m));
        Assert.That(result.Price, Is.EqualTo(165.00m));
        Assert.That(result.Status, Is.EqualTo(OrderStatusType.Completed));
        Assert.That(result.UpdateDT, Is.Not.Null);
    }

    [Test]
    public async Task DeleteOrder_WithValidId_ShouldSucceed()
    {
        // Arrange
        Order order = new()
        {
            CustomerId = 1,
            PricePreTax = 100.00m,
            Tax = 10.00m,
            Status = OrderStatusType.Pending
        };

        Order created = await _repository.AddAsync(order);

        // Act
        await _repository.DeleteAsync(created.Id);
        Order result = await _repository.GetByIdAsync(created.Id);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAllOrders_ShouldReturnAllOrders()
    {
        // Arrange
        Order[] orders = new[]
        {
            new Order
            {
                CustomerId = 1,
                PricePreTax = 100.00m,
                Tax = 10.00m,
                Status = OrderStatusType.Pending
            },
            new Order
            {
                CustomerId = 2,
                PricePreTax = 200.00m,
                Tax = 20.00m,
                Status = OrderStatusType.Completed
            }
        };

        foreach (Order? order in orders)
        {
            await _repository.AddAsync(order);
        }

        // Act
        IEnumerable<Order> result = await _repository.GetByAsync(o => o);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.GreaterThanOrEqualTo(2));
    }
}
