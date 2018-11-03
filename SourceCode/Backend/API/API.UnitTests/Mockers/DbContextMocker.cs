using API.Core.DataLayer;

namespace API.UnitTests.Mockers
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
