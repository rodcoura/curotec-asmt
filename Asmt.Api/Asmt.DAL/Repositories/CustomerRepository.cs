namespace Asmt.DAL.Repositories;

using Asmt.DAL.Context;
using Asmt.DAL.Interfaces;
using Asmt.DAL.Models; 

/// <inheritdoc/>
public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AsmtDBContext context) : base(context) { }
} 