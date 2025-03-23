namespace Asmt.DAL.Repositories;

using Asmt.DAL.Context;
using Asmt.DAL.Interfaces;
using Asmt.DAL.Models;

/// <inheritdoc/>
public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AsmtDBContext context) : base(context) { }
} 