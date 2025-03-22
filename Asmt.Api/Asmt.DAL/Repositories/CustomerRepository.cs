namespace Asmt.DAL.Repositories;

using Asmt.DAL.Context;
using Asmt.DAL.Models; 

/// <inheritdoc/>
public class CustomerRepository : GenericRepository<Customer>
{
    public CustomerRepository(AsmtDBContext context) : base(context) { }
} 