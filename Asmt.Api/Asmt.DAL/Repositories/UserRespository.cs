using Asmt.DAL.Context;
using Asmt.DAL.Interfaces;
using Asmt.DAL.Models;

namespace Asmt.DAL.Repositories;

/// <inheritdoc/>
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AsmtDBContext context) : base(context) { }
}
