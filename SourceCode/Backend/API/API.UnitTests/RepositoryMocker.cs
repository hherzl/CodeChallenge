using API.Core.DataLayer;
using API.Core.DataLayer.Contracts;
using API.Core.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.UnitTests
{
    public static class RepositoryMocker
    {
        public static IWarehouseRepository GetWarehouseRepository(string dbName)
        {
            var options = new DbContextOptionsBuilder<CodeChallengeDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var dbContext = new CodeChallengeDbContext(options);

            dbContext.SeedInMemory();

            return new WarehouseRepository(dbContext);
        }
    }
}
