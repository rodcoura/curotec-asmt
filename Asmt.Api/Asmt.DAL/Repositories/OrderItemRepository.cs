namespace Asmt.DAL.Repositories;

using Asmt.DAL.Context;
using Asmt.DAL.Models;

/// <inheritdoc/>
public class OrderItemRepository : GenericRepository<OrderItem>
{
    public OrderItemRepository(AsmtDBContext context) : base(context) { }
} 