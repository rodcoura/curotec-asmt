using Asmt.DAL.Context;
using Asmt.DAL.Interfaces;
using Asmt.DAL.Models;

namespace Asmt.DAL.Repositories;

/// <inheritdoc/>
public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AsmtDBContext context) : base(context) { }
} 