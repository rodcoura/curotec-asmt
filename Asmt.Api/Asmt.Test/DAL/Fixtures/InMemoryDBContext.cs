using System;
using Asmt.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Asmt.Test.DAL.Fixtures;

/// <summary>
/// Factory for creating an in-memory database context.
/// </summary>
public static class InMemoryDBContextFactory
{
    /// <summary>
    /// Creates an in-memory database context.
    /// </summary>
    /// <param name="databaseName">The name of the database to create.</param>
    /// <returns>An in-memory database context.</returns>
    public static AsmtDBContext CreateAsmtDBContext(string databaseName)
    {
        DbContextOptions<AsmtDBContext> options = new DbContextOptionsBuilder<AsmtDBContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        return new AsmtDBContext(options);
    }
}
