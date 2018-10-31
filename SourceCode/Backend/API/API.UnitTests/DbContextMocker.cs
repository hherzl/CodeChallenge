using API.Core.DataLayer;

namespace API.UnitTests
{
    public static class DbContextMocker
    {
        public static CodeChallengeDbContext GetCodeChallengeDbContext(string dbName)
        {
            var dbContext = new CodeChallengeDbContext(DbContextOptionsMocker.GetDbOptions(dbName));

            dbContext.SeedInMemory();

            return dbContext;
        }
    }
}
