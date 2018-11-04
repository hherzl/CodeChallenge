using API.Core.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.UnitTests.Mockers
{
    public static class DbContextOptionsMocker
    {
        public static DbContextOptions<StoreDbContext> GetDbOptions(string dbName)
            => new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
    }
}
