using Asmt.DAL.Context;

namespace Asmt.Test.DAL.Fixtures;

[TestFixture]
public abstract class DBContextFixture
{
    protected AsmtDBContext _context;

    [SetUp]
    public void SetupFixture()
    {
        _context = InMemoryDBContextFactory.CreateAsmtDBContext(Guid.NewGuid().ToString());
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}
