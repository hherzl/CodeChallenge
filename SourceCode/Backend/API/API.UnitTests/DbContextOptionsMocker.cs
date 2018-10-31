using API.Core.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.UnitTests
{
    public static class DbContextOptionsMocker
    {
        public static DbContextOptions<CodeChallengeDbContext> GetDbOptions(string dbName)
            => new DbContextOptionsBuilder<CodeChallengeDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
    }
}
