using Asmt.DAL.Context;
using Asmt.DAL.Interfaces;
using Asmt.DAL.Models;

namespace Asmt.DAL.Repositories;

/// <inheritdoc/>
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AsmtDBContext context) : base(context) { }
} 