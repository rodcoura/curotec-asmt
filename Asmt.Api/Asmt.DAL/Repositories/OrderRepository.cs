using Asmt.DAL.Context;
using Asmt.DAL.Models;

namespace Asmt.DAL.Repositories;

/// <inheritdoc/>
public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(AsmtDBContext context) : base(context) { }
} 